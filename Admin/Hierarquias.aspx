<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Hierarquias.aspx.cs"
    Inherits="_Hierarquias" ValidateRequest="false" Title="Hierarquias" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Hierarquias</h1>
    <div id="dvSalvarHierarquia" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100px">
                        Nome Hierarquia:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" Width="423px"></asp:TextBox><br />
                    </td>
                </tr>
                 <tr>
                    <td style="width: 100px">
                        Coluna estrutura:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlColunaEstrutura" runat="server">
                        </asp:DropDownList>
                        <br />
                    </td>
                </tr>
                 <tr>
                    <td style="width: 100px">
                        Hierarquia pai:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlHierarquiaPai" runat="server">
                        </asp:DropDownList><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Ordem:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrdem" runat="server" Text="0" Width="60px"></asp:TextBox><br />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarHierarquias" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <p>
                <div>
                    Nome Hierarquia:
                </div>
                <asp:TextBox ID="txtNomeHierarquia" runat="server" Width="400px"></asp:TextBox>
                <asp:Button runat="server" ID="btnFiltar" Text="Filtrar" OnClick="btnFiltar_Click" />
            </p>
            <asp:GridView ID="gvHierarquias" runat="server" Width="465px" AutoGenerateColumns="False"
                    EmptyDataText="Nenhum registro encontrado." 
                OnRowCommand="gvHierarquias_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Descricao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDHierarquia") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
