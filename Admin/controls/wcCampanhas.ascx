<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcCampanhas.ascx.cs" Inherits="controls_wcCampanhas" %>
<div id="dvSalvarPessoaCampanha" visible="false" runat="server">
    <asp:TextBox ID="txtIdPessoa" runat="server" Width="45px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtIdUsuario" runat="server" Width="45px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
    <div id="dvFields">
        <div class="dvRoll">
            <div class="dvColl">
                DataAdesao:
            </div>
            <div class="dvColl">
                <asp:TextBox ID="txtDataAdesao" CssClass="data" runat="server"></asp:TextBox><br />
            </div>
        </div>
        <div class="dvRoll">
            <div class="dvColl">
                DataExclusao:
            </div>
            <div class="dvColl">
                <asp:TextBox ID="txtDataExclusao" CssClass="data" runat="server"></asp:TextBox><br />
            </div>
        </div>
    </div>
    <br />
    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
</div>
<div id="dvListarPessoaCampanhas" runat="server">
    <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" /><br />
    <br />
    <asp:GridView ID="gvPessoaCampanhas" runat="server" Width="800px" AutoGenerateColumns="False"
        EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
        OnRowCommand="gvPessoaCampanhas_RowCommand" OnPageIndexChanging="gvPessoaCampanhas_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="Data adesão">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "DataAdesaoFormatada") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Data exclusão">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "DataExclusaoComValidacao") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" >
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
</div>
<div id="dvCampanha" runat="server">
</div>
