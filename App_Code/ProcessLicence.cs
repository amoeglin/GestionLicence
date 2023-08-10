using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Summary description for ProcessLicence
/// </summary>
public class ProcessLicence
{
    //
    // Attributes
    //

    string bcc_avertissement = "support@actuariesservices.com", bcc_renouvellement = "support@actuariesservices.com";
    string from_avertissement = "support@actuariesservices.com", from_renouvellement = "support@actuariesservices.com";
    int LicenceLifeDuration = 365;
    int LicenceLifeDurationNoMachineName = 15;
    string ParamDateExpiration = "31/12";

    StringBuilder strLog;

    // modeles
    int idModeleAvertissement;
    Modele theModeleAvertissement;
    int idModeleRenouvellement;
    Modele theModeleRenouvellement;
    int idModeleRegeneration;
    Modele theModeleRegeneration;
    int idModeleInstallation;
    Modele theModeleInstallation;

    // pour les méthodes de génération des messages
    Licence theLicence;
    Logiciel theLogiciel;
    Client theClient;
    Contact theContactTechnique;
    Contact theContactAdministratif;
    StringCollection filenames;


    //
    // Properties
    //

    public int ModeleAvertissement
    {
        get { return idModeleAvertissement; }
        set
        {
            idModeleAvertissement = value;
            if (idModeleAvertissement != 0)
            {
                theModeleAvertissement = new Modele();
                theModeleAvertissement.Load(idModeleAvertissement);
            }
        }
    }

    public int ModeleRenouvellement
    {
        get { return idModeleRenouvellement; }
        set
        {
            idModeleRenouvellement = value;
            if (idModeleRenouvellement != 0)
            {
                theModeleRenouvellement = new Modele();
                theModeleRenouvellement.Load(idModeleRenouvellement);
            }
        }
    }

    public int ModeleRegeneration
    {
        get { return idModeleRegeneration; }
        set
        {
            idModeleRegeneration = value;
            if (idModeleRegeneration != 0)
            {
                theModeleRegeneration = new Modele();
                theModeleRegeneration.Load(idModeleRegeneration);
            }
        }
    }

    public int ModeleInstallation
    {
        get { return idModeleInstallation; }
        set
        {
            idModeleInstallation = value;
            if (idModeleInstallation != 0)
            {
                theModeleInstallation = new Modele();
                theModeleInstallation.Load(idModeleInstallation);
            }
        }
    }


    //
    // Methodes
    //

    public StringBuilder Logger
    {
        get { return strLog; }
        set { strLog = value; }
    }

    public ProcessLicence()
    {
        // default email address 
        if (ConfigurationManager.AppSettings["email_bcc_avertissement"] != null)
            bcc_avertissement = ConfigurationManager.AppSettings["email_bcc_avertissement"];

        if (ConfigurationManager.AppSettings["email_bcc_renouvellement"] != null)
            bcc_renouvellement = ConfigurationManager.AppSettings["email_bcc_renouvellement"];

        if (ConfigurationManager.AppSettings["email_from_avertissement"] != null)
            from_avertissement = ConfigurationManager.AppSettings["email_from_avertissement"];

        if (ConfigurationManager.AppSettings["email_from_renouvellement"] != null)
            from_renouvellement = ConfigurationManager.AppSettings["email_from_renouvellement"];

        // durée de vie de la licence
        if (ConfigurationManager.AppSettings["LicenceLifeDuration"] != null &&
            Int32.TryParse(ConfigurationManager.AppSettings["LicenceLifeDuration"], out LicenceLifeDuration) == false)
            LicenceLifeDuration = 365;

        if (ConfigurationManager.AppSettings["LicenceLifeDurationNoMachineName"] != null &&
            Int32.TryParse(ConfigurationManager.AppSettings["LicenceLifeDurationNoMachineName"], out LicenceLifeDurationNoMachineName) == false)
            LicenceLifeDurationNoMachineName = 15;

        if (ConfigurationManager.AppSettings["DateExpiration"] != null)
            ParamDateExpiration = ConfigurationManager.AppSettings["DateExpiration"];
    }

    /// <summary>
    /// Prepare un job (chargement des objets Licence, Client, Logiciel, Contacts... et Filenames
    /// </summary>
    /// <param name="idLicence">identifiant de la licence concernée</param>
    private void PrepareJob(int idLicence)
    {
        theLicence = new Licence();
        theLogiciel = new Logiciel();
        theClient = new Client();
        theContactTechnique = null;
        theContactAdministratif = null;

        // chargement des informations
        theLicence.Load(idLicence);
        theLogiciel.Load(theLicence.IdLogiciel);
        theClient.Load(theLicence.IdClient);
        if (theLicence.IdContactTechnique != 0)
        {
            theContactTechnique = new Contact();
            theContactTechnique.Load(theLicence.IdContactTechnique);
        }

        if (theLicence.IdContactAdministratif != 0)
        {
            theContactAdministratif = new Contact();
            theContactAdministratif.Load(theLicence.IdContactAdministratif);
        }

        // fabrique les fichiers licences
        filenames = new StringCollection();
    }

    /// <summary>
    /// Prepare un message d'installation de logiciel
    /// </summary>
    /// <param name="idLicence">identifiant de la licence concernée</param>
    /// <param name="message">en retour, message généré</param>
    /// <param name="addr">en retour, addresses de destination</param>
    /// <returns>true si OK</returns>
    public bool PrepareJobInstallation(int idLicence, out MailMessage message, out string addr)
    {
        message = null;
        addr = "erreur";

        // préparation du job
        PrepareJob(idLicence);


        // chargement des contacts
        if (theContactAdministratif == null)
            strLog.AppendFormat("<b>Licence Id={0} : Contact administratif non renseigné !</b><br/>", idLicence.ToString());

        if (theContactTechnique == null)
            strLog.AppendFormat("<b>Licence Id={0} : Contact technique non renseigné !</b><br/>", idLicence.ToString());

        if (theContactAdministratif == null && theContactTechnique == null)
            return false;

        // fabrique les fichiers à attacher
        filenames = BuildLicenceFiles(theLicence, theLogiciel, theClient);
        if (filenames.Count == 0)
            return false;

        //
        // Prepare le message
        //
        PrepareEmailMessage(from_renouvellement, theContactTechnique, theContactAdministratif, bcc_renouvellement,
                            theModeleInstallation, filenames, theLicence, theLogiciel, out message, out addr);

        return true;
    }

    /// <summary>
    /// Envoi le message préparé par PrepareJobInstallation()
    /// </summary>
    /// <param name="message">Message à envoyer</param>
    /// <param name="addr">liste des destinataires</param>
    /// <returns>true si OK</returns>
    public bool ExecuteJobInstallation(MailMessage message, string addr)
    {
        //
        // envoi le message
        //
        bool ret = SendMessage(theContactTechnique, theContactAdministratif, theModeleInstallation,
                               filenames, theLicence, theLogiciel, false, false, message, addr);

        // efface les fichiers attachés générés
        if (ret == true)
            CleanUpAttachmentFiles();

        return ret;
    }

