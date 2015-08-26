using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;

public partial class _Default : System.Web.UI.MasterPage
{
    Usuario usuario = Usuario.Current();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetConfiguracao();
    }

    private void GetConfiguracao()
    {
        var cliente = Cliente.Current();
        if (cliente == null)
        {
            dvConfig.Visible = false;
            return;
        }

        string config = cliente.Nome + "/";

        var programa = Programa.Current();
        var campanha = Campanha.Current();

        if (programa != null)
            config += programa.Descricao + "/";

        if (campanha != null)
            config += campanha.Descricao + "/";

        lblConfiguracao.Text = config;
        dvConfig.Visible = true;
    }

    protected void hlSair_Click(object sender, EventArgs e)
    {
        usuario.Logoff();
        Response.Redirect("~/");
    }

    protected void btnVoltarMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/");
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        Cliente.Dispose();
        Programa.Dispose();
        Campanha.Dispose();

        Response.Redirect("~/home.aspx");
    }
}
