using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using System.Web.UI.MobileControls;
using TradeVision.UI;

public partial class _Cargos : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idCargo"] != null)
            {
                GetCargo(int.Parse(Request["idCargo"]));
                return;
            }

            GetCargos();            
        }
    }

    private void LoadCompos()
    {
        var icargos = new Cargo().Find();
        var cargos = new List<Cargo>();
        var idCargo = Convert.ToInt32("0" + txtId.Text);
        foreach (var c in icargos)
            if (c.Id != idCargo) cargos.Add((Cargo)c);

        ddlCargoPai.DataSource = cargos;
        ddlCargoPai.DataTextField = "Descricao";
        ddlCargoPai.DataValueField = "IdCargo";
        ddlCargoPai.DataBind();
        ddlCargoPai.Items.Insert(0, new ListItem("Sem Pai", "0"));

        ddlColunaEstrutura.Items.Clear();
        for (var i = 1; i <= 10; i++)
            ddlColunaEstrutura.Items.Add(new ListItem("idPessoaNivel" + i, "idPessoaNivel" + i));
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Cargo = new Cargo();
        try
        {
            if (txtId.Text != "")
            {
                Cargo.IDCargo = int.Parse(txtId.Text);
                Cargo.Get();
            }

            Cargo.Descricao = txtNome.Text;
            Cargo.ColunaEstrutura = ddlColunaEstrutura.SelectedValue;
            Cargo.IDCargoPai = int.Parse(ddlCargoPai.SelectedValue);
            Cargo.Ordem = int.Parse(txtOrdem.Text);
            Cargo.Save();
            GetCargo((int)Cargo.IDCargo);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetCargos();
    }

    private void GetCargos()
    {
        dvSalvarCargo.Visible = false;
        dvListarCargos.Visible = true;

        gvCargos.DataSource = new Cargo(txtNomeCargo.Text).Find();
        gvCargos.DataBind();
    }

    private void GetCargo(int idCargo)
    {
        dvSalvarCargo.Visible = true;
        dvListarCargos.Visible = false; 

        var Cargo = new Cargo();
        Cargo.IDCargo = idCargo;
        Cargo.Get();

        txtId.Text = Cargo.IDCargo.ToString();
        LoadCompos();
        txtNome.Text = Cargo.Descricao;
        txtOrdem.Text = Cargo.Ordem.ToString();
        if (ddlColunaEstrutura.Items.FindByValue(Cargo.ColunaEstrutura) != null)
            ddlColunaEstrutura.SelectedValue = Cargo.ColunaEstrutura;

        if (ddlCargoPai.Items.FindByValue(Cargo.IDCargoPai.ToString()) != null) 
            ddlCargoPai.SelectedValue = Cargo.IDCargoPai.ToString();
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetCargos();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarCargo.Visible = true;
        dvListarCargos.Visible = false; 

        txtId.Text = "";
        LoadCompos();
        txtNome.Text = "";
        txtOrdem.Text = "0";
        ddlColunaEstrutura.SelectedIndex = 0;
        ddlCargoPai.SelectedIndex = 0;
    }

    protected void gvCargos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetCargo(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeleteCargo(int.Parse(e.CommandArgument.ToString()));
    }

    protected void DeleteCargo(int idCargo)
    {
        try
        {
            var Cargo = new Cargo();
            Cargo.IDCargo = idCargo;
            Cargo.Delete();
            GetCargos();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }
}
