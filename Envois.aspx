<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Untitled Page" 
  CodeFile="~/Envois.aspx.cs" Inherits="Envois" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <strong><span><span style="color: lightsteelblue"></span><span style="text-decoration: underline">
  </span></span></strong>
  <ul>
    <li><strong><span><span style="text-decoration: underline">Liste des Modèles de document:</span></span></strong></li></ul>
  <asp:GridView ID="grdModele" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdModele" DataSourceID="dsModeles"
    ForeColor="#333333" GridLines="None">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
      <asp:BoundField DataField="Nom" HeaderText="Nom" SortExpression="Nom" />
      <asp:HyperLinkField DataNavigateUrlFields="IdModele" DataNavigateUrlFormatString="Modele_Detail.aspx?IdModele={0}"
        Text="&lt;img alt=&quot;edit&quot; border=&quot;0&quot; src=&quot;Images/icon-edit.gif&quot; /&gt;" />
      <asp:HyperLinkField DataNavigateUrlFields="IdModele" DataNavigateUrlFormatString="Modele_Delete.aspx?IdModele={0}"
        Text="&lt;img alt=&quot;delete&quot; border=&quot;0&quot; src=&quot;Images/icon-delete.gif&quot; /&gt;" />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EmptyDataTemplate>
      (aucun modèle)
    </EmptyDataTemplate>
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
  <asp:Button ID="btnNouveauModele" runat="server" PostBackUrl="Modele_Detail.aspx?IdModele=0"
    Text="Nouveau..." /><br />
  <asp:SqlDataSource ID="dsModeles" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [IdModele], [Nom], [Type] FROM [ListeModeles]">
  </asp:SqlDataSource>
  <br />
  <hr color="lightsteelblue" />
  <br />
  <span><strong><span style="color: #b0c4de"></span><span style="text-decoration: underline">
  </span></strong></span>
  <ul>
    <li><span><strong><span style="text-decoration: underline">Liste des Envois :</span></strong></span>
      &nbsp;
      <asp:CheckBox ID="chkEnvoiNonTraite" runat="server" Font-Bold="False" OnCheckedChanged="chkEnvoiNonTraite_CheckedChanged"
        Text="Envois non-traités" AutoPostBack="True" />
      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
      &nbsp; &nbsp; &nbsp; &nbsp;
      <asp:Button ID="btnPurgerEnvois" runat="server" PostBackUrl="~/Envois_Purge.aspx"
        Text="Purger les envois de +6 mois" /></li></ul>
  <asp:GridView ID="grdEnvois" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdEnvoi" DataSourceID="dsEnvois"
    ForeColor="#333333" GridLines="None">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
      <asp:BoundField DataField="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="Date"
        HtmlEncode="False" SortExpression="Date">
        <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>
      <asp:BoundField DataField="Titre" HeaderText="Titre" SortExpression="Titre" />
      <asp:BoundField DataField="Raison_Sociale" HeaderText="Raison Sociale" SortExpression="Raison_Sociale" />
      <asp:BoundField DataField="NomComplet" HeaderText="Logiciel" SortExpression="NomComplet" />
      <asp:CheckBoxField DataField="Avertissement" HeaderText="Avertissement" SortExpression="Avertissement">
        <ItemStyle HorizontalAlign="Center" />
      </asp:CheckBoxField>
      <asp:CheckBoxField DataField="Trace" HeaderText="Trace" SortExpression="Trace">
        <ItemStyle HorizontalAlign="Center" />
      </asp:CheckBoxField>
      <asp:HyperLinkField DataNavigateUrlFields="IdEnvoi" DataNavigateUrlFormatString="Envois_Detail.aspx?IdEnvoi={0}"
        Text="&lt;img alt=&quot;edit&quot; border=&quot;0&quot; src=&quot;Images/icon-edit.gif&quot; /&gt;" />
      <asp:HyperLinkField DataNavigateUrlFields="IdEnvoi" DataNavigateUrlFormatString="Envois_Delete.aspx?IdEnvoi={0}"
        Text="&lt;img alt=&quot;delete&quot; border=&quot;0&quot; src=&quot;Images/icon-delete.gif&quot; /&gt;" />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EmptyDataTemplate>
      (aucun envoi)
    </EmptyDataTemplate>
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
  <asp:SqlDataSource ID="dsEnvois" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    SelectCommand="SELECT IdEnvoi, Date, [Raison Sociale] AS Raison_Sociale, Trace, Avertissement, Titre, NomComplet FROM ListeEnvois ORDER BY Date DESC">
  </asp:SqlDataSource>
</asp:Content>
