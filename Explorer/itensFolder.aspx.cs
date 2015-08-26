using System;
using System.Web;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Web.UI;

namespace explorer
{
	
	public class itensFolder : Page
	{
		protected override void Render( HtmlTextWriter writer)
		{
				Response.ContentType = "text/xml";
				Response.Buffer = false;
				Response.Cache.SetMaxAge( new TimeSpan(0) );

                string rootPath = Request.PhysicalApplicationPath;
                string xmlPath = rootPath + @"\xml"; 
				string startFolder   = ConfigurationSettings.AppSettings[ "startFolder" ];
				string url           = ConfigurationSettings.AppSettings[ "url" ];
				string path			 = rootPath + startFolder + Request.QueryString[ "folder"];
		
				DirectoryInfo root	       = new DirectoryInfo( path );
				DirectoryInfo[] folders    = root.GetDirectories();
				FileInfo[] files		   = root.GetFiles();
			
				XmlDocument xmlDoc = new XmlDocument();
				XmlNode xmlnode = xmlDoc.CreateNode( XmlNodeType.XmlDeclaration,"","" );
				xmlDoc.AppendChild( xmlnode );
				XmlElement explorer = xmlDoc.CreateElement( "explorer" );
								
				foreach( DirectoryInfo d in folders )
				{
					XmlElement folder = xmlDoc.CreateElement( "item" );

					XmlElement name = xmlDoc.CreateElement( "name" );
					name.InnerText = d.Name;

					XmlElement image = xmlDoc.CreateElement( "image" );
					image.InnerText = ConfigurationSettings.AppSettings[ "imageFolder" ];
						
					XmlElement pathXml = xmlDoc.CreateElement( "path" );
					pathXml.InnerText = d.FullName.Replace( rootPath + startFolder , "" );

					XmlElement type = xmlDoc.CreateElement( "type" );
					type.InnerText = "folder";

					XmlElement description = xmlDoc.CreateElement( "description" );
					description.InnerText = ConfigurationSettings.AppSettings[ "folderDescription" ];

					folder.AppendChild( name );
					folder.AppendChild( image );
					folder.AppendChild( pathXml );
					folder.AppendChild( type );
					folder.AppendChild( description );
					explorer.AppendChild( folder );
				}
				
				string extImages = ConfigurationSettings.AppSettings[ "extImages" ];
				
				foreach( FileInfo f in files )
				{
                    if ((f.Attributes | FileAttributes.Hidden) != f.Attributes)
                    {
                        XmlElement file = xmlDoc.CreateElement("item");

                        XmlElement name = xmlDoc.CreateElement("name");
                        name.InnerText = f.Name;

                        XmlElement image = xmlDoc.CreateElement("image");
                        image.InnerText = ConfigurationSettings.AppSettings[f.Extension.ToLower()];

                        XmlElement pathXml = xmlDoc.CreateElement("path");
                        pathXml.InnerText = f.FullName.Replace(rootPath, "");
                        pathXml.InnerText = pathXml.InnerText.Replace(@"\", "/");

                        XmlElement type = xmlDoc.CreateElement("type");
                        type.InnerText = (extImages.IndexOf(f.Extension.ToLower()) > -1) ? "image" : "file";

                        XmlElement description = xmlDoc.CreateElement("description");
                        description.InnerText = ConfigurationSettings.AppSettings[f.Extension.ToLower() + "_Description"];

                        file.AppendChild(name);
                        file.AppendChild(image);
                        file.AppendChild(pathXml);
                        file.AppendChild(type);
                        file.AppendChild(description);
                        explorer.AppendChild(file);
                    }
				}

				string actualFolder = ( Request.QueryString[ "folder" ] == "" ) ? "raiz" : Request.QueryString[ "folder"].Replace( "\\","_" );
                string nameFile = xmlPath + @"\" + actualFolder + ".xml";
					
				xmlDoc.AppendChild( explorer );				
				Response.Write( xmlDoc.InnerXml );
				
		}
	}
}
