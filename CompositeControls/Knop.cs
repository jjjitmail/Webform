using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    [AspNetHostingPermission(SecurityAction.Demand,
    Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
    DefaultEvent("Submit"),
    DefaultProperty("ButtonText"),
    ToolboxData("<{0}:Knop runat=\"server\"> </{0}:Knop>"),]
    public class Knop : CompositeControl
    {
        private Button PrisKnop;

        private static readonly object EventSubmitKey =
            new object();

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("Tekst Op de Knop")
        ]
        public string Tekst
        {
            get
            {
                EnsureChildControls();
                return PrisKnop.Text;
            }
            set
            {
                EnsureChildControls();
                PrisKnop.Text = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("Style Op de Knop")
        ]
        public string CssKlasse
        {
            get
            {
                EnsureChildControls();
                return PrisKnop.CssClass;
            }
            set
            {
                EnsureChildControls();
                PrisKnop.CssClass = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("Knop CommandNaam")
        ]
        public string CommandNaam
        {
            get
            {
                EnsureChildControls();
                return PrisKnop.CommandName;
            }
            set
            {
                EnsureChildControls();
                PrisKnop.CommandName = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("Knop CommandArgument")
        ]
        public string CommandArgument
        {
            get
            {
                EnsureChildControls();
                return PrisKnop.CommandArgument;
            }
            set
            {
                EnsureChildControls();
                PrisKnop.CommandArgument = value;
            }
        }

        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(""),
        Description("Knop Enabled")
        ]
        public bool Enabled
        {
            get
            {
                EnsureChildControls();
                return PrisKnop.Enabled;
            }
            set
            {
                EnsureChildControls();
                PrisKnop.Enabled = value;
            }
        }

        public void InitEigenschappen(int value)
        {
            int P = Convert.ToInt16(value);
            PrisKnop.Enabled = (P < 2);
            PrisKnop.Visible = (P > 0);
        }

        [
        Category("Action"),
        Description("Klik..Klik..")
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

        protected virtual void OnSubmit(EventArgs e)
        {
            EventHandler SubmitHandler =
                (EventHandler)Events[EventSubmitKey];
            if (SubmitHandler != null)
            {
                SubmitHandler(this, e);
            }
        }

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
            PrisKnop = new Button();
            PrisKnop.ID = "knop1";
            PrisKnop.Click
                += new EventHandler(_button_Click);
            this.Controls.Add(PrisKnop);
        }

    }
}
