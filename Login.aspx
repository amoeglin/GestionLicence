<%@ Page Language="C#" CodeFile="Login.aspx.cs" Inherits="Login_Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Connexion...</title>
  <link rel="Stylesheet" type="text/css" href="Style/StyleSheet.css" />
</head>
<body style="font-size: 10pt; font-family: Verdana">
  <form id="form1" runat="server">
    <table style="font-family: Verdana">
      <tr>
        <td style="width: 215px">
          <img alt="logo" border="0" src="Images/logocmmedium.gif" width="200" /></td>
        <td style="width: 409px">
          <strong><span style="font-size: 16pt; color: #000073">Actuaries Services<br />
            </span></strong><span style="font-size: 7pt">&nbsp;</span><span style="color: #000073">Gestion des licences clients</span>
            <asp:Label ID="lblDebug" runat="server" ForeColor="Red"></asp:Label></td>
      </tr>
    </table>
    <table border="0" style="width: 100%; font-family: Verdana">
      <tr>
        <td>
          <hr color="lightsteelblue" />
        </td>
      </tr>
    </table>
    <table border="0" style="width: 100%; font-family: Verdana">
      <tr>
        <td style="width: 10%; height: 80%">
        </td>
        <td style="width: 100%; height: 80%">
          <br />
          <br />
          <table cellspacing="0" cellpadding="4" border="0" style="font-size: 10pt">
            <tr>
              <td>
                <table cellspacing="0" cellpadding="4" border="1" style="background-color: #EFF3FB;
                  border-color: #B5C7DE; border-width: 1px; border-style: Solid; border-collapse: collapse;
                  font-size: 10pt;">
                  <tr>
                    <td>
                      <table cellpadding="0" border="0">
                        <tr>
                          <td align="center" colspan="2" style="color: White; background-color: #507CD1; font-size: 0.9em;
                            font-weight: bold;">
                            Connexion requise</td>
                        </tr>
                        <tr>
                          <td>
                            Utilisateur</td>
                          <td>
                            <asp:TextBox ID="UserEmail" runat="server" Font-Names="Verdana" Font-Size="10pt" />
                          </td>
                        </tr>
                        <tr>
                          <td>
                            Mot de passe &nbsp;
                          </td>
                          <td>
                            <asp:TextBox ID="UserPass" TextMode="Password" runat="server" Font-Names="Verdana"
                              Font-Size="10pt" />
                          </td>
                        </tr>
                        <tr>
                          <td>
                          </td>
                          <td>
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="Submit1" OnClick="Logon_Click" Text="Connexion" runat="server" Font-Names="Verdana"
                              Font-Size="10pt" /></td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
                <asp:Label ID="Msg" runat="server" CssClass="ErrorLabel" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserEmail"
                  ErrorMessage="Vous devez entrez un nom d'<b>utilisateur</b>." runat="server" Font-Names="Verdana"
                  Font-Size="10pt" /><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="UserPass"
                  ErrorMessage="Vous devez entrez un <b>mot de passe</b>." runat="server" Font-Names="Verdana"
                  Font-Size="10pt" />
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
