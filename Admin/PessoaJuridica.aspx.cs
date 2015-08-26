using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using System.Web.UI.MobileControls;
using TradeVision.UI;

public partial class _PessoaJuridica : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtId.Text))
            setIdOnControls(int.Parse(txtId.Text)); 

        if (!IsPostBack)
        {
            if (Request["idPessoaJuridica"] != null)
            {
                GetPessoaJuridica(int.Parse(Request["idPessoaJuridica"])); 
                if (!string.IsNullOrEmpty(Request["tab"]))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script" + Funcoes.KeyStript(), "<script>$('#" + Request["tab"] + "').trigger(\"click\");</script>");
                return;
            }
            GetPessoaJuridicas();            
        }
    }

    protected void btnSalvarPessoaJuridica_Click(object sender, EventArgs e)
    {
        var pessoaJuridica = new PessoaJuridica();
        try
        {
            var pessoa = new Pessoa();
            if (txtId.Text != "")
            {
                pessoaJuridica.IDPessoaJuridica = int.Parse(txtId.Text);
                pessoaJuridica.Get();
            }

            pessoaJuridica.Pessoa = pessoa;
            pessoaJuridica.RazaoSocial = txtRazaoSocial.Text;
            pessoaJuridica.InscricaoEstadual = txtInscricaoEstadual.Text;
            pessoaJuridica.CNPJ = txtCNPJ.Text;
            pessoaJuridica.DataFundacaoFormatada = txtDataFundacao.Text;
            pessoaJuridica.Save();

            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aPessoaJuridica').trigger(\"click\");alert('Pessoa juridica salva.')</script>");
        }
        catch (Exception err)
        {
            GetPessoaJuridica(pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aPessoaJuridica').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");            
        }
    }

    protected void btnSalvarEmails_Click(object sender, EventArgs e)
    {
        var pessoaJuridica = new PessoaJuridica();
        try
        {
            pessoaJuridica.IDPessoaJuridica = int.Parse(txtId.Text);
            pessoaJuridica.Get();

            wcEmail.SavePessoaEmail(pessoaJuridica.Pessoa);
            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aEmails').trigger(\"click\");alert('Emails salvo.');</script>");
        }
        catch (Exception err)
        {
            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aEmails').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }

    protected void btnSalvarTelefones_Click(object sender, EventArgs e)
    {
        var pessoaJuridica = new PessoaJuridica();
        try
        {
            pessoaJuridica.IDPessoaJuridica = int.Parse(txtId.Text);
            pessoaJuridica.Get();

            wcTelefone.SavePessoaTelefone(pessoaJuridica.Pessoa);
            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aTelefones').trigger(\"click\");alert('Telefones salvo.');</script>");
        }
        catch (Exception err)
        {
            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aTelefones').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }

    protected void btnSalvarEnderecos_Click(object sender, EventArgs e)
    {
        var pessoaJuridica = new PessoaJuridica();
        try
        {
            pessoaJuridica.IDPessoaJuridica = int.Parse(txtId.Text);
            pessoaJuridica.Get();

            wcEndereco.SavePessoaEndereco(pessoaJuridica.Pessoa);
            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aEnderecos').trigger(\"click\");alert('Endereços salvo.');</script>");
        }
        catch (Exception err)
        {
            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aEnderecos').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }

    protected void btnSalvarTabelaDinamica_Click(object sender, EventArgs e)
    {
        var pessoaJuridica = new PessoaJuridica();
        try
        {
            pessoaJuridica.IDPessoaJuridica = int.Parse(txtId.Text);
            pessoaJuridica.Get();

            wcTabelaDinamica.SaveTabelaDinamica(pessoaJuridica.Pessoa);
            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aDadosAdicionais').trigger(\"click\");alert('Dados adicionais salvo.');</script>");
        }
        catch (Exception err)
        {
            GetPessoaJuridica((int)pessoaJuridica.IDPessoaJuridica);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aDadosAdicionais').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }
    
    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetPessoaJuridicas();
    }

    private void GetPessoaJuridicas()
    {
        dvSalvarPessoaJuridica.Visible = false;
        dvListarPessoaJuridicas.Visible = true;

        gvPessoaJuridicas.DataSource = new PessoaJuridica(txtRazaoSocialBusca.Text).Find();
        gvPessoaJuridicas.DataBind();
    }

    private void setIdOnControls(int IDPessoaJuridica)
    {
        wcTelefone.SetIdPessoaJuridica(IDPessoaJuridica);
        wcEmail.SetIdPessoaJuridica(IDPessoaJuridica);
        wcEndereco.SetIdPessoaJuridica(IDPessoaJuridica);
        wcTabelaDinamica.SetIdPessoaJuridica(IDPessoaJuridica);
    }

    private void GetPessoaJuridica(int? idPessoaJuridica)
    {
        if (idPessoaJuridica == null) return;
        var pessoaJuridica = new PessoaJuridica(idPessoaJuridica);
        pessoaJuridica.Get();
        if (pessoaJuridica.IDPessoaJuridica == null) return;
        if (pessoaJuridica.Pessoa == null) return;
        clearFields();

        setIdOnControls((int)pessoaJuridica.IDPessoaJuridica);
        txtId.Text = pessoaJuridica.IDPessoaJuridica.ToString();

        dvSalvarPessoaJuridica.Visible = true;
        dvListarPessoaJuridicas.Visible = false;
        wcTelefone.EnableDivs();
        wcEmail.EnableDivs();
        wcEndereco.EnableDivs();
        wcHierarquias.GetHierarquias(pessoaJuridica.Pessoa, TipoPessoa.Juridica);
        wcPessoaFisica.GetPessoasFisicas((int)idPessoaJuridica);
        wcCampanhas.Load(pessoaJuridica.Pessoa.IDPessoa, null);
        wcDePara.LoadPessoaJuridicaDePara(pessoaJuridica);

        txtRazaoSocial.Text = pessoaJuridica.RazaoSocial;
        txtInscricaoEstadual.Text = pessoaJuridica.InscricaoEstadual;
        txtCNPJ.Text = pessoaJuridica.CNPJ;
        txtTVI.Text = pessoaJuridica.Pessoa.TVI;
        txtDataFundacao.Text = pessoaJuridica.DataFundacaoFormatada;

        if (pessoaJuridica.ExistePessoa())
        {
            var email = pessoaJuridica.Pessoa.Email;
            txtTVI.Text = pessoaJuridica.Pessoa.TVI;
            wcTelefone.GetTelefone(pessoaJuridica.Pessoa);
            wcEmail.GetEmail(pessoaJuridica.Pessoa);
            wcEndereco.GetEndereco(pessoaJuridica.Pessoa);
            wcTabelaDinamica.GetTabelaDinamica(pessoaJuridica.Pessoa);
        }
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetPessoaJuridicas();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        clearFields(); 
        dvSalvarPessoaJuridica.Visible = true;
        dvListarPessoaJuridicas.Visible = false;
    }
    
    private void clearFields()
    {
        clearFields(null);
    }

    private void clearFields(int? IDPessoaJuridica)
    {
        dvSalvarPessoaJuridica.Visible = false;
        wcTelefone.ClearData();
        wcEmail.ClearData();
        wcEndereco.ClearData();

        if (IDPessoaJuridica != null)
            wcTabelaDinamica.ClearData(IDPessoaJuridica);

        txtId.Text = string.Empty;
        txtRazaoSocial.Text = string.Empty;
        txtInscricaoEstadual.Text = string.Empty;
        txtCNPJ.Text = string.Empty;
        txtDataFundacao.Text = string.Empty;
        txtTVI.Text = string.Empty;
    }

    protected void gvPessoaJuridicas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetPessoaJuridica(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvPessoaJuridicas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPessoaJuridicas.PageIndex = e.NewPageIndex;
        GetPessoaJuridicas();
    }
}
