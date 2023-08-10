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
/// Summary description for Utilisateur_Detail
/// </summary>
public partial class Utilisateur_Detail : System.Web.UI.Page
{

  private Utilisateur CurrentUtilisateur = null;

  void GetCurrentUtilisateur()
  {
    int IdUtilisateur;

    if (Request.QueryString["IdUtilisateur"] != null
        && Int32.TryParse((string)Request.QueryString["IdUtilisateur"], out IdUtilisateur))
    {
      if (CurrentUtilisateur == null)
        CurrentUtilisateur = new Utilisateur();

      if (CurrentUtilisateur.Load(IdUtilisateur) == false)
        CurrentUtilisateur = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Détail Utilisateur";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentUtilisateur();

      if (CurrentUtilisateur != null)
      {
        txtNomPrenom.Text = CurrentUtilisateur.NomPrenom;
        txtMotDePasse.Text = CurrentUtilisateur.MotDePasse;
        txtCommentaire.Text = CurrentUtilisateur.Commentaire;

        lstDroits.DataBind();

        foreach (ListItem li in lstDroits.Items)
        {
          li.Selected = ((CurrentUtilisateur.Droits & Int32.Parse(li.Value)) != 0);
        }

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eAdministration))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des modifications";
        }
      }
      else
      {
        txtNomPrenom.Text = "";
        txtMotDePasse.Text = "";
        txtCommentaire.Text = "";

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eAdministration))
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

    if (txtMotDePasse.Text.Trim().Length < 6)
    {
      ErrorMessage.Text = "<b>Le mot de passe doit comporter au moins 6 caractères !</b>";
      Page.SetFocus(txtMotDePasse);
      return;
    }

    // donnée client
    GetCurrentUtilisateur();
    if (CurrentUtilisateur == null)
      CurrentUtilisateur = new Utilisateur();

    // recupère les infos
    CurrentUtilisateur.NomPrenom = txtNomPrenom.Text.Trim();
    CurrentUtilisateur.MotDePasse = txtMotDePasse.Text.Trim();
    CurrentUtilisateur.Commentaire = txtCommentaire.Text.Trim();

    int droits = 0;
    foreach (ListItem li in lstDroits.Items)
    {
      if (li.Selected)
      {
        droits += Int32.Parse(li.Value);
        if (Int32.Parse(li.Value) == (int)Utilisateur.eDroit.eAdministration)
        {
          droits = -1;
          break;
        }
      }
    }
    CurrentUtilisateur.Droits = droits;

    try
    {
      if (CurrentUtilisateur.Save() == false)
      {
        ErrorMessage.Text = "Impossible de Sauvegarder !";
      }
      else
      {
        // retour à la liste des Utilisateurs
        Response.Redirect("Utilisateurs.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la sauvegarde:<br />" + ex.Message;
    }
  }
}
