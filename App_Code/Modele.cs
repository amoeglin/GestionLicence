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
/// Summary description for Client
/// </summary>
public class Modele : DataAccess
{
  int m_IdModele;
  string m_Nom;
  string m_Titre;
  string m_Contenu;
  int m_IdTypeModele;

  DateTime m_DateCreation;
  int m_IdUtilisateur;
  DateTime m_DateModification;

  public Modele()
  {
    m_IdModele = 0;
    m_Nom = "";
    m_Titre = "";
    m_Contenu = "";
    m_IdTypeModele = 1; // avertissement

    m_DateCreation = DateTime.Now;
    m_IdUtilisateur = 0;
    m_DateModification = DateTime.Now;
  }

  public int IdModele
  {
    get { return m_IdModele; }
  }

  public string Nom
  {
    get { return m_Nom; }
    set { m_Nom = value; }
  }

  public string Titre
  {
    get { return m_Titre; }
    set { m_Titre = value; }
  }

  public string Contenu
  {
    get { return m_Contenu; }
    set { m_Contenu = value; }
  }

  public int IdTypeModele
  {
    get { return m_IdTypeModele; }
    set { m_IdTypeModele = value; }
  }

  public DateTime DateCreation
  {
    get { return m_DateCreation; }
  }

  public DateTime DateModification
  {
    get { return m_DateModification; }
  }

  public int IdUtilisateur
  {
    get { return m_IdUtilisateur; }
  }

  public bool Load(int idModele)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM Modele WHERE IdModele=" + idModele.ToString());

    if (theData.Read() == true)
    {
      m_IdModele = (int)theData["IdModele"];
      m_Nom = (string)theData["Nom"];
      m_Titre = (string)theData["Titre"];
      m_Contenu = (string)(theData["Contenu"] == DBNull.Value ? "" : theData["Contenu"]);
      m_IdTypeModele = (int)theData["IdTypeModele"];

      m_DateCreation = (DateTime)theData["DateCreation"];
      m_IdUtilisateur = (int)theData["IdUtilisateur"];
      m_DateModification = (DateTime)theData["DateModification"];

      theData.Close();

      return true;
    }

    theData.Close();

    return false;
  }

  public bool Save()
  {
    using (SqlConnection cn = GetConnection())
    {
      string sql;

      if (IdModele == 0)
      {
        sql = "INSERT INTO Modele(Nom, Titre, Contenu, IdTypeModele, DateCreation, DateModification, IdUtilisateur) "
              + " VALUES(@Nom, @Titre, @Contenu, @IdTypeModele, GETDATE(), GETDATE(), @IdUtilisateur)";
      }
      else
      {
        sql = "UPDATE Modele SET Nom=@Nom, Titre=@Titre, Contenu=@Contenu, IdTypeModele=@IdTypeModele, DateModification=GETDATE(), "
              + "IdUtilisateur=@IdUtilisateur WHERE IdModele=" + IdModele.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@Nom", SqlDbType.NText, Nom.Length, ParameterDirection.Input, Nom);
      AddParamToSQLCmd(theCommand, "@Titre", SqlDbType.NText, Titre.Length, ParameterDirection.Input, Titre);
      AddParamToSQLCmd(theCommand, "@Contenu", SqlDbType.NText, Contenu.Length, ParameterDirection.Input, Contenu);
      AddParamToSQLCmd(theCommand, "@IdTypeModele", SqlDbType.Int, 0, ParameterDirection.Input, IdTypeModele);
      AddParamToSQLCmd(theCommand, "@IdUtilisateur", SqlDbType.Int, 0, ParameterDirection.Input, 1);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Delete()
  {
    if (IdModele == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      // on détruit le modele.
      sql = "DELETE FROM Modele WHERE IdModele=@IdModele";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdModele", SqlDbType.Int, 0, ParameterDirection.Input, IdModele);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

}
