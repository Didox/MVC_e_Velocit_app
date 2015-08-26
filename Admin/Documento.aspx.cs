using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;
using TradeVision.UI;

public partial class _Documento : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlIDTipoDocumento.DataSource = new TipoDocumento().Find();
            ddlIDTipoDocumento.DataTextField = "Nome";
            ddlIDTipoDocumento.DataValueField = "IDTipoDocumento";
            ddlIDTipoDocumento.DataBind();

            if (Request["idDocumento"] != null)
            {
                GetDocumento(int.Parse(Request["idDocumento"]));
                return;
            }

            GetDocumentos();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var Documento = new Documento();
        try
        {
            if (txtId.Text != "")
            {
                Documento.IDDocumento = int.Parse(txtId.Text);
                Documento.Get();
            }

            Documento.IDPessoa = int.Parse(txtIDPessoa.Text);
            Documento.IDTipoDocumento = int.Parse(ddlIDTipoDocumento.SelectedValue);
            Documento.DescDocumento = txtDescDocumento.Text;
            Documento.DocNumero = txtDocNumero.Text;
            Documento.DocComplemento = txtDocComplemento.Text;
            Documento.DocDV = txtDocDV.Text;

            Documento.Save();
            GetDocumento((int)Documento.IDDocumento);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    private void GetDocumentos()
    {
        dvSalvarDocumento.Visible = false;
        dvListarDocumentos.Visible = true;

        gvDocumentos.DataSource = new Documento().Find();
        gvDocumentos.DataBind();
    }

    private void GetDocumento(int idDocumento)
    {
        dvSalvarDocumento.Visible = true;
        dvListarDocumentos.Visible = false;

        var Documento = new Documento();
        Documento.IDDocumento = idDocumento;
        Documento.Get();

        txtId.Text = Documento.IDDocumento.ToString();
        txtIDPessoa.Text = Documento.IDPessoa.ToString();
        ddlIDTipoDocumento.SelectedValue = Documento.IDTipoDocumento.ToString();
        txtDescDocumento.Text = Documento.DescDocumento.ToString();
        txtDocNumero.Text = Documento.DocNumero.ToString();
        txtDocComplemento.Text = Documento.DocComplemento.ToString();
        txtDocDV.Text = Documento.DocDV.ToString();

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetDocumentos();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarDocumento.Visible = true;
        dvListarDocumentos.Visible = false;

        txtId.Text = "";
        txtIDPessoa.Text = string.Empty;
        ddlIDTipoDocumento.SelectedIndex = 0;
        txtDescDocumento.Text = string.Empty;
        txtDocNumero.Text = string.Empty;
        txtDocComplemento.Text = string.Empty;
        txtDocDV.Text = string.Empty;
    }

    protected void DeleteDocumento(int idDocumento)
    {
        try
        {
            var Documento = new Documento();
            Documento.IDDocumento = idDocumento;
            Documento.Delete();
            GetDocumentos();
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void gvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetDocumento(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "Excluir")
            DeleteDocumento(int.Parse(e.CommandArgument.ToString()));
    }

    protected void gvDocumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDocumentos.PageIndex = e.NewPageIndex;
        GetDocumentos();
    }
}