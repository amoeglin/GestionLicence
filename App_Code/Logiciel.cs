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
using System.Collections.Generic;

/// <summary>
/// Summary description for Logiciel
/// </summary>
public class OptionLogiciel : DataAccess
{
  int m_IdOptionLogiciel;
  int m_IdLogiciel;
  string m_NomOption;
  string m_NomLicence;
  string m_Commentaire;

  public OptionLogiciel()
  {
    m_IdLogiciel = 0;
    m_IdOptionLogiciel = 0;
    m_NomOption = "";
    m_NomLicence = "";
    m_Commentaire = "";
  }

  public int IdOptionLogiciel
  {
    get { return m_IdOptionLogiciel; }
  }

  public int IdLogiciel
  {
    get { return m_IdLogiciel; }
    set { m_IdLogiciel = value; }
  }

  public string NomOption
  {
    get { return m_NomOption; }
    set { m_NomOption = value; }
  }

  public string NomLicence
  {
    get { return m_NomLicence; }
    set { m_NomLicence = value; }
  }

  public string Commentaire
  {
    get { return m_Commentaire; }
    set { m_Commentaire = value; }
  }

  public bool Load(int idOptionLogiciel)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM OptionLogiciel WHERE IdOptionLogiciel=" + idOptionLogiciel.ToString());

