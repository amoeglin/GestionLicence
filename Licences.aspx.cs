using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;

using GestionLicences;

/// <summary>
/// Summary description for Licences
/// </summary>
public partial class Licences : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Gestion des Licences";

    if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eEnvois))
    {
      Response.Redirect("Default.aspx");
    }

    if (!Page.IsPostBack)
    {
      Page.DataBind();

      // affiche la liste des licences arrivant à expiration
      MultiView1.ActiveViewIndex = 0;
    }

    lblLegend.Text = string.Format("&nbsp;Licences expirant avant le {0:dd/MM/yyyy}&nbsp;", DateTime.Now.AddDays(Int32.Parse(cboNbJour.SelectedValue)));

    if (cboLogiciel.SelectedItem != null)
      lblLegendeLogiciel.Text = string.Format("&nbsp;Licences du logiciel {0}&nbsp;", cboLogiciel.SelectedItem.Text);
  }

  protected void cboNbJour_SelectedIndexChanged(object sender, EventArgs e)
  {
    dsLicences.DataBind();
  }

  protected void btnListExpired_Click(object sender, EventArgs e)
  {
    /*int nbJours = Int32.Parse(cboNbJour.SelectedValue);
        
    dsLicences.SelectCommand = "SELECT [IdLicence], [RaisonSociale], [Logiciel], [PrixMiseAJour], "
                             + " [PrixVente], [DateExpiration], [NbLicence], [NomSite], [IdClient], "
                             + " [NomMachine], [Repertoire], [Abandonne], [Commentaire] FROM [ListeLicences] "
                             + " WHERE [DateExpiration] <= DATEADD(day, " + nbJours.ToString() + ", GetDate())";*/
    dsLicences.DataBind();
  }

  class toDo
  {
    public int id;
    public bool renouveler;
    public bool regenerer;
    public bool avertir;

    public toDo()
    {
      id = -1;
      renouveler = false;
      regenerer = false;
      avertir = false;
    }
  };

  protected void btnTratement_Click(object sender, EventArgs e)
  {
    Server.ScriptTimeout = 90;
    
    List<toDo> lstToDo = new List<toDo>(Repeater1.Items.Count);

    // parcours le repeater pour créer la liste des actions à réaliser
    foreach (RepeaterItem it in Repeater1.Items)
    {
      toDo td = new toDo();

      foreach (Control c in it.Controls)
      {
        if (c.ID == "lblId")
        {
          Label ctl = (Label)c;
          td.id = Int32.Parse(ctl.Text);
        }
        else
          if (c.ID == "chkRenouveller")
          {
            CheckBox ctl = (CheckBox)c;
            td.renouveler = ctl.Checked;
          }
          else
            if (c.ID == "chkRegenerer")
            {
              CheckBox ctl = (CheckBox)c;
              td.regenerer = ctl.Checked;
            }
            else
              if (c.ID == "chkAvertir")
              {
                CheckBox ctl = (CheckBox)c;
                td.avertir = ctl.Checked;
              }
      }

      if (td.id != -1 && (td.avertir == true || td.regenerer == true || td.renouveler == true))
        lstToDo.Add(td);
    }

    /*
     * Execute les actions
    */

    StringBuilder strLog = new StringBuilder();

    ProcessLicence theProcessor = new ProcessLicence();

    theProcessor.Logger = strLog;

    // load models
    theProcessor.ModeleAvertissement = Int32.Parse(cboAvertissement.SelectedValue);
    theProcessor.ModeleRenouvellement = Int32.Parse(cboRenouvellement.SelectedValue);
    theProcessor.ModeleRegeneration = Int32.Parse(cboRegenereration.SelectedValue);

    // parcours les actions
    foreach (toDo td in lstToDo)
    {
      theProcessor.ExecuteJob(td.id, td.avertir, td.renouveler, td.regenerer, chkGarderTrace.Checked);
    }

    // affichage du log d'execution
    ErrorMessage.Text = strLog.ToString();

    dsLicences.DataBind();
    Repeater1.DataBind();
  }


  protected void btnRenewAll_Click(object sender, EventArgs e)
  {
    // parcours la liste pour voir les actions à réaliser
    foreach (RepeaterItem it in Repeater1.Items)
    {
      foreach (Control c in it.Controls)
      {
        if (c.ID == "chkRenouveller")
        {
          CheckBox ctl = (CheckBox)c;
          ctl.Checked = !ctl.Checked;
        }
      }
    }
  }

  protected void btnRenewAllAvertir_Click(object sender, EventArgs e)
  {
    // parcours la liste pour voir les actions à réaliser
    foreach (RepeaterItem it in Repeater1.Items)
    {
      foreach (Control c in it.Controls)
      {
        if (c.ID == "chkAvertir")
        {
          CheckBox ctl = (CheckBox)c;
          ctl.Checked = !ctl.Checked;
        }
      }
    }
  }

  protected void btnUpdAllAvertir_Click(object sender, EventArgs e)
  {
    // parcours la liste pour voir les actions à réaliser
    foreach (RepeaterItem it in rptLicenceLogiciel.Items)
    {
      foreach (Control c in it.Controls)
      {
        if (c.ID == "chkAvertir")
        {
          CheckBox ctl = (CheckBox)c;
          ctl.Checked = !ctl.Checked;
        }
      }
    }
  }

  enum eDestination { ePreview, ePreviewUpd };

  /// <summary>
  /// Rempli lblPreveiw avec le contenu du modèle
  /// </summary>
  /// <param name="idModele">clé IdModele du modele à afficher</param>
  private void PreviewModele(eDestination eUpdate, string idModele)
  {
    Modele CurrentModele = null;

    int IdModele;

    if (idModele != null
        && Int32.TryParse(idModele, out IdModele))
    {
      if (CurrentModele == null)
        CurrentModele = new Modele();

      if (CurrentModele.Load(IdModele) == false)
        CurrentModele = null;
    }

    if (eUpdate == eDestination.ePreview)
    {
      // pour les licences
      if (CurrentModele != null)
      {
        lblPreviewTitle.Text = "&nbsp;" + CurrentModele.Nom + "&nbsp;";
        lblPreview.Text = CurrentModele.Contenu;
      }
      else
      {
        lblPreviewTitle.Text = "&nbsp;Modèle introuvable&nbsp;";
        lblPreview.Text = "<b>Erreur lors du chargement du modèle !<b>";
      }
    }
    else
    {
      // pour les mises à jour de logiciel
      if (CurrentModele != null)
      {
        lblPreviewTitleUpd.Text = "&nbsp;" + CurrentModele.Nom + "&nbsp;";
        lblPreviewUpd.Text = CurrentModele.Contenu;
      }
      else
      {
        lblPreviewTitleUpd.Text = "&nbsp;Modèle introuvable&nbsp;";
        lblPreviewUpd.Text = "<b>Erreur lors du chargement du modèle !<b>";
      }
    }
  }

  protected void btnPreviewAvertissement_Click(object sender, ImageClickEventArgs e)
  {
    PreviewModele(eDestination.ePreview, cboAvertissement.SelectedValue);
  }

  protected void btnPreviewRenouvellement_Click(object sender, ImageClickEventArgs e)
  {
    PreviewModele(eDestination.ePreview, cboRenouvellement.SelectedValue);
  }

  protected void btnPreviewRegeneration_Click(object sender, ImageClickEventArgs e)
  {
    PreviewModele(eDestination.ePreview, cboRegenereration.SelectedValue);
  }

  protected void btnExpiringLicences_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 0;
  }

  protected void btnLicencesLogiciel_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 1;
  }

  protected void btnListNbLicence_Click(object sender, EventArgs e)
  {
    MultiView1.ActiveViewIndex = 2;
    dsNbLicences.DataBind();
  }

  protected void btnListeLogiciels_Click(object sender, EventArgs e)
  {
    dsLicenceUpd.DataBind();
  }

  protected void btnPreviewMiseAJour_Click(object sender, ImageClickEventArgs e)
  {
    PreviewModele(eDestination.ePreviewUpd, cboMiseAJour.SelectedValue);
  }

  protected void cboLogiciel_SelectedIndexChanged(object sender, EventArgs e)
  {
    dsLicenceUpd.DataBind();
  }

  protected void btnTraitementUpd_Click(object sender, EventArgs e)
  {
    List<toDo> lstToDo = new List<toDo>(rptLicenceLogiciel.Items.Count);

    // parcours le repeater pour créer la liste des actions à réaliser
    foreach (RepeaterItem it in rptLicenceLogiciel.Items)
    {
      toDo td = new toDo();

      foreach (Control c in it.Controls)
      {
        if (c.ID == "lblId")
        {
          Label ctl = (Label)c;
          td.id = Int32.Parse(ctl.Text);
        }
        else
          if (c.ID == "chkAvertir")
          {
            CheckBox ctl = (CheckBox)c;
            td.avertir = ctl.Checked;
          }
      }

      if (td.id != -1 && (td.avertir == true || td.regenerer == true || td.renouveler == true))
        lstToDo.Add(td);
      else
        td = null;
    }

    /*
     * Execute les actions
    */

    StringBuilder strLog = new StringBuilder();

    ProcessLicence theProcessor = new ProcessLicence();

    theProcessor.Logger = strLog;

    // load models
    theProcessor.ModeleAvertissement = Int32.Parse(cboMiseAJour.SelectedValue);

    // parcours les actions
    foreach (toDo td in lstToDo)
    {
      theProcessor.ExecuteJob(td.id, td.avertir, false, false, chkGarderTraceUpd.Checked);
    }

    // affichage du log d'execution
    ErrorMessageUpd.Text = strLog.ToString();
  }
}