    /// <summary>
    /// Envois les messages demandé pour la licence spécifiée
    /// </summary>
    /// <param name="idLicence">identifiant de licence</param>
    /// <param name="avertir">envoi le message d'avertissement si true</param>
    /// <param name="renouveler">envoi le message de renouvellement si true</param>
    /// <param name="regenerer">envoi le message de regeneration si true</param>
    /// <returns>true si tout va bien</returns>
    public bool ExecuteJob(int idLicence, bool avertir, bool renouveler, bool regenerer, bool bGarderTrace)
    {
        bool ret = true;

        CompilationSection compilation = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");

        if (compilation.Debug == true)
        {
            strLog.AppendFormat("Licence Id={0} Avertir={1} Renew={2} Regen={3}<br />",
                                idLicence.ToString(), (avertir ? "Vrai" : "Faux"),
                                (renouveler ? "Vrai" : "Faux"), (regenerer ? "Vrai" : "Faux"));
        }

        // préparation du job
        PrepareJob(idLicence);

        // avertissement
        if (avertir == true)
        {
            if (theContactAdministratif == null)
            {
                strLog.AppendFormat("<b>Licence Id={0} : Contact administratif non renseigné !</b><br/>", idLicence.ToString());
                ret = false;
            }

            ret &= BuildMessage(from_avertissement, theContactAdministratif,
                         null, bcc_avertissement,
                         theModeleAvertissement, filenames, theLicence, theLogiciel, true, bGarderTrace);
        }

        // renouvellement
        if (renouveler == true)
        {
            if (theContactAdministratif == null)
                strLog.AppendFormat("<b>Licence Id={0} : Contact administratif non renseigné !</b><br/>", idLicence.ToString());

            if (theContactTechnique == null)
                strLog.AppendFormat("<b>Licence Id={0} : Contact technique non renseigné !</b><br/>", idLicence.ToString());

            if (theContactAdministratif == null && theContactTechnique == null)
                ret = false;

            if (ret == true)
            {
                DateTime OldDateExpiration = theLicence.DateExpiration;

                // mise à jour de la date d'expiration de la licence
                if (ParamDateExpiration == "31/12")
                    theLicence.DateExpiration = new DateTime(theLicence.DateExpiration.Year + 1, 12, 31);
                else
                    theLicence.DateExpiration = theLicence.DateExpiration.AddYears(1);
                theLicence.Save();

                strLog.AppendFormat("Licence Id={0} : Date expiration de {1} vers {2} <br/>", idLicence.ToString(), OldDateExpiration.ToString("dd/MM/yyyy"), theLicence.DateExpiration.ToString("dd/MM/yyyy"));
            }

            filenames = BuildLicenceFiles(theLicence, theLogiciel, theClient);

            if (ret == true && filenames.Count != 0)
            {
                ret &= BuildMessage(from_renouvellement, theContactTechnique == null ? theContactAdministratif : theContactTechnique,
                           theContactAdministratif == null ? theContactTechnique : theContactAdministratif, bcc_renouvellement,
                           theModeleRenouvellement, filenames, theLicence, theLogiciel, false, bGarderTrace);
            }
            else
            {
                strLog.Append("<b>Aucun email de renouvellement ou régénération n'a été envoyé</b><br/>");
            }
        }

        // regenerer
        if (regenerer == true)
        {
            if (theContactAdministratif == null)
                strLog.AppendFormat("<b>Licence Id={0} : Contact administratif non renseigné !</b><br/>", idLicence.ToString());

            if (theContactTechnique == null)
                strLog.AppendFormat("<b>Licence Id={0} : Contact technique non renseigné !</b><br/>", idLicence.ToString());

            if (theContactAdministratif == null && theContactTechnique == null)
                ret = false;

            filenames = BuildLicenceFiles(theLicence, theLogiciel, theClient);

            if (ret == true && filenames.Count != 0)
            {
                ret &= BuildMessage(from_renouvellement, theContactTechnique == null ? theContactAdministratif : theContactTechnique,
                             theContactAdministratif == null ? theContactTechnique : theContactAdministratif, bcc_renouvellement,
                             theModeleRegeneration, filenames, theLicence, theLogiciel, false, bGarderTrace);
            }
            else
            {
                strLog.Append("<b>Aucun email de renouvellement ou régénération n'a été envoyé</b><br/>");
            }
        }

        // efface les fichiers attachés générés
        CleanUpAttachmentFiles();

        // log de fin
        strLog.Append("-------------------------------------------------<br/>");

        return ret;
    }

    private void CleanUpAttachmentFiles()
    {
        // nettoyage des fichiers temporaires
        if (filenames != null && filenames.Count != 0)
        {
            foreach (string fileName in filenames)
            {
                try
                {
                    // suppression du .key
                    File.Delete(fileName);
                }
                catch (Exception e)
                {
                    //strLog.AppendFormat("<b>Impossible de supprimer le fichier temporaire {0} : {1}</b><br/>", fileName, e.Message);
                }
            }
        }
    }

