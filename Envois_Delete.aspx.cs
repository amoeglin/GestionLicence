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
/// Summary description for Envois_Delete
/// </summary>
public partial class Envois_Delete : System.Web.UI.Page
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
    Page.Title = Global.AppTitle + " - Suppression Envoi";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentEnvoi();

      if (CurrentEnvoi != null)
      {
        lblEnvoi.Text = CurrentEnvoi.IdEnvoi + " - " + CurrentEnvoi.TitreEnvoi + "  du  " + CurrentEnvoi.DateEnvoi.ToString("dd/MM/yyyy HH:mm");

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eSuppression))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des suppressions";
        }
      }
      else
      {
        lblEnvoi.Text = "Erreur !";
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

    // donnée Envoi
    GetCurrentEnvoi();
    if (CurrentEnvoi == null)
      CurrentEnvoi = new Envoi();

    try
    {
      if (CurrentEnvoi.Delete() == false)
      {
        ErrorMessage.Text = "Impossible de Supprimer !";
      }
      else
      {
        // retour à la liste des Envois
        Response.Redirect("Envois.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la suppression:<br />" + ex.Message;
    }
  }
    
}
