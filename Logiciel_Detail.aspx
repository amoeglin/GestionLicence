<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Détail Logiciel" 
  CodeFile="~/Logiciel_Detail.aspx.cs" Inherits="Logiciel_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <table border="0" cellspacing="4" cellpadding="0">
    <tr>
      <td>
        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Nom complet du logiciel (nom commercial)"></asp:Label><br />
        &nbsp; &nbsp;<asp:TextBox ID="txtNomLogiciel" runat="server" Width="335px"></asp:TextBox>&nbsp;<br />
        &nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomLogiciel"
          ErrorMessage="Vous devez entrer un <b>nom de logiciel</b> !" Display="Dynamic"></asp:RequiredFieldValidator><br />
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Nom de la licence (nom interne)"></asp:Label>
        <br />
        &nbsp;&nbsp;
        <asp:TextBox ID="txtNomLicence" runat="server" Width="335px"></asp:TextBox>&nbsp;<br />
        &nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomLicence"
          ErrorMessage="Vous devez entrer un <b>nom de licence</b> !" Display="Dynamic"></asp:RequiredFieldValidator><br />
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="URL de téléchargement du programme d'installation"></asp:Label><br />
        &nbsp;&nbsp;
        <asp:TextBox ID="txtURLInstall" runat="server" Width="335px"></asp:TextBox><br /><br />
        &nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" Text="URL de téléchargement du programme de mise à jour"></asp:Label><br />
        &nbsp;&nbsp;
        <asp:TextBox ID="txtURLUpdate" runat="server" Width="335px"></asp:TextBox><br />
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" Text="URL de la page Internet listant les évolutions"></asp:Label><br />
        &nbsp;&nbsp;
        <asp:TextBox ID="txtURLChangeLog" runat="server" Width="335px"></asp:TextBox><br />
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="Label7" runat="server" Text="Répertoire d'installation par défaut"></asp:Label><br />
        &nbsp;&nbsp;
        <asp:TextBox ID="txtDefaultDir" runat="server" Width="335px"></asp:TextBox><br />
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="Label8" runat="server" Text="Répertoire par défaut où se trouve la licence"></asp:Label><br />
        &nbsp;&nbsp;
        <asp:TextBox ID="txtLicenceDir" runat="server" Width="335px"></asp:TextBox><br />
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
        <asp:Button ID="btnCancel" runat="server" Text="Annuler" CausesValidation="False"
          PostBackUrl="~/Logiciels.aspx" /><br />
      </td>
      <td>
        &nbsp;&nbsp;
      </td>
      <td>
        <span style="text-decoration: underline"><strong>Liste des options possible :</strong></span>&nbsp;
        <asp:GridView ID="grdLicenceMachine" runat="server" AllowPaging="True" AllowSorting="True"
          AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdOptionLogiciel" DataSourceID="dsOptionLogiciel"
          ForeColor="#333333" GridLines="None" OnRowUpdating="grdLicenceMachine_RowUpdating">
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <Columns>
            <asp:BoundField DataField="IdOptionLogiciel" HeaderText="IdOptionLogiciel" InsertVisible="False"
              ReadOnly="True" SortExpression="IdOptionLogiciel" Visible="False" />
            <asp:BoundField DataField="NomOption" HeaderText="Nom Option" SortExpression="NomOption" />
            <asp:BoundField DataField="NomLicence" HeaderText="Nom Licence" SortExpression="NomLicence" />
            <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
            <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/icon-cancel.gif" DeleteImageUrl="~/Images/icon-delete.gif"
              EditImageUrl="~/Images/icon-edit.gif" ShowDeleteButton="True" ShowEditButton="True"
              UpdateImageUrl="~/Images/icon-save.gif" />
          </Columns>
          <RowStyle BackColor="#EFF3FB" />
          <EmptyDataTemplate>
            (aucune machine)
          </EmptyDataTemplate>
          <EditRowStyle BackColor="#2461BF" />
          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
          <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <br />
        <span style="text-decoration: underline"><strong>Nouvelle option :</strong></span><br />
        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="4"
          DataKeyNames="IdOptionLogiciel" DataSourceID="dsOptionLogiciel" ForeColor="#333333"
          GridLines="None" OnItemInserted="DetailsView1_ItemInserted" OnItemInserting="DetailsView1_ItemInserting"
          OnItemUpdating="DetailsView1_ItemUpdating" OnModeChanged="DetailsView1_ModeChanged"
          Visible="False">
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
          <EditRowStyle BackColor="#2461BF" />
          <RowStyle BackColor="#EFF3FB" />
          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
          <EmptyDataTemplate>
            &nbsp;
          </EmptyDataTemplate>
          <Fields>
            <asp:BoundField DataField="NomOption" HeaderText="Nom Option" SortExpression="NomOption" />
            <asp:BoundField DataField="NomLicence" HeaderText="Nom Licence" SortExpression="NomLicence" />
            <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
            <asp:CommandField ButtonType="Button" CancelImageUrl="~/Images/icon-cancel.gif" CancelText="Annuler"
              DeleteImageUrl="~/Images/icon-delete.gif" EditImageUrl="~/Images/icon-edit.gif"
              EditText="Editer" InsertText="Ajouter" NewImageUrl="~/Images/icon-edit.gif" NewText="Ajouter"
              ShowInsertButton="True" UpdateImageUrl="~/Images/icon-save.gif" UpdateText="OK" />
          </Fields>
          <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
          <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <AlternatingRowStyle BackColor="White" />
        </asp:DetailsView>
        <!-- DeleteCommand="DELETE FROM OptionLogiciel WHERE [IdOptionLogiciel] = @IdOptionLogiciel" -->
        <asp:SqlDataSource ID="dsOptionLogiciel" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
          SelectCommand="SELECT IdOptionLogiciel, [NomOption], [NomLicence], [Commentaire] FROM [OptionLogiciel] WHERE (([Supprime] = 0 OR [Supprime] IS NULL) AND ([IdLogiciel] = @IdLogiciel)) ORDER BY [NomOption]"
          DeleteCommand="UPDATE OptionLogiciel SET Supprime=1 WHERE [IdOptionLogiciel] = @IdOptionLogiciel"
          InsertCommand="INSERT INTO OptionLogiciel(IdLogiciel, NomOption, NomLicence, Commentaire) VALUES (@IdLogiciel, @NomOption, @NomLicence, @Commentaire)"
          UpdateCommand="UPDATE OptionLogiciel SET [NomOption]=@NomOption, [NomLicence]=@NomLicence, [Commentaire]=@Commentaire WHERE [IdOptionLogiciel] = @IdOptionLogiciel">
          <SelectParameters>
            <asp:QueryStringParameter Name="IdLogiciel" QueryStringField="IdLogiciel" Type="Int32" />
          </SelectParameters>
          <DeleteParameters>
            <asp:Parameter Name="IdOptionLogiciel" />
          </DeleteParameters>
          <UpdateParameters>
            <asp:Parameter Name="NomOption" />
            <asp:Parameter Name="NomLicence" />
            <asp:Parameter Name="Commentaire" />
            <asp:Parameter Name="IdOptionLogiciel" />
          </UpdateParameters>
          <InsertParameters>
            <asp:QueryStringParameter Name="IdLogiciel" QueryStringField="IdLogiciel" Type="Int32" />
            <asp:Parameter Name="NomOption" />
            <asp:Parameter Name="NomLicence" />
            <asp:Parameter Name="Commentaire" />
          </InsertParameters>
        </asp:SqlDataSource>
        <asp:Button ID="btnAddOption" runat="server" OnClick="btnAddOption_Click" Text="Ajouter" />&nbsp;
        <asp:Label ID="lblErreur" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
    </tr>
  </table>
</asp:Content>
