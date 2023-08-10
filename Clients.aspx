<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Untitled Page" Culture="fr-FR"
  CodeFile="~/Clients.aspx.cs" Inherits="Clients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <ul>
    <li><span style="text-decoration: underline">Liste des Clients :</span> &nbsp; &nbsp;
      &nbsp; &nbsp; &nbsp;
      <asp:TextBox ID="txtFiltreClient" runat="server" Width="68px" MaxLength="10"></asp:TextBox>
      <asp:Button ID="btnFiltreClient" runat="server" OnClick="btnFiltreClient_Click" Text="Filtrer" />
      <asp:Button ID="btnAucunFiltreClient" runat="server" OnClick="btnAucunFiltreClient_Click" Text="Aucun" />
      <asp:Button ID="cmdFill" runat="server" OnClick="cmdFill_Click" Text="Avec Contacts" Visible="true" />
      <asp:Button ID="cmdSansClient" runat="server"  Text="Sans Contacts" Visible="true" OnClick="cmdSansClient_Click" />

      </li></ul>
  <asp:SqlDataSource ID="dsClients" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="SELECT [IdClient], [NumeroClient], [RaisonSociale], [Commentaire] FROM [ListeClients]"
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"></asp:SqlDataSource>

    <%-- DataSourceID="dsClients" --%>
  <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4"  ForeColor="#333333" GridLines="None" DataKeyNames="IdCLient"     
    AllowPaging="True">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:CommandField ButtonType="Image" SelectText="&gt;" ShowSelectButton="True" SelectImageUrl="~/Images/puce.png" />
      <asp:BoundField DataField="NumeroClient" HeaderText="Numero Client" SortExpression="NumeroClient" />
      <asp:BoundField DataField="RaisonSociale" HeaderText="Raison Sociale" SortExpression="RaisonSociale" />
      <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
        <asp:TemplateField HeaderText="Contacts">


            <ItemTemplate>
                <asp:DropDownList ID="lstContacts" runat="server">
                </asp:DropDownList>
            </ItemTemplate>


        </asp:TemplateField>
      <asp:HyperLinkField DataNavigateUrlFields="IdClient" DataNavigateUrlFormatString="Client_Detail.aspx?IdClient={0}"
        Text="&lt;img alt=&quot;edit&quot; border=&quot;0&quot; src=&quot;Images/icon-edit.gif&quot; /&gt;" />
      <asp:HyperLinkField DataNavigateUrlFields="IdClient" DataNavigateUrlFormatString="Client_Delete.aspx?IdClient={0}"
        Text="&lt;img alt=&quot;delete&quot; border=&quot;0&quot; src=&quot;Images/icon-delete.gif&quot; /&gt;" />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EmptyDataTemplate>
      (aucun client)
    </EmptyDataTemplate>
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
  &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp;
  <asp:Button ID="btnNouveauClient" runat="server" OnClick="btnNouveau_Click" Text="Nouveau..."
    PostBackUrl="Client_Detail.aspx?IdClient=0" /><br />
  <br />
  <span><span style="text-decoration: underline"><strong></strong></span></span>
  <ul>
    <li><span><span style="text-decoration: underline"><strong>Liste des contacts du client
      sélectionné :</strong></span></span> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtFindContact" runat="server"
        MaxLength="10" Width="70px"></asp:TextBox>
      <asp:Button ID="btnFindContact" runat="server" Text="Filtrer" OnClick="btnFindContact_Click" />
      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtSeekContact"
        runat="server" Width="70px"></asp:TextBox>
      <asp:Button ID="btnSeekContact" runat="server" OnClick="btnSeekContact_Click" Text="Rechercher" />
      <asp:Label ID="lblSeekError" runat="server" ForeColor="Red"></asp:Label></li></ul>
  <asp:SqlDataSource ID="dsContact" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="SELECT IdContact, Libelle, NomPrenom, Adresse, Email, Telephone, Fax, [Contact Technique], Commentaire FROM ListeContactsClients WHERE IdClient=@IdClient"
    ConflictDetection="CompareAllValues" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
      <asp:ControlParameter ControlID="GridView1" Name="IdClient" PropertyName="SelectedValue" />
    </SelectParameters>
  </asp:SqlDataSource>
  <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataSourceID="dsContact" ForeColor="#333333" GridLines="None" DataKeyNames="IdContact"
    AllowPaging="True">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="Libelle" HeaderText="Type" SortExpression="Libelle" />
      <asp:BoundField DataField="NomPrenom" HeaderText="Nom Prenom" SortExpression="NomPrenom" />
      <asp:BoundField DataField="Adresse" HeaderText="Adresse" SortExpression="Adresse" />
      <asp:BoundField DataField="Email" HeaderText="E-mail" SortExpression="Email" />
      <asp:BoundField DataField="Telephone" HeaderText="T&#233;l&#233;phone" SortExpression="Telephone" />
      <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
      <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
      <asp:BoundField DataField="Contact Technique" HeaderText="Contact Technique" SortExpression="Contact Technique" />
      <asp:HyperLinkField DataNavigateUrlFields="IdContact" DataNavigateUrlFormatString="Contact_Detail.aspx?IdContact={0}"
        Text="&lt;img alt=&quot;edit&quot; border=&quot;0&quot; src=&quot;Images/icon-edit.gif&quot; /&gt;" />
      <asp:HyperLinkField DataNavigateUrlFields="IdContact" DataNavigateUrlFormatString="Contact_Delete.aspx?IdContact={0}"
        Text="&lt;img alt=&quot;delete&quot; border=&quot;0&quot; src=&quot;Images/icon-delete.gif&quot; /&gt;" />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EmptyDataTemplate>
      (aucun contact)
    </EmptyDataTemplate>
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp;<asp:Button ID="btnNouveauContact" runat="server" OnClick="btnNouveauContact_Click"
    Text="Nouveau..." /><br />
  <br />
  <span><strong><span style="text-decoration: underline"></span></strong></span>
  <ul>
    <li><span><strong><span style="text-decoration: underline">Liste des licences du client
      sélectionné :</span></strong></span> </li>
  </ul>
  <asp:SqlDataSource ID="dsLicences" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="SELECT [IdLicence], [Logiciel], [NbLicence], [NomMachine], [PrixMiseAJour], [PrixVente], [DateExpiration], [NomSite], [Commentaire], [Abandonne], [Repertoire], [IdClient] FROM [ListeLicencesClient] WHERE ([IdClient] = @IdClient)"
    OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
      <asp:ControlParameter ControlID="GridView1" Name="IdClient" PropertyName="SelectedValue"
        Type="Int32" />
    </SelectParameters>
  </asp:SqlDataSource>
  <asp:GridView ID="grdLicences" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="4" DataSourceID="dsLicences" ForeColor="#333333" GridLines="None"
    AllowPaging="True">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="Logiciel" HeaderText="Logiciel" SortExpression="Logiciel" />
      <asp:BoundField DataField="NbLicence" HeaderText="Nb licences" SortExpression="NbLicence">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="DateExpiration" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date expiration"
        HtmlEncode="False" SortExpression="DateExpiration">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="PrixVente" DataFormatString="{0:N2}" HeaderText="Prix vente"
        HtmlEncode="False" SortExpression="PrixVente">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="PrixMiseAJour" DataFormatString="{0:N2}" HeaderText="Prix mise &#224; jour"
        HtmlEncode="False" SortExpression="PrixMiseAJour">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:CheckBoxField DataField="Abandonne" HeaderText="Abandonn&#233;e" SortExpression="Abandonne">
        <ItemStyle HorizontalAlign="Center" />
      </asp:CheckBoxField>
      <asp:HyperLinkField DataNavigateUrlFields="IdLicence" DataNavigateUrlFormatString="Licence_Detail.aspx?IdLicence={0}"
        Text="&lt;img alt=&quot;edit&quot; border=&quot;0&quot; src=&quot;Images/icon-edit.gif&quot; /&gt;" />
      <asp:HyperLinkField DataNavigateUrlFields="IdLicence" DataNavigateUrlFormatString="Licence_Delete.aspx?IdLicence={0}"
        Text="&lt;img alt=&quot;delete&quot; border=&quot;0&quot; src=&quot;Images/icon-delete.gif&quot; /&gt;" />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EmptyDataTemplate>
      (aucune licence)
    </EmptyDataTemplate>
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Button ID="btnNewLicence" runat="server" OnClick="btnNewLicence_Click" Text="Nouvelle..." /><br />
  <br />
</asp:Content>
