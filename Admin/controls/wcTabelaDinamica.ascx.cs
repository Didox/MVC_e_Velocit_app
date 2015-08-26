using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using Didox.DataBase.Generics;

public partial class controls_wcTabelaDinamica : System.Web.UI.UserControl
{
    private int IdConteudo = 0;
    private bool isPessoaJuridica = false;

    public void SetIdUsuario(int idJuridica)
    {
        IdConteudo = idJuridica;
        isPessoaJuridica = true;
    }

    public void SetIdPessoaJuridica(int idUsuario)
    {
        IdConteudo = idUsuario;
        isPessoaJuridica = false;
    }

    protected void Page_Load(object sender, EventArgs e) { }

    public void SaveTabelaDinamica(Pessoa pessoa)
    {
        var tabela = pessoa.GetTabelaDinamica();
        if (tabela != null && tabela.IDTabela != null)
        {
            foreach (var campo in tabela.Campos)
            {
                campo.Valor(pessoa).SetValor(Request[campo.Nome]);
                campo.Valor(pessoa).Save();
            }
        }
    }
      
    public void GetTabelaDinamica(Pessoa pessoa)
    {
        dvTabelaDinamica.Visible = true;
        if (!CType.Exist(pessoa) || (!CType.Exist(pessoa.Fisica) && !CType.Exist(pessoa.Juridica)))
            dvTabelaDinamica.Visible = false;

        dvTabela.InnerHtml = string.Empty;

        var tabela = pessoa.GetTabelaDinamica();
        if (tabela != null) dvTabela.InnerHtml = tabela.RenderTabela(pessoa);
    }

    public void ClearData(int? idUsuario)
    {
        dvTabela.InnerHtml = string.Empty;
        if (idUsuario == null) return;
        var usuario = new Usuario();
        usuario.IDUsuario = idUsuario;
        usuario.Get();

        if (usuario.ExistePessoa())
        {
            GetTabelaDinamica(usuario.Pessoa);
        }
    }

    public void DisableDivs()
    {
        dvTabelaDinamica.Visible = false;
    }

    protected void btnEditarTabelaFisica_Click(object sender, EventArgs e)
    {
        if (!isPessoaJuridica)
        {
            var usuario = new Usuario();
            usuario.IDUsuario = IdConteudo;
            usuario.Get();

            var tabela = usuario.Pessoa.GetTabelaDinamica();
            if (tabela == null)
                usuario.CreateTabelaDinamica();
            tabela = usuario.Pessoa.GetTabelaDinamica();
            Response.Redirect("~/Tabelas.aspx?idTabela=" + tabela.IDTabela.ToString());
        }
        else
        {
            var PessoaJuridica = new PessoaJuridica();
            PessoaJuridica.IDPessoaJuridica = IdConteudo;
            PessoaJuridica.Get();

            var tabela = PessoaJuridica.GetTabelaDinamica();
            if (tabela == null)
                PessoaJuridica.CreateTabelaDinamica();
            tabela = PessoaJuridica.GetTabelaDinamica();
            Response.Redirect("~/Tabelas.aspx?idTabela=" + tabela.IDTabela.ToString());
        }
    }
}
