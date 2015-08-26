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

public partial class _HierarquiaPessoaPrimeiroNivel : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlHierarquia.DataSource = new Hierarquia().GetPrimeiroNoHierarquia();
            ddlHierarquia.DataValueField = "IDHierarquia";
            ddlHierarquia.DataTextField = "Descricao";
            ddlHierarquia.DataBind();

            loadPessoasJuridicas();
        }
    }

    private void loadPessoasJuridicas()
    {
        findTop();
        loadEstrutura();
    }

    private void loadEstrutura()
    {
        var pessoasJuridicasAdded = new Pessoa().FindAllPessoaPrimeiroNivel(new Hierarquia(int.Parse(ddlHierarquia.SelectedValue)));
        listPessoasJuridicasAdd.DataSource = pessoasJuridicasAdded;
        listPessoasJuridicasAdd.DataTextField = "Nome";
        listPessoasJuridicasAdd.DataValueField = "IDPessoa";
        listPessoasJuridicasAdd.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        var list = new List<ListItem>();
        foreach (ListItem item in listPessoasJuridicas.Items)
        {
            if (item.Selected)
            {
                listPessoasJuridicasAdd.Items.Add(item);
                list.Add(item);
            }
        }

        foreach (ListItem item in list) listPessoasJuridicas.Items.Remove(item);
        savePessoasJuridica();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        var list = new List<ListItem>();
        foreach (ListItem item in listPessoasJuridicasAdd.Items)
        {
            if (item.Selected)
            {
                listPessoasJuridicas.Items.Add(item);
                list.Add(item);
            }
        }

        foreach (ListItem item in list) listPessoasJuridicasAdd.Items.Remove(item);
        savePessoasJuridica();
    }

    private void savePessoasJuridica()
    {
        var estrutura = new Estrutura();
        try
        {
            estrutura.IsTransaction = true;
            var idsPessoaNew = new List<int>();
            foreach (ListItem item in listPessoasJuridicasAdd.Items)
                idsPessoaNew.Add(int.Parse(item.Value));
            estrutura.SaveEstruturaPrimeiroNivel(idsPessoaNew, int.Parse(ddlHierarquia.SelectedValue));
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
        listPessoasJuridicas.DataSource = new Pessoa().FindTop(txtData.Text, TipoPessoa.Juridica);
        listPessoasJuridicas.DataTextField = "Nome";
        listPessoasJuridicas.DataValueField = "IDPessoa";
        listPessoasJuridicas.DataBind();
    }

    protected void btnVoltarHierarquias_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HierarquiaPessoa.aspx");
    }

    protected void ddlHierarquia_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPessoasJuridicas();
    }
}