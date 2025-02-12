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
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CalendarVeld runat=server></{0}:CalendarVeld>")]
    public class CalendarVeld : ControlBase
    {
        //To retrieve value i am using textbox
        private TextBox _TxtDate = new TextBox();
        // Image to select the calender date
        private Image _ImgDate = new Image();
        // Image URL to expose the image URL Property
        private string _ImageUrl;
        // Exposing autopostback property 
        private bool _AutoPostBack;
        // property get the value from CalendarVeld.
        private string _Value;
        //CSS class to design the Image
        private string _ImageCssClass;
        //CSS class to design the TextBox
        private string _TextBoxCssClass;

        private Label PrisLabel = new Label();

        /**** properties***/

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string ImageUrl
        {
            set
            {
                this._ImageUrl = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public string Waarde
        {
            get
            {

                return _Value = _TxtDate.Text;
            }

            set
            {
                _Value = _TxtDate.Text = value;
            }
        }
        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public bool AutoPostBack
        {
            get
            {
                return _AutoPostBack;
            }

            set
            {
                _AutoPostBack = value;
            }
        }
        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public string ImageCssClass
        {
            get
            {
                return _ImageCssClass;
            }

            set
            {
                _ImageCssClass = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Localizable(true)]
        public string TextBoxCssClass
        {
            get
            {
                return _TextBoxCssClass;
            }

            set
            {
                _TextBoxCssClass = value;
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

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("Columns")]
        public int Columns
        {
            get
            {
                EnsureChildControls();
                return _TxtDate.Columns;
            }
            set
            {
                EnsureChildControls();
                _TxtDate.Columns = value;
            }
        }

        [Bindable(true), Category("Custom"), DefaultValue(""), Localizable(true)]
        public string CommandName
        {
            get
            {
                string s = ViewState["CommandName"] as string;
                return s == null ? String.Empty : s;
            }
            set
            {
                ViewState["CommandName"] = value;
            }
        }

        [Bindable(true), Category("Custom"), DefaultValue(""), Localizable(true)]
        public string CommandArgument
        {
            get
            {
                string s = ViewState["CommandArgument"] as string;
                return s == null ? String.Empty : s;
            }
            set
            {
                ViewState["CommandArgument"] = value;
            }
        }

        protected static readonly object EventCommandObj = new object();

        public event CommandEventHandler Command
        {
            add
            {
                Events.AddHandler(EventCommandObj, value);
            }
            remove
            {
                Events.RemoveHandler(EventCommandObj, value);
            }
        }

        public void InitEigenschappen(int value)
        {
            // 0=hidden, 1= readonly, 2=edit
            int P = Convert.ToInt16(value);

            _ImgDate.Visible = (P > 1);

            _TxtDate.ReadOnly = (P < 2);
            _TxtDate.Visible = (P > 0);
            _TxtDate.ForeColor = (P < 2) ? System.Drawing.Color.Gray : System.Drawing.Color.Black;

            PrisLabel.Visible = (P > 0);
            PrisLabel.ForeColor = (P < 2) ? System.Drawing.Color.Gray : System.Drawing.Color.Black;
        }

        //this will raise the bubble event
        protected virtual void OnCommand(CommandEventArgs commandEventArgs)
        {
            CommandEventHandler eventHandler = (CommandEventHandler)Events[EventCommandObj];
            if (eventHandler != null)
            {
                eventHandler(this, commandEventArgs);
            }
            base.RaiseBubbleEvent(this, commandEventArgs);
        }
        //this will be initialized to  OnTextChanged event on the normal textbox
        private void OnTextChanged(object sender, EventArgs e)
        {
            if (this.AutoPostBack)
            {
                //pass the event arguments to the OnCommand event to bubble up
                CommandEventArgs args = new CommandEventArgs(this.CommandName, this.CommandArgument);
                OnCommand(args);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            //AddStyleSheet();
            //AddJavaScript();
            base.OnInit(e);

            // For TextBox
            // setting name for textbox. using t just to concat with this.ID for unqiueName
            _TxtDate.ID = this.ID + "t";
            // setting postback
            _TxtDate.AutoPostBack = this.AutoPostBack;
            // giving the textbox default value for date
            _TxtDate.Text = this.Waarde;
            //Initializing the TextChanged with our custom event to raise bubble event
            _TxtDate.TextChanged += new System.EventHandler(this.OnTextChanged);
            //Setting textbox to readonly to make sure user dont play with the textbox
            //_TxtDate.Attributes.Add("readonly", "readonly");
            // adding stylesheet 
            _TxtDate.Attributes.Add("class", this.TextBoxCssClass);

            // For Image
            // setting alternative name for image
            _ImgDate.AlternateText = "imageURL";
            if (!string.IsNullOrEmpty(_ImageUrl))
                _ImgDate.ImageUrl = _ImageUrl;
            
            //setting name for image
            _ImgDate.ID = this.ID + "i";
            //setting image class for textbox
            _ImgDate.Attributes.Add("class", this.ImageCssClass);
        }

        /// <summary>
        /// adding child controls to composite control
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Add(PrisLabel);
            this.Controls.Add(_TxtDate);
            this.Controls.Add(_ImgDate);
            base.CreateChildControls();
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1", false);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            PrisLabel.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            _TxtDate.RenderControl(writer);
            _ImgDate.RenderControl(writer);
            writer.RenderEndTag();

            writer.RenderEndTag();
            RenderContents(writer);
        }

        /// <summary>
        /// Adding the javascript to render the content 
        /// </summary>
        /// <param name="output"></param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder calnder = new StringBuilder();
            //adding javascript first
            calnder.AppendFormat(@"<script type='text/javascript'>
                                 document.observe('dom:loaded', function() {{
                                    Calendar.setup({{
                                    dateField: '{0}',
                                    triggerElement: '{1}',
                                    dateFormat: '%d-%m-%Y'
                                 }})
                                }});
                          ", _TxtDate.ClientID, _ImgDate.ClientID);
            calnder.Append("</script>");
            output.Write(calnder.ToString());
        }

    }
}
