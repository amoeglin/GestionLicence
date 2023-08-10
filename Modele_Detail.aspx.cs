using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebHtmlEdit;

using GestionLicences;

/// <summary>
/// Summary description for Modele_Detail
/// </summary>
public partial class Modele_Detail : System.Web.UI.Page
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
    Page.Title = Global.AppTitle + " - Détail Modèle";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentModele();

      if (CurrentModele != null)
      {
        txtNom.Text = CurrentModele.Nom;
        txtTitre.Text = CurrentModele.Titre;
        txtContenu.Text = CurrentModele.Contenu;
        cboTypeModele.SelectedValue = CurrentModele.IdTypeModele.ToString();

        lblDateCreation.Text = "Créé le " + CurrentModele.DateCreation.ToString("dd/MM/yyyy hh:mm");
        lblDateModification.Text = "Modifié le " + CurrentModele.DateModification.ToString("dd/MM/yyyy hh:mm") + " par " + CurrentModele.IdUtilisateur.ToString();

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eModification))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des modifications";
        }
      }
      else
      {
        txtNom.Text = "";
        txtTitre.Text = "";
        txtContenu.Text = "";
        cboTypeModele.SelectedValue = "1"; // avertissement

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
    GetCurrentModele();
    if (CurrentModele == null)
      CurrentModele = new Modele();

    // recupère les infos
    CurrentModele.Nom = txtNom.Text.Trim();
    CurrentModele.Titre = txtTitre.Text.Trim();
    CurrentModele.Contenu = txtContenu.Text.Trim();
    CurrentModele.IdTypeModele = Int32.Parse(cboTypeModele.SelectedValue);

    try
    {
      if (CurrentModele.Save() == false)
      {
        ErrorMessage.Text = "Impossible de Sauvegarder !";
      }
      else
      {
        // retour à la liste des Modeles
        Response.Redirect("Envois.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la sauvegarde:<br />" + ex.Message;
    }
  }

}
