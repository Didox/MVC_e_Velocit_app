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
    public class PaginasController : AtimosferaController
    {
        [HttpGet]
        public string CarregarPaginasFilhas()
        {
            Response.Cache.SetMaxAge(new TimeSpan(0));
            Response.Buffer = false;
            return new Pagina().GetPaginasFilhas(int.Parse(Request["idPaginaPai"]));
        }
    }
}
