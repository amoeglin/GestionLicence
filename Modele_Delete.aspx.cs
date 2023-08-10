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
/// Summary description for Modele_Delete
/// </summary>
public partial class Modele_Delete : System.Web.UI.Page
{

  private Modele CurrentModele = null;

  void GetCurrentModele()
  {
    int IdModele;

    if (Request.QueryString["IdModele"] != null
        && Int32.TryParse((string)Request.QueryString["IdModele"], out IdModele))
    {
      if (CurrentModele == null)
        CurrentModele = new Modele();

      if (CurrentModele.Load(IdModele) == false)
        CurrentModele = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Suppression Modele";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentModele();

      if (CurrentModele != null)
      {
        lblModele.Text = CurrentModele.Nom;

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eSuppression))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des suppressions";
        }
      }
      else
      {
        lblModele.Text = "Erreur !";
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

    // donnée Modele
    GetCurrentModele();
    if (CurrentModele == null)
      CurrentModele = new Modele();

    try
    {
      if (CurrentModele.Delete() == false)
      {
        ErrorMessage.Text = "Impossible de Supprimer !";
      }
      else
      {
        // retour à la liste des Modeles
        Response.Redirect("Envois.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la suppression:<br />" + ex.Message;
    }
  }

}
