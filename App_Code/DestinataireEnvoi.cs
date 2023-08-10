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
/// Summary description for DestinataireEnvoi
/// </summary>
public class DestinataireEnvoi : DataAccess
{
  int m_IdDestinataireEnvoi;
  int m_IdEnvoi;
  int m_IdContact;
  bool m_Traite;
  string m_Commentaire;

  public DestinataireEnvoi()
  {
    m_IdDestinataireEnvoi = 0;
    m_IdEnvoi = 0;
    m_IdContact = 0;
    m_Traite = false;
    m_Commentaire = "";
  }

  public int IdDestinataireEnvoi
  {
    get { return m_IdDestinataireEnvoi; }
  }

  public int IdEnvoi
  {
    get { return m_IdEnvoi; }
    set { m_IdEnvoi = value; }
  }

  public int IdContact
  {
    get { return m_IdContact; }
    set { m_IdContact = value; }
  }

  public bool Traite
  {
    get { return m_Traite; }
    set { m_Traite = value; }
  }

  public string Commentaire
  {
    get { return m_Commentaire; }
    set { m_Commentaire = value; }
  }

  public bool Load(int idDestinataireEnvoi)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM DestinataireEnvoi WHERE IdDestinataireEnvoi=" + idDestinataireEnvoi.ToString());

    if (theData.Read() == true)
    {
      m_IdDestinataireEnvoi = (int)theData["IdDestinataireEnvoi"];
      m_IdEnvoi = (int)theData["IdEnvoi"];
      m_IdContact = (int)theData["IdContact"];
      m_Traite = (bool)(theData["Traite"] == DBNull.Value ? false : theData["Traite"]);
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

      if (IdDestinataireEnvoi == 0)
      {
        sql = "INSERT INTO DestinataireEnvoi(IdEnvoi, IdContact, Traite, Commentaire) "
              + " VALUES(@IdEnvoi, @IdContact, @Traite, @Commentaire)";
      }
      else
      {
        sql = "UPDATE DestinataireEnvoi SET IdEnvoi=@IdEnvoi, IdContact=@IdContact, "
              + " Traite=@Traite, Commentaire=@Commentaire "
              + " WHERE IdDestinataireEnvoi=" + IdDestinataireEnvoi.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdEnvoi", SqlDbType.Int, 0, ParameterDirection.Input, IdEnvoi);
      AddParamToSQLCmd(theCommand, "@IdContact", SqlDbType.Int, 0, ParameterDirection.Input, IdContact);
      AddParamToSQLCmd(theCommand, "@Traite", SqlDbType.Bit, 0, ParameterDirection.Input, Traite);
      AddParamToSQLCmd(theCommand, "@Commentaire", SqlDbType.NText, Commentaire.Length, ParameterDirection.Input, Commentaire);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Delete()
  {
    if (IdDestinataireEnvoi == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      /*DestinataireEnvoi
      DestinataireDestinataireEnvoi*/

      // on supprime le destinataire
      sql = "DELETE FROM DestinataireEnvoi WHERE IdDestinataireEnvoi=@IdDestinataireEnvoi";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      AddParamToSQLCmd(theCommand, "@IdDestinataireEnvoi", SqlDbType.Int, 0, ParameterDirection.Input, IdDestinataireEnvoi);

      theCommand.ExecuteNonQuery();

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

}
