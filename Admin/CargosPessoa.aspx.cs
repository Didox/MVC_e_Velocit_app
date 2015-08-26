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

public partial class _CargosPessoa : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            trvPessoas.Nodes.Clear();
            var iPessoas = new Pessoa().GetPrimeirosNosPessoaCargo();
            foreach (var p in iPessoas)
            {
                var cargo = (Pessoa)p;
                addNode(trvPessoas.Nodes, cargo);
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
        var cargo = pessoa.GetCargo();
        if (cargo == null) return;
        var node = new TreeNode(pessoa.Nome + " - " + cargo.Descricao, pessoa.IDPessoa.ToString());
        treeNodeCollection.Add(node); 
        node.Expanded = true;
        ViewState["idsPessoa"] += pessoa.IDPessoa.ToString() + ",";

        var iPessoas = pessoa.GetCargoPessoasFilhas();
        foreach (var p in iPessoas)
        {
            var pessoaFilha = (Pessoa)p;
            addNode(node.ChildNodes, pessoaFilha);
        }
    }

    protected void trvPessoas_SelectedNodeChanged(object sender, EventArgs e)
    {
        txtIdPessoa.Text = trvPessoas.SelectedNode.Value;
        addPessoas(int.Parse(trvPessoas.SelectedNode.Value));
    }

    private void addPessoas(int idCargo)
    {
        txtIdPessoa.Text = idCargo.ToString();
        var pessoa = new Pessoa(idCargo);
        pessoa.Get();
        legendAddPessoas.InnerHtml = "Adicionar subordinados a (" + pessoa.Nome + " - " + pessoa.GetCargo().Descricao + ")";
    }

    protected void btnAdicionarPrimeiroNivel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CargosPessoaPrimeiroNivel.aspx");
    }

    protected void btnAddPessoas_Click(object sender, EventArgs e)
    {
        dvAdicionarPessoas.Visible = true;
        legendPessoasFisica.InnerHtml = legendAddPessoas.InnerHtml;
        dvCargos.Visible = false;
        loadPessoasFisicas();
    }


    #region "Metodos adicioar pessoa Fisica"
    private void loadPessoasFisicas()
    {
        findTop();
        loadCargoEstrutura();
    }

    private void loadCargoEstrutura()
    {
        var pessoasFisicasAdded = new Pessoa(int.Parse(txtIdPessoa.Text)).GetCargoPessoasFilhas();
        listPessoasFisicasAdd.DataSource = pessoasFisicasAdded;
        listPessoasFisicasAdd.DataTextField = "Nome";
        listPessoasFisicasAdd.DataValueField = "IDPessoa";
        listPessoasFisicasAdd.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
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
        savePessoasFisica();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
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
        savePessoasFisica();
    }

    private void savePessoasFisica()
    {
        var estrutura = new CargoEstrutura();
        try
        {
            estrutura.IsTransaction = true;
            var idsPessoaNew = new List<int>();
            foreach (ListItem item in listPessoasFisicasAdd.Items)
                idsPessoaNew.Add(int.Parse(item.Value));
            estrutura.SaveCargoEstrutura(int.Parse(txtIdPessoa.Text), idsPessoaNew);
            estrutura.Commit();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script",
                "<script>alert('Não existe um nivel abaixo da hierarquia de cargo que vc está tentando adicionar'); window.location.href = \"CargosPessoa.aspx?idPessoa=" + txtIdPessoa.Text + "\"</script>");
            estrutura.Rollback();
        }
    }

    protected void btnVoltarFisica_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CargosPessoa.aspx?idPessoa=" + txtIdPessoa.Text);
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        findTop();
    }

    private void findTop()
    {
        listPessoasFisicas.DataSource = new Pessoa().FindTop(txtData.Text, TipoPessoa.Fisica);
        listPessoasFisicas.DataTextField = "Nome";
        listPessoasFisicas.DataValueField = "IDPessoa";
        listPessoasFisicas.DataBind();
    }
    #endregion
}