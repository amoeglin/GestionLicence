<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Suppression Utilisateur"
  CodeFile="~/Utilisateur_Delete.aspx.cs" Inherits="Utilisateur_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  &nbsp;&nbsp;&nbsp;<br />
  &nbsp;&nbsp; L'utilisateur suivant va �tre supprim� :<br />
  <br />
  &nbsp; &nbsp; &nbsp; &nbsp;
  <asp:Label ID="lblUtilisateur" runat="server" Text="Utilisateur..."></asp:Label><br />
  &nbsp;&nbsp;&nbsp;<br />
  &nbsp;&nbsp; Confirmez-vous la <strong><span style="color: red">suppression</span></strong>
  ?<br />
  &nbsp;&nbsp; &nbsp;<br />
  &nbsp;&nbsp;
  <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
  <br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Button ID="btnSave" runat="server" Text="Supprimer" OnClick="btnSave_Click" />
  &nbsp;&nbsp;
  <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Annuler"
    CausesValidation="False" /><br />
</asp:Content>
