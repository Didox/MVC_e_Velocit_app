<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Credenciais.aspx.cs"
    Inherits="_Credenciais" ValidateRequest="false" Title="Credenciais" %>

<%@ Register Src="controls/wcTelefone.ascx" TagName="wcTelefone" TagPrefix="uc1" %>
<%@ Register Src="controls/wcEndereco.ascx" TagName="wcEndereco" TagPrefix="uc2" %>
<%@ Register Src="controls/wcTabelaDinamica.ascx" TagName="wcTabelaDinamica" TagPrefix="uc3" %>
<%@ Register Src="controls/wcCargos.ascx" TagName="wcCargos" TagPrefix="uc4" %>
<%@ Register Src="controls/wcHierarquias.ascx" TagName="wcHierarquias" TagPrefix="uc5" %>
<%@ Register Src="controls/wcPessoaJuridica.ascx" TagName="wcPessoaJuridica" TagPrefix="uc6" %>
<%@ Register Src="controls/wcCampanhas.ascx" TagName="wcCampanhas" TagPrefix="uc7" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/form_changed.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Credencial Usuarios</h1>
    <div id="dvSalvarUsuario" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar a listagem" OnClick="btnVoltar_Click" /><br /><br />            
            <div class="demo">
                <div id="tabs">
                    <ul>
                        <li><a href="#tabs-Credencial" id="aCredencial">Credencial</a></li>
                        <li><a href="#tabs-PessoaFisica" id="aPessoaFisica">Pessoa Fisica</a></li>
                        <li><a href="#tabs-Telefones" id="aTelefones">Telefones</a></li>
                        <li><a href="#tabs-Enderecos" id="aEnderecos">Endereços</a></li>
                        <li><a href="#tabs-DadosAdicionais" id="aDadosAdicionais">Dados Adicionais</a></li>
                        <li><a href="#tabs-Cargos" id="aCargos">Cargos</a></li>
                        <li><a href="#tabs-Hierarquias" id="aHierarquias">Hierarquias</a></li>
                        <li><a href="#tabs-PessoasJuridicas" id="aPessoasJuridicas">Pessoas Juridicas</a></li>
                        <li><a href="#tabs-Campanhas" id="aCampanhas">Campanhas</a></li>
                    </ul>
                    <div id="tabs-Credencial">
                        <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 100px">
                                    Nome:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomeCompleto" runat="server" Width="423px"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Login:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLogin" runat="server" Width="100px"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Senha:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSenha" TextMode="Password" runat="server" Width="100px"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    TVI:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTVI" Enabled="false" runat="server" Width="200px"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Email:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" Width="423px"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Ramal:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRamal" CssClass="integer" runat="server" Width="70px"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Ativo:
                                </td>
                                <td>
                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdoAtivo" runat="server">
                                        <asp:ListItem Text="Sim" Value="true" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Não" Value="false"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnSalvarCredencial" runat="server" Text="Salvar" OnClick="btnSalvarCredencial_Click" />
                    </div>
                    <div id="tabs-PessoaFisica">
                        <div id="dvSalvarPessoaFisica" visible="false" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 100px">
                                        CPF:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCpf" CssClass="cpf" runat="server" Width="150px"></asp:TextBox><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        RG:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRg" CssClass="rg" runat="server" Width="130px"></asp:TextBox><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Data Nascimento:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataNascimento" CssClass="data" runat="server" Width="110px"></asp:TextBox><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Sexo:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoSexo" runat="server">
                                            <asp:ListItem Text="Masculino" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Feminino" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Estado civil:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstadoCivil" runat="server">
                                            <asp:ListItem Text="Solteiro" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Casado" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Viuvo" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Separado" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Button ID="btnSalvarPessoaFisica" runat="server" Text="Salvar" OnClick="btnSalvarPessoaFisica_Click" />
                        </div>
                    </div>
                    <div id="tabs-Telefones">
                        <uc1:wcTelefone ID="wcTelefone" runat="server" />
                        <asp:Button ID="btnSalvarTelefones" runat="server" Text="Salvar" OnClick="btnSalvarTelefones_Click" />
                    </div>
                    <div id="tabs-Enderecos">
                        <uc2:wcEndereco ID="wcEndereco" runat="server" />
                        <asp:Button ID="btnSalvarEnderecos" runat="server" Text="Salvar" OnClick="btnSalvarEnderecos_Click" />
                    </div>
                    <div id="tabs-DadosAdicionais" style="float: left;">
                        <uc3:wcTabelaDinamica ID="wcTabelaDinamica" runat="server" />     
                        <div style="clear:both"></div>
                        <asp:Button ID="btnSalvarTabelaDinamica" runat="server" Text="Salvar" OnClick="btnSalvarTabelaDinamica_Click" />                   
                    </div>
                    <div id="tabs-Cargos" style="float: left;">   
                        <uc4:wcCargos ID="wcCargos" runat="server" />              
                    </div>
                    <div id="tabs-Hierarquias" style="float: left;">    
                        <uc5:wcHierarquias ID="wcHierarquias" runat="server" />                           
                    </div>
                    <div id="tabs-PessoasJuridicas" style="float: left;">                 
                    <uc6:wcPessoaJuridica ID="wcPessoaJuridica" runat="server" />
                    </div>
                    <div id="tabs-Campanhas" style="float: left;">       
                    <uc7:wcCampanhas ID="wcCampanhas" runat="server" />          
                    </div>
                </div>
            </div>

            <script type="text/javascript">
                $("#tabs").tabs();
            </script>

            <div style="clear: both">
            </div><br />
        </fieldset>
    </div>
    <div id="dvListarUsuarios" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <p>
                <div>
                    Nome:
                </div>
                <asp:TextBox ID="txtNomeCompletoBusca" runat="server" Width="400px"></asp:TextBox>
                <asp:Button runat="server" ID="btnFiltar" Text="Filtrar" OnClick="btnFiltar_Click" />
            </p>
            <asp:GridView ID="gvUsuarios" runat="server" Width="465px" AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado." OnRowCommand="gvUsuarios_RowCommand"
                AllowPaging="true" PageSize="18" OnPageIndexChanging="gvUsuarios_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Nome") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDUsuario") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>