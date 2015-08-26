using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using TradeVision.UI;

public partial class _Template : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idTemplate"] != null)
            {
                GetTemplate(int.Parse(Request["idTemplate"]));
                return;
            }
            GetTemplates();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var template = new Template();
        try
        {
            if (txtId.Text != "")
            {
                template.IDTemplate = int.Parse(txtId.Text);
                template.Get();
            }

            template.Descricao = txtNome.Text;
            template.Chave = txtChave.Text;
            template.Conteudo = txtConteudo.Text;
            template.Save();
            GetTemplate((int)template.IDTemplate);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetTemplates();
    }

    private void GetTemplates()
    {
        dvSalvarTemplate.Visible = false;
        dvListarTemplates.Visible = true;
        gvTemplates.DataSource = new Template(txtNomeTemplate.Text).Find();
        gvTemplates.DataBind();
    }

    private void GetTemplate(int idTemplate)
    {
        dvSalvarTemplate.Visible = true;
        dvListarTemplates.Visible = false;

        var template = new Template();
        template.IDTemplate = idTemplate;
        template.Get();

        txtId.Text = template.IDTemplate.ToString();
        txtNome.Text = template.Descricao;
        txtConteudo.Text = template.Conteudo;
        txtChave.Text = template.Chave;
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetTemplates();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarTemplate.Visible = true;
        dvListarTemplates.Visible = false;

        txtId.Text = "";
        txtNome.Text = "";
        txtConteudo.Text = "";
        txtChave.Text = "";
    }

    protected void gvTemplates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            GetTemplate(int.Parse(e.CommandArgument.ToString()));
        }
    }
}