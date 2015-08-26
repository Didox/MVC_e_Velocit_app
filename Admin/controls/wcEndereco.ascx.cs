using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;

public partial class controls_wcEndereco : System.Web.UI.UserControl
{
    private int IdConteudo = 0;
    private bool PessoJuridica = false;

    public void SetIdUsuario(int idJuridica)
    {
        IdConteudo = idJuridica;
        PessoJuridica = true;
    }

    public void SetIdPessoaJuridica(int idUsuario)
    {
        IdConteudo = idUsuario;
        PessoJuridica = false;
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCep.Attributes.Add("onkeypress", "return formatValue( this , '99999-999', event )");
        txtEnderecoNumero.Attributes.Add("onkeypress", "return formatValue( this , '999999999', event )");
        if (!IsPostBack) LoadCombos();
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

    public void ClearData()
    {
        dvScript.InnerHtml = string.Empty;
    }

    public void GetEndereco(Pessoa pessoa)
    {
        dvScript.InnerHtml = "<script type=\"text/javascript\">";
        dvScript.InnerHtml += "jsonEnderecos = " + pessoa.Enderecos.ToJson() + ";";
        dvScript.InnerHtml += "$(\"#hiddenEnderecoCount\").val(\"0\");";
        dvScript.InnerHtml += "GetEnderecos();";
        dvScript.InnerHtml += "</script>";
    }

    public void DisableDivs()
    {
        dvSalvarEndereco.Visible = false;
    }

    public void EnableDivs()
    {
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
    }
}