    /// <summary>
    /// Prépare et envoi un email au client
    /// </summary>
    /// <param name="fromAdress">adresse email de la source de l'envoi</param>
    /// <param name="toContact">contact de destination</param>
    /// <param name="ccContact">contact de copie</param>
    /// <param name="bccAdress">adresse de copie cachée</param>
    /// <param name="theModele">modele de document</param>
    /// <param name="fileNames">liste des fichiers joint préalablement fabriqué par BuildLicence</param>
    /// <param name="theLicence">licence correspondante</param>
    /// <param name="theLogiciel">logiciel correspondant</param>
    /// <param name="bAvertir">message d'avertissement ? (pour trace dans la base de données)</param>
    /// <param name="bGarderTrace">surveillé les retours de ce message</param>
    /// <returns>true si OK</returns>
    protected bool BuildMessage(string fromAdress, Contact toContact,
                              Contact ccContact, string bccAdress,
                              Modele theModele, StringCollection fileNames,
                              Licence theLicence, Logiciel theLogiciel,
                              bool bAvertir, bool bGarderTrace)
    {
        MailMessage message;
        string addr;

        //
        // Prepare le message
        //
        PrepareEmailMessage(fromAdress, toContact, ccContact, bccAdress, theModele,
                            fileNames, theLicence, theLogiciel, out message, out addr);

        //
        // envoi le message
        //
        return SendMessage(toContact, ccContact, theModele, fileNames, theLicence,
               theLogiciel, bAvertir, bGarderTrace, message, addr);
    }

    /// <summary>
    /// envoi le message
    /// </summary>
    /// <param name="toContact">contact de destination</param>
    /// <param name="ccContact">contact de copie</param>
    /// <param name="theModele">modele de document</param>
    /// <param name="fileNames">liste des fichiers joint préalablement fabriqué par BuildLicence</param>
    /// <param name="theLicence">licence correspondante</param>
    /// <param name="theLogiciel">logiciel correspondant</param>
    /// <param name="bAvertir">message d'avertissement ? (pour trace dans la base de données)</param>
    /// <param name="bGarderTrace">surveillé les retours de ce message</param>
    /// <param name="message">message preparé par PrepareMessage()</param>
    /// <param name="addr">liste des adresses de destinations</param>
    /// <returns>true si OK</returns>
    protected bool SendMessage(Contact toContact, Contact ccContact, Modele theModele,
                            StringCollection fileNames, Licence theLicence, Logiciel theLogiciel,
                            bool bAvertir, bool bGarderTrace, MailMessage message, string addr)
    {
        SmtpClient client = new SmtpClient();

        try
        {
            //###save Email to disk 
            //http://localhost:2924/Licence_Detail.aspx?IdLicence=74
            string mailDir = System.Web.HttpRuntime.BinDirectory;
            mailDir = mailDir.Replace("bin\\", "");
            mailDir = Path.Combine(mailDir, "_Mails"); // Path.Combine(dir, "_Mails"); //, importName + "-" + DateTime.Now.ToString("s").Replace(":", "-"));

            if (!Directory.Exists(mailDir))
            {
                Directory.CreateDirectory(mailDir);
            }

            mailDir = Path.Combine(mailDir, theLogiciel.NomLicence + "-" + DateTime.Now.ToString("s").Replace(":", "-"));
            if (!Directory.Exists(mailDir))
            {
                Directory.CreateDirectory(mailDir);
            }
           
            List<string> zipFiles = new List<string>();
            //save all files & the email
            foreach (string fl in fileNames)
            {
                string fileName = Path.GetFileName(fl);
                string target = Path.Combine(mailDir, fileName);

                if (!zipFiles.Contains(fileName))
                {
                    zipFiles.Add(fileName);
                    File.Copy(fl, target);
                }                
            }

            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            client.PickupDirectoryLocation = mailDir;
            client.Send(message);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            //return true;

            client.Send(message);

            //
            // sauvegarde l'envoi
            //
            Envoi theInvoice = new Envoi();

            theInvoice.IdClient = theLicence.IdClient;
            theInvoice.IdLogiciel = theLogiciel.IdLogiciel;
            theInvoice.DateEnvoi = DateTime.Now;

            theInvoice.FichierEnvoye = "";
            foreach (string fileName in fileNames)
                theInvoice.FichierEnvoye += fileName + "; ";

            theInvoice.TitreEnvoi = message.Subject;
            theInvoice.TexteEnvoi = message.Body;
            theInvoice.GardeTrace = bGarderTrace;
            theInvoice.AvertissementRenouvellement = bAvertir;
            theInvoice.Commentaire = "";

            theInvoice.Save();

            // to:
            DestinataireEnvoi theDestinataire;
            if (toContact != null)
            {
                theDestinataire = new DestinataireEnvoi();

                theDestinataire.IdEnvoi = theInvoice.IdEnvoi;
                theDestinataire.IdContact = toContact.IdContact;
                theDestinataire.Commentaire = "";
                theDestinataire.Traite = false;

                theDestinataire.Save();
            }

            // cc:
            if (ccContact != null)
            {
                theDestinataire = new DestinataireEnvoi();

                theDestinataire.IdEnvoi = theInvoice.IdEnvoi;
                theDestinataire.IdContact = ccContact.IdContact;
                theDestinataire.Commentaire = "";
                theDestinataire.Traite = false;

                theDestinataire.Save();
            }

            strLog.AppendFormat("Message envoyé à :<br/>{0}<br/>Sujet : {1}<br/>", addr, theModele.Titre);

            return true;
        }
        catch (Exception e)
        {
            strLog.AppendFormat("Impossible d'envoyer l'email à :<br/>{0}<br/>Erreur : {1}<br/>", addr, e.Message);

            return false;
        }
    }

