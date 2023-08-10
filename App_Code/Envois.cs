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
/// Summary description for Envoi
/// </summary>
public class Envoi : DataAccess
{
  int m_IdEnvoi;
  int m_IdClient;
  int m_IdLogiciel;
  DateTime m_DateEnvoi;
  string m_FichierEnvoye;
  string m_TitreEnvoi;
  string m_TexteEnvoi;
  bool m_GardeTrace;
  bool m_AvertissementRenouvellement;
  string m_Commentaire;

  DateTime m_DateCreation;
  int m_IdUtilisateur;
  DateTime m_DateModification;

  public Envoi()
  {
    m_IdEnvoi = 0;
    m_IdClient = 0;
    m_IdLogiciel = 0;
    m_DateEnvoi = DateTime.Now;
    m_FichierEnvoye = "";
    m_TitreEnvoi = "";
    m_TexteEnvoi = "";
    m_GardeTrace = false;
    m_AvertissementRenouvellement = false;
    m_Commentaire = "";

    m_DateCreation = DateTime.Now;
    m_IdUtilisateur = 0;
    m_DateModification = DateTime.Now;
  }

  public int IdEnvoi
  {
    get { return m_IdEnvoi; }
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

  public DateTime DateEnvoi
  {
    get { return m_DateEnvoi; }
    set { m_DateEnvoi = value; }
  }

  public string FichierEnvoye
  {
    get { return m_FichierEnvoye; }
    set { m_FichierEnvoye = value; }
  }

  public string TitreEnvoi
  {
    get { return m_TitreEnvoi; }
    set { m_TitreEnvoi = value; }
  }

  public string TexteEnvoi
  {
    get { return m_TexteEnvoi; }
    set { m_TexteEnvoi = value; }
  }

  public bool GardeTrace
  {
    get { return m_GardeTrace; }
    set { m_GardeTrace = value; }
  }

  public bool AvertissementRenouvellement
  {
    get { return m_AvertissementRenouvellement; }
    set { m_AvertissementRenouvellement = value; }
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

  public bool Load(int idEnvoi)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM Envois WHERE IdEnvoi=" + idEnvoi.ToString());

    if (theData.Read() == true)
    {
      m_IdEnvoi = (int)theData["IdEnvoi"];
      m_IdClient = (int)theData["IdClient"];
      m_IdLogiciel = (int)(theData["IdLogiciel"] == DBNull.Value ? 0 : theData["IdLogiciel"]);
      m_DateEnvoi = (DateTime)theData["DateEnvoi"];
      m_FichierEnvoye = (string)(theData["FichierEnvoye"] == DBNull.Value ? "" : theData["FichierEnvoye"]);
      m_TitreEnvoi = (string)theData["TitreEnvoi"];
      m_TexteEnvoi = (string)theData["TexteEnvoi"];
      m_GardeTrace = (bool)(theData["GarderTrace"] == DBNull.Value ? false : theData["GarderTrace"]);
      m_AvertissementRenouvellement = (bool)(theData["AvertissementRenouvelement"] == DBNull.Value ? false : theData["AvertissementRenouvelement"]);
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

      if (IdEnvoi == 0)
      {
        sql = "INSERT INTO Envois(IdClient, IdLogiciel, DateEnvoi, FichierEnvoye, TitreEnvoi, TexteEnvoi, GarderTrace, AvertissementRenouvelement, Commentaire, DateCreation, DateModification, IdUtilisateur) "
              + " VALUES(@IdClient, @IdLogiciel, @DateEnvoi, @FichierEnvoye, @TitreEnvoi, @TexteEnvoi, @GarderTrace, @AvertissementRenouvelement, @Commentaire, GETDATE(), GETDATE(), @IdUtilisateur)";
      }
      else
      {
        sql = "UPDATE Envois SET IdClient=@IdClient, IdLogiciel=@IdLogiciel, DateEnvoi=@DateEnvoi, FichierEnvoye=@FichierEnvoye, TitreEnvoi=@TitreEnvoi, "
              + " TexteEnvoi=@TexteEnvoi, GarderTrace=@GarderTrace, AvertissementRenouvelement=@AvertissementRenouvelement, "
              + " Commentaire=@Commentaire, DateModification=GETDATE(), IdUtilisateur=@IdUtilisateur "
              + " WHERE IdEnvoi=" + IdEnvoi.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdClient", SqlDbType.Int, 0, ParameterDirection.Input, IdClient);
      AddParamToSQLCmd(theCommand, "@IdLogiciel", SqlDbType.Int, 0, ParameterDirection.Input, (IdLogiciel == 0 ? null : (object)IdLogiciel));
      AddParamToSQLCmd(theCommand, "@DateEnvoi", SqlDbType.DateTime, 0, ParameterDirection.Input, DateEnvoi);
      AddParamToSQLCmd(theCommand, "@FichierEnvoye", SqlDbType.NText, FichierEnvoye.Length, ParameterDirection.Input, FichierEnvoye);
      AddParamToSQLCmd(theCommand, "@TitreEnvoi", SqlDbType.NText, TitreEnvoi.Length, ParameterDirection.Input, TitreEnvoi);
      AddParamToSQLCmd(theCommand, "@TexteEnvoi", SqlDbType.NText, TexteEnvoi.Length, ParameterDirection.Input, TexteEnvoi);
      AddParamToSQLCmd(theCommand, "@GarderTrace", SqlDbType.Bit, 0, ParameterDirection.Input, GardeTrace);
      AddParamToSQLCmd(theCommand, "@AvertissementRenouvelement", SqlDbType.Bit, 0, ParameterDirection.Input, AvertissementRenouvellement);
      AddParamToSQLCmd(theCommand, "@Commentaire", SqlDbType.NText, Commentaire.Length, ParameterDirection.Input, Commentaire);
      AddParamToSQLCmd(theCommand, "@IdUtilisateur", SqlDbType.Int, 0, ParameterDirection.Input, 1);

      int ret = theCommand.ExecuteNonQuery();

      if (ret < 1)
        return false;

      if (IdEnvoi == 0)
      {
        theCommand.Dispose();

        // sqlserver earlier version
        theCommand = new SqlCommand("SELECT @@IDENTITY", cn);

        m_IdEnvoi = (int)(decimal)theCommand.ExecuteScalar();
      }

      return true;
    }
  }

  public bool Delete()
  {
    if (IdEnvoi == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      /*Envoi
      DestinataireEnvoi*/

      // on supprime les destinataires et ensuite l'Envoi
      // DestinataireEnvoi 
      sql = "DELETE FROM DestinataireEnvoi WHERE IdEnvoi=@IdEnvoi";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      AddParamToSQLCmd(theCommand, "@IdEnvoi", SqlDbType.Int, 0, ParameterDirection.Input, IdEnvoi);

      theCommand.ExecuteNonQuery();

      theCommand.Dispose();

      // Envois
      sql = "DELETE FROM Envois WHERE IdEnvoi=@IdEnvoi";

      theCommand = new SqlCommand(sql, cn);

      AddParamToSQLCmd(theCommand, "@IdEnvoi", SqlDbType.Int, 0, ParameterDirection.Input, IdEnvoi);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public static void PurgerEnvois(DateTime DateEnvois)
  {
    using (SqlConnection cn = GetConnection())
    {
      // appelle la proc stockée PurgeEnvoi
      SqlCommand theCommand = new SqlCommand("PurgeEnvois", cn);

      theCommand.CommandType = CommandType.StoredProcedure;

      AddParamToSQLCmd(theCommand, "@DateEnvoi", SqlDbType.DateTime, 0, ParameterDirection.Input, DateEnvois);

      theCommand.ExecuteNonQuery();
    }
  }

}
