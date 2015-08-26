<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="TipoTabelaColuna.aspx.cs"
    Inherits="_TipoTabelaColuna" ValidateRequest="false" Title="TipoTabelaColuna" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        TipoTabelaColunas</h1>
    <div id="dvSalvarTipoTabelaColuna" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <div id="dvFields">
                <div class="dvRoll">
                    <div class="dvColl">
                        Tabela:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtTabela" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        Coluna:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtColuna" runat="server"></asp:TextBox><br />
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarTipoTabelaColunas" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <asp:GridView ID="gvTipoTabelaColunas" runat="server" Width="465px" AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
                OnRowCommand="gvTipoTabelaColunas_RowCommand" OnPageIndexChanging="gvTipoTabelaColunas_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Tabela">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Tabela") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Coluna">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Coluna") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTipoTabelaColuna") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTipoTabelaColuna") %>'
                                OnClientClick="if(confirm('Confirma exclusao?')){ return true; }else{ return false; }">Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