    /// <summary>
    /// Prepare le message email
    /// </summary>
    /// <param name="fromAdress">adresse email de la source de l'envoi</param>
    /// <param name="toContact">contact de destination</param>
    /// <param name="ccContact">contact de copie</param>
    /// <param name="bccAdress">adresse de copie cachée</param>
    /// <param name="theModele">modele de document</param>
    /// <param name="fileNames">liste des fichiers joint préalablement fabriqué par BuildLicence</param>
    /// <param name="theLicence">licence correspondante</param>
    /// <param name="theLogiciel">logiciel correspondant</param>
    /// <param name="message">message en sortie</param>
    /// <param name="addr">liste des adresses email de destination</param>
    protected void PrepareEmailMessage(string fromAdress, Contact toContact, Contact ccContact, string bccAdress, Modele theModele, StringCollection fileNames, Licence theLicence, Logiciel theLogiciel, out MailMessage message, out string addr)
    {
        message = new MailMessage();

        message.IsBodyHtml = true;
        message.Priority = MailPriority.Normal;

        message.From = new MailAddress(fromAdress);


        CompilationSection compilation = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");

        if (compilation.Debug == true)
        {
            // pour les tests
            string debugMail = "marchois@usa.net";
            if (ConfigurationManager.AppSettings["DebugMail"] != null)
                debugMail = ConfigurationManager.AppSettings["DebugMail"];

            message.To.Add(new MailAddress(debugMail, "DebugMail"));
        }
        else
        {
            // en réèl !
            if (toContact != null)
                message.To.Add(new MailAddress(toContact.Email, toContact.NomPrenom));
            else if (ccContact != null)
                message.To.Add(new MailAddress(ccContact.Email, ccContact.NomPrenom));

            if (ccContact != null && toContact != null)
                message.CC.Add(new MailAddress(ccContact.Email, ccContact.NomPrenom));

            if (bccAdress != null)
                message.Bcc.Add(new MailAddress(bccAdress));
        }

        StringBuilder sb = new StringBuilder(theModele.Titre);

        // le titre peut faire reference à qq champs
        sb.Replace("#LOGICIEL#", theLogiciel.NomLogiciel);
        message.Subject = sb.ToString();

        //
        // fabrique le texte
        //

        sb.Length = 0;

        // debug : real address
        if (toContact != null)
            sb.Append("To: " + toContact.NomPrenom + "(" + toContact.Email + ")<br/>");
        else if (ccContact != null)
            sb.Append("To: " + ccContact.NomPrenom + "(" + ccContact.Email + ")<br/>");

        if (ccContact != null && toContact != null)
            sb.Append("cc: " + ccContact.NomPrenom + "(" + ccContact.Email + ")<br/>");
        sb.Append("Bcc: " + bccAdress + "<br/>");

        addr = sb.ToString();

        sb.Append("<hr>");

        if (compilation.Debug == false)
        {
            sb.Remove(0, sb.Length);
        }

        // contenu d'après le modele
        sb.Append(theModele.Contenu);





        /* ***
            Résolution de la valeur des champs
        *** */
        sb.Replace("#LOGICIEL#", theLogiciel.NomLogiciel);
        sb.Replace("#DATEEXPIRATION#", theLicence.DateExpiration.ToString("dd/MM/yyyy"));
        sb.Replace("#NBLICENCE#", theLicence.NbLicenceFacture.ToString()); // licence à facturer

        /*    sb.Replace("#PRIXMAINTENANCE#", theLicence.PrixMiseAJour.ToString("#,#0.00"));
            double px = theLicence.NbLicenceFacture * theLicence.PrixMiseAJour;
            sb.Replace("#PRIXTOTAL#", px.ToString("#,#0.00"));*/

        double px = theLicence.NbLicenceFacture != 0 ? (theLicence.PrixMiseAJour / theLicence.NbLicenceFacture) : 0;
        sb.Replace("#PRIXMAINTENANCE#", px.ToString("#,#0.00"));
        sb.Replace("#PRIXTOTAL#", theLicence.PrixMiseAJour.ToString("#,#0.00"));

        sb.Replace("#REPERTOIRE#", string.Format("<a href=\"{0}\">{0}</a>", theLicence.Repertoire));
        sb.Replace("#URLINSTALL#", string.Format("<a href=\"{0}\">{0}</a>", theLogiciel.URLInstall));
        sb.Replace("#URLUPDATE#", string.Format("<a href=\"{0}\">{0}</a>", theLogiciel.URLUpdate));
        sb.Replace("#CHANGELOG#", string.Format("<a href=\"{0}\">{0}</a>", theLogiciel.ChangeLog));
        sb.Replace("#LICENCEDIR#", theLogiciel.LicenceDir);
        sb.Replace("#DEFAULTDIR#", theLogiciel.DefaultDir);
        sb.Replace("#RAISONSOCIALE#", theClient.RaisonSociale);
        sb.Replace("#SITE#", theLicence.NomSite);

        // licences optionneles : options
        StringBuilder sbOption = new StringBuilder();

        SqlDataReader theData = DataAccess.OpenReader("SELECT OptionLogiciel.NomOption "
                                                      + " FROM OptionLogiciel INNER JOIN OptionLicence ON OptionLogiciel.IdOptionLogiciel = OptionLicence.IdOptionLogiciel "
                                                      + " WHERE OptionLicence.IdLicence=" + theLicence.IdLicence.ToString());

        while (theData.Read() == true)
        {
            string optName = (string)theData["NomOption"];

            sbOption.AppendFormat("&nbsp;&nbsp;&nbsp;-&nbsp;{0}<br/>", optName);
        }

        theData.Close();

        if (sbOption.Length == 0)
            sbOption.Append("(aucune)");

        sb.Replace("#OPTIONS#", sbOption.ToString());


        // fabrication de la liste des machines
        StringBuilder listeMachne = new StringBuilder("<table style=\"font-size: 10pt; font-family: Verdana\" border=\"0\"><tr><td>&nbsp;&nbsp;&nbsp;</td><td><b>Utilisateur</b></td><td></td><td><b>Machine</b></td></tr>");

        theData = DataAccess.OpenReader(
                                        string.Format("SELECT [NomUtilisateur], [NomMachine], [IdMachine] "
                                        + "FROM [LicenceMachine] WHERE ([IdLicence] = {0}) ORDER BY [NomUtilisateur]",
                                        theLicence.IdLicence)
                                       );

        while (theData.Read() == true)
        {
            listeMachne.AppendFormat("<tr><td>&nbsp;&nbsp;&nbsp;</td><td>{0}</td><td>&nbsp;&nbsp;</td><td>{1}</td></tr>", theData["NomUtilisateur"].ToString().Trim(), theData["NomMachine"].ToString().ToUpper().Trim());
        }
        theData.Close();
        theData.Dispose();

        listeMachne.Append("</table>");

        sb.Replace("#MACHINE#", listeMachne.ToString());

        message.Body = sb.ToString();

        /* ***
            Résolution de la valeur des champs
        *** */




        //
        // fichiers attachés (licences)
        //
        if (fileNames.Count != 0)
        {
            foreach (string fileName in fileNames)
            {
                Attachment att = new Attachment(fileName);

                message.Attachments.Add(att);
            }
        }
    }

