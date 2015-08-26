<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="CamposTabela.aspx.cs"
    Inherits="_CamposTabela" ValidateRequest="false" Title="CamposTabelas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Campos tabela</h1>
    <div id="dvSalvarCampo" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100px">
                        Nome campo:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" Width="423px"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                     <td style="width: 100px">
                        Label campo:
                    </td>
                    <td>
                        <asp:TextBox ID="txtLabel" runat="server" Width="423px"></asp:TextBox><br />
                    </td>
                </tr>
                
                <tr>
                     <td style="width: 100px">
                        Tamanho:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTamanho" runat="server" Width="50px" Text="0"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                     <td style="width: 100px">
                        Valor padrão:
                    </td>
                    <td>
                        <asp:TextBox ID="txtValorPadrao" runat="server" Width="300px"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                     <td style="width: 100px">
                        Permite nulo:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoPermiteNulo" runat="server" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="True">Sim</asp:ListItem>
                            <asp:ListItem Selected="True" Value="False">Não</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                     <td style="width: 100px">
                        Tipo de dado:
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlTipo" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                     <td style="width: 100px">
                        Tipo de campo:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoCampo" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr> 
                <tr>
                     <td style="width: 100px">
                        Ordem:
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlOrdem" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnIncluirValoresCampo" runat="server" Text="Incluir valores" OnClick="btnIncluirValoresCampo_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarCampos" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />            
            <asp:Button ID="btnVoltaTabela" runat="server" Text="Voltar a tabela" OnClick="btnVoltaTabela_Click" />
            <p>
                <div>
                    Label tabela:
                </div>
                <asp:TextBox ID="txtNomeCampo" runat="server" Width="400px"></asp:TextBox>
                <asp:Button runat="server" ID="btnFiltar" Text="Filtrar" OnClick="btnFiltar_Click" />
            </p>
            <asp:GridView ID="gvCampos" runat="server" Width="465px" AutoGenerateColumns="False"
                    EmptyDataText="Nenhum registro encontrado." 
                OnRowCommand="gvCampos_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Nome">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Nome") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDCampo") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDCampo") %>'>Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
