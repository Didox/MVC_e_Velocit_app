using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;

namespace explorer
{
	public class Upload : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton rdUnzip;
		protected System.Web.UI.WebControls.ImageButton imgUp;
		protected System.Web.UI.WebControls.RadioButton rdNothing;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileUp;

		private void InitializeComponent()
		{
			this.imgUp.Click += new System.Web.UI.ImageClickEventHandler(this.imgUp_Click);
			this.imgUp.Load += new System.EventHandler(this.imgUp_Load);

		}
	
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
	
		#endregion

		private void imgUp_Load(object sender, System.EventArgs e)
		{
			imgUp.Attributes.Add( "onclick" , "return checkFile()" );
		}

		private void imgUp_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				string actualFolder	 = Request.Cookies[ "actualFolder" ].Value + "\\";
                string rootPath = Request.PhysicalApplicationPath;
				string startFolder   = ConfigurationSettings.AppSettings[ "startFolder" ];
				string file          = Path.GetFileName( fileUp.PostedFile.FileName );

				string fileToSave = rootPath + startFolder + actualFolder + file;
				fileUp.PostedFile.SaveAs( Server.UrlDecode( fileToSave ) ); 
				Page.RegisterStartupScript( "","<script>window.parent.uploaded()</script>" );
			}
			catch( Exception )
			{
				Page.RegisterStartupScript( "","<script>window.parent.uploadError()</script>" );
			}

		}

	}
}