    if (theData.Read() == true)
    {
      m_IdOptionLogiciel = (int)theData["IdOptionLogiciel"];
      m_IdLogiciel = (int)theData["IdLogiciel"];
      m_NomOption = (string)theData["NomOption"];
      m_NomLicence = (string)theData["NomLicence"];
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

      if (IdLogiciel == 0)
      {
        sql = "INSERT INTO OptionLogiciel(NomOption, NomLicence, Commentaire) "
              + " VALUES(@NomOption, @NomLicence, @Commentaire)";
      }
      else
      {
        sql = "UPDATE OptionLogiciel SET NomOption=@NomOption, NomLicence=@NomLicence, Commentaire=@Commentaire"
              + " WHERE IdOptionLogiciel=" + IdOptionLogiciel.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@NomOption", SqlDbType.NText, NomOption.Length, ParameterDirection.Input, NomOption);
      AddParamToSQLCmd(theCommand, "@NomLicence", SqlDbType.NText, NomLicence.Length, ParameterDirection.Input, NomLicence);
      AddParamToSQLCmd(theCommand, "@Commentaire", SqlDbType.NText, Commentaire.Length, ParameterDirection.Input, Commentaire);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Delete()
  {
    if (IdOptionLogiciel == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      /*OptionLogiciel*/

      // on marque le client comme supprimé plutot que de la détruire.
      sql = "UPDATE OptionLogiciel SET Supprime=1 WHERE IdOptionLogiciel=@IdOptionLogiciel";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdOptionLogiciel", SqlDbType.Int, 0, ParameterDirection.Input, IdOptionLogiciel);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Undelete()
  {
    if (IdOptionLogiciel == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      // on marque le client comme supprimé plutot que de la détruire.
      sql = "UPDATE OptionLogiciel SET Supprime=0 WHERE IdOptionLogiciel=@IdOptionLogiciel";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdOptionLogiciel", SqlDbType.Int, 0, ParameterDirection.Input, IdOptionLogiciel);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

}

/// <summary>
/// Summary description for Logiciel
/// </summary>
public class Logiciel : DataAccess
{
  int m_IdLogiciel;
  string m_NomLogiciel;
  string m_NomLicence;
  string m_URLInstall;
  string m_URLUpdate;
  string m_DefaultDir;
  string m_LicenceDir;
  string m_ChangeLog;
  string m_Commentaire;

  DateTime m_DateCreation;
  int m_IdUtilisateur;
  DateTime m_DateModification;

  List<OptionLogiciel> m_ListOptionLogiciel;

  public Logiciel()
  {
    m_IdLogiciel = 0;
    m_NomLogiciel = "";
    m_NomLicence = "";
    m_URLInstall = "";
    m_URLUpdate = "";
    m_LicenceDir = "";
    m_DefaultDir = "";
    m_ChangeLog = "";
    m_Commentaire = "";

    m_DateCreation = DateTime.Now;
    m_IdUtilisateur = 0;
    m_DateModification = DateTime.Now;

    m_ListOptionLogiciel = new List<OptionLogiciel>();
  }

  public int IdLogiciel
  {
    get { return m_IdLogiciel; }
  }

  public string NomLogiciel
  {
    get { return m_NomLogiciel; }
    set { m_NomLogiciel = value; }
  }

  public string NomLicence
  {
    get { return m_NomLicence; }
    set { m_NomLicence = value; }
  }

  public string URLInstall
  {
    get { return m_URLInstall; }
    set { m_URLInstall = value; }
  }

  public string URLUpdate
  {
    get { return m_URLUpdate; }
    set { m_URLUpdate = value; }
  }

  public string ChangeLog
  {
    get { return m_ChangeLog; }
    set { m_ChangeLog = value; }
  }

  public string LicenceDir
  {
    get { return m_LicenceDir; }
    set { m_LicenceDir = value; }
  }

  public string DefaultDir
  {
    get { return m_DefaultDir; }
    set { m_DefaultDir = value; }
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

  public bool Load(int idLogiciel)
  {
    SqlDataReader theData = OpenReader("SELECT * FROM Logiciel WHERE IdLogiciel=" + idLogiciel.ToString());

    if (theData.Read() == true)
    {
      m_IdLogiciel = (int)theData["IdLogiciel"];
      m_NomLogiciel = (string)theData["NomComplet"];
      m_NomLicence = (string)theData["NomLicence"];
      m_URLInstall = (string)(theData["URLInstall"] == DBNull.Value ? "" : theData["URLInstall"]);
      m_URLUpdate = (string)(theData["URLUpdate"] == DBNull.Value ? "" : theData["URLUpdate"]);
      m_LicenceDir = (string)(theData["LicenceDir"] == DBNull.Value ? "" : theData["LicenceDir"]);
      m_DefaultDir = (string)(theData["DefaultDir"] == DBNull.Value ? "" : theData["DefaultDir"]);
      m_ChangeLog = (string)(theData["ChangeLog"] == DBNull.Value ? "" : theData["ChangeLog"]);
      m_Commentaire = (string)(theData["Commentaire"] == DBNull.Value ? "" : theData["Commentaire"]);

      m_DateCreation = (DateTime)theData["DateCreation"];
      m_IdUtilisateur = (int)theData["IdUtilisateur"];
      m_DateModification = (DateTime)theData["DateModification"];

      theData.Close();

      // charge les options
      LoadOption();

      return true;
    }

    theData.Close();

    return false;
  }

  public bool LoadOption()
  {
    SqlDataReader theData = OpenReader("SELECT IdOptionLogiciel FROM OptionLogiciel WHERE (Supprime=0 OR Supprime IS NULL) AND IdLogiciel=" + IdLogiciel.ToString());

    m_ListOptionLogiciel.Clear();

    while (theData.Read() == true)
    {
      int Id = (int)theData["IdOptionLogiciel"];

      OptionLogiciel opt = new OptionLogiciel();

      if (opt.Load(Id) == true)
        m_ListOptionLogiciel.Add(opt);
    }

    theData.Close();
    theData.Dispose();

    return true;
  }

  public bool SaveOption()
  {
    foreach (OptionLogiciel opt in m_ListOptionLogiciel)
    {
      opt.IdLogiciel = IdLogiciel;

      opt.Save();
    }

    return true;
  }

  public bool Save()
  {
    using (SqlConnection cn = GetConnection())
    {
      string sql;

      if (IdLogiciel == 0)
      {
        sql = "INSERT INTO Logiciel(NomComplet, NomLicence, URLInstall, URLUpdate, ChangeLog, LicenceDir, DefaultDir, "
              + " Commentaire, DateCreation, DateModification, IdUtilisateur) "
              + " VALUES(@NomLogiciel, @NomLicence, @URLInstall, @URLUpdate, @ChangeLog, @LicenceDir, @DefaultDir, "
              + " @Commentaire, GETDATE(), GETDATE(), @IdUtilisateur)";
      }
      else
      {
        sql = "UPDATE Logiciel SET NomComplet=@NomLogiciel, NomLicence=@NomLicence, URLInstall=@URLInstall, URLUpdate=@URLUpdate, "
              + " ChangeLog=@ChangeLog, LicenceDir=@LicenceDir, DefaultDir=@DefaultDir, "
              + " Commentaire=@Commentaire, DateModification=GETDATE(), IdUtilisateur=@IdUtilisateur "
              + " WHERE IdLogiciel=" + IdLogiciel.ToString();
      }

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@NomLogiciel", SqlDbType.NText, NomLogiciel.Length, ParameterDirection.Input, NomLogiciel);
      AddParamToSQLCmd(theCommand, "@NomLicence", SqlDbType.NText, NomLicence.Length, ParameterDirection.Input, NomLicence);
      AddParamToSQLCmd(theCommand, "@URLInstall", SqlDbType.NText, URLInstall.Length, ParameterDirection.Input, URLInstall);
      AddParamToSQLCmd(theCommand, "@URLUpdate", SqlDbType.NText, URLUpdate.Length, ParameterDirection.Input, URLUpdate);
      AddParamToSQLCmd(theCommand, "@ChangeLog", SqlDbType.NText, ChangeLog.Length, ParameterDirection.Input, ChangeLog);
      AddParamToSQLCmd(theCommand, "@LicenceDir", SqlDbType.NText, LicenceDir.Length, ParameterDirection.Input, LicenceDir);
      AddParamToSQLCmd(theCommand, "@DefaultDir", SqlDbType.NText, DefaultDir.Length, ParameterDirection.Input, DefaultDir);
      AddParamToSQLCmd(theCommand, "@Commentaire", SqlDbType.NText, Commentaire.Length, ParameterDirection.Input, Commentaire);
      AddParamToSQLCmd(theCommand, "@IdUtilisateur", SqlDbType.Int, 0, ParameterDirection.Input, 1);

      theCommand.ExecuteNonQuery();

      // recupère l'IdLogiciel pour les options
      if (IdLogiciel == 0)
      {
        theCommand.CommandText = "SELECT @@Identity";

        m_IdLogiciel = (int)(decimal)theCommand.ExecuteScalar();
      }

      // sauve les options
      if (IdLogiciel != 0)
        SaveOption();

      return true;
    }
  }

  public bool Delete()
  {
    if (IdLogiciel == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      /*Licence*/

      // on marque le client comme supprimé plutot que de la détruire.
      sql = "UPDATE Logiciel SET Supprime=1 WHERE IdLogiciel=@IdLogiciel";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdLogiciel", SqlDbType.Int, 0, ParameterDirection.Input, IdLogiciel);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

  public bool Undelete()
  {
    if (IdLogiciel == 0)
      return false;

    using (SqlConnection cn = GetConnection())
    {
      string sql;

      // on marque le client comme supprimé plutot que de la détruire.
      sql = "UPDATE Logiciel SET Supprime=0 WHERE IdLogiciel=@IdLogiciel";

      SqlCommand theCommand = new SqlCommand(sql, cn);

      // requete parametrée pour éviter une SQL injection !
      AddParamToSQLCmd(theCommand, "@IdLogiciel", SqlDbType.Int, 0, ParameterDirection.Input, IdLogiciel);

      return theCommand.ExecuteNonQuery() == 1;
    }
  }

}
