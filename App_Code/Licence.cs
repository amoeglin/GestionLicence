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
/// Summary description for Licence
/// </summary>
public class Licence : DataAccess
{
  int m_IdLicence;
  int m_IdClient;
  int m_IdContactTechnique;
  int m_IdContactAdministratif;
  int m_IdLogiciel;
  string m_NomSite;
  int m_NbLicence; // licence à autoriser
  int m_NbLicenceFacture; // licence à facturer
  DateTime m_DateExpiration;
  double m_PrixVente;
  double m_PrixMiseAJour;
  string m_Repertoire;
  bool m_Abandonne;
  string m_Commentaire;
  bool m_UseUNCPath;

  DateTime m_DateCreation;
  int m_IdUtilisateur;
  DateTime m_DateModification;

  public Licence()
  {
    m_IdLicence = 0;
    m_IdClient = 0;
    m_IdLogiciel = 0;
    m_IdContactTechnique = 0;
    m_IdContactAdministratif = 0;
    m_NomSite = "";
    m_NbLicence = 0;
    m_NbLicenceFacture = 0;
    m_DateExpiration = DateTime.Parse("31/12/" + DateTime.Now.Year.ToString());
    m_PrixVente = 0.0;
    m_PrixMiseAJour = 0.0;
    m_Repertoire = "";
    m_Abandonne = false;
    m_Commentaire = "";
    m_UseUNCPath = true;

    m_DateCreation = DateTime.Now;
    m_IdUtilisateur = 0;
    m_DateModification = DateTime.Now;
  }

  public int IdLicence
  {
    get { return m_IdLicence; }
  }

  public int IdClient
  {
    get { return m_IdClient; }
    set { m_IdClient = value; }
  }

  public int IdLogiciel
  {
    get { return m_IdLogiciel; }
    set { m_IdLogiciel = value; }
  }

  public int IdContactTechnique
  {
    get { return m_IdContactTechnique; }
    set { m_IdContactTechnique = value; }
  }

  public int IdContactAdministratif
  {
    get { return m_IdContactAdministratif; }
    set { m_IdContactAdministratif = value; }
  }

  public string NomSite
  {
    get { return m_NomSite; }
    set { m_NomSite = value; }
  }

  public int NbLicence
  {
    get { return m_NbLicence; }
    set { m_NbLicence = value; }
  }

  public int NbLicenceFacture
  {
    get { return m_NbLicenceFacture; }
    set { m_NbLicenceFacture = value; }
  }

  public DateTime DateExpiration
  {
    get { return m_DateExpiration; }
    set { m_DateExpiration = value; }
  }

  public double PrixVente
  {
    get { return m_PrixVente; }
    set { m_PrixVente = value; }
  }

  public double PrixMiseAJour
  {
    get { return m_PrixMiseAJour; }
    set { m_PrixMiseAJour = value; }
  }

  public string Repertoire
  {
    get { return m_Repertoire; }
    set { m_Repertoire = value; }
  }

  public bool Abandonne
  {
    get { return m_Abandonne; }
    set { m_Abandonne = value; }
  }

  public bool UseUNCPath
  {
    get { return m_UseUNCPath; }
    set { m_UseUNCPath = value; }
  }

