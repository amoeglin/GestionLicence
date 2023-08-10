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
/// Summary description for Clients
/// </summary>
public partial class Clients : System.Web.UI.Page
{

    //"""TEST
    protected void cmdFill_Click(object sender, EventArgs e)
    {
        FillGridFromDB();
    }

    protected void cmdSansClient_Click(object sender, EventArgs e)
    {
        GridView1.Columns[4].Visible = false;
        GridView1.DataSource = dsClients;
        GridView1.DataBind();
    }

    private void FillGridFromDB()
    {
        GridView1.Columns[4].Visible = true;
        GridView1.DataSource = null;
        GridView1.DataBind();

        string[] cols;
        string query = "SELECT * FROM Client";
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();

        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        cn.Open();

        SqlCommand cmd = new SqlCommand(query, cn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dataTable);

        GridView1.DataSource = dataTable;
        GridView1.DataBind();

        //fill the contacts combo
        foreach (GridViewRow row in GridView1.Rows)
        {
            //Label Id = row.FindControl("lblId") as Label;
            DropDownList lst = row.FindControl("lstContacts") as DropDownList;

            //var id = row.Cells[1].Text;
            string id = GridView1.DataKeys[row.RowIndex].Value.ToString();

            query = "Select NomPrenom From Contact Where idclient = " + id;
            dataTable2 = new DataTable();

            SqlCommand cmd2 = new SqlCommand(query, cn);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dataTable2);

            lst.DataSource = dataTable2;
            lst.DataTextField = "NomPrenom";
            lst.DataBind();


            //var result = Employee.GetEmployeeById(Id.Text);

            //if (result.Count > 0)
            //{
            //    CheckBox chkBox = row.FindControl("chkSelected") as CheckBox;
            //    if (chkBox != null)
            //    {
            //        chkBox.Checked = result.Any(x => x.Id.ToString() == Id.Text);

            //    }
            //}

        }


        cn.Close();
        da.Dispose();


        //SqlDataReader dr = cmdRead.ExecuteReader();

        //cols = new string[dr.FieldCount];

        //for (int i = 0; i < dr.FieldCount; i++)
        //{
        //    cols[i] = dr.GetName(i);
        //}

        //while (dr.Read())
        //{ 
        //    //strNatAss = dr.GetValue("FFF").ToString();
        //}


    }

    protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Gestion des Clients";

    if (Session["Filtre_Clients_aspx"] != null)
      DoFiltreClient();

    if (!Page.IsPostBack)
    {
            GridView1.Columns[4].Visible = false;
            GridView1.DataSource = dsClients;
            GridView1.DataBind();
            GridView1.SelectedIndex = 0;

            SelectCurrentClient();

            if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eCréation))
            {
            btnNouveauClient.Enabled = false;
            btnNouveauContact.Enabled = false;
            }
    }
  }

  private void SelectCurrentClient()
  {
    int IdClient;

    if (Request.QueryString["IdClient"] != null
        && Int32.TryParse((string)Request.QueryString["IdClient"], out IdClient))
    {
      //            if (Session["IdClient"] != null)
      //            {
      //                IdClient = (int)Session["IdClient"];

      DataAccess.SelectGridRow(GridView1, IdClient);
    }
  }

  protected void btnNouveau_Click(object sender, EventArgs e)
  {
    //        Session["IdClient"] = null;
    //        Response.Redirect("Client_Detail.aspx");
    Response.Redirect("Client_Detail.aspx?IdClient=0");
  }

  protected void DoFiltreClient()
  {
    string filtre;

    if (Session["Filtre_Clients_aspx"] != null)
      filtre = (string)Session["Filtre_Clients_aspx"];
    else
      filtre = "";
    
    //txtFiltreClient.Text = filtre;
    string aaa = txtFiltreClient.Text;

    if (filtre.Length > 0)
      dsClients.FilterExpression = "RaisonSociale like '" + filtre + "%'";
    else
      dsClients.FilterExpression = null;

    SelectCurrentClient();
  }

  protected void btnFiltreClient_Click(object sender, EventArgs e)
  {
    string filtre = txtFiltreClient.Text.Trim();

    DataAccess.RemoveInvalidChar(ref filtre);

    if (filtre.Length > 0)
    {
      Session["Filtre_Clients_aspx"] = filtre;
      DoFiltreClient();
    }
    else
      Session.Remove("Filtre_Clients_aspx");
  }

  protected void btnAucunFiltreClient_Click(object sender, EventArgs e)
  {
    Session.Remove("Filtre_Clients_aspx");
    txtFiltreClient.Text = "";
    
    DoFiltreClient();
  }

  protected void btnFindContact_Click(object sender, EventArgs e)
  {
    string filtre = txtFindContact.Text.Trim();

    DataAccess.RemoveInvalidChar(ref filtre);

    txtFindContact.Text = filtre;

    if (filtre.Length > 0)
      dsContact.FilterExpression = "NomPrenom like '" + filtre + "%'";
    else
      dsContact.FilterExpression = null;
  }

  protected void btnNewLicence_Click(object sender, EventArgs e)
  {
    Response.Redirect(string.Format("Licence_Detail.aspx?IdLicence=0&IdClient={0}", GridView1.SelectedValue));
  }

  protected void btnNouveauContact_Click(object sender, EventArgs e)
  {
    Response.Redirect(string.Format("Contact_Detail.aspx?IdContact=0&IdClient={0}", GridView1.SelectedValue));
  }

  protected void btnSeekContact_Click(object sender, EventArgs e)
  {
    lblSeekError.Text = "";

    if (txtSeekContact.Text.Trim().Length == 0)
      return;

    SqlDataReader theData = DataAccess.OpenReader("SELECT IdClient, IdContact FROM Contact WHERE NomPrenom LIKE '%" + txtSeekContact.Text + "%'");

    if (theData.Read() == true)
    {
      int idClient = (int)theData["IdClient"];
      int idContact = (int)theData["IdContact"];

      // selectionne le client
      DataAccess.SelectGridRow(GridView1, idClient);

      // selectionne le contact
      GridView2.DataBind();
      DataAccess.SelectGridRow(GridView2, idContact);
    }
    else
      lblSeekError.Text = "Aucun contact ne correspond à votre recherche.";

    theData.Close();
    theData.Dispose();
  }


    
}
