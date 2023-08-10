<%@ Page Language="C#" MasterPageFile="~/MainMenu.master" Title="Untitled Page" CodeFile="ChangePassword.aspx.cs"
  Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
  <table border="0" style="width: 100%; font-family: Verdana">
    <tr>
      <td style="width: 10%; height: 80%">
      </td>
      <td style="width: 100%; height: 80%">
        <br />
        <br />
        <table border="0" cellpadding="4" cellspacing="0">
          <tr>
            <td>
              <table border="1" cellpadding="4" cellspacing="0" style="border-right: #b5c7de 1px solid;
                border-top: #b5c7de 1px solid; font-size: 10pt; border-left: #b5c7de 1px solid;
                border-bottom: #b5c7de 1px solid; border-collapse: collapse; background-color: #eff3fb">
                <tr>
                  <td>
                    <table border="0" cellpadding="0">
                      <tr>
                        <td align="center" colspan="2" style="font-weight: bold; font-size: 0.9em; color: white;
                          background-color: #507cd1">
                          Changer de mot de passe</td>
                      </tr>
                      <tr>
                        <td>
                          Ancien</td>
                        <td>
                          <asp:TextBox ID="txtOldPassword" runat="server" Font-Names="Verdana" Font-Size="10pt"
                            TextMode="Password"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Nouveau&nbsp;</td>
                        <td>
                          <asp:TextBox ID="txtNewPassword" runat="server" Font-Names="Verdana" Font-Size="10pt"
                            TextMode="Password"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Confirmer</td>
                        <td>
                          <asp:TextBox ID="txtConfirmPassword" runat="server" Font-Names="Verdana" Font-Size="10pt"
                            TextMode="Password"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td>
                        </td>
                        <td>
                          &nbsp; &nbsp; &nbsp;&nbsp;
                          <asp:Button ID="Submit1" runat="server" Font-Names="Verdana" Font-Size="10pt" OnClick="Logon_Click"
                            Text="Changer" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
              <asp:Label ID="Msg" runat="server" Font-Names="Verdana" Font-Size="10pt" ForeColor="Red"></asp:Label>
              <br />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPassword"
                ErrorMessage="Vous devez entrez <b>l'ancien mot de passe</b>." Font-Names="Verdana"
                Font-Size="10pt"></asp:RequiredFieldValidator><br />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOldPassword"
                ErrorMessage="Vous devez entrez un <b> nouveau mot de passe</b>." Font-Names="Verdana"
                Font-Size="10pt"></asp:RequiredFieldValidator><br />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword"
                ErrorMessage="Vous devez répetez le <b>nouveau mot de passe</b>." Font-Names="Verdana"
                Font-Size="10pt"></asp:RequiredFieldValidator>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</asp:Content>
