using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using TradeVision.UI;

public partial class _ValoresCampo : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetValoresCampo();
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var campoOpcao = new CampoOpcao();
        try
        {
            if (txtId.Text != "")
            {
                campoOpcao.IDCampoOpcao = int.Parse(txtId.Text);
                campoOpcao.Get();
            }
            campoOpcao.IDCampo = int.Parse(Request["idCampo"]);
            campoOpcao.Opcao = txtNome.Text;
            campoOpcao.Save();
            GetValoresCampo();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetValoresCampo();
    }

    private void GetValoresCampo()
    {
        dvSalvarCampoOpcao.Visible = false;
        dvListarValoresCampo.Visible = true;

        gvValoresCampo.DataSource = new CampoOpcao(txtNomeCampoOpcao.Text, int.Parse(Request["idCampo"])).Find();
        gvValoresCampo.DataBind();
    }

    private void GetCampoOpcao(int idCampoOpcao)
    {
        dvSalvarCampoOpcao.Visible = true;
        dvListarValoresCampo.Visible = false;

        var campoOpcao = new CampoOpcao();
        campoOpcao.IDCampoOpcao = idCampoOpcao;
        campoOpcao.Get();

        txtId.Text = campoOpcao.IDCampoOpcao.ToString();
        txtNome.Text = campoOpcao.Opcao;        
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetValoresCampo();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarCampoOpcao.Visible = true;
        dvListarValoresCampo.Visible = false;

        txtId.Text = "";
        txtNome.Text = "";        
    }

    protected void gvValoresCampo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetCampoOpcao(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            Excluir(int.Parse(e.CommandArgument.ToString()));
    }

    private void Excluir(int idCampoOpcao)
    {
        try
        {
            new CampoOpcao(idCampoOpcao).Delete();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
        GetValoresCampo();
    }

    protected void btnVoltaCampo_Click(object sender, EventArgs e)
    {
        var campo = new Campo(int.Parse(Request["idCampo"]));
        campo.Get();
        Response.Redirect("~/camposTabela.aspx?idCampo=" + campo.IDCampo + "&idTabela=" + campo.Tabela.IDTabela );
    }
}
