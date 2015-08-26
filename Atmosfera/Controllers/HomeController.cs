using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Didox.Business;
using System.Net.NetworkInformation;

namespace Atimosfera.Controllers
{
    [HandleError]
    public class HomeController : AtimosferaController
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public string CarregarCliente(string slugCliente)
        {
            SetAcesso(slugCliente);
            var pagina = Pagina.HomeCliente();
            if(pagina != null) return pagina.Template.GetConteudoRenderizado();
            throw new TradeVisionError404("Erro 404, Pagina não encontrada.");
        }

        public string CarregarClientePrograma(string slugCliente, string slugPrograma)
        {
            SetAcesso(slugCliente, slugPrograma);
            var pagina = Pagina.HomePrograma();
            if (pagina != null) return pagina.Template.GetConteudoRenderizado();

            pagina = Pagina.GetPaginaCliente(slugPrograma);
            if (pagina != null) return pagina.Template.GetConteudoRenderizado();

            throw new TradeVisionError404("Erro 404, Pagina não encontrada.");
        }

        public string CarregarClienteProgramaCampanha(string slugCliente, string slugPrograma, string slugCampanha)
        {
            SetAcesso(slugCliente, slugPrograma, slugCampanha);
            var pagina = Pagina.HomeCampanha();
            if (pagina != null) return pagina.Template.GetConteudoRenderizado();

            pagina = Pagina.GetPaginaPrograma(slugCampanha);
            if (pagina != null) return pagina.Template.GetConteudoRenderizado();

            throw new TradeVisionError404("Erro 404, Pagina não encontrada.");
        }

        public string CarregarClienteProgramaCampanhaPagina(string slugCliente, string slugPrograma, string slugCampanha, string slugPagina)
        {
            SetAcesso(slugCliente, slugPrograma, slugCampanha);
            var pagina = Pagina.GetPaginaCampanha(slugPagina);
            if (pagina != null) return pagina.Template.GetConteudoRenderizado();

            throw new TradeVisionError404("Erro 404, Pagina não encontrada.");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
