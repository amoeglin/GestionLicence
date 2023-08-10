<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Suppression Envois" 
  CodeFile="~/Envois_Purge.aspx.cs" Inherits="Envois_Purge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  &nbsp;&nbsp;&nbsp;<br />
  &nbsp;&nbsp; Les envois de plus de 6 mois vont être supprimés :&nbsp;<br />
  <br />
  &nbsp; &nbsp; &nbsp; &nbsp; Date des Envois &lt;
  <asp:Label ID="lblEnvoi" runat="server" Text="Envoi..."></asp:Label><br />
  &nbsp;&nbsp;
  <br />
  &nbsp;&nbsp; Les destinataires de cette envoi vont aussi être <strong>supprimés</strong>.
  <br />
  <br />
  &nbsp;&nbsp; Confirmez-vous la <strong><span style="color: red">PURGE DEFINITIVE DES
    ENVOIS DE PLUS DE 6 MOIS </span></strong>
  ?<br />
  &nbsp;&nbsp; &nbsp;<br />
  &nbsp;&nbsp;
  <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
  <br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Button ID="btnSave" runat="server" Text="Purger" OnClick="btnSave_Click" />
  &nbsp;&nbsp;
  <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Annuler"
    CausesValidation="False" /><br />
</asp:Content>
