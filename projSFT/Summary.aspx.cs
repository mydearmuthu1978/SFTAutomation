using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projSFT
{
    public partial class Summary : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlCommand cmd = null;

        string gvUniqueID = String.Empty;
        int gvNewPageIndex = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Page.Master as ProjectManagement;
            string str = (string)Session["id"];
            string conString = string.Empty;

            try
            {
                con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
                if (!IsPostBack)
                {
                    if (str != null)
                    {
                        BindGrid(str);
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Unable to retrieve 'ConnectionString' from configuration file.");
            }
        }
        private void BindGrid(string str)
        {            
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prcGetProgramAppTask";
            cmd.Parameters.AddWithValue("@ProgId", str);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                GridView gv = (GridView)Page.FindControl("gv1");
                gv1.DataSource = dt;
                gv1.DataBind();
            }
            con.Close();
        }

        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    try
            //    {
            //        con.Open();
            //        DropDownList proirityList = (e.Row.FindControl("ddlPriority") as DropDownList);

            //        SqlCommand cmd = new SqlCommand("SELECT DISTINCT Priority from Application", con);
            //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);
            //        con.Close();
            //        proirityList.DataSource = dt;

            //        proirityList.DataTextField = "Priority";
            //        proirityList.DataValueField = "Priority";
            //        string strPriority = proirityList.Text;
            //        proirityList.DataBind();

            //        //ddl.Items.Insert(0, new ListItem("--Select Priority--", "0"));
            //        //ddl.SelectedIndex = 0;  // selecting default Text by index Number
            //        //ddl.SelectedItem.Text = "Full_Function"; // selecting Default Text by Text
            //        //ddl.SelectedValue = "Full_Function"; // Selecting Default Text by value
            //    }
            //    catch (Exception ex)
            //    {
            //        string msg = ex.Message;
            //    }
            //}
        }

        protected void gv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            var master = Page.Master as ProjectManagement;
            string str = (string)Session["id"];
            gv1.EditIndex = e.NewEditIndex;
            BindGrid(str);
        }
        protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvNewPageIndex = e.NewPageIndex;
            gv1.DataBind();
        }

        protected void gv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var master = Page.Master as ProjectManagement;
            string str = (string)Session["id"];
            GridViewRow row = (GridViewRow)gv1.Rows[e.RowIndex];
            string strId = ((Label)gv1.Rows[e.RowIndex].FindControl("lbl_ID")).Text;
            string strTaskId = ((Label)gv1.Rows[e.RowIndex].FindControl("lbl_TaskID")).Text;
            DropDownList proirityList = (DropDownList)row.FindControl("ddlPriority");
            string strPriority = proirityList.Text;
            string strTimeTaken = ((TextBox)gv1.Rows[e.RowIndex].FindControl("txtTimeTaken")).Text;
            gv1.EditIndex = -1;

            try
            {
                SqlCommand cmd = new SqlCommand("prcUpdateAppTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppId", strId);
                cmd.Parameters.AddWithValue("@TaskId", strTaskId);
                cmd.Parameters.AddWithValue("@Priority", strPriority);
                cmd.Parameters.AddWithValue("@TimeTaken", strTimeTaken);
                con.Open();
                cmd.ExecuteNonQuery();
                gv1.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                cmd.Dispose();
            }

        }

        protected void gv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            var master = Page.Master as ProjectManagement;
            string str = (string)Session["id"];
            gv1.EditIndex = -1;
            gv1.DataBind();
            BindGrid(str);
        }

    }
}