<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMenu.master"
  CodeFile="Modele_Preview.aspx.cs" Inherits="Model_Preview" %>

<%@ Register Assembly="WebHtmlEdit" Namespace="WebHtmlEdit" TagPrefix="htmlEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <div style="font-size: 10pt; font-family: Verdana">
    <table cellpadding="0" cellspacing="4">
      <tr>
        <td>
          <img src="Images/puce_fleche1_vert.gif" />
        </td>
        <td>
          Voici le texte de l'e-mail qui va être envoyé au client.<br />
          Veuillez effectuer les changements voulus puis cliquez sur <strong>[Envoyer]</strong>.
        </td>
      </tr>
      <tr>
        <td style="width: 10px; height: 16px;">
        </td>
      </tr>
      <tr>
        <td style="width: 10px">
        </td>
        <td>
          <span> </span>
                <asp:Label ID="lblPreview" runat="server" Text="Texte du message..."></asp:Label></td>
        <td align="center">
          <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
          <br />
          &nbsp;<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Envoyer" />&nbsp;<asp:Button
            ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click" Text="Annuler" /></td>
      </tr>
    </table>
  </div>
  <br />
  <table cellpadding="0" cellspacing="4">
    <tr>
      <td style="width: 10px">
      </td>
      <td style="width: 600px">
        <htmlEdit:HtmlEdit ID="txtContenu" runat="server" ColorTheme="Blue" EnableTheming="True">
        </htmlEdit:HtmlEdit>
      </td>
      <td style="width: 244px" align=center>
        <strong><span style="text-decoration: underline">Champs pour le contenu :<br />
        </span></strong>
        <br />
        <span style="color: red">ATTENTION:<br />
          N'utilisez aucune syntaxe #...#,<br />
          Les champs ne sont plus convertis !</span></td>
    </tr>
  </table>
</asp:Content>
