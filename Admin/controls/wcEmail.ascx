<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcEmail.ascx.cs" Inherits="controls_wcEmail" %>
<div id="dvSalvarEmails" visible="false" runat="server">
    <table style="width: 100%;">
        <tr id="dvEmailPadrao">
            <td id="tdEmails">
                <input type="button" id="btnAdd" style="width: 200px" value="Adicionar email"
                    onclick="AddEmail()" /><br />
                <input type="hidden" id="hiddenEmailCount" name="hiddenEmailCount" value="0" />
                <div id="dvEmail" style="display: none">
                    <input type="text" id="txtEmail_x" class="Email" name="txtEmail_x"
                        style="width: 500px" />
                    <asp:DropDownList ID="ddlTiposEmail" CssClass="TiposEmail" runat="server">
                    </asp:DropDownList>
                    <input type="button" id="btnRemoveEmail" style="width: 100px" value="Remover" onclick="RemoverEmail(this)" />
                    <br />
                </div>
            </td>
        </tr>
    </table>
    <div id="dvScript" runat="server">
    </div>
    <br />
</div>
