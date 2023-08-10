<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Détail Utilisateur"
  CodeFile="~/Utilisateur_Detail.aspx.cs" Inherits="Utilisateur_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Nom complet de l'utilisateur"></asp:Label><br />
  &nbsp; &nbsp;<asp:TextBox ID="txtNomPrenom" runat="server" Width="335px"></asp:TextBox>&nbsp;<br />
  &nbsp;&nbsp;
  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomPrenom"
    ErrorMessage="Vous devez entrer un <b>nom d'utilisateur</b> !" Display="Dynamic"></asp:RequiredFieldValidator><br />
  <br />
  &nbsp;&nbsp;
  <asp:Label ID="Label2" runat="server" Text="Mot de passe"></asp:Label>
  <br />
  &nbsp;&nbsp;
  <asp:TextBox ID="txtMotDePasse" runat="server" Width="335px"></asp:TextBox>&nbsp;<br />
  &nbsp;&nbsp;
  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMotDePasse"
    ErrorMessage="Vous devez entrer un <b>mot de passe</b> !" Display="Dynamic"></asp:RequiredFieldValidator>
  &nbsp;&nbsp;&nbsp;<br />
  &nbsp;&nbsp;
  <asp:Label ID="Label4" runat="server" Text="Droits"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:CheckBoxList ID="lstDroits" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
    BorderWidth="1px" DataSourceID="dsListeDroits" DataTextField="Nom" DataValueField="IdDroit"
    RepeatLayout="Flow">
  </asp:CheckBoxList><asp:SqlDataSource ID="dsListeDroits" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="SELECT [IdDroit], [Nom] FROM [TypeDroit] ORDER BY [IdDroit]"></asp:SqlDataSource>
  &nbsp;<br />
  &nbsp;&nbsp;
  <asp:Label ID="Label3" runat="server" Text="Commentaires"></asp:Label><br />
  &nbsp;&nbsp;
  <asp:TextBox ID="txtCommentaire" runat="server" Width="335px" Rows="5" TextMode="MultiLine"></asp:TextBox><br />
  &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;<br />
  &nbsp;&nbsp;
  <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Button ID="btnSave" runat="server" Text="Enregistrer" OnClick="btnSave_Click" />
  &nbsp;&nbsp;
  <asp:Button ID="btnCancel" runat="server" Text="Annuler" CausesValidation="False"
    PostBackUrl="~/Utilisateurs.aspx" /><br />
</asp:Content>
