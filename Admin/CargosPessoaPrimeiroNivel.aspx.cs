using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using System.Web.UI.MobileControls;
using System.Web.UI.HtmlControls;
using TradeVision.UI;

public partial class _CargosPessoaPrimeiroNivel : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCargo.DataSource = new Cargo().GetPrimeiroNoCargo();
            ddlCargo.DataValueField = "IDCargo";
            ddlCargo.DataTextField = "Descricao";
            ddlCargo.DataBind();

            loadPessoasFisicas();
        }
    }

    private void loadPessoasFisicas()
    {
        findTop();
        loadCargoEstrutura();
    }

    private void loadCargoEstrutura()
    {
        var pessoasFisicasAdded = new Pessoa().FindAllPessoaPrimeiroNivel(new Cargo(int.Parse(ddlCargo.SelectedValue)));
        listPessoasAdd.DataSource = pessoasFisicasAdded;
        listPessoasAdd.DataTextField = "Nome";
        listPessoasAdd.DataValueField = "IDPessoa";
        listPessoasAdd.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        var list = new List<ListItem>();
        foreach (ListItem item in listPessoas.Items)
        {
            if (item.Selected)
            {
                listPessoasAdd.Items.Add(item);
                list.Add(item);
            }
        }

        foreach (ListItem item in list) listPessoas.Items.Remove(item);
        savePessoasFisica();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        var list = new List<ListItem>();
        foreach (ListItem item in listPessoasAdd.Items)
        {
            if (item.Selected)
            {
                listPessoas.Items.Add(item);
                list.Add(item);
            }
        }

        foreach (ListItem item in list) listPessoasAdd.Items.Remove(item);
        savePessoasFisica();
    }

    private void savePessoasFisica()
    {
        var estrutura = new CargoEstrutura();
        try
        {
            estrutura.IsTransaction = true;
            var idsPessoaNew = new List<int>();
            foreach (ListItem item in listPessoasAdd.Items)
                idsPessoaNew.Add(int.Parse(item.Value));
            estrutura.SaveEstruturaPrimeiroNivel(idsPessoaNew, int.Parse(ddlCargo.SelectedValue));
            estrutura.Commit();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
            estrutura.Rollback();
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        findTop();
    }

    private void findTop()
    {
        listPessoas.DataSource = new Pessoa().FindTop(txtData.Text, TipoPessoa.Fisica);
        listPessoas.DataTextField = "Nome";
        listPessoas.DataValueField = "IDPessoa";
        listPessoas.DataBind();
    }

    protected void btnVoltarHierarquias_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CargosPessoa.aspx");
    }

    protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPessoasFisicas();
    }
}



