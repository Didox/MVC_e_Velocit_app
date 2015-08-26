using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _Tipo : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idTipo"] != null)
            {
                GetTipo(int.Parse(Request["idTipo"]));
                return;
            }

            GetTipos();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Tipo = new Tipo();
        try
        {
            if (txtId.Text != "")
            {
                Tipo.IDTipo = int.Parse(txtId.Text);
                Tipo.Get();
            }

            Tipo.Nome = txtNome.Text;
Tipo.IDTipoTabelaColuna = int.Parse(txtIDTipoTabelaColuna.Text);

            Tipo.Save();
            GetTipo((int)Tipo.IDTipo);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetTipos()
    {
        dvSalvarTipo.Visible = false;
        dvListarTipos.Visible = true;
        
        gvTipos.DataSource = new Tipo().Find();
        gvTipos.DataBind();
    }

    private void GetTipo(int idTipo)
    {
        dvSalvarTipo.Visible = true;
        dvListarTipos.Visible = false;

        var Tipo = new Tipo();
        Tipo.IDTipo = idTipo;
        Tipo.Get();

        txtId.Text = Tipo.IDTipo.ToString();
        txtNome.Text = Tipo.Nome.ToString();
txtIDTipoTabelaColuna.Text = Tipo.IDTipoTabelaColuna.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetTipos();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarTipo.Visible = true;
        dvListarTipos.Visible = false;

        txtId.Text = "";
        txtNome.Text = string.Empty;
txtIDTipoTabelaColuna.Text = string.Empty;

    }

	protected void DeleteTipo(int idTipo)
    {
        try
        {
			var Tipo = new Tipo();
			Tipo.IDTipo = idTipo;
			Tipo.Delete();
			GetTipos();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }
    
    protected void gvTipos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetTipo(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
			DeleteTipo(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvTipos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTipos.PageIndex = e.NewPageIndex;
        GetTipos();
    }
}
