using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using TradeVision.UI;

public partial class _Grupos : PageHelper
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["idGrupo"] != null)
            {
                GetGrupo(int.Parse(Request["idGrupo"]));
                return;
            }

            GetGrupos();
        }
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        var grupo = new Grupo();
        try
        {
            if (txtId.Text != "")
            {
                grupo.IDGrupo = int.Parse(txtId.Text);
                grupo.Get();
            }

            grupo.Descricao = txtNome.Text;
            grupo.Save();
            GetGrupo((int)grupo.IDGrupo);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('Registro salvo.')</script>");
        }
        catch (Exception err)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>alert('" + FormatError.FormatMessageForJAlert(err.Message) + "')</script>");
        }
    }

    protected void btnFiltar_Click(object sender, EventArgs e)
    {
        GetGrupos();
    }

    private void GetGrupos()
    {
        dvSalvarGrupo.Visible = false;
        dvListarGrupos.Visible = true;
        dvAddPaginas.Visible = false;
        dvAddUsuarios.Visible = false;

        gvGrupos.DataSource = new Grupo(txtNomeGrupo.Text).Find();
        gvGrupos.DataBind();
    }

    private void GetGrupo(int idGrupo)
    {
        dvSalvarGrupo.Visible = true;
        dvListarGrupos.Visible = false; 
        dvAddPaginas.Visible = false;
        dvAddUsuarios.Visible = false;

        var grupo = new Grupo();
        grupo.IDGrupo = idGrupo;
        grupo.Get();

        txtId.Text = grupo.IDGrupo.ToString();
        txtNome.Text = grupo.Descricao;        
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        GetGrupos();
    }

    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        dvSalvarGrupo.Visible = true;
        dvListarGrupos.Visible = false; 
        dvAddPaginas.Visible = false;
        dvAddUsuarios.Visible = false;

        txtId.Text = "";
        txtNome.Text = "";
    }

    protected void gvGrupos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
            GetGrupo(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "AddPaginas")
            GetAddPaginas(int.Parse(e.CommandArgument.ToString()));
        else if (e.CommandName == "AddUsuarios")
            GetAddUsuarios(int.Parse(e.CommandArgument.ToString()));
    }

    private void GetAddUsuarios(int idGrupo)
    {
        txtId.Text = idGrupo.ToString();
        dvSalvarGrupo.Visible = false;
        dvListarGrupos.Visible = false;
        dvAddPaginas.Visible = false;
        dvAddUsuarios.Visible = true;

        listUsuarios.DataSource = new Usuario().Find();
        listUsuarios.DataBind();

        var grupoUsuario = new GrupoUsuario();
        grupoUsuario.IDGrupo = idGrupo;
        listUsuariosAdd.DataSource = grupoUsuario.GetUsuarios();
        listUsuariosAdd.DataBind();

        foreach (ListItem usuario in listUsuariosAdd.Items)
        {
            var listItem = listUsuarios.Items.FindByValue(usuario.Value);
            listUsuarios.Items.Remove(listItem);
        }
    }

    private void GetAddPaginas(int idGrupo)
    {
        txtId.Text = idGrupo.ToString();
        dvSalvarGrupo.Visible = false;
        dvListarGrupos.Visible = false;
        dvAddPaginas.Visible = true;
        dvAddUsuarios.Visible = false;

        listPaginas.DataSource = Pagina.GetPaginas(string.Empty, true);
        listPaginas.DataBind();

        var grupoPagina = new GrupoPagina();
        grupoPagina.IDGrupo = idGrupo;
        listPaginasAdd.DataSource = grupoPagina.GetPaginas();
        listPaginasAdd.DataBind();

        foreach (ListItem pagina in listPaginasAdd.Items)
        {
            var listItem = listPaginas.Items.FindByValue(pagina.Value);
            listPaginas.Items.Remove(listItem);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        var list = new List<ListItem>();
        foreach (ListItem item in listPaginasAdd.Items)
        {
            if (item.Selected)
            {
                listPaginas.Items.Add(item);
                list.Add(item);
            }
        }

        foreach (ListItem item in list) listPaginasAdd.Items.Remove(item);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        var grupoPaginaDelete = new GrupoPagina();
        grupoPaginaDelete.IDGrupo = int.Parse(txtId.Text);
        grupoPaginaDelete.Delete();

        foreach (ListItem item in listPaginas.Items)
        {
            if (item.Selected)
            {
                var grupoPagina = new GrupoPagina();
                grupoPagina.IDPagina = int.Parse(item.Value);
                grupoPagina.IDGrupo = int.Parse(txtId.Text);
                grupoPagina.Save();
            }
        }

        foreach (ListItem item in listPaginasAdd.Items)
        {
            var grupoPagina = new GrupoPagina();
            grupoPagina.IDPagina = int.Parse(item.Value);
            grupoPagina.IDGrupo = int.Parse(txtId.Text);
            grupoPagina.Save();
        }

        GetAddPaginas((int)grupoPaginaDelete.IDGrupo);
    }

    protected void btnVoltarGruposPaginas_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Grupos.aspx?idGrupo=" + txtId.Text);
    }

    protected void btnVoltarGruposUsuario_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Grupos.aspx?idGrupo=" + txtId.Text);
    }

    protected void btnDeleteUsuario_Click(object sender, EventArgs e)
    {
        var list = new List<ListItem>();
        foreach (ListItem item in listUsuariosAdd.Items)
        {
            if (item.Selected)
            {
                listUsuarios.Items.Add(item);
                list.Add(item);
            }
        }

        foreach (ListItem item in list) listUsuariosAdd.Items.Remove(item);
    }

    protected void btnAddUsuario_Click(object sender, EventArgs e)
    {
        var grupoUsuarioDelete = new GrupoUsuario();
        grupoUsuarioDelete.IDGrupo = int.Parse(txtId.Text);
        grupoUsuarioDelete.Delete();

        foreach (ListItem item in listUsuarios.Items)
        {
            if (item.Selected)
            {
                var grupoUsuario = new GrupoUsuario();
                grupoUsuario.IDUsuario = int.Parse(item.Value);
                grupoUsuario.IDGrupo = int.Parse(txtId.Text);
                grupoUsuario.Get();
                grupoUsuario.Save();
            }
        }

        foreach (ListItem item in listUsuariosAdd.Items)
        {
            var grupoUsuario = new GrupoUsuario();
            grupoUsuario.IDUsuario = int.Parse(item.Value);
            grupoUsuario.IDGrupo = int.Parse(txtId.Text);
            grupoUsuario.Get();
            grupoUsuario.Save();
        }

        GetAddUsuarios((int)grupoUsuarioDelete.IDGrupo);
    }
}
