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
/// Summary description for Utilisateur_Delete
/// </summary>
public partial class Utilisateur_Delete:System.Web.UI.Page
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
        
            if (CurrentUtilisateur.Load(IdUtilisateur)==false)
                CurrentUtilisateur = null;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = Global.AppTitle + " - Suppression Utilisateur";

        ErrorMessage.Text = "";

        if (!Page.IsPostBack)
        {
            GetCurrentUtilisateur();

            if (CurrentUtilisateur != null)
            {
                lblUtilisateur.Text = CurrentUtilisateur.NomPrenom;
                if (CurrentUtilisateur.NomPrenom == "Administrateur")
                {
                    ErrorMessage.Text = "<b>L'administrateur ne peut pas être supprimé !</b>";
                    btnSave.Enabled = false;
                }

                if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eSuppression))
                {
                    btnSave.Enabled = false;
                    ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des suppressions";
                }
            }
            else
            {
                lblUtilisateur.Text = "Erreur !";
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Utilisateurs.aspx");
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
            if (CurrentUtilisateur.Delete() == false)
            {
                ErrorMessage.Text = "Impossible de Supprimer !";
            }
            else
            {
                // retour à la liste des Utilisateurs
                Response.Redirect("Utilisateurs.aspx");
            }
       }
       catch (System.Exception ex)
       {
           ErrorMessage.Text = "Erreur durant la suppression:<br />" + ex.Message;
       }
   }
    
}
