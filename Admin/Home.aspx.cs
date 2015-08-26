using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using System.Web.UI.HtmlControls;
using TradeVision.UI;

public partial class _Home : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Cliente.Current() == null) LoadSelects();
            else QueDeseja();
        }       
    }

    private void LoadSelects()
    {
        ddlClientes.DataSource = new Cliente().Find();
        ddlClientes.DataBind();
        ddlClientes.Items.Insert(0, new ListItem("[Selecione]", "0"));

        ddlCampanhas.Items.Insert(0, new ListItem("[Selecione]", "0"));
        ddlProgramas.Items.Insert(0, new ListItem("[Selecione]", "0"));
    }

    private void QueDeseja()
    {
        dvSeleciona.Visible = false;
        dvQueDeseja.Visible = true;
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        if (ddlClientes.SelectedValue == "0")
            Response.Write("Selecione o cliente");

        var cliente = new Cliente(int.Parse(ddlClientes.SelectedValue));
        cliente.BuscaAdicionaCurrent();

        if (ddlProgramas.SelectedValue != "0")
        {
            var programa = new Programa(int.Parse(ddlProgramas.SelectedValue));
            programa.BuscaAdicionaCurrent();
        }

        if (ddlCampanhas.SelectedValue != "0")
        {
            var campanha = new Campanha(int.Parse(ddlCampanhas.SelectedValue));
            campanha.BuscaAdicionaCurrent();
        }

        Response.Redirect("~/");
    }

    protected void ddlProgramas_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgramas.SelectedValue == "0")
        {
            dvCampanha.Visible = false;
            return;
        }

        dvCampanha.Visible = true;
        ddlCampanhas.DataSource = new Campanha(new Programa(int.Parse(ddlProgramas.SelectedValue))).Find();
        ddlCampanhas.DataBind();
        ddlCampanhas.Items.Insert(0, new ListItem("[Selecione]", "0"));
    }

    protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClientes.SelectedValue == "0")
        {
            dvCampanha.Visible = false;
            dvProduto.Visible = false;
            return;
        }

        dvProduto.Visible = true;
        ddlProgramas.DataSource = new Programa(new Cliente(int.Parse(ddlClientes.SelectedValue))).Find();
        ddlProgramas.DataBind();
        ddlProgramas.Items.Insert(0, new ListItem("[Selecione]", "0"));
    }
}
