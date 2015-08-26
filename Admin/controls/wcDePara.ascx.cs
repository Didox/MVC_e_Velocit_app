using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;

public partial class controls_wcDePara : System.Web.UI.UserControl
{
    private PessoaJuridica pessoaJuridica = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtIdPessoaJuridica.Text))
            pessoaJuridica = new PessoaJuridica(int.Parse(txtIdPessoaJuridica.Text));
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        LoadPessoaJuridicaDePara(pessoaJuridica);
    }

    public void LoadPessoaJuridicaDePara(PessoaJuridica pessoaJuridica)
    {
        listPessoasJuridicas.DataSource = new PessoaJuridica().FindTop(txtPessoasJuridicas.Text);
        listPessoasJuridicas.DataTextField = "RazaoSocial";
        listPessoasJuridicas.DataValueField = "IDPessoaJuridica";
        listPessoasJuridicas.DataBind();

        this.pessoaJuridica = pessoaJuridica;

        var historicoDePara = getHistorico();

        txtIdPessoaJuridica.Text = pessoaJuridica.IDPessoaJuridica.ToString();
        foreach (DeJuridicaParaJuridica h in historicoDePara)
        {
            var item = listPessoasJuridicas.Items.FindByValue(h.IDPessoaJuridicaDe.ToString());
            if (item != null) listPessoasJuridicas.Items.Remove(item);

            var itemAntigo = listPessoasJuridicas.Items.FindByValue(h.IDPessoaJuridicaPara.ToString());
            if (itemAntigo != null) listPessoasJuridicas.Items.Remove(itemAntigo);
        }

        var listItem = listPessoasJuridicas.Items.FindByValue(pessoaJuridica.IDPessoaJuridica.ToString());
        if (listItem != null) listPessoasJuridicas.Items.Remove(listItem);

        GetPessoaJuridicaDePara(historicoDePara);
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(listPessoasJuridicas.SelectedValue))
                throw new TradeVisionValidationError("Selecione uma pessoa juridica ");

            if (listPessoasJuridicas.SelectedValue.IndexOf(",") != -1)
                throw new TradeVisionValidationError("Selecione somente uma pessoa juridica ");

            var para = new PessoaJuridica(int.Parse(listPessoasJuridicas.SelectedValue));
            para.Get();

            var dePara = new DeJuridicaParaJuridica();
            dePara.PessoaJuridicaDe = pessoaJuridica;
            dePara.PessoaJuridicaPara = para;
            dePara.Save();

            pessoaJuridica = para;
            loadTab();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#aDePara').trigger(\"click\");alert('" + FormatError.FormatMessageForJAlert(err.Message) + "');</script>");
        }
    }

    private void loadTab()
    {
        Response.Redirect("~/PessoaJuridica.aspx?idPessoaJuridica=" + pessoaJuridica.IDPessoaJuridica);// + "&tab=aDePara");
    }

    protected void gvPessoaJuridicasDePara_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Excluir")
        {
            new DeJuridicaParaJuridica().ExecSql("sp_deleteDeParaJuridico @IDDeJuridicaParaJuridica = " + int.Parse(e.CommandArgument.ToString()));
            Response.Redirect("~/PessoaJuridica.aspx?idPessoaJuridica=" + pessoaJuridica.IDPessoaJuridica + "&tab=aDePara");
        }
    }

    protected void gvPessoaJuridicasDePara_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPessoaJuridicasDePara.PageIndex = e.NewPageIndex;
        GetPessoaJuridicaDePara();
    }

    private void GetPessoaJuridicaDePara()
    {
        GetPessoaJuridicaDePara(null);
    }

    private void GetPessoaJuridicaDePara(LIType historicoDePara)
    {
        if (historicoDePara == null)
            historicoDePara = getHistorico();

        gvPessoaJuridicasDePara.DataSource = historicoDePara;
        gvPessoaJuridicasDePara.DataBind();
    }

    private LIType getHistorico()
    {
        return new DeJuridicaParaJuridica().FindBySql("sp_listDeParaJuridica @idPessoaJuridica = " + pessoaJuridica.IDPessoaJuridica);
    }
}
