using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projSFT
{
    public partial class Estimates : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        private int numOfRows = 1;
        Table Tabletask = new Table();
        //IDictionary<string, string> TaskList = new Dictionary<string, string>();
        List<string> TaskList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ConnectionString;
            if (!Page.IsPostBack)
            {
                this.BindApplication(ddlApplication);
                this.RepeaterBind();
                //GenerateTable(numOfRows);
                //Session.Remove("clicks");
            }
            if((string)ViewState["I_am_panel"] == "0")
            {
                this.RepeaterBind();
                ViewState["I_am_panel"] =1;
            }

        }
        private void RepeaterBind()
        {
            con.Open();
            SqlCommand com = new SqlCommand("select * from Section where AppId=" + ddlApplication.SelectedValue, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Repeater1.DataSource = null;
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        private void BindApplication(DropDownList DropDownList2)
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            SqlCommand com = new SqlCommand("select * from Application", con);
            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();
            da.Fill(ds);
            ddlApplication.DataTextField = ds.Tables[0].Columns["AppName"].ToString();
            ddlApplication.DataValueField = ds.Tables[0].Columns["Id"].ToString();
            ddlApplication.DataSource = ds.Tables[0];
            ddlApplication.DataBind();
            ddlApplication.Items[ddlApplication.Items.Count - 1].Text = "new application";
        }
        protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater taskRep = (Repeater)e.Item.FindControl("Repeater2");
                //List<string> notes = new List<string>();
                //notes.Add("Task1");
                //notes.Add("Task2");
                //taskRep.DataSource = from c in notes select new { NAME = c }; ;
                //taskRep.DataBind();
                HiddenField hf = (HiddenField)e.Item.FindControl("hfSectionId");

                SqlCommand com = new SqlCommand("select * from Task where SecId=" + hf.Value, con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                taskRep.DataSource = dt;
                taskRep.DataBind();
            }
        }
        private DataTable getData(string sQuery)
        {
            SqlCommand cmd = new SqlCommand("select * from SectionDetails", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            return dt;
        }
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            TaskList = (List<string>)Session["TaskListCollection"];
            RepeaterBind();
            
        }
        void End_Block(Panel p)
        {
            foreach (dynamic txtBox in p.Controls)
            {

                if (txtBox is TextBox)
                    txtBox.Text = String.Empty;

            }
            mpe1.Hide();
            //mpe2.Hide();
            //mpe3.Hide();
        }
        // Button click event for to insert Section and Task.
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            //int rowsCount = (Int32)ViewState["RowsCount"];

            //SetPreviousData(rowsCount-1, 2);
            int sectionId = 0;
            try
            {
                con.Open();
                cmd = new SqlCommand("Insert into Section (AppId,SecName,SecType,TotalTestCases) values(@AppId,@SecName, @SecType, @TotalTestCases); select SCOPE_IDENTITY()", con);
                cmd.Parameters.AddWithValue("@AppId", ddlApplication.SelectedValue);
                cmd.Parameters.AddWithValue("@SecName", txtSectionname.Text);
                cmd.Parameters.AddWithValue("@SecType", RadioButtonList1.Text);
                cmd.Parameters.AddWithValue("@TotalTestCases", txtTotaltestcase.Text);
                Int32.TryParse(cmd.ExecuteScalar().ToString(), out sectionId);

                cmd.Dispose();
                mpe1.Hide();
                con.Close();
                TaskList = (List<string>)Session["TaskListCollection"];
                InsertTask(TaskList, sectionId);
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Records are Saved Successfuly!');", true);
                lblStatus1.Text = "Record is not added";
                mpe1.Show();
            }
            ViewState["I_am_panel"] = 0;
        }
        // An Insert Method  
        private void InsertTask(List<string> TaskList, int sectionId)
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            StringBuilder sb = new StringBuilder(string.Empty);
            string[] splitItems = null;

            // new code refactaring requierd.            
            con.Open();
            foreach (string task in TaskList)
            {
                splitItems = new string[2];
                
                splitItems = task.ToString().Split('|');
                sb.Append(" INSERT INTO Task ");
                sb.Append(" ([TaskName]  ");
                sb.Append(" ,[TimeTaken] ");
                sb.Append(" ,[SecId] ");

                //sb.Append(" VALUES       ");
                //sb.Append(" (@SecId  ");
                //sb.Append(" (@TaskName  ");
                //sb.Append(" ,@TimeTaken   ");

                //cmd.Parameters["@SecId"].Value = sectionId;
                //cmd.Parameters["@TaskName"].Value = splitItems[1];
                //cmd.Parameters["@TimeTaken"].Value = splitItems[1];
                //cmd.ExecuteNonQuery();

                sb.Append(" VALUES       ");
                sb.Append(" (SecId  ");
                sb.Append(" (TaskName  ");
                sb.Append(" ,TimeTaken   ");

                cmd = new SqlCommand("insert into Task(TaskName,SecId,TimeTaken) values('" + splitItems[0] + "','" + sectionId + "','" + splitItems[1] + "')", con);

                //cmd.Parameters["SecId"].Value = sectionId;
                //cmd.Parameters["TaskName"].Value = splitItems[1];
                //cmd.Parameters["TimeTaken"].Value = splitItems[1];
                cmd.ExecuteNonQuery();                
            }
            con.Close();
            //////try
            //////{
            //////    con.Open();
            //////    SqlCommand cmd = new SqlCommand(sb.ToString(), con);
            //////    cmd.CommandType = CommandType.Text;
            //////    cmd.ExecuteNonQuery();
            //////    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Records are Saved Successfuly!');", true);
            //////}
            //////catch (System.Data.SqlClient.SqlException ex)
            //////{
            //////    string msg = "Insert Error:";
            //////    msg += ex.Message;
            //////    throw new Exception(msg);
            //////}
            //////finally
            //////{
            //////    con.Close();
            //////}
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        private void GenerateTable(int rowsCount)
        {
            //Session["mytable"] =(Table) plcholder.FindControl("Tabletask");

            Tabletask = new Table();
            Tabletask.ID = "Tabletask";

            plcholder.Controls.Add(Tabletask);

            //The number of Columns to be generated
            const int colsCount = 2;
           // Table Tabletask = (Table)panelAddNew.FindControl("Tabletask");
            // Now iterate through the table and add your controls

            for (int i = 0; i < rowsCount; i++)
            {
                TableRow row = new TableRow();              

                for (int j = 0; j < colsCount; j++)
                {
                    TableCell cell = new TableCell();                   
                    TextBox tb = new TextBox();
                    
                    // Set a unique ID for each TextBox added
                    tb.ID = "TextBoxRow_" + i + "Col_" + j;

                    // Add the control to the TableCell
                    cell.Controls.Add(tb);

                    // Add the TableCell to the TableRow
                    row.Cells.Add(cell);
                }
                // And finally, add the TableRow to the Table
                Tabletask.Rows.Add(row);
            }

            //Set Previous Data on PostBacks
            SetPreviousData(rowsCount, colsCount);

            //Sore the current Rows Count in ViewState
            rowsCount++;
            ViewState["RowsCount"] = rowsCount;
        }

        protected void btnAddNewRow_Click(object sender, EventArgs e)
        {
         
            if (ViewState["RowsCount"] != null)
            {
                numOfRows = Convert.ToInt32(ViewState["RowsCount"].ToString());
            }
         
            GenerateTable(numOfRows);

            mpe1.Show();

        }
        private void SetPreviousData(int rowsCount, int colsCount)
        {
            Table Tabletask1 = (Table)plcholder.FindControl("Tabletask");

            //Table table = (Table)Page.FindControl("Tabletask");
            string taskname = string.Empty;
            string timetaken = string.Empty;
            TaskList = (List<string>)Session["TaskListCollection"];

            if (Tabletask1 != null)
            {
                for (int i = 0; i < rowsCount; i++)
                {
                    string[] task=null;                   
                    if (TaskList != null && TaskList.Count>i)
                        task = TaskList[i].Split('|');
                   
                    for (int j = 0; j < colsCount; j++)
                    {
                        TextBox tb = (TextBox)panelAddNew.FindControl("TextBoxRow_" + i + "Col_" + j);                      
                        try
                        {
                            if(task!=null)
                            tb.Text = !string.IsNullOrEmpty(task[j].ToString()) ? task[j].ToString() : string.Empty;
                        }
                        catch
                        {
                            if (tb != null)
                            {
                                tb.Text = string.Empty;
                            }
                        }
                        
                    }

                }
            }
           
        }

        protected void Cancel1_Click(object sender, EventArgs e)
        {
            ViewState["RowsCount"] = 1;
            ViewState["I_am_panel"] = 0;
            End_Block(panelAddNew);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {            
            //need to handle null exception on the dropdown chnage when section panel is not present.
            try
            {
                List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("TextBoxRow_")).ToList();
                for (int i = 0; i < keys.Count; i += 2)
                {
                    if (Request.Form[keys[i].ToString()] != "")
                    {
                        TaskList.Add(Request.Form[keys[i].ToString()] + "|" + Request.Form[keys[i + 1].ToString()]);
                    }
                }
                Session["TaskListCollection"] = TaskList;
            }
            catch
            {

            }
            //TaskList= new Dictionary<string, string>();
            
        }
    }
}