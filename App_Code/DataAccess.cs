using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// DataAccess : class d'acces au données de la base SQL Server
/// </summary>
public class DataAccess
{
  /// <summary>
  /// connexion partagée par les objets C# de l'appli
  /// </summary>
  //private static SqlConnection theConnection = null;

  public DataAccess()
  {

  }

  /*** PROPERTIES ***/
  
  /// <summary>
  /// Récupère la chaine de connexion à la base depuis le paramètre
  /// ConnectionString de la section /configuration/connectionStrings/
  /// </summary>
  public static string ConnectionString
  {
    get
    {
      const string msgErreur = "ConnectionString configuration is missing from you web.config. It should contain  <connectionStrings> <add key=\"ConnectionString\" value=\"Server=(local);Integrated Security=True;Database=GestionLicences\" </connectionStrings>";
      if (ConfigurationManager.ConnectionStrings["ConnectionString"] == null)
        throw (new NullReferenceException(msgErreur));

      string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

      if (String.IsNullOrEmpty(connectionString))
        throw (new NullReferenceException(msgErreur));
      else
        return (connectionString);
    }
  }

  /// <summary>
  /// return the result of a SQL query as a DataReader.
  /// utiliser theReader.GetLong(theReader.GetOrdinal("FieldName"));
  /// </summary>
  /// <param name="SQLQuery">la requete a executer</param>
  /// <returns>un DataSet contenant les infos</returns>
  public static SqlDataReader OpenReader(string SQLQuery)
  {
    // la requete ne doit pas etre vide
    if (SQLQuery.Trim() == "")
    {
      throw (new ArgumentException("SQLQuery ne peut être vide", "SQLQuery"));
    }

    // convertie les " en '
    SQLQuery = SQLQuery.Replace("\"", "'");

    SqlConnection cn = GetConnection();

    SqlCommand theCommand = new SqlCommand(SQLQuery, cn);

    SqlDataReader theReader = theCommand.ExecuteReader(CommandBehavior.CloseConnection);

    return theReader;
  }

  /// <summary>
  /// renvoi la connexion à la base de données.
  /// cette connexion est réutilisée par tous les
  /// objets C#.
  /// </summary>
  /// <returns>la connexion ouverte</returns>
  public static SqlConnection GetConnection()
  {
    //
    // version optimisée : ne marche pas : on a droit qu'à un seul datareader par connection
    //
/*
    // crée l'objet si detruit par le garbage collector
    if (theConnection == null)
      theConnection = new SqlConnection(ConnectionString);

    // ouvre la connexion si elle est fermée
    if (theConnection.State != ConnectionState.Open)
    {
      theConnection.ConnectionString = ConnectionString;
      theConnection.Open();
    }

    return theConnection;*/
    
    //
    // version bourrin qui marche (pooling : bien fermer la connection apres usage)
    //

    SqlConnection cn = new SqlConnection(ConnectionString);

    // ouvre la connexion
    cn.Open();

    return cn;
  }

  /// <summary>
  /// Ajoute la valeur d'un paramètre d'une requète
  /// </summary>
  /// <param name="sqlCmd">la command concernée</param>
  /// <param name="paramId">le nom du paramètre ("@...")</param>
  /// <param name="sqlType">le type de donnée</param>
  /// <param name="paramSize">la taille (pour les chaines par exemple)</param>
  /// <param name="paramDirection">le sens</param>
  /// <param name="paramvalue">la valeur ou null</param>
  public static void AddParamToSQLCmd(SqlCommand sqlCmd,
                               string paramId,
                               SqlDbType sqlType,
                               int paramSize,
                               ParameterDirection paramDirection,
                               object paramvalue)
  {

    if (sqlCmd == null)
      throw (new ArgumentNullException("sqlCmd"));
    if (paramId == string.Empty)
      throw (new ArgumentOutOfRangeException("paramId"));

    SqlParameter newSqlParam = new SqlParameter();
    newSqlParam.ParameterName = paramId;
    newSqlParam.SqlDbType = sqlType;
    newSqlParam.Direction = paramDirection;

    if (paramSize > 0)
      newSqlParam.Size = paramSize;

