<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Détail Licence" 
  CodeFile="Licence_Detail.aspx.cs" Inherits="Licence_Detail" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
  Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Namespace="DeKale" TagPrefix="kale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  &nbsp;&nbsp;&nbsp;
  <table cellspacing="4" cellpadding="0">
    <tr>
      <td style="width: 4px">
      </td>
      <td style="border: none; width: 400px;" valign="top">
        <asp:Label ID="Label8" runat="server" Text="Client"></asp:Label><br />
        <asp:DropDownList ID="cboClient" runat="server" DataSourceID="dsClient" DataTextField="NomCLient"
          DataValueField="IdClient" Width="335px" OnSelectedIndexChanged="cboClient_SelectedIndexChanged"
          AutoPostBack="True">
        </asp:DropDownList><asp:SqlDataSource ID="dsClient" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT [IdClient], [NumeroClient] + '  -  ' + [RaisonSociale] as [NomCLient] FROM [Client] &#13;&#10;WHERE Supprime=0 OR Supprime IS NULL&#13;&#10;ORDER BY [RaisonSociale]">
        </asp:SqlDataSource>
        <asp:RequiredFieldValidator ID="valCLient" runat="server" ControlToValidate="cboClient"
          Display="Dynamic" ErrorMessage="Vous devez choisir un <b>client</b> !"></asp:RequiredFieldValidator></td>
      <td>
        <asp:Label ID="Label7" runat="server" Text="Logiciel"></asp:Label>
        <br />
        <asp:DropDownList ID="cboLogiciel" runat="server" DataSourceID="dsLogiciel" DataTextField="NomComplet"
          DataValueField="IdLogiciel" Width="335px" AutoPostBack="True" OnSelectedIndexChanged="cboLogiciel_SelectedIndexChanged">
        </asp:DropDownList><br />
        <asp:SqlDataSource ID="dsLogiciel" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT [IdLogiciel], [NomComplet] FROM [ListeLogiciels] ORDER BY [NomComplet]"></asp:SqlDataSource>
        <asp:RequiredFieldValidator ID="valLogiciel" runat="server" ControlToValidate="cboLogiciel"
          Display="Dynamic" ErrorMessage="Vous devez choisir un <b>logiciel</b> !"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
      <td style="width: 4px">
      </td>
      <td style="border-right: medium none; border-top: medium none; border-left: medium none;
        width: 400px; border-bottom: medium none" valign="top">
        <br />
        <asp:Label ID="Label1" runat="server" Text="Site"></asp:Label><br />
        <asp:TextBox ID="txtSite" runat="server" Width="335px"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="valSite" runat="server" ControlToValidate="txtSite"
          ErrorMessage="Vous devez entrer un <b>nom de Site</b> !" Display="Dynamic"></asp:RequiredFieldValidator><br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Répertoire"></asp:Label><asp:TextBox
          ID="txtRepertoire" runat="server" Width="335px"></asp:TextBox></td>
      <td valign="middle">
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="dsOptionLogiciel" OnItemDataBound="Repeater1_ItemDataBound">
          <HeaderTemplate>
            <table cellspacing="0" cellpadding="4">
              <tr>
                <td style="border: solid 1px LightSteelBlue; color: #333333; border-collapse: collapse;">
                  <table cellpadding="4" cellspacing="0">
                    <tr style="color: White; background-color: #507CD1; font-weight: bold;">
                      <th>
                        <%--Id--%>
                      </th>
                      <th>
                        Option</th>
                      <th>
                      </th>
                    </tr>
          </HeaderTemplate>
          <ItemTemplate>
            <tr style="color: #333333; background-color: #EFF3FB;">
              <td align="center">
                <asp:Label ID="lblIdOptionLogiciel" runat="server" ForeColor="#EFF3FB" Text='<%# Eval("IdOptionLogiciel") %>'></asp:Label>
              </td>
              <td>
                <asp:Label ID="lblNomOption" runat="server" Text='<%# Eval("NomOption") %>'></asp:Label>
              </td>
              <td align="center">
                <asp:CheckBox ID="chkSelectionner" runat="server" />
              </td>
            </tr>
          </ItemTemplate>
          <AlternatingItemTemplate>
            <tr style="color: #333333; background-color: white;">
              <td align="center">
                <asp:Label ID="lblIdOptionLogiciel" runat="server" ForeColor="white" Text='<%# Eval("IdOptionLogiciel") %>'></asp:Label>
              </td>
              <td>
                <asp:Label ID="lblNomOption" runat="server" Text='<%# Eval("NomOption") %>'></asp:Label>
              </td>
              <td align="center">
                <asp:CheckBox ID="chkSelectionner" runat="server" />
              </td>
            </tr>
          </AlternatingItemTemplate>
          <FooterTemplate>
            </table> </td> </tr> </table>
          </FooterTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="dsOptionLogiciel" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT [IdOptionLogiciel], [NomOption] FROM [OptionLogiciel] WHERE [IdLogiciel] = @IdLogiciel AND ([Supprime] = 0 OR Supprime IS NULL) ORDER BY [NomOption]">
          <SelectParameters>
            <asp:ControlParameter ControlID="cboLogiciel" Name="IdLogiciel" PropertyName="SelectedValue"
              Type="Int32" />
          </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <br />
      </td>
    </tr>
    <tr>
      <td style="width: 4px">
      </td>
      <td valign="top" style="width: 400px">
        <br />
        <asp:Label ID="Label9" runat="server" Text="Nombre de licences à <b>autoriser</b>"></asp:Label><br />
        <kale:UnsignedIntegerTextBox ID="txtNbLicence" runat="server" Width="130px"></kale:UnsignedIntegerTextBox><br />
        <kale:UnsignedIntegerTextBoxValidator ID="valLicence" runat="server" ControlToValidate="txtNbLicence"
          ErrorMessage="Vous devez entrer un <b>nombre de licences</b>" SetFocusOnError="True"
          Display="Dynamic" CultureInvariantValues="True"></kale:UnsignedIntegerTextBoxValidator><br /><br />
        <asp:Label ID="Label18" runat="server" Text="Nombre de licences à <b>facturer</b>"></asp:Label><br />
        <kale:UnsignedIntegerTextBox ID="txtNbLicenceFacturer" runat="server" Width="130px"></kale:UnsignedIntegerTextBox><br />
        <kale:UnsignedIntegerTextBoxValidator ID="valLicenceFacturer" runat="server"
          ControlToValidate="txtNbLicenceFacturer" CultureInvariantValues="True" Display="Dynamic"
          ErrorMessage="Vous devez entrer un <b>nombre de licences</b>" SetFocusOnError="True"></kale:UnsignedIntegerTextBoxValidator><br />
        <br />
        <asp:CheckBox ID="chkAbandonne" runat="server" Text="Licence abandonnée" />
        &nbsp;&nbsp;
        <br />
        <asp:CheckBox ID="chkUNCPath" runat="server" Text="Utiliser le chemin UNC absolu" Font-Bold="False" Font-Underline="True" ForeColor="Red" />&nbsp;<br />
        (cette case doit être cochée sinon elle autorise le
        <br />
        déplacement du serveur où est stockée la licence)<br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Montant facturé"></asp:Label><br />
        <kale:NumericTextBox ID="txtPrixVente" runat="server" Width="130px"></kale:NumericTextBox><br />
        <kale:NumericTextBoxValidator ID="valPrixVente" runat="server" ControlToValidate="txtPrixVente"
          ErrorMessage="Vous devez entrer un <b>montant facturé</b>." SetFocusOnError="True"
          Display="Dynamic" CultureInvariantValues="True"></kale:NumericTextBoxValidator><br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Montant maintenance à facturer"></asp:Label><br />
        <kale:NumericTextBox ID="txtPrixMiseAJour" runat="server" Width="130px"></kale:NumericTextBox><br />
        <kale:NumericTextBoxValidator ID="valPrixMiseAJour" runat="server" ControlToValidate="txtPrixMiseAJour"
          ErrorMessage="Vous devez entrer un <b>montant maintenance à facturer</b>." SetFocusOnError="True"
          Display="Dynamic" CultureInvariantValues="True"></kale:NumericTextBoxValidator></td>
      <td valign="top">
        &nbsp;
        <br />
        <asp:Label ID="Label10" runat="server" Text="Date d'expiration"></asp:Label>
        <br />
        <asp:TextBox ID="txtDateExpiration" runat="server" OnTextChanged="txtDateExpiration_TextChanged"
          Width="130px" AutoPostBack="True"></asp:TextBox>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC"
          BorderWidth="1px" CaptionAlign="Left" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399"
          Height="200px" Width="221px" CellPadding="1" DayNameFormat="Shortest" OnSelectionChanged="Calendar1_SelectionChanged">
          <SelectedDayStyle BackColor="#009999" ForeColor="#CCFF99" Font-Bold="True" />
          <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
          <OtherMonthDayStyle ForeColor="#999999" />
          <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
          <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
          <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
            Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
          <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
          <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:Calendar>
      </td>
    </tr>
    <tr>
      <td style="width: 4px">
      </td>
      <td valign="top" style="width: 400px">
        <br />
        <asp:Label ID="Label11" runat="server" Text="Contact technique"></asp:Label><br />
        <asp:DropDownList ID="cboContactTechnique" runat="server" Width="335px">
        </asp:DropDownList><br />
        <asp:RequiredFieldValidator ID="valContactTechnique" runat="server" ControlToValidate="cboContactTechnique"
          Display="Dynamic" ErrorMessage="Vous devez choisir un <b>contact technique</b> !"></asp:RequiredFieldValidator>
      </td>
      <td valign="top">
        <br />
        <asp:Label ID="Label12" runat="server" Text="Contact administratif"></asp:Label><br />
        <asp:DropDownList ID="cboContactAdministratif" runat="server" Width="335px">
        </asp:DropDownList><br />
        <asp:RequiredFieldValidator ID="valContactAdministratif" runat="server" ControlToValidate="cboContactAdministratif"
          Display="Dynamic" ErrorMessage="Vous devez choisir un <b>contact administratif</b> !"></asp:RequiredFieldValidator>
      </td>
    </tr>
  </table>
  <br />
  <table cellspacing="4" cellpadding="0">
    <tr>
      <td style="width: 4px">
      </td>
      <td style="border: none; width: 500px;" valign="top">
        &nbsp;<asp:Label ID="Label6" runat="server" Text="Machines :" Font-Bold="True"></asp:Label>
        <table cellpadding="0" cellspacing="4">
          <tr>
            <td valign="top">
              <asp:GridView ID="grdLicenceMachine" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdMachine" DataSourceID="dsLicenceMachine"
                ForeColor="#333333" GridLines="None" OnRowUpdating="grdLicenceMachine_RowUpdating" OnSelectedIndexChanged="grdLicenceMachine_SelectedIndexChanged" ><RowStyle BackColor="#EFF3FB" />
                <Columns>
                  <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/puce.png" ShowSelectButton="True" />
                  <asp:BoundField DataField="NomUtilisateur" HeaderText="Utilisateur" SortExpression="NomUtilisateur" />
                  <asp:BoundField DataField="NomMachine" HeaderText="Machine" SortExpression="NomMachine" />
                  <asp:BoundField DataField="IdMachine" HeaderText="IdMachine" InsertVisible="False"
                    ReadOnly="True" SortExpression="IdMachine" Visible="False" />
                  <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/icon-delete.gif" EditImageUrl="~/Images/icon-edit.gif"
                    ShowDeleteButton="True" ShowEditButton="True" CancelImageUrl="~/Images/icon-cancel.gif"
                    UpdateImageUrl="~/Images/icon-save.gif" />
                </Columns><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <EmptyDataTemplate>
                  (aucune machine)
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
              </asp:GridView>
              <strong><span style="text-decoration: underline">
                <br />
                Détail : </span></strong>&nbsp;
              <asp:GridView ID="grdLicenceMachine_Mac" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsLicenceMachine_Mac"
                ForeColor="#333333" GridLines="None" OnRowUpdating="grdLicenceMachine_RowUpdating" DataKeyNames="IdMachine" >
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                  <asp:BoundField DataField="MacAddress" HeaderText="MacAddress" SortExpression="MacAddress" />
                  <asp:BoundField DataField="RepertoireLicence" HeaderText="RepertoireLicence" SortExpression="RepertoireLicence" />
                  <asp:BoundField DataField="IdMachine" HeaderText="IdMachine" InsertVisible="False"
                    ReadOnly="True" SortExpression="IdMachine" Visible="False" />
                  <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/icon-delete.gif" ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <EmptyDataTemplate>
                  (aucun détail)
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
              </asp:GridView>
              <asp:SqlDataSource ID="dsLicenceMachine_Mac" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT [MacAddress], [RepertoireLicence], [IdMachine] FROM [LicenceMachine_MajLicence] WHERE ([IdMachine] = @IdMachine)" DeleteCommand="DELETE FROM LicenceMachine_MajLicence WHERE (IdMachine = @IdMachine)">
                <SelectParameters>
                  <asp:ControlParameter ControlID="grdLicenceMachine" Name="IdMachine" PropertyName="SelectedValue"
                    Type="Int32" />
                </SelectParameters>
                <DeleteParameters>
                  <asp:ControlParameter ControlID="grdLicenceMachine_Mac" Name="IdMachine" PropertyName="SelectedValue" />
                </DeleteParameters>
              </asp:SqlDataSource>
            </td>
            <td style="width: 4px">
            </td>
            <td>
              <strong><span style="text-decoration: underline">Nouvelle machine : </span></strong>
              <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="4"
                DataKeyNames="IdMachine" DataSourceID="dsLicenceMachine" ForeColor="#333333" GridLines="None"
                OnItemInserting="DetailsView1_ItemInserting" OnItemUpdating="DetailsView1_ItemUpdating"
                OnItemInserted="DetailsView1_ItemInserted" Visible="False" OnModeChanged="DetailsView1_ModeChanged">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                <EditRowStyle BackColor="#2461BF" />
                <RowStyle BackColor="#EFF3FB" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <Fields>
                  <asp:BoundField DataField="NomUtilisateur" HeaderText="Utilisateur" SortExpression="NomUtilisateur" />
                  <asp:BoundField DataField="NomMachine" HeaderText="Machine" SortExpression="NomMachine" />
                  <asp:CommandField ButtonType="Button" CancelImageUrl="~/Images/icon-cancel.gif" DeleteImageUrl="~/Images/icon-delete.gif"
                    EditImageUrl="~/Images/icon-edit.gif" NewImageUrl="~/Images/icon-edit.gif" ShowInsertButton="True"
                    UpdateImageUrl="~/Images/icon-save.gif" CancelText="Annuler" EditText="Editer"
                    InsertText="Ajouter" NewText="Ajouter" UpdateText="OK" />
                </Fields>
                <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                <EmptyDataTemplate>
                  &nbsp;
                </EmptyDataTemplate>
              </asp:DetailsView>
              <asp:SqlDataSource ID="dsLicenceMachine" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                DeleteCommand="DELETE FROM [LicenceMachine] WHERE [IdMachine] = @IdMachine" InsertCommand="INSERT INTO [LicenceMachine] ([NomUtilisateur], [NomMachine], IdLIcence) VALUES (@NomUtilisateur, @NomMachine, @IdLicence)"
                SelectCommand="SELECT [NomUtilisateur], [NomMachine], [IdMachine] FROM [LicenceMachine] WHERE ([IdLicence] = @IdLicence) ORDER BY [NomUtilisateur]"
                UpdateCommand="UPDATE [LicenceMachine] SET [NomUtilisateur] = @NomUtilisateur, [NomMachine] = @NomMachine WHERE [IdMachine] = @IdMachine">
                <DeleteParameters>
                  <asp:Parameter Name="IdMachine" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                  <asp:Parameter Name="NomUtilisateur" Type="String" />
                  <asp:Parameter Name="NomMachine" Type="String" />
                  <asp:Parameter Name="IdMachine" Type="Int32" />
                </UpdateParameters>
                <SelectParameters>
                  <asp:QueryStringParameter Name="IdLicence" QueryStringField="IdLicence" Type="Int32" />
                </SelectParameters>
                <InsertParameters>
                  <asp:Parameter Name="NomUtilisateur" Type="String" />
                  <asp:Parameter Name="NomMachine" Type="String" />
                  <asp:QueryStringParameter Name="IdLicence" QueryStringField="IdLicence" />
                </InsertParameters>
              </asp:SqlDataSource>
              <br />
              <asp:Button ID="btnAddMachine" runat="server" OnClick="btnAddMachine_Click" Text="Ajouter" /><br />
            </td>
          </tr>
        </table>
        <asp:Label ID="lblErreur" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
        &nbsp;</td>
      <td style="border: solid 1px LightSteelBlue; color: #333333; border-collapse: collapse; " align="left" valign="top">
        <asp:Label ID="Label16" runat="server" Text="&nbsp;<b>Gestion des licences</b>" Width="100%" CssClass="EnteteBoite"></asp:Label><br />
        <br />
        &nbsp;Pour enregistrer les changements
        <br />
        &nbsp;sans quitter la page :<asp:Button ID="btnSaveChanges" runat="server" OnClick="btnSaveChanges_Click"
          Text="Sauvegarder" /><br />
        <br />
        &nbsp;<asp:Label ID="Label13" runat="server" Text="Modèle d'avertissement : "></asp:Label><br />
        &nbsp;<asp:DropDownList ID="cboAvertissement" runat="server" DataSourceID="dsAvertissement"
          DataTextField="Nom" DataValueField="IdModele" Width="200px">
        </asp:DropDownList>
        <asp:Button ID="btnAvertir" runat="server" Text="Avertir" OnClick="btnAvertir_Click" /><br />
        <br />
        &nbsp;<asp:Label ID="Label14" runat="server" Text="Modèle de renouvellement : "></asp:Label><br />
        &nbsp;<asp:DropDownList ID="cboRenouvellement" runat="server" DataSourceID="dsRenouvellement"
          DataTextField="Nom" DataValueField="IdModele" Width="200px">
        </asp:DropDownList>
        <asp:Button ID="btnRenew" runat="server" Text="Renouveller" OnClick="btnRenew_Click" /><br />
        <br />
        &nbsp;<asp:Label ID="Label15" runat="server" Text="Modèle de régénération : "></asp:Label><br />
        &nbsp;<asp:DropDownList ID="cboRegenereration" runat="server" DataSourceID="dsRegenereration"
          DataTextField="Nom" DataValueField="IdModele" Width="200px">
        </asp:DropDownList>
        <asp:Button ID="btnRegenerate" runat="server" Text="Regénérer" OnClick="btnRegenerate_Click" /><br />
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;<asp:Label ID="Label17" runat="server" Text="Modèle d'installation : "></asp:Label><br />
        &nbsp;<asp:DropDownList ID="cboInstallation" runat="server" DataSourceID="dsInstallation"
          DataTextField="Nom" DataValueField="IdModele" Width="200px">
        </asp:DropDownList>
        <asp:Button ID="btnInstallation" runat="server" Text="Installation" OnClick="btnInstallation_Click" /><br />
        &nbsp; &nbsp; &nbsp; &nbsp;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;<asp:CheckBox ID="chkGarderTrace" runat="server" Text="Garder trace" /><br />
        <asp:SqlDataSource ID="dsAvertissement" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT [IdModele], [Nom] FROM [ListeModelesAvertissement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsRegenereration" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT [IdModele], [Nom] FROM [ListeModelesRegeneration]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsRenouvellement" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT [Nom], [IdModele] FROM [ListeModelesRenouvellement]"></asp:SqlDataSource><asp:SqlDataSource ID="dsInstallation" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT [IdModele], [Nom] FROM [ListeModelesInstallation] ORDER BY [Nom]">
          </asp:SqlDataSource>
        &nbsp;&nbsp;
        <asp:Label ID="lblStatus" runat="server" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px"
          EnableViewState="False" ForeColor="Red" Visible="False">Status...</asp:Label></td>
    </tr>
    <tr>
      <td style="width: 4px">
      </td>
      <td style="border-right: medium none; border-top: medium none; border-left: medium none;
        width: 500px; border-bottom: medium none" valign="top">
      </td>
      <td align="left" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
        border-left: lightsteelblue 1px solid; width: 328px; color: #333333; border-bottom: lightsteelblue 1px solid;
        border-collapse: collapse" valign="top">
      </td>
    </tr>
  </table>
  &nbsp; &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Commentaires"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:TextBox ID="txtCommentaire" runat="server" Width="335px" Rows="5" TextMode="MultiLine"></asp:TextBox>
  &nbsp;&nbsp;<br />
  &nbsp;&nbsp;
  <asp:Label ID="lblDateCreation" runat="server" Text="Date de création : xxx" Font-Italic="True"
    ForeColor="LightGray"></asp:Label>
  &nbsp;&nbsp;
  <asp:Label ID="lblDateModification" runat="server" Text="Date de modification : xxx"
    Font-Italic="True" ForeColor="LightGray"></asp:Label><br />
  &nbsp;&nbsp;
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Button ID="btnSave" runat="server" Text="Enregistrer" OnClick="btnSave_Click" />
  &nbsp;&nbsp;
  <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Annuler"
    CausesValidation="False" />
  &nbsp;
  <br />
</asp:Content>
