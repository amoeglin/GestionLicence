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
/// Summary description for Client_Delete
/// </summary>
public partial class Client_Delete : System.Web.UI.Page
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
    Page.Title = Global.AppTitle + " - Suppression Client";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentClient();

      if (CurrentClient != null)
      {
        lblClient.Text = CurrentClient.NumeroClient + "  -  " + CurrentClient.RaisonSociale;

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eSuppression))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des suppressions";
        }
      }
      else
      {
        lblClient.Text = "Erreur !";
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

    try
    {
      if (CurrentClient.Delete() == false)
      {
        ErrorMessage.Text = "Impossible de Supprimer !";
      }
      else
      {
        // retour à la liste des clients
        //Session["IdClient"] = null;
        Response.Redirect("Clients.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la suppression:<br />" + ex.Message;
    }
  }

}
