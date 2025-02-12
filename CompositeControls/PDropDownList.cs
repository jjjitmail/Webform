using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PrisControlToolkit
{
    public class PDropDownList : DropDownList
    {
        public string CodeName
        {
            get
            {
                if (ViewState["CodeName"] != null)
                {
                    return ViewState["CodeName"].ToString();
                }
                else
                {
                    return String.Empty;
                }
            }

            set { ViewState["CodeName"] = value; }
        }

        protected override void EnsureDataBound()
        {
            try
            {
                if (this.RequiresDataBinding)
                {
                    this.DataBind();
                }
            }
            catch { }
        }

        public override void DataBind()
        {
            this.DataSource = GetDataSource();
            this.DataTextField = "Name";
            this.DataValueField = "Value";

            base.DataBind();
        }

        public IEnumerable GetDataSource()
        {
            string key = "PDrowDownList_" + CodeName;

            object item = Page.Cache.Get(key);
            if (item == null)
            {
                item = GetDataFromDB();
                Page.Cache.Insert(key, item, null, System.DateTime.UtcNow.AddHours(5), System.Web.Caching.Cache.NoSlidingExpiration);
            }

            return (IEnumerable)item;
        }

        public IEnumerable GetDataFromDB()
        {
            List<DropDownListItm> lItems = new List<DropDownListItm>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=TestingAjax;Integrated Security=True;"))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT NAME, VALUE FROM LOOKUP_GESLACHT";
                    cmd.CommandType = CommandType.Text;


                    using (SqlDataReader dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dtr.Read())
                        {
                            lItems.Add(new DropDownListItm(dtr["Name"].ToString(), dtr["Value"].ToString()));
                        }
                    }
                }
            }

            return lItems;
        }

        public class DropDownListItm
        {
            private string _Text;
            public string Text
            {
                get { return _Text; }
            }

            private string _Value;
            public string Value
            {
                get { return _Value; }
            }

            public DropDownListItm(string text, string value)
            {
                _Text = text;
                _Value = value;
            }
        }
    }
}
