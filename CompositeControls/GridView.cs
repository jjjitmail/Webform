using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace UI
{
    public partial class GridView : System.Web.UI.WebControls.GridView
    {
        private const string GRIDVIEW_JS = "PrisControlToolkit.GridView.js";

        private ArrayList cachedSelectedIndices;

        public GridView()
        {
        }

        #region Properties
        // PROPERTY:: AutoGenerateCheckBoxColumn
        [Category("Behavior")]
        [Description("Whether a checkbox column is generated automatically at runtime")]
        [DefaultValue(false)]
        public bool AutoGenerateCheckBoxColumn
        {
            get
            {
                object o = ViewState["AutoGenerateCheckBoxColumn"];
                if (o == null)
                    return false;
                return (bool)o;
            }
            set { ViewState["AutoGenerateCheckBoxColumn"] = value; }
        }

        // PROPERTY:: CheckBoxColumnIndex
        [Category("Behavior")]
        [Description("Indicates the 0-based position of the checkbox column")]
        [DefaultValue(0)]
        public int CheckBoxColumnIndex
        {
            get
            {
                object o = ViewState["CheckBoxColumnIndex"];
                if (o == null)
                    return 0;
                return (int)o;
            }
            set
            {
                ViewState["CheckBoxColumnIndex"] = (value < 0 ? 0 : value);
            }
        }

        // PROPERTY:: SelectedIndices
        internal virtual ArrayList SelectedIndices
        {
            get
            {
                cachedSelectedIndices = new ArrayList();
                for (int i = 0; i < Rows.Count; i++)
                {
                    // Retrieve the reference to the checkbox
                    CheckBox cb = (CheckBox)Rows[i].FindControl(GridViewCheckBoxField.CheckBoxID);
                    if (cb == null)
                        return cachedSelectedIndices;
                    if (cb.Checked)
                        cachedSelectedIndices.Add(i);
                }
                return cachedSelectedIndices;
            }
        }

        // METHOD:: GetSelectedIndices
        public virtual int[] GetSelectedIndices()
        {
            return (int[])SelectedIndices.ToArray(typeof(int));
        }



        #endregion


        #region Members overrides
        // METHOD:: CreateColumns
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            // Let the GridView create the default set of columns
            ICollection columnList = base.CreateColumns(dataSource, useDataSource);
            if (!AutoGenerateCheckBoxColumn)
                return columnList;

            // Add a checkbox column if required
            ArrayList extendedColumnList = AddCheckBoxColumn(columnList);
            return extendedColumnList;
        }


        // METHOD:: OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Type t = this.GetType();
            string url = Page.ClientScript.GetWebResourceUrl(t, GRIDVIEW_JS);
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(t, GRIDVIEW_JS))
                Page.ClientScript.RegisterClientScriptInclude(t, GRIDVIEW_JS, url);
        }


        // METHOD:: OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            // Do as usual
            base.OnPreRender(e);

            // Adjust each data row
            foreach (GridViewRow r in Rows)
            {
                // Get the appropriate style object for the row
                TableItemStyle style = GetRowStyleFromState(r.RowState);

                // Retrieve the reference to the checkbox
                CheckBox cb = (CheckBox)r.FindControl(GridViewCheckBoxField.CheckBoxID);

                // Build the ID of the checkbox in the header
                string headerCheckBoxID = String.Format(CheckBoxColumHeaderID, ClientID);

                // Add script code to enable selection
                //cb.Attributes["onclick"] = String.Format("ApplyStyle(this, '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                //        ColorTranslator.ToHtml(SelectedRowStyle.ForeColor),
                //        ColorTranslator.ToHtml(SelectedRowStyle.BackColor),
                //        ColorTranslator.ToHtml(style.ForeColor),
                //        ColorTranslator.ToHtml(style.BackColor),
                //        (style.Font.Bold ? 700 : 400),
                //        headerCheckBoxID);

                //// Update the style of the checkbox if checked
                //if (cb.Checked)
                //{
                //    r.BackColor = SelectedRowStyle.BackColor;
                //    r.ForeColor = SelectedRowStyle.ForeColor;
                //    r.Font.Bold = SelectedRowStyle.Font.Bold;
                //}
                //else
                //{
                //    r.BackColor = style.BackColor;
                //    r.ForeColor = style.ForeColor;
                //    r.Font.Bold = style.Font.Bold;
                //}
            }
        }
        #endregion
    }
}
