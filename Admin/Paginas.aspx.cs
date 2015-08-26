using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using TradeVision.UI;

public partial class _Paginas : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetPaginas();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var pagina = new Pagina();
        pagina.IsTransaction = true;
        try
        {
            if (txtId.Text != "")
            {
                pagina.IDPagina = int.Parse(txtId.Text);
                pagina.Get();
            }

            pagina.Nome = txtNome.Text;
            pagina.Descricao = txtDescricao.Text;
            pagina.Slug = txtSlug.Text;
            if (ddlPagina.SelectedValue != "0")
                pagina.IDPaginaPai = int.Parse(ddlPagina.SelectedValue);
            pagina.IDTemplate = int.Parse(ddlTemplate.SelectedValue);
            pagina.Save();

            var relacionaPagina = new RelacionaPagina();
            relacionaPagina.Transaction = pagina.Transaction;
            relacionaPagina.Pagina = pagina;
            relacionaPagina.Cliente = Cliente.Current();
            relacionaPagina.Programa = Programa.Current();
            relacionaPagina.Campanha = Campanha.Current();
            relacionaPagina.Get();
            relacionaPagina.Restrito = chkRestrito.Checked;
            relacionaPagina.Interna = chkInterna.Checked;
            relacionaPagina.Ordem = int.Parse(ddlOrdem.SelectedValue);            
            relacionaPagina.Save();

            pagina.Commit();

            GetPagina((int)pagina.IDPagina);
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            pagina.Rollback();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetPaginas();
    }

    private void GetPaginas()
    {
        dvSalvarPagina.Visible = false;
        dvListarPaginas.Visible = true;
        gvPaginas.DataSource = Pagina.GetPaginas(txtNomePagina.Text, chkRestritoPagina.Checked);
        gvPaginas.DataBind();
    }

    private void GetPagina(int idPagina)
    {        
        dvSalvarPagina.Visible = true;
        dvListarPaginas.Visible = false;
        btnEditarTemplate.Visible = true;
        btnNovoTemplate.Visible = false;

        var pagina = new Pagina();
        pagina.IDPagina = idPagina;
        pagina.Get();
        if (pagina.IDPagina == null)
        {
            GetPaginas();
            return;
        }

        txtId.Text = pagina.IDPagina.ToString();

        LoadCombos();

        txtNome.Text = pagina.Nome;
        txtDescricao.Text = pagina.Descricao;
        ddlTemplate.SelectedValue = pagina.IDTemplate.ToString();
        txtSlug.Text = pagina.Slug;


        if (ddlPagina.Items.FindByValue(pagina.IDPaginaPai.ToString()) != null)
            ddlPagina.SelectedValue = pagina.IDPaginaPai.ToString();

        var relacionaPagina = new RelacionaPagina();
        relacionaPagina.Pagina = pagina;
        relacionaPagina.Cliente = Cliente.Current();
        relacionaPagina.Programa = Programa.Current();
        relacionaPagina.Campanha = Campanha.Current();
        relacionaPagina.Get();

        chkRestrito.Checked = (bool)relacionaPagina.Restrito;
        chkInterna.Checked = (bool)relacionaPagina.Interna;

        if(ddlOrdem.Items.FindByValue(relacionaPagina.Ordem.ToString()) != null)
            ddlOrdem.SelectedValue = relacionaPagina.Ordem.ToString();

    }
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetPaginas();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        IncluirPagina(false);
    }

    private void IncluirPagina(bool restrito)
    {
        dvSalvarPagina.Visible = true;
        dvListarPaginas.Visible = false;
        btnEditarTemplate.Visible = false;
        btnNovoTemplate.Visible = true;

        chkRestrito.Checked = restrito;
        chkInterna.Checked = false;
        txtId.Text = "";
        txtNome.Text = "";
        txtDescricao.Text = "";
        ddlTemplate.SelectedValue = "0";
        txtSlug.Text = "";

        LoadCombos();
    }

    private void LoadCombos()
    {
        var templates = new Template().Find();
        ddlTemplate.Items.Clear();
        foreach (var template in templates)
        {
            var cTemplate = (Template)template;
            ddlTemplate.Items.Add(new ListItem(cTemplate.Descricao, cTemplate.IDTemplate.ToString()));
        }
        ddlTemplate.Items.Insert(0, new ListItem("[Selecione]", "0"));

        int qtdPaginas = Pagina.GetQuantidadePaginas(chkRestrito.Checked);
        ddlOrdem.Items.Clear();
        for (int i = 0; i <= (qtdPaginas + 1); i++)
            ddlOrdem.Items.Add(new ListItem(i.ToString(), i.ToString()));

        int? idPagina = null;
        if(txtId.Text != string.Empty ) idPagina = int.Parse(txtId.Text);
        ddlPagina.DataSource = new Pagina(idPagina).GetPaginasPai();
        ddlPagina.DataBind();
        ddlPagina.Items.Insert(0, new ListItem("[menu]", "0"));
    }

    protected void gvPaginas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            GetPagina(int.Parse(e.CommandArgument.ToString()));
        }
    }

    protected void btnNovoTemplate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Template.aspx");
    }

    protected void btnEditarTemplate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Template.aspx?idTemplate=" + ddlTemplate.SelectedValue);
    }

    protected void btnIncluirRestrito_Click(object sender, EventArgs e)
    {
        IncluirPagina(true);
    }
}
