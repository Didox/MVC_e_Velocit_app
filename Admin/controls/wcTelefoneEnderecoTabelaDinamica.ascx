<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcTelefoneEnderecoTabelaDinamica.ascx.cs" Inherits="controls_wcTelefoneEnderecoTabelaDinamica" %>
<script type="text/javascript" src="js/cadastro.js"></script>
<div id="dvSalvarTelefones" visible="false" runat="server">
    <fieldset>
        <legend>Telefones</legend>
        <table style="width: 100%;">
            <tr id="dvTelefonePadrao">
                <td id="tdTelefones">
                    <input type="button" id="btnAdd" style="width: 200px" value="Adicionar telefone"
                        onclick="AddTelefone()" /><br />
                    <input type="hidden" id="hiddenTelefoneCount" name="hiddenTelefoneCount" value="0" />
                    <div id="dvTelefone" style="display: none">
                        <input type="text" id="txtDDITelefone_x" class="DDITelefone" name="txtDDITelefone_x"
                            style="width: 20px" />
                        <input type="text" id="txtDDDTelefone_x" class="DDDTelefone" name="txtDDDTelefone_x"
                            style="width: 20px" />
                        <input type="text" id="txtNumeroTelefone_x" class="NumeroTelefone" name="txtNumeroTelefone_x"
                            style="width: 120px" />
                        <asp:DropDownList ID="ddlTiposTelefone" CssClass="TiposTelefone" runat="server">
                        </asp:DropDownList>
                        <input type="button" id="btnRemoveTel" style="width: 100px" value="Remover" onclick="RemoverTelefone(this)" />
                        <br />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
</div>
<div id="dvSalvarEndereco" visible="false" runat="server">
    <fieldset>
        <legend>Endereço</legend>
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
                        <asp:TextBox ID="txtCep" CssClass="Cep" runat="server" Width="80px"></asp:TextBox><br />
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
    </fieldset>
    <br />
</div>
<div style="width: 400px; float: left" id="dvTabelaDinamica" runat="server" visible="false">
    <fieldset>
        <legend>Pessoa fisica dinâmica</legend>
        <div style="width: 100%;" id="dvTabela" runat="server">
        </div>
        <br />
        <br />
        <asp:LinkButton ID="btnEditarTabelaFisica" runat="server" On Click="btnEditarTabelaFisica_Click">Configurar Tabela</asp:LinkButton>
    </fieldset>
    <br />
</div>