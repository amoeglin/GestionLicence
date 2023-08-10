using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using GestionLicences;

/// <summary>
/// Summary description for Client_Undelete
/// </summary>
public partial class Client_Undelete : System.Web.UI.Page
{

  private Client CurrentClient = null;

  void GetCurrentClient()
  {
    int IdClient;

    if (Request.QueryString["IdClient"] != null
        && Int32.TryParse((string)Request.QueryString["IdClient"], out IdClient))
    {
      if (CurrentClient == null)
        CurrentClient = new Client();

      if (CurrentClient.Load(IdClient) == false)
        CurrentClient = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Récupération Client";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      GetCurrentClient();

      if (CurrentClient != null)
      {
        lblClient.Text = CurrentClient.NumeroClient + "  -  " + CurrentClient.RaisonSociale;
      }
      else
      {
        lblClient.Text = "Erreur !";
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

    // donnée client
    GetCurrentClient();
    if (CurrentClient == null)
      CurrentClient = new Client();

    try
    {
      if (CurrentClient.Undelete() == false)
      {
        ErrorMessage.Text = "Impossible de Récupérer !";
      }
      else
      {
        // retour à la liste des clients
        Response.Redirect("Administration.aspx");
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la récupération:<br />" + ex.Message;
    }
  }

}
