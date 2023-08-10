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
/// Summary description for Licence_Undelete
/// </summary>
public partial class Licence_Undelete : System.Web.UI.Page
{

  private Licence CurrentLicence = null;

  void GetCurrentLicence()
  {
    int IdLicence;

    if (Request.QueryString["IdLicence"] != null
        && Int32.TryParse((string)Request.QueryString["IdLicence"], out IdLicence))
    {
      if (CurrentLicence == null)
        CurrentLicence = new Licence();

      if (CurrentLicence.Load(IdLicence) == false)
        CurrentLicence = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Récupération Licence";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentLicence();

      if (CurrentLicence != null)
      {
        Logiciel logiciel = new Logiciel();

        logiciel.Load(CurrentLicence.IdLogiciel);

        Client client = new Client();

        client.Load(CurrentLicence.IdClient);

        lblLicence.Text = "Id : <b>" + CurrentLicence.IdLicence + "</b>  -  Logiciel : <b>" + logiciel.NomLogiciel + "</b> - Client : <b>" + client.RaisonSociale + "</b> - Site : <b>" + CurrentLicence.NomSite + "</b>";
      }
      else
      {
        lblLicence.Text = "Erreur !";
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

    // donnée Licence
    GetCurrentLicence();
    if (CurrentLicence == null)
      CurrentLicence = new Licence();

    try
    {
      if (CurrentLicence.Undelete() == false)
      {
        ErrorMessage.Text = "Impossible de Récupérer !";
      }
      else
      {
        // retour à la liste des Licences
        Response.Redirect("Administration.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la récupération:<br />" + ex.Message;
    }
  }

}