    if (paramvalue != null)
      newSqlParam.Value = paramvalue;
    else
    {
      newSqlParam.IsNullable = true;
      newSqlParam.Value = DBNull.Value;
    }

    sqlCmd.Parameters.Add(newSqlParam);
  }

  /// <summary>
  /// Validation d'un utilisateur
  /// </summary>
  /// <param name="Name">nom</param>
  /// <param name="Password">mot de passe</param>
  /// <param name="IdUtilisateur">retour, id de l'utilisateur</param>
  /// <returns>true si OK</returns>
  public static bool VerifyUser(string Name, string Password, ref int IdUtilisateur)
  {
    // vérification des paramètres
    if (Name.Trim() == "")
    {
      throw (new ArgumentException("Name ne peut être vide", "Name"));
    }

    if (Password.Trim() == "")
    {
      throw (new ArgumentException("Password ne peut être vide", "Password"));
    }

    // recherche l'utilisateur
    SqlDataReader theData = OpenReader("SELECT * FROM Utilisateur");

    while (theData.Read() == true)
    {
      string NomPrenom = (string)theData["NomPrenom"];
      string MotDePasse = (string)theData["MotDePasse"];

      if (string.Compare(NomPrenom, Name, true) == 0
          && string.Compare(MotDePasse, Password, true) == 0)
      {
        IdUtilisateur = (int)theData["IdUtilisateur"];

        theData.Close();

        return true;
      }
    }

    theData.Close();
 
    return false;
  }


  /// <summary>
  /// enlève les caractères interdits d'un chaine de filtre/recherche
  /// </summary>
  /// <param name="filtre">la chaine filtrée</param>
  public static void RemoveInvalidChar(ref string filtre)
  {
    string fOut = "";

    foreach (char c in filtre)
    {
      if (char.IsLetterOrDigit(c) || c == ' ' || c == '.')
        fOut += c;
    }

    filtre = fOut;
  }

  /// <summary>
  /// rempli une liste déroulante avec les données d'une requète
  /// </summary>
  /// <param name="cbo">le combo à remplir</param>
  /// <param name="szSQL">la requete SQL</param>
  /// <param name="szDisplay">le champ à afficher</param>
  /// <param name="szId">la valeur correspondante</param>
  /// <param name="selectedValue">la valeur à sélectionner</param>
  /// <param name="bAddAucun">si true, ajoute "(aucun)"/"0"</param>
  /// <param name="szAucun">chaine de à afficher pour "(aucun)"</param>
  public static void FillCombo(DropDownList cbo, string szSQL, string szDisplay, string szId,
                        int selectedValue, bool bAddAucun, string szAucun)
  {
    // remplissage du combo contact technique
    SqlDataReader theData = OpenReader(szSQL);

    cbo.Items.Clear();

    if (bAddAucun)
      cbo.Items.Add(new ListItem(szAucun, "0"));

    while (theData.Read() == true)
    {
      cbo.Items.Add(new ListItem((string)theData[szDisplay], theData[szId].ToString()));
    }

    theData.Close();

    cbo.SelectedValue = selectedValue.ToString();
  }

  /// <summary>
  /// slectionne une ligne d'un gridview
  /// </summary>
  /// <param name="grd">gridview à parcourir</param>
  /// <param name="id">valeur de clé à rechercher</param>
  public static void SelectGridRow(GridView grd, int id)
  {
    for (int index = 0; index < grd.PageCount; index++)
    {
      grd.PageIndex = index;
      grd.DataBind();

      for (int row = 0; row < grd.Rows.Count; row++)
      {
        if (id == Convert.ToInt32(grd.DataKeys[row].Value.ToString()))
        {
          grd.SelectedIndex = row;
          return;
        }
      }
    }
  }

  public static bool Execute(string SQLQuery)
  {
    // la requete ne doit pas etre vide
    if (SQLQuery.Trim() == "")
    {
      throw (new ArgumentException("SQLQuery ne peut être vide", "SQLQuery"));
    }

    // convertie les " en '
    SQLQuery = SQLQuery.Replace("\"", "'");

    SqlConnection cn = GetConnection();

    SqlCommand theCommand = new SqlCommand(SQLQuery, cn);

    return theCommand.ExecuteNonQuery() != 0;
  }

}
