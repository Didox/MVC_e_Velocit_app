<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="PessoaCampanha.aspx.cs"
    Inherits="_PessoaCampanha" ValidateRequest="false" Title="PessoaCampanha" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        PessoaCampanhas</h1>
    <div id="dvSalvarPessoaCampanha" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <div id="dvFields">
                <div class="dvRoll">
                    <div class="dvColl">
                        IDCampanha:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtIDCampanha" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        IDPessoa:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtIDPessoa" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DataAdesao:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDataAdesao" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DataExclusao:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDataExclusao" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        IDUsuario:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtIDUsuario" runat="server"></asp:TextBox><br />
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarPessoaCampanhas" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <asp:GridView ID="gvPessoaCampanhas" runat="server" Width="465px" AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
                OnRowCommand="gvPessoaCampanhas_RowCommand" OnPageIndexChanging="gvPessoaCampanhas_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="IDCampanha">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "IDCampanha") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IDPessoa">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "IDPessoa") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DataAdesao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DataAdesao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DataExclusao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DataExclusao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IDUsuario">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "IDUsuario") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDPessoaCampanha") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDPessoaCampanha") %>'
                                OnClientClick="if(confirm('Confirma exclusao?')){ return true; }else{ return false; }">Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
