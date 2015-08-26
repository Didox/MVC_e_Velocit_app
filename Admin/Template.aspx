<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Template.aspx.cs"
    Inherits="_Template" ValidateRequest="false" Title="Template" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="ckeditor/ckeditor.js"></script>

    <script src="ckeditor/sample.js" type="text/javascript"></script>

    <link href="ckeditor/sample.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Templates</h1>
    <div id="dvSalvarTemplate" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100px">
                        Nome template:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" Width="423px"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        Chave template:
                    </td>
                    <td>
                        <asp:TextBox ID="txtChave" runat="server" Width="134px"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        Conteúdo:
                    </td>
                    <td>
                        <asp:TextBox ID="txtConteudo" TextMode="MultiLine" runat="server" Height="429px"
                            Width="1106px"></asp:TextBox>

                        <script type="text/javascript">//CKEDITOR.replace('ctl00_ContentPlaceHolder1_txtConteudo', { language: 'pt-br', height: '600px' });</script>

                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarTemplates" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <p>
                <div>
                    Nome template:
                </div>
                <asp:TextBox ID="txtNomeTemplate" runat="server" Width="400px"></asp:TextBox>
                <asp:Button runat="server" ID="btnFiltar" Text="Filtrar" OnClick="btnFiltar_Click" />
            </p>
            <asp:GridView ID="gvTemplates" runat="server" Width="465px" EmptyDataText="Nenhum registro encontrado."
                AutoGenerateColumns="False" OnRowCommand="gvTemplates_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Descricao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTemplate") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
    <br />
</asp:Content>
