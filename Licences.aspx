<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Untitled Page" 
  CodeFile="~/Licences.aspx.cs" Inherits="Licences" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server"><asp:Image ID="Image3" runat="server" AlternateText="puce" ImageAlign="Bottom" ImageUrl="~/Images/puce.png" />
  <strong><span style="text-decoration: underline">Afficher la liste des licences :</span></strong>&nbsp;
    <asp:Button ID="btnExpiringLicences" runat="server" OnClick="btnExpiringLicences_Click"
      Text="Arrivant à expiration..." CausesValidation="False" />&nbsp;
    <asp:Button ID="btnLicencesLogiciel" runat="server" Text="Par logiciel..."
      OnClick="btnLicencesLogiciel_Click" CausesValidation="False" />&nbsp;
  <asp:Button ID="btnListNbLicence" runat="server" Text="Nombre..."
      OnClick="btnListNbLicence_Click" CausesValidation="False" />
  <br />
  <br />
  
  <asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
      <asp:Image ID="Image1" runat="server" AlternateText="puce" ImageAlign="Bottom" ImageUrl="~/Images/puce.png" />
      Licences arrivant à expiration dans moins de &nbsp;<asp:DropDownList ID="cboNbJour"
        runat="server" Width="78px" OnSelectedIndexChanged="cboNbJour_SelectedIndexChanged"
        AutoPostBack="True">
        <asp:ListItem Value="15">15 jours</asp:ListItem>
        <asp:ListItem Value="30">1 mois</asp:ListItem>
        <asp:ListItem Selected="True" Value="45">45 jours</asp:ListItem>
        <asp:ListItem Value="60">2 mois</asp:ListItem>
        <asp:ListItem Value="65500">Toutes</asp:ListItem>
      </asp:DropDownList>
      <asp:Button ID="btnListExpired" runat="server" Text="Afficher" OnClick="btnListExpired_Click" /><br />
      <br />
      &nbsp;<asp:Label ID="lblLegend" runat="server" Font-Bold="True" Font-Underline="False"
        Text="&nbsp;Légende...&nbsp;" BackColor="LightSteelBlue" ForeColor="White"></asp:Label><br />
      <asp:Repeater ID="Repeater1" runat="server" DataSourceID="dsLicences">
        <HeaderTemplate>
          <table cellspacing="0" cellpadding="4">
            <tr>
              <td style="border: solid 1px LightSteelBlue; color: #333333; border-collapse: collapse;">
                <table cellspacing="0" cellpadding="4" border="0" style="color: #333333; border-collapse: collapse;">
                  <tr style="color: White; background-color: #507CD1; font-weight: bold;">
                    <th>
                      <%--Id--%>
                    </th>
                    <th>
                      Client</th>
                    <th>
                      Logiciel</th>
                    <th>
                      Nb licences</th>
                    <th>
                      Date d'expiration</th>
                    <th>
                      Avertir le client</th>
                    <th>
                      Renouveler</th>
                    <th>
                      Regénérer</th>
                  </tr>
        </HeaderTemplate>
        <ItemTemplate>
          <tr style="color: #333333; background-color: #EFF3FB;">
            <td align="center">
              <asp:Label runat="server" ID="lblId" ForeColor="#EFF3FB" Text='<%# Eval("IdLicence") %>' />
            </td>
            <td>
              <asp:Label runat="server" ID="lblRaisonSocial" Text='<%# Eval("RaisonSociale") %>' />
            </td>
            <td>
              <asp:Label runat="server" ID="lblLogiciel" Text='<%# Eval("Logiciel") %>' />
            </td>
            <td align="center">
              <asp:Label runat="server" ID="lblNbLicence" Text='<%# Eval("NbLicence") %>' />
            </td>
            <td align="center">
              <asp:Label runat="server" ID="lblDateExpiration" Text='<%# Eval("DateExpiration", "{0:dd/MM/yyyy}") %>' />
            </td>
            <td align="center">
              <asp:CheckBox ID="chkAvertir" runat="server" />
            </td>
            <td align="center">
              <asp:CheckBox ID="chkRenouveller" runat="server" />
            </td>
            <td align="center">
              <asp:CheckBox ID="chkRegenerer" runat="server" />
            </td>
          </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
          <tr style="color: #333333; background-color: white;">
            <td align="center">
              <asp:Label runat="server" ID="lblId" ForeColor="white" Text='<%# Eval("IdLicence") %>' />
            </td>
            <td>
              <asp:Label runat="server" ID="lblRaisonSocial" Text='<%# Eval("RaisonSociale") %>' />
            </td>
            <td>
              <asp:Label runat="server" ID="lblLogiciel" Text='<%# Eval("Logiciel") %>' />
            </td>
            <td align="center">
              <asp:Label runat="server" ID="lblNbLicence" Text='<%# Eval("NbLicence") %>' />
            </td>
            <td align="center">
              <asp:Label runat="server" ID="lblDateExpiration" Text='<%# Eval("DateExpiration", "{0:dd/MM/yyyy}") %>' />
            </td>
            <td align="center">
              <asp:CheckBox ID="chkAvertir" runat="server" />
            </td>
            <td align="center">
              <asp:CheckBox ID="chkRenouveller" runat="server" />
            </td>
            <td align="center">
              <asp:CheckBox ID="chkRegenerer" runat="server" />
            </td>
          </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
          <tr style="color: #333333; background-color: white;">
            <td align="center">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td align="center">
            </td>
            <td align="center">
            </td>
            <td align="center">
              <asp:Button ID="btnRenewAllAvertir" runat="server" Text="Tout" OnClick="btnRenewAllAvertir_Click" />
            </td>
            <td align="center">
              <asp:Button ID="btnRenewAll" runat="server" Text="Tout" OnClick="btnRenewAll_Click" />
            </td>
            </td>
          </tr>
          </table> </td> </tr> </table>
        </FooterTemplate>
      </asp:Repeater>
      <br />
      <table cellpadding="0" cellspacing="4">
        <tr>
          <td style="width: 670px" valign="top">
            &nbsp;<asp:Label ID="Label4" runat="server" Text="Sélectionner les modèles de documents à utiliser :"
              Font-Bold="True" Font-Underline="False"></asp:Label>
            <table cellpadding="0" cellspacing="4">
              <tr>
                <td>
                  <asp:Label ID="Label1" runat="server" Text="Modèle pour les avertissements : "></asp:Label>
                </td>
                <td style="color: gray">
                  <asp:DropDownList ID="cboAvertissement" runat="server" DataSourceID="dsAvertissement"
                    DataTextField="Nom" DataValueField="IdModele" Width="300px">
                  </asp:DropDownList>*
                </td>
                <td>
                  <asp:ImageButton ID="btnPreviewAvertissement" runat="server" ImageAlign="AbsMiddle"
                    ImageUrl="~/Images/icon-preview.gif" OnClick="btnPreviewAvertissement_Click" />&nbsp;
                  </td>
              </tr>
              <tr>
                <td>
                  <asp:Label ID="Label2" runat="server" Text="Modèle pour les renouvellements : "></asp:Label>
                </td>
                <td style="color: gray">
                  <asp:DropDownList ID="cboRenouvellement" runat="server" DataSourceID="dsRenouvellement"
                    DataTextField="Nom" DataValueField="IdModele" Width="300px">
                  </asp:DropDownList>*
                </td>
                <td>
                  <asp:ImageButton ID="btnPreviewRenouvellement" runat="server" ImageAlign="AbsMiddle"
                    ImageUrl="~/Images/icon-preview.gif" OnClick="btnPreviewRenouvellement_Click" />
                  &nbsp; &nbsp;&nbsp;
                  <asp:Button ID="btnTratement" runat="server" Text="Traiter" OnClick="btnTratement_Click" />
                </td>
              </tr>
              <tr>
                <td>
                  <asp:Label ID="Label3" runat="server" Text="Modèle pour les régénérations : "></asp:Label>
                </td>
                <td style="color: gray">
                  <asp:DropDownList ID="cboRegenereration" runat="server" DataSourceID="dsRegenereration"
                    DataTextField="Nom" DataValueField="IdModele" Width="300px">
                  </asp:DropDownList>*
                </td>
                <td>
                  <asp:ImageButton ID="btnPreviewRegeneration" runat="server" ImageAlign="AbsMiddle"
                    ImageUrl="~/Images/icon-preview.gif" OnClick="btnPreviewRegeneration_Click" />
                </td>
              </tr>
            </table>
            <em style="color: gray">&nbsp;* Pour éditer les modèles, utiliser le menu "Envois".
              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                  <asp:CheckBox ID="chkGarderTrace" runat="server" Text="Garder trace" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" /><br />
              &nbsp;&nbsp; Pour utiliser des modèles différents, executer l'action en plusieurs
              fois.</em></td>
          <td style="width: 10px">
          </td>
          <td style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
            border-left: lightsteelblue 1px solid; border-bottom: lightsteelblue 1px solid; width: 90px;"
            valign="top">
            <asp:Label ID="lblPreviewTitle" runat="server" Width="100%" Text="&nbsp;Modèle...&nbsp;"
              BackColor="LightSteelBlue" Font-Bold="True" ForeColor="White"></asp:Label><br />
            <table cellpadding="0" cellspacing="4">
              <tr>
                <td>
                  <asp:Label ID="lblPreview" runat="server" Text=""></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <br />
      <asp:Label ID="ErrorMessage" runat="server" EnableViewState="False" ForeColor="Red"
        BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">Statut...</asp:Label>&nbsp;
      <br />
      <br />
      <asp:SqlDataSource ID="dsLicences" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT [IdLicence], [RaisonSociale], [Logiciel], [PrixMiseAJour], [PrixVente], [DateExpiration], [NbLicence], [Commentaire], [Abandonne], [Repertoire], [NomMachine], [NomSite], [IdClient] FROM [ListeLicences] WHERE (Abandonne IS NULL OR Abandonne = 0) AND ([DateExpiration] <= DATEADD(day, CAST(@DateExpiration AS Int), GetDate()))" CacheDuration="1" CacheExpirationPolicy="Sliding">
        <SelectParameters>
          <asp:ControlParameter ControlID="cboNbJour" Name="DateExpiration" PropertyName="SelectedValue"
            Type="String" />
        </SelectParameters>
      </asp:SqlDataSource>
      <asp:SqlDataSource ID="dsAvertissement" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT [IdModele], [Nom] FROM [ListeModelesAvertissement]"></asp:SqlDataSource>
      <asp:SqlDataSource ID="dsRegenereration" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT [IdModele], [Nom] FROM [ListeModelesRegeneration]"></asp:SqlDataSource>
      <asp:SqlDataSource ID="dsRenouvellement" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT [Nom], [IdModele] FROM [ListeModelesRenouvellement]"></asp:SqlDataSource>
    </asp:View>
    <asp:View ID="View2" runat="server">
      <asp:Image ID="Image2" runat="server" AlternateText="puce" ImageAlign="Bottom" ImageUrl="~/Images/puce.png" />
      Licences pour le logiciel &nbsp;<asp:DropDownList ID="cboLogiciel" runat="server"
        Width="284px" AutoPostBack="True" DataSourceID="dsLogiciel" DataTextField="NomComplet"
        DataValueField="IdLogiciel" OnSelectedIndexChanged="cboLogiciel_SelectedIndexChanged">
      </asp:DropDownList>
      <asp:Button ID="btnListeLogiciels" runat="server" Text="Afficher" OnClick="btnListeLogiciels_Click" /><br />
      <br />
      &nbsp;<asp:Label ID="lblLegendeLogiciel" runat="server" BackColor="LightSteelBlue"
        Font-Bold="True" Font-Underline="False" ForeColor="White" Text="&nbsp;Légende...&nbsp;"></asp:Label><br />
      <asp:Repeater ID="rptLicenceLogiciel" runat="server" DataSourceID="dsLicenceUpd">
        <HeaderTemplate>
          <table cellspacing="0" cellpadding="4">
            <tr>
              <td style="border: solid 1px LightSteelBlue; color: #333333; border-collapse: collapse;">
                <table cellspacing="0" cellpadding="4" border="0" style="color: #333333; border-collapse: collapse;">
                  <tr style="color: White; background-color: #507CD1; font-weight: bold;">
                    <th>
                      <%--Id--%>
                    </th>
                    <th>
                      Client</th>
                    <th>
                      Nb licences</th>
                    <th>
                      Date d'expiration</th>
                    <th>
                      Avertir le client</th>
                  </tr>
        </HeaderTemplate>
        <ItemTemplate>
          <tr style="color: #333333; background-color: #EFF3FB;">
            <td align="center">
              <asp:Label runat="server" ID="lblId" ForeColor="#EFF3FB" Text='<%# Eval("IdLicence") %>' />
            </td>
            <td>
              <asp:Label runat="server" ID="lblRaisonSocial" Text='<%# Eval("RaisonSociale") %>' />
            </td>
            <td align="center">
              <asp:Label runat="server" ID="lblNbLicence" Text='<%# Eval("NbLicence") %>' />
            </td>
            <td align="center">
              <asp:Label runat="server" ID="lblDateExpiration" Text='<%# Eval("DateExpiration", "{0:dd/MM/yyyy}") %>' />
            </td>
            <td align="center">
              <asp:CheckBox ID="chkAvertir" runat="server" />
            </td>
          </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
          <tr style="color: #333333; background-color: white;">
            <td align="center">
              <asp:Label runat="server" ID="lblId" ForeColor="white" Text='<%# Eval("IdLicence") %>' />
            </td>
            <td>
              <asp:Label runat="server" ID="lblRaisonSocial" Text='<%# Eval("RaisonSociale") %>' />
            </td>
            <td align="center">
              <asp:Label runat="server" ID="lblNbLicence" Text='<%# Eval("NbLicence") %>' />
            </td>
            <td align="center">
              <asp:Label runat="server" ID="lblDateExpiration" Text='<%# Eval("DateExpiration", "{0:dd/MM/yyyy}") %>' />
            </td>
            <td align="center">
              <asp:CheckBox ID="chkAvertir" runat="server" />
            </td>
          </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
          <tr style="color: #333333; background-color: white;">
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td align="center">
              <asp:Button ID="Button1" runat="server" Text="Tout" OnClick="btnUpdAllAvertir_Click" />
            </td>
          </tr>
          </table> </td> </tr> </table>
        </FooterTemplate>
      </asp:Repeater>
      <br />
      <table cellpadding="0" cellspacing="4">
        <tr>
          <td style="width: 663px" valign="top">
            &nbsp;<asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Underline="False"
              Text="Sélectionner les modèles de documents à utiliser :"></asp:Label>
            <table cellpadding="0" cellspacing="4">
              <tr>
                <td>
                  <asp:Label ID="Label7" runat="server" Text="Modèle pour les mises à jour : "></asp:Label>
                </td>
                <td style="color: gray">
                  <asp:DropDownList ID="cboMiseAJour" runat="server" DataSourceID="dsMiseAjour" DataTextField="Nom"
                    DataValueField="IdModele" Width="300px">
                  </asp:DropDownList>*
                </td>
                <td>
                  <asp:ImageButton ID="btnPreviewMiseAJour" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/icon-preview.gif"
                    OnClick="btnPreviewMiseAJour_Click" />
                  &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                  <asp:Button ID="btnTraitementUpd" runat="server" Text="Traiter" OnClick="btnTraitementUpd_Click" /></td>
              </tr>
            </table>
            <em style="color: gray">&nbsp;* Pour éditer les modèles, utiliser le menu "Envois".
              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
              <asp:CheckBox ID="chkGarderTraceUpd" runat="server" Text="Garder trace" Font-Italic="False" ForeColor="Black" /><br />
              &nbsp;&nbsp; Pour utiliser des modèles différents, executer l'action en plusieurs
              fois.</em></td>
          <td style="width: 10px">
          </td>
          <td style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
            border-left: lightsteelblue 1px solid; border-bottom: lightsteelblue 1px solid;"
            valign="top">
            <asp:Label ID="lblPreviewTitleUpd" runat="server" BackColor="LightSteelBlue" Font-Bold="True"
              ForeColor="White" Text="&nbsp;Modèle...&nbsp;" Width="100%"></asp:Label><br />
            <table cellpadding="0" cellspacing="4">
              <tr>
                <td>
                  <asp:Label ID="lblPreviewUpd" runat="server"></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <br />
      <asp:Label ID="ErrorMessageUpd" runat="server" BorderColor="Red" BorderStyle="Solid"
        BorderWidth="1px" EnableViewState="False" ForeColor="Red">Statut...</asp:Label>&nbsp;
      <br />
      <br />
      <asp:SqlDataSource ID="dsLicenceUpd" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT IdLicence, RaisonSociale, Logiciel, PrixMiseAJour, PrixVente, DateExpiration, NbLicence, Commentaire, Abandonne, Repertoire, NomMachine, NomSite, IdClient, IdLogiciel FROM ListeLicences WHERE (Abandonne IS NULL OR Abandonne = 0) AND (IdLogiciel = CAST(@IdLogiciel AS Int))">
        <SelectParameters>
          <asp:ControlParameter ControlID="cboLogiciel" Name="IdLogiciel" PropertyName="SelectedValue" />
        </SelectParameters>
      </asp:SqlDataSource>
      <asp:SqlDataSource ID="dsMiseAjour" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT [IdModele], [Nom] FROM [ListeModelesMiseajour]"></asp:SqlDataSource>
      <asp:SqlDataSource ID="dsLogiciel" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="SELECT [IdLogiciel], [NomComplet] FROM [ListeLogiciels] ORDER BY [NomComplet]">
      </asp:SqlDataSource>
      &nbsp;
    </asp:View>
    <asp:View ID="View3" runat="server">
      <br />
      <asp:SqlDataSource ID="dsNbLicences" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Logiciel.NomComplet AS Nom, SUM(Licence.NbLicenceFacture) AS [Nb Licences], SUM(Licence.PrixMiseAJour) AS [Mise à jour] FROM Licence INNER JOIN Logiciel ON Licence.IdLogiciel = Logiciel.IdLogiciel WHERE (Licence.Abandonne = 0 OR Licence.Abandonne IS NULL) AND (Licence.Supprime = 0 OR Licence.Supprime IS NULL) GROUP BY Logiciel.NomComplet ORDER BY Nom">
      </asp:SqlDataSource>
      <asp:GridView ID="grdNbLicences" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataSourceID="dsNbLicences" ForeColor="#333333" GridLines="None">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><Columns>
          <asp:BoundField DataField="Nom" HeaderText="Nom" SortExpression="Nom" />
          <asp:BoundField DataField="Nb Licences" HeaderText="Nb Licences" ReadOnly="True"
            SortExpression="Nb Licences">
            <ItemStyle HorizontalAlign="Center" />
          </asp:BoundField>
          <asp:BoundField DataField="Mise &#224; jour" DataFormatString="{0:# ### ##0.00 €}"
            HeaderText="Mise &#224; jour" ReadOnly="True" SortExpression="Mise &#224; jour" HtmlEncode="False">
            <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
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
      <br />
      <asp:SqlDataSource ID="dsTotalNbLicences" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT SUM(Licence.NbLicenceFacture) AS [TOTAL Nb Licences], SUM(Licence.PrixMiseAJour) AS [TOTAL Mise à jour] FROM Licence INNER JOIN Logiciel ON Licence.IdLogiciel = Logiciel.IdLogiciel WHERE (Licence.Abandonne = 0 OR Licence.Abandonne IS NULL) AND (Licence.Supprime = 0 OR Licence.Supprime IS NULL)">
      </asp:SqlDataSource>
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        CellPadding="4" DataSourceID="dsTotalNbLicences" ForeColor="#333333" GridLines="None">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
          <asp:BoundField DataField="TOTAL Nb Licences" DataFormatString="{0:# ### ##0}" HeaderText="TOTAL Nb Licences"
            HtmlEncode="False" ReadOnly="True" SortExpression="TOTAL Nb Licences">
            <ItemStyle HorizontalAlign="Center" />
          </asp:BoundField>
          <asp:BoundField DataField="TOTAL Mise &#224; jour" DataFormatString="{0:# ### ##0.00 €}"
            HeaderText="TOTAL Mise &#224; jour" HtmlEncode="False" ReadOnly="True" SortExpression="TOTAL Mise &#224; jour">
            <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
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
      &nbsp;</asp:View>
    (No Active View)</asp:MultiView><br />
  &nbsp;
</asp:Content>
