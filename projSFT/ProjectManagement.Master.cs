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
    public partial class ProjectManagement : System.Web.UI.MasterPage
    {
        SqlConnection con;
        public string program = string.Empty;
        public string tPage = string.Empty;

        public void Topmenu()
        {

            try
            {
                con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to retrieve 'ConnectionString' from configuration file.");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bindmenu();

                BindMenuData();
                lblUsername.Text = "Welcome " + (string)Session["Username"];
            }

        }
        private void BindMenuData()
        {
            ///new logic
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            SqlCommand cmd = new SqlCommand("select Id, ProgName from Program", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DataRow[] droparent = dt.Select("Id >" + 0);
            foreach (DataRow dr in droparent)
            {
                //VerticalMenu bind corresponding it to index bind to estimate page
                VerticalMenu.Items.Add(new MenuItem(dr["ProgName"].ToString(), dr["id"].ToString()));
            }
        }
        private void Bindmenu()
        {
            ///new logic
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            SqlCommand cmd = new SqlCommand("select Id, Menuname from Top_menu", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DataRow[] droparent = dt.Select("Id >" + 0);
            foreach (DataRow dr in droparent)
            {
                NavigationMenu.Items.Add(new MenuItem(dr["Menuname"].ToString(), dr["id"].ToString()));
            }
        }
        /// <summary>
        /// Displaying Top menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NavigationMenu_MenuItemClick1(object sender, MenuEventArgs e)
        {
            MenuItem item1 = e.Item;
            ViewState["item1"] = item1.Text;
            item1.Selected = true;
        }
        protected void Menu1_MenuItemDataBound1(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
                {
                    e.Item.Selected = true;
                }
            }
        }
        ///
        ///Displaying left menu item
        ///
        public void NavigationMenu_MenuItemClick2(object sender, MenuEventArgs e)
        {
            MenuItem item2 = e.Item;
            ViewState["item2"] = item2.Value;


            item2.Selected = true;
            NavigateMe();
        }
        public void NavigateMe()
        {

            if (ViewState["item1"] != null)
                tPage = ViewState["item1"].ToString();
            if (ViewState["item2"] != null)
                program = ViewState["item2"].ToString();

            switch (tPage)
            {
                case "Scope":
                    Session["id"] = program;
                    Response.Redirect("~/Scope.aspx?Program=" + program);
                    break;

                case "Summary":
                    Session["id"] = program;
                    Response.Redirect("~/Summary.aspx?Program=" + program);
                    break;
                case "Estimate":
                    Session["id"] = program;
                    Response.Redirect("~/Estimates.aspx?Program=" + program);
                    break;
                default:
                    break;
            }

        }
        protected void Menu1_MenuItemDataBound2(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
                {
                    e.Item.Selected = true;
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("prcUpdatePassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Session["Id"]);
                cmd.Parameters.AddWithValue("@password", Encryptpassword(txtPassword.Text));
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                Response.Write("<script>alert('Password Updated Successfully!');</script>");

            }
        }

        public string Encryptpassword(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
    }
}