using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    [ToolboxData("<{0}:FileUpload runat=server></{0}:FileUpload>")]
    public class PrisFileUpload : ControlBase
    {
        private Label PrisLabel;
        private FileUpload _PrisFileUpload;


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
        //[Bindable(true), Category("Appearance"), DefaultValue(""), Description("Enabled")]
        //public bool IsEnabled
        //{
        //    get
        //    {
        //        EnsureChildControls();
        //        return PrisFileUpload.Enabled;
        //    }
        //    set
        //    {
        //        EnsureChildControls();
        //        PrisFileUpload.Enabled = value;
        //    }
        //}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public HttpPostedFile PostedFile { get { EnsureChildControls(); return _PrisFileUpload.PostedFile; } }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("PrisFileUpload")]
        public bool FileUploadVisible
        {
            get
            {
                EnsureChildControls();
                return _PrisFileUpload.Visible;
            }
            set
            {
                EnsureChildControls();
                _PrisFileUpload.Visible = value;
            }
        }

        public void InitEigenschappen(int value)
        {
            int P = Convert.ToInt16(value);

            _PrisFileUpload.Enabled = (P > 1);
            _PrisFileUpload.Visible = (P > 0);
            PrisLabel.ForeColor = (P < 2) ? System.Drawing.Color.Gray : System.Drawing.Color.Black;
            PrisLabel.Visible = (P > 0);
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            PrisLabel = new Label();
            _PrisFileUpload = new FileUpload();

            _PrisFileUpload.ID = "PrisFileUploadID";

            this.Controls.Add(PrisLabel);
            this.Controls.Add(_PrisFileUpload);
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
            _PrisFileUpload.RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderEndTag();

            //AddAttributesToRender(writer);

            //writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1", false);

            //writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            //writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //PrisLabel.RenderControl(writer);
            //writer.RenderEndTag();

            //writer.RenderBeginTag(HtmlTextWriterTag.Td);
            //PrisFileUpload.RenderControl(writer);
            //writer.RenderEndTag();

            //writer.RenderEndTag();
        }
    }
}
