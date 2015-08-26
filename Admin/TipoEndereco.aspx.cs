using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _TipoEndereco : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idTipoEndereco"] != null)
            {
                GetTipoEndereco(int.Parse(Request["idTipoEndereco"]));
                return;
            }

            GetTipoEnderecos();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var TipoEndereco = new TipoEndereco();
        try
        {
            if (txtId.Text != "")
            {
                TipoEndereco.IDTipoEndereco = int.Parse(txtId.Text);
                TipoEndereco.Get();
            }

            TipoEndereco.Nome = txtNome.Text;

            TipoEndereco.Save();
            GetTipoEnderecos();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetTipoEnderecos()
    {
        dvSalvarTipoEndereco.Visible = false;
        dvListarTipoEnderecos.Visible = true;

        gvTipoEnderecos.DataSource = new TipoEndereco().Find();
        gvTipoEnderecos.DataBind();
    }

    private void GetTipoEndereco(int idTipoEndereco)
    {
        dvSalvarTipoEndereco.Visible = true;
        dvListarTipoEnderecos.Visible = false;

        var TipoEndereco = new TipoEndereco();
        TipoEndereco.IDTipoEndereco = idTipoEndereco;
        TipoEndereco.Get();

        txtId.Text = TipoEndereco.IDTipoEndereco.ToString();
        txtNome.Text = TipoEndereco.Nome.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetTipoEnderecos();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarTipoEndereco.Visible = true;
        dvListarTipoEnderecos.Visible = false;

        txtId.Text = "";
        txtNome.Text = string.Empty;

    }

    protected void DeleteTipoEndereco(int idTipoEndereco)
    {
        try
        {
            var TipoEndereco = new TipoEndereco();
            TipoEndereco.IDTipoEndereco = idTipoEndereco;
            TipoEndereco.Delete();
            GetTipoEnderecos();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvTipoEnderecos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetTipoEndereco(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeleteTipoEndereco(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvTipoEnderecos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTipoEnderecos.PageIndex = e.NewPageIndex;
        GetTipoEnderecos();
    }
}