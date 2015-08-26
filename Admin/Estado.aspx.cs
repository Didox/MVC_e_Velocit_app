using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _Estado : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idEstado"] != null)
            {
                GetEstado(int.Parse(Request["idEstado"]));
                return;
            }

            GetEstados();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Estado = new Estado();
        try
        {
            if (txtId.Text != "")
            {
                Estado.IDEstado = int.Parse(txtId.Text);
                Estado.Get();
            }

            Estado.Nome = txtNome.Text;

            Estado.Save();
            GetEstado((int)Estado.IDEstado);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetEstados()
    {
        dvSalvarEstado.Visible = false;
        dvListarEstados.Visible = true;
        
        gvEstados.DataSource = new Estado().Find();
        gvEstados.DataBind();
    }

    private void GetEstado(int idEstado)
    {
        dvSalvarEstado.Visible = true;
        dvListarEstados.Visible = false;

        var Estado = new Estado();
        Estado.IDEstado = idEstado;
        Estado.Get();

        txtId.Text = Estado.IDEstado.ToString();
        txtNome.Text = Estado.Nome.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetEstados();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarEstado.Visible = true;
        dvListarEstados.Visible = false;

        txtId.Text = "";
        txtNome.Text = string.Empty;

    }

	protected void DeleteEstado(int idEstado)
    {
        try
        {
			var Estado = new Estado();
			Estado.IDEstado = idEstado;
			Estado.Delete();
			GetEstados();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }
    
    protected void gvEstados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetEstado(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
			DeleteEstado(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvEstados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEstados.PageIndex = e.NewPageIndex;
        GetEstados();
    }
}
