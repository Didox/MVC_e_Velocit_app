using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Xml;

namespace explorer
{
	
	public class createFolder : System.Web.UI.Page
	{
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			Response.Buffer = false;
			
			Response.Cache.SetMaxAge( new TimeSpan(0) );
			Response.Expires = 0;

			if( Request.QueryString[ "folder" ] != null )
			{
                string rootPath = Request.PhysicalApplicationPath;
				string startFolder = ConfigurationSettings.AppSettings[ "startFolder" ];
				string itemReq = Request.QueryString[ "folder" ];
				string folder = rootPath + startFolder + itemReq ;
				Directory.CreateDirectory(  folder );
			}
		}

	}
}
