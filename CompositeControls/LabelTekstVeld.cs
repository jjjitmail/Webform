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
    [ToolboxData("<{0}:LabelTekstVeld runat=server></{0}:LabelTekstVeld>")]
    public class LabelTekstVeld : ControlBase
    {
        private TextBox PrisTextBox;
        private Label PrisLabel;        

        [Bindable(true), Category("Default"), DefaultValue(""), Description("Tekst Veld")]
        public string Waarde
        {
            get
            {
                EnsureChildControls();
                return PrisTextBox.Text;
            }
            set
            {
                EnsureChildControls();
                PrisTextBox.Text = value;
            }
        }

        [Bindable(true), Category("Default"), DefaultValue(""), Description("Tekst Veld Eigenschap")]
        public int Eigenschap
        {
            set
            {
                EnsureChildControls();
                PrisTextBox.ReadOnly = (value < 2);
                PrisTextBox.Visible = (value > 0);
                PrisTextBox.ForeColor = (value < 2) ? Color.Gray : Color.Black;
                PrisLabel.Visible = (value > 0);
                PrisLabel.ForeColor = (value < 2) ? Color.Gray : Color.Black;
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

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("ReadOnly voor veld")]
        public bool ReadOnly
        {
            get
            {
                EnsureChildControls();
                return PrisTextBox.ReadOnly;
            }
            set
            {
                EnsureChildControls();
                PrisTextBox.ReadOnly = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("TextMode")
        ]
        public TextBoxMode TextMode
        {
            get
            {
                EnsureChildControls();
                return PrisTextBox.TextMode;
            }
            set
            {
                EnsureChildControls();
                PrisTextBox.TextMode = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("Columns")
        ]
        public int Columns
        {
            get
            {
                EnsureChildControls();
                return PrisTextBox.Columns;
            }
            set
            {
                EnsureChildControls();
                PrisTextBox.Columns = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("Rows")
        ]
        public int Rows
        {
            get
            {
                EnsureChildControls();
                return PrisTextBox.Rows;
            }
            set
            {
                EnsureChildControls();
                PrisTextBox.Rows = value;
            }
        }
        
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("MaxLength")
        ]
        public int MaxLength
        {
            get
            {
                EnsureChildControls();
                return PrisTextBox.MaxLength;
            }
            set
            {
                EnsureChildControls();
                PrisTextBox.MaxLength = value;
            }
        }

        public void InitEigenschappen(int value)
        {
            int P = Convert.ToInt16(value);
            PrisTextBox.ReadOnly = (P < 2);
            PrisTextBox.Visible = (P > 0);
            PrisTextBox.ForeColor = (P < 2) ? Color.Gray : Color.Black;

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

            PrisTextBox = new TextBox();
            PrisTextBox.ID = "PrisTextBox";            
            this.Controls.Add(PrisLabel);
            this.Controls.Add(PrisTextBox);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding,"1", false);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            PrisLabel.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            PrisTextBox.RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }
}
