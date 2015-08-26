using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;

public partial class controls_wcTelefone : System.Web.UI.UserControl
{
    private int IdConteudo = 0;
    private bool PessoJuridica = false;

    public void SetIdUsuario(int idJuridica)
    {
        IdConteudo = idJuridica;
        PessoJuridica = true;
    }

    public void SetIdPessoaJuridica(int idUsuario)
    {
        IdConteudo = idUsuario;
        PessoJuridica = false;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) LoadCombos();
    }

    public void SavePessoaTelefone(Pessoa pessoa)
    {
        var telefoneCount = int.Parse(Request["hiddenTelefoneCount"]);
        var telefoneDel = new Telefone();
        telefoneDel.Pessoa = pessoa;
        telefoneDel.Delete();

        for (int i = 0; i < telefoneCount; i++)
        {
            if (string.IsNullOrEmpty(Request["txtNumeroTelefone-" + i])) continue;
            var telefone = new Telefone();
            telefone.Pessoa = pessoa;
            if (!string.IsNullOrEmpty(Request["ddlTiposTelefone-" + i]))
                telefone.IDTipoTelefone = int.Parse(Request["ddlTiposTelefone-" + i]);
            if (!string.IsNullOrEmpty(Request["txtDDITelefone-" + i]))
                telefone.DDI = int.Parse(Request["txtDDITelefone-" + i]);
            if (!string.IsNullOrEmpty(Request["txtDDDTelefone-" + i]))
                telefone.DDD = int.Parse(Request["txtDDDTelefone-" + i]);
            telefone.Numero = Request["txtNumeroTelefone-" + i];

            telefone.Save();
        }
    }

    public void ClearData()
    {
        dvScript.InnerHtml = string.Empty;
    }

    public void GetTelefone(Pessoa pessoa)
    {
        dvScript.InnerHtml = "<script type=\"text/javascript\">";
        dvScript.InnerHtml += "jsonTelefones = " + pessoa.Telefones.ToJson() + ";";
        dvScript.InnerHtml += "$(\"#hiddenTelefoneCount\").val(\"0\");";
        dvScript.InnerHtml += "GetTelefones();";
        dvScript.InnerHtml += "</script>";
    }

    public void DisableDivs()
    {
        dvSalvarTelefones.Visible = false;
    }

    public void EnableDivs()
    {
        dvSalvarTelefones.Visible = true;
    }

    private void LoadCombos()
    {
        ddlTiposTelefone.DataSource = new TipoTelefone().Find();
        ddlTiposTelefone.DataTextField = "Nome";
        ddlTiposTelefone.DataValueField = "IDTipoTelefone";
        ddlTiposTelefone.DataBind();
    }
}
