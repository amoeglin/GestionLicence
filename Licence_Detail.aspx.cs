using System;
using System.Data;
using System.Data.SqlClient;
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

// Global.asax
using GestionLicences;

// numeric textbox
using DeKale;

/// <summary>
/// Summary description for Licence_Detail
/// </summary>
public partial class Licence_Detail : System.Web.UI.Page
{
  //
  // members
  //

  private Licence CurrentLicence = null;


  //
  // controls
  //



  //
  // methods
  //

  void GetCurrentLicence()
  {
    int IdLicence;

    if (Request.QueryString["IdLicence"] != null
        && Int32.TryParse((string)Request.QueryString["IdLicence"], out IdLicence))
    {
      if (CurrentLicence == null)
        CurrentLicence = new Licence();

      if (0 != (int)Session["CurrentLicence"])
        IdLicence = (int)Session["CurrentLicence"];
      else
        Session["CurrentLicence"] = IdLicence;

      if (CurrentLicence.Load(IdLicence) == false)
        CurrentLicence = null;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    Page.Title = Global.AppTitle + " - Détail Licence";

    ErrorMessage.Text = "";

    if (!Page.IsPostBack)
    {
      Session["CurrentLicence"] = 0;

      GetCurrentLicence();

      if (CurrentLicence != null)
      {
        Page.DataBind();

        cboClient.SelectedValue = CurrentLicence.IdClient.ToString();
        cboLogiciel.SelectedValue = CurrentLicence.IdLogiciel.ToString();

        txtSite.Text = CurrentLicence.NomSite;
        txtNbLicence.Text = CurrentLicence.NbLicence.ToString();
        txtNbLicenceFacturer.Text = CurrentLicence.NbLicenceFacture.ToString();
        Calendar1.VisibleDate = CurrentLicence.DateExpiration.Date;
        Calendar1.SelectedDate = CurrentLicence.DateExpiration.Date;
        txtDateExpiration.Text = CurrentLicence.DateExpiration.ToString("dd/MM/yyyy");
        txtPrixVente.Text = CurrentLicence.PrixVente.ToString("0.00");
        txtPrixMiseAJour.Text = CurrentLicence.PrixMiseAJour.ToString("0.00");
        txtRepertoire.Text = CurrentLicence.Repertoire;
        chkAbandonne.Checked = CurrentLicence.Abandonne;
        chkUNCPath.Checked = CurrentLicence.UseUNCPath;
        txtCommentaire.Text = CurrentLicence.Commentaire;

        // remplissage des combos contacts technique et administratif
        FillContactTechnique(CurrentLicence.IdClient, CurrentLicence.IdContactTechnique);
        FillContactAdministratif(CurrentLicence.IdClient, CurrentLicence.IdContactAdministratif);

        // gestion des boutons de message
        btnAddMachine.Enabled = true;
        btnAvertir.Enabled = true;
        btnInstallation.Enabled = true;
        btnRegenerate.Enabled = true;
        btnRenew.Enabled = true;
        lblStatus.Text = "";

        lblDateCreation.Text = "Créé le " + CurrentLicence.DateCreation.ToString("dd/MM/yyyy hh:mm");
        lblDateModification.Text = "Modifié le " + CurrentLicence.DateModification.ToString("dd/MM/yyyy hh:mm") + " par " + CurrentLicence.IdUtilisateur.ToString();

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eModification))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des modifications";
        }

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eCréation))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des créations";
        }
      }
      else
      {
        int IdClient = 0;

        Page.DataBind();

        if (Request.QueryString["IdClient"] != null
            && Int32.TryParse((string)Request.QueryString["IdClient"], out IdClient))
          cboClient.SelectedValue = IdClient.ToString();
        else
          cboClient.SelectedValue = null;

        cboLogiciel.SelectedValue = null;
        txtSite.Text = "";
        txtNbLicence.Text = "";
        txtNbLicenceFacturer.Text = "";
        Calendar1.SelectedDate = DateTime.Now;
        txtDateExpiration.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtPrixVente.Text = "";
        txtPrixMiseAJour.Text = "";
        txtRepertoire.Text = "";
        chkAbandonne.Checked = false;
        txtCommentaire.Text = "";
        chkUNCPath.Checked = true;

        // remplissage des combos contacts technique et administratif
        FillContactTechnique(IdClient, 0);
        FillContactAdministratif(IdClient, 0);

        // gestion des boutons de message
        btnAddMachine.Enabled = false;
        btnAvertir.Enabled = false;
        btnInstallation.Enabled = false;
        btnRegenerate.Enabled = false;
        btnRenew.Enabled = false;
        lblStatus.Text = "Vous devez enregistrer cette licence pour pouvoir générer des messages.";

        lblDateCreation.Text = "";
        lblDateModification.Text = "";

        if (!Utilisateur.UserHasRight((int)Session["DroitUtilisateur"], Utilisateur.eDroit.eCréation))
        {
          btnSave.Enabled = false;
          ErrorMessage.Text = "Vous n'avez pas les droits suffisant pour effectuer des créations";
        }
      }
    }
  }

  protected void btnCancel_Click(object sender, EventArgs e)
  {
    GetCurrentLicence();

    if (CurrentLicence != null)
      Response.Redirect("Clients.aspx?IdClient=" + CurrentLicence.IdClient.ToString());
    else
      Response.Redirect("Clients.aspx");
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    if (DoSave())
    {
      // retour à la liste des Licences client
      Response.Redirect("Clients.aspx?IdClient=" + CurrentLicence.IdClient.ToString());
    }
  }

  protected bool DoSave()
  {
    // erreurs sur la page ?
    if (Page.IsValid == false)
      return false;

    // donnée Licence
    GetCurrentLicence();
    if (CurrentLicence == null)
      CurrentLicence = new Licence();

    // recupère les infos
    double dbl;
    int n;

    CurrentLicence.IdClient = Int32.Parse(cboClient.SelectedValue);
    CurrentLicence.IdLogiciel = Int32.Parse(cboLogiciel.SelectedValue);
    CurrentLicence.NomSite = txtSite.Text;

    if (!Int32.TryParse(txtNbLicence.Text, out n))
    {
      ErrorMessage.Text = "Vous devez entrez un <b>nombre de licences à autoriser</b> correct.";
      Page.SetFocus(txtNbLicence);
      return false;
    }
    CurrentLicence.NbLicence = n;

    if (!Int32.TryParse(txtNbLicenceFacturer.Text, out n))
    {
      ErrorMessage.Text = "Vous devez entrez un <b>nombre de licences à facturer</b> correct.";
      Page.SetFocus(txtNbLicenceFacturer);
      return false;
    }
    CurrentLicence.NbLicenceFacture = n;

    CurrentLicence.DateExpiration = Calendar1.SelectedDate;

    if (!Double.TryParse(txtPrixVente.Text, out dbl))
    {
      ErrorMessage.Text = "Vous devez entrez un <b>prix de vente</b> correct.";
      Page.SetFocus(txtPrixVente);
      return false;
    }
    CurrentLicence.PrixVente = dbl;

    if (!Double.TryParse(txtPrixMiseAJour.Text, out dbl))
    {
      ErrorMessage.Text = "Vous devez entrez un <b>prix de mise à jour</b> correct.";
      Page.SetFocus(txtPrixVente);
      return false;
    }
    CurrentLicence.PrixMiseAJour = dbl;

    CurrentLicence.Repertoire = txtRepertoire.Text;
    CurrentLicence.Abandonne = chkAbandonne.Checked;
    CurrentLicence.Commentaire = txtCommentaire.Text;
    CurrentLicence.UseUNCPath = chkUNCPath.Checked;

    if (cboContactTechnique.SelectedValue != null)
      CurrentLicence.IdContactTechnique = Int32.Parse(cboContactTechnique.SelectedValue);
    else
      CurrentLicence.IdContactTechnique = 0;

    if (cboContactAdministratif.SelectedValue != null)
      CurrentLicence.IdContactAdministratif = Int32.Parse(cboContactAdministratif.SelectedValue);
    else
      CurrentLicence.IdContactAdministratif = 0;

    if (CurrentLicence.IdContactTechnique == 0 && CurrentLicence.IdContactAdministratif == 0)
    {
      ErrorMessage.Text = "<b>Vous devez sélectionner un contact technique et/ou un contact administratif !</b>";
      return false;
    }

    try
    {
      if (CurrentLicence.Save() == false)
      {
        ErrorMessage.Text = "Impossible de Sauvegarder !";
      }
    }
    catch (System.Exception ex)
    {
      ErrorMessage.Text = "Erreur durant la sauvegarde:<br />" + ex.Message;
    }

    return ProcessOptionLogiciel();
  }


  class toDo
  {
    public int id;
    public bool selectionner;

    public toDo()
    {
      id = -1;
      selectionner = false;
    }
  };

  protected bool ProcessOptionLogiciel()
  {
    List<toDo> lstToDo = new List<toDo>(Repeater1.Items.Count);

    // parcours le repeater pour créer la liste des actions à réaliser
    foreach (RepeaterItem it in Repeater1.Items)
    {
      toDo td = new toDo();

      foreach (Control c in it.Controls)
      {
        if (c.ID == "lblIdOptionLogiciel")
        {
          Label ctl = (Label)c;
          td.id = Int32.Parse(ctl.Text);
        }
        else
          if (c.ID == "chkSelectionner")
          {
            CheckBox ctl = (CheckBox)c;
            td.selectionner = ctl.Checked;
          }
      }

      if (td.id != -1 && td.selectionner == true)
        lstToDo.Add(td);
      else
        td = null;
    }

    /*
     * Execute les actions
    */

    using (SqlConnection cn = DataAccess.GetConnection())
    {
      SqlTransaction theTransaction = cn.BeginTransaction();

      try
      {

        // supprime les anciennes options
        SqlCommand theDeleteCommand = new SqlCommand("DELETE FROM OptionLicence WHERE IdLicence=@IdLicence", theTransaction.Connection, theTransaction);

        DataAccess.AddParamToSQLCmd(theDeleteCommand, "@IdLicence", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CurrentLicence.IdLicence);

        theDeleteCommand.ExecuteNonQuery();

        // parcours les actions
        foreach (toDo td in lstToDo)
        {
          if (td.selectionner == true)
          {
            // ajoute l'option
            SqlCommand theCommand = new SqlCommand("INSERT INTO OptionLicence(IdLicence, IdOptionLogiciel) VALUES (@IdLicence, @IdOptionLogiciel)", theTransaction.Connection, theTransaction);

            DataAccess.AddParamToSQLCmd(theCommand, "@IdLicence", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CurrentLicence.IdLicence);
            DataAccess.AddParamToSQLCmd(theCommand, "@IdOptionLogiciel", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, td.id);

            theCommand.ExecuteNonQuery();
          }
        }

        // valide les changements
        theTransaction.Commit();

        return true;
      }
      catch (System.Exception ex)
      {
        ErrorMessage.Text = "Erreur durant la sauvegarde:<br />" + ex.Message;

        // annule les changements
        theTransaction.Rollback();

        return false;
      }
    }
  }

  protected bool DisplayOptionLogiciel(RepeaterItem it)
  {
    // rempli le repeater
    if (CurrentLicence == null)
      GetCurrentLicence();

    if (CurrentLicence == null)
      return false;

    SqlDataReader theData = DataAccess.OpenReader("SELECT IdOptionLogiciel FROM OptionLicence WHERE IdLicence=" + CurrentLicence.IdLicence.ToString());

    while (theData.Read() == true)
    {
      toDo td = new toDo();

      foreach (Control c in it.Controls)
      {
        if (c.ID == "lblIdOptionLogiciel")
        {
          Label ctl = (Label)c;
          td.id = Int32.Parse(ctl.Text);
        }
        else
          if (c.ID == "chkSelectionner" && td.id == (int)theData["IdOptionLogiciel"])
          {
            CheckBox ctl = (CheckBox)c;
            ctl.Checked = true;
          }
      }

    }

    theData.Close();

    return true;
  }

  protected void txtDateExpiration_TextChanged(object sender, EventArgs e)
  {
    DateTime dt;

    if (DateTime.TryParse(txtDateExpiration.Text, out dt))
    {
      Calendar1.VisibleDate = dt;
      Calendar1.SelectedDate = dt;
    }
  }

  protected void Calendar1_SelectionChanged(object sender, EventArgs e)
  {
    txtDateExpiration.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
  }

  private void FillContactTechnique(int idClient, int idContact)
  {
    // remplissage du combo contact technique
    DataAccess.FillCombo(cboContactTechnique, "SELECT * FROM ListeContactTechnique WHERE IdClient=" + idClient.ToString(),
                             "NomPrenom", "IdContact", idContact, true, "(aucun)");
  }

  private void FillContactAdministratif(int idClient, int idContact)
  {
    // remplissage du combo contact technique
    DataAccess.FillCombo(cboContactAdministratif, "SELECT * FROM ListeContactAdministratif WHERE IdClient=" + idClient.ToString(),
                             "NomPrenom", "IdContact", idContact, true, "(aucun)");
  }

  protected void cboClient_SelectedIndexChanged(object sender, EventArgs e)
  {
    int idClient = Int32.Parse(cboClient.SelectedValue);

    FillContactTechnique(idClient, 0);
    FillContactAdministratif(idClient, 0);
  }

  protected void btnNewMachine_Click(object sender, EventArgs e)
  {
    /*    GetCurrentLicence();
        if (CurrentLicence == null)
          return;

        StringBuilder szSQL = new StringBuilder();

        szSQL.AppendFormat("INSERT INTO LicenceMachine(IdLicence, NomMachine, NomUtilisateur)"
                           + " VALUES({0}, 'Nouvelle Machine', 'Nouvel Utilisateur')", CurrentLicence.IdLicence);
    
        SqlCommand cmd = new SqlCommand(szSQL.ToString(), CurrentLicence.GetConnection());

        cmd.ExecuteNonQuery();

        dsLicenceMachine.InsertParameters["NomUtilisateur"] = "Nouvel Utilisateur";
        dsLicenceMachine.InsertParameters["NomMachine"] = "Nouvelle Machine";
        dsLicenceMachine.Insert();

        grdLicenceMachine.DataBind();*/
  }

  protected void btnAddMachine_Click(object sender, EventArgs e)
  {
    DetailsView1.Visible = true;
    btnAddMachine.Visible = false;
    DetailsView1.ChangeMode(DetailsViewMode.Insert);
  }

  protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
  {
    lblErreur.Text = "";
    if (e.Values["NomUtilisateur"] == null
        || e.Values["NomMachine"] == null)
    {
      e.Cancel = true;
      lblErreur.Text = "Vous devez entrer une valeur pour tous les champs !";
    }

    string nomMachine = e.Values["NomMachine"].ToString();
    e.Values["NomMachine"] = nomMachine.ToUpper();
  }

  protected void DetailsView1_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
  {
    lblErreur.Text = "";
    if (e.NewValues["NomUtilisateur"] == null
        || e.NewValues["NomMachine"] == null)
    {
      e.Cancel = true;
      lblErreur.Text = "Vous devez entrer une valeur pour tous les champs !";
    }

    string nomMachine = e.NewValues["NomMachine"].ToString();
    e.NewValues["NomMachine"] = nomMachine.ToUpper();
  }

  protected void grdLicenceMachine_RowUpdating(object sender, GridViewUpdateEventArgs e)
  {
    lblErreur.Text = "";
    if (e.NewValues["NomUtilisateur"] == null
        || e.NewValues["NomMachine"] == null)
    {
      e.Cancel = true;
      lblErreur.Text = "Vous devez entrer une valeur pour tous les champs !";
    }

    string nomMachine = e.NewValues["NomMachine"].ToString();
    e.NewValues["NomMachine"] = nomMachine.ToUpper();
  }

  protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
  {
    DetailsView1.Visible = false;
    btnAddMachine.Visible = true;
  }

  protected void DetailsView1_ModeChanged(object sender, EventArgs e)
  {
    if (DetailsView1.CurrentMode == DetailsViewMode.ReadOnly)
    {
      DetailsView1.Visible = false;
      btnAddMachine.Visible = true;
    }
  }

  protected void DoProcessLicencce(bool avertir, bool renouveler, bool regenerer)
  {
    GetCurrentLicence();
    if (CurrentLicence == null)
      return;

    StringBuilder strLog = new StringBuilder();

    ProcessLicence theProcessor = new ProcessLicence();

    theProcessor.Logger = strLog;

    // load models
    theProcessor.ModeleAvertissement = Int32.Parse(cboAvertissement.SelectedValue);
    theProcessor.ModeleRenouvellement = Int32.Parse(cboRenouvellement.SelectedValue);
    theProcessor.ModeleRegeneration = Int32.Parse(cboRegenereration.SelectedValue);

    // parcours les actions
    theProcessor.ExecuteJob(CurrentLicence.IdLicence, avertir, renouveler, regenerer, chkGarderTrace.Checked);

    // affichage du log d'execution
    lblStatus.Visible = true;
    lblStatus.Text = strLog.ToString();
  }

  protected void btnAvertir_Click(object sender, EventArgs e)
  {
    DoProcessLicencce(true, false, false);
  }

  protected void btnRenew_Click(object sender, EventArgs e)
  {
    DoProcessLicencce(false, true, false);
  }

  protected void btnRegenerate_Click(object sender, EventArgs e)
  {
    DoProcessLicencce(false, false, true);
  }

  protected void cboLogiciel_SelectedIndexChanged(object sender, EventArgs e)
  {
    dsOptionLogiciel.DataBind();
  }

  protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
  {
    DisplayOptionLogiciel(e.Item);
  }

  protected void btnInstallation_Click(object sender, EventArgs e)
  {
    GetCurrentLicence();
    if (CurrentLicence == null)
      return;

    if (cboInstallation.SelectedValue != null)
    {
      Session["IdLicence"] = CurrentLicence.IdLicence;
      Session["IdModele"] = Int32.Parse(cboInstallation.SelectedValue);
      Response.Redirect("Modele_Preview.aspx");
    }
  }

  protected void btnSaveChanges_Click(object sender, EventArgs e)
  {
    if (DoSave() == true)
    {
      btnAddMachine.Enabled = true;

      Session["CurrentLicence"] = CurrentLicence.IdLicence;

      Response.Redirect("Licence_Detail.aspx?IdLicence=" + CurrentLicence.IdLicence.ToString());
    }
  }

  protected void grdLicenceMachine_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (grdLicenceMachine.SelectedValue != null)
    {
      if (CurrentLicence == null)
        GetCurrentLicence();

      if (CurrentLicence == null)
        return;

      string NomPC = grdLicenceMachine.SelectedRow.Cells[2].Text;

      dsLicenceMachine_Mac.SelectCommand = "SELECT [MacAddress], [RepertoireLicence], [IdMachine] FROM [LicenceMachine_MajLicence] "
                                          + " WHERE IdLicence=" + CurrentLicence.IdLicence.ToString() + " AND NomMachine='" + NomPC.ToUpper() + "'";
      dsLicenceMachine_Mac.DataBind();
    }
  }
}
