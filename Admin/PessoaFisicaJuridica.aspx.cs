using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _PessoaFisicaJuridica : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idPessoaFisicaJuridica"] != null)
            {
                GetPessoaFisicaJuridica(int.Parse(Request["idPessoaFisicaJuridica"]));
                return;
            }

            GetPessoaFisicaJuridicas();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var PessoaFisicaJuridica = new PessoaFisicaJuridica();
        try
        {
            if (txtId.Text != "")
            {
                PessoaFisicaJuridica.IDPessoaFisicaJuridica = int.Parse(txtId.Text);
                PessoaFisicaJuridica.Get();
            }

            PessoaFisicaJuridica.IDPessoaFisica = int.Parse(txtIDPessoaFisica.Text);
PessoaFisicaJuridica.IDPessoaJuridica = int.Parse(txtIDPessoaJuridica.Text);

            PessoaFisicaJuridica.Save();
            GetPessoaFisicaJuridica((int)PessoaFisicaJuridica.IDPessoaFisicaJuridica);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetPessoaFisicaJuridicas()
    {
        dvSalvarPessoaFisicaJuridica.Visible = false;
        dvListarPessoaFisicaJuridicas.Visible = true;
        
        gvPessoaFisicaJuridicas.DataSource = new PessoaFisicaJuridica().Find();
        gvPessoaFisicaJuridicas.DataBind();
    }

    private void GetPessoaFisicaJuridica(int idPessoaFisicaJuridica)
    {
        dvSalvarPessoaFisicaJuridica.Visible = true;
        dvListarPessoaFisicaJuridicas.Visible = false;

        var PessoaFisicaJuridica = new PessoaFisicaJuridica();
        PessoaFisicaJuridica.IDPessoaFisicaJuridica = idPessoaFisicaJuridica;
        PessoaFisicaJuridica.Get();

        txtId.Text = PessoaFisicaJuridica.IDPessoaFisicaJuridica.ToString();
        txtIDPessoaFisica.Text = PessoaFisicaJuridica.IDPessoaFisica.ToString();
txtIDPessoaJuridica.Text = PessoaFisicaJuridica.IDPessoaJuridica.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetPessoaFisicaJuridicas();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarPessoaFisicaJuridica.Visible = true;
        dvListarPessoaFisicaJuridicas.Visible = false;

        txtId.Text = "";
        txtIDPessoaFisica.Text = string.Empty;
txtIDPessoaJuridica.Text = string.Empty;

    }

	protected void DeletePessoaFisicaJuridica(int idPessoaFisicaJuridica)
    {
        try
        {
			var PessoaFisicaJuridica = new PessoaFisicaJuridica();
			PessoaFisicaJuridica.IDPessoaFisicaJuridica = idPessoaFisicaJuridica;
			PessoaFisicaJuridica.Delete();
			GetPessoaFisicaJuridicas();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }
    
    protected void gvPessoaFisicaJuridicas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetPessoaFisicaJuridica(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
			DeletePessoaFisicaJuridica(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvPessoaFisicaJuridicas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPessoaFisicaJuridicas.PageIndex = e.NewPageIndex;
        GetPessoaFisicaJuridicas();
    }
}
