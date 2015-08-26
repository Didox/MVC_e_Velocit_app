using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _PessoaCampanha : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idPessoaCampanha"] != null)
            {
                GetPessoaCampanha(int.Parse(Request["idPessoaCampanha"]));
                return;
            }

            GetPessoaCampanhas();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var PessoaCampanha = new PessoaCampanha();
        try
        {
            if (txtId.Text != "")
            {
                PessoaCampanha.IDPessoaCampanha = int.Parse(txtId.Text);
                PessoaCampanha.Get();
            }

            PessoaCampanha.IDCampanha = int.Parse(txtIDCampanha.Text);
            PessoaCampanha.IDPessoa = int.Parse(txtIDPessoa.Text);
            PessoaCampanha.DataAdesao = DateTime.Parse(txtDataAdesao.Text);
            PessoaCampanha.DataExclusao = DateTime.Parse(txtDataExclusao.Text);
            PessoaCampanha.IDUsuario = int.Parse(txtIDUsuario.Text);

            PessoaCampanha.Save();
            GetPessoaCampanha((int)PessoaCampanha.IDPessoaCampanha);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
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

        gvPessoaCampanhas.DataSource = new PessoaCampanha().Find();
        gvPessoaCampanhas.DataBind();
    }

    private void GetPessoaCampanha(int idPessoaCampanha)
    {
        dvSalvarPessoaCampanha.Visible = true;
        dvListarPessoaCampanhas.Visible = false;

        var PessoaCampanha = new PessoaCampanha();
        PessoaCampanha.IDPessoaCampanha = idPessoaCampanha;
        PessoaCampanha.Get();

        txtId.Text = PessoaCampanha.IDPessoaCampanha.ToString();
        txtIDCampanha.Text = PessoaCampanha.IDCampanha.ToString();
        txtIDPessoa.Text = PessoaCampanha.IDPessoa.ToString();
        txtDataAdesao.Text = PessoaCampanha.DataAdesao.ToString();
        txtDataExclusao.Text = PessoaCampanha.DataExclusao.ToString();
        txtIDUsuario.Text = PessoaCampanha.IDUsuario.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetPessoaCampanhas();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarPessoaCampanha.Visible = true;
        dvListarPessoaCampanhas.Visible = false;

        txtId.Text = "";
        txtIDCampanha.Text = string.Empty;
        txtIDPessoa.Text = string.Empty;
        txtDataAdesao.Text = string.Empty;
        txtDataExclusao.Text = string.Empty;
        txtIDUsuario.Text = string.Empty;
    }

    protected void DeletePessoaCampanha(int idPessoaCampanha)
    {
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
        if (e.CommandName == "Editar")
            GetPessoaCampanha(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeletePessoaCampanha(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvPessoaCampanhas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPessoaCampanhas.PageIndex = e.NewPageIndex;
        GetPessoaCampanhas();
    }
}