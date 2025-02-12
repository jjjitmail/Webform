using System;
using System.Web;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;

namespace PrisControlToolkit
{
    public class CalendarField : DataControlField
    {
        #region Private properties
        private bool _inInsertMode;
        #endregion


        #region Custom properties
        // *******************************************************************
        // PROPERTY: DataField
        // Indicates the field providing the date in view mode
        public virtual string DataField
        {
            get
            {
                object o = this.ViewState["DataField"];
                if (o != null)
                    return (string)o;
                return String.Empty;
            }
            set
            {
                ViewState["DataField"] = value;
                OnFieldChanged();
            }
        }


        // *******************************************************************
        // PROPERTY: ReadOnly
        // Indicates the field from which the text of the drop-down items is taken
        public virtual bool ReadOnly
        {
            get
            {
                object o = base.ViewState["ReadOnly"];
                if (o != null)
                    return (bool)o;
                return false;
            }
            set
            {
                base.ViewState["ReadOnly"] = value;
                OnFieldChanged();
            }
        }


        // *******************************************************************
        // PROPERTY: DataFormatString
        // Indicates the format string for the date 
        public virtual string DataFormatString
        {
            get
            {
                object o = this.ViewState["DataFormatString"];
                if (o != null)
                    return (string)o;
                return String.Empty;
            }
            set
            {
                ViewState["DataFormatString"] = value;
                OnFieldChanged();
            }
        }

        #endregion


        #region Overridden methods
        // *******************************************************************
        // METHOD: CreateField
        // Must override because it is abstract on the base class
        protected override DataControlField CreateField()
        {
            return new CalendarField();
        }

        // *******************************************************************
        // METHOD: ExtractValuesFromCell
        // Extract values from cell (presumably in edit mode)
        public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
        {
            object selectedValue = null;
            if (cell.Controls.Count > 0)
            {
                Calendar cal = cell.Controls[0] as Calendar;

                if (cal == null)
                {
                    throw new InvalidOperationException("CalendarField could not extract control.");
                }
                else
                    selectedValue = cal.SelectedDate;
            }

            // Add the value to the dictionary
            if (dictionary.Contains(DataField))
                dictionary[DataField] = selectedValue;
            else
                dictionary.Add(DataField, selectedValue);
        }

        // *******************************************************************
        // METHOD: CopyProperties
        //  
        protected override void CopyProperties(DataControlField newField)
        {
            ((CalendarField)newField).DataField = this.DataField;
            ((CalendarField)newField).DataFormatString = this.DataFormatString;
            ((CalendarField)newField).ReadOnly = this.ReadOnly;

            base.CopyProperties(newField);
        }

        // *******************************************************************
        // METHOD: InitializeCell
        // 
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            // Call the base method
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            // Initialize the contents of the cell quitting if it is a header/footer
            if (cellType == DataControlCellType.DataCell)
                InitializeDataCell(cell, rowState);
        }
        #endregion


        #region Custom methods
        // *******************************************************************
        // METHOD: InitializeDataCell
        // 
        protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            Control ctrl = null;

            // If we're in edit/insert mode...
            DataControlRowState state = rowState & DataControlRowState.Edit;
            if ((!ReadOnly && (state != DataControlRowState.Normal)) || rowState == DataControlRowState.Insert)
            {
                Calendar cal = new Calendar();
                cal.ToolTip = this.HeaderText;
                cell.Controls.Add(cal);

                // Save the control to use for binding (edit/insert mode)
                if ((DataField.Length != 0) &&
                    (DataField.Length != 0))
                    ctrl = cal;

                _inInsertMode = rowState == DataControlRowState.Insert;
            }
            else if (DataField.Length != 0)
            {
                // Save the control to use for binding (view mode)
                ctrl = cell;
            }

            // If the column is visible, trigger the binding process
            if ((ctrl != null) && Visible)
            {
                ctrl.DataBinding += new EventHandler(this.OnBindingField);
            }
        }

        // *******************************************************************
        // METHOD: OnBindingField
        // 
        protected virtual void OnBindingField(object sender, EventArgs e)
        {
            Control target = (Control)sender;

            // If in view mode ...
            if (target is TableCell)
            {
                ((TableCell)target).Text = LookupValueForView(target.NamingContainer);
            }
            else if (target is Calendar)
            {
                Calendar cal = (Calendar)target;
                DateTime dt = LookupValueForEdit(target.NamingContainer);
                cal.SelectedDate = dt;
                cal.VisibleDate = dt;
            }
        }

        // *******************************************************************
        // METHOD: LookupValueForEdit
        // 
        protected virtual DateTime LookupValueForEdit(Control container)
        {
            if (container == null)
                throw new HttpException("No data bound container");

            // Get the data item object
            if (!_inInsertMode)
            {
                object dataItem = DataBinder.GetDataItem(container);
                return (DateTime)DataBinder.GetPropertyValue(dataItem, DataField);
            }
            return DateTime.Now;
        }

        // *******************************************************************
        // METHOD: LookupValueForView
        // 
        protected virtual string LookupValueForView(Control container)
        {
            if (container == null)
                throw new HttpException("No data bound container");

            // Take care of what's displayed at design-time
            if (DesignMode)
                return GetDesignTimeValue();

            // Get the data item object
            object dataItem = DataBinder.GetDataItem(container);
            DateTime dt = (DateTime)DataBinder.GetPropertyValue(dataItem, DataField);
            if (DataFormatString.Length > 0)
                return dt.ToString(DataFormatString);

            return dt.ToString();
        }


        // *******************************************************************
        // METHOD: GetDesignTimeValue
        // 
        protected virtual string GetDesignTimeValue()
        {
            return "<select><option>Databound Date</option></select>";
        }

        #endregion
    }
}
