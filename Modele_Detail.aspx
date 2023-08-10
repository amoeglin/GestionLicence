<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Détail Modele" CodeFile="~/Modele_Detail.aspx.cs"
  Inherits="Modele_Detail" %>

<%@ Register Assembly="WebHtmlEdit" Namespace="WebHtmlEdit" TagPrefix="htmlEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <table cellpadding="0" cellspacing="4">
    <tr>
      <td style="width: 10px">
      </td>
      <td style="width: 384px" valign="top">
        <asp:Label ID="Label1" runat="server" Text="Nom du Modèle"></asp:Label><br />
        <asp:TextBox ID="txtNom" runat="server" Width="335px"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNom"
          ErrorMessage="Vous devez entrer un <b>nom de Modèle</b> !" Display="Dynamic"></asp:RequiredFieldValidator><br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Titre du message"></asp:Label><asp:TextBox
          ID="txtTitre" runat="server" MaxLength="50" Width="335px"></asp:TextBox><asp:RequiredFieldValidator
            ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitre" Display="Dynamic"
            ErrorMessage="Vous devez entrer un <b>titre de Message</b> !"></asp:RequiredFieldValidator></td>
      <td valign="top">
        <asp:Label ID="Label7" runat="server" Text="Type de Modèle"></asp:Label><br />
        <asp:DropDownList ID="cboTypeModele" runat="server" DataSourceID="dsTypeModele" DataTextField="Nom"
          DataValueField="IdTypeModele" Width="215px">
        </asp:DropDownList><br />
        <asp:RequiredFieldValidator ID="valTypeModele" runat="server" ControlToValidate="cboTypeModele"
          Display="Dynamic" ErrorMessage="Vous devez choisir un <b>type de Modèle</b> !"></asp:RequiredFieldValidator><br />
        <asp:SqlDataSource ID="dsTypeModele" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT [IdTypeModele], [Nom] FROM [TypeModele] ORDER BY [Nom]"></asp:SqlDataSource>
        <br />
        <strong><span style="text-decoration: underline">Champs pour le titre:<br />
        </span></strong> #LOGICIEL#</td>
    </tr>
  </table>
  <br />
  <table cellpadding="0" cellspacing="4">
    <tr>
      <td style="width: 10px">
      </td>
      <td style="width: 600px">
        <htmlEdit:HtmlEdit ID="txtContenu" runat="server" ColorTheme="Blue" EnableTheming="True" />
      </td>
      <td>
        <strong><span style="text-decoration: underline">Champs pour le contenu :</span></strong><br />
        <br />
        #CHANGELOG#<br />
        #DATEEXPIRATION#<br />
        #DEFAULTDIR#<br />
        #LICENCEDIR#<br />
        #LOGICIEL#<br />
        #MACHINE#<br />
        #NBLICENCE#
        <br />
        #OPTIONS#<br />
        #PRIXMAINTENANCE#
        <br />
        #PRIXTOTAL#
        <br />
        #REPERTOIRE#<br />
        #URLINSTALL#<br />
        #URLUPDATE#</td>
    </tr>
  </table>
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
  <asp:Button ID="btnCancel" runat="server" Text="Annuler" CausesValidation="False"
    PostBackUrl="~/Envois.aspx" /><br />
</asp:Content>
