using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

public partial class Model_Preview : System.Web.UI.Page
{
  private Modele CurrentModele = null;
  private Licence CurrentLicence = null;

  void GetCurrentModeleAndLicence()
  {
    int IdModele;
    int IdLicence;

    if (Session["IdModele"] != null)
    {
      IdModele = (int)Session["IdModele"];

      if (CurrentModele == null)
        CurrentModele = new Modele();

      if (CurrentModele.Load(IdModele) == false)
        CurrentModele = null;
    }

    if (Session["IdLicence"] != null)
    {
      IdLicence = (int)Session["IdLicence"];

      if (CurrentLicence == null)
        CurrentLicence = new Licence();

      if (CurrentLicence.Load(IdLicence) == false)
        CurrentLicence = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      GetCurrentModeleAndLicence();

      if (CurrentModele != null && CurrentLicence != null)
      {
        // prepare le message
        StringBuilder strLog = new StringBuilder();

        MailMessage theMessage = new MailMessage();
        string theAdresses = "";

        ProcessLicence theProcessor = new ProcessLicence();

        theProcessor.Logger = strLog;

        // load the model
        theProcessor.ModeleInstallation = CurrentModele.IdModele;

        //
        // prepare le mail
        //

        theProcessor.PrepareJobInstallation(CurrentLicence.IdLicence, out theMessage, out theAdresses);
        if (theMessage != null)
        {
          // sauvegarde dans la session
          Session["MailProcessor"] = theProcessor;
          Session["MailMessage"] = theMessage;
          Session["MailAddresses"] = theAdresses;

          //
          // boite d'information
          //
          StringBuilder sb = new StringBuilder();

          // simule une TextBox
          const string debutTableau = "<table cellspacing=\"0\" cellpadding=\"2\"><tr><td style=\"border: solid 1px LightSteelBlue; border-collapse: collapse;\">";
          const string finTableau = "</td></tr></table>";

          sb.AppendFormat("Titre du message<br>{0}{1}{2}<br>", debutTableau, theMessage.Subject, finTableau);
          sb.AppendFormat("Destinataires<br>{0}{1}{2}", debutTableau, theAdresses, finTableau);

          lblPreview.Text = sb.ToString();

          // contenu du message pour retouche
          txtContenu.Text = theMessage.Body;
        }

        // affichage du log d'execution
        ErrorMessage.Text = strLog.ToString();
      }
      else
      {
        lblPreview.Text = "Modele et Licence inconnus !<br/><br/><span style=\"color: red\"><b>Vous devez spécifiez les variables de Session !</b></span>";
      }
    }
  }

  protected void btnCancel_Click(object sender, EventArgs e)
  {
    GetCurrentModeleAndLicence();

    if (CurrentModele != null && CurrentLicence != null)
    {
      Session.Remove("MailProcessor");
      Session.Remove("MailMessage");
      Session.Remove("MailAddresses");

      Response.Redirect("Licence_Detail.aspx?IdLicence=" + CurrentLicence.IdLicence.ToString());
    }
    else
    {
      Response.Redirect("Default.aspx");
    }
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    GetCurrentModeleAndLicence();

    try
    {

      if (CurrentModele != null && CurrentLicence != null)
      {
        ProcessLicence theProcessor = (ProcessLicence)Session["MailProcessor"];
        MailMessage theMessage = (MailMessage)Session["MailMessage"];
        string theAdresses = (string)Session["MailAddresses"];

        theMessage.Body = txtContenu.Text;

        if (theProcessor.ExecuteJobInstallation(theMessage, theAdresses) == true)
        {
          // clean up
          Session.Remove("MailProcessor");
          Session.Remove("MailMessage");
          Session.Remove("MailAddresses");

          // retour à la page de la licence
          Response.Redirect("Licence_Detail.aspx?IdLicence=" + CurrentLicence.IdLicence.ToString());
        }
        else
          ErrorMessage.Text = theProcessor.Logger.ToString();
      }
    }
    catch (Exception ex)
    {
      ErrorMessage.Text = "Erreur durant l'execution : " + ex.Message;
    }
  }
}
