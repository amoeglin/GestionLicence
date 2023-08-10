<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Untitled Page"
   CodeFile="~/Utilisateurs.aspx.cs" Inherits="Utilisateurs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
    <span>
        <strong><span style="text-decoration: underline"></span></strong></span>
  <ul>
    <li><span><strong><span style="text-decoration: underline">Liste des Utilisateurs :</span></strong></span></li>
  </ul>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" CellPadding="4" DataSourceID="dsUtilisateurs"
        ForeColor="#333333" GridLines="None">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:BoundField DataField="NomPrenom" HeaderText="NomPrenom" SortExpression="NomPrenom" />
            <asp:BoundField DataField="MotDePasse" HeaderText="MotDePasse" SortExpression="MotDePasse" DataFormatString="******" />
            <asp:BoundField DataField="Droits" HeaderText="Droits" SortExpression="Droits" />
            <asp:BoundField DataField="Commentaire" HeaderText="Commentaire" ReadOnly="True"
                SortExpression="Commentaire" />
            <asp:HyperLinkField DataNavigateUrlFields="IdUtilisateur" DataNavigateUrlFormatString="Utilisateur_Detail.aspx?IdUtilisateur={0}"
                Text="&lt;img alt=&quot;edit&quot; border=&quot;0&quot; src=&quot;Images/icon-edit.gif&quot; /&gt;" />
            <asp:HyperLinkField DataNavigateUrlFields="IdUtilisateur" DataNavigateUrlFormatString="Utilisateur_Delete.aspx?IdUtilisateur={0}"
                Text="&lt;img alt=&quot;delete&quot; border=&quot;0&quot; src=&quot;Images/icon-delete.gif&quot; /&gt;" />
        </Columns>
        <RowStyle BackColor="#EFF3FB" />
        <EmptyDataTemplate>
            (aucun utilisateur)
        </EmptyDataTemplate>
        <EditRowStyle BackColor="#2461BF" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Button ID="btnNouveau" runat="server" PostBackUrl="Utilisateur_Detail.aspx?IdUtilisateur=0"
        Text="Nouveau..." />
    <asp:SqlDataSource ID="dsUtilisateurs" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [NomPrenom], [MotDePasse], [Droits], [Commentaire], [IdUtilisateur] FROM [ListeUtilisateurs]">
    </asp:SqlDataSource>
</asp:Content>

