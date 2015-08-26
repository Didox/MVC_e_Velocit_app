using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _Cliente : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idCliente"] != null)
            {
                GetCliente(int.Parse(Request["idCliente"]));
                return;
            }

            GetClientes();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Cliente = new Cliente();
        try
        {
            if (txtId.Text != "")
            {
                Cliente.IDCliente = int.Parse(txtId.Text);
                Cliente.Get();
            }

            Cliente.Nome = txtDescricao.Text;
            Cliente.IPServidor = txtIPServidor.Text;
            Cliente.DBName = txtDBName.Text;
            Cliente.DBUser = txtDBUser.Text;
            Cliente.DBPassword = txtDBPassword.Text;
            Cliente.Slug = txtSlug.Text;

            Cliente.Save();
            GetCliente((int)Cliente.IDCliente);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetClientes()
    {
        dvSalvarCliente.Visible = false;
        dvListarClientes.Visible = true;

        gvClientes.DataSource = new Cliente().Find();
        gvClientes.DataBind();
    }

    private void GetCliente(int idCliente)
    {
        dvSalvarCliente.Visible = true;
        dvListarClientes.Visible = false;

        var Cliente = new Cliente();
        Cliente.IDCliente = idCliente;
        Cliente.Get();

        txtId.Text = Cliente.IDCliente.ToString();
        txtDescricao.Text = Cliente.Nome;
        txtIPServidor.Text = Cliente.IPServidor.ToString();
        txtDBName.Text = Cliente.DBName.ToString();
        txtDBUser.Text = Cliente.DBUser.ToString();
        txtDBPassword.Text = Cliente.DBPassword.ToString();
        txtSlug.Text = Cliente.Slug.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetClientes();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarCliente.Visible = true;
        dvListarClientes.Visible = false;

        txtId.Text = "";
        txtDescricao.Text = string.Empty;
        txtIPServidor.Text = string.Empty;
        txtDBName.Text = string.Empty;
        txtDBUser.Text = string.Empty;
        txtDBPassword.Text = string.Empty;
        txtSlug.Text = string.Empty;

    }

    protected void DeleteCliente(int idCliente)
    {
        try
        {
            var Cliente = new Cliente();
            Cliente.IDCliente = idCliente;
            Cliente.Delete();
            GetClientes();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetCliente(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeleteCliente(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvClientes.PageIndex = e.NewPageIndex;
        GetClientes();
    }
}