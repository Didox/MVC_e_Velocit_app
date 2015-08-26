<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TradeVision</title>
    <link rel="stylesheet" href="css/admin.css" type="text/css" media="screen" />
</head>
<body>
    <form id="frmLoginAtimosfera" runat="server">
    <fieldset>
        <div>
            <h1>TradeVision Admin</h1>
            <div class="error" id="dvErro" runat="server"></div>
            <div class="line">
                <div>
                    Login:</div>
                <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
            </div>
            <div class="line">
                <div>
                    Senha:</div>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnEntrar" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
            </div>
        </div>
    </fieldset>
    </form>
</body>
</html>
