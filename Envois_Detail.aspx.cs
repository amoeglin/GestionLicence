using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using GestionLicences;

/// <summary>
/// Summary description for Envois_Detail
/// </summary>
public partial class Envois_Detail : System.Web.UI.Page
{

  private Envoi CurrentEnvoi = null;

  void GetCurrentEnvoi()
  {
    int IdEnvoi;

    if (Request.QueryString["IdEnvoi"] != null
        && Int32.TryParse((string)Request.QueryString["IdEnvoi"], out IdEnvoi))
    {
      if (CurrentEnvoi == null)
        CurrentEnvoi = new Envoi();

      if (CurrentEnvoi.Load(IdEnvoi) == false)
        CurrentEnvoi = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Détail Envoi";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      // simule une TextBox
      const string debutTableau = "<table cellspacing=\"0\" cellpadding=\"2\"><tr><td style=\"border: solid 1px LightSteelBlue; border-collapse: collapse;\">";
      const string finTableau = "</td></tr></table>";

      // donnée de l'envoi
      GetCurrentEnvoi();
      if (CurrentEnvoi == null)
        CurrentEnvoi = new Envoi();

      StringBuilder sb = new StringBuilder();

      SqlDataReader theData = DataAccess.OpenReader("SELECT RaisonSociale FROM Client WHERE IdClient=" + CurrentEnvoi.IdClient);

      if (theData.Read() == true)
        sb.AppendFormat("Raison Sociale du Client<br>{0}{1}{2}<br>", debutTableau, theData["RaisonSociale"], finTableau);
      else
        sb.AppendFormat("Raison Sociale du Client<br>{0}IMPOSSIBLE DE LIRE LES DONNEES DU CLIENT{1}<br>", debutTableau, finTableau);
      sb.AppendFormat("Titre du message<br>{0}{1}{2}<br>", debutTableau, CurrentEnvoi.TitreEnvoi, finTableau);
      sb.AppendFormat("Date de l'envoi<br>{0}{1}{2}<br>", debutTableau, CurrentEnvoi.DateEnvoi.ToString("dd/MM/yyyy HH:mm"), finTableau);
      sb.AppendFormat("Texte de l'envoi<br>{0}{1}{2}", debutTableau, CurrentEnvoi.TexteEnvoi, finTableau);

      lblEnvoi.Text = sb.ToString();

      theData.Close();
      theData.Dispose();

      chkAvertissement.Checked = CurrentEnvoi.AvertissementRenouvellement;
      chkGarderTrace.Checked = CurrentEnvoi.GardeTrace;
      txtCommentaire.Text = CurrentEnvoi.Commentaire;

      lblDateCreation.Text = "Créé le " + CurrentEnvoi.DateCreation.ToString("dd/MM/yyyy hh:mm");
      lblDateModification.Text = "Modifié le " + CurrentEnvoi.DateModification.ToString("dd/MM/yyyy hh:mm") + " par " + CurrentEnvoi.IdUtilisateur.ToString();

      if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eCréation))
      {
        btnSave.Enabled = false;
        ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des créations";
      }
    }
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    // erreurs sur la page ?
    if (Page.IsValid == false)
      return;

    // donnée de l'envoi
    GetCurrentEnvoi();
    if (CurrentEnvoi == null)
      CurrentEnvoi = new Envoi();

    // recupère les infos
    CurrentEnvoi.Commentaire = txtCommentaire.Text.Trim();
    try
    {
      if (CurrentEnvoi.Save() == false)
      {
        ErrorMessage.Text = "Impossible de Sauvegarder !";
      }
      else
      {
        // retour à la liste des Envois
        Response.Redirect("Envois.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la sauvegarde:<br />" + ex.Message;
    }
  }

}
