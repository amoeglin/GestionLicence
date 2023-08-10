<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Détail Contact"
  CodeFile="~/Contact_Detail.aspx.cs" Inherits="Contact_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  &nbsp;&nbsp;
  <asp:Label ID="Label8" runat="server" Text="Client"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:DropDownList ID="cboClient" runat="server" DataSourceID="dsClient" DataTextField="NomCLient"
    DataValueField="IdClient" Width="335px" OnSelectedIndexChanged="cboClient_SelectedIndexChanged"
    AutoPostBack="True">
  </asp:DropDownList><asp:SqlDataSource ID="dsClient" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="SELECT [IdClient], [NumeroClient] + '  -  ' + [RaisonSociale] as [NomCLient] FROM [Client] &#13;&#10;WHERE Supprime=0 OR Supprime IS NULL&#13;&#10;ORDER BY [RaisonSociale]">
  </asp:SqlDataSource>
  &nbsp;&nbsp;
  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboClient"
    Display="Dynamic" ErrorMessage="Vous devez choisir un <b>client</b> !"></asp:RequiredFieldValidator>&nbsp;<br />
  <br />
  &nbsp; &nbsp;<asp:Label ID="Label1" runat="server" Text="Nom Prénom"></asp:Label><br />
  &nbsp; &nbsp;<asp:TextBox ID="txtNomPrenom" runat="server" Width="335px"></asp:TextBox>&nbsp;<br />
  &nbsp;&nbsp;
  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomPrenom"
    ErrorMessage="Vous devez entrer un <b>nom de Contact</b> !" Display="Dynamic"></asp:RequiredFieldValidator><br />
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="Label7" runat="server" Text="Type de contact"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:DropDownList ID="cboTypeContact" runat="server" DataSourceID="dsTypeContact"
    DataTextField="Libelle" DataValueField="IdTypeContact" Width="335px">
  </asp:DropDownList><br />
  <asp:SqlDataSource ID="dsTypeContact" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="SELECT [IdTypeContact], [Libelle] FROM [TypeContact] ORDER BY [Libelle]">
  </asp:SqlDataSource>
  &nbsp; &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
    ControlToValidate="cboTypeContact" Display="Dynamic" ErrorMessage="Vous devez choisir un <b>type de Contact</b> !"></asp:RequiredFieldValidator>&nbsp;<br />
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="Label2" runat="server" Text="Adresse"></asp:Label>
  <br />
  &nbsp;&nbsp;
  <asp:TextBox ID="txtAdresse" runat="server" Width="335px" Rows="3" TextMode="MultiLine"></asp:TextBox>&nbsp;<br />
  &nbsp;&nbsp;
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="Label4" runat="server" Text="Téléphone"></asp:Label>
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp;&nbsp;
  <asp:Label ID="Label5" runat="server" Text="Fax"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:TextBox ID="txtTelephone" runat="server" Width="130px"></asp:TextBox>
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:TextBox ID="txtFax" runat="server" Width="130px"></asp:TextBox><br />
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="Label6" runat="server" Text="E-mail"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:TextBox ID="txtEmail" runat="server" Width="335px"></asp:TextBox><br />
  &nbsp;&nbsp;
  <asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="txtEmail"
    Display="Dynamic" ErrorMessage="Vous devez entrer un <b>e-mail</b> !" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
  &nbsp;&nbsp;
  <asp:Label ID="Label9" runat="server" Text="Contact technique"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:DropDownList ID="cboContactTechnique" runat="server" Width="335px">
  </asp:DropDownList>&nbsp;&nbsp;<br />
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="Label3" runat="server" Text="Commentaires"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:TextBox ID="txtCommentaire" runat="server" Width="335px" Rows="5" TextMode="MultiLine"></asp:TextBox><br />
  &nbsp;&nbsp;
  <asp:Label ID="lblDateCreation" runat="server" Text="Date de création : xxx" Font-Italic="True"
    ForeColor="LightGray"></asp:Label>
  &nbsp;&nbsp;
  <asp:Label ID="lblDateModification" runat="server" Text="Date de modification : xxx"
    Font-Italic="True" ForeColor="LightGray"></asp:Label><br />
  &nbsp;&nbsp;
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" CssClass="ErrorLabel"></asp:Label><br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Button ID="btnSave" runat="server" Text="Enregistrer" OnClick="btnSave_Click" />
  &nbsp;&nbsp;
  <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Annuler"
    CausesValidation="False" /><br />
</asp:Content>
