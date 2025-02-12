using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrisControlToolkit
{
    [AspNetHostingPermission(SecurityAction.Demand, 
        Level = AspNetHostingPermissionLevel.Minimal),   
    AspNetHostingPermission(SecurityAction.InheritanceDemand,Level = AspNetHostingPermissionLevel.Minimal),
   DefaultEvent("Submit"),
   DefaultProperty("ButtonText"),
   ToolboxData("<{0}:Login runat=\"server\"> </{0}:Login>"),]
    public class Login : CompositeControl
    {
        private Button submitButton;
        private TextBox userNameTextBox;
        private Label nameLabel;
        private TextBox PasswordTextBox;
        private Label passwordlLabel;
        private RequiredFieldValidator emailValidator;
        private RequiredFieldValidator nameValidator;

        private static readonly object EventSubmitKey =
            new object();

        // The following properties are delegated to 
        // child controls.
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The text to display on the button.")
        ]
        public string ButtonText
        {
            get
            {
                EnsureChildControls();
                return submitButton.Text;
            }
            set
            {
                EnsureChildControls();
                submitButton.Text = value;
            }
        }

        [
        Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("The user name.")
        ]
        public string Name
        {
            get
            {
                EnsureChildControls();
                return userNameTextBox.Text;
            }
            set
            {
                EnsureChildControls();
                userNameTextBox.Text = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description(
            "Error message for the name validator.")
        ]
        public string NameErrorMessage
        {
            get
            {
                EnsureChildControls();
                return nameValidator.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                nameValidator.ErrorMessage = value;
                nameValidator.ToolTip = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The text for the name label.")
        ]
        public string NameLabelText
        {
            get
            {
                EnsureChildControls();
                return nameLabel.Text;
            }
            set
            {
                EnsureChildControls();
                nameLabel.Text = value;
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
                return PasswordTextBox.TextMode;
            }
            set
            {
                EnsureChildControls();
                PasswordTextBox.TextMode = value;
            }
        }

        [
        Bindable(true),
        Category("Default"),
        DefaultValue(""),
        Description("Password")
        ]
        public string Password
        {
            get
            {
                EnsureChildControls();
                return PasswordTextBox.Text;
            }
            set
            {
                EnsureChildControls();
                TextMode = TextBoxMode.Password;
                PasswordTextBox.Text = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description(
            "Error message for the Password validator.")
        ]
        public string PasswordErrorMessage
        {
            get
            {
                EnsureChildControls();
                return emailValidator.ErrorMessage;
            }
            set
            {
                EnsureChildControls();
                emailValidator.ErrorMessage = value;
                emailValidator.ToolTip = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("The text for the Password label.")
        ]
        public string PasswordLabelText
        {
            get
            {
                EnsureChildControls();
                return passwordlLabel.Text;
            }
            set
            {
                EnsureChildControls();
                passwordlLabel.Text = value;

            }
        }

        // The Submit event.
        [
        Category("Action"),
        Description("Raised when the user clicks the button.")
        ]
        public event EventHandler Submit
        {
            add
            {
                Events.AddHandler(EventSubmitKey, value);
            }
            remove
            {
                Events.RemoveHandler(EventSubmitKey, value);
            }
        }

        // The method that raises the Submit event.
        protected virtual void OnSubmit(EventArgs e)
        {
            EventHandler SubmitHandler =
                (EventHandler)Events[EventSubmitKey];
            if (SubmitHandler != null)
            {
                SubmitHandler(this, e);
            }
        }

        // Handles the Click event of the Button and raises
        // the Submit event.
        private void _button_Click(object source, EventArgs e)
        {
            OnSubmit(EventArgs.Empty);
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }


        protected override void CreateChildControls()
        {
            Controls.Clear();

            nameLabel = new Label();

            userNameTextBox = new TextBox();
            userNameTextBox.ID = "userNameTextBox";

            nameValidator = new RequiredFieldValidator();
            nameValidator.ID = "validator1";
            nameValidator.ControlToValidate = userNameTextBox.ID;
            nameValidator.Text = "Failed validation.";
            nameValidator.Display = ValidatorDisplay.Static;

            passwordlLabel = new Label();

            PasswordTextBox = new TextBox();
            PasswordTextBox.ID = "PasswordTextBox";

            emailValidator = new RequiredFieldValidator();
            emailValidator.ID = "validator2";
            emailValidator.ControlToValidate =
                PasswordTextBox.ID;
            emailValidator.Text = "Failed validation.";
            emailValidator.Display = ValidatorDisplay.Static;

            submitButton = new Button();
            submitButton.ID = "button1";
            submitButton.Click
                += new EventHandler(_button_Click);

            this.Controls.Add(nameLabel);
            this.Controls.Add(userNameTextBox);
            this.Controls.Add(nameValidator);
            this.Controls.Add(passwordlLabel);
            this.Controls.Add(PasswordTextBox);
            this.Controls.Add(emailValidator);
            this.Controls.Add(submitButton);
        }


        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);

            writer.AddAttribute(
                HtmlTextWriterAttribute.Cellpadding,
                "1", false);
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            nameLabel.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            userNameTextBox.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            nameValidator.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            passwordlLabel.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            PasswordTextBox.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            emailValidator.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(
                HtmlTextWriterAttribute.Colspan,
                "2", false);
            writer.AddAttribute(
                HtmlTextWriterAttribute.Align,
                "right", false);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            submitButton.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("&nbsp;");
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }
}
