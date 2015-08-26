<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="Paginas.aspx.cs"
    Inherits="_Paginas" Title="Paginas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Paginas</h1>
    <div id="dvSalvarPagina" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <table style="width: 100%;">
                <tr>
                    <td width="100px">
                        Nome pagina:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" Width="177px"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td width="100px">
                        Descrição pagina:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescricao" runat="server" Width="423px"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        Slug pagina:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSlug" runat="server" Width="134px"></asp:TextBox><br />
                    </td>
                </tr>
                <tr >
                    <td>
                        Pagina restrita:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkRestrito" runat="server" />
                    </td>
                </tr>
                <tr >
                    <td>
                        Pagina interna:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkInterna" runat="server" />
                    </td>
                </tr>
                <tr >
                    <td>
                        Ordem:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrdem" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr >
                    <td>
                        Pagina pai:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPagina" DataTextField="Descricao" DataValueField="IDPagina" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Template:
                    </td>
                    <td> 
                        <asp:DropDownList ID="ddlTemplate" runat="server"></asp:DropDownList>                       
                        <asp:Button ID="btnNovoTemplate" runat="server" Text="Novo Template" 
                            onclick="btnNovoTemplate_Click" />
                        <asp:Button ID="btnEditarTemplate" runat="server" Text="Editar Template" 
                            onclick="btnEditarTemplate_Click" />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" 
                onclick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarPaginas" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir pagina" 
                onclick="btnIncluir_Click" />
    
            <asp:Button runat="server" ID="btnIncluirRestrito" Text="Incluir pagina restrita" 
                onclick="btnIncluirRestrito_Click" />
            <p>
                <div> Nome pagina: </div>
                <asp:TextBox ID="txtNomePagina" runat="server" Width="268px"></asp:TextBox>          
                <asp:CheckBox ID="chkRestritoPagina" Text="Restrito" Checked="true" runat="server" />  
                <asp:Button runat="server" ID="btnFiltar" Text="Filtrar" OnClick="btnFiltar_Click" />
                    </p>
            <asp:GridView ID="gvPaginas" runat="server" Width="465px" 
                    EmptyDataText="Nenhum registro encontrado." AutoGenerateColumns="False" OnRowCommand="gvPaginas_RowCommand">
             <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Descricao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDPagina") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>