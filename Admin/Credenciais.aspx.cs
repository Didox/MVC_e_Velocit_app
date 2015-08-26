using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using System.Web.UI.MobileControls;
using TradeVision.UI;

public partial class _Credenciais : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtId.Text))
        {
            int idUsuario = int.Parse(txtId.Text);
            wcTelefone.SetIdUsuario(idUsuario);
            wcEndereco.SetIdUsuario(idUsuario);
            wcTabelaDinamica.SetIdUsuario(idUsuario);
        }

        if (!IsPostBack)
        {
            if (Request["idUsuario"] != null)
            {
                GetUsuario(int.Parse(Request["idUsuario"]));
                if (!string.IsNullOrEmpty(Request["tab"]))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script" + Funcoes.KeyStript(), "<script>$('#" + Request["tab"] + "').trigger(\"click\");</script>");
                return;
            }
            GetUsuarios();            
        }
    }

    protected void btnSalvarCredencial_Click(object sender, EventArgs e)
    {
        var usuario = new Usuario();
        try
        {
            if (txtId.Text != "")
            {
                usuario.IDUsuario = int.Parse(txtId.Text);
                usuario.Get();
            }

            usuario.Nome = txtNomeCompleto.Text;
            usuario.Login = txtLogin.Text;
            if (!string.IsNullOrEmpty(txtSenha.Text)) usuario.Senha = txtSenha.Text;
            usuario.Email = txtEmail.Text;
            usuario.Ramal = txtRamal.Text;
            usuario.Ativo = bool.Parse(rdoAtivo.SelectedValue);
            usuario.Save();
            GetUsuario((int)usuario.IDUsuario);

            var fisica = usuario.Pessoa.Fisica;
            if (fisica == null || fisica.IDPessoaFisica == null) 
                salvarPessoaFisica();
            else Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aCredencial').trigger(\"click\");alert('Credencial salva.');</script>");
        }
        catch (Exception err)
        {
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aCredencial').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");            
        }
    }

    protected void btnSalvarPessoaFisica_Click(object sender, EventArgs e)
    {
        salvarPessoaFisica();
    }

    private void salvarPessoaFisica()
    {
        var usuario = new Usuario();
        try
        {
            usuario.IDUsuario = int.Parse(txtId.Text);
            usuario.Get();
            savePessoaFisica(usuario);
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aPessoaFisica').trigger(\"click\");alert('Dados pessoa fisica salva.');</script>");
        }
        catch (Exception err)
        {
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aPessoaFisica').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }

    protected void btnSalvarTelefones_Click(object sender, EventArgs e)
    {
        var usuario = new Usuario();
        try
        {
            usuario.IDUsuario = int.Parse(txtId.Text);
            usuario.Get();
            
            wcTelefone.SavePessoaTelefone(usuario.Pessoa);
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aTelefones').trigger(\"click\");alert('Telefones salvo.');</script>");
        }
        catch (Exception err)
        {
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aTelefones').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }

    protected void btnSalvarEnderecos_Click(object sender, EventArgs e)
    {
        var usuario = new Usuario();
        try
        {
            usuario.IDUsuario = int.Parse(txtId.Text);
            usuario.Get();

            wcEndereco.SavePessoaEndereco(usuario.Pessoa);
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aEnderecos').trigger(\"click\");alert('Endereços salvo.');</script>");
        }
        catch (Exception err)
        {
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aEnderecos').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }

    protected void btnSalvarTabelaDinamica_Click(object sender, EventArgs e)
    {
        var usuario = new Usuario();
        try
        {
            usuario.IDUsuario = int.Parse(txtId.Text);
            usuario.Get();

            wcTabelaDinamica.SaveTabelaDinamica(usuario.Pessoa);
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aDadosAdicionais').trigger(\"click\");alert('Dados adicionais salvo.');</script>");
        }
        catch (Exception err)
        {
            GetUsuario((int)usuario.IDUsuario);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aDadosAdicionais').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }

    private void savePessoaFisica(Usuario usuario)
    {
        if (usuario.ExistePessoa())
        {
            var fisica = usuario.Pessoa.Fisica;
            fisica.CPF = txtCpf.Text;
            fisica.RG = txtRg.Text;
            fisica.Sexo = (Sexo)Enum.Parse(typeof(Sexo), rdoSexo.SelectedValue);
            fisica.EstadoCivil = (EstadoCivil)Enum.Parse(typeof(EstadoCivil), ddlEstadoCivil.SelectedValue);
            fisica.RG = txtRg.Text;
            fisica.DataNascimentoFormatada = txtDataNascimento.Text;
            fisica.Save();
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetUsuarios();
    }

    private void GetUsuarios()
    {
        dvSalvarUsuario.Visible = false;
        dvListarUsuarios.Visible = true;

        gvUsuarios.DataSource = new Usuario(txtNomeCompletoBusca.Text).Find();
        gvUsuarios.DataBind();
    }

    private void GetUsuario(int idUsuario)
    {
        var usuario = new Usuario(idUsuario);
        usuario.Get();

        LimparCampos(idUsuario);
        dvSalvarUsuario.Visible = true;
        dvSalvarPessoaFisica.Visible = true;
        wcTelefone.EnableDivs();
        wcEndereco.EnableDivs();
        dvListarUsuarios.Visible = false;

        txtId.Text = usuario.IDUsuario.ToString();
        txtNomeCompleto.Text = usuario.Nome;
        txtLogin.Text = usuario.Login;
        txtSenha.Text = usuario.Senha;
        txtEmail.Text = usuario.Email;
        txtRamal.Text = usuario.Ramal;
        rdoAtivo.SelectedValue = usuario.Ativo.ToString().ToLower();

        if (usuario.ExistePessoa())
        {
            wcCargos.GetCargos(idUsuario);
            wcHierarquias.GetHierarquias(usuario.Pessoa, TipoPessoa.Fisica);

            wcPessoaJuridica.GetPessoasJuridicas(idUsuario);
            wcCampanhas.Load(usuario.Pessoa.IDPessoa, idUsuario);

            txtTVI.Text = usuario.Pessoa.TVI;

            if (usuario.Pessoa.ExisteFisica())
            {
                txtCpf.Text = usuario.Pessoa.Fisica.CPF;
                txtRg.Text = usuario.Pessoa.Fisica.RG;
                txtDataNascimento.Text = usuario.Pessoa.Fisica.DataNascimentoFormatada;
                ddlEstadoCivil.SelectedValue = ((int)usuario.Pessoa.Fisica.EstadoCivil).ToString();
                rdoSexo.SelectedValue = ((int)usuario.Pessoa.Fisica.Sexo).ToString();
            }

            wcTelefone.GetTelefone(usuario.Pessoa);
            wcEndereco.GetEndereco(usuario.Pessoa);
            wcTabelaDinamica.GetTabelaDinamica(usuario.Pessoa);
        }
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetUsuarios();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        LimparCampos();
        dvSalvarUsuario.Visible = true;
        dvListarUsuarios.Visible = false;
    }

    private void LimparCampos()
    {
        LimparCampos(null);
    }

    private void LimparCampos(int? idUsuario)
    {
        dvSalvarUsuario.Visible = false;
        dvSalvarPessoaFisica.Visible = false;
        wcTelefone.DisableDivs();
        wcEndereco.DisableDivs();
        wcTabelaDinamica.DisableDivs();

        txtId.Text = string.Empty;
        txtNomeCompleto.Text = string.Empty;
        txtLogin.Text = string.Empty;
        txtSenha.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtRamal.Text = string.Empty;
        rdoAtivo.SelectedValue = "true";
        txtCpf.Text = string.Empty;
        txtRg.Text = string.Empty;
        txtTVI.Text = string.Empty;
        txtDataNascimento.Text = string.Empty;
        ddlEstadoCivil.SelectedIndex = 0;
        rdoSexo.SelectedIndex = 0;

        wcTelefone.ClearData();
        wcEndereco.ClearData();

        if (idUsuario != null)
            wcTabelaDinamica.ClearData(idUsuario);
    }

    protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetUsuario(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsuarios.PageIndex = e.NewPageIndex;
        GetUsuarios();
    }
}
