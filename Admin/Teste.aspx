<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Teste.aspx.cs"
    Inherits="_Teste" ValidateRequest="false" Title="Teste" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
 <h1>
        Testes</h1>
    <div id="dvSalvarTeste" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <div id="dvFields">
				<div class="dvRoll">  <div class="dvColl">    Descricao:  </div>  <div class="dvColl">    <asp:TextBox ID="txtDescricao" runat="server"></asp:TextBox><br />  </div></div><div class="dvRoll">  <div class="dvColl">    Oliveira:  </div>  <div class="dvColl">    <asp:TextBox ID="txtOliveira" runat="server"></asp:TextBox><br />  </div></div><div class="dvRoll">  <div class="dvColl">    Assi:  </div>  <div class="dvColl">    <asp:TextBox ID="txtAssi" runat="server"></asp:TextBox><br />  </div></div>				
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarTestes" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <asp:GridView ID="gvTestes" runat="server" Width="465px" AutoGenerateColumns="False"
                    EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
                OnRowCommand="gvTestes_RowCommand"
                OnPageIndexChanging="gvTestes_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">  <ItemTemplate>      <%# DataBinder.Eval(Container.DataItem, "Descricao") %>  </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Oliveira">  <ItemTemplate>      <%# DataBinder.Eval(Container.DataItem, "Oliveira") %>  </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Assi">  <ItemTemplate>      <%# DataBinder.Eval(Container.DataItem, "Assi") %>  </ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTeste") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDTeste") %>' OnClientClick="if(confirm('Confirma exclusao?')){ return true; }else{ return false; }" >Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
