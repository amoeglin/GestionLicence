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
/// Summary description for Utilisateurs
/// </summary>
public partial class Utilisateurs : System.Web.UI.Page
{
  public Utilisateurs()
  {
    //
    // TODO: Add constructor logic here
    //
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Gestion des Utilisateurs";

    if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eAdministration))
    {
      Response.Redirect("Default.aspx");
    }
  }

}
