using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projSFT
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
           
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myDbConnection"].ToString());
                SqlCommand cmd = new SqlCommand("Insert into users values(@username, @password, @firstname, @lastname)", con);  
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@firstname", txtFirstname.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLastname.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                txtUsername.Text = "";
                txtPassword.Text = "";
                txtFirstname.Text = "";
                txtLastname.Text = "";
                Response.Write("<script>alert('Registered Successfully!');</script>");
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}