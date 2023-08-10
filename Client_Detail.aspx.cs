using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using GestionLicences;

/// <summary>
/// Summary description for Client_Detail
/// </summary>
public partial class Client_Detail : System.Web.UI.Page
{

  private Client CurrentClient = null;

  void GetCurrentClient()
  {
    int IdClient;

    if (Request.QueryString["IdClient"] != null
        && Int32.TryParse((string)Request.QueryString["IdClient"], out IdClient))
    {
      //        if (Session["IdClient"] != null)
      //        {
      //            IdClient = (int)Session["IdClient"];

      if (CurrentClient == null)
        CurrentClient = new Client();

      if (CurrentClient.Load(IdClient) == false)
        CurrentClient = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Détail Client";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentClient();

      if (CurrentClient != null)
      {
        txtNumeroClient.Text = CurrentClient.NumeroClient;
        txtRaisonSociale.Text = CurrentClient.RaisonSociale;
        txtCommentaire.Text = CurrentClient.Commentaire;

        lblDateCreation.Text = "Créé le " + CurrentClient.DateCreation.ToString("dd/MM/yyyy hh:mm");
        lblDateModification.Text = "Modifié le " + CurrentClient.DateModification.ToString("dd/MM/yyyy hh:mm") + " par " + CurrentClient.IdUtilisateur.ToString();

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eModification))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des modifications";
        }
      }
      else
      {
        txtNumeroClient.Text = "";
        txtRaisonSociale.Text = "";
        txtCommentaire.Text = "";

        lblDateCreation.Text = "";
        lblDateModification.Text = "";

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eCréation))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des créations";
        }
      }
    }
  }

  protected void btnCancel_Click(object sender, EventArgs e)
  {
    Response.Redirect("Clients.aspx");
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    // erreurs sur la page ?
    if (Page.IsValid == false)
      return;

    // donnée client
    GetCurrentClient();
    if (CurrentClient == null)
      CurrentClient = new Client();

    // recupère les infos
    CurrentClient.NumeroClient = txtNumeroClient.Text.Trim();
    CurrentClient.RaisonSociale = txtRaisonSociale.Text.Trim();
    CurrentClient.Commentaire = txtCommentaire.Text.Trim();
    try
    {
      if (CurrentClient.Save() == false)
      {
        ErrorMessage.Text = "Impossible de Sauvegarder !";
      }
      else
      {
        // retour à la liste des clients
        if (CurrentClient.IdClient == 0)
        {
          //Session["IdClient"] = null;
          Response.Redirect("Clients.aspx");
        }
        else
        {
          //Session["IdClient"] = CurrentClient.IdClient;
          //Response.Redirect("Clients.aspx");
          Response.Redirect("Clients.aspx?IdClient=" + CurrentClient.IdClient.ToString());
        }
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la sauvegarde:<br />" + ex.Message;
    }
  }

}
