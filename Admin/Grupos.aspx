<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Grupos.aspx.cs"
    Inherits="_Grupos" ValidateRequest="false" Title="Grupos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Grupos</h1>
    <div id="dvSalvarGrupo" visible="false" runat="server">
        <fieldset>
            <legend>Editar</legend>
            <asp:TextBox ID="txtId" runat="server" Width="45px" Visible="False"></asp:TextBox>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100px">
                        Nome grupo:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" Width="423px"></asp:TextBox><br />
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /><br />
        </fieldset>
    </div>
    <div id="dvListarGrupos" runat="server">
        <fieldset>
            <legend>Listar</legend>
            <asp:Button runat="server" ID="btnIncluir" Text="Incluir" OnClick="btnIncluir_Click" />
            <p>
                <div>
                    Nome grupo:
                </div>
                <asp:TextBox ID="txtNomeGrupo" runat="server" Width="400px"></asp:TextBox>
                <asp:Button runat="server" ID="btnFiltar" Text="Filtrar" OnClick="btnFiltar_Click" />
            </p>
            <asp:GridView ID="gvGrupos" runat="server" Width="465px" AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado." 
                OnRowCommand="gvGrupos_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Descricao">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Descricao") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAlterar" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDGrupo") %>'>Editar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAddPaginas" CommandName="AddPaginas" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDGrupo") %>'>Add Paginas</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkAddUsuarios" CommandName="AddUsuarios" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IDGrupo") %>'>Add Usuarios</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
    <div id="dvAddPaginas" runat="server">
            <fieldset>
                <legend>Adicionar paginas</legend>
                <table style="width: 100% border: 0px">
                    <tr>
                        <td style="width: 100px">
                            <asp:ListBox ID="listPaginas" runat="server" Height="330px" Width="300px" 
                                SelectionMode="Multiple" DataTextField="Descricao" DataValueField="IDPagina">
                            </asp:ListBox>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnDelete" runat="server" Text="<<" OnClick="btnDelete_Click" />
                            <asp:Button ID="btnAdd" runat="server" Text=">>" OnClick="btnAdd_Click" />
                        </td>
                        <td style="width: 100px">
                            <asp:ListBox ID="listPaginasAdd" runat="server" Height="330px" Width="300px" 
                                SelectionMode="Multiple" DataTextField="Descricao" DataValueField="IDPagina">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnVoltarGruposPaginas" runat="server" Text="Voltar" 
                    onclick="btnVoltarGruposPaginas_Click"/>
            </fieldset>            
    </div>
    <div id="dvAddUsuarios" runat="server">
        <fieldset>
                <legend>Adicionar usuarios</legend>
                <table style="width: 100% border: 0px">
                    <tr>
                        <td style="width: 100px">
                            <asp:ListBox ID="listUsuarios" runat="server" Height="330px" Width="300px" 
                                SelectionMode="Multiple" DataTextField="Nome" DataValueField="IDUsuario">
                            </asp:ListBox>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnDeleteUsuario" runat="server" Text="<<" 
                                onclick="btnDeleteUsuario_Click"  />
                            <asp:Button ID="btnAddUsuario" runat="server" Text=">>" 
                                onclick="btnAddUsuario_Click"  />
                        </td>
                        <td style="width: 100px">
                            <asp:ListBox ID="listUsuariosAdd" runat="server" Height="330px" Width="300px" 
                                SelectionMode="Multiple" DataTextField="Nome" DataValueField="IDUsuario">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
            <asp:Button ID="btnVoltarGruposUsuario" runat="server" Text="Voltar" 
                    onclick="btnVoltarGruposUsuario_Click"/>
            </fieldset>
    </div>
</asp:Content>
