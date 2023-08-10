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
/// Summary description for Contact_Delete
/// </summary>
public partial class Contact_Delete : System.Web.UI.Page
{

  private Contact CurrentContact = null;

  void GetCurrentContact()
  {
    int IdContact;

    if (Request.QueryString["IdContact"] != null
        && Int32.TryParse((string)Request.QueryString["IdContact"], out IdContact))
    {
      if (CurrentContact == null)
        CurrentContact = new Contact();

      if (CurrentContact.Load(IdContact) == false)
        CurrentContact = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Suppression Contact";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentContact();

      if (CurrentContact != null)
      {
        lblContact.Text = CurrentContact.IdContact + "  -  " + CurrentContact.NomPrenom;

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eSuppression))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des suppressions";
        }
      }
      else
      {
        lblContact.Text = "Erreur !";
      }
    }
  }

  protected void btnCancel_Click(object sender, EventArgs e)
  {
    GetCurrentContact();
    if (CurrentContact == null)
      Response.Redirect("Clients.aspx");
    else
      Response.Redirect("Clients.aspx?IdClient=" + CurrentContact.IdClient.ToString());
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    // erreurs sur la page ?
    if (Page.IsValid == false)
      return;

    // donnée Contact
    GetCurrentContact();
    if (CurrentContact == null)
      CurrentContact = new Contact();

    try
    {
      if (CurrentContact.Delete() == false)
      {
        ErrorMessage.Text = "Impossible de Supprimer !";
      }
      else
      {
        // retour à la liste des Contacts
        Response.Redirect("Clients.aspx?IdClient=" + CurrentContact.IdClient.ToString());
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la suppression:<br />" + ex.Message;
    }
  }

}
