<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="R�cup�ration Licence"
  CodeFile="~/Licence_Undelete.aspx.cs" Inherits="Licence_Undelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
    &nbsp;&nbsp;&nbsp;<br />
    &nbsp;&nbsp; Le Licence suivant va �tre r�cup�r� :<br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Label ID="lblLicence" runat="server" Text="Licence..."></asp:Label><br />
    &nbsp;&nbsp;
    <br />
    &nbsp;&nbsp; Les envois associ�s vont aussi �tre <strong>r�cup�r�s</strong>.
    <br />
    <br />
    &nbsp;&nbsp; Confirmez-vous la <strong><span style="color: red">r�cup�ration</span></strong>
    ?<br />
    &nbsp;&nbsp;
    &nbsp;<br />
    &nbsp;&nbsp;
    <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Button ID="btnSave" runat="server" Text="R�cup�rer" OnClick="btnSave_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Annuler" CausesValidation="False" /><br />
</asp:Content>

