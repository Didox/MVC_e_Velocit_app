using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _TipoTabelaColuna : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idTipoTabelaColuna"] != null)
            {
                GetTipoTabelaColuna(int.Parse(Request["idTipoTabelaColuna"]));
                return;
            }

            GetTipoTabelaColunas();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var TipoTabelaColuna = new TipoTabelaColuna();
        try
        {
            if (txtId.Text != "")
            {
                TipoTabelaColuna.IDTipoTabelaColuna = int.Parse(txtId.Text);
                TipoTabelaColuna.Get();
            }

            TipoTabelaColuna.Tabela = txtTabela.Text;
            TipoTabelaColuna.Coluna = txtColuna.Text;

            TipoTabelaColuna.Save();
            GetTipoTabelaColuna((int)TipoTabelaColuna.IDTipoTabelaColuna);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetTipoTabelaColunas()
    {
        dvSalvarTipoTabelaColuna.Visible = false;
        dvListarTipoTabelaColunas.Visible = true;

        gvTipoTabelaColunas.DataSource = new TipoTabelaColuna().Find();
        gvTipoTabelaColunas.DataBind();
    }

    private void GetTipoTabelaColuna(int idTipoTabelaColuna)
    {
        dvSalvarTipoTabelaColuna.Visible = true;
        dvListarTipoTabelaColunas.Visible = false;

        var TipoTabelaColuna = new TipoTabelaColuna();
        TipoTabelaColuna.IDTipoTabelaColuna = idTipoTabelaColuna;
        TipoTabelaColuna.Get();

        txtId.Text = TipoTabelaColuna.IDTipoTabelaColuna.ToString();
        txtTabela.Text = TipoTabelaColuna.Tabela.ToString();
        txtColuna.Text = TipoTabelaColuna.Coluna.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetTipoTabelaColunas();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarTipoTabelaColuna.Visible = true;
        dvListarTipoTabelaColunas.Visible = false;

        txtId.Text = "";
        txtTabela.Text = string.Empty;
        txtColuna.Text = string.Empty;

    }

    protected void DeleteTipoTabelaColuna(int idTipoTabelaColuna)
    {
        try
        {
            var TipoTabelaColuna = new TipoTabelaColuna();
            TipoTabelaColuna.IDTipoTabelaColuna = idTipoTabelaColuna;
            TipoTabelaColuna.Delete();
            GetTipoTabelaColunas();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvTipoTabelaColunas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetTipoTabelaColuna(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeleteTipoTabelaColuna(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvTipoTabelaColunas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTipoTabelaColunas.PageIndex = e.NewPageIndex;
        GetTipoTabelaColunas();
    }
}