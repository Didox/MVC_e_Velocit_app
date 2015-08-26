using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _Programa : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlIDCliente.DataSource = new Cliente().Find();
            ddlIDCliente.DataTextField = "Nome";
            ddlIDCliente.DataValueField = "IDCliente";
            ddlIDCliente.DataBind();

            if (Request["idPrograma"] != null)
            {
                GetPrograma(int.Parse(Request["idPrograma"]));
                return;
            }

            GetProgramas();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Programa = new Programa();
        try
        {
            if (txtId.Text != "")
            {
                Programa.IDPrograma = int.Parse(txtId.Text);
                Programa.Get();
            }

            Programa.Descricao = txtDescricao.Text;
            Programa.IDCliente = int.Parse(ddlIDCliente.SelectedValue);
            Programa.Slug = txtSlug.Text;

            Programa.Save();
            GetPrograma((int)Programa.IDPrograma);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetProgramas()
    {
        dvSalvarPrograma.Visible = false;
        dvListarProgramas.Visible = true;

        gvProgramas.DataSource = new Programa().Find();
        gvProgramas.DataBind();
    }

    private void GetPrograma(int idPrograma)
    {
        dvSalvarPrograma.Visible = true;
        dvListarProgramas.Visible = false;

        var Programa = new Programa();
        Programa.IDPrograma = idPrograma;
        Programa.Get();

        txtId.Text = Programa.IDPrograma.ToString();
        txtDescricao.Text = Programa.Descricao.ToString();
        ddlIDCliente.SelectedValue = Programa.IDCliente.ToString();
        txtSlug.Text = Programa.Slug.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetProgramas();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarPrograma.Visible = true;
        dvListarProgramas.Visible = false;

        txtId.Text = "";
        txtDescricao.Text = string.Empty;
        ddlIDCliente.SelectedIndex = 0;
        txtSlug.Text = string.Empty;

    }

    protected void DeletePrograma(int idPrograma)
    {
        try
        {
            var Programa = new Programa();
            Programa.IDPrograma = idPrograma;
            Programa.Delete();
            GetProgramas();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvProgramas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetPrograma(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeletePrograma(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvProgramas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProgramas.PageIndex = e.NewPageIndex;
        GetProgramas();
    }
}