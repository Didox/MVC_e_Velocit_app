using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;

public partial class controls_wcEmail : System.Web.UI.UserControl
{
    private int IdConteudo = 0;
    private bool PessoJuridica = false;

    public void SetIdUsuario(int idUsuario)
    {
        IdConteudo = idUsuario;
        PessoJuridica = true;
    }

    public void SetIdPessoaJuridica(int idJuridica)
    {
        IdConteudo = idJuridica;
        PessoJuridica = false;
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) LoadCombos();
    }

    public void SavePessoaEmail(Pessoa pessoa)
    {
        var emailCount = int.Parse(Request["hiddenEmailCount"]);
        var emailsInvalidos = new List<string>();
        for (int i = 0; i < emailCount; i++)
        {
            if (string.IsNullOrEmpty(Request["txtEmail-" + i])) continue;
            if (!Funcoes.ValidateEmail(Request["txtEmail-" + i]))
                emailsInvalidos.Add(Request["txtEmail-" + i]);
        }

        if (emailsInvalidos.Count > 0)
            throw new TradeVisionValidationError("Endereço de email inválido\n( " + string.Join(",", emailsInvalidos.ToArray()) + " )");

        var emailDel = new Email();
        emailDel.Pessoa = pessoa;
        emailDel.Delete();

        for (int i = 0; i < emailCount; i++)
        {
            if (string.IsNullOrEmpty(Request["txtEmail-" + i])) continue;
            var email = new Email();
            email.Pessoa = pessoa;
            if (!string.IsNullOrEmpty(Request["ddlTiposEmail-" + i]))
                email.IDTipoEmail = int.Parse(Request["ddlTiposEmail-" + i]);
            email.EnderecoEmail = Request["txtEmail-" + i];
            email.Save();
        }
    }

    public void ClearData()
    {
        dvScript.InnerHtml = string.Empty;
    }

    public void GetEmail(Pessoa pessoa)
    {
        dvScript.InnerHtml = "<script type=\"text/javascript\">";
        dvScript.InnerHtml += "jsonEmails = " + pessoa.Emails.ToJson() + ";";
        dvScript.InnerHtml += "$(\"#hiddenEmailCount\").val(\"0\");";
        dvScript.InnerHtml += "GetEmails();";
        dvScript.InnerHtml += "</script>";
    }

    public void DisableDivs()
    {
        dvSalvarEmails.Visible = false;
    }

    public void EnableDivs()
    {
        dvSalvarEmails.Visible = true;
    }

    private void LoadCombos()
    {
        ddlTiposEmail.DataSource = new TipoEmail().Find();
        ddlTiposEmail.DataTextField = "Nome";
        ddlTiposEmail.DataValueField = "IDTipoEmail";
        ddlTiposEmail.DataBind();
    }
}
