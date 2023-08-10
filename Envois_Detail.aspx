<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Détail Envoi" 
  CodeFile="~/Envois_Detail.aspx.cs" Inherits="Envois_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <table cellpadding="0" cellspacing="4" width="1500px">
    <tr>
      <td valign="top" style="width: 500px; height: 291px">
        <asp:Label ID="lblEnvoi" runat="server" Text="Détail de l'envoi..."></asp:Label></td>
      <td style="text-align: center; width: 20px; height: 291px;" valign="top">
      </td>
      <td valign="top" style="width: 950px; height: 291px">
        <ul>
          <li><span style="text-decoration: underline"><strong>Indicateurs :</strong></span><span></span>
            &nbsp;
            <asp:CheckBox ID="chkGarderTrace" runat="server" Enabled="False" Text="Garder Trace" />
            &nbsp;
            <asp:CheckBox ID="chkAvertissement" runat="server" Enabled="False" Text="Avertissement" /><br />
            &nbsp;&nbsp;&nbsp; </li>
          <li><span style="text-decoration: underline"><strong>Destinataires de l'envoi :</strong></span></li></ul>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
          DataSourceID="dsDestinataireEnvoi" ForeColor="#333333" GridLines="None" DataKeyNames="IdDestinataireEnvoi">
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <Columns>
            <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/icon-edit.gif" ShowEditButton="True"
              CancelImageUrl="~/Images/icon-cancel.gif" UpdateImageUrl="~/Images/icon-save.gif" />
            <asp:CheckBoxField DataField="Traite" HeaderText="Trait&#233;" SortExpression="Traite" />
            <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
            <asp:BoundField DataField="NomPrenom" HeaderText="Nom Pr&#233;nom" SortExpression="NomPrenom"
              ReadOnly="True" />
            <asp:BoundField DataField="Libelle" HeaderText="Type" SortExpression="Libelle" ReadOnly="True" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ReadOnly="True" />
            <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone"
              ReadOnly="True" />
            <asp:BoundField DataField="Adresse" HeaderText="Adresse" SortExpression="Adresse"
              ReadOnly="True" />
            <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" ReadOnly="True" />
            <asp:BoundField DataField="IdDestiantaireEnvoi" HeaderText="IdDestiantaireEnvoi"
              InsertVisible="False" ReadOnly="True" SortExpression="IdDestiantaireEnvoi" Visible="False" />
          </Columns>
          <RowStyle BackColor="#EFF3FB" />
          <EditRowStyle BackColor="#2461BF" />
          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
          <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="dsDestinataireEnvoi" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT DestinataireEnvoi.Traite, DestinataireEnvoi.Commentaire, Contact.NomPrenom, TypeContact.Libelle, Contact.Email, Contact.Telephone, Contact.Adresse, Contact.Fax, DestinataireEnvoi.IdDestinataireEnvoi FROM DestinataireEnvoi INNER JOIN Contact ON DestinataireEnvoi.IdContact = Contact.IdContact INNER JOIN TypeContact ON Contact.IdTypeContact = TypeContact.IdTypeContact WHERE (Contact.Supprime = 0 OR Contact.Supprime IS NULL) AND (DestinataireEnvoi.IdEnvoi = @IdEnvoi) ORDER BY Contact.NomPrenom"
          UpdateCommand="UPDATE DestinataireEnvoi SET Traite = @Traite, Commentaire = @Commentaire WHERE (IdDestinataireEnvoi = @IdDestinataireEnvoi)">
          <SelectParameters>
            <asp:QueryStringParameter Name="IdEnvoi" QueryStringField="IdEnvoi" Type="Int32" />
          </SelectParameters>
          <UpdateParameters>
            <asp:Parameter Name="Traite" />
            <asp:Parameter Name="Commentaire" />
            <asp:ControlParameter Name="IdDestinataireEnvoi" ControlID="GridView1" PropertyName="SelectedValue" />
          </UpdateParameters>
        </asp:SqlDataSource>
      </td>
    </tr>
  </table>
  <br />
  <asp:Label ID="Label3" runat="server" Text="Commentaires"></asp:Label><br />
  <asp:TextBox ID="txtCommentaire" runat="server" Width="335px" Rows="5" TextMode="MultiLine"></asp:TextBox><br />
  <asp:Label ID="lblDateCreation" runat="server" Text="Date de création : xxx" Font-Italic="True"
    ForeColor="LightGray"></asp:Label>
  &nbsp;&nbsp;
  <asp:Label ID="lblDateModification" runat="server" Text="Date de modification : xxx"
    Font-Italic="True" ForeColor="LightGray"></asp:Label><br />
  &nbsp;&nbsp;&nbsp;<br />
  <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label><br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
  &nbsp; &nbsp;
  <asp:Button ID="btnSave" runat="server" Text="Enregistrer" OnClick="btnSave_Click" />
  &nbsp;&nbsp;
  <asp:Button ID="btnCancel" runat="server" Text="Annuler" CausesValidation="False"
    PostBackUrl="~/Envois.aspx" /><br />
</asp:Content>
