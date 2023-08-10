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
/// Summary description for Envois
/// </summary>
public partial class Envois : System.Web.UI.Page
{

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Gestion des Envois";

    if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eEnvois))
    {
      Response.Redirect("Default.aspx");
    }
  }

  protected void chkEnvoiNonTraite_CheckedChanged(object sender, EventArgs e)
  {
    if (chkEnvoiNonTraite.Checked == true)
    {
      dsEnvois.SelectCommand = "SELECT [IdEnvoi], [Date], [Raison Sociale] AS Raison_Sociale, [Trace], [Avertissement], [Titre], NomComplet FROM [ListeEnvoisNonTraites] ORDER BY [Date] DESC";
    }
    else
    {
      dsEnvois.SelectCommand = "SELECT [IdEnvoi], [Date], [Raison Sociale] AS Raison_Sociale, [Trace], [Avertissement], [Titre], NomComplet FROM [ListeEnvois] ORDER BY [Date] DESC";
    }

    dsEnvois.DataBind();
  }
}
