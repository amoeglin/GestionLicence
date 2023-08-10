using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ChangePassword
/// </summary>
public partial class ChangePassword : System.Web.UI.Page
{
	public ChangePassword()
	{
		//
		// TODO: Add constructor logic here
		//
	}

  protected void Logon_Click(object sender, EventArgs e)
  {
    int idUtilisateur = (int)Session["IdUtilisateur"];

    Utilisateur CurrentUtilisateur = new Utilisateur();

    if (CurrentUtilisateur.Load(idUtilisateur) == true)
    {
      if (CurrentUtilisateur.MotDePasse != txtOldPassword.Text.Trim())
      {
        Msg.Text = "<b>L'ancien mot de passe est incorrect !</b>";
        return;
      }

      if (txtNewPassword.Text.Trim().Length < 6)
      {
        Msg.Text = "<b>Le mot de passe doit comporter au moins 6 caractères !</b>";
        return;
      }

      if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
      {
        Msg.Text = "<b>Les nouveaux mots de passe ne correspondent pas !</b>";
        return;
      }

      CurrentUtilisateur.MotDePasse = txtNewPassword.Text.Trim();

      CurrentUtilisateur.Save();

      Msg.Text = "<b>Votre mot de passe a été changé.</b>";
    }
    else
    {
      Msg.Text = "<b>Utilisateur introuvable !<br />Contactez l'administrateur du site !</b>";
    }

  }

}
