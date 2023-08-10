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
/// Summary description for Contact
/// </summary>
public class Contact : DataAccess
{
  int m_IdContact;
  int m_IdClient;
  int m_IdTypeContact;
  int m_IdContactTechnique;
  string m_NomPrenom;
  string m_Adresse;
  string m_Telephone;
  string m_Fax;
  string m_Email;
  string m_Commentaire;

  DateTime m_DateCreation;
  int m_IdUtilisateur;
  DateTime m_DateModification;

  public Contact()
  {
    m_IdContact = 0;
    m_IdClient = 0;
    m_IdTypeContact = 4; // utilisateur
    m_IdContactTechnique = 0;
    m_NomPrenom = "";
    m_Adresse = "";
    m_Telephone = "";
    m_Fax = "";
    m_Email = "";
    m_Commentaire = "";

    m_DateCreation = DateTime.Now;
    m_IdUtilisateur = 0;
    m_DateModification = DateTime.Now;
  }

  public int IdContact
  {
    get { return m_IdContact; }
  }

  public int IdClient
  {
    get { return m_IdClient; }
    set { m_IdClient = value; }
  }

  public int IdTypeContact
  {
    get { return m_IdTypeContact; }
    set { m_IdTypeContact = value; }
  }

  public int IdContactTechnique
  {
    get { return m_IdContactTechnique; }
    set { m_IdContactTechnique = value; }
  }

  public string NomPrenom
  {
    get { return m_NomPrenom; }
    set { m_NomPrenom = value; }
  }

  public string Adresse
  {
    get { return m_Adresse; }
    set { m_Adresse = value; }
  }

  public string Telephone
  {
    get { return m_Telephone; }
    set { m_Telephone = value; }
  }

  public string Fax
  {
    get { return m_Fax; }
    set { m_Fax = value; }
  }

  public string Email
  {
    get { return m_Email; }
    set { m_Email = value; }
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

  public bool Load(int idContact)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM Contact WHERE IdContact=" + idContact.ToString());

    if (theData.Read() == true)
    {
      m_IdContact = (int)theData["IdContact"];
      m_IdClient = (int)theData["IdClient"];
      m_IdTypeContact = (int)theData["IdTypeContact"];
      m_IdContactTechnique = (int)(theData["IdContactTechnique"] == DBNull.Value ? 0 : theData["IdContactTechnique"]);
      m_NomPrenom = (string)theData["NomPrenom"];
      m_Adresse = (string)(theData["Adresse"] == DBNull.Value ? "" : theData["Adresse"]);
      m_Telephone = (string)(theData["Telephone"] == DBNull.Value ? "" : theData["Telephone"]);
      m_Fax = (string)(theData["Fax"] == DBNull.Value ? "" : theData["Fax"]);
      m_Email = (string)(theData["Email"] == DBNull.Value ? "" : theData["Email"]);
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

      if (IdContact == 0)
      {
        sql = "INSERT INTO Contact(IdClient, IdTypeContact, IdContactTechnique, NomPrenom, Adresse, Telephone, Fax, Email, Commentaire, DateCreation, DateModification, IdUtilisateur) "
              + " VALUES(@IdClient, @IdTypeContact, @IdContactTechnique, @NomPrenom, @Adresse, @Telephone, @Fax, @Email, @Commentaire, GETDATE(), GETDATE(), @IdUtilisateur)";
      }
      else
      {
        sql = "UPDATE Contact SET IdClient=@IdClient, IdTypeContact=@IdTypeContact, IdContactTechnique=@IdContactTechnique, "
              + " NomPrenom=@NomPrenom, Adresse=@Adresse, Telephone=@Telephone, Fax=@Fax, Email=@Email, "
              + " Commentaire=@Commentaire, DateModification=GETDATE(), IdUtilisateur=@IdUtilisateur "
              + " WHERE IdContact=" + IdContact.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdClient", SqlDbType.Int, 0, ParameterDirection.Input, IdClient);
      AddParamToSQLCmd(theCommand, "@IdTypeContact", SqlDbType.Int, 0, ParameterDirection.Input, IdTypeContact);
      AddParamToSQLCmd(theCommand, "@IdContactTechnique", SqlDbType.Int, 0, ParameterDirection.Input, IdContactTechnique == 0 ? DBNull.Value : (object)IdContactTechnique);
      AddParamToSQLCmd(theCommand, "@NomPrenom", SqlDbType.NText, NomPrenom.Length, ParameterDirection.Input, NomPrenom);
      AddParamToSQLCmd(theCommand, "@Adresse", SqlDbType.NText, Adresse.Length, ParameterDirection.Input, Adresse);
      AddParamToSQLCmd(theCommand, "@Telephone", SqlDbType.NText, Telephone.Length, ParameterDirection.Input, Telephone);
      AddParamToSQLCmd(theCommand, "@Fax", SqlDbType.NText, Fax.Length, ParameterDirection.Input, Fax);
      AddParamToSQLCmd(theCommand, "@Email", SqlDbType.NText, Email.Length, ParameterDirection.Input, Email);
      AddParamToSQLCmd(theCommand, "@Commentaire", SqlDbType.NText, Commentaire.Length, ParameterDirection.Input, Commentaire);
      AddParamToSQLCmd(theCommand, "@IdUtilisateur", SqlDbType.Int, 0, ParameterDirection.Input, 1);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Delete()
  {
    if (IdContact == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      /*Envois
      DestinataireEnvoi
      Contact
      Licence*/

      // on marque le Contact comme supprimé plutot que de la détruire.
      sql = "UPDATE Contact SET Supprime=1 WHERE IdContact=@IdContact";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdContact", SqlDbType.Int, 0, ParameterDirection.Input, IdContact);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Undelete()
  {
    if (IdContact == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      // on marque le Contact comme supprimé plutot que de la détruire.
      sql = "UPDATE Contact SET Supprime=0 WHERE IdContact=@IdContact";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdContact", SqlDbType.Int, 0, ParameterDirection.Input, IdContact);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

}
