<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcTelefone.ascx.cs" Inherits="controls_wcTelefone" %>
<div id="dvSalvarTelefones" visible="false" runat="server">
    <table style="width: 100%;">
        <tr id="dvTelefonePadrao">
            <td id="tdTelefones">
                <input type="button" id="btnAdd" style="width: 200px" value="Adicionar telefone"
                    onclick="AddTelefone()" /><br />
                <input type="hidden" id="hiddenTelefoneCount" name="hiddenTelefoneCount" value="0" />
                <div id="dvTelefone" style="display: none">
                    <input type="text" id="txtDDITelefone_x" onkeypress="return formatValue( this , '99', event )" class="DDITelefone" name="txtDDITelefone_x"
                        style="width: 20px" />
                    <input type="text" id="txtDDDTelefone_x" onkeypress="return formatValue( this , '99', event )" class="DDDTelefone" name="txtDDDTelefone_x"
                        style="width: 20px" />
                    <input type="text" id="txtNumeroTelefone_x" onkeypress="return formatValue( this , '9999-9999', event )" class="NumeroTelefone" name="txtNumeroTelefone_x"
                        style="width: 120px" />
                    <asp:DropDownList ID="ddlTiposTelefone" CssClass="TiposTelefone" runat="server">
                    </asp:DropDownList>
                    <input type="button" id="btnRemoveTel" style="width: 100px" value="Remover" onclick="RemoverTelefone(this)" />
                    <br />
                </div>
            </td>
        </tr>
    </table>
    <div id="dvScript" runat="server">
    </div>
    <br />
</div>
