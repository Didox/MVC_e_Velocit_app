using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Didox.Business;
using System.Collections;

namespace Atimosfera.Controllers
{
    [HandleError]
    public class AdesaoController : AtimosferaController
    {
        [UsuarioLogadoAttribute]
        public string CarregarListaUsuariosAdesao()
        {
            Response.Cache.SetMaxAge(new TimeSpan(0));
            Response.Buffer = false;
            return new Usuario().GetUsuariosAdesao(int.Parse(Request["idHierarquia"]));
        }

        [UsuarioLogadoAttribute]
        public string CarregarComboAdesao()
        {
            Response.Cache.SetMaxAge(new TimeSpan(0));
            Response.Buffer = false;
            return new Usuario().GetComboAdesao(int.Parse(Request["idHierarquia"]), int.Parse(Request["nivelAtual"]));
        }

        [UsuarioLogadoAttribute]
        public string AlterarEnderecoUsuarioHtml()
        {
            Response.Cache.SetMaxAge(new TimeSpan(0));
            Response.Buffer = false;
             Usuario usuario = new Usuario(int.Parse(Request["idUsuario"]));
            usuario.Get();
            return usuario.GetHtmlAlterarEndereco();
        }

        [UsuarioLogadoAttribute]
        public string IncluirResponsavelHtml()
        {
            Response.Cache.SetMaxAge(new TimeSpan(0));
            Response.Buffer = false;
            Usuario usuario = new Usuario();
            if (!string.IsNullOrEmpty(Request["idUsuario"]))
            {
                usuario.IDUsuario = int.Parse(Request["idUsuario"]);
                usuario.Get();
            }

            return usuario.GetHtmlIncluirResponsavelHtml(int.Parse(Request["idHierarquia"]));
        }

        [HttpPost]
        [UsuarioLogadoAttribute]
        public string IncluirResponsavel()
        {
            Response.Cache.SetMaxAge(new TimeSpan(0));
            Response.Buffer = false;
            if (string.IsNullOrEmpty(Request["idHierarquia"])) return "Hierarquia não encontrada.";

            var hierarquia = new Hierarquia(int.Parse(Request["idHierarquia"]));
            hierarquia.Get();
            if (hierarquia.IDHierarquia == null) return "Hierarquia não encontrada.";
            
            Usuario usuario = new Usuario();

            if (!string.IsNullOrEmpty(Request["idUsuario"]))
            {
                usuario.IDUsuario = int.Parse(Request["idUsuario"]);
                usuario.Get();
            }

            if (usuario.IDUsuario == null)
            {
                if (string.IsNullOrEmpty(Request["nome"]) && string.IsNullOrEmpty(Request["email"]) && string.IsNullOrEmpty(Request["login"]))
                    return "Preencha o nome, email ou login, para incluir usuario a estrutura ";

                //return "Preencha o nome, email ou login, para incluir usuario a estrutura (" + hierarquia.Estrutura.Descricao + ") ";

                if (!string.IsNullOrEmpty(Request["login"]))
                {
                    usuario.Login = Request["login"];
                    usuario.Get();
                }

                if (usuario.IDUsuario == null)
                {
                    usuario.Login = null;
                    if (!string.IsNullOrEmpty(Request["email"]))
                    {
                        usuario.Email = Request["email"];
                        usuario.Get();
                    }

                    if (usuario.IDUsuario == null)
                    {
                        if (Request["nome"].Length < 4) return "Digite um nome com minimo de 4 letras.";

                        usuario.Login = null;
                        usuario.Email = null;
                        usuario.Nome = Request["nome"];
                        var iUsuarios = usuario.Find();
                        if (iUsuarios.Count < 1) return "Usuário não encontrado.";

                        var usuarios = new List<Usuario>();
                        iUsuarios.ForEach(u => usuarios.Add((Usuario)u));

                        return usuario.GetHtmlIncluirResponsavelHtml((int)hierarquia.IDHierarquia, usuarios);
                    }
                }
            }

            var credencialPessoa = new UsuarioPessoa();
            credencialPessoa.Usuario = usuario;
            //credencialPessoa.Estrutura = hierarquia.Estrutura;
            credencialPessoa.Campanha = Campanha.Current();
            credencialPessoa.Get();
            if (credencialPessoa.IDUsuarioPessoa != null)
                return "Usuário já estava na estrutura ";
            //return "Usuário já estava na estrutura (" + hierarquia.Estrutura.Descricao + ") ";

            credencialPessoa.Save();

            return "Usuário incluido na estrutura ";
            //return "Usuário incluido na estrutura (" + hierarquia.Estrutura.Descricao + ") ";
        }
    }
}
