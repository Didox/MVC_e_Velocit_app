using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Xml;

namespace explorer
{
	
	public class deleteItem : System.Web.UI.Page
	{
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			Response.Buffer = false;
			Response.Cache.SetMaxAge( new TimeSpan(0) );
			Response.Expires = 0;

			if( Request.QueryString[ "item" ] != null )
			{
                string rootPath     = Request.PhysicalApplicationPath;
                string xmlPath      = rootPath + ConfigurationSettings.AppSettings["xmlPath"]; 
				string startFolder  = ConfigurationSettings.AppSettings[ "startFolder" ];
				string item         = Request.QueryString[ "item" ];
				string folder       = rootPath + startFolder  + item;
                string file         = rootPath + item;
				
				if( Request.QueryString[ "type" ].ToString() == "folder" && Directory.Exists( folder ) )
				{
					Directory.Delete( folder , true );

                    if (File.Exists(xmlPath + item.Replace("\\", "_") + ".xml"))
					{
                        DirectoryInfo dInfo = new DirectoryInfo(xmlPath);
						FileInfo []fInfo = dInfo.GetFiles();
						foreach( FileInfo fileInfo in fInfo )
						{
							if( fileInfo.Name.StartsWith( item.Replace( "\\","_" ) ) )
								File.Delete( fileInfo.FullName );
						}
					}
				}
				else if( File.Exists( file ) )
				{
					File.Delete( file );
				}
			}
		}

	}
}
