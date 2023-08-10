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
/// Summary description for Logiciel_Delete
/// </summary>
public partial class Logiciel_Delete : System.Web.UI.Page
{

  private Logiciel CurrentLogiciel = null;

  void GetCurrentLogiciel()
  {
    int IdLogiciel;

    if (Request.QueryString["IdLogiciel"] != null
        && Int32.TryParse((string)Request.QueryString["IdLogiciel"], out IdLogiciel))
    {
      if (CurrentLogiciel == null)
        CurrentLogiciel = new Logiciel();

      if (CurrentLogiciel.Load(IdLogiciel) == false)
        CurrentLogiciel = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Suppression Logiciel";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentLogiciel();

      if (CurrentLogiciel != null)
      {
        lblLogiciel.Text = CurrentLogiciel.NomLogiciel + "  -  " + CurrentLogiciel.NomLicence;

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eSuppression))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des suppressions";
        }
      }
      else
      {
        lblLogiciel.Text = "Erreur !";
      }
    }
  }

  protected void btnCancel_Click(object sender, EventArgs e)
  {
    Response.Redirect("Logiciels.aspx");
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    // erreurs sur la page ?
    if (Page.IsValid == false)
      return;

    // donnée Logiciel
    GetCurrentLogiciel();
    if (CurrentLogiciel == null)
      CurrentLogiciel = new Logiciel();

    try
    {
      if (CurrentLogiciel.Delete() == false)
      {
        ErrorMessage.Text = "Impossible de Supprimer !";
      }
      else
      {
        // retour à la liste des Logiciels
        Response.Redirect("Logiciels.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la suppression:<br />" + ex.Message;
    }
  }

}
