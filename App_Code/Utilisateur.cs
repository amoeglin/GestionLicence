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
public class Utilisateur : DataAccess
{
  public enum eDroit
  {
    eLectureSeule = 1,
    eModification = 2,
    eCréation = 4,
    eEnvois = 8,
    eSuppression = 16,
    eAdministration = 256
  };

  int m_IdUtilisateur;
  string m_NomPrenom;
  string m_MotDePasse;
  int m_Droits;
  string m_Commentaire;

  public Utilisateur()
  {
    m_IdUtilisateur = 0;
    m_NomPrenom = "";
    m_MotDePasse = "";
    m_Droits = 0;
    m_Commentaire = "";
  }

  public int IdUtilisateur
  {
    get { return m_IdUtilisateur; }
  }

  public string NomPrenom
  {
    get { return m_NomPrenom; }
    set { m_NomPrenom = value; }
  }

  public string MotDePasse
  {
    get { return m_MotDePasse; }
    set { m_MotDePasse = value; }
  }

  public int Droits
  {
    get { return m_Droits; }
    set { m_Droits = value; }
  }

  public string Commentaire
  {
    get { return m_Commentaire; }
    set { m_Commentaire = value; }
  }

  public bool Load(int idUtilisateur)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM Utilisateur WHERE IdUtilisateur=" + idUtilisateur.ToString());

    if (theData.Read() == true)
    {
      m_IdUtilisateur = (int)theData["IdUtilisateur"];
      m_NomPrenom = (string)theData["NomPrenom"];
      m_MotDePasse = (string)theData["MotDePasse"];
      m_Droits = (int)theData["Droits"];
      m_Commentaire = (string)(theData["Commentaire"] == DBNull.Value ? "" : theData["Commentaire"]);

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

      if (IdUtilisateur == 0)
      {
        sql = "INSERT INTO Utilisateur(NomPrenom, MotDePasse, Droits, Commentaire) "
              + " VALUES(@NomPrenom, @MotDePasse, @Droits, @Commentaire)";
      }
      else
      {
        sql = "UPDATE Utilisateur SET NomPrenom=@NomPrenom, MotDePasse=@MotDePasse, Droits=@Droits, "
              + " Commentaire=@Commentaire "
              + " WHERE IdUtilisateur=" + IdUtilisateur.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@NomPrenom", SqlDbType.NText, NomPrenom.Length, ParameterDirection.Input, NomPrenom);
      AddParamToSQLCmd(theCommand, "@MotDePasse", SqlDbType.NText, MotDePasse.Length, ParameterDirection.Input, MotDePasse);
      AddParamToSQLCmd(theCommand, "@Droits", SqlDbType.Int, 0, ParameterDirection.Input, Droits);
      AddParamToSQLCmd(theCommand, "@Commentaire", SqlDbType.NText, Commentaire.Length, ParameterDirection.Input, Commentaire);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Delete()
  {
    if (IdUtilisateur == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      /*Licence*/

      // on marque le client comme supprimé plutot que de la détruire.
      sql = "UPDATE Utilisateur SET Supprime=1 WHERE IdUtilisateur=@IdUtilisateur";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdUtilisateur", SqlDbType.Int, 0, ParameterDirection.Input, IdUtilisateur);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Undelete()
  {
    if (IdUtilisateur == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      // on marque le client comme supprimé plutot que de la détruire.
      sql = "UPDATE Utilisateur SET Supprime=0 WHERE IdUtilisateur=@IdUtilisateur";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdUtilisateur", SqlDbType.Int, 0, ParameterDirection.Input, IdUtilisateur);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public static bool UserHasRight(int UserRights, eDroit d)
  {
    return ((UserRights & (int)d) != 0);
  }

}
