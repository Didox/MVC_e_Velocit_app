using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using TradeVision.UI;

public partial class _Tabelas : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idTabela"] != null)
            {
                GetTabela(int.Parse(Request["idTabela"]));
                return;
            }

            GetTabelas();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            var tabela = new Tabela();
            if (txtId.Text != "")
            {
                tabela.IDTabela = int.Parse(txtId.Text);
                tabela.Get();
            }

            tabela.Descricao = txtNome.Text;
            tabela.Save();
            GetTabela((int)tabela.IDTabela);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetTabelas();
    }

    private void GetTabelas()
    {
        dvSalvarTabela.Visible = false;
        dvListarTabelas.Visible = true;
        btnIncluirCampos.Visible = false;

        gvTabelas.DataSource = new Tabela(txtNomeTabela.Text).Find();
        gvTabelas.DataBind();
    }

    private void GetTabela(int idTabela)
    {
        btnIncluirCampos.Visible = true;
        dvSalvarTabela.Visible = true;
        dvListarTabelas.Visible = false; 

        var tabela = new Tabela();
        tabela.IDTabela = idTabela;
        tabela.Get();

        txtId.Text = tabela.IDTabela.ToString();
        txtNome.Text = tabela.Descricao;        
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetTabelas();
    }

    protected void btnIncluirCampos_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CamposTabela.aspx?idTabela=" + txtId.Text);
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarTabela.Visible = true;
        dvListarTabelas.Visible = false;
        btnIncluirCampos.Visible = false;

        txtId.Text = "";
        txtNome.Text = "";        
    }

    protected void gvTabelas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetTabela(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            Excluir(int.Parse(e.CommandArgument.ToString()));
    }

    private void Excluir(int idTabela)
    {
        try
        {
            new Tabela(idTabela).Delete();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }

        GetTabelas();
    }
}
