<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Suppression Logiciel"
  CodeFile="~/Logiciel_Undelete.aspx.cs" Inherits="Logiciel_Undelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  &nbsp;&nbsp;&nbsp;<br />
  &nbsp;&nbsp; Le Logiciel suivant va �tre r�cup�r�e :<br />
  <br />
  &nbsp; &nbsp; &nbsp; &nbsp;
  <asp:Label ID="lblLogiciel" runat="server" Text="Logiciel..."></asp:Label><br />
  &nbsp;&nbsp;
  <br />
  &nbsp;&nbsp; Les licences associ�es vont aussi �tre <strong>r�cup�r�es</strong>.
  <br />
  <br />
  &nbsp;&nbsp; Confirmez-vous la <strong><span style="color: red">r�cup�ration</span></strong>
  ?<br />
  &nbsp;&nbsp; &nbsp;<br />
  &nbsp;&nbsp;
  <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
  <br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Button ID="btnSave" runat="server" Text="R�cup�rer" OnClick="btnSave_Click" />
  &nbsp;&nbsp;
  <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Annuler"
    CausesValidation="False" /><br />
</asp:Content>
