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
    public class LoginController : AtimosferaController
    {
        [HttpPost]
        public void LogIn()
        {
            Usuario usuario = new Usuario();
            if (!usuario.Logon(Request["login"], Request["senha"]))
            {
                Didox.Business.Session.Add("[Erro]Login", "Usuário e senha inválido");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                return;
            }

            Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrente(false));
        }

        public void PrimeiroAcesso()
        {
            if (!ChaveAtivacao.Validate(Request["chave"], Request["senha"]))
            {
                Didox.Business.Session.Add("[Erro]PrimeiroAcesso", "Chave de acesso inválida");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                return;
            }

            Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrente(false) + "/cadastro");
        }

        public void LogOff()
        {
            Usuario usuario = Usuario.Current();
            if (usuario != null) usuario.Logoff();
            Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrente(false));
        }

        [HttpPost]
        public void EsqueciSenha()
        {
            if (string.IsNullOrEmpty(Request["email"]))
            {
                Didox.Business.Session.Add("[EsqueciSenha]Login", "Email não preenchido.");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                return;
            }

            Usuario usuario = new Usuario();
            usuario.Email = Request["email"];
            usuario.Get();

            if (usuario.IDUsuario == null)
            {
                Didox.Business.Session.Add("[EsqueciSenha]Login", "Email não encontrado.");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                return;
            }

            if (!ConfiguracaoSenha.SenhaEstaCriptografada())
            {
                /*
                MailBox oEmail = new MailBox();
                oEmail.EmailTo = usuario.Email;
                oEmail.Subject = "Envio de senha - TradeVision";
                oEmail.Body = "<b>Envio de senha </b>" +
                    "<br><b>Login:</b> " + usuario.Login+
                    "<br><b>Senha:</b> " + usuario.Senha;
                oEmail.Send();
                 * */

                Didox.Business.Session.Add("[EsqueciSenha]Login", "Email enviado com sucesso.");
            }
            else
            {
                var resetSenhaToken = new ResetSenhaToken();
                resetSenhaToken.Usuario = usuario;
                resetSenhaToken.Token = Guid.NewGuid().ToString();
                resetSenhaToken.Get();
                resetSenhaToken.Save();

                var url = Pagina.Site() + "/" + Pagina.GetAreaCorrente(false) + "/resetsenha?usuario=" + usuario.IDUsuario + "&token=" + resetSenhaToken.Token;
                
                // FIXME desabilitar depois coloquei de teste
                Response.Redirect(url);
                return;

                /*
                MailBox oEmail = new MailBox();
                oEmail.EmailTo = usuario.Email;
                oEmail.Subject = "Reset de senha - TradeVision";
                oEmail.Body = "<b>Por favor clique no link abaixo para resetar a sua senha </b>" +
                    "<br><a href=\"" + url + "\" >Resetar senha</a> ";
                oEmail.Send();
                */

                Didox.Business.Session.Add("[EsqueciSenha]Login", "Um email de reset de senha foi enviado.");
            }

            Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
        }
    }
}
