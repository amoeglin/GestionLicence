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
/// Summary description for Contact_Detail
/// </summary>
public partial class Contact_Detail : System.Web.UI.Page
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
    Page.Title = Global.AppTitle + " - Détail Contact";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentContact();

      if (CurrentContact != null)
      {
        cboClient.SelectedValue = CurrentContact.IdClient.ToString();
        cboTypeContact.SelectedValue = CurrentContact.IdTypeContact.ToString();
        txtNomPrenom.Text = CurrentContact.NomPrenom;
        txtAdresse.Text = CurrentContact.Adresse;
        txtTelephone.Text = CurrentContact.Telephone;
        txtFax.Text = CurrentContact.Fax;
        txtEmail.Text = CurrentContact.Email;
        txtCommentaire.Text = CurrentContact.Commentaire;

        // remplissage du combo contact technique
        FillContactTechnique(CurrentContact.IdClient, CurrentContact.IdContactTechnique == 0 ? 0 : CurrentContact.IdContactTechnique);

        lblDateCreation.Text = "Créé le " + CurrentContact.DateCreation.ToString("dd/MM/yyyy hh:mm");
        lblDateModification.Text = "Modifié le " + CurrentContact.DateModification.ToString("dd/MM/yyyy hh:mm") + " par " + CurrentContact.IdUtilisateur.ToString();

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eModification))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des modifications";
        }

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eCréation))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des créations";
        }
      }
      else
      {
        int IdClient = 0;

        if (Request.QueryString["IdClient"] != null
            && Int32.TryParse((string)Request.QueryString["IdClient"], out IdClient))

          cboClient.SelectedValue = IdClient.ToString();
        else
          cboClient.SelectedValue = null;

        cboTypeContact.SelectedValue = "4"; // utilisateur
        txtNomPrenom.Text = "";
        txtAdresse.Text = "";
        txtTelephone.Text = "";
        txtFax.Text = "";
        txtEmail.Text = "";
        txtCommentaire.Text = "";

        // remplissage du combo contact technique
        FillContactTechnique(IdClient, 0);

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
    GetCurrentContact();

    if (CurrentContact != null)
      Response.Redirect("Clients.aspx?IdClient=" + CurrentContact.IdClient.ToString());
    else
      Response.Redirect("Clients.aspx");
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

    // recupère les infos
    CurrentContact.IdClient = Int32.Parse(cboClient.SelectedValue);
    CurrentContact.IdTypeContact = Int32.Parse(cboTypeContact.SelectedValue);
    CurrentContact.NomPrenom = txtNomPrenom.Text.Trim();
    CurrentContact.Adresse = txtAdresse.Text.Trim();
    CurrentContact.Telephone = txtTelephone.Text.Trim();
    CurrentContact.Fax = txtFax.Text.Trim();
    CurrentContact.Email = txtEmail.Text.Trim();
    if (cboContactTechnique.SelectedValue == "0")
      CurrentContact.IdContactTechnique = 0;
    else
      CurrentContact.IdContactTechnique = Int32.Parse(cboContactTechnique.SelectedValue);
    CurrentContact.Commentaire = txtCommentaire.Text.Trim();
    try
    {
      if (CurrentContact.Save() == false)
      {
        ErrorMessage.Text = "Impossible de Sauvegarder !";
      }
      else
      {
        // retour à la liste des Contacts client
        Response.Redirect("Clients.aspx?IdClient=" + CurrentContact.IdClient.ToString());
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la sauvegarde:<br />" + ex.Message;
    }
  }

  protected void btnAucun_Click(object sender, EventArgs e)
  {
    cboContactTechnique.SelectedValue = null;
  }

  private void FillContactTechnique(int idClient, int idContact)
  {
    // remplissage du combo contact technique
    DataAccess.FillCombo(cboContactTechnique, "SELECT * FROM ListeContactTechnique WHERE IdClient=" + idClient.ToString(),
                             "NomPrenom", "IdContact", idContact, true, "(aucun)");
  }

  protected void cboClient_SelectedIndexChanged(object sender, EventArgs e)
  {
    int idClient = Int32.Parse(cboClient.SelectedValue);

    FillContactTechnique(idClient, 0);
  }

}
