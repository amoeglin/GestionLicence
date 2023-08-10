<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Untitled Page" CodeFile="~/Logiciels.aspx.cs"
  Inherits="Logiciels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <span><strong><span style="text-decoration: underline"></span></strong></span>
  <ul>
    <li><span><strong><span style="text-decoration: underline">Liste des Logiciels :</span></strong></span></li>
  </ul>
  <strong></strong>
  <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdLogiciel" DataSourceID="dsLogiciels"
    ForeColor="#333333" GridLines="None">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="NomComplet" HeaderText="Nom" SortExpression="NomComplet" />
      <asp:BoundField DataField="NomLicence" HeaderText="Licence" SortExpression="NomLicence" />
      <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" ReadOnly="True"
        SortExpression="Commentaire" />
      <asp:HyperLinkField DataNavigateUrlFields="IdLogiciel" DataNavigateUrlFormatString="Logiciel_Detail.aspx?IdLogiciel={0}"
        Text="&lt;img alt=&quot;edit&quot; border=&quot;0&quot; src=&quot;Images/icon-edit.gif&quot; /&gt;" />
      <asp:HyperLinkField DataNavigateUrlFields="IdLogiciel" DataNavigateUrlFormatString="Logiciel_Delete.aspx?IdLogiciel={0}"
        Text="&lt;img alt=&quot;delete&quot; border=&quot;0&quot; src=&quot;Images/icon-delete.gif&quot; /&gt;" />
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
  &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  &nbsp;
  <asp:Button ID="btnNouveau" runat="server" Text="Nouveau..." PostBackUrl="Logiciel_Detail.aspx?IdLogiciel=0" />
  <asp:SqlDataSource ID="dsLogiciels" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [NomComplet], [NomLicence], [Commentaire], [IdLogiciel] FROM [ListeLogiciels] ORDER BY [NomComplet]">
  </asp:SqlDataSource>
</asp:Content>
