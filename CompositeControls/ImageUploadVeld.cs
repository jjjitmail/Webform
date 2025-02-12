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
    [ToolboxData("<{0}:ImageUploadVeld runat=server></{0}:ImageUploadVeld>")]
    public class ImageUploadVeld : ControlBase
    {
        private Label PrisLabel;
        private FileUpload PrisFileUpload;
        private Image PrisImage;
        private Button PrisButton;

        private object tag;
        private string imageId;

        private static readonly object EventClick;
        private static readonly object EventCommand;


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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public HttpPostedFile PostedFile { get { EnsureChildControls(); return PrisFileUpload.PostedFile; } }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("PrisFileUpload")]
        public bool FileUploadVisible
        {
            get
            {
                EnsureChildControls();
                return PrisFileUpload.Visible;
            }
            set
            {
                EnsureChildControls();
                PrisFileUpload.Visible = value;
            }
        }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("ButtonVisible")]
        public bool ButtonVisible
        {
            get
            {
                EnsureChildControls();
                return PrisButton.Visible;
            }
            set
            {
                EnsureChildControls();
                PrisButton.Visible = value;
            }
        }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("ImageVisible")]
        public bool ImageVisible
        {
            get
            {
                EnsureChildControls();
                return PrisImage.Visible;
            }
            set
            {
                EnsureChildControls();
                PrisImage.Visible = value;
            }
        }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The text to display on the button.")]
        public string ButtonText
        {
            get
            {
                EnsureChildControls();
                return PrisButton.Text;
            }
            set
            {
                EnsureChildControls();
                PrisButton.Text = value;
            }
        }
        [Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("ImageUrl")]
        public string ImageUrl
        {
            get
            {
                EnsureChildControls();
                return PrisImage.ImageUrl;
            }
            set
            {
                EnsureChildControls();
                PrisImage.ImageUrl = value;
            }
        }
        
        [Bindable(true),
        Category("Appearance"),
        DefaultValue("ButtonStyleDeleteSmall"),
        Description("CssClass")]
        public string CssClass
        {
            get
            {
                EnsureChildControls();
                return PrisButton.CssClass;
            }
            set
            {
                EnsureChildControls();
                PrisButton.CssClass = value;
            }
        }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(null),
        Description("Tag")]
        public object Tag
        {
            get
            {
                EnsureChildControls();
                return tag;
            }
            set
            {
                EnsureChildControls();
                tag = value;
            }
        }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue(0),
        Description("ImageId")]
        public string ImageId
        {
            get
            {
                EnsureChildControls();
                return imageId;
            }
            set
            {
                EnsureChildControls();
                imageId = value;
            }
        }

        public void InitEigenschappen(int value)
        {
            int P = Convert.ToInt16(value);

            PrisFileUpload.Enabled = (P > 1);
            PrisFileUpload.Visible = (P > 0);
            PrisLabel.ForeColor = (P < 2) ? System.Drawing.Color.Gray : System.Drawing.Color.Black;
            PrisButton.Enabled = (P > 1);
            PrisImage.Visible = (P > 0);
            PrisLabel.Visible = (P > 0);
            PrisButton.Visible = (P > 0);
        }

        // The Submit event.
        [Category("Action"),
        Description("Raised when the user clicks the button.")]
        public event EventHandler Click
        {
            add
            {
                base.Events.AddHandler(EventClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventClick, value);
            }
        }

        [Category("Action"), Description("Button_OnCommand")]
        public event CommandEventHandler Command
        {
            add
            {
                base.Events.AddHandler(EventCommand, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventCommand, value);
            }
        }

        protected virtual void OnClick(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventClick];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnCommand(CommandEventArgs e)
        {
            CommandEventHandler handler = (CommandEventHandler)base.Events[EventCommand];
            if (handler != null)
            {
                handler(this, e);
            }
            base.RaiseBubbleEvent(this, e);
        }

        // Handles the Click event of the Button and raises
        // the Submit event.
        private void _button_Click(object source, EventArgs e)
        {
            OnClick(EventArgs.Empty);
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            PrisLabel = new Label();
            PrisFileUpload = new FileUpload();
            PrisImage = new Image();
            PrisButton = new Button();

            PrisFileUpload.ID = "PrisFileUploadID";
            PrisButton.ID = "PrisButtonID";
            PrisButton.Click += new EventHandler(_button_Click);

            this.Controls.Add(PrisLabel);
            this.Controls.Add(PrisFileUpload);
            this.Controls.Add(PrisImage);
            this.Controls.Add(PrisButton);
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
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            PrisFileUpload.RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            PrisImage.RenderControl(writer);
            PrisButton.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();


            writer.RenderEndTag();
        }
    }
}
