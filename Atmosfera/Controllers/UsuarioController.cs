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
    public class UsuarioController : AtimosferaController
    {
        [HttpPost]
        [UsuarioLogadoAttribute]
        public void AlterarSenha()
        {
            Usuario usuarioCurrent = Usuario.Current();
            Usuario usuario = new Usuario();

            if (string.IsNullOrEmpty(Request["senhaAntiga"]))
            {
                Didox.Business.Session.Add("[ERRO]AlterarSenha", "Senha antiga obrigatória.");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                return;
            }

            if (string.IsNullOrEmpty(Request["novaSenha"]))
            {
                Didox.Business.Session.Add("[ERRO]AlterarSenha", "Nova senha obrigatória.");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                return;
            }

            if (string.IsNullOrEmpty(Request["confirmacaoSenha"]))
            {
                Didox.Business.Session.Add("[ERRO]AlterarSenha", "Confirmação de senha obrigatória.");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                return;
            }

            usuario.Senha = Request["senhaAntiga"];
            usuario.Login = usuarioCurrent.Login;
            usuario.Get();

            if (usuario.IDUsuario == null)
            {
                Didox.Business.Session.Add("[ERRO]AlterarSenha", "Senha antiga não confere.");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                return;
            }
            else
            {
                if (Request["novaSenha"] != Request["confirmacaoSenha"])
                {
                    Didox.Business.Session.Add("[ERRO]AlterarSenha", "Confirmação de senha não é igual a senha nova.");
                    Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
                    return;
                }

                usuarioCurrent.Senha = Request["novaSenha"];
                usuarioCurrent.Save();
                Didox.Business.Session.Add("[MSG]AlterarSenha", "Senha alterada com sucesso.");
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
            }
        }

        [HttpPost]
        [UsuarioLogadoAttribute]
        public void AlterarCadastro()
        {
            Usuario usuario = Usuario.Current();
            try
            {
                usuario.Nome = Request["nome"];
                usuario.Email = Request["email"];
                usuario.Ramal = Request["ramal"];
                /*
                var pessoa = usuario.Fisica;
                foreach (var campo in pessoa.Tabela.Campos)
                {
                    try
                    {
                        campo.Valor(pessoa).SetValor(Request[campo.Nome]);
                        campo.Valor(pessoa).Save();
                    }
                    catch (Exception err)
                    {
                        campo.HasError = true;
                        throw err;
                    }
                }
                 */

                usuario.Save();
                Didox.Business.Session.Add("[MSG]AlterarCadastro", "Alteração concluída.");
            }
            catch (Exception err)
            {
                Didox.Business.Session.Add("[ERRO]AlterarCadastro", err.Message);
            }

            Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrenteComPagina());
        }

        [UsuarioLogadoAttribute]
        public string AlterarStatusUsuario()
        {
            try
            {
                Usuario usuario = new Usuario(int.Parse(Request["idUsuario"]));
                usuario.Get();
                usuario.Ativo = Convert.ToBoolean(int.Parse(Request["status"]));
                usuario.Save();
                return "Status alterado com sucesso.";
            }
            catch { }

            return "Erro ao alterar status do usuário.";
        }

        public string ResetSenha()
        {
            var resetSenhaToken = new ResetSenhaToken();
            resetSenhaToken.IDUsuario = int.Parse(Request["usuario"]);
            resetSenhaToken.Token = Request["token"];
            resetSenhaToken.Get();
            if (resetSenhaToken.IDResetSenhaToken == null)
            {                
                var url = Request.ServerVariables["URL"].Replace("resetsenha", "");
                Response.Redirect(url);
                return string.Empty;
            }

            var user = new Usuario(resetSenhaToken.IDUsuario);
            user.Get();
            user.AdicionaSessao();            
            resetSenhaToken.Delete();
            Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrente(false) +  "/alterar-senha");
            return string.Empty; 
        }

        [UsuarioLogadoAttribute]
        public string ResetSenhaUsuario()
        {
            try
            {
                Usuario usuario = new Usuario(int.Parse(Request["idUsuario"]));
                usuario.Get();
                usuario.Senha = new Random().Next(0, 999999).ToString();
                usuario.Save();

                        /*
                  MailBox oEmail = new MailBox();
                  oEmail.EmailTo = usuario.Email;
                  oEmail.Subject = "Envio de senha - TradeVision";
                  oEmail.Body = "<b>Envio de senha </b>" +
                      "<br><b>Login:</b> " + usuario.Login+
                      "<br><b>Senha:</b> " + usuario.Senha;
                  oEmail.Send();
                   * */

                return "Nova senha enviada  para o email " + usuario.Email + ".";
            }
            catch { }

            return "Erro ao alterar senha do usuário.";
        }

        [UsuarioLogadoAttribute]
        public string EnvioSenhaUsuario()
        {
            try
            {
                Usuario usuario = new Usuario(int.Parse(Request["idUsuario"]));
                usuario.Get();

                /*
                  MailBox oEmail = new MailBox();
                  oEmail.EmailTo = usuario.Email;
                  oEmail.Subject = "Envio de senha - TradeVision";
                  oEmail.Body = "<b>Envio de senha </b>" +
                      "<br><b>Login:</b> " + usuario.Login+
                      "<br><b>Senha:</b> " + usuario.Senha;
                  oEmail.Send();
                   * */

                return "Nova senha enviada  para o email " + usuario.Email + ".";
            }
            catch { }

            return "Erro ao alterar senha do usuário.";
        }

        [UsuarioLogadoAttribute]
        public string SalvarAlteracoesUsuario()
        {
            try
            {
                Usuario usuario = new Usuario(int.Parse(Request["idUsuario"]));
                usuario.Get();
                usuario.Nome = Request["nome"];
                usuario.Email = Request["email"];
                usuario.Save();
                return "1";
            }
            catch (Exception err) { return err.Message; }
        }
    }
}
