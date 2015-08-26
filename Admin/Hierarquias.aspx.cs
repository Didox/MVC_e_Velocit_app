using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using System.Web.UI.MobileControls;
using TradeVision.UI;

public partial class _Hierarquias : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idHierarquia"] != null)
            {
                GetHierarquia(int.Parse(Request["idHierarquia"]));
                return;
            }

            GetHierarquias();            
        }
    }

    private void LoadCompos()
    {
        var iHierarquias = new Hierarquia().Find();
        var Hierarquias = new List<Hierarquia>();
        var idHierarquia = Convert.ToInt32("0" + txtId.Text);
        foreach (var c in iHierarquias)
            if (c.Id != idHierarquia) Hierarquias.Add((Hierarquia)c);

        ddlHierarquiaPai.DataSource = Hierarquias;
        ddlHierarquiaPai.DataTextField = "Descricao";
        ddlHierarquiaPai.DataValueField = "IdHierarquia";
        ddlHierarquiaPai.DataBind();
        ddlHierarquiaPai.Items.Insert(0, new ListItem("Sem Pai", "0"));

        ddlColunaEstrutura.Items.Clear();
        for (var i = 1; i <= 10; i++)
            ddlColunaEstrutura.Items.Add(new ListItem("idPessoaNivel" + i, "idPessoaNivel" + i));
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Hierarquia = new Hierarquia();
        try
        {
            if (txtId.Text != "")
            {
                Hierarquia.IDHierarquia = int.Parse(txtId.Text);
                Hierarquia.Get();
            }

            Hierarquia.Descricao = txtNome.Text;
            Hierarquia.ColunaEstrutura = ddlColunaEstrutura.SelectedValue;
            Hierarquia.IDHierarquiaPai = int.Parse(ddlHierarquiaPai.SelectedValue);
            Hierarquia.Ordem = int.Parse(txtOrdem.Text);
            Hierarquia.Save();
            GetHierarquia((int)Hierarquia.IDHierarquia);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetHierarquias();
    }

    private void GetHierarquias()
    {
        dvSalvarHierarquia.Visible = false;
        dvListarHierarquias.Visible = true;

        gvHierarquias.DataSource = new Hierarquia(txtNomeHierarquia.Text).Find();
        gvHierarquias.DataBind();
    }

    private void GetHierarquia(int idHierarquia)
    {

        dvSalvarHierarquia.Visible = true;
        dvListarHierarquias.Visible = false;

        var Hierarquia = new Hierarquia();
        Hierarquia.IDHierarquia = idHierarquia;
        Hierarquia.Get();

        txtId.Text = Hierarquia.IDHierarquia.ToString();

        LoadCompos();

        txtNome.Text = Hierarquia.Descricao;
        txtOrdem.Text = Hierarquia.Ordem.ToString();
        if (ddlColunaEstrutura.Items.FindByValue(Hierarquia.ColunaEstrutura) != null)
            ddlColunaEstrutura.SelectedValue = Hierarquia.ColunaEstrutura;

        if (ddlHierarquiaPai.Items.FindByValue(Hierarquia.IDHierarquiaPai.ToString()) != null)
            ddlHierarquiaPai.SelectedValue = Hierarquia.IDHierarquiaPai.ToString();
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetHierarquias();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarHierarquia.Visible = true;
        dvListarHierarquias.Visible = false; 

        txtId.Text = "";
        LoadCompos();
        txtNome.Text = "";
        txtOrdem.Text = "0";
        ddlColunaEstrutura.SelectedIndex = 0;
        ddlHierarquiaPai.SelectedIndex = 0;
    }

    protected void gvHierarquias_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetHierarquia(int.Parse(e.CommandArgument.ToString()));
    }
}
