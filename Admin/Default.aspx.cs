using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Usuario.Current() != null) Response.Redirect("~/Home.aspx");
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {        
        Usuario usuario = new Usuario();

        if (string.IsNullOrEmpty(txtLogin.Text))
        {
            dvErro.InnerText = "Preenha o login";
            return;
        }

        if (string.IsNullOrEmpty(txtSenha.Text))
        {
            dvErro.InnerText = "Preenha o senha";
            return;
        }

        if (usuario.Logon(txtLogin.Text, txtSenha.Text))
        {
            Response.Redirect("~/Home.aspx");
            return;
        }

        dvErro.InnerText = "Usuário ou senha inválido";
    }
}
