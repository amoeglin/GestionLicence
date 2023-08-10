using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using LicenceServer;
using ErrorServices;


// Pour la compression ZIP
using NZipLib.Checksums;
using NZipLib.Zip;


/// <summary>
/// Summary description for BuildLicenceFile
/// </summary>
public class LicenceFile
{
  public int IdLicence;
  public string Client;
  public string Site;
  public string Machine;
  public string Program;
  public int LicencesCount;
  public DateTime endDate;
  public int LifeDuration;
  public bool UseUNCPath;

  string tempPath;
  string workerTempPath;
  LicenceFile2.eFileMode keyMode;

  //clsCrypter mPasswordCrypter;
  Crypter mPasswordCrypter;

  const string keyFile = "LICENCES.KEY";

  public LicenceFile()
  {      
    //mPasswordCrypter = new clsCrypter();
    mPasswordCrypter = new Crypter();


    tempPath = Path.GetTempPath();
    if (tempPath[tempPath.Length - 1] != '\\')
      tempPath += '\\';
    //tempPath = "~/App_Data/";

    keyMode = LicenceFile2.eFileMode.KeyFile;

    IdLicence = -1;
    Client = "";
    Site = "";
    Machine = "";
    Program = "";
    LicencesCount = 0;
    LifeDuration = 0;
    UseUNCPath = true;
  }

  public bool Build(out string zipFilename, ref string strError)
  {
    zipFilename = "";
    strError = "";

    workerTempPath = tempPath + Guid.NewGuid().ToString() + '\\';
    try
    {
      // creation du répertoire temporaire
      Directory.CreateDirectory(workerTempPath);
    }
    catch (Exception e)
    {
      strError = string.Format("Impossible de créer le répertoire temporaire {0} : {1}<br/>", workerTempPath, e.Message);
      return false;
    }

    try
    {

      //CLicenceFile clf = new CLicenceFile();

      LicenceFile2 clf = new LicenceFile2();

      //clf.set_ClientName(ref Client);
      //clf.set_SiteName(ref Site);
      //clf.set_MachineName(ref Machine);
      //clf.set_LifeDuration(ref LifeDuration);
      //clf.set_UseUNCName(ref UseUNCPath);

      clf.ClientName = Client;
      clf.SiteName = Site;
      clf.MachineName = Machine;
      clf.LifeDuration = LifeDuration;
      clf.UseUNCName = UseUNCPath;

      //string pass = clsCrypter.mdp + DateTime.Now.ToString("HH:mm");
      string pass = Crypter.mdp + DateTime.Now.ToString("hh:mm");
      //pass = mPasswordCrypter.Encrypte(ref pass);
      pass = mPasswordCrypter.Encrypte(pass);

      // licence principale
      //CLicence cls = clf.CreateLicence2(ref Program, ref LicencesCount, ref endDate, ref pass);
      LicenceServer.Licence cls = clf.CreateLicence2(Program, LicencesCount, endDate, pass);

      if (cls == null)
      {
        strError = string.Format("Impossible de générer la licence du programme {0}<br/>", Program);
        return false;
      }

      // licences optionneles : options
      using (SqlDataReader theData = DataAccess.OpenReader("SELECT OptionLogiciel.NomLicence "
                                                    + " FROM OptionLogiciel INNER JOIN OptionLicence ON OptionLogiciel.IdOptionLogiciel = OptionLicence.IdOptionLogiciel "
                                                    + " WHERE OptionLicence.IdLicence=" + IdLicence.ToString()))
      {
        while (theData.Read() == true)
        {
          string optName = (string)theData["NomLicence"];

          // on recalcul le mot de passe, on ne sait jamais !
          //pass = clsCrypter.mdp + DateTime.Now.ToString("HH:mm");
          //pass = mPasswordCrypter.Encrypte(ref pass);
          pass = Crypter.mdp + DateTime.Now.ToString("hh:mm");
          pass = mPasswordCrypter.Encrypte(pass);

          // crée les licences des options du logiciel
          //CLicence clsOpt = clf.CreateLicence2(ref optName, ref LicencesCount, ref endDate, ref pass);
          LicenceServer.Licence clsOpt = clf.CreateLicence2(optName, LicencesCount, endDate, pass);

            if (clsOpt == null)
          {
            strError = string.Format("Impossible de générer les option licence du programme {0}<br/>", optName);
            theData.Close();
            return false;
          }
        }

        theData.Close();
      }

      // sauvegarde et generation
      string filename = workerTempPath + keyFile;
      //clf.Save(ref filename, ref keyMode);
      clf.Save(filename, keyMode);

      zipFilename = filename.Replace(keyFile, Machine == "-" ? "LICENCES.ZIP" : (Machine + ".ZIP"));
      ZipFile(filename, zipFilename);

      try
      {
        // suppression du .key
        File.Delete(filename);
      }
      catch (Exception e)
      {
        strError = string.Format("Impossible de supprimer le fichier licence temporaire du programme {0} : {1}<br/>", Program, e.Message);
      }

      return true;
    }
    catch (Exception e)
    {
      strError = string.Format("Erreur durant la creation de la licence du programme {0} : {1}<br/>", Program, e.Message);

      return false;
    }
  }

  private void ZipFile(string SrcFile, string DstFile)
  {
    Crc32 crc = new Crc32();
    ZipOutputStream s = new ZipOutputStream(File.Create(DstFile));

    s.SetLevel(6); // 0 - store only to 9 - means best compression

    FileStream fs = File.OpenRead(SrcFile);

    byte[] buffer = new byte[fs.Length];
    fs.Read(buffer, 0, buffer.Length);

    FileInfo fio = new FileInfo(SrcFile);

    ZipEntry entry = new ZipEntry(fio.Name); // nom.ext sans répertoire

    entry.DateTime = DateTime.Now;

    // set Size and the crc, because the information
    // about the size and crc should be stored in the header
    // if it is not set it is automatically written in the footer.
    // (in this case size == crc == -1 in the header)
    // Some ZIP programs have problems with zip files that don't store
    // the size and crc in the header.
    entry.Size = fs.Length;

    fs.Close();
    fs.Dispose();

    crc.Reset();
    crc.Update(buffer);

    entry.Crc = crc.Value;

    s.PutNextEntry(entry);

    s.Write(buffer, 0, buffer.Length);

    s.Finish();
    s.Close();
    s.Dispose();
  }

  public void DeleteWorkerDir()
  {
    try
    {
      // suppression du répertoire temporaire
      Directory.Delete(workerTempPath, true);
    }
    catch (Exception e)
    {
    }
  }
}
