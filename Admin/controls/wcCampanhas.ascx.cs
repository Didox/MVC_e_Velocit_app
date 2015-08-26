using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;

public partial class controls_wcCampanhas : System.Web.UI.UserControl
{
    private Pessoa pessoa = null;
    private Usuario usuario = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtIdPessoa.Text))
        {
            pessoa = new Pessoa(int.Parse(txtIdPessoa.Text));
            pessoa.Get();
        }

        if (!string.IsNullOrEmpty(txtIdUsuario.Text))
        {
            usuario = new Usuario(int.Parse(txtIdUsuario.Text));
            usuario.Get();
        }
    }

    private void loadTab()
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), Funcoes.KeyStript(), "<script>$('#aCampanhas').trigger(\"click\");</script>");
    }

    public void Load(int? idPessoa, int? idUsuario)
    {
        dvCampanha.Visible = false;
        dvListarPessoaCampanhas.Visible = false;
        dvSalvarPessoaCampanha.Visible = false;

        if (idPessoa == null)
        {
            dvCampanha.InnerHtml = "Pessoa não cadastrada.";
            dvCampanha.Visible = true;
            return;
        }

        pessoa = new Pessoa(idPessoa);
        pessoa.Get();

        if (idUsuario != null)
        {
            usuario = new Usuario(idUsuario);
            usuario.Get();
            txtIdUsuario.Text = usuario.IDUsuario.ToString();
        }

        txtIdPessoa.Text = pessoa.IDPessoa.ToString();
        GetPessoaCampanhas();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        loadTab();
        var pessoaCampanha = new PessoaCampanha();
        try
        {
            if (txtId.Text != "")
            {
                pessoaCampanha.IDPessoaCampanha = int.Parse(txtId.Text);
                pessoaCampanha.Get();
            }

            pessoaCampanha.Campanha = Campanha.Current();
            pessoaCampanha.Pessoa = pessoa;
            pessoaCampanha.Usuario = usuario;
            pessoaCampanha.DataAdesao = DateTime.Parse(txtDataAdesao.Text);
            if (!string.IsNullOrEmpty(txtDataExclusao.Text))
                pessoaCampanha.DataExclusao = DateTime.Parse(txtDataExclusao.Text);
            else pessoaCampanha.DataExclusao = null;

            pessoaCampanha.Save();
            GetPessoaCampanhas();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetPessoaCampanhas()
    {
        dvSalvarPessoaCampanha.Visible = false;
        dvListarPessoaCampanhas.Visible = true;

        gvPessoaCampanhas.DataSource = new PessoaCampanha(pessoa, Campanha.Current()).Find();
        gvPessoaCampanhas.DataBind();
    }

    private void GetPessoaCampanha(int idPessoaCampanha)
    {
        loadTab();
        dvSalvarPessoaCampanha.Visible = true;
        dvListarPessoaCampanhas.Visible = false;

        var PessoaCampanha = new PessoaCampanha();
        PessoaCampanha.IDPessoaCampanha = idPessoaCampanha;
        PessoaCampanha.Get();

        txtId.Text = PessoaCampanha.IDPessoaCampanha.ToString();
        txtDataAdesao.Text = PessoaCampanha.DataAdesaoFormatada.ToString();
        txtDataExclusao.Text = PessoaCampanha.DataExclusaoFormatada.ToString();
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        loadTab();
        GetPessoaCampanhas();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        loadTab();
        dvSalvarPessoaCampanha.Visible = true;
        dvListarPessoaCampanhas.Visible = false;

        txtId.Text = "";
        txtDataAdesao.Text = string.Empty;
        txtDataExclusao.Text = string.Empty;
    }

    protected void DeletePessoaCampanha(int idPessoaCampanha)
    {
        loadTab();
        try
        {
            var PessoaCampanha = new PessoaCampanha();
            PessoaCampanha.IDPessoaCampanha = idPessoaCampanha;
            PessoaCampanha.Delete();
            GetPessoaCampanhas();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvPessoaCampanhas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        loadTab();
        if (e.CommandName == "Editar")
            GetPessoaCampanha(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeletePessoaCampanha(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvPessoaCampanhas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadTab();
        gvPessoaCampanhas.PageIndex = e.NewPageIndex;
        GetPessoaCampanhas();
    }
}
