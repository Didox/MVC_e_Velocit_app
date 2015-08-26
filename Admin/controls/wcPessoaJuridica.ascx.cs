using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;

public partial class controls_wcPessoaJuridica : System.Web.UI.UserControl
{
    private Usuario usuario = null;
    protected void Page_Load(object sender, EventArgs e) { }

    private void loadLists(PessoaFisica pessoaFisica)
    {
        listPessoasJuridicas.DataSource = new PessoaJuridica().FindTop(txtData.Text);
        listPessoasJuridicas.DataTextField = "RazaoSocial";
        listPessoasJuridicas.DataValueField = "IDPessoaJuridica";
        listPessoasJuridicas.DataBind();

        var pessoasJuridicasAdded = new PessoaFisicaJuridica().FindPessoasJuridicas(pessoaFisica);
        listPessoasJuridicasAdd.DataSource = pessoasJuridicasAdded;
        listPessoasJuridicasAdd.DataTextField = "RazaoSocial";
        listPessoasJuridicasAdd.DataValueField = "IDPessoaJuridica";
        listPessoasJuridicasAdd.DataBind();

        foreach (var pessoaJuridica in pessoasJuridicasAdded)
        {
            var listItem = listPessoasJuridicas.Items.FindByValue(pessoaJuridica.IDPessoaJuridica.ToString());
            listPessoasJuridicas.Items.Remove(listItem);
        }
    }

    public void GetPessoasJuridicas(int idUsuario)
    {
        tableAddPessoas.Visible = true;
        usuario = new Usuario(idUsuario);
        usuario.Get();

        if (CType.Exist(usuario.Pessoa) && CType.Exist(usuario.Pessoa.Fisica))
        {
            txtId.Text = usuario.Id.ToString();
            loadLists(usuario.Pessoa.Fisica);
            return;
        }

        dvPessoaFisicaNotFound.Visible = true;
        tableAddPessoas.Visible = false;
        dvPessoaFisicaNotFound.InnerHtml = "Pessoa fisica não cadastrada";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        usuario = new Usuario(int.Parse(txtId.Text));
        usuario.Get();
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
        saveParceiros(usuario.Pessoa.Fisica);
    }

    private void saveParceiros(PessoaFisica pessoaFisica)
    {
        var PessoaFisicaJuridicaDel = new PessoaFisicaJuridica();
        try
        {
            PessoaFisicaJuridicaDel.IsTransaction = true;
            PessoaFisicaJuridicaDel.PessoaFisica = pessoaFisica;
            PessoaFisicaJuridicaDel.Delete();

            foreach (ListItem item in listPessoasJuridicasAdd.Items)
            {
                var pessoaJuridicaAdd = new PessoaJuridica(int.Parse(item.Value));
                pessoaJuridicaAdd.Get();
                var PessoaFisicaJuridica = new PessoaFisicaJuridica();
                PessoaFisicaJuridica.Transaction = PessoaFisicaJuridicaDel.Transaction;
                PessoaFisicaJuridica.PessoaFisica = pessoaFisica;
                PessoaFisicaJuridica.PessoaJuridica = pessoaJuridicaAdd;
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
        Response.Redirect("~/credenciais.aspx?idUsuario=" + usuario.IDUsuario + "&tab=aPessoasJuridicas");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        usuario = new Usuario(int.Parse(txtId.Text));
        usuario.Get();

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
        saveParceiros(usuario.Pessoa.Fisica);
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        GetPessoasJuridicas(int.Parse(txtId.Text));
        loadTab();
    }
}