    /// <summary>
    /// Fabrique les fichiers licences correspondant à une licence
    /// </summary>
    /// <param name="theLicence">licence concernée</param>
    /// <param name="theLogiciel">logiciel concerné</param>
    /// <param name="theClient">client concerné</param>
    /// <returns>liste des nom de fichiers créés</returns>
    StringCollection BuildLicenceFiles(Licence theLicence, Logiciel theLogiciel, Client theClient)
    {
        StringCollection filenames = new StringCollection();
        string filename = "";
        LicenceFile lf = new LicenceFile();

        SqlDataReader theData = DataAccess.OpenReader("SELECT NomMachine FROM LicenceMachine WHERE IdLicence = " + theLicence.IdLicence.ToString());

        bool atLeastOne = false;
        string strError = "";

        while (theData.Read() == true)
        {
            if (theData["NomMachine"] != DBNull.Value)
            {
                atLeastOne = true;

                string Machine = (string)theData["NomMachine"];
                Machine = Machine.ToUpper();

                lf.Client = theClient.RaisonSociale;
                lf.endDate = theLicence.DateExpiration;
                lf.LicencesCount = theLicence.NbLicence; // licence à autoriser
                lf.LifeDuration = LicenceLifeDuration;
                lf.Machine = Machine;
                lf.Program = theLogiciel.NomLicence;
                lf.Site = theLicence.NomSite;
                lf.IdLicence = theLicence.IdLicence;
                lf.UseUNCPath = theLicence.UseUNCPath;

                if (lf.Build(out filename, ref strError) == false)
                {
                    strLog.AppendFormat("<b>Impossible de générer le fichier licences pour la machine {0} : {1}!</b><br/>", Machine, strError == null ? "" : strError);
                }
                else
                {
                    if (strError != "")
                    {
                        strLog.AppendFormat("<b>Erreur durant la génération du fichier licences pour la machine {0} : {1}</b><br/>", Machine, strError);
                    }

                    filenames.Add(filename);
                }
            }
        }

        theData.Close();
        theData.Dispose();

        if (atLeastOne == false)
        {
            lf.Client = theClient.RaisonSociale;
            lf.endDate = theLicence.DateExpiration;
            lf.LicencesCount = theLicence.NbLicence; // licence à autoriser
            lf.LifeDuration = LicenceLifeDurationNoMachineName;
            lf.Machine = "-";
            lf.Program = theLogiciel.NomLicence;
            lf.Site = theLicence.NomSite;
            lf.IdLicence = theLicence.IdLicence;
            lf.UseUNCPath = theLicence.UseUNCPath;

            if (lf.Build(out filename, ref strError) == false)
            {
                strLog.Append("<b>Impossible de générer le fichier licences.zip (pas de machine spécifiée) !</b><br/>");
            }
            else
            {
                if (strError != "")
                {
                    strLog.AppendFormat("<b>Erreur durant la génération du fichier licences.zip (pas de machine spécifiée) : {0}</b><br/>", strError);
                }

                filenames.Add(filename);
            }
        }

        return filenames;
    }

}
