<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wcTabelaDinamica.ascx.cs" Inherits="controls_wcTabelaDinamica" %>
<div style="width: 400px; float: left" id="dvTabelaDinamica" runat="server" visible="false">
    <div style="width: 100%;" id="dvTabela" runat="server">
    </div>
    <br style="clear:both" />
    <asp:LinkButton ID="btnEditarTabelaFisica" runat="server" onclick="btnEditarTabelaFisica_Click">Configurar Tabela</asp:LinkButton>
    <br />
    <br />
</div>
