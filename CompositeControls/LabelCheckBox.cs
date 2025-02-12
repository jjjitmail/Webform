using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

namespace UI
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:LabelCheckBox runat=server></{0}:LabelCheckBox>")]
    public class LabelCheckBox : ControlBase
    {
        private CheckBox PrisCheckBox;
        private Label PrisLabel;
        private object _Tag;
        private string _ObjectType;

        [Bindable(true), Category("Default"), DefaultValue(""), Description("CheckBox ObjectType")]
        public string ObjectType
        {
            get
            {
                EnsureChildControls();
                return this._ObjectType;
            }
            set
            {
                EnsureChildControls();
                this._ObjectType = value;
            }
        }

        [Bindable(true), Category("Default"), DefaultValue(""), Description("CheckBox tag")]
        public object Tag
        {
            get
            {
                EnsureChildControls();
                return this._Tag;
            }
            set
            {
                EnsureChildControls();
                this._Tag = value;
            }
        }

        [Bindable(true), Category("Default"), DefaultValue(""), Description("CheckBox ischeck")]
        public bool IsCheck
        {
            get
            {
                EnsureChildControls();
                return PrisCheckBox.Checked;
            }
            set
            {
                EnsureChildControls();
                PrisCheckBox.Checked = value;
            }
        }

        [Bindable(true), Category("Default"), DefaultValue(""), Description("Tekst Veld Eigenschap")]
        public int Eigenschap
        {
            set
            {
                EnsureChildControls();
                PrisCheckBox.Enabled = (value < 2);
                PrisCheckBox.Visible = (value > 0);
                PrisCheckBox.ForeColor = (value < 2) ? Color.Gray : Color.Black;
                PrisLabel.Visible = (value > 0);
                PrisLabel.ForeColor = (value < 2) ? Color.Gray : Color.Black;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("ReadOnly voor veld")]
        public bool Enabled
        {
            get
            {
                EnsureChildControls();
                return PrisCheckBox.Enabled;
            }
            set
            {
                EnsureChildControls();
                PrisCheckBox.Enabled = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("Tekst voor CheckBox")]
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

        //[Bindable(true), Category("Appearance"), DefaultValue(""), Description("ReadOnly voor veld")]
        //public bool Enabled
        //{
        //    get
        //    {
        //        EnsureChildControls();
        //        return PrisCheckBox.Enabled;
        //    }
        //    set
        //    {
        //        EnsureChildControls();
        //        PrisCheckBox.Enabled = value;
        //    }
        //}

        public void ToggleCheckBox(bool value)
        {
            PrisCheckBox.Checked = value;
        }

        public void InitEigenschappen(int value)
        {
            int P = value;
            PrisCheckBox.Enabled = (P > 1);
            PrisCheckBox.Visible = (P > 0);
            PrisCheckBox.ForeColor = (P < 2) ? Color.Gray : Color.Black;

            PrisLabel.Visible = (P > 0);
            PrisLabel.ForeColor = (P > 1) ? Color.Black : Color.Gray;
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            PrisLabel = new Label();

            PrisCheckBox = new CheckBox();
            PrisCheckBox.ID = "PrisCheckBox";
            this.Controls.Add(PrisLabel);
            this.Controls.Add(PrisCheckBox);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1", false);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            PrisLabel.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            PrisCheckBox.RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }
}
