<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcEndereco.ascx.cs" Inherits="controls_wcEndereco" %>
<div id="dvSalvarEndereco" visible="false" runat="server">
    <div id="dvEnderecos">
        <input type="button" id="btnAddEndereco" style="width: 200px" value="Adicionar endereço"
            onclick="AddEndereco()" /><br />
        <input type="hidden" id="hiddenEnderecoCount" name="hiddenEnderecoCount" value="0" />
        <table id="tbEndereco" style="width: 100%; display: none">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlTipoEndereco" CssClass="TipoEndereco" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <input type="button" id="btnRemoverEndereco" style="width: 100px" value="Remover"
                        onclick="RemoverEndereco(this)" /><br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Endereço:
                </td>
                <td>
                    <asp:TextBox ID="txtEndereco" runat="server" CssClass="Endereco" Width="300px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Número:
                </td>
                <td>
                    <asp:TextBox ID="txtEnderecoNumero" CssClass="EnderecoNumero" runat="server" Width="50px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Bairro:
                </td>
                <td>
                    <asp:TextBox ID="txtBairro" runat="server" CssClass="Bairro" Width="300px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Cidade:
                </td>
                <td>
                    <asp:TextBox ID="txtCidade" runat="server" CssClass="Cidade" Width="300px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEstado" CssClass="Estado" runat="server">
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    País:
                </td>
                <td>
                    <asp:DropDownList ID="ddlPais" CssClass="Pais" runat="server">
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Cep:
                </td>
                <td>
                    <asp:TextBox ID="txtCep" CssClass="Cep" runat="server" Width="90px"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    Complemento:
                </td>
                <td>
                    <asp:TextBox ID="txtComplemento" CssClass="Complemento" runat="server" Width="300px"></asp:TextBox><br />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvScript" runat="server">
    </div>
    <br />
</div>
