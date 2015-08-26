<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="ConfigurarSenhas.aspx.cs"
    Inherits="_ConfigurarSenhas" ValidateRequest="false" Title="Configurar Senhas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Configurar Senha</h1>
    <div runat="server">
        <fieldset>
            <legend>Editar</legend>
            Senha criptografada?
            <asp:RadioButtonList ID="rblCroptografada" runat="server" 
                RepeatDirection="Horizontal" AutoPostBack="True" 
                onselectedindexchanged="rblCroptografada_SelectedIndexChanged">
                <asp:ListItem Value="true">Sim</asp:ListItem>
                <asp:ListItem Selected="True" Value="false">Não</asp:ListItem>
            </asp:RadioButtonList>
        </fieldset>
    </div>
</asp:Content>
