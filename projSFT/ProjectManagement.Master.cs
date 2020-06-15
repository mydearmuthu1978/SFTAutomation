﻿using System;
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
    public partial class ProjectManagement : System.Web.UI.MasterPage
    {
        SqlConnection con;
        public void Topmenu()
        {
            string conString = string.Empty;
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
                bindmenu();              

                BindMenuData();                
            }

        }
        private void BindMenuData()
        {
            ///new logic
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            SqlCommand cmd = new SqlCommand("select Id, name from programs", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DataRow[] droparent = dt.Select("Id >" + 0);
            foreach (DataRow dr in droparent)
            {
                VerticalMenu.Items.Add(new MenuItem(dr["name"].ToString())); 
                //VerticalMenu bind corresponding it to index bind to estimate page
            }
        }
        private void bindmenu()
        {            
            ///new logic
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            SqlCommand cmd = new SqlCommand("select Id, menuname from top_menu", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            DataRow[] droparent = dt.Select("Id >" + 0);
            foreach (DataRow dr in droparent)
            {
                NavigationMenu.Items.Add(new MenuItem(dr["menuname"].ToString()));
            }
        }
        /// <summary>
        /// Displaying Top menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NavigationMenu_MenuItemClick1(object sender, MenuEventArgs e)
        {
            MenuItem item = e.Item;
            if (item.Text == "Scope")
            {
                item.Selected = true;
                Response.Redirect("~/Topmenu.aspx");
            }

            else if (item.Text == "Summary")
            {
                item.Selected = true;
                Response.Redirect("~/Summary.aspx");
            }
            else if (item.Text == "Estimate")
            {
                item.Selected = true;
                Response.Redirect("Estimates.aspx");
            }
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
        protected void NavigationMenu_MenuItemClick2(object sender, MenuEventArgs e)
        {
            
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
    }
}