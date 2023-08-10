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
/// Summary description for Logiciel_Undelete
/// </summary>
public partial class Logiciel_Undelete : System.Web.UI.Page
{

  private Logiciel CurrentLogiciel = null;

  void GetCurrentLogiciel()
  {
    int IdLogiciel;

    if (Request.QueryString["IdLogiciel"] != null
        && Int32.TryParse((string)Request.QueryString["IdLogiciel"], out IdLogiciel))
    {
      if (CurrentLogiciel == null)
        CurrentLogiciel = new Logiciel();

      if (CurrentLogiciel.Load(IdLogiciel) == false)
        CurrentLogiciel = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Récupération Logiciel";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentLogiciel();

      if (CurrentLogiciel != null)
      {
        lblLogiciel.Text = CurrentLogiciel.NomLogiciel + "  -  " + CurrentLogiciel.NomLicence;
      }
      else
      {
        lblLogiciel.Text = "Erreur !";
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

    // donnée Logiciel
    GetCurrentLogiciel();
    if (CurrentLogiciel == null)
      CurrentLogiciel = new Logiciel();

    try
    {
      if (CurrentLogiciel.Undelete() == false)
      {
        ErrorMessage.Text = "Impossible de Récupérer !";
      }
      else
      {
        // retour à la liste des Logiciels
        Response.Redirect("Administration.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la récupération:<br />" + ex.Message;
    }
  }

}
