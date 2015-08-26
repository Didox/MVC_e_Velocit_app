using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using TradeVision.UI;

public partial class _CamposTabela : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idCampo"] != null)
            {
                GetCampo(int.Parse(Request["idCampo"]));
                return;
            }
            GetCampos();
        }
    }

    private void LoadCombos()
    {
        ddlOrdem.Items.Clear();
        for (int i = 0; i <= 100; i++ )
            ddlOrdem.Items.Add(new ListItem(i.ToString(), i.ToString()));

        ddlTipo.Items.Clear();
        foreach (TipoCampo tipoCampo in Enum.GetValues(typeof(TipoCampo)))
            ddlTipo.Items.Add(new ListItem(tipoCampo.ToString(), tipoCampo.ToString()));

        ddlTipoCampo.Items.Clear();
        foreach (TipoInput tipoInput in Enum.GetValues(typeof(TipoInput)))
            ddlTipoCampo.Items.Add(new ListItem(tipoInput.ToString(), tipoInput.ToString()));
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var campo = new Campo();
        try
        {
            if (txtId.Text != "")
            {
                campo.IDCampo = int.Parse(txtId.Text);
                campo.Get();
            }

            campo.IDTabela = int.Parse(Request["idTabela"]);
            campo.Nome = txtNome.Text;
            campo.Label = txtLabel.Text;
            campo.Tamanho = int.Parse(txtTamanho.Text);
            campo.ValorDefault = txtValorPadrao.Text;
            campo.PermiteNulo = bool.Parse(rdoPermiteNulo.SelectedValue);
            campo.Tipo = (TipoCampo)Enum.Parse(typeof(TipoCampo), ddlTipo.SelectedValue);
            campo.TipoInput = (TipoInput)Enum.Parse(typeof(TipoInput), ddlTipoCampo.SelectedValue);
            campo.Ordem = int.Parse(ddlOrdem.SelectedValue);
            campo.Save();
            GetCampo((int)campo.IDCampo);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetCampos();
    }

    private void GetCampos()
    {
        dvSalvarCampo.Visible = false;
        dvListarCampos.Visible = true;
        btnIncluirValoresCampo.Visible = true;

        gvCampos.DataSource = new Campo(txtNomeCampo.Text, int.Parse(Request["idTabela"])).Find();
        gvCampos.DataBind();
    }

    private void GetCampo(int idCampo)
    {
        LoadCombos();
        dvSalvarCampo.Visible = true;
        dvListarCampos.Visible = false;

        var campo = new Campo();
        campo.IDTabela = int.Parse(Request["idTabela"]);
        campo.IDCampo = idCampo;
        campo.Get();

        txtId.Text = campo.IDCampo.ToString();
        txtNome.Text = campo.Nome;
        txtLabel.Text = campo.Label;
        txtTamanho.Text = campo.Tamanho.ToString();
        txtValorPadrao.Text = campo.ValorDefault;
        rdoPermiteNulo.SelectedValue = campo.PermiteNulo.ToString();
        ddlTipo.SelectedValue = campo.Tipo.ToString();
        ddlTipoCampo.SelectedValue = campo.TipoInput.ToString();
        ddlOrdem.SelectedValue = campo.Ordem.ToString();
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetCampos();
    }

    protected void btnIncluirValoresCampo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ValoresCampo.aspx?idCampo=" + txtId.Text);
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        LoadCombos();
        dvSalvarCampo.Visible = true;
        dvListarCampos.Visible = false;
        btnIncluirValoresCampo.Visible = false;

        txtId.Text = "";
        txtNome.Text = "";
        txtLabel.Text = "";
        txtTamanho.Text = "0";
        txtValorPadrao.Text = "";
        rdoPermiteNulo.SelectedValue = "False";
        ddlTipo.Items[0].Selected = true;
        ddlTipoCampo.Items[0].Selected = true;
        ddlOrdem.SelectedIndex = 0;
    }

    protected void gvCampos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetCampo(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            Excluir(int.Parse(e.CommandArgument.ToString()));
    }

    private void Excluir(int idCampo)
    {
        try
        {
            new Campo(idCampo).Delete();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
        GetCampos();
    }

    protected void btnVoltaTabela_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Tabelas.aspx?idTabela=" + Request["idTabela"]);
    }
}
