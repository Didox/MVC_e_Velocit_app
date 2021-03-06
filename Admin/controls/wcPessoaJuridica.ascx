﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcPessoaJuridica.ascx.cs"
    Inherits="controls_wcPessoaJuridica" %>
<div id="dvSalvarPessoaJuridica" runat="server">
    <asp:TextBox ID="txtId" Visible="false" runat="server"></asp:TextBox>
    <table style="width: 100% border: 0px" id="tableAddPessoas" runat="server">
        <tr>
            <td style="width: 100px">
                <div>
                    Razão social:
                    <asp:TextBox ID="txtData" Width="202px" runat="server"></asp:TextBox><asp:Button
                        ID="btnFind" runat="server" Text="Ok" OnClick="btnFind_Click" />
                </div>
                <asp:ListBox ID="listPessoasJuridicas" runat="server" Height="330px" Width="300px"
                    SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td align="center">
                <asp:Button ID="btnDelete" CssClass="btn" runat="server" Text="<<" OnClick="btnDelete_Click" />
                <asp:Button ID="btnAdd" CssClass="btn" runat="server" Text=">>" OnClick="btnAdd_Click" />
            </td>
            <td style="width: 100px">
                <asp:ListBox ID="listPessoasJuridicasAdd" runat="server" Height="330px" Width="300px"
                    SelectionMode="Multiple"></asp:ListBox>
            </td>
        </tr>
    </table>
    <div id="dvPessoaFisicaNotFound" runat="server">
    </div>
    <br />
</div>
