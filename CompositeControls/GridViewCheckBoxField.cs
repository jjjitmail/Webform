using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    internal sealed class GridViewCheckBoxField : CheckBoxField
    {
        public const string CheckBoxID = "CheckBoxButton";

        public GridViewCheckBoxField()
        {
        }

        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);

            // Add a checkbox anyway, if not done already
            if (cell.Controls.Count == 0)
            {
                CheckBox chk = new CheckBox();
                chk.ID = GridViewCheckBoxField.CheckBoxID;
                cell.Controls.Add(chk);
            }
        }

    }
}
