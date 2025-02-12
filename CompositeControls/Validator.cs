using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public class ListValidator : BaseValidator
    {
        public ListValidator()
        {

        }

        protected override bool ControlPropertiesValid()
        {
            Control ctrl = FindControl(ControlToValidate) as ListControl;
            return (ctrl != null);
        }

        protected override bool EvaluateIsValid()
        {
            return this.CheckIfItemIsChecked();
        }

        protected bool CheckIfItemIsChecked()
        {
            ListControl listItemValidate = ((ListControl)this.FindControl(this.ControlToValidate));
            foreach (ListItem listItem in listItemValidate.Items)
            {
                if (listItem.Selected == true)
                    return true;
            }
            return false;
        }
    }
    public class CurrencyValidator : BaseValidator
    {
        public CurrencyValidator()
        {

        }

        protected override bool ControlPropertiesValid()
        {
            Control ctrl = FindControl(ControlToValidate) as LabelTekstVeld;
            return (ctrl != null && EvaluateIsValid());
        }

        protected override bool EvaluateIsValid()
        {
            return this.CheckIfValueIsCurrency();
        }

        protected bool CheckIfValueIsCurrency()
        {
            bool q = false;
            LabelTekstVeld TextBoxValidate = ((LabelTekstVeld)this.FindControl(this.ControlToValidate));
            try
            {
                Double a = Convert.ToDouble(TextBoxValidate.Waarde);
                q = true;
            }
            catch (Exception err)
            {
                q = false;
            }
            return q;
        }
    }
}
