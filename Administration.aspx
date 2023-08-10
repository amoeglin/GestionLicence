<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Untitled Page" CodeFile="Administration.aspx.cs"
  Inherits="Administration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <ul>
    <li><span style="text-decoration: underline">Liste des clients supprimés :</span></li></ul>
  <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdClient" DataSourceID="dsClients"
    ForeColor="#333333" GridLines="None">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="IdClient" HeaderText="IdClient" InsertVisible="False"
        ReadOnly="True" SortExpression="IdClient">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="NumeroClient" HeaderText="NumeroClient" SortExpression="NumeroClient" />
      <asp:BoundField DataField="RaisonSociale" HeaderText="RaisonSociale" SortExpression="RaisonSociale" />
      <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
      <asp:HyperLinkField DataNavigateUrlFields="IdClient" DataNavigateUrlFormatString="Client_Undelete.aspx?IdClient={0}"
        Text="R&#233;cup&#233;rer..." />
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
  <asp:SqlDataSource ID="dsClients" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [IdClient], [NumeroClient], [RaisonSociale], [Commentaire] FROM [ListeClientsSupprimes]">
  </asp:SqlDataSource>
  <br />
  <br />
  <hr class="Separator" />
  <br />
  <ul>
    <li><span style="text-decoration: underline">Liste des contacts supprimés :</span></li></ul>
  <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdContact" DataSourceID="dsContacts"
    ForeColor="#333333" GridLines="None">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="Client" HeaderText="Client" SortExpression="Client" />
      <asp:BoundField DataField="IdContact" HeaderText="IdContact" ReadOnly="True" SortExpression="IdContact">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="NomPrenom" HeaderText="NomPrenom" SortExpression="NomPrenom" />
      <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
      <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
      <asp:HyperLinkField DataNavigateUrlFields="IdContact" DataNavigateUrlFormatString="Contact_Undelete.aspx?IdContact={0}"
        Text="R&#233;cup&#233;rer..." />
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
  <asp:SqlDataSource ID="dsContacts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [IdContact], [Client], [NomPrenom], [Email], [Type] FROM [ListeContactsSupprimes]">
  </asp:SqlDataSource>
  <br />
  <br />
  <hr class="Separator" />
  <br />
  <ul>
    <li><span style="text-decoration: underline">Liste des logiciels supprimés :</span></li></ul>
  <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdLogiciel" DataSourceID="dsLogiciels"
    ForeColor="#333333" GridLines="None">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="IdLogiciel" HeaderText="IdLogiciel" InsertVisible="False"
        ReadOnly="True" SortExpression="IdClient">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="NomComplet" HeaderText="Nom" SortExpression="NomComplet" />
      <asp:BoundField DataField="NomLicence" HeaderText="Licence" SortExpression="NomLicence" />
      <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
      <asp:HyperLinkField DataNavigateUrlFields="IdLogiciel" DataNavigateUrlFormatString="Logiciel_Undelete.aspx?IdLogiciel={0}"
        Text="R&#233;cup&#233;rer..." />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EmptyDataTemplate>
      (aucun logiciel)
    </EmptyDataTemplate>
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
  <asp:SqlDataSource ID="dsLogiciels" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [IdLogiciel], [NomComplet], [NomLicence], [Commentaire] FROM [ListeLogicielsSupprimes]">
  </asp:SqlDataSource>
  <br />
  <br />
  <hr class="Separator" />
  <br />
  <ul>
    <li><span style="text-decoration: underline">Liste des licences supprimées :</span></li></ul>
  <asp:GridView ID="grdLicences" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsLicences" ForeColor="#333333"
    GridLines="None">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="IdLicence" HeaderText="IdLicence" ReadOnly="True" SortExpression="IdLicence" />
      <asp:BoundField DataField="RaisonSociale" HeaderText="RaisonSociale" SortExpression="Raison Sociale" />
      <asp:BoundField DataField="Logiciel" HeaderText="Logiciel" SortExpression="Logiciel" />
      <asp:BoundField DataField="NbLicence" HeaderText="NbLicence" SortExpression="Nb Licences">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="DateExpiration" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DateExpiration"
        HtmlEncode="False" SortExpression="Date Expiration">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="NomSite" HeaderText="NomSite" SortExpression="Site" />
      <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
      <asp:CheckBoxField DataField="Abandonne" HeaderText="Abandonne" SortExpression="Abandonn&#233;e">
        <ItemStyle HorizontalAlign="Center" />
      </asp:CheckBoxField>
      <asp:HyperLinkField DataNavigateUrlFields="IdLicence" DataNavigateUrlFormatString="Licence_Undelete.aspx?IdLicence={0}"
        Text="R&#233;cup&#233;rer..." />
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
  <asp:SqlDataSource ID="dsLicences" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [IdLicence], [RaisonSociale], [Logiciel], [NbLicence], [DateExpiration], [PrixVente], [PrixMiseAJour], [NomSite], [IdClient], [Commentaire], [Abandonne], [Repertoire], [NomMachine] FROM [ListeLicencesClientSupprimes]">
  </asp:SqlDataSource>
  <br />
  <br />
  <hr class="Separator" />
  <br />
  <ul>
    <li><span style="text-decoration: underline">Liste des Options de Logiciel supprimées
      :</span></li></ul>
  <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsOptionLogiciel" ForeColor="#333333"
    GridLines="None">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="IdOptionLogiciel" HeaderText="IdOptionLogiciel" InsertVisible="False"
        ReadOnly="True" SortExpression="IdOptionLogiciel">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="NomOption" HeaderText="NomOption" SortExpression="NomOption" />
      <asp:BoundField DataField="NomLicence" HeaderText="NomLicence" SortExpression="NomLicence" />
      <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" SortExpression="Commentaire" />
      <asp:HyperLinkField DataNavigateUrlFields="IdOptionLogiciel" DataNavigateUrlFormatString="OptionLogiciel_Undelete.aspx?IdOptionLogiciel={0}"
        Text="R&#233;cup&#233;rer..." />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EmptyDataTemplate>
      (aucune Option de Logiciel)
    </EmptyDataTemplate>
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
  <asp:SqlDataSource ID="dsOptionLogiciel" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [NomOption], [NomLicence], [Commentaire], [IdOptionLogiciel] FROM [OptionLogiciel] WHERE ([Supprime] = @Supprime) ORDER BY [NomOption]">
    <SelectParameters>
      <asp:Parameter DefaultValue="True" Name="Supprime" Type="Boolean" />
    </SelectParameters>
  </asp:SqlDataSource>
  <br />
  <br />
  <hr class="Separator" />
  <br />
  <ul>
    <li><span style="text-decoration: underline">Statistiques :</span></li><ul>
      <li>- Total hits:
        <% Response.Write(Application["Hits"].ToString()); %>
      </li>
      <li>- Total sessions:
        <% Response.Write(Application["Sessions"].ToString()); %>
      </li>
      <li>- Expired sessions:
        <% Response.Write(Application["TerminatedSessions"].ToString()); %>
      </li>
    </ul>
  </ul>
</asp:Content>
