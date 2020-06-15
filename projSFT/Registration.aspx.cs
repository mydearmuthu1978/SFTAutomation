using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        public string Encryptpassword(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string userType = "A";
                if (drpUserType.SelectedItem.Value == "Mphasis_Developer")
                {
                    userType = "D";
                }
                string strpass = Encryptpassword(txtPassword.Text);
                SqlCommand cmd = new SqlCommand("SP_InsertUserDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", strpass);
                cmd.Parameters.AddWithValue("@firstname", txtFirstname.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLastname.Text);
                cmd.Parameters.AddWithValue("@usertype", userType);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

                txtUsername.Text = "";
                txtPassword.Text = "";
                txtFirstname.Text = "";
                txtLastname.Text = "";
                drpUserType.SelectedValue = "";
                Response.Write("<script>alert('Registered Successfully!');</script>");
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}