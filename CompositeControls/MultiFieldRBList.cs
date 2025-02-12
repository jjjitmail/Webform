using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Collections;
using System.Web;
using System.Security.Permissions;

namespace PrisControlToolkit
{
    /// <summary>
    /// RadioButtonList doesn't allow you to created data for the text or values of the list items
    /// from multiple fields. This version allows multiple fields to participate in binding
    /// for the text part of each item
    /// </summary>
    [ValidationProperty("SelectedItem")]
    [SupportsEventValidation]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class MultiFieldRBList : RadioButtonList
    {


        /// <summary>
        /// A comma separated list of the fields used in the databinding of the text for each ListItem
        /// </summary>
        [TypeConverter(typeof(StringArrayConverter))]
        [DefaultValue((string)null)]
        public virtual string[] DataTextFields
        {
            get
            {
                object currentValues = base.ViewState["DataTextFields"];
                if (currentValues != null)
                {
                    return (string[])((string[])currentValues).Clone();
                }
                return new string[0];
            }
            set
            {
                string[] strArray = base.ViewState["DataTextFields"] as string[];
                if (!this.StringArraysEqual(strArray, value))
                {
                    if (value != null)
                    {
                        base.ViewState["DataTextFields"] = (string[])value.Clone();
                        if (base.Initialized)
                        {
                            base.RequiresDataBinding = true;
                        }
                    }
                    else
                    {
                        base.ViewState["DataTextFields"] = null;
                    }
                }
            }
        }

        /// <summary>
        /// For simplicity, we disallow using the DataTextField property provided by the ListControl
        /// base class.  Because the field is public, we cannot hide it, so the best we can do
        /// is throw exceptions when it is accessed
        /// </summary>
        public override string DataTextField
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Overrides the default data binding after the select method has been called.
        /// This allows us to create the ListItems using multiple fields
        /// </summary>
        /// <param name="dataSource">The item containing the data items to enumerate</param>
        protected override void PerformDataBinding(IEnumerable dataSource)
        {
            if (dataSource != null)
            {
                string[] dataTextFields = this.DataTextFields;
                string dataValueField = this.DataValueField;
                string dataTextFormatString = this.DataTextFormatString;

                bool dataBindingFieldsSupplied = (dataTextFields.Length != 0) || (dataValueField.Length != 0);
                bool hasTextFormatString = dataTextFormatString.Length != 0;

                if (!this.AppendDataBoundItems)
                    this.Items.Clear();

                if (dataSource is ICollection)
                    this.Items.Capacity = (dataSource as ICollection).Count + this.Items.Count;


                PropertyDescriptor[] textFieldPropertyDescriptors = null;

                foreach (object dataItem in dataSource)
                {
                    ListItem item = new ListItem();

                    if (dataBindingFieldsSupplied)
                    {
                        if (dataTextFields.Length > 0)
                        {
                            if (textFieldPropertyDescriptors == null)
                                textFieldPropertyDescriptors = GetPropertyDescriptors(dataTextFields, dataItem);

                            object[] dataTextValues = GetValues(dataItem, textFieldPropertyDescriptors);
                            item.Text = FormatDataTextValue(dataTextValues, dataTextFormatString);
                        }
                        if (dataValueField.Length > 0)
                        {
                            item.Value = DataBinder.GetPropertyValue(dataItem, dataValueField, null);
                        }
                    }
                    else
                    {
                        if (hasTextFormatString)
                        {
                            item.Text = string.Format(CultureInfo.CurrentCulture, dataTextFormatString, new object[] { dataItem });
                        }
                        else
                        {
                            item.Text = dataItem.ToString();
                        }
                        item.Value = dataItem.ToString();
                    }
                    this.Items.Add(item);
                }
            }

            //get the base class to do stuff with the cached selection - this uses private variables which 
            // we can't access here. The null means that the actual databinding carried out by ListControl base 
            //class will not take place - but you have to look at the implemnentation to know that, as well as the fact that 
            //and that the PerformDataBinding in BaseDataBoundControl is empty
            base.PerformDataBinding(null);

        }


        /// <summary>
        /// Returns an array of PropertyDescriptor objects representing each property in the 
        /// specified object.
        /// </summary>
        /// <param name="PropertyNames">An array of the names of the properties to find</param>
        /// <param name="ObjectToSearch">The object containing the properties to find</param>
        private PropertyDescriptor[] GetPropertyDescriptors(string[] PropertyNames, object ObjectToSearch)
        {
            PropertyDescriptorCollection allProperties = TypeDescriptor.GetProperties(ObjectToSearch);
            PropertyDescriptor[] selectedProperties = new PropertyDescriptor[PropertyNames.Length];

            for (int counter = 0; counter < PropertyNames.Length; counter++)
            {
                string name = PropertyNames[counter];
                if (name.Length != 0)
                {
                    selectedProperties[counter] = allProperties.Find(name, true);
                    if ((selectedProperties[counter] == null) && !base.DesignMode)
                    {
                        throw new ApplicationException(String.Format("Property '{0}' Not Found", name));
                    }
                }
            }
            return selectedProperties;
        }

        /// <summary>
        /// Returns a set of properties from an object as an object array
        /// </summary>
        /// <param name="DataItem">The object containing the properties to find</param>
        /// <param name="PropertiesToResolve">An array of PropertyDescriptor objects representing the required properties</param>
        private object[] GetValues(object DataItem, PropertyDescriptor[] PropertiesToResolve)
        {
            object[] items = new object[PropertiesToResolve.Length];
            for (int counter = 0; counter < PropertiesToResolve.Length; counter++)
                items[counter] = PropertiesToResolve[counter].GetValue(DataItem);

            return items;
        }


        /// <summary>
        /// The System.Web implementation is not accessible outside the assembly, so for 
        /// completeness it is included here
        /// </summary>

        private static bool IsNull(object value)
        {
            return (value == null) || Convert.IsDBNull(value);
        }

        /// <summary>
        /// Processes a forma string using the specified objects
        /// </summary>
        /// <param name="DataTextValues">The objects to insert</param>
        /// <param name="DataTextFormatString">The data format string specifying how the objects should be formatted</param>
        protected virtual string FormatDataTextValue(object[] DataTextValues, string DataTextFormatString)
        {
            string outputValue = string.Empty;

            if (DataTextValues != null)
            {
                if (DataTextFormatString.Length == 0)
                {
                    if ((DataTextValues.Length > 0) && !IsNull(DataTextValues[0]))
                    {
                        DataTextFormatString = "{0}";
                    }
                }
                outputValue = string.Format(CultureInfo.CurrentCulture, DataTextFormatString, DataTextValues);
            }
            return outputValue;
        }

        /// <summary>
        /// Compares the values two arrays of strings. The strings are treated as equal if their text is the same, regardless
        /// of whether the object references are different
        /// </summary>
        /// <returns>True if both arrays contain exactly the same data, false if not.</returns>
        private bool StringArraysEqual(string[] First, string[] Second)
        {
            if ((First != null) || (Second != null))
            {
                if ((First == null) || (Second == null))
                {
                    return false;
                }
                if (First.Length != Second.Length)
                {
                    return false;
                }
                for (int i = 0; i < First.Length; i++)
                {
                    if (!string.Equals(First[i], Second[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


    }
}
