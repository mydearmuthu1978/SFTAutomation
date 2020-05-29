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
    public partial class SectionDetails : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlDataAdapter da = null;
        SqlDataReader dr = null;
        SqlCommand cmd = null;
        DataSet ds = null;
        string sqlQry = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["myDbConnection"].ConnectionString;
            if (!Page.IsPostBack)
            {
                //GridViewBind();                
                Bind_Data();
            }
        }
        private void Bind_Data()
        {
            //sqlQry = "select e.AppName,e.Section,e.SectionType,e.TimeTaken from App_Details e,App_testcases d where e.ID=d.Id";
            ////sqlQry = "select * from App_Details";
            //da = new SqlDataAdapter(sqlQry, con);
            //ds = new DataSet();
            //da.Fill(ds, "App_Details");
            ////da.Fill(ds);
            //GridView1.DataSource = ds.Tables[0];
            //GridView1.DataMember = "App_Details";
            //GridView1.DataBind();

            //New logic

            //GridView1.DataSource = ds.Tables[0].Rows.Count;
            SqlCommand cmd = new SqlCommand("select ID, Appname, Section, SectionType ,TimeTaken from TransactionDetails", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        void End_Block(Panel p)
        {
            foreach (dynamic txtBox in p.Controls)
            {

                if (txtBox is TextBox)
                    txtBox.Text = String.Empty;

            }
            mpe1.Hide();
            mpe2.Hide();
            mpe3.Hide();
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("Insert into TransactionDetails (Appname,Section,SectionType,TimeTaken ) values(@Appname, @Section, @SectionType, @TimeTaken)", con);
                cmd.Parameters.AddWithValue("@Appname", txtName1.Text);
                cmd.Parameters.AddWithValue("@Section", txtSection.Text);
                cmd.Parameters.AddWithValue("@SectionType", RadioButtonList1.Text);
                cmd.Parameters.AddWithValue("@TimeTaken", txtTimeTaken.Text);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                mpe1.Hide();
                con.Close();
            }
            catch
            {
                lblStatus1.Text = "Record is not added";
                mpe1.Show();
            }
            finally
            {
                Bind_Data();
                cmd.Dispose();
                con.Close();
            }
        }
        protected void Cancel1_Click(object sender, EventArgs e)
        {
            End_Block(panelAddNew);
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                GridViewRow gr = (GridViewRow)lnk.NamingContainer;
                string tempID = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                ViewState["tempId"] = tempID;
                sqlQry = "select AppName,Section,SectionType,TimeTaken from TransactionDetails where ID=" + tempID;
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand(sqlQry, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtName2.Text = dr["AppName"].ToString();
                    txtSection2.Text = dr["Section"].ToString();
                    RadioButtonList2.SelectedValue = dr["SectionType"].ToString();
                    txtTimeTaken2.Text = dr["TimeTaken"].ToString();
                }
                mpe2.Show();
            }
            catch
            {
                return;
            }
            finally
            {
                cmd.Dispose();
                dr.Close();
                con.Close();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int tempID = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
                ViewState["tempId"] = tempID;

                //sqlQry = "select AppName,Section,SectionType,TimeTaken from App_Details where ID=" + tempID;
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("update TransactionDetails Set Appname = @Appname, Section = @Section, SectionType = @SectionType, TimeTaken = @TimeTaken where ID = tempID", con);
                cmd.Parameters.AddWithValue("@Appname", txtName2.Text);
                cmd.Parameters.AddWithValue("@Section", txtSection2.Text);
                cmd.Parameters.AddWithValue("@SectionType", RadioButtonList2.Text);
                cmd.Parameters.AddWithValue("@TimeTaken", txtTimeTaken2.Text);
                //cmd.Parameters.AddWithValue("@ID", ID);
                //Assign the connection to command
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();

            }
            catch
            {
                lblStatus1.Text = "Record is not updated";
                mpe2.Show();
            }
            finally
            {
                Bind_Data();
                cmd.Dispose();
                con.Close();
            }
        }
        protected void Cancel2_Click(object sender, EventArgs e)
        {
            End_Block(panelEdit);
        }
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                GridViewRow gr = (GridViewRow)lnk.NamingContainer;
                string tempID = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                ViewState["tempId"] = tempID;
                sqlQry = "select Appname from TransactionDetails where Id=" + tempID;
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand(sqlQry, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblId.Text = dr.GetValue(0).ToString();
                }
                mpe3.Show();
            }
            catch
            {
                return;
            }
            finally
            {
                cmd.Dispose();
                dr.Close();
                con.Close();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand();
                //cmd.CommandText = "prcDeleteAppSection";
                cmd = new SqlCommand("delete from TransactionDetails where ID = @ID", con);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID ", Convert.ToInt16(ViewState["tempId"].ToString()));
                cmd.ExecuteNonQuery();
                mpe3.Hide();
                con.Close();
            }
            catch
            {
                lblStatus3.Text = "Record not deleted";
                mpe3.Show();
            }
            finally
            {
                Bind_Data();
                cmd.Dispose();
                con.Close();
            }
        }
    }
}