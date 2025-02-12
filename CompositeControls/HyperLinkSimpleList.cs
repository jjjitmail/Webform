using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace PrisControlToolkit
{
    public class HyperLinkSimpleList : System.Web.UI.WebControls.ListControl, IRepeatInfoUser
    {
		#region Private members
		// ***************************************************************************************************
		// Private members
		private HyperLink _controlToRepeat;
		#endregion


		#region Properties
		// ***************************************************************************************************
		// PROPERTY (PRIVATE) ControlToRepeat 
		// Represent the tree of controls to repeat in the final list
		private HyperLink ControlToRepeat
		{
			get
			{
				if (_controlToRepeat == null)
				{
					_controlToRepeat = new HyperLink();
					Controls.Add(_controlToRepeat);
				}
				return _controlToRepeat;
			}
		}

		// ***************************************************************************************************
		// PROPERTY RepeatDirection 
		// Gets and sets the direction of the rendering (vertical or horizontal)
		public virtual RepeatDirection RepeatDirection
		{
			get
			{
				object o = ViewState["RepeatDirection"];
				if (o != null)
					return (RepeatDirection)o;
				return RepeatDirection.Vertical;
			}
			set
			{
				ViewState["RepeatDirection"] = value;
			}
		}

		// ***************************************************************************************************
		// PROPERTY RepeatColumns 
		// Gets and sets the number of columns to render out 
		public virtual int RepeatColumns
		{
			get
			{
				object o = ViewState["RepeatColumns"];
				if (o != null)
					return (int)o;
				return 0;
			}
			set
			{
				ViewState["RepeatColumns"] = value;
			}
		}

		// ***************************************************************************************************
		// PROPERTY RepeatLayout 
		// Gets and sets the expected layout for the control's output (flow or table) 
		public virtual RepeatLayout RepeatLayout
		{
			get
			{
				object o = ViewState["RepeatLayout"];
				if (o != null)
					return (RepeatLayout)o;
				return RepeatLayout.Table;
			}
			set
			{
				ViewState["RepeatLayout"] = value;
			}
		}

		#endregion


		#region Overridden Methods
		// ***************************************************************************************************
		// METHOD CreateControlStyle 
		// Create the style object to apply to the control
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		// ***************************************************************************************************
		// METHOD Render 
		// Generates the markup for the control
		protected override void Render(HtmlTextWriter writer)
		{
			if (Items.Count > 0)
			{
				RepeatInfo ri = new RepeatInfo();
				Style controlStyle = (base.ControlStyleCreated ? base.ControlStyle : null);
				ri.RepeatColumns = RepeatColumns;
				ri.RepeatDirection = RepeatDirection;
				ri.RepeatLayout = RepeatLayout;
				ri.RenderRepeater(writer, this, controlStyle, this);
			}
		}
		#endregion


		#region IRepeatInfoUser Interface
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return false;
			}
		}
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return false;
			}
		}
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return false;
			}
		}
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				return this.Items.Count;
			}
		}
		public Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}
		public void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			HyperLink ctl = ControlToRepeat;
			int i = repeatIndex;
			ctl.ID = i.ToString();
			ctl.Text = Items[i].Text;
			ctl.NavigateUrl = Items[i].Text;
			ctl.ToolTip = Items[i].Value;
			ctl.RenderControl(writer);
		}

		#endregion
	}
}
