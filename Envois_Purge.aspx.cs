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
/// Summary description for Envois_Purge
/// </summary>
public partial class Envois_Purge : System.Web.UI.Page
{

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Purge des Envois de plus de 6 mois";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      lblEnvoi.Text = DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy");
      
      if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eSuppression))
      {
        btnSave.Enabled = false;
        ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des suppressions";
      }
    }
  }

  protected void btnCancel_Click(object sender, EventArgs e)
  {
    Response.Redirect("Envois.aspx");
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    // erreurs sur la page ?
    if (Page.IsValid == false)
      return;

    try
    {
      Envoi.PurgerEnvois(DateTime.Now.AddMonths(-6));

      // retour à la liste des Envois
      Response.Redirect("Envois.aspx");
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la purge:<br />" + ex.Message;
    }
  }
    
}
