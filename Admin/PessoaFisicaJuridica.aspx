<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="PessoaFisicaJuridica.aspx.cs"
    Inherits="_PessoaFisicaJuridica" ValidateRequest="false" Title="PessoaFisicaJuridica" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
 <h1>
        PessoaFisicaJuridicas</h1>
    <div id="dvSalvarPessoaFisicaJuridica" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <div id="dvFields">
				<div class="dvRoll">  <div class="dvColl">    IDPessoaFisica:  </div>  <div class="dvColl">    <asp:TextBox ID="txtIDPessoaFisica" runat="server"></asp:TextBox><br />  </div></div><div class="dvRoll">  <div class="dvColl">    IDPessoaJuridica:  </div>  <div class="dvColl">    <asp:TextBox ID="txtIDPessoaJuridica" runat="server"></asp:TextBox><br />  </div></div>				
            </div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarPessoaFisicaJuridicas" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <asp:GridView ID="gvPessoaFisicaJuridicas" runat="server" Width="465px" AutoGenerateColumns="False"
                    EmptyDataText="Nenhum registro encontrado." AllowPaging="true" PageSize="20"
                OnRowCommand="gvPessoaFisicaJuridicas_RowCommand"
                OnPageIndexChanging="gvPessoaFisicaJuridicas_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="IDPessoaFisica">  <ItemTemplate>      <%# DataBinder.Eval(Container.DataItem, "IDPessoaFisica") %>  </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="IDPessoaJuridica">  <ItemTemplate>      <%# DataBinder.Eval(Container.DataItem, "IDPessoaJuridica") %>  </ItemTemplate></asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDPessoaFisicaJuridica") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDPessoaFisicaJuridica") %>' OnClientClick="if(confirm('Confirma exclusao?')){ return true; }else{ return false; }" >Excluir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
