﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcCargos.ascx.cs" Inherits="controls_wcCargos" %>
<div id="dvSalvarCargos" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:TreeView ID="trvPessoas" runat="server" AutoGenerateDataBindings="False" ShowLines="True" >
                </asp:TreeView>
            </td>
        </tr>
    </table>
    <DIV  id="dvTreeview" runat="server"></DIV>
    <br />
</div>
