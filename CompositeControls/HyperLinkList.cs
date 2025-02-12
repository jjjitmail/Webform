
#region Using directives
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
#endregion



namespace PrisControlToolkit
{
	#region HyperLinkItem class
	public class HyperLinkItem 
	{
		private string _text;
		private string _url;
		private string _tooltip;

		public HyperLinkItem()
		{
		}
		public HyperLinkItem(string url, string text, string tooltip)
		{
			_text = text;
			_url = url;
			_tooltip = tooltip;
		}
		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}
		public string Tooltip
		{
			get { return _tooltip; }
			set { _tooltip = value; }
		}
		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		public override string ToString()
		{
			return "HyperLink [" + Text + "]";
		}
	}
	#endregion


	#region HyperLinkItemCollection class
	public class HyperLinkItemCollection : List<HyperLinkItem>, IStateManager
	{
		#region Private members
		private bool _marked;
		#endregion

		#region Ctor(s)
		public HyperLinkItemCollection()
		{
			_marked = false;
		}
		#endregion

		#region IStateManager interface
		public bool IsTrackingViewState
		{
			get { return _marked; }
		}

		public void LoadViewState(object state)
		{
			if (state != null)
			{
				Triplet t = (Triplet) state;

				Clear();

				string[] rgUrl = (string[])t.First;
				string[] rgText = (string[])t.Second;
				string[] rgTooltip = (string[])t.Third;

				for (int i = 0; i < rgUrl.Length; i++)
				{
					Add(new HyperLinkItem(rgUrl[i], rgText[i], rgTooltip[i]));
				}
			}
		}

		public object SaveViewState()
		{
			int numOfItems = Count;
			object[] rgTooltip = new string[numOfItems];
			object[] rgText = new string[numOfItems];
			object[] rgUrl = new string[numOfItems];

			for (int i = 0; i < numOfItems; i++)
			{
				rgTooltip[i] = this[i].Tooltip;
				rgText[i] = this[i].Text;
				rgUrl[i] = this[i].Url;
			}

			return new Triplet(rgUrl, rgText, rgTooltip);
		}

		public void TrackViewState()
		{
			_marked = true;
		}
		#endregion
	}
	#endregion


	#region HyperLinkList class
	[Designer("PrisControlToolkit.Design.HyperLinkListDesign")]
	[ParseChildren(true, "Items")]
	[ToolboxData("<{0}:HyperLinkList runat=server></{0}:HyperLinkList>")]
	public class HyperLinkList : System.Web.UI.WebControls.DataBoundControl, INamingContainer, IRepeatInfoUser
	{
		#region Private members
		private HyperLinkItemCollection _items;
		#endregion

		#region Items property
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual HyperLinkItemCollection Items
		{
			get
			{
				if (_items == null)
				{
					_items = new HyperLinkItemCollection();
					if (base.IsTrackingViewState)
						_items.TrackViewState();
				}
				return _items;
			}
		}
		#endregion

		#region DataTextField, DataTooltipField, and DataUrlField properties
		public virtual string DataTextField
		{
			get
			{
				object o = ViewState["DataTextField"];
				if (o == null)
					return "";
				return (string)o;
			}
			set { ViewState["DataTextField"] = value; }
		}

		public virtual string DataTooltipField
		{
			get
			{
				object o = ViewState["DataTooltipField"];
				if (o == null)
					return "";
				return (string)o;
			}
			set { ViewState["DataTooltipField"] = value; }
		}

		public virtual string DataUrlField
		{
			get
			{
				object o = ViewState["DataUrlField"];
				if (o == null)
					return "";
				return (string)o;
			}
			set { ViewState["DataUrlField"] = value; }
		}		
		#endregion

		#region RepeatDirection, RepeatColumns, RepeatLayout properties
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

		#region Control to repeat
		// ***************************************************************************************************
		// PROPERTY (PRIVATE) ControlToRepeat 
		// Represent the tree of controls to repeat in the final list
		private HyperLink _controlToRepeat;
		private HyperLink ControlToRepeat
		{
			get
			{
				if (_controlToRepeat == null)
				{
					_controlToRepeat = new HyperLink();
		//			Controls.Add(_controlToRepeat);
				}
				return _controlToRepeat;
			}
		}
		#endregion

		#region Render method
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

		// ***************************************************************************************************
		// METHOD CreateControlStyle 
		// Create the style object to apply to the control
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}
		#endregion

		#region PerformDataBinding override
		protected override void PerformDataBinding(IEnumerable dataSource)
		{
			base.PerformDataBinding(dataSource);

			string urlField = DataUrlField;
			string textField = DataTextField;
			string tooltipField = DataTooltipField;

			if (dataSource != null)
			{
				// Fill Items
				foreach (object o in dataSource)
				{
					HyperLinkItem item = new HyperLinkItem();
					item.Url = DataBinder.GetPropertyValue(o, urlField, null);
					item.Text = DataBinder.GetPropertyValue(o, textField, null);
					item.Tooltip = DataBinder.GetPropertyValue(o, tooltipField, null);
					Items.Add(item);
				} 
			}
		}
		#endregion

		#region ViewState extra management 
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair p = (Pair) savedState;
				base.LoadViewState(p.First);
				Items.LoadViewState(p.Second);
			}
			else
			{
				base.LoadViewState(null);
			}
		}

		protected override object SaveViewState()
		{
			object baseState = base.SaveViewState();
			object itemState = Items.SaveViewState();
			if ((baseState == null) && (itemState == null))
			{
				return null;
			}
			return new Pair(baseState, itemState);
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
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			HyperLink ctl = ControlToRepeat;
			int i = repeatIndex;
			ctl.ID = i.ToString();
			ctl.Text = Items[i].Text;
			ctl.NavigateUrl = Items[i].Url;
			ctl.ToolTip = Items[i].Tooltip;
			ctl.RenderControl(writer);
		}
		#endregion

	}

	#endregion
}
