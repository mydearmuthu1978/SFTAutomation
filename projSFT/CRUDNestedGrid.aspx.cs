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
	public partial class CRUDNestedGrid : System.Web.UI.Page
	{
		string conString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ConnectionString;
		#region Variables
		string gvUniqueID = String.Empty;
		int gvNewPageIndex = 0;
		int gvEditIndex = -1;

		#endregion
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SelectApplication();
			}
		}
		protected void btnShow_Click(object sender, EventArgs e)
		{
			ShowData();
		}
		private void SelectApplication()
		{
			string strQRY = "SELECT Distinct AppName from Application";
			DataTable program = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter(strQRY, conString);
			adapter.Fill(program);
			ddlApplist.DataSource = program;
			ddlApplist.DataTextField = "AppName";
			ddlApplist.DataBind();
		}
        private void ShowData()
        {


            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("prcSectionsOfApplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppName", ddlApplist.SelectedItem.Value);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                GridView1.DataSource = dt;

                GridView1.DataBind();
                if (dt.Rows.Count > 0)
                {
                    if (ddlApplist.SelectedItem.Value != "0")
                    {
                        ((TextBox)GridView1.FooterRow.FindControl("txtApplicationName")).Visible = true;
                        ((TextBox)GridView1.FooterRow.FindControl("txtApplicationName")).Text = ddlApplist.SelectedItem.Value;
                    }
                    else
                    {
                        ((TextBox)GridView1.FooterRow.FindControl("txtApplicationName")).Attributes.Add("style", "display:none");
                        ((DropDownList)GridView1.FooterRow.FindControl("ddlApplicationName")).Visible = true;
                        string strQRY = "SELECT Distinct AppName from Application";
                        DataTable program = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(strQRY, conString);
                        adapter.Fill(program);
                        ((DropDownList)GridView1.FooterRow.FindControl("ddlApplicationName")).DataSource = program;
                        ((DropDownList)GridView1.FooterRow.FindControl("ddlApplicationName")).DataTextField = "AppName";
                        ((DropDownList)GridView1.FooterRow.FindControl("ddlApplicationName")).DataBind();
                    }
                }


            }
        }
        //This procedure prepares the query to bind the child GridView
        private DataTable ChildDataSource(string strId, string strSort)
        {
            string strQRY = "SELECT [Task].[SecId],[Task].[Id]," +
                                    "[Task].[TaskName],[Task].[TimeTaken] FROM [Task]" +
                                    " WHERE [Task].[SecId] = '" + strId + "'" +
                                    "UNION ALL " +
                                    "SELECT '" + strId + "','','','' FROM [Task] WHERE [Task].[SecId] = '" + strId + "'" +
                                    "HAVING COUNT(*)=0 " + strSort;

            SqlDataAdapter sdt = new SqlDataAdapter();
            DataTable dTable = new DataTable();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand(strQRY, con);
            sdt.SelectCommand = cmd;
            sdt.Fill(dTable);
            con.Close();
            return dTable;
        }
        #region GridView1 Event Handlers
        //This event occurs for each row
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            string strSort = string.Empty;
            //DropDownList ddlApplicationName = (DropDownList)e.Row.FindControl("ddlApplicationName");

            // Make sure we aren't in header/footer rows
            if (row.DataItem == null)
            {
                return;
            }

            //Find Child GridView control
            GridView gv = new GridView();
            gv = (GridView)row.FindControl("GridView2");

            //Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
            if (gv.UniqueID == gvUniqueID)
            {
                gv.PageIndex = gvNewPageIndex;
                gv.EditIndex = gvEditIndex;

                //Expand the Child grid
                ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["ID"].ToString() + "','one');</script>");
            }

            //Prepare the query for Child GridView by passing the Section ID of the parent row
            gv.DataSource = ChildDataSource(((DataRowView)e.Row.DataItem)["Id"].ToString(), strSort);
            gv.DataBind();

            //Add delete confirmation message for Section
            LinkButton l = (LinkButton)e.Row.FindControl("linkDeleteSec");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this Section " +
            DataBinder.Eval(e.Row.DataItem, "ID") + "')");

        }
        //This event occurs for any operation on the row of the grid
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            if (e.CommandName == "AddSection")
            {
                //Get the values stored in the text boxes
                string strSectionName = ((TextBox)GridView1.FooterRow.FindControl("txtSectionName")).Text;
                string strAppName = ((TextBox)GridView1.FooterRow.FindControl("txtApplicationName")).Text;
                string strSectionType = ((DropDownList)GridView1.FooterRow.FindControl("txtSectionType")).Text;
                string strTotalTestCase = ((TextBox)GridView1.FooterRow.FindControl("txtTotalTestCase")).Text;
                if (String.IsNullOrEmpty(strAppName))
                {
                    strAppName = ((DropDownList)GridView1.FooterRow.FindControl("ddlApplicationName")).Text;
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString());

                SqlCommand cmd = new SqlCommand("SP_AddSection", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppName", strAppName);
                cmd.Parameters.AddWithValue("@SecName", strSectionName);
                cmd.Parameters.AddWithValue("@SecType", strSectionType);
                cmd.Parameters.AddWithValue("@TotalTestCases", strTotalTestCase);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                //Re bind the grid to refresh the data
                ShowData();
            }
        }
        //This event occurs on click of the Update button
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Get the values stored in the text boxes
            string strSectionName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtSectionName")).Text;
            string strTotalTestCase = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTotalTestCase")).Text;
            string strSectionType = ((DropDownList)GridView1.Rows[e.RowIndex].FindControl("txtSectionType")).Text;
            string strID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblSectionID")).Text;

            try
            {
                //Prepare the Update Command of the DataSource control
                string strSQL = "";
                strSQL = "UPDATE Section set SecName = '" + strSectionName + "'" +
                         ",SecType = '" + strSectionType + "'" +
                         ",TotalTestCases = '" + strTotalTestCase + "'" +
                         " WHERE Id = '" + strID + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand(strSQL, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GridView1.EditIndex = -1;
                ShowData();
            }
            catch { }
        }
        //This event occurs on click of the Delete button
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Get the value 
            int taskId = 0;
            int strSectionID = int.Parse(((Label)GridView1.Rows[e.RowIndex].FindControl("lblSectionID")).Text);
            if ((Label)GridView1.Rows[e.RowIndex].FindControl("lblTaskID") != null)
            {
                taskId = int.Parse(((Label)GridView1.Rows[e.RowIndex].FindControl("lblTaskID")).Text);
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("SP_DeleteSectionandrelatedtask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sec_ID", strSectionID);
            cmd.Parameters.AddWithValue("@Task_ID", taskId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ShowData();

        }
        #endregion

        #region GridView2 Event Handlers
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvNewPageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddTask")
            {
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

                //Get the values stored in the text boxes
                string strSecID = gvTemp.DataKeys[0].Value.ToString();
                string strTaskname = ((TextBox)gvTemp.FooterRow.FindControl("txtTaskname")).Text;
                string strTimetaken = ((TextBox)gvTemp.FooterRow.FindControl("txtTimetaken")).Text;

                //Prepare the Insert Command of the DataSource control
                string strSQL = "";
                strSQL = "INSERT INTO Task (SecId, TaskName, TimeTaken) VALUES ('" + strSecID + "','" + strTaskname + "','" +
                        strTimetaken + "')";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand(strSQL, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ShowData();

            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            GridView1.EditIndex = e.NewEditIndex;
            ShowData();
        }
        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            ShowData();
        }
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            ShowData();
        }

        protected void GridView2_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            ShowData();
        }
        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;

            //Get the values stored in the text boxes
            string strID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblTaskID")).Text;
            string strTaskname = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtTaskname")).Text;
            string strTimetaken = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtTimetaken")).Text;

            string strSQL = "";
            strSQL = "UPDATE Task set TaskName = '" + strTaskname + "'" +
                     ",TimeTaken = '" + strTimetaken + "'" +
                     " WHERE Id = " + strID;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString());
            con.Open();
            //updating the record  
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.ExecuteNonQuery();
            con.Close();

            //Reset Edit Index
            gvEditIndex = -1;
            ShowData();

        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;

            //Get the value        
            string strTaskID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblTaskID")).Text;

            //Prepare the Update Command of the DataSource control
            string strSQL = "";

            strSQL = "DELETE from Task WHERE Id = " + strTaskID;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.ExecuteNonQuery();
            con.Close();
            ShowData();
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if this is our Blank Row being databound, if so make the row invisible
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataRowView)e.Row.DataItem)["SecId"].ToString() == String.Empty) e.Row.Visible = false;
            }
        }
    }
}
#endregion