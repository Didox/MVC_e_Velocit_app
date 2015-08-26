using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _TipoDocumento : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idTipoDocumento"] != null)
            {
                GetTipoDocumento(int.Parse(Request["idTipoDocumento"]));
                return;
            }

            GetTipoDocumentos();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var TipoDocumento = new TipoDocumento();
        try
        {
            if (txtId.Text != "")
            {
                TipoDocumento.IDTipoDocumento = int.Parse(txtId.Text);
                TipoDocumento.Get();
            }

            TipoDocumento.Nome = txtNome.Text;

            TipoDocumento.Save();
            GetTipoDocumentos();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetTipoDocumentos()
    {
        dvSalvarTipoDocumento.Visible = false;
        dvListarTipoDocumentos.Visible = true;

        gvTipoDocumentos.DataSource = new TipoDocumento().Find();
        gvTipoDocumentos.DataBind();
    }

    private void GetTipoDocumento(int idTipoDocumento)
    {
        dvSalvarTipoDocumento.Visible = true;
        dvListarTipoDocumentos.Visible = false;

        var TipoDocumento = new TipoDocumento();
        TipoDocumento.IDTipoDocumento = idTipoDocumento;
        TipoDocumento.Get();

        txtId.Text = TipoDocumento.IDTipoDocumento.ToString();
        txtNome.Text = TipoDocumento.Nome.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetTipoDocumentos();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarTipoDocumento.Visible = true;
        dvListarTipoDocumentos.Visible = false;

        txtId.Text = "";
        txtNome.Text = string.Empty;

    }

    protected void DeleteTipoDocumento(int idTipoDocumento)
    {
        try
        {
            var TipoDocumento = new TipoDocumento();
            TipoDocumento.IDTipoDocumento = idTipoDocumento;
            TipoDocumento.Delete();
            GetTipoDocumentos();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvTipoDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetTipoDocumento(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeleteTipoDocumento(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvTipoDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTipoDocumentos.PageIndex = e.NewPageIndex;
        GetTipoDocumentos();
    }
}