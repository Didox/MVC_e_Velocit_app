using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _TipoEmail : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idTipoEmail"] != null)
            {
                GetTipoEmail(int.Parse(Request["idTipoEmail"]));
                return;
            }

            GetTipoEmails();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var TipoEmail = new TipoEmail();
        try
        {
            if (txtId.Text != "")
            {
                TipoEmail.IDTipoEmail = int.Parse(txtId.Text);
                TipoEmail.Get();
            }

            TipoEmail.Nome = txtNome.Text;

            TipoEmail.Save();
            GetTipoEmails();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetTipoEmails()
    {
        dvSalvarTipoEmail.Visible = false;
        dvListarTipoEmails.Visible = true;

        gvTipoEmails.DataSource = new TipoEmail().Find();
        gvTipoEmails.DataBind();
    }

    private void GetTipoEmail(int idTipoEmail)
    {
        dvSalvarTipoEmail.Visible = true;
        dvListarTipoEmails.Visible = false;

        var TipoEmail = new TipoEmail();
        TipoEmail.IDTipoEmail = idTipoEmail;
        TipoEmail.Get();

        txtId.Text = TipoEmail.IDTipoEmail.ToString();
        txtNome.Text = TipoEmail.Nome.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetTipoEmails();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarTipoEmail.Visible = true;
        dvListarTipoEmails.Visible = false;

        txtId.Text = "";
        txtNome.Text = string.Empty;

    }

    protected void DeleteTipoEmail(int idTipoEmail)
    {
        try
        {
            var TipoEmail = new TipoEmail();
            TipoEmail.IDTipoEmail = idTipoEmail;
            TipoEmail.Delete();
            GetTipoEmails();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvTipoEmails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetTipoEmail(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeleteTipoEmail(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvTipoEmails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTipoEmails.PageIndex = e.NewPageIndex;
        GetTipoEmails();
    }
}