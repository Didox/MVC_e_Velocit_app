using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _Pais : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idPais"] != null)
            {
                GetPais(int.Parse(Request["idPais"]));
                return;
            }

            GetPaiss();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Pais = new Pais();
        try
        {
            if (txtId.Text != "")
            {
                Pais.IDPais = int.Parse(txtId.Text);
                Pais.Get();
            }

            Pais.Nome = txtNome.Text;

            Pais.Save();
            GetPais((int)Pais.IDPais);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetPaiss()
    {
        dvSalvarPais.Visible = false;
        dvListarPaiss.Visible = true;

        gvPaiss.DataSource = new Pais().Find();
        gvPaiss.DataBind();
    }

    private void GetPais(int idPais)
    {
        dvSalvarPais.Visible = true;
        dvListarPaiss.Visible = false;

        var Pais = new Pais();
        Pais.IDPais = idPais;
        Pais.Get();

        txtId.Text = Pais.IDPais.ToString();
        txtNome.Text = Pais.Nome.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetPaiss();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarPais.Visible = true;
        dvListarPaiss.Visible = false;

        txtId.Text = "";
        txtNome.Text = string.Empty;

    }

    protected void DeletePais(int idPais)
    {
        try
        {
            var Pais = new Pais();
            Pais.IDPais = idPais;
            Pais.Delete();
            GetPaiss();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvPaiss_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetPais(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeletePais(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvPaiss_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPaiss.PageIndex = e.NewPageIndex;
        GetPaiss();
    }
}