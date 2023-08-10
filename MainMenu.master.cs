using System;
using System.Text;
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
/// Summary description for MainMenu
/// </summary>
public partial class MainMenu_Master : System.Web.UI.MasterPage
{

  public MainMenu_Master()
  {
    //
    // TODO: Add constructor logic here
    //
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    lblClock.Text = "Nous sommes le " + DateTime.Now.ToString("dddd dd MMMM yyyy à HH:mm:ss");

    if (Global.gDebugMode == true)
    {
      lblDebug.Text = "<b>[Debug Mode]</b>";
      lblDebug.Visible = true;
    }
    else
      lblDebug.Visible = false;
  }

}
