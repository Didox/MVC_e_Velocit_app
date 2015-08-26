using System;
using System.Web;

namespace Didox.Business
{
    public class Cookie
    {
        public static void Add(string key, string value)
        {
            if (HttpContext.Current == null) return;
            var cookie = HttpContext.Current.Request.Cookies.Get(key);
            if(cookie == null) cookie = new HttpCookie(key, value);
            cookie.Value = value;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void Invalidate(string key)
        {
            if (HttpContext.Current == null) return;
            var cookie = HttpContext.Current.Request.Cookies.Get(key);
            if (cookie == null) return;
            cookie.Value = string.Empty;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string Get(string key)
        {
            if (HttpContext.Current == null) return null;
            var cookie = HttpContext.Current.Request.Cookies.Get(key);
            if (cookie == null) return null;
            if (string.IsNullOrEmpty(cookie.Value)) return null;
            return cookie.Value;
        }

        public static void ClearAll()
        {
            if (HttpContext.Current == null) return;
            HttpContext.Current.Request.Cookies.Clear();
        }
    }
}