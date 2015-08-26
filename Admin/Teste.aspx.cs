using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _Teste : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idTeste"] != null)
            {
                GetTeste(int.Parse(Request["idTeste"]));
                return;
            }

            GetTestes();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Teste = new Teste();
        try
        {
            if (txtId.Text != "")
            {
                Teste.IDTeste = int.Parse(txtId.Text);
                Teste.Get();
            }

            Teste.Descricao = txtDescricao.Text;
Teste.Oliveira = txtOliveira.Text;
Teste.Assi = int.Parse(txtAssi.Text);

            Teste.Save();
            GetTeste((int)Teste.IDTeste);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetTestes()
    {
        dvSalvarTeste.Visible = false;
        dvListarTestes.Visible = true;
        
        gvTestes.DataSource = new Teste().Find();
        gvTestes.DataBind();
    }

    private void GetTeste(int idTeste)
    {
        dvSalvarTeste.Visible = true;
        dvListarTestes.Visible = false;

        var Teste = new Teste();
        Teste.IDTeste = idTeste;
        Teste.Get();

        txtId.Text = Teste.IDTeste.ToString();
        txtDescricao.Text = Teste.Descricao.ToString();
txtOliveira.Text = Teste.Oliveira.ToString();
txtAssi.Text = Teste.Assi.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetTestes();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarTeste.Visible = true;
        dvListarTestes.Visible = false;

        txtId.Text = "";
        txtDescricao.Text = string.Empty;
txtOliveira.Text = string.Empty;
txtAssi.Text = string.Empty;

    }

	protected void DeleteTeste(int idTeste)
    {
        try
        {
			var Teste = new Teste();
			Teste.IDTeste = idTeste;
			Teste.Delete();
			GetTestes();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }
    
    protected void gvTestes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetTeste(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
			DeleteTeste(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvTestes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTestes.PageIndex = e.NewPageIndex;
        GetTestes();
    }
}
