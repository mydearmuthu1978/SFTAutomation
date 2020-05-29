using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace projSFT
{
    public partial class Topmenu : System.Web.UI.Page
    {
        SqlConnection con;
        public Topmenu()
        {
            string conString = string.Empty;
            try
            {
                con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myDbConnection"].ToString();
                //conString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
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
                bindmenu();
                //Menu Menu = (Menu)Page.Master.FindControl("NavigationMenu");
                Menu Menu = (Menu)Page.FindControl("NavigationMenu");
                if (Menu.Items.Count > 0)
                {
                    foreach (MenuItem mi in Menu.Items)
                    {
                        if (mi.Text == "Scope")
                        {
                            mi.Selected = true;
                        }
                    }
                }
            }

        }
        private void bindmenu()
        {
            con.Open();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = "select * from [top_menu]";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(ds);
            dt = ds.Tables[0];
            DataRow[] droparent = dt.Select("Id >" + 0);
            foreach (DataRow dr in droparent)
            {
                //NavigationMenu.Items.Add(new MenuItem(dr["Id"].ToString(), dr["menuname"].ToString()));
                NavigationMenu.Items.Add(new MenuItem(dr["menuname"].ToString()));
            }
            //foreach (DataRow dr in dt.Select("Id >" + 0))
            //{
            //    MenuItem men1 = new MenuItem(dr["Id"].ToString(), dr["menuname"].ToString());
            //    NavigationMenu.FindItem(dr["Id"].ToString()).ChildItems.Add(men1);
            //}
            using (SqlCommand cmd = new SqlCommand("SELECT Id, name FROM programs"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                //con.Open();
                ProgramList.DataSource = cmd.ExecuteReader();
                ProgramList.DataTextField = "name";
                ProgramList.DataValueField = "Id";
                ProgramList.DataBind();

                ProgramList.Rows = ProgramList.Items.Count;

                //find specific items from listbox
                var items = from ListItem li in ProgramList.Items
                            where li.Text.Contains("CON")
                            select li;

                //programmatically select specific items 
                //by default first time those items are selected.
                foreach (ListItem li in items)
                {
                    li.Selected = true;
                }
                //con.Close();
            }
        }
        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ToString();
            //if (NavigationMenu.SelectedItem.Value == "1")
            //{
            //    SqlCommand cmd = new SqlCommand("Select * from App_testcases", con);
            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();
            //}

            //if (e.Item.Selected == true)
            //{                
            //    Response.Redirect("~/main.aspx");
            //}

            //for (int i = 0; i < NavigationMenu.Items.Count; i++)
            //{
            //    String tmpNav = NavigationMenu.Items[i].NavigateUrl.ToLower();
            //    if (tmpNav.Contains(tmpNav))
            //    {
            //        NavigationMenu.Items[i].Selected = true;
            //    }
            //}
            //Response.Redirect("~/content.aspx");

            //Get list of the programs.

            ProgramList.Rows = ProgramList.Items.Count;

            //string val = ProgramList.Text.Trim();

            //find specific items from listbox
            var items = from ListItem li in ProgramList.Items
                        where li.Text.Contains(ProgramList.Text)
                        select li;

            // Get the menu item being bound to data.
            MenuItem item = e.Item;

            // Use the Selected property to select the Home 
            // menu item when the page is first loaded.
            if (item.Text == "Scope")
            {
                item.Selected = true;
                Response.Redirect("~/content.aspx");
            }
            else if (item.Text == "Summary")
            {
                item.Selected = true;
                Response.Redirect("~/main.aspx");
            }
            else if (item.Text == "Estimate")
            {
                item.Selected = true;
            }

        }

        protected void Menu1_MenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
                {
                    e.Item.Selected = true;
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("lblID");
            //con.Open();
            SqlCommand cmd = new SqlCommand("delete FROM detail where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", con);
            cmd.ExecuteNonQuery();
            //con.Close();
            GridView1.DataBind();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataBind();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];

            TextBox txt_Appname = (TextBox)row.Cells[0].Controls[0];
            TextBox txt_Priority = (TextBox)row.Cells[1].Controls[0];
            TextBox txt_Testcases = (TextBox)row.Cells[2].Controls[0];

            GridView1.EditIndex = -1;
            con.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);  
            SqlCommand cmd = new SqlCommand("update App_testcases set Appname='" + txt_Appname.Text + "',Priority='" + txt_Priority.Text + "',Testcases='" + txt_Testcases.Text + "'", con);
            //SqlCommand cmd = new SqlCommand("update App_testcases set Appname='" + txt_Appname.Text + "',Priority='" + txt_Priority.Text + "',Testcases='" + txt_Testcases.Text + "' where Appname=" + txt_Appname, con);
            //SqlCommand cmd = new SqlCommand("update App_testcases set Appname='" + txt_Appname.Text + "',Priority='" + txt_Priority.Text + "',Testcases='" + txt_Testcases.Text + "'where Id=" + Convert.ToInt32(Id.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }
    }
}