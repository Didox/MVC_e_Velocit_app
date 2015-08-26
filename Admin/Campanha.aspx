<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Campanha.aspx.cs"
    Inherits="_Campanha" ValidateRequest="false" Title="Campanhas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Campanhas</h1>
    <div id="dvSalvarCampanha" visible="false" runat="server">
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
                        Programa:
                    </div>
                    <div class="dvColl">
                        <asp:DropDownList ID="ddlIDPrograma" runat="server">
                        </asp:DropDownList><br />
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
                <div class="dvRoll">
                    <div class="dvColl">
                        DataInicio:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDataInicio" CssClass="data" Width="110px"  runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DataFim:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDataFim" CssClass="data" Width="110px" runat="server"></asp:TextBox><br />
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarCampanhas" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <asp:GridView ID="gvCampanhas" runat="server" Width="465px" AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
                OnRowCommand="gvCampanhas_RowCommand" OnPageIndexChanging="gvCampanhas_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Descricao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IDPrograma">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Programa.Descricao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Slug">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Slug") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DataInicio">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DataInicioFormatada") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DataFim">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DataFimFormatada")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDCampanha") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDCampanha") %>'
                                OnClientClick="if(confirm('Confirma exclusao?')){ return true; }else{ return false; }">Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
