<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Suppression Licence" 
  CodeFile="~/Licence_Delete.aspx.cs" Inherits="Licence_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
    &nbsp;&nbsp;&nbsp;<br />
    &nbsp;&nbsp; Le Licence suivant va être supprimé :<br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Label ID="lblLicence" runat="server" Text="Licence..."></asp:Label><br />
    &nbsp;&nbsp;
    <br />
    &nbsp;&nbsp; Les envois associés vont aussi <strong></strong>
    être <strong>supprimés</strong>.
    <br />
    <br />
    &nbsp;&nbsp; Confirmez-vous la <strong><span style="color: red">suppression</span></strong>
    ?<br />
    &nbsp;&nbsp;
    &nbsp;<br />
    &nbsp;&nbsp;
    <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Button ID="btnSave" runat="server" Text="Supprimer" OnClick="btnSave_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Annuler" CausesValidation="False" /><br />
</asp:Content>

