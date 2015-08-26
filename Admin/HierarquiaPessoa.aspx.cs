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

public partial class _HierarquiaPessoa : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            trvPessoas.Nodes.Clear();
            var iPessoas = new Pessoa().GetPrimeirosNosPessoaHierarquia();
            foreach (var p in iPessoas)
            {
                var hierarquia = (Pessoa)p;
                addNode(trvPessoas.Nodes, hierarquia);
            }

            if (Request["idPessoa"] == null)
            {
                if (trvPessoas.Nodes.Count > 0)
                    addPessoas(int.Parse(trvPessoas.Nodes[0].Value));
            }
            else addPessoas(int.Parse(Request["idPessoa"]));
        }
    }

    private void addNode(TreeNodeCollection treeNodeCollection, Pessoa pessoa)
    {
        var hierarquia = pessoa.GetHierarquia();
        if (hierarquia == null) return;
        var node = new TreeNode(pessoa.Nome + " - " + hierarquia.Descricao, pessoa.IDPessoa.ToString());
        treeNodeCollection.Add(node); 
        node.Expanded = true;
        ViewState["idsPessoa"] += pessoa.IDPessoa.ToString() + ",";

        var iPessoas = pessoa.GetHierarquiaPessoasFilhas();
        foreach (var p in iPessoas)
        {
            var pessoaFilha = (Pessoa)p;
            addNode(node.ChildNodes, pessoaFilha);
        }
    }

    protected void trvPessoas_SelectedNodeChanged(object sender, EventArgs e)
    {
        addPessoas(int.Parse(trvPessoas.SelectedNode.Value));
    }

    private void addPessoas(int idPessoa)
    {
        txtIdPessoa.Text = idPessoa.ToString();
        var pessoa = new Pessoa(idPessoa);
        pessoa.Get();
        legendAddPessoas.InnerHtml = "Adicionar filhos de (" + pessoa.Nome + " - " + pessoa.GetHierarquia().Descricao + ")";
    }

    protected void btnVoltarJuridica_Click(object sender, EventArgs e)
    {
        voltarAsHierarquias();
    }

    protected void btnVoltarFisica_Click(object sender, EventArgs e)
    {
        voltarAsHierarquias();
    }

    private void voltarAsHierarquias()
    {
        Response.Redirect("~/HierarquiaPessoa.aspx?idPessoa=" + txtIdPessoa.Text);
    }

    protected void btnAddPessoasJurificas_Click(object sender, EventArgs e)
    {
        dvAdicionarPessoasJuridicas.Visible = true;
        dvAdicionarPessoasFisicas.Visible = false;
        legendPessoasJuridica.InnerHtml = legendAddPessoas.InnerHtml;
        dvHierarquias.Visible = false;
        loadPessoasJuridicas();
    }

    protected void btnAddPessoasFisicas_Click(object sender, EventArgs e)
    {
        dvAdicionarPessoasJuridicas.Visible = false;
        dvAdicionarPessoasFisicas.Visible = true;
        legendPessoasFisica.InnerHtml = legendAddPessoas.InnerHtml;
        dvHierarquias.Visible = false;
        loadPessoasFisicas();
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HierarquiaPessoa.aspx?idPessoa=" + txtIdPessoa.Text);
    }

    protected void btnVoltarDel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HierarquiaPessoa.aspx?idPessoa=" + txtIdPessoa.Text);
    }

    protected void btnAdicionarPrimeiroNivel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/HierarquiaPessoaPrimeiroNivel.aspx");
    }

    #region "Metodos adicioar pessoa juridica"
    private void loadPessoasJuridicas()
    {
        findTop();
        loadEstrutura();
    }

    private void loadEstrutura()
    {
        var pessoasJuridicasAdded = new Pessoa(int.Parse(txtIdPessoa.Text)).GetHierarquiaPessoasFilhas();
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
            estrutura.SaveEstrutura(int.Parse(txtIdPessoa.Text), idsPessoaNew);
            estrutura.Commit();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", 
                "<script>alert('Não existe um nivel abaixo da hierarquia'); window.location.href = \"HierarquiaPessoa.aspx?idPessoa=" + txtIdPessoa.Text + "\"</script>");
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
    #endregion

    #region "Metodos adicionar pessoa fisica"
    private void loadPessoasFisicas()
    {
        var pessoaJuridica = new PessoaJuridica();
        pessoaJuridica.Pessoa = new Pessoa(int.Parse(txtIdPessoa.Text));
        pessoaJuridica.Get();

        if (pessoaJuridica.IDPessoaJuridica != null)
        {
            var PessoasFisicasAdded = new PessoaFisicaJuridica().FindPessoasFisicas(pessoaJuridica);
            listPessoasFisicasAdd.DataSource = PessoasFisicasAdded;
            listPessoasFisicasAdd.DataTextField = "PessoaNome";
            listPessoasFisicasAdd.DataValueField = "IDPessoaFisica";
            listPessoasFisicasAdd.DataBind();

            findTopFisica();
        }
    }

    private void findTopFisica()
    {
        listPessoasFisicas.DataSource = new PessoaFisica().FindTop(txtDataFisica.Text);
        listPessoasFisicas.DataTextField = "PessoaNome";
        listPessoasFisicas.DataValueField = "IDPessoaFisica";
        listPessoasFisicas.DataBind();

        foreach (ListItem item in listPessoasFisicasAdd.Items)
        {
            var listItem = listPessoasFisicas.Items.FindByValue(item.Value);
            listPessoasFisicas.Items.Remove(listItem);
        }
    }

    protected void btnAddFisica_Click(object sender, EventArgs e)
    {
        var pessoaJuridica = new PessoaJuridica();
        pessoaJuridica.Pessoa = new Pessoa(int.Parse(txtIdPessoa.Text));
        pessoaJuridica.Get();
        if (pessoaJuridica.IDPessoaJuridica != null)
        {
            var list = new List<ListItem>();
            foreach (ListItem item in listPessoasFisicas.Items)
            {
                if (item.Selected)
                {
                    listPessoasFisicasAdd.Items.Add(item);
                    list.Add(item);
                }
            }

            foreach (ListItem item in list) listPessoasFisicas.Items.Remove(item);
            savePessoasFisicas(pessoaJuridica.Pessoa.Juridica);
        }
    }

    private void savePessoasFisicas(PessoaJuridica pessoaJuridica)
    {
        var pessoaFisicaJuridicaDel = new PessoaFisicaJuridica();
        try
        {
            pessoaFisicaJuridicaDel.IsTransaction = true;
            pessoaFisicaJuridicaDel.PessoaJuridica = pessoaJuridica;
            pessoaFisicaJuridicaDel.Delete();

            foreach (ListItem item in listPessoasFisicasAdd.Items)
            {
                var pessoaFisicaAdd = new PessoaFisica(int.Parse(item.Value));
                pessoaFisicaAdd.Get();
                var PessoaFisicaJuridica = new PessoaFisicaJuridica();
                PessoaFisicaJuridica.Transaction = pessoaFisicaJuridicaDel.Transaction;
                PessoaFisicaJuridica.PessoaJuridica = pessoaJuridica;
                PessoaFisicaJuridica.PessoaFisica = pessoaFisicaAdd;
                PessoaFisicaJuridica.Get();
                PessoaFisicaJuridica.Save();
            }

            pessoaFisicaJuridicaDel.Commit();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
            pessoaFisicaJuridicaDel.Rollback();
        }
    }

    protected void btnDeleteFisica_Click(object sender, EventArgs e)
    {
        var pessoaJuridica = new PessoaJuridica();
        pessoaJuridica.Pessoa = new Pessoa(int.Parse(txtIdPessoa.Text));
        pessoaJuridica.Get();
        if (pessoaJuridica.IDPessoaJuridica != null)
        {
            var list = new List<ListItem>();
            foreach (ListItem item in listPessoasFisicasAdd.Items)
            {
                if (item.Selected)
                {
                    listPessoasFisicas.Items.Add(item);
                    list.Add(item);
                }
            }

            foreach (ListItem item in list) listPessoasFisicasAdd.Items.Remove(item);
            savePessoasFisicas(pessoaJuridica.Pessoa.Juridica);
        }
    }

    protected void btnFindFisica_Click(object sender, EventArgs e)
    {
        findTopFisica();
    }

    #endregion
}