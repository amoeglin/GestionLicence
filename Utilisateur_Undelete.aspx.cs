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
/// Summary description for Utilisateur_Undelete
/// </summary>
public partial class Utilisateur_Undelete : System.Web.UI.Page
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
    Page.Title = Global.AppTitle + " - Récupération Utilisateur";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentUtilisateur();

      if (CurrentUtilisateur != null)
      {
        lblUtilisateur.Text = CurrentUtilisateur.NomPrenom;
      }
      else
      {
        lblUtilisateur.Text = "Erreur !";
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

    // donnée Utilisateur
    GetCurrentUtilisateur();
    if (CurrentUtilisateur == null)
      CurrentUtilisateur = new Utilisateur();

    try
    {
      if (CurrentUtilisateur.Undelete() == false)
      {
        ErrorMessage.Text = "Impossible de Récupérer !";
      }
      else
      {
        // retour à la liste des Utilisateurs
        Response.Redirect("Administration.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la récupération:<br />" + ex.Message;
    }
  }

}
