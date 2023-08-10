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
public class Client : DataAccess
{
  int m_IdCLient;
  string m_NumeroClient;
  string m_RaisonSociale;
  string m_Commentaire;

  DateTime m_DateCreation;
  int m_IdUtilisateur;
  DateTime m_DateModification;

  public Client()
  {
    m_IdCLient = 0;
    m_NumeroClient = "";
    m_RaisonSociale = "";
    m_Commentaire = "";

    m_DateCreation = DateTime.Now;
    m_IdUtilisateur = 0;
    m_DateModification = DateTime.Now;
  }

  public int IdClient
  {
    get { return m_IdCLient; }
  }

  public string NumeroClient
  {
    get { return m_NumeroClient; }
    set { m_NumeroClient = value; }
  }

  public string RaisonSociale
  {
    get { return m_RaisonSociale; }
    set { m_RaisonSociale = value; }
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

  public bool Load(int idClient)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM Client WHERE IdClient=" + idClient.ToString());

    if (theData.Read() == true)
    {
      m_IdCLient = (int)theData["IdClient"];
      m_NumeroClient = (string)theData["NumeroClient"];
      m_RaisonSociale = (string)theData["RaisonSociale"];
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

      if (IdClient == 0)
      {
        sql = "INSERT INTO Client(NumeroClient, RaisonSociale, Commentaire, DateCreation, DateModification, IdUtilisateur) "
              + " VALUES(@NumeroClient, @RaisonSociale, @Commentaire, GETDATE(), GETDATE(), @IdUtilisateur)";
      }
      else
      {
        sql = "UPDATE Client SET NumeroClient=@NumeroClient, RaisonSociale=@RaisonSociale, "
              + " Commentaire=@Commentaire, DateModification=GETDATE(), IdUtilisateur=@IdUtilisateur "
              + " WHERE IdClient=" + IdClient.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@NumeroClient", SqlDbType.NText, NumeroClient.Length, ParameterDirection.Input, NumeroClient);
      AddParamToSQLCmd(theCommand, "@RaisonSociale", SqlDbType.NText, RaisonSociale.Length, ParameterDirection.Input, RaisonSociale);
      AddParamToSQLCmd(theCommand, "@Commentaire", SqlDbType.NText, Commentaire.Length, ParameterDirection.Input, Commentaire);
      AddParamToSQLCmd(theCommand, "@IdUtilisateur", SqlDbType.Int, 0, ParameterDirection.Input, 1);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Delete()
  {
    if (IdClient == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      /*Envois
      DestinataireEnvoi
      Contact
      Licence*/

      // on marque le client comme supprimé plutot que de la détruire.
      sql = "UPDATE Client SET Supprime=1 WHERE IdClient=@IdClient";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdClient", SqlDbType.Int, 0, ParameterDirection.Input, IdClient);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Undelete()
  {
    if (IdClient == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {

      string sql;

      // on marque le client comme supprimé plutot que de la détruire.
      sql = "UPDATE Client SET Supprime=0 WHERE IdClient=@IdClient";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdClient", SqlDbType.Int, 0, ParameterDirection.Input, IdClient);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

}
