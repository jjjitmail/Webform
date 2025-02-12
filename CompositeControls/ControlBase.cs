using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace UI
{
    public class ControlBase : CompositeControl
    {
        private string _DataBinding;

        [Bindable(true), Category("Default"), DefaultValue("")]
        public string DataBinding
        {
            get { return _DataBinding; }
            set
            {
                this._DataBinding = value;
            }
        }
    }
}
