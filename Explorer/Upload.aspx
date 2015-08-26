<%@ Page Language="c#" CodeBehind="Upload.aspx.cs" AutoEventWireup="false" Inherits="explorer.Upload" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Upload</title>
    <link href="css/style.css" type="text/css" rel="stylesheet">
    <style> .tdPadding { padding-left: 10px; } </style>
    <script type="text/javascript" src="javascript/jquery-1.4.1.js"></script>
    <script type="text/javascript">
        function checkFile() {
            if ($("#fileUp").val() == "") {
                alert("Escolha um arquivo para fazer o Upload.")
                return false
            }
            else {
                parent.uploading()
                return true
            }

            parent.focus()

        }
		
    </script>

</head>
<body bottommargin="5" bgcolor="#d6dff7" leftmargin="5" topmargin="10" rightmargin="5">
    <form id="frmUpload" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%">      
        <tr>
            <td class="tdPadding">
                <fieldset style="border-right: #0053a5 1px solid; padding-right: 5px; border-top: #0053a5 1px solid;
                    padding-left: 5px; font-size: 10px; padding-bottom: 5px; border-left: #0053a5 1px solid;
                    width: 210px; padding-top: 5px; border-bottom: #0053a5 1px solid; font-family: verdana;height: 62px;
    margin-left: -5px;">
                    <legend>Selecione um arquivo</legend>
                    <input id="fileUp" style="width: 170px; border-right: black 1px solid; border-top: black 1px solid;
                    font-size: 10px; border-left: black 1px solid; border-bottom: black 1px solid;
                    font-family: verdana" type="file" size="20" runat="server" name="fileUp">
                <asp:ImageButton ID="imgUp" runat="server" ImageUrl="imagens/lateral/save.gif" ToolTip="Fazer Upload"
                    ImageAlign="Middle"></asp:ImageButton>
                </fieldset>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
