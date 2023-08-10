namespace GestionLicences
{
  using System;
  using System.Data;
  using System.Configuration;
  using System.Web.Configuration;
  using System.Web;
  using System.Web.Security;
  using System.Web.UI;
  using System.Web.UI.WebControls;
  using System.Web.UI.WebControls.WebParts;
  using System.Web.UI.HtmlControls;


  /// <summary>
  /// Summary description for Global
  /// </summary>
  public class Global : System.Web.HttpApplication
  {
    #region Web Form Designer generated code
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
    }
    #endregion

    public Global()
    {
      InitializeComponent();
    }

    public const string AppTitle = "Gestion des licences logiciels";
    public static bool gDebugMode = false;

    void Application_Start(object sender, EventArgs e)
    {
      // Code that runs on application startup
      CompilationSection compilation = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");

      gDebugMode = compilation.Debug;

      Application["DebugMode"] = compilation.Debug;

      Application["Hits"] = 0;
      Application["Sessions"] = 0;
      Application["TerminatedSessions"] = 0;
    }

    void Application_End(object sender, EventArgs e)
    {
      //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
      // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
      // Code that runs when a new session is started
      Session["DroitUtilisateur"] = 0;
      Session["IdUtilisateur"] = -1;

      Application.Lock();
      Application["Sessions"] = (int)Application["Sessions"] + 1;
      Application.UnLock();
    }

    void Session_End(object sender, EventArgs e)
    {
      // Code that runs when a session ends. 
      // Note: The Session_End event is raised only when the sessionstate mode
      // is set to InProc in the Web.config file. If session mode is set to StateServer 
      // or SQLServer, the event is not raised.

      Application.Lock();
      Application["TerminatedSessions"] = (int)Application["TerminatedSessions"] + 1;
      Application["Sessions"] = (int)Application["Sessions"] - 1;
      Application.UnLock();
    }

    //The BeginRequest event is fired for every hit to every page in the site
    void Application_BeginRequest(Object Sender, EventArgs e)
    {
      Application.Lock();
      Application["Hits"] = (int)Application["Hits"] + 1;
      Application.UnLock();
    }
  }
}