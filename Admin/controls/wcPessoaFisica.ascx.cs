using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;

public partial class controls_wcPessoaFisica : System.Web.UI.UserControl
{
    private PessoaJuridica pessoaJuridica = null;
    protected void Page_Load(object sender, EventArgs e) { }

    private void loadLists(PessoaJuridica pessoaJuridica)
    {        
        listPessoasFisicas.DataSource = new PessoaFisica().FindTop(txtData.Text);
        listPessoasFisicas.DataTextField = "PessoaNome";
        listPessoasFisicas.DataValueField = "IDPessoaFisica";
        listPessoasFisicas.DataBind();

        var PessoasFisicasAdded = new PessoaFisicaJuridica().FindPessoasFisicas(pessoaJuridica);
        listPessoasFisicasAdd.DataSource = PessoasFisicasAdded;
        listPessoasFisicasAdd.DataTextField = "PessoaNome";
        listPessoasFisicasAdd.DataValueField = "IDPessoaFisica";
        listPessoasFisicasAdd.DataBind();

        foreach (var pessoaFisica in PessoasFisicasAdded)
        {
            var listItem = listPessoasFisicas.Items.FindByValue(pessoaFisica.IDPessoaFisica.ToString());
            listPessoasFisicas.Items.Remove(listItem);
        }
    }

    public void GetPessoasFisicas(int idPessoaJuridica)
    {
        tableAddPessoas.Visible = true;
        dvPessoaFisicaNotFound.Visible = false;
        pessoaJuridica = new PessoaJuridica(idPessoaJuridica);
        pessoaJuridica.Get();

        if (CType.Exist(pessoaJuridica.Pessoa) && CType.Exist(pessoaJuridica.Pessoa.Juridica))
        {
            txtId.Text = pessoaJuridica.Id.ToString();
            loadLists(pessoaJuridica);
            return;
        }

        dvPessoaFisicaNotFound.Visible = true;
        tableAddPessoas.Visible = false;
        dvPessoaFisicaNotFound.InnerHtml = "Pessoa fisica não cadastrada";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pessoaJuridica = new PessoaJuridica(int.Parse(txtId.Text));
        pessoaJuridica.Get();
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
        saveParceiros(pessoaJuridica.Pessoa.Juridica);
    }

    private void saveParceiros(PessoaJuridica pessoaJuridica)
    {
        var PessoaFisicaJuridicaDel = new PessoaFisicaJuridica();
        try
        {
            PessoaFisicaJuridicaDel.IsTransaction = true;
            PessoaFisicaJuridicaDel.PessoaJuridica = pessoaJuridica;
            PessoaFisicaJuridicaDel.Delete();

            foreach (ListItem item in listPessoasFisicasAdd.Items)
            {
                var pessoaFisicaAdd = new PessoaFisica(int.Parse(item.Value));
                pessoaFisicaAdd.Get();
                var PessoaFisicaJuridica = new PessoaFisicaJuridica();
                PessoaFisicaJuridica.Transaction = PessoaFisicaJuridicaDel.Transaction;
                PessoaFisicaJuridica.PessoaJuridica = pessoaJuridica;
                PessoaFisicaJuridica.PessoaFisica = pessoaFisicaAdd;
                PessoaFisicaJuridica.Get();
                PessoaFisicaJuridica.Save();
            }

            PessoaFisicaJuridicaDel.Commit();
            loadTab();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
            PessoaFisicaJuridicaDel.Rollback();
        }
    }

    private void loadTab()
    {
        //Page.ClientScript.RegisterStartupScript(this.GetType(), Funcoes.KeyStript(), "<script>$('#aPessoasFisicas').trigger(\"click\");</script>");
        Response.Redirect("~/PessoaJuridica.aspx?idPessoaJuridica=" + pessoaJuridica.IDPessoaJuridica + "&tab=aPessoasFisicas");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        pessoaJuridica = new PessoaJuridica(int.Parse(txtId.Text));
        pessoaJuridica.Get();

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
        saveParceiros(pessoaJuridica.Pessoa.Juridica);
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        GetPessoasFisicas(int.Parse(txtId.Text));
        loadTab();
    }
}
