using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using TradeVision.UI;

public partial class _Componente : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idComponente"] != null)
            {
                GetComponente(int.Parse(Request["idComponente"]));
                return;
            }
            GetComponentes();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var componente = new Componente();
        try
        {
            if (txtId.Text != "")
            {
                componente.IDComponente = int.Parse(txtId.Text);
                componente.Get();
            }

            componente.Descricao = txtNome.Text;
            componente.Chave = txtChave.Text;
            componente.Conteudo = txtConteudo.Text;
            componente.Save();

            GetComponente((int)componente.IDComponente);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetComponentes();
    }

    private void GetComponentes()
    {
        dvSalvarComponente.Visible = false;
        dvListarComponentes.Visible = true;
        gvComponentes.DataSource = new Componente(txtNomeComponente.Text).Find();
        gvComponentes.DataBind();
    }

    private void GetComponente(int idComponente)
    {
        dvSalvarComponente.Visible = true;
        dvListarComponentes.Visible = false;

        var componente = new Componente();
        componente.IDComponente = idComponente;
        componente.Get();

        txtId.Text = componente.IDComponente.ToString();
        txtNome.Text = componente.Descricao;
        txtConteudo.Text = componente.Conteudo;
        txtChave.Text = componente.Chave;
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetComponentes();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarComponente.Visible = true;
        dvListarComponentes.Visible = false;

        txtId.Text = "";
        txtNome.Text = "";
        txtConteudo.Text = "";
        txtChave.Text = "";
    }

    protected void gvComponentes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            GetComponente(int.Parse(e.CommandArgument.ToString()));
        }
    }
}
