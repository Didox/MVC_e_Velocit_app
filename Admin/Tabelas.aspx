<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Tabelas.aspx.cs"
    Inherits="_Tabelas" ValidateRequest="false" Title="Tabelas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Tabelas</h1>
    <div id="dvSalvarTabela" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100px">
                        Nome tabela:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" Width="423px"></asp:TextBox><br />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnIncluirCampos" runat="server" Text="Campos" OnClick="btnIncluirCampos_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarTabelas" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <p>
                <div>
                    Nome tabela:
                </div>
                <asp:TextBox ID="txtNomeTabela" runat="server" Width="400px"></asp:TextBox>
                <asp:Button runat="server" ID="btnFiltar" Text="Filtrar" OnClick="btnFiltar_Click" />
            </p>
            <asp:GridView ID="gvTabelas" runat="server" Width="465px" AutoGenerateColumns="False"
                    EmptyDataText="Nenhum registro encontrado." 
                OnRowCommand="gvTabelas_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Descricao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTabela") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTabela") %>'>Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
