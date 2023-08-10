<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Untitled Page" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Gestion des licences logiciels";
    }
  
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
    Bonjour, choisissez un action dans le menu !
</asp:Content>

