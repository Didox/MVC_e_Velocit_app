<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Documento.aspx.cs"
    Inherits="_Documento" ValidateRequest="false" Title="Documento" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Documentos</h1>
    <div id="dvSalvarDocumento" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <div id="dvFields">
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
                        TipoDocumento:
                    </div>
                    <div class="dvColl">
                        <asp:DropDownList ID="ddlIDTipoDocumento" runat="server">
                        </asp:DropDownList><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DescDocumento:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDescDocumento" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DocNumero:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDocNumero" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DocComplemento:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDocComplemento" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <div class="dvRoll">
                    <div class="dvColl">
                        DocDV:
                    </div>
                    <div class="dvColl">
                        <asp:TextBox ID="txtDocDV" runat="server"></asp:TextBox><br />
                    </div>
                </div>
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarDocumentos" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <asp:GridView ID="gvDocumentos" runat="server" Width="465px" AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
                OnRowCommand="gvDocumentos_RowCommand" OnPageIndexChanging="gvDocumentos_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Pessoa">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Pessoa.Nome") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TipoDocumento">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TipoDocumento.Nome")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DescDocumento">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DescDocumento") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DocNumero">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DocNumero") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DocComplemento">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DocComplemento") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DocDV">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "DocDV") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDDocumento") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDDocumento") %>'
                                OnClientClick="if(confirm('Confirma exclusao?')){ return true; }else{ return false; }">Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
