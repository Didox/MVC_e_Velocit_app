using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;

public partial class controls_wcTelefoneEnderecoTabelaDinamica : System.Web.UI.UserControl
{
    public int IdConteudo = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCombos();
        }
    }

    public void SaveTelefoneEnderecoTabelaDinamica(Pessoa pessoa)
    {
        SavePessoaTelefone(pessoa);
        SavePessoaEndereco(pessoa);
        SaveTabelaDinamica(pessoa);
    }

    public void SavePessoaTelefone(Pessoa pessoa)
    {
        var telefoneCount = int.Parse(Request["hiddenTelefoneCount"]);
        var telefoneDel = new Telefone();
        telefoneDel.Pessoa = pessoa;
        telefoneDel.Delete();

        for (int i = 0; i < telefoneCount; i++)
        {
            if (string.IsNullOrEmpty(Request["txtNumeroTelefone-" + i])) continue;
            var telefone = new Telefone();
            telefone.Pessoa = pessoa;
            if (!string.IsNullOrEmpty(Request["ddlTiposTelefone-" + i]))
                telefone.IDTipoTelefone = int.Parse(Request["ddlTiposTelefone-" + i]);
            if (!string.IsNullOrEmpty(Request["txtDDITelefone-" + i]))
                telefone.DDI = int.Parse(Request["txtDDITelefone-" + i]);
            if (!string.IsNullOrEmpty(Request["txtDDDTelefone-" + i]))
                telefone.DDD = int.Parse(Request["txtDDDTelefone-" + i]);
            telefone.Numero = Request["txtNumeroTelefone-" + i];

            telefone.Save();
        }
    }

    public void SavePessoaEndereco(Pessoa pessoa)
    {
        var enderecoCount = int.Parse(Request["hiddenEnderecoCount"]);
        var enderecoDel = new Endereco();
        enderecoDel.Pessoa = pessoa;
        enderecoDel.Delete();

        for (int i = 0; i < enderecoCount; i++)
        {

            var endereco = new Endereco();
            endereco.Pessoa = pessoa;

            if (!string.IsNullOrEmpty(Request["ddlTipoEndereco-" + i]))
                endereco.IDTipoEndereco = int.Parse(Request["ddlTipoEndereco-" + i]);

            if (!string.IsNullOrEmpty(Request["ddlEstado-" + i]))
                endereco.IDEstado = int.Parse(Request["ddlEstado-" + i]);

            if (!string.IsNullOrEmpty(Request["ddlPais-" + i]))
                endereco.IDPais = int.Parse(Request["ddlPais-" + i]);

            endereco.Descricao = Request["txtEndereco-" + i];

            if (!string.IsNullOrEmpty(Request["txtEnderecoNumero-" + i]))
                endereco.Numero = int.Parse(Request["txtEnderecoNumero-" + i]);

            endereco.Bairro = Request["txtBairro-" + i];
            endereco.Cidade = Request["txtCidade-" + i];
            endereco.Cep = Request["txtCep-" + i];
            endereco.Complemento = Request["txtComplemento-" + i];

            endereco.Save();
        }
    }

    private void SaveTabelaDinamica(Pessoa pessoa)
    {
        var tabela = pessoa.GetTabelaDinamica();
        if (tabela != null && tabela.IDTabela != null)
        {
            foreach (var campo in tabela.Campos)
            {
                campo.Valor(pessoa).SetValor(Request[campo.Nome]);
                campo.Valor(pessoa).Save();
            }
        }
    }
      
    public void GetTelefoneEnderecoTabelaDinamica(Pessoa pessoa)
    {
        dvScript.InnerHtml = "<script type=\"text/javascript\">";
        dvScript.InnerHtml += "jsonTelefones = " + pessoa.Telefones.ToJson() + ";";
        dvScript.InnerHtml += "$(\"#hiddenEmailCount\").val(\"0\");";
        dvScript.InnerHtml += "GetEmails();";
        dvScript.InnerHtml += "jsonEnderecos = " + pessoa.Enderecos.ToJson() + ";";
        dvScript.InnerHtml += "$(\"#hiddenEnderecoCount\").val(\"0\");";
        dvScript.InnerHtml += "GetEnderecos();";
        dvScript.InnerHtml += "</script>";
        GetTabelaDinamica(pessoa);
    }

    public void GetTabelaDinamica(Pessoa pessoa)
    {
        dvTabelaDinamica.Visible = true;
        if (pessoa == null || pessoa.Fisica == null || pessoa.Fisica.IDPessoaFisica == null)
            dvTabelaDinamica.Visible = false;

        dvTabela.InnerHtml = string.Empty;

        var tabela = pessoa.GetTabelaDinamica();
        if (tabela != null) dvTabela.InnerHtml = tabela.RenderTabela(pessoa);
    }

    public void DisableDivs()
    {
        dvSalvarTelefones.Visible = false;
        dvSalvarEndereco.Visible = false;
        dvTabelaDinamica.Visible = false;
    }

    public void EnableDivs()
    {
        dvSalvarTelefones.Visible = true;
        dvSalvarEndereco.Visible = true;
    }

    private void LoadCombos()
    {
        ddlEstado.DataSource = new Estado().Find();
        ddlEstado.DataTextField = "Nome";
        ddlEstado.DataValueField = "IDEstado";
        ddlEstado.DataBind();

        ddlPais.DataSource = new Pais().Find();
        ddlPais.DataTextField = "Nome";
        ddlPais.DataValueField = "IDPais";
        ddlPais.DataBind();

        ddlTipoEndereco.DataSource = new TipoEndereco().Find();
        ddlTipoEndereco.DataTextField = "Nome";
        ddlTipoEndereco.DataValueField = "IDTipoEndereco";
        ddlTipoEndereco.DataBind();

        ddlTiposTelefone.DataSource = new TipoTelefone().Find();
        ddlTiposTelefone.DataTextField = "Nome";
        ddlTiposTelefone.DataValueField = "IDTipoTelefone";
        ddlTiposTelefone.DataBind();
    }

    protected void btnEditarTabelaFisica_Click(object sender, EventArgs e)
    {
        var usuario = new Usuario();
        usuario.IDUsuario = IdConteudo;
        usuario.Get();

        var tabela = usuario.Pessoa.GetTabelaDinamica();
        if (tabela == null)
            usuario.CreateTabelaDinamica();
        tabela = usuario.Pessoa.GetTabelaDinamica();
        Response.Redirect("~/Tabelas.aspx?idTabela=" + tabela.IDTabela.ToString());
    }
}
