using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Didox.Business;

namespace Atimosfera.Controllers
{
    public class AtimosferaController : Controller
    {
        public class UsuarioLogadoAttribute : ActionFilterAttribute
        {
            public UsuarioLogadoAttribute()
            {
                var usuario = Usuario.Current();
                if (usuario == null)
                    throw new TradeVisionError403("Usuário não logado.");
            }
        }

        public void SetAcesso(string slugCliente)
        {
            if (slugCliente.Contains('.')) return;
            Cliente cliente = new Cliente(slugCliente);
            cliente.Get();
            if (cliente.IDCliente != null)
            {
                Cliente.Dispose();
                Programa.Dispose();
                Campanha.Dispose();

                cliente.BuscaAdicionaCurrent();
            }
            else Cliente.Dispose();                
        }

        public void SetAcesso(string slugCliente, string slugPrograma)
        {
            if (slugCliente.Contains('.') || slugPrograma.Contains('.')) return;
            Cliente cliente = new Cliente(slugCliente);
            cliente.Get();
            if (cliente.IDCliente != null)
            {
                Cliente.Dispose();
                Programa.Dispose();
                Campanha.Dispose();

                cliente.BuscaAdicionaCurrent();

                Programa programa = new Programa(slugPrograma);
                programa.Cliente = cliente;
                programa.Get();

                if (programa.IDPrograma != null)
                {
                    programa.BuscaAdicionaCurrent();
                }
            }
            else Cliente.Dispose();
        }

        public void SetAcesso(string slugCliente, string slugPrograma, string slugCampanha)
        {
            if (slugCliente.Contains('.') || slugPrograma.Contains('.') || slugCampanha.Contains('.')) return;
            Cliente cliente = new Cliente(slugCliente);
            cliente.Get();
            if (cliente.IDCliente != null)
            {
                Cliente.Dispose();
                Programa.Dispose();
                Campanha.Dispose();

                cliente.BuscaAdicionaCurrent();

                Programa programa = new Programa(slugPrograma);
                programa.Cliente = cliente;
                programa.Get();

                if (programa.IDPrograma != null)
                {
                    programa.BuscaAdicionaCurrent();


                    Campanha campanha = new Campanha(slugCampanha);
                    campanha.Programa = programa;
                    campanha.Get();

                    if (campanha.IDCampanha != null)
                    {
                        campanha.BuscaAdicionaCurrent();
                    }
                }
            }
            else Cliente.Dispose();
        }
    }
}
