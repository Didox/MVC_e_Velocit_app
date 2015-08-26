using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using TradeVision.UI;

public partial class _ConfigurarSenhas : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var configuracaoSenha = new ConfiguracaoSenha();
            configuracaoSenha.Programa = Programa.Current();
            configuracaoSenha.Cliente = Cliente.Current();
            configuracaoSenha.Campanha = Campanha.Current();
            configuracaoSenha.Get();
            if (configuracaoSenha.IDConfiguracaoSenha != null && (bool)configuracaoSenha.SenhaCriptografada)
                rblCroptografada.SelectedValue = "true";
            else rblCroptografada.SelectedValue = "false";
        }
    }

    protected void rblCroptografada_SelectedIndexChanged(object sender, EventArgs e)
    {
        var configuracaoSenha = new ConfiguracaoSenha();
        configuracaoSenha.Programa = Programa.Current();
        configuracaoSenha.Cliente = Cliente.Current();
        configuracaoSenha.Campanha = Campanha.Current();
        configuracaoSenha.Get();

        configuracaoSenha.SenhaCriptografada = Convert.ToBoolean(rblCroptografada.SelectedValue);
        configuracaoSenha.Save();
    }
}
