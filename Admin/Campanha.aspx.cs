using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _Campanha : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlIDPrograma.DataSource = new Programa().Find();
            ddlIDPrograma.DataTextField = "Descricao";
            ddlIDPrograma.DataValueField = "IDPrograma";
            ddlIDPrograma.DataBind();

            if (Request["idCampanha"] != null)
            {                
                GetCampanha(int.Parse(Request["idCampanha"]));
                return;
            }

            GetCampanhas();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Campanha = new Campanha();
        try
        {
            if (txtId.Text != "")
            {
                Campanha.IDCampanha = int.Parse(txtId.Text);
                Campanha.Get();
            }

            Campanha.Descricao = txtDescricao.Text;
            Campanha.IDPrograma = int.Parse(ddlIDPrograma.SelectedValue);
            Campanha.Slug = txtSlug.Text;
            Campanha.DataInicioFormatada =  txtDataInicio.Text;
            Campanha.DataFimFormatada = txtDataFim.Text;

            Campanha.Save();
            GetCampanha((int)Campanha.IDCampanha);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetCampanhas()
    {
        dvSalvarCampanha.Visible = false;
        dvListarCampanhas.Visible = true;

        gvCampanhas.DataSource = new Campanha().Find();
        gvCampanhas.DataBind();
    }

    private void GetCampanha(int idCampanha)
    {
        dvSalvarCampanha.Visible = true;
        dvListarCampanhas.Visible = false;

        var Campanha = new Campanha();
        Campanha.IDCampanha = idCampanha;
        Campanha.Get();

        txtId.Text = Campanha.IDCampanha.ToString();
        txtDescricao.Text = Campanha.Descricao.ToString();
        ddlIDPrograma.SelectedValue = Campanha.IDPrograma.ToString();
        txtSlug.Text = Campanha.Slug.ToString();
        txtDataInicio.Text = Campanha.DataInicioFormatada;
        txtDataFim.Text = Campanha.DataFimFormatada;

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetCampanhas();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarCampanha.Visible = true;
        dvListarCampanhas.Visible = false;

        txtId.Text = "";
        txtDescricao.Text = string.Empty;
        ddlIDPrograma.SelectedIndex = 0;
        txtSlug.Text = string.Empty;
        txtDataInicio.Text = string.Empty;
        txtDataFim.Text = string.Empty;

    }

    protected void DeleteCampanha(int idCampanha)
    {
        try
        {
            var Campanha = new Campanha();
            Campanha.IDCampanha = idCampanha;
            Campanha.Delete();
            GetCampanhas();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvCampanhas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetCampanha(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeleteCampanha(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvCampanhas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCampanhas.PageIndex = e.NewPageIndex;
        GetCampanhas();
    }
}