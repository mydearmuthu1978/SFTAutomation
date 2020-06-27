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
    public partial class Scope : System.Web.UI.Page
    {
        SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = (string)Session["id"];


            try
            {
                con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
                if (!IsPostBack)
                {
                    if (str != null)
                    {
                        BindAppData(str);
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Unable to retrieve 'ConnectionString' from configuration file.");
            }

        }

        public void BindAppData(string str)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prcGetProgramAppSections";
            cmd.Parameters.AddWithValue("@ProgId", str);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            con.Close();
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView gv = (GridView)Page.FindControl("GridView1");
            gv.PageIndex = e.NewPageIndex;
            this.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string str = (string)Session["id"];
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataBind();
            BindAppData(str);
        }

        protected void GridView1_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string str = (string)Session["id"];
                GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
                string strId = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_ID")).Text;
                string strAppId = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_AppID")).Text;
                DropDownList proirityList = (DropDownList)row.FindControl("ddlPriority");
                string strPriority = proirityList.Text;
                string strTotalTestCases = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtTotalTestCases")).Text;
                GridView1.EditIndex = -1;
                SqlCommand cmd = new SqlCommand("prcUpdateAppSections", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", strId);
                cmd.Parameters.AddWithValue("@AppId", strAppId);
                cmd.Parameters.AddWithValue("@Priority", strPriority);
                cmd.Parameters.AddWithValue("@TotalTestCases", strTotalTestCases);
                con.Open();
                cmd.ExecuteNonQuery();
                GridView1.DataBind();
                con.Close();
                BindAppData(str);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string str = (string)Session["id"];
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();

        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            string str = (string)Session["id"];
            GridView1.EditIndex = -1;
            GridView1.DataBind();
            BindAppData(str);
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }

}