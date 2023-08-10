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
/// Summary description for OptionLogiciel_Undelete
/// </summary>
public partial class OptionLogiciel_Undelete : System.Web.UI.Page
{

  private OptionLogiciel CurrentOptionLogiciel = null;

  void GetCurrentOptionLogiciel()
  {
    int IdOptionLogiciel;

    if (Request.QueryString["IdOptionLogiciel"] != null
        && Int32.TryParse((string)Request.QueryString["IdOptionLogiciel"], out IdOptionLogiciel))
    {
      if (CurrentOptionLogiciel == null)
        CurrentOptionLogiciel = new OptionLogiciel();

      if (CurrentOptionLogiciel.Load(IdOptionLogiciel) == false)
        CurrentOptionLogiciel = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Récupération Option de Logiciel";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentOptionLogiciel();

      if (CurrentOptionLogiciel != null)
      {
        lblOptionLogiciel.Text = CurrentOptionLogiciel.NomOption + "  -  " + CurrentOptionLogiciel.NomLicence;
      }
      else
      {
        lblOptionLogiciel.Text = "Erreur !";
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

    // donnée OptionLogiciel
    GetCurrentOptionLogiciel();
    if (CurrentOptionLogiciel == null)
      CurrentOptionLogiciel = new OptionLogiciel();

    try
    {
      if (CurrentOptionLogiciel.Undelete() == false)
      {
        ErrorMessage.Text = "Impossible de Récupérer !";
      }
      else
      {
        // retour à la liste des OptionLogiciels
        Response.Redirect("Administration.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la récupération:<br />" + ex.Message;
    }
  }

}
