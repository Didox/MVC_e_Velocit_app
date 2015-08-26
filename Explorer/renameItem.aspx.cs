using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Xml;

namespace explorer
{
	
	public class renameItem : System.Web.UI.Page
	{
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			Response.Buffer = false;
			Response.Cache.SetMaxAge( new TimeSpan(0) );
			Response.Expires = 0;

            if (Request.QueryString["oldFile"] != null)
			{
                string rootPath     = Request.PhysicalApplicationPath;
                string xmlPath      = rootPath + ConfigurationSettings.AppSettings["xmlPath"]; 
				string startFolder  = ConfigurationSettings.AppSettings[ "startFolder" ];
                string item         = Request.QueryString["oldFile"];
                item                = item.Replace(startFolder.Replace(@"\", "/"), "");
				string folder       = rootPath + startFolder  + item;
                string newFile      = folder.Replace(Path.GetFileName(folder),"") + Request.QueryString["newFile"];
				
				if( Request.QueryString[ "type" ].ToString() == "folder" && Directory.Exists( folder ) )
				{
                    Directory.Move(folder, newFile);
				}
                else if (File.Exists(folder))
				{
                    File.Move(folder, newFile);
				}
			}
		}

	}
}
