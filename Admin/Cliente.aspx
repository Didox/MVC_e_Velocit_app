<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Cliente.aspx.cs"
    Inherits="_Cliente" ValidateRequest="false" Title="Cliente" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Clientes</h1>
    <div id="dvSalvarCliente" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <div id="dvFields">
                <div class="dvRoll">
                    <div class="dvColl">
                        Descricao:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDescricao" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        IPServidor:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtIPServidor" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DBName:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDBName" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DBUser:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDBUser" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DBPassword:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDBPassword" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        Slug:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtSlug" runat="server"></asp:TextBox><br />
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarClientes" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <asp:GridView ID="gvClientes" runat="server" Width="465px" AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
                OnRowCommand="gvClientes_RowCommand" OnPageIndexChanging="gvClientes_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Nome")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IPServidor">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "IPServidor") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DBName">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DBName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DBUser">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DBUser") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DBPassword">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DBPassword") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Slug">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Slug") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDCliente") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDCliente") %>'
                                OnClientClick="if(confirm('Confirma exclusao?')){ return true; }else{ return false; }">Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
