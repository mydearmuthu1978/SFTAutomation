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
        SqlDataAdapter dadapter = null;
        SqlDataReader dr = null;
        SqlCommand cmd = null;
        DataSet ds = null;
        string sqlQry = null;
        string gvUniqueID = String.Empty;
        int gvNewPageIndex = 0;
        int gvEditIndex = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ConnectionString;
            string sql = "select * from Application";
            string gvUniqueID = String.Empty;            

            if (!Page.IsPostBack)
            {                
                //GridViewBind();                
                BindGrid();
            }

        }
        private void BindGrid()
        {
            SqlCommand cmd = new SqlCommand("Select Application.Id, Application.AppName, Application.Priority, Task.TimeTaken from Application, Section, Task", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();            

            if (dt.Rows.Count > 0)
            {
                gv1.DataSource = dt;
                gv1.DataBind();
            }
        }
        public void refreshdata()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select Application.Id, Application.AppName, Application.Priority, Task.TimeTaken from Application, Task", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gv1.DataSource = dt;
                gv1.DataBind();
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }

        }
        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    con.Open();
                    DropDownList ddl = (e.Row.FindControl("ddlPriority") as DropDownList);

                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Priority from Application", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Close();
                    ddl.DataSource = dt;

                    ddl.DataTextField = "Priority";
                    ddl.DataValueField = "Priority";
                    ddl.DataBind();
                    //ddl.Items.Insert(0, new ListItem("--Select Priority--", "0"));
                    //ddl.SelectedIndex = 0;  // selecting default Text by index Number
                    ddl.SelectedItem.Text = "Full_Function"; // selecting Default Text by Text
                    ddl.SelectedValue = "Full_Function"; // Selecting Default Text by value
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }

        protected void gv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //GridViewRow row = (GridViewRow)gv1.Rows[e.RowIndex];
            //Label lbldeleteid = (Label)row.FindControl("lblID");
            //con.Open();
            //SqlCommand cmd = new SqlCommand("delete FROM detail where id='" + Convert.ToInt32(gv1.DataKeys[e.RowIndex].Value.ToString()) + "'", con);
            //cmd.ExecuteNonQuery();
            //con.Close();
            //BindGrid();

            //int Id = Convert.ToInt32(gv1.DataKeys[e.RowIndex].Value);
            ////SqlConnection oconn = new SqlConnection();
            //con.Open();
            //SqlCommand ocmd = new SqlCommand();
            //cmd.CommandText = "DELETE FROM Application WHERE Id=@ID";
            //cmd.Parameters.AddWithValue("@EID", Id);
            //cmd.Connection = con;
            //cmd.ExecuteNonQuery();
            //con.Close();
            //BindGrid();
        }
        protected void gv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridView gvTemp = (GridView)sender;
            //gvUniqueID = gvTemp.UniqueID;
            //gvEditIndex = e.NewEditIndex;
            ////gv1.EditIndex = e.NewEditIndex;
            //NewEditIndex property used to determine the index of the row being edited.  
            gv1.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvNewPageIndex = e.NewPageIndex;
            gv1.DataBind();
        }
        //protected void gv1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    //if (e.CommandName.Equals("Edit"))
        //    //{                
        //    //    //TextBox txtAppName = (TextBox)gv1.FooterRow.FindControl("txtAppName");
        //    //    //DropDownList ddlnew = (DropDownList)gv1.FooterRow.FindControl("ddlPriority");
        //    //    //TextBox txtTimeTaken = (TextBox)gv1.FooterRow.FindControl("txtTimeTaken");

        //    //    ////SqlConnection oconn = new SqlConnection();
        //    //    //con.Open();
        //    //    //SqlCommand cmd = new SqlCommand();
        //    //    //cmd.CommandText = "INSERT INTO Application VALUES ('" + txtAppName.Text + "'," + ddlnew.SelectedValue + "'," + txtTimeTaken.Text + ")";
        //    //    //cmd.Connection = con;
        //    //    //cmd.ExecuteNonQuery();
        //    //    //BindGrid();
        //    //}
        //}
        protected void gv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int TimeTaken = int.Parse(((TextBox)(gv1.Rows[e.RowIndex].Cells[1].FindControl("txtTimeTaken"))).Text);
            string Appname = ((TextBox)(gv1.Rows[e.RowIndex].Cells[1].FindControl("txtAppName"))).Text;
            string Priority = ((DropDownList)(gv1.Rows[e.RowIndex].Cells[1].FindControl("ddlPriority"))).SelectedValue;
            try
            {
                //SqlConnection oconn = new SqlConnection();
                con.Open();
                SqlCommand ocmd = new SqlCommand();
                cmd.CommandText = "update Application set app.AppName = @AppName,app.Priority = @Priority, task.TimeTaken = @TimeTaken from Application app, Task task where task.Id = app.Id and app.Id = @Id";
                cmd.Parameters.AddWithValue("@Appname", Appname);
                cmd.Parameters.AddWithValue("@TimeTaken", TimeTaken);
                cmd.Parameters.AddWithValue("@Priority", Priority);

                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                gv1.EditIndex = -1;
                BindGrid();
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
            refreshdata();

        }
        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gv1.PageIndex = e.NewPageIndex;
        //    BindGrid();
        //}
        protected void gv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv1.EditIndex = -1;
            BindGrid();
        }

    }
}