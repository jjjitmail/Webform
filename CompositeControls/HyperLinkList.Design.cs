
#region Using directives
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using PrisControlToolkit;
#endregion

namespace PrisControlToolkit
{
	namespace Design
	{
		#region HyperLinkListDesign Class
		public class HyperLinkListDesign : ControlDesigner
		{
			#region Constants
			// *******************************************************************************************
			// Constants 
			public const int ItemCount = 6;
			#endregion


			#region Private members
			// *******************************************************************************************
			// Private members
			private HyperLinkList _instance;
			#endregion
			

			#region Ctor(s)
			// *******************************************************************************************
			// Ctor 
			public HyperLinkListDesign() : base()
			{
			}
			#endregion


			#region Overridden methods
			// *******************************************************************************************
			// METHOD: Initialize
			// Initialize the control to render at design-time
			public override void Initialize(System.ComponentModel.IComponent component)
			{
				_instance = (HyperLinkList)component;
				base.Initialize(component);
			}


			// *******************************************************************************************
			// METHOD: GetErrorDesignTimeHtml
			// Returns the HTML to display in the VS IDE in case of error
			protected override string GetErrorDesignTimeHtml(Exception e)
			{
				string msg = "<span style=\"font-family:verdana;font-size:8pt;\">" + e.Message;
				msg += "<hr>" + e.Source + "<hr>" + e.StackTrace + "</span>";
				return msg;
			}


			// *******************************************************************************************
			// METHOD: GetDesignTimeHtml
			// Returns the HTML to display in the VS IDE
			public override string GetDesignTimeHtml() 
			{
				int numOfItems = _instance.Items.Count;
				if (numOfItems == 0)
				{
					_instance.Items.Clear();
					for (int i = 0; i < HyperLinkListDesign.ItemCount; i++)
					{
						HyperLinkItem item = new HyperLinkItem();
						item.Text = "HyperLink #" + i.ToString();
						item.Url = "HyperLink #" + i.ToString();
						_instance.Items.Add(item);
					}
				}

				// Force rendering
				if (_instance.DataSourceID.Length == 0)
					_instance.DataBind();

				// Rendering
				StringWriter sw = new StringWriter();
				HtmlTextWriter writer = new HtmlTextWriter(sw);
				_instance.RenderControl(writer);

				// Remove fake items
				if (numOfItems == 0)
					_instance.Items.Clear();

				return sw.ToString();
			}

			#endregion

		}
		#endregion
	}
}
