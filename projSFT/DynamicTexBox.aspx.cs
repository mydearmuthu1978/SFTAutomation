using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projSFT
{
    public partial class DynamicTexBox : System.Web.UI.Page
    {
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dt.Rows.Add(dr);
            //dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            Gridview1.DataSource = dt;
            Gridview1.DataBind();
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Column1"] = box1.Text;
                        drCurrentRow["Column2"] = box2.Text;
                        drCurrentRow["Column3"] = box3.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    Gridview1.DataSource = dtCurrentTable;
                    Gridview1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");

                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        box2.Text = dt.Rows[i]["Column2"].ToString();
                        box3.Text = dt.Rows[i]["Column3"].ToString();

                        rowIndex++;

                    }
                }
                // ViewState["CurrentTable"] = dt;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRow();
            }
        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            StringCollection sc = new StringCollection();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                        TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                        TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("TextBox3");

                        //get the values here

                        //Response.Write(box1.Text + "<BR/>");
                        //Response.Write(box2.Text + "<BR/>");
                        //Response.Write(box3.Text);

                        sc.Add(box1.Text + "," + box2.Text + "," + box3.Text);
                        rowIndex++;
                    }
                    InsertRecords(sc);
                }
            }
        }
        //A method that returns a string which calls the connection string from the web.config
        private string GetConnectionString()
        {
            //"DBConnection" is the name of the Connection String
            //that was set up from the web.config file
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }

        //A method that Inserts the records to the database
        private void InsertRecords(StringCollection sc)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            StringBuilder sb = new StringBuilder(string.Empty);
            string[] splitItems = null;
            foreach (string item in sc)
            {

                const string sqlStatement = "INSERT INTO SampleTable (Column1,Column2,Column3) VALUES";
                if (item.Contains(","))
                {
                    splitItems = item.Split(",".ToCharArray());
                    sb.AppendFormat("{0}('{1}','{2}','{3}'); ", sqlStatement, splitItems[0], splitItems[1], splitItems[2]);
                }

            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Records Successfuly Saved!');", true);

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                conn.Close();
            }
        }
        protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label l = (Label)e.Row.FindControl("Label1");
                if (l != null)
                {
                    string script = "window.open('Default.aspx');";
                    l.Attributes.Add("onclick", script);
                }
            }
        }
    }
}