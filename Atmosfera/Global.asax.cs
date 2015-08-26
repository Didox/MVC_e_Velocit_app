using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Didox.Business;

namespace Atimosfera
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.gif/{*pathInfo}");

            #region Rotas
            routes.MapRoute(
                "login", // Route name
                "login", // URL with parameters
                new { controller = "Login", action = "LogIn" } // Parameter defaults
            );

            routes.MapRoute(
               "primeiro_acesso", // Route name
               "PrimeiroAcesso", // URL with parameters
               new { controller = "Login", action = "PrimeiroAcesso" } // Parameter defaults
            );

            routes.MapRoute(
                 "alterar_cadastro", // Route name
                 "AlterarCadastro", // URL with parameters
                 new { controller = "Usuario", action = "AlterarCadastro" } // Parameter defaults
            );

            routes.MapRoute(
                "logoff", // Route name
                "logoff", // URL with parameters
                new { controller = "Login", action = "LogOff" } // Parameter defaults
            );

            routes.MapRoute(
              "alterar_status_usuario", // Route name
              "AlterarStatusUsuario", // URL with parameters
               new { controller = "Usuario", action = "AlterarStatusUsuario" } // Parameter defaults
            );

            routes.MapRoute(
              "reset_senha_usuario", // Route name
              "ResetSenhaUsuario", // URL with parameters
               new { controller = "Usuario", action = "ResetSenhaUsuario" } // Parameter defaults
            );

            routes.MapRoute(
              "envio_senha_usuario", // Route name
              "EnvioSenhaUsuario", // URL with parameters
               new { controller = "Usuario", action = "EnvioSenhaUsuario" } // Parameter defaults
            );
            
            routes.MapRoute(
              "Salvar_alteracoes_usuario", // Route name
              "SalvarAlteracoesUsuario", // URL with parameters
               new { controller = "Usuario", action = "SalvarAlteracoesUsuario" } // Parameter defaults
            );            

            routes.MapRoute(
              "alterar_endereco_usuario_html", // Route name
              "AlterarEnderecoUsuarioHtml", // URL with parameters
               new { controller = "Adesao", action = "AlterarEnderecoUsuarioHtml" } // Parameter defaults
            ); 

            routes.MapRoute(
              "incluir_responsavel_html", // Route name
              "IncluirResponsavelHtml", // URL with parameters
               new { controller = "Adesao", action = "IncluirResponsavelHtml" } // Parameter defaults
            );

            routes.MapRoute(
              "incluir_responsavel", // Route name
              "IncluirResponsavel", // URL with parameters
               new { controller = "Adesao", action = "IncluirResponsavel" } // Parameter defaults
            ); 
            
            routes.MapRoute(
              "carregar_lista_usuarios_adesao", // Route name
              "CarregarListaUsuariosAdesao", // URL with parameters
               new { controller = "Adesao", action = "CarregarListaUsuariosAdesao" } // Parameter defaults
            );

            routes.MapRoute(
              "carregar_combo_adesao", // Route name
              "CarregarComboAdesao", // URL with parameters
               new { controller = "Adesao", action = "CarregarComboAdesao" } // Parameter defaults
            );

            routes.MapRoute(
              "home", // Route name
              "home", // URL with parameters
               new { controller = "Home", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "esqueci_senha", // Route name
                "EsqueciSenha", // URL with parameters
                new { controller = "Login", action = "EsqueciSenha" } // Parameter defaults
            );

            routes.MapRoute(
                "alterar_senha", // Route name
                "AlterarSenha", // URL with parameters
                new { controller = "Usuario", action = "AlterarSenha" } // Parameter defaults
            );

            routes.MapRoute(
                "carregarpaginasfilhas", // Route name
                "CarregarPaginasFilhas", // URL with parameters
                new { controller = "Paginas", action = "CarregarPaginasFilhas" } // Parameter defaults
            );

            routes.MapRoute(
                "resetsenha", // Route name
                "{slugCliente}/resetsenha", // URL with parameters
                new { controller = "Usuario", action = "ResetSenha" } // Parameter defaults
            );

            routes.MapRoute(
                "resetsenha2", // Route name
                "{slugCliente}/{slugPrograma}/resetsenha", // URL with parameters
                new { controller = "Usuario", action = "ResetSenha" } // Parameter defaults
            );
                        
            routes.MapRoute(
                "resetsenha3", // Route name
                "{slugCliente}/{slugPrograma}/{slugCampanha}/resetsenha", // URL with parameters
                new { controller = "Usuario", action = "ResetSenha" } // Parameter defaults
            );

            routes.MapRoute(
                "cliente", // Route name
                "{slugCliente}", // URL with parameters
                new { controller = "Home", action = "CarregarCliente"} // Parameter defaults
            );

            routes.MapRoute(
                "cliente_programa", // Route name
                "{slugCliente}/{slugPrograma}", // URL with parameters
                new { controller = "Home", action = "CarregarClientePrograma" } // Parameter defaults
            );
           
            routes.MapRoute(
                "cliente_programa_campanha", // Route name
                "{slugCliente}/{slugPrograma}/{slugCampanha}", // URL with parameters
                new { controller = "Home", action = "CarregarClienteProgramaCampanha" } // Parameter defaults
            );

            routes.MapRoute(
                "cliente_programa_campanha_pagina", // Route name
                "{slugCliente}/{slugPrograma}/{slugCampanha}/{slugPagina}", // URL with parameters
                new { controller = "Home", action = "CarregarClienteProgramaCampanhaPagina" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            #endregion
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception erro = Server.GetLastError().GetBaseException();
            if (erro is TradeVisionError404)
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrente(false) + "/" + Pagina.ERROR404);
            else if (erro is TradeVisionError403)
                Response.Redirect(Pagina.Site() + "/" + Pagina.GetAreaCorrente(false) + "/" + Pagina.ERROR403);
        }
    }
}