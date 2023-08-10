<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Détail Client" CodeFile="~/Client_Detail.aspx.cs"
  Inherits="Client_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Numero Client"></asp:Label><br />
  &nbsp; &nbsp;<asp:TextBox ID="txtNumeroClient" runat="server" Width="129px"></asp:TextBox>&nbsp;<br />
  &nbsp;&nbsp;
  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumeroClient"
    ErrorMessage="Vous devez entrer un <b>numéro de client</b> !" Display="Dynamic"></asp:RequiredFieldValidator><br />
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="Label2" runat="server" Text="Raison Sociale"></asp:Label>
  <br />
  &nbsp;&nbsp;
  <asp:TextBox ID="txtRaisonSociale" runat="server" Width="335px"></asp:TextBox>&nbsp;<br />
  &nbsp;&nbsp;
  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRaisonSociale"
    ErrorMessage="Vous devez entrer une <b>raison sociale</b> !" Display="Dynamic"></asp:RequiredFieldValidator><br />
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
  <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Button ID="btnSave" runat="server" Text="Enregistrer" OnClick="btnSave_Click" />
  &nbsp;&nbsp;
  <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Annuler"
    CausesValidation="False" /><br />
</asp:Content>
