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
/// Summary description for Contact_Undelete
/// </summary>
public partial class Contact_Undelete : System.Web.UI.Page
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
    Page.Title = Global.AppTitle + " - Récupération Contact";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentContact();

      if (CurrentContact != null)
      {
        lblContact.Text = CurrentContact.IdContact + "  -  " + CurrentContact.NomPrenom;
      }
      else
      {
        lblContact.Text = "Erreur !";
      }
    }
  }

  protected void btnCancel_Click(object sender, EventArgs e)
  {
    Response.Redirect("Administration.aspx");
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
      if (CurrentContact.Undelete() == false)
      {
        ErrorMessage.Text = "Impossible de Récupérer !";
      }
      else
      {
        // retour à la liste des Contacts
        Response.Redirect("Administration.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la récupération:<br />" + ex.Message;
    }
  }

}
