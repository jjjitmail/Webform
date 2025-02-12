using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Collections;
using System.Security.Permissions;

namespace UI
{
    [DefaultProperty("Text")]
    [DefaultEvent("SelectedIndexChanged"), 
    ToolboxData("<{0}:ComboBox runat=server></{0}:ComboBox>")]
    public class ComboBox : ControlBase
    {
        private DropDownList PrisDropDownList;
        private Label PrisLabel;
        private string _ItemsField; 
        private string _DefaultWaarde;

        public event EventHandler SelectedIndexChanged
        {
            add { this.PrisDropDownList.SelectedIndexChanged += value; }
            remove { this.PrisDropDownList.SelectedIndexChanged -= value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //PrisDropDownList.SelectedIndexChanged += new EventHandler(PrisDropDownList_SelectedIndexChanged);
        }

        public ComboBox()
        {
            //this.PrisDropDownList.SelectedIndexChanged +=new EventHandler(PrisDropDownList_SelectedIndexChanged);
        }
        [Bindable(true), Category("Default"), DefaultValue(false), Description("AutoPostBack")]
        public bool AutoPostBack
        {
            get
            {
                EnsureChildControls();
                return PrisDropDownList.AutoPostBack;
            }
            set
            {
                EnsureChildControls();
                PrisDropDownList.AutoPostBack = value;
            }
        }


        [Bindable(true), Category("Default"), Description("ItemValue")]
        public string ItemValue
        {
            get { return PrisDropDownList.DataValueField; }
            set
            {
                PrisDropDownList.DataValueField = value;
            }
        }

        [Bindable(true), Category("Default"), Description("ItemText")]
        public string ItemText
        {
            get { return PrisDropDownList.DataTextField; }
            set
            {
                PrisDropDownList.DataTextField = value;
            }
        }

        [Bindable(true), Category("Default"), Description("ItemsSource")]
        public object ItemsSource
        {
            get { return PrisDropDownList.DataSource; }
            set
            {
                PrisDropDownList.DataSource = value;
            }
        }

        [Bindable(true), Category("Default"), DefaultValue(""), Description("DefaultWaarde")]
        public string DefaultWaarde
        {
            get
            {
                return _DefaultWaarde;
            }
            set
            {
                _DefaultWaarde = value;
            }
        }
        /// <summary>
        /// Collection source
        /// </summary>
        [Bindable(true), Category("Default"), DefaultValue(""), Description("ItemsField")]
        public string ItemsField
        {
            get
            {
                return _ItemsField;
            }
            set
            {
                _ItemsField = value;
            }
        }

        [Bindable(true), Category("Default"), DefaultValue(""), Description("GekozenWaarde")]
        public string GekozenWaarde
        {
            get
            {
                EnsureChildControls();
                return PrisDropDownList.SelectedValue;
            }
            set
            {
                EnsureChildControls();
                PrisDropDownList.SelectedValue = value;
            }
        }

        [Bindable(true), Category("Default"), DefaultValue(""), Description("GekozenItem")]
        public ListItem GekozenItem
        {
            get
            {
                EnsureChildControls();
                return PrisDropDownList.SelectedItem;
            }
        }

        [Bindable(true), Category("Default"), DefaultValue(""), Description("GekozenIndex")]
        public int GekozenIndex
        {
            set
            {
                EnsureChildControls();
                PrisDropDownList.SelectedIndex = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("Tekst voor veld")]
        public string LabelTekst
        {
            get
            {
                EnsureChildControls();
                return PrisLabel.Text;
            }
            set
            {
                EnsureChildControls();
                PrisLabel.Text = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("DataSourceID")]
        public string DataSourceID
        {
            get
            {
                EnsureChildControls();
                return PrisDropDownList.DataSourceID;
            }
            set
            {
                EnsureChildControls();
                PrisDropDownList.DataSourceID = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("DataTekstVeld")]
        public string DataTekstVeld
        {
            get
            {
                EnsureChildControls();
                return PrisDropDownList.DataTextField;
            }
            set
            {
                EnsureChildControls();
                PrisDropDownList.DataTextField = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("DataWaardeVeld")]
        public string DataWaardeVeld
        {
            get
            {
                EnsureChildControls();
                return PrisDropDownList.DataValueField;
            }
            set
            {
                EnsureChildControls();
                PrisDropDownList.DataValueField = value;
            }
        }

        public void AddItem(ListItem Item)
        {
            PrisDropDownList.Items.Add(Item);
        }

        public void ClearAllItems()
        {
            PrisDropDownList.Items.Clear();
        }

        public void InitEigenschappen(int value)
        {
            int P = Convert.ToInt16(value);
            PrisDropDownList.Enabled = (P > 1);
            PrisDropDownList.Visible = (P > 0);
            PrisDropDownList.ForeColor = (P < 2) ? Color.Gray : Color.Black;

            PrisLabel.Visible = (P > 0);
            PrisLabel.ForeColor = (P < 2) ? Color.Gray : Color.Black;
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            PrisLabel = new Label();

            PrisDropDownList = new DropDownList();
            PrisDropDownList.ID = "PrisDropDownList";
            //PrisDropDownList.SelectedIndexChanged += new EventHandler(PrisDropDownList_SelectedIndexChanged);
            this.Controls.Add(PrisLabel);
            this.Controls.Add(PrisDropDownList);
        }

        //void PrisDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    OnSelectedIndexChanged_x(new ChangedEventHandler());            
        //}

        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1", false);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            PrisLabel.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            PrisDropDownList.AutoPostBack = AutoPostBack;
            PrisDropDownList.RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }

    }
}
