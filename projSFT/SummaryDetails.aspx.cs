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
    public partial class SummaryDetails : System.Web.UI.Page
    {
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        string cs = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowData();
        }
        //ShowData method for Displaying Data in Gridview  
        protected void ShowData()
        {
            dt = new DataTable();
            con = new SqlConnection(cs);
            con.Open();
            adapt = new SqlDataAdapter("Select Application.Id, Application.AppName, Application.Priority, Task.TimeTaken from Application, Section, Task where  Application.Id = Section.AppId AND  Section.Id = Task.SecId", con);
            adapt.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }            
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                DropDownList DropDownList1 = (e.Row.FindControl("ddlPriority") as DropDownList);

                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Application", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                DropDownList1.DataSource = dt;

                DropDownList1.DataTextField = "Priority";
                DropDownList1.DataValueField = "Priority";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("--Select Priority--", "0"));


            }

        }
    }
}