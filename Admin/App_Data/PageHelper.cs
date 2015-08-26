using System;
using Didox.Business;

namespace Atmosfera.UI
{
    public class PageHelper : System.Web.UI.Page
    {
        protected override void OnPreLoad(EventArgs e)
        {
            if (Usuario.Current() == null)
            {
                Response.Write("Sua sessão expirou!<br/>");
                Response.Write("Clique <a href='~/'>aqui</a> para começar novamente.");
                Response.End();
            }
        }
    }
}