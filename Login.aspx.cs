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
/// Summary description for Login
/// </summary>
public partial class Login_Page : System.Web.UI.Page
{
  protected void Logon_Click(object sender, EventArgs e)
  {
    //DataAccess da = new DataAccess();
    int idUtilisateur = 0;

    if (DataAccess.VerifyUser(UserEmail.Text.Trim(), UserPass.Text.Trim(), ref idUtilisateur) == true)
    {

      if (idUtilisateur != 0)
      {
        Utilisateur LoggedUser = new Utilisateur();

        if (LoggedUser.Load(idUtilisateur) == false)
        {
          Msg.Text = "Problème de login (erreur 127) !";
          return;
        }

        Session["DroitUtilisateur"] = LoggedUser.Droits;
        Session["IdUtilisateur"] = LoggedUser.IdUtilisateur;
      }

      FormsAuthentication.RedirectFromLoginPage(UserEmail.Text, false);
    }
    else
    {
      Msg.Text = "Essaye encore !";
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.SetFocus(UserEmail);

    if (Global.gDebugMode == true)
    {
      lblDebug.Text = "<b>[Debug Mode]</b>";
      lblDebug.Visible = true;
    }
    else
      lblDebug.Visible = false;

  }

}
