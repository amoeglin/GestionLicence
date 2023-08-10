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
/// Summary description for Logiciel_Detail
/// </summary>
public partial class Logiciel_Detail : System.Web.UI.Page
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
    Page.Title = Global.AppTitle + " - Détail Logiciel";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentLogiciel();

      if (CurrentLogiciel != null)
      {
        txtNomLogiciel.Text = CurrentLogiciel.NomLogiciel;
        txtNomLicence.Text = CurrentLogiciel.NomLicence;
        txtURLInstall.Text = CurrentLogiciel.URLInstall;
        txtURLUpdate.Text = CurrentLogiciel.URLUpdate;
        txtURLChangeLog.Text = CurrentLogiciel.ChangeLog;
        txtCommentaire.Text = CurrentLogiciel.Commentaire;
        txtDefaultDir.Text = CurrentLogiciel.DefaultDir;
        txtLicenceDir.Text = CurrentLogiciel.LicenceDir;

        btnAddOption.Enabled = true;

        lblDateCreation.Text = "Créé le " + CurrentLogiciel.DateCreation.ToString("dd/MM/yyyy hh:mm");
        lblDateModification.Text = "Modifié le " + CurrentLogiciel.DateModification.ToString("dd/MM/yyyy hh:mm") + " par " + CurrentLogiciel.IdUtilisateur.ToString();

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eModification))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des modifications";
        }
      }
      else
      {
        txtNomLogiciel.Text = "";
        txtNomLicence.Text = "";
        txtURLInstall.Text = "";
        txtURLUpdate.Text = "";
        txtURLChangeLog.Text = "";
        txtCommentaire.Text = "";
        txtDefaultDir.Text = "";
        txtLicenceDir.Text = "";

        btnAddOption.Enabled = false;

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

  protected void btnSave_Click(object sender, EventArgs e)
  {
    // erreurs sur la page ?
    if (Page.IsValid == false)
      return;

    // donnée client
    GetCurrentLogiciel();
    if (CurrentLogiciel == null)
      CurrentLogiciel = new Logiciel();

    // recupère les infos
    CurrentLogiciel.NomLogiciel = txtNomLogiciel.Text.Trim();
    CurrentLogiciel.NomLicence = txtNomLicence.Text.Trim();
    CurrentLogiciel.URLInstall = txtURLInstall.Text;
    CurrentLogiciel.URLUpdate = txtURLUpdate.Text;
    CurrentLogiciel.ChangeLog = txtURLChangeLog.Text;
    CurrentLogiciel.Commentaire = txtCommentaire.Text.Trim();
    CurrentLogiciel.DefaultDir = txtDefaultDir.Text;
    CurrentLogiciel.LicenceDir = txtLicenceDir.Text;

    try
    {
      if (CurrentLogiciel.Save() == false)
      {
        ErrorMessage.Text = "Impossible de Sauvegarder !";
      }
      else
      {
        // retour à la liste des logiciels
        Response.Redirect("Logiciels.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la sauvegarde:<br />" + ex.Message;
    }
  }
  protected void btnAddOption_Click(object sender, EventArgs e)
  {
    DetailsView1.Visible = true;
    btnAddOption.Visible = false;
    DetailsView1.ChangeMode(DetailsViewMode.Insert);
  }

  protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
  {
    lblErreur.Text = "";
    if (e.Values["NomOption"] == null
        || e.Values["NomLicence"] == null)
    {
      e.Cancel = true;
      lblErreur.Text = "Vous devez entrer une valeur pour tous les champs Nom... !";
    }
  }

  protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
  {
    lblErreur.Text = "";
    if (e.NewValues["NomOption"] == null
        || e.NewValues["NomLicence"] == null)
    {
      e.Cancel = true;
      lblErreur.Text = "Vous devez entrer une valeur pour tous les champs Nom... !";
    }
  }

  protected void grdLicenceMachine_RowUpdating(object sender, GridViewUpdateEventArgs e)
  {
    lblErreur.Text = "";
    if (e.NewValues["NomOption"] == null
        || e.NewValues["NomLicence"] == null)
    {
      e.Cancel = true;
      lblErreur.Text = "Vous devez entrer une valeur pour tous les champs Nom... !";
    }
  }

  protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
  {
    DetailsView1.Visible = false;
    btnAddOption.Visible = true;
  }

  protected void DetailsView1_ModeChanged(object sender, EventArgs e)
  {
    if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
    {
      DetailsView1.Visible = false;
      btnAddOption.Visible = true;
    }
  }

}
