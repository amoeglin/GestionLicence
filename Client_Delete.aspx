<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Suppression Client"
  CodeFile="Client_Delete.aspx.cs" Inherits="Client_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  &nbsp;&nbsp;&nbsp;<br />
  &nbsp;&nbsp; Le client suivant va être supprimé :<br />
  <br />
  &nbsp; &nbsp; &nbsp; &nbsp;
  <asp:Label ID="lblClient" runat="server" Text="Client..."></asp:Label><br />
  &nbsp;&nbsp;
  <br />
  &nbsp;&nbsp; Les contact, licences et envois associés vont aussi <strong></strong>
  être <strong>supprimés</strong>.
  <br />
  <br />
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
