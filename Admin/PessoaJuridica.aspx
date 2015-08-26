<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="PessoaJuridica.aspx.cs"
    Inherits="_PessoaJuridica" ValidateRequest="false" Title="PessoaJuridica" %>

<%@ Register Src="controls/wcTelefone.ascx" TagName="wcTelefone" TagPrefix="uc1" %>
<%@ Register Src="controls/wcEmail.ascx" TagName="wcEmail" TagPrefix="uc4" %>
<%@ Register Src="controls/wcEndereco.ascx" TagName="wcEndereco" TagPrefix="uc2" %>
<%@ Register Src="controls/wcTabelaDinamica.ascx" TagName="wcTabelaDinamica" TagPrefix="uc3" %>
<%@ Register Src="controls/wcHierarquias.ascx" TagName="wcHierarquias" TagPrefix="uc5" %>
<%@ Register Src="controls/wcPessoaFisica.ascx" TagName="wcPessoaFisica" TagPrefix="uc6" %>
<%@ Register Src="controls/wcCampanhas.ascx" TagName="wcCampanhas" TagPrefix="uc7" %>
<%@ Register Src="controls/wcDePara.ascx" TagName="wcDePara" TagPrefix="uc8" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Pessoa Juridica</h1>
    <div id="dvSalvarPessoaJuridica" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar a listagem" OnClick="btnVoltar_Click" /><br />
            <div class="demo">
                <div id="tabs">
                    <ul>
                        <li><a href="#tabs-PessoaJuridica" id="aPessoaJuridica">Pessoa Jurídica</a></li>
                        <li><a href="#tabs-Emails" id="aEmails">Emails</a></li>
                        <li><a href="#tabs-Telefones" id="aTelefones">Telefones</a></li>
                        <li><a href="#tabs-Enderecos" id="aEnderecos">Endereços</a></li>
                        <li><a href="#tabs-DadosAdicionais" id="aDadosAdicionais">Dados Adicionais</a></li>
                        <li><a href="#tabs-Hierarquias" id="aHierarquias">Hierarquias</a></li>
                        <li><a href="#tabs-PessoasFisicas" id="aPessoasFisicas">Pessoas Fisicas</a></li>
                        <li><a href="#tabs-Campanhas" id="aCampanhas">Campanhas</a></li>
                        <li><a href="#tabs-DePara" id="aDePara">De para</a></li>
                    </ul>
                    <div id="tabs-PessoaJuridica">
                        <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 100px">
                                    Razão Social:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRazaoSocial" runat="server" Width="423px"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Inscrição Estadual:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInscricaoEstadual" runat="server" Width="423px"></asp:TextBox><br />
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
                                    CNPJ:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCNPJ" CssClass="cnpj" runat="server" Width="180px"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    DataFundacao:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataFundacao" CssClass="data" Width="110px" runat="server"></asp:TextBox><br />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnSalvarPessoaJuridica" runat="server" Text="Salvar" OnClick="btnSalvarPessoaJuridica_Click" />
                    </div>
                    <div id="tabs-Emails">
                        <uc4:wcEmail ID="wcEmail" runat="server" />
                        <asp:Button ID="btnSalvarEmails" runat="server" Text="Salvar" OnClick="btnSalvarEmails_Click" />
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
                        <div style="clear: both">
                        </div>
                        <asp:Button ID="btnSalvarTabelaDinamica" runat="server" Text="Salvar" OnClick="btnSalvarTabelaDinamica_Click" />
                    </div>
                    <div id="tabs-Hierarquias" style="float: left;">
                        <uc5:wcHierarquias ID="wcHierarquias" runat="server" />
                    </div>
                    <div id="tabs-PessoasFisicas" style="float: left;">
                        <uc6:wcPessoaFisica ID="wcPessoaFisica" runat="server" />
                    </div>
                    <div id="tabs-Campanhas" style="float: left;">
                        <uc7:wcCampanhas ID="wcCampanhas" runat="server" />
                    </div>
                    <div id="tabs-DePara" style="float: left;">
                        <uc8:wcDePara ID="wcDePara" runat="server" />
                    </div>
                </div>
            </div>

            <script type="text/javascript">
                $("#tabs").tabs();
            </script>

            <div style="clear: both">
            </div>
        </fieldset>
    </div>
    <div id="dvListarPessoaJuridicas" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <p>
                <div>
                    Nome:
                </div>
                <asp:TextBox ID="txtRazaoSocialBusca" runat="server" Width="400px"></asp:TextBox>
                <asp:Button runat="server" ID="btnFiltar" Text="Filtrar" OnClick="btnFiltar_Click" />
            </p>
            <asp:GridView ID="gvPessoaJuridicas" runat="server" Width="465px" AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado." OnRowCommand="gvPessoaJuridicas_RowCommand"
                AllowPaging="true" PageSize="18" OnPageIndexChanging="gvPessoaJuridicas_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "RazaoSocial") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDPessoaJuridica") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
