using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace UI
{
    public class LabelTextBox : CompositeControl
    {
        #region Custom Properties
        // ***************************************************************
        // Text
        public string Text
        {
            get
            {
                object o = ViewState["Text"];
                if (o == null)
                    return String.Empty;
                return (string) o;
            }
            set
            {
                ViewState["Text"] = value;
            }
        }

        // ***************************************************************
        // Title
        public string Title
        {
            get
            {
                object o = ViewState["Title"];
                if (o == null)
                    return String.Empty;
                return (string)o;
            }
            set
            {
                ViewState["Title"] = value;
            }
        }

 		#endregion


        #region Rendering

		// ***************************************************************
        // CreateChildControls
        protected override void CreateChildControls()
        {
            // Clears child controls
            Controls.Clear();

            // Build the control tree
            CreateControlHierarchy();
			ClearChildViewState();
		}

        // ***************************************************************
        // CreateControlHierarchy
        protected virtual void CreateControlHierarchy()
        {
            TextBox t = new TextBox();
            Label l = new Label();

            // Configure controls
            t.Text = Text;
            l.Text = Title;

            // Connect to the parent
            Controls.Add(l);
            Controls.Add(t);
        }

        #endregion
    }

	public class PrisTextBox : WebControl, INamingContainer, IPostBackDataHandler
	{
		#region Custom Properties
		// ***************************************************************
		// Text
		public string Text
		{
			get
			{
				object o = ViewState["Text"];
				if (o == null)
					return String.Empty;
				return (string)o;
			}
			set
			{
				ViewState["Text"] = value;
			}
		}

		// ***************************************************************
		// Title
		public string Title
		{
			get
			{
				object o = ViewState["Title"];
				if (o == null)
					return String.Empty;
				return (string)o;
			}
			set
			{
				ViewState["Title"] = value;
			}
		}

		#endregion

        #region IPostBackDataHandler
        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string currentText = this.Text;
            string postedText = postCollection[postDataKey];
            if (!currentText.Equals(postedText, StringComparison.Ordinal))
            {
                Text = postedText;
                return true;
            }
            return false;
        }
        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            return;
        }
        #endregion


        #region Rendering

        // ***************************************************************
		// Render
		protected override void Render(HtmlTextWriter writer)
		{
			string code = String.Format("<span>{0}</span><input name='{2}' type='text' value='{1}'>",
				Title, Text, ClientID);
			writer.Write(code);
		}

		#endregion
	}
}
