<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Tipo.aspx.cs"
    Inherits="_Tipo" ValidateRequest="false" Title="Tipo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
 <h1>
        Tipos</h1>
    <div id="dvSalvarTipo" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <div id="dvFields">
				<div class="dvRoll">  <div class="dvColl">    Nome:  </div>  <div class="dvColl">    <asp:TextBox ID="txtNome" runat="server"></asp:TextBox><br />  </div></div><div class="dvRoll">  <div class="dvColl">    IDTipoTabelaColuna:  </div>  <div class="dvColl">    <asp:TextBox ID="txtIDTipoTabelaColuna" runat="server"></asp:TextBox><br />  </div></div>				
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarTipos" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <asp:GridView ID="gvTipos" runat="server" Width="465px" AutoGenerateColumns="False"
                    EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
                OnRowCommand="gvTipos_RowCommand"
                OnPageIndexChanging="gvTipos_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Nome">  <ItemTemplate>      <%# DataBinder.Eval(Container.DataItem, "Nome") %>  </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="IDTipoTabelaColuna">  <ItemTemplate>      <%# DataBinder.Eval(Container.DataItem, "IDTipoTabelaColuna") %>  </ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTipo") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTipo") %>' OnClientClick="if(confirm('Confirma exclusao?')){ return true; }else{ return false; }" >Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
