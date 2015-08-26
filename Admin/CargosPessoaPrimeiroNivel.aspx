<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="CargosPessoaPrimeiroNivel.aspx.cs"
    Inherits="_CargosPessoaPrimeiroNivel" ValidateRequest="false" Title="CargosPessoaPrimeiroNivel" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <h1>
        Adicionar pessoas fisicas ao primeiro nivel da hierarquia de cargos</h1>
    <asp:Button ID="btnVoltarHierarquias" runat="server" Text="Voltar para hierarquia de cargos"
        OnClick="btnVoltarHierarquias_Click" />
        <br /><br />
    <div>
        Cargos:
        <asp:DropDownList ID="ddlCargo" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCargo_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <hr />
    <table style="width: 100% border: 0px" id="tableAddPessoas" runat="server">
        <tr>
            <td style="width: 100px">
                <div>
                    Nome:
                    <asp:TextBox ID="txtData" Width="202px" runat="server"></asp:TextBox><asp:Button
                        ID="btnFind" runat="server" Text="Ok" OnClick="btnFind_Click" />
                </div>
                <asp:ListBox ID="listPessoas" runat="server" Height="330px" Width="300px"
                    SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td align="center">
                <asp:Button ID="btnDelete" CssClass="btn" runat="server" Text="<<" OnClick="btnDelete_Click" />
                <asp:Button ID="btnAdd" CssClass="btn" runat="server" Text=">>" OnClick="btnAdd_Click" />
            </td>
            <td style="width: 100px">
                <asp:ListBox ID="listPessoasAdd" runat="server" Height="330px" Width="300px"
                    SelectionMode="Multiple"></asp:ListBox>
            </td>
        </tr>
    </table>
</asp:Content>