  public string Commentaire
  {
    get { return m_Commentaire; }
    set { m_Commentaire = value; }
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

  public bool Load(int idLicence)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM Licence WHERE IdLicence=" + idLicence.ToString());

    if (theData.Read() == true)
    {
      m_IdLicence = (int)theData["IdLicence"];
      m_IdClient = (int)theData["IdClient"];
      m_IdLogiciel = (int)theData["IdLogiciel"];
      m_IdContactTechnique = (int)(theData["IdContactTechnique"] == DBNull.Value ? 0 : theData["IdContactTechnique"]);
      m_IdContactAdministratif = (int)(theData["IdContactAdministratif"] == DBNull.Value ? 0 : theData["IdContactAdministratif"]);
      m_NomSite = (string)theData["NomSite"];
      m_NbLicence = (int)theData["NbLicence"];
      m_NbLicenceFacture = (int)theData["NbLicenceFacture"];
      m_DateExpiration = (DateTime)theData["DateExpiration"];
      m_PrixVente = (double)(theData["PrixVente"] == DBNull.Value ? 0.0 : theData["PrixVente"]);
      m_PrixMiseAJour = (double)(theData["PrixMiseAJour"] == DBNull.Value ? 0.0 : theData["PrixMiseAJour"]);
      m_Repertoire = (string)(theData["Repertoire"] == DBNull.Value ? "" : theData["Repertoire"]);
      m_Abandonne = (bool)(theData["Abandonne"] == DBNull.Value ? false : theData["Abandonne"]);
      m_UseUNCPath = (bool)(theData["UseUNCPath"] == DBNull.Value ? true : theData["UseUNCPath"]);
      m_Commentaire = (string)(theData["Commentaire"] == DBNull.Value ? "" : theData["Commentaire"]);

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

      if (IdLicence == 0)
      {
        sql = "INSERT INTO Licence(IdClient, IdLogiciel, IdContactTechnique, IdContactAdministratif, NomSite, NbLicence, NbLicenceFacture, DateExpiration, PrixVente, PrixMiseAJour, Repertoire, Abandonne, UseUNCPath, Commentaire, DateCreation, DateModification, IdUtilisateur) "
              + " VALUES(@IdClient, @IdLogiciel, @IdContactTechnique, @IdContactAdministratif, @NomSite, @NbLicence, @NbLicenceFacture, @DateExpiration, @PrixVente, @PrixMiseAJour, @Repertoire, @Abandonne, @UseUNCPath, @Commentaire, GETDATE(), GETDATE(), @IdUtilisateur)";
      }
      else
      {
        sql = "UPDATE Licence SET IdClient=@IdClient, IdLogiciel=@IdLogiciel, IdContactTechnique=@IdContactTechnique, NomSite=@NomSite, "
              + " NbLicence=@NbLicence, NbLicenceFacture=@NbLicenceFacture, DateExpiration=@DateExpiration, PrixVente=@PrixVente, IdContactAdministratif=@IdContactAdministratif, "
              + " PrixMiseAJour=@PrixMiseAJour, Repertoire=@Repertoire, Abandonne=@Abandonne, UseUNCPath=@UseUNCPath, "
              + " Commentaire=@Commentaire, DateModification=GETDATE(), IdUtilisateur=@IdUtilisateur "
              + " WHERE IdLicence=" + IdLicence.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdClient", SqlDbType.Int, 0, ParameterDirection.Input, IdClient);
      AddParamToSQLCmd(theCommand, "@IdLogiciel", SqlDbType.Int, 0, ParameterDirection.Input, IdLogiciel);
      AddParamToSQLCmd(theCommand, "@IdContactTechnique", SqlDbType.Int, 0, ParameterDirection.Input, IdContactTechnique == 0 ? null : (object)IdContactTechnique);
      AddParamToSQLCmd(theCommand, "@NomSite", SqlDbType.NText, NomSite.Length, ParameterDirection.Input, NomSite);
      AddParamToSQLCmd(theCommand, "@NbLicence", SqlDbType.Int, 0, ParameterDirection.Input, NbLicence);
      AddParamToSQLCmd(theCommand, "@NbLicenceFacture", SqlDbType.Int, 0, ParameterDirection.Input, NbLicenceFacture);
      AddParamToSQLCmd(theCommand, "@DateExpiration", SqlDbType.DateTime, 0, ParameterDirection.Input, DateExpiration);
      AddParamToSQLCmd(theCommand, "@PrixVente", SqlDbType.Float, 0, ParameterDirection.Input, PrixVente);
      AddParamToSQLCmd(theCommand, "@IdContactAdministratif", SqlDbType.Int, 0, ParameterDirection.Input, IdContactAdministratif == 0 ? null : (object)IdContactAdministratif);
      AddParamToSQLCmd(theCommand, "@PrixMiseAJour", SqlDbType.Float, 0, ParameterDirection.Input, PrixMiseAJour);
      AddParamToSQLCmd(theCommand, "@Repertoire", SqlDbType.NText, Repertoire.Length, ParameterDirection.Input, Repertoire);
      AddParamToSQLCmd(theCommand, "@Abandonne", SqlDbType.Bit, 0, ParameterDirection.Input, Abandonne);
      AddParamToSQLCmd(theCommand, "@UseUNCPath", SqlDbType.Bit, 0, ParameterDirection.Input, UseUNCPath);
      AddParamToSQLCmd(theCommand, "@Commentaire", SqlDbType.NText, Commentaire.Length, ParameterDirection.Input, Commentaire);
      AddParamToSQLCmd(theCommand, "@IdUtilisateur", SqlDbType.Int, 0, ParameterDirection.Input, 1);

      theCommand.ExecuteNonQuery();

      // recupère l'IdLogiciel pour les options
      if (IdLicence == 0)
      {
        theCommand.CommandText = "SELECT @@Identity";

        m_IdLicence = (int)(decimal)theCommand.ExecuteScalar();
      }

      return true;
    }
  }

  public bool Delete()
  {
    if (IdLicence == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      // on marque le Licence comme supprimé plutot que de la détruire.
      sql = "UPDATE Licence SET Supprime=1 WHERE IdLicence=@IdLicence";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdLicence", SqlDbType.Int, 0, ParameterDirection.Input, IdLicence);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Undelete()
  {
    if (IdLicence == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      // on marque le Licence comme supprimé plutot que de la détruire.
      sql = "UPDATE Licence SET Supprime=0 WHERE IdLicence=@IdLicence";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdLicence", SqlDbType.Int, 0, ParameterDirection.Input, IdLicence);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

}
