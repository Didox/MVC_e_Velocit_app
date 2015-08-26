<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcDePara.ascx.cs" Inherits="controls_wcDePara" %>
<div id="dvSalvarPessoaCampanha" runat="server">
    <asp:TextBox ID="txtIdPessoaJuridica" runat="server" Width="45px" Visible="False"></asp:TextBox>
    <div id="dvFields">
        <div class="dvRoll">
            <div class="dvColl">
                 <div>
                    Nome:
                    <asp:TextBox ID="txtPessoasJuridicas" Width="202px" runat="server"></asp:TextBox><asp:Button ID="btnFind" runat="server"
                        Text="Ok" OnClick="btnFind_Click" />
                </div>
                <asp:ListBox ID="listPessoasJuridicas" runat="server" Height="62px" Width="300px"
                    SelectionMode="Multiple"></asp:ListBox>
            </div>
        </div>
    </div>
    <br />
    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
    <br /><br />
    
    <asp:GridView ID="gvPessoaJuridicasDePara" runat="server" Width="465px" AutoGenerateColumns="False"
        EmptyDataText="Nenhum vinculo entre empresas." OnRowCommand="gvPessoaJuridicasDePara_RowCommand"
        AllowPaging="true" PageSize="18" OnPageIndexChanging="gvPessoaJuridicasDePara_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="De">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "RazaoSocialDe") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Para">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "RazaoSocialPara") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDDeJuridicaParaJuridica") %>'>Excluir</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>