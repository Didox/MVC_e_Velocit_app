using System;
using System.Web;

namespace Didox.Business
{
    public class Session
    {
        public static void Add(string key, object value)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session.Add(key, value);
        }

        public static void Invalidate(string key)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session.Remove(key);
        }

        public static object Get(string key)
        {
            if (HttpContext.Current == null) return null;
            return HttpContext.Current.Session[key];
        }

        public static void ClearAll()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Session.Clear();
        }
    }
}