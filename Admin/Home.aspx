<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Home" Title="Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="dvSeleciona" runat="server">
        <fieldset>
            <legend>Selecione a configuração inicial</legend>
            <div>
                Selecione o Cliente
                <asp:DropDownList ID="ddlClientes" AutoPostBack="true" runat="server" DataTextField="Nome"
                    DataValueField="IDCliente" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div id="dvProduto" runat="server" visible="false">
                Selecione o Programa
                <asp:DropDownList ID="ddlProgramas" AutoPostBack="true" runat="server" DataTextField="Descricao"
                    DataValueField="IDPrograma" OnSelectedIndexChanged="ddlProgramas_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div id="dvCampanha" runat="server" visible="false">
                Selecione a campanha
                <asp:DropDownList ID="ddlCampanhas" runat="server" DataTextField="Descricao" DataValueField="IDCampanha">
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnEntrar" runat="server" Text="Configurar" OnClick="btnEntrar_Click" />
        </fieldset>
    </div>
    <div id="dvQueDeseja" runat="server" visible="false">
        <fieldset>
            <legend>O que você deseja fazer</legend>
            <ul>
                <li><a href="Campanha.aspx">Cadastrar Campanha</a> </li>
                <li><a href="Cliente.aspx">Cadastrar Cliente</a> </li>
                <li><a href="Estado.aspx">Cadastrar Estado</a> </li>
                <li><a href="Pais.aspx">Cadastrar País</a> </li>
                <li><a href="Programa.aspx">Cadastrar Programa</a> </li>
                <li><a href="Credenciais.aspx">Cadastrar pessoa fisica</a> </li>
                <li><a href="PessoaJuridica.aspx">Cadastrar pessoa juridica</a> </li>
                <li id="lstMenuss" runat="server"><a href="Paginas.aspx">Cadastrar Paginas</a> </li>
                <li><a href="Template.aspx">Cadastrar Templates</a> </li>
                <li><a href="Componente.aspx">Cadastrar Componentes</a> </li>
                <li><a href="Grupos.aspx">Cadastrar Grupos</a> </li>
                <!--li><a href="ConfigurarSenhas.aspx">Configurar Senhas</a> </li-->
                <li><a href="Tabelas.aspx">Tabela dinâmica</a> </li>
                <li><a href="Cargos.aspx">Cargos</a> </li>
                <li><a href="CargosPessoa.aspx">Cargos Pessoa</a> </li>
                <li><a href="Hierarquias.aspx">Hierarquia</a> </li>
                <li><a href="HierarquiaPessoa.aspx">Hierarquia Pessoa</a> </li>
                
                <li><a href="Documento.aspx">Documentos</a> </li>
                
                
                <li><a href="TipoTelefone.aspx">Cadastrar TipoTelefone</a> </li>
                <li><a href="TipoEndereco.aspx">Cadastrar TipoEndereco</a> </li>
                <li><a href="TipoEmail.aspx">Cadastrar TipoEmail</a> </li>
                <li><a href="TipoDocumento.aspx">Cadastrar TipoDocumento</a> </li>
                <li><a href="Tipo.aspx">Cadastrar Tipos</a> </li>
                <li><a href="TipoTabelaColuna.aspx">Cadastrar TipoTabelaColuna</a> </li>
            </ul>
        </fieldset>
    </div>
</asp:Content>
