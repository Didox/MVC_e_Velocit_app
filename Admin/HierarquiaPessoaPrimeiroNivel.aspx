<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="HierarquiaPessoaPrimeiroNivel.aspx.cs"
    Inherits="_HierarquiaPessoaPrimeiroNivel" ValidateRequest="false" Title="HierarquiaPessoaPrimeiroNivel" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Adicionar pessoas juridicas ao primeiro nivel da hierarquia</h1>
    <asp:Button ID="btnVoltarHierarquias" runat="server" Text="Voltar para hierarquias"
        OnClick="btnVoltarHierarquias_Click" />
        <br /><br />
    <div>
        Hierarquia:
        <asp:DropDownList ID="ddlHierarquia" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlHierarquia_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <hr />
    <table style="width: 100% border: 0px" id="tableAddPessoas" runat="server">
        <tr>
            <td style="width: 100px">
                <div>
                    Razão social:
                    <asp:TextBox ID="txtData" Width="202px" runat="server"></asp:TextBox><asp:Button
                        ID="btnFind" runat="server" Text="Ok" OnClick="btnFind_Click" />
                </div>
                <asp:ListBox ID="listPessoasJuridicas" runat="server" Height="330px" Width="300px"
                    SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td align="center">
                <asp:Button ID="btnDelete" CssClass="btn" runat="server" Text="<<" OnClick="btnDelete_Click" />
                <asp:Button ID="btnAdd" CssClass="btn" runat="server" Text=">>" OnClick="btnAdd_Click" />
            </td>
            <td style="width: 100px">
                <asp:ListBox ID="listPessoasJuridicasAdd" runat="server" Height="330px" Width="300px"
                    SelectionMode="Multiple"></asp:ListBox>
            </td>
        </tr>
    </table>
</asp:Content>
