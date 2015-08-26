using System;
using Didox.Business;

namespace TradeVision.UI
{
    public class PageHelper : System.Web.UI.Page
    {
        protected override void OnPreLoad(EventArgs e)
        {
            if (Usuario.Current() == null)
            {
                Response.Redirect("~/");
                Response.End();
            }
        }
    }
}