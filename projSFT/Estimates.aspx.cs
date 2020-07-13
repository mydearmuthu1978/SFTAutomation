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
        SqlDataReader dr = null;
        private int numOfRows = 1;
        Table Tabletask = new Table();
        //IDictionary<string, string> TaskList = new Dictionary<string, string>();
        List<string> TaskList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ConnectionString;
            var master = Page.Master as ProjectManagement;
            string str = (string)Session["id"];
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
            if (!Page.IsPostBack)
            {
                this.BindApplication(ddlApplication);
                this.RepeaterBind();
            }

        }
        //Bind repeater1 with section details.
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
            con.Close();
        }
        //Bind dropdownlist with application.
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
            ddlApplication.Items.Insert(0, new ListItem("select", "0"));
        }
        //Bind task details with repeater2.
        protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater taskRep = (Repeater)e.Item.FindControl("Repeater2");

                HiddenField hf = (HiddenField)e.Item.FindControl("hfSectionId");

                SqlCommand com = new SqlCommand("select * from Task where SecId=" + hf.Value, con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                taskRep.DataSource = dt;
                taskRep.DataBind();
            }
        }
        //Getting section details from Section table.
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
        //Get selected application section and task details.
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApplication.SelectedItem.Text == "new application")
            {
                btnAddNew1.Visible = false;
                btnAddNewApp.Visible = true;
                RepeaterBind();
            }
            else
            {
                btnAddNew1.Visible = true;
                btnAddNewApp.Visible = false;

                TaskList = (List<string>)Session["TaskListCollection"];
                RepeaterBind();
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
            mpe4.Hide();
        }
        // Button click event for to insert new Section and Task.
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
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

                //Display success message.
                string message = "Your details have been saved successfully.";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Record has been inserted successfully.')", true);
                mpe1.Hide();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Records are Saved Successfuly!');", true);
                lblStatus.Text = "Record is not added";
                mpe1.Show();
            }
            finally
            {
                RepeaterBind();
                cmd.Dispose();
                con.Close();
            }
            ViewState["I_am_panel"] = 0;
        }
        //Inserting task using InsertTask method  
        public void InsertTask(List<string> TaskList, int sectionId)
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

                sb.Append(" VALUES       ");
                sb.Append(" (SecId  ");
                sb.Append(" (TaskName  ");
                sb.Append(" ,TimeTaken   ");

                cmd = new SqlCommand("insert into Task(TaskName,SecId,TimeTaken) values('" + splitItems[0] + "','" + sectionId + "','" + splitItems[1] + "')", con);

                cmd.ExecuteNonQuery();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Task has been inserted successfully.')", true);
            }
            con.Close();
        }
        //Update Task details.
        private void UpdateTask(List<string> TaskList)
        {
            int sectionId = int.Parse(hfSectionIdEdit.Value);

            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString();
            StringBuilder sb = new StringBuilder(string.Empty);
            string[] splitItems = null;

            con.Open();
            foreach (string task in TaskList)
            {
                splitItems = new string[2];
                splitItems = task.ToString().Split('|');

                //check splitItems[3]==1 then call the delete task.
                int chkDelete = int.Parse(splitItems[3]);

                //Insert new task if taskId is 0 otherwise update task                
                int? taskId = null;
                taskId = int.Parse(splitItems[0]);
                //int i = taskId ?? 0;                
                if (taskId == null || taskId == 0)
                {
                    //InsertTask(TaskList, sectionId);
                    //For new task inserting recors here.
                    cmd = new SqlCommand("insert into Task(TaskName,SecId,TimeTaken) values('" + splitItems[1] + "','" + sectionId + "','" + splitItems[2] + "')", con);
                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Task has been inserted successfully.')", true);
                }
                else if (chkDelete == 1)
                {
                    //deleting selected task from section.                    
                    cmd = new SqlCommand("delete from Task where Id = '" + splitItems[0] + "'", con);
                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Task has been inserted successfully.')", true);
                }
                else
                {
                    sb.Append(" UPDATE Task SET ");
                    sb.Append(" ([TaskName]  ");
                    sb.Append(" ,[TimeTaken] ");
                    sb.Append(" ,[Id] ");

                    sb.Append(" VALUES       ");
                    sb.Append(" (Id  ");
                    sb.Append(" (TaskName  ");
                    sb.Append(" ,TimeTaken   ");

                    // make chnages base on the taskId
                    cmd = new SqlCommand("UPDATE Task set TaskName = '" + splitItems[1] + "'" + ",TimeTaken = '" + splitItems[2] + "'" + " WHERE Id = '" + taskId + "'", con);

                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "alert('Task has been updated successfully.')", true);
                }
            }
            con.Close();
        }
        // Button click event for to insert new Application, section and Task.
        protected void btnAddAppSectionTask_Click(object sender, EventArgs e)
        {
            int sectionId = 0;

            TextBox txtAppName = (TextBox)panelAddNewApplication.FindControl("txtAppName");
            DropDownList ddlPriority = (DropDownList)panelAddNewApplication.FindControl("ddlPriority1");
            TextBox txtAppSectionName = (TextBox)panelAddNewApplication.FindControl("txtAppSectionName");
            RadioButtonList rblSectionType = (RadioButtonList)panelAddNewApplication.FindControl("rblSectionType");
            TextBox txtAppTotalTestCases = (TextBox)panelAddNewApplication.FindControl("txtAppTotalTestCases");

            try
            {
                var master = Page.Master as ProjectManagement;
                string str = (string)Session["Id"];
                if (str != null)
                {
                    //con.Open();
                    SqlCommand cmd = new SqlCommand("prcInsertAppSectionsTasks", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AppName", txtAppName.Text);
                    cmd.Parameters.AddWithValue("@ProgramId", str);
                    cmd.Parameters.AddWithValue("@Priority", ddlPriority.Text);
                    cmd.Parameters.AddWithValue("@SecName", txtAppSectionName.Text);
                    cmd.Parameters.AddWithValue("@SecType", rblSectionType.Text);
                    cmd.Parameters.AddWithValue("@TotalTestCases", txtAppTotalTestCases.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmd.Dispose();
                    con.Close();

                    //Insert Task details for new application.
                    TaskList = (List<string>)Session["TaskListCollection"];
                    InsertTask(TaskList, sectionId);
                    mpe4.Hide();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Records are Saved Successfuly!');", true);
                lblStatus.Text = "Record is not added";
                mpe4.Show();
            }
            finally
            {
                RepeaterBind();
                cmd.Dispose();
                con.Close();
            }
            ViewState["I_am_panel"] = 0;
        }
        //Open edit panel of Section & Task details.
        protected void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                //mpe2.Show();
                Button button = (sender as Button);
                RepeaterItem item = button.NamingContainer as RepeaterItem;
                HiddenField hf = (HiddenField)item.FindControl("hfSectionId");

                int SectionId = int.Parse(hf.Value);
                string sqlQry = "select Id, SecName,SecType,TotalTestCases from Section where Id =" + SectionId + " and AppId = " + ddlApplication.SelectedValue;

                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand(sqlQry, con);
                dr = cmd.ExecuteReader();

                TextBox txtBox = (TextBox)panelEdit.FindControl("txtEditSectionName");
                RadioButtonList RadioButtonList = (RadioButtonList)panelEdit.FindControl("RadioButtonList2");
                TextBox txtBox3 = (TextBox)panelEdit.FindControl("txtEdittestcases");
                if (txtBox != null && RadioButtonList != null && txtBox3 != null)
                {
                    if (dr.Read())
                    {
                        hfSectionIdEdit.Value = dr["Id"].ToString();
                        txtEditSectionName.Text = dr["SecName"].ToString();
                        RadioButtonList2.SelectedValue = dr["SecType"].ToString();
                        txtEdittestcases.Text = dr["TotalTestcases"].ToString();
                    }
                }
                dr.Close();
                GenerateEditTable(SectionId);
                mpe2.Show();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return;
            }
            finally
            {
                cmd.Dispose();
                dr.Close();
                con.Close();
            }
        }
        //Update Section & Task details.
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (sender as Button);
                RepeaterItem item = button.NamingContainer as RepeaterItem;

                //Section Hidden field Id.
                int sectionId = int.Parse(hfSectionIdEdit.Value);

                TextBox txtSectionName = (TextBox)panelEdit.FindControl("txtEditSectionName");
                RadioButtonList ddlSectionType = (RadioButtonList)panelEdit.FindControl("RadioButtonList2");
                TextBox txtTotalTestCase = (TextBox)panelEdit.FindControl("txtEdittestcases");

                //Prepare the Update Command of the DataSource control
                string strSQL = "";
                strSQL = "UPDATE Section set SecName = '" + txtSectionName.Text + "'" +
                         ",SecType = '" + ddlSectionType.SelectedItem + "'" +
                         ",TotalTestCases = '" + txtTotalTestCase.Text + "'" +
                         " WHERE Id = '" + sectionId + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand(strSQL, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                TaskList = (List<string>)Session["TaskListCollection"];
                UpdateTask(TaskList);

                //Display success message.
                string message = "Your details have been updated successfully.";
                string script = "window.onload = function(){ alert('";
                script += message;
                script += "')};";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                mpe2.Hide();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                lblStatus1.Text = "Record is not updated";
                mpe2.Show();
            }
            finally
            {
                RepeaterBind();
                cmd.Dispose();
                con.Close();
            }
        }

        //Generating dynamically textbox for task details.
        private void GenerateTable(int rowsCount)
        {
            Tabletask = new Table();
            Tabletask.ID = "Tabletask";

            plcholder.Controls.Add(Tabletask);

            //The number of Columns to be generated
            const int colsCount = 2;
            // Now iterate through the table and add your controls

            for (int i = 0; i < rowsCount; i++)
            {
                TableRow row = new TableRow();

                for (int j = 0; j < colsCount; j++)
                {
                    TableCell cell = new TableCell();
                    TextBox tb = new TextBox();
                    tb.TextMode = TextBoxMode.MultiLine;

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
        //Generating dynamically textbox for task details.
        protected void btnAddNewRow_Click(object sender, EventArgs e)
        {

            if (ViewState["RowsCount"] != null)
            {
                numOfRows = Convert.ToInt32(ViewState["RowsCount"].ToString());
            }

            GenerateTable(numOfRows);

            mpe1.Show();

        }
        //Generate new row for new Application with Section and Task.
        private void GenerateNewAppTable(int rowsCount)
        {
            Tabletask = new Table();
            Tabletask.ID = "Tabletask";

            NewAppPlaceHolder.Controls.Add(Tabletask);

            //The number of Columns to be generated
            const int colsCount = 2;

            for (int i = 0; i < rowsCount; i++)
            {
                TableRow row = new TableRow();

                for (int j = 0; j < colsCount; j++)
                {
                    TableCell cell = new TableCell();
                    TextBox tb = new TextBox();
                    tb.TextMode = TextBoxMode.MultiLine;

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
            SetAppPreviousData(rowsCount, colsCount);

            //Sore the current Rows Count in ViewState
            rowsCount++;
            ViewState["RowsCount"] = rowsCount;
        }
        //Set previous data for new application.
        private void SetAppPreviousData(int rowsCount, int colsCount)
        {
            Table Tabletask1 = (Table)NewAppPlaceHolder.FindControl("Tabletask");

            string taskname = string.Empty;
            string timetaken = string.Empty;
            TaskList = (List<string>)Session["TaskListCollection"];

            if (Tabletask1 != null)
            {
                for (int i = 0; i < rowsCount; i++)
                {
                    string[] task = null;
                    if (TaskList != null && TaskList.Count > i)
                        task = TaskList[i].Split('|');

                    for (int j = 0; j < colsCount; j++)
                    {
                        TextBox tb = (TextBox)panelAddNewApplication.FindControl("TextBoxRow_" + i + "Col_" + j);
                        tb.TextMode = TextBoxMode.MultiLine;
                        try
                        {
                            if (task != null)
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
        //Added new task list for the new application
        protected void btnAddNewAppRow_Click(object sender, EventArgs e)
        {
            if (ViewState["RowsCount"] != null)
            {
                numOfRows = Convert.ToInt32(ViewState["RowsCount"].ToString());
            }

            GenerateNewAppTable(numOfRows);

            mpe4.Show();

        }

        //this is calling only at the repeater values.
        //private void GenerateEditTable(int SectionId, int rowsCount)
        private void GenerateEditTable(int SectionId)
        {
            try
            {
                Tabletask = new Table();
                Tabletask.ID = "Tabletask";
                EditPlaceHolder.Controls.Add(Tabletask);
                string sqlQry = "select Id, TaskName, TimeTaken from Task where SecId =" + SectionId;
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand(sqlQry, con);
                dr = cmd.ExecuteReader();

                int i = 0;
                while (dr.Read())
                {
                    TableRow row = new TableRow();
                    for (int j = 0; j < 4; j++)
                    {
                        TableCell cell = new TableCell();

                        if (j == 0) // first cell for hiddlen value to store Task ID
                        {
                            HiddenField hfTaskIdEdit = new HiddenField();
                            hfTaskIdEdit.ID = "hfTaskIdEditRow_" + i + "Col_" + j;
                            hfTaskIdEdit.Value = dr[j].ToString();
                            cell.Controls.Add(hfTaskIdEdit);

                        }
                        else if (j == 3)
                        {
                            //Added dynamic delete button for every task.
                            CheckBox chkDelete = new CheckBox();
                            chkDelete.ID = "chkDelRow_" + i + "Col_" + j;

                            cell.Controls.Add(chkDelete);
                        }
                        else // cell 1 for task name & cell 2 for time taken
                        {
                            TextBox tb = new TextBox();
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.ID = "TextBoxRow_" + i + "Col_" + j;
                            tb.Text = dr[j].ToString();
                            cell.Controls.Add(tb);

                        }
                        // Add the TableCell to the TableRow
                        row.Cells.Add(cell);
                    }
                    i++;
                    // And finally, add the TableRow to the Table
                    Tabletask.Rows.Add(row);
                    //Sore the current Rows Count in ViewState
                    //rowsCount++;
                    ViewState["RowsCount"] = Tabletask.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                cmd.Dispose();
                dr.Close();
                con.Close();
            }

        }
        //Generate method for calling only add new row .
        private void EditGenerateTable(int rowsCount)
        {
            Tabletask = new Table();
            Tabletask.ID = "Tabletask";
            EditPlaceHolder.Controls.Add(Tabletask);

            //The number of Columns to be generated
            const int colsCount = 3; // 0 for hidden control , 1 Task name, 2 for Time taken and 4th one is delete button.
            for (int i = 0; i < rowsCount; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < 4; j++)
                {
                    TableCell cell = new TableCell();


                    if (j == 0) // first cell for hiddlen value to store Task ID
                    {
                        HiddenField hfTaskIdEdit = new HiddenField();
                        hfTaskIdEdit.ID = "hfTaskIdEditRow_" + i + "Col_" + j;
                        cell.Controls.Add(hfTaskIdEdit);
                    }
                    else if (j == 3)
                    {
                        //Added dynamic delete button for every task.
                        CheckBox chkDelete = new CheckBox();
                        chkDelete.ID = "chkDelRow_" + i + "Col_" + j;
                        cell.Controls.Add(chkDelete);
                    }
                    else // cell 1 for task name & cell 2 for time taken
                    {
                        TextBox tb = new TextBox();
                        tb.TextMode = TextBoxMode.MultiLine;
                        tb.ID = "TextBoxRow_" + i + "Col_" + j;
                        cell.Controls.Add(tb);

                    }
                    row.Cells.Add(cell);
                    // And finally, add the TableRow to the Table
                    Tabletask.Rows.Add(row);
                }
            }
            //Set Previous Data on PostBacks
            SetEditPreviousData(rowsCount, colsCount);
            //Sore the current Rows Count in ViewState
            //rowsCount++;
            ViewState["RowsCount"] = rowsCount;
        }
        //Set previous row data.
        private void SetEditPreviousData(int rowsCount, int colsCount)
        {
            Table Tabletask1 = (Table)EditPlaceHolder.FindControl("Tabletask");

            string taskname = string.Empty;
            string timetaken = string.Empty;
            TaskList = (List<string>)Session["TaskListCollection"];

            if (Tabletask1 != null)
            {
                for (int i = 0; i < rowsCount; i++)
                {
                    string[] task = null;
                    //Exeception will throw for newly added rows, since no previous data is available to set. so will handle it in try and put the null in catch
                    try
                    {
                        task = TaskList[i].Split('|');
                    }
                    catch
                    {
                        task = null;
                    }
                    if (task != null)
                    {
                        for (int j = 0; j < colsCount; j++)
                        {
                            if (j == 0)
                            {
                                HiddenField hfTaskIdEdit = (HiddenField)panelEdit.FindControl("hfTaskIdEditRow_" + i + "Col_" + j);
                                if (hfTaskIdEdit != null)
                                {
                                    try
                                    {
                                        hfTaskIdEdit.Value = !string.IsNullOrEmpty(task[j].ToString()) ? task[j].ToString() : string.Empty;
                                    }
                                    catch
                                    {
                                        hfTaskIdEdit.Value = string.Empty;
                                    }
                                }
                            }
                            else
                            {
                                TextBox tb = (TextBox)panelEdit.FindControl("TextBoxRow_" + i + "Col_" + j);
                                tb.TextMode = TextBoxMode.MultiLine;
                                if (tb != null)
                                {
                                    try
                                    {
                                        tb.Text = !string.IsNullOrEmpty(task[j].ToString()) ? task[j].ToString() : string.Empty;
                                    }
                                    catch
                                    {
                                        tb.Text = string.Empty;
                                    }
                                }
                            }

                        }
                    }
                }
            }

        }
        //For editing section and task add new task row.
        protected void btnAddNewRow2_Click(object sender, EventArgs e)
        {
            if (ViewState["RowsCount"] != null)
            {
                numOfRows = Convert.ToInt32(ViewState["RowsCount"].ToString()) + 1;
            }

            EditGenerateTable(numOfRows);

            mpe2.Show();
        }
        //Set previous row data for new section
        private void SetPreviousData(int rowsCount, int colsCount)
        {
            Table Tabletask1 = (Table)plcholder.FindControl("Tabletask");

            string taskname = string.Empty;
            string timetaken = string.Empty;
            TaskList = (List<string>)Session["TaskListCollection"];

            if (Tabletask1 != null)
            {
                for (int i = 0; i < rowsCount; i++)
                {
                    string[] task = null;
                    if (TaskList != null && TaskList.Count > i)
                        task = TaskList[i].Split('|');

                    for (int j = 0; j < colsCount; j++)
                    {
                        TextBox tb = (TextBox)panelAddNew.FindControl("TextBoxRow_" + i + "Col_" + j);
                        tb.TextMode = TextBoxMode.MultiLine;
                        try
                        {
                            if (task != null)
                                tb.Text = !string.IsNullOrEmpty(task[j].ToString()) ? task[j].ToString() : string.Empty;
                        }
                        catch
                        {
                            if (tb != null)
                            {
                                tb.Text = string.Empty;
                                tb.TextMode = TextBoxMode.MultiLine;
                            }
                        }

                    }

                }
            }

        }
        //Delete setion panel
        protected void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (sender as Button);
                RepeaterItem item = button.NamingContainer as RepeaterItem;
                HiddenField hf = (HiddenField)item.FindControl("hfSectionId");
                HiddenField hfTaskId = (HiddenField)item.FindControl("hfTaskId");

                int SectionId = int.Parse(hf.Value);
                //int TaskId = int.Parse(hfTaskId.Value);

                string sqlQry = "select Id, SecName,SecType,TotalTestCases from Section sec where Id =" + SectionId + " and AppId = " + ddlApplication.SelectedValue;

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
        //Delete Task and sections based on the selected Id.
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //Section Hidden field Id.
                int sectionId = int.Parse(lblId.Text);
                HiddenField hfTaskId = new HiddenField();
                int taskId = int.Parse(hfTaskId.Value);

                //if (con.State != ConnectionState.Open)
                //    con.Open();
                //cmd = new SqlCommand();
                ////cmd.CommandText = "prcDeleteAppSection";
                //cmd = new SqlCommand("delete from Section where Id = @Sec_ID", con);
                //cmd.Connection = con;
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ID ", Convert.ToInt16(ViewState["tempId"].ToString()));
                //cmd.ExecuteNonQuery();
                //mpe3.Hide();
                //con.Close();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SFT_AutomationConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("SP_DeleteSectionandrelatedtask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sec_ID", sectionId);
                cmd.Parameters.AddWithValue("@Task_ID", taskId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                lblStatus3.Text = "Record not deleted";
                mpe3.Show();
            }
            finally
            {
                RepeaterBind();
                cmd.Dispose();
                con.Close();
            }
        }
        //Cancel Section, Task records
        protected void Cancel1_Click(object sender, EventArgs e)
        {
            ViewState["RowsCount"] = 1;
            ViewState["I_am_panel"] = 0;
            End_Block(panelAddNew);
        }
        //Cancel edit section,Task
        protected void Cancel2_Click(object sender, EventArgs e)
        {
            ViewState["RowsCount"] = 1;
            ViewState["I_am_panel"] = 0;
            End_Block(panelEdit);
        }
        //Cancel new appliccation section task
        protected void AppSectionTaskCancel_Click(object sender, EventArgs e)
        {
            ViewState["RowsCount"] = 1;
            ViewState["I_am_panel"] = 0;
            End_Block(panelAddNewApplication);
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //need to handle null exception on the dropdown chnage when section panel is not present.

            try
            {
                List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("TextBoxRow_")).ToList();
                List<string> taskIdKeys = Request.Form.AllKeys.Where(key => key.Contains("hfTaskIdEdit")).ToList();

                TaskList.Clear();
                //Check if taskIdKeys is null for inserding new section & task.
                int j = 0;
                if (taskIdKeys.Count == 0)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (Request.Form[keys[i].ToString()] != "")
                        {
                            TaskList.Add(Request.Form[keys[j].ToString()] + "|" + Request.Form[keys[j + 1].ToString()]);
                            i++;
                        }
                        j += 2;
                    }
                }
                else
                {
                    //int j = 0;
                    for (int i = 0; i < taskIdKeys.Count; i++)
                    {
                        if (Request.Form[keys[i].ToString()] != "")
                        {
                            string taskId = !string.IsNullOrEmpty(Request.Form[taskIdKeys[i].ToString()]) ? Request.Form[taskIdKeys[i].ToString()] : "0";

                            // Checking check box is selected or not, if selected then only below qry will return chkbox on that i'th Row. else it will be null.
                            List<string> chkBoxDel = Request.Form.AllKeys.Where(key => key.Contains("chkDelRow_" + i)).ToList();
                            if (chkBoxDel.Count > 0)
                            {
                                // appending "1" for set delete true
                                TaskList.Add(taskId + "|" + Request.Form[keys[j].ToString()] + "|" + Request.Form[keys[j + 1].ToString()] + "|1");
                            }
                            else
                            {
                                // appending "0" for set delete false, means un checked.
                                TaskList.Add(taskId + "|" + Request.Form[keys[j].ToString()] + "|" + Request.Form[keys[j + 1].ToString()] + "|0");
                            }

                            j += 2;
                        }
                    }
                }

                Session["TaskListCollection"] = TaskList;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}