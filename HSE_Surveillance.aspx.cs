using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2021
{
    public partial class L_T_EmpLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = "SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL AND IsSubmittedToObserver='false'";
                getANo(s);
                getSiteNameDrp();
                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    string a = "SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL";
                    getANo(a);
                    FrmSubmit.Text = "BACK";
                    string id = Request.QueryString["id"];
                    GetDataFilled(id);
                }
                else if (Request.QueryString["EditID"] != null && Request.QueryString["EditID"] != string.Empty)
                {
                    string a = "SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL";
                    getANo(a);
                    
                    getSiteNameDrp();
                    FrmSubmit.Text = "UPDATE";
                    string id = Request.QueryString["EditID"];
                    GetDataFilled(id);
                }
                else
                {
                    datepicker.Value = DateTime.Now.ToString("MM/dd/yyyy");
                    string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
                    SqlConnection cnn;
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();
                    string sql = "SELECT Id,Group_no,Question FROM dbo.mst_HSEQuest_tbl";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dataReader);
                    string json = JsonConvert.SerializeObject(dt);
                    hdnjson.Value = json;
                    sitename.Items[0].Attributes["selected"] = "selected";
                    cnn.Close();
                }
            }
        }
        protected void GetDataFilled(string id)
        {
            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection scnn1;
            scnn1 = new SqlConnection(connetionString1);
            scnn1.Open();
            SqlCommand sql = new SqlCommand("SELECT Audit_ID FROM dbo.AuditTable WHERE id=" + id, scnn1);
            string aNo = (string)sql.ExecuteScalar();
            CheckDrp(aNo, AuditNo);
            string sql1 = "SELECT * FROM dbo.HSE_details_tbl WHERE FK_Audit_ID =" + id;
            SqlCommand cmd1 = new SqlCommand(sql1, scnn1);
            SqlDataReader dataReader1 = cmd1.ExecuteReader();
            if (!dataReader1.HasRows)
            {
                string title = "No Data Available";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            while (dataReader1.Read())
            {
                ic.Value = dataReader1.GetValue(1).ToString();
                string sitename1 = dataReader1.GetValue(2).ToString();
                CheckDrp(sitename1, sitename);
                string aDate = dataReader1.GetValue(3).ToString();
                DateTime dt = Convert.ToDateTime(aDate);
                datepicker.Value = dt.ToString("dd/MM/yyyy");
            }
            dataReader1.Close();
            SqlCommand s = new SqlCommand("SELECT Id FROM dbo.HSE_details_tbl WHERE FK_Audit_ID=" + id, scnn1);
            int i = (int)s.ExecuteScalar();
            SqlCommand sqlcommand = new SqlCommand("HSE_getData", scnn1);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter("@id", i);
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.Int16;
            sqlcommand.Parameters.Add(parameter);
            SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            string data = JsonConvert.SerializeObject(dataTable);
            hdnjson.Value = data;
            scnn1.Close();
        }
        public void CheckDrp(string s, DropDownList drp)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (drp.Items.Count > 0)
                {
                    drp.Items.FindByText(s).Selected = true;
                }
            }
            foreach (ListItem li in drp.Items)
            {
                if (li.Selected == false)
                {
                    li.Attributes["disabled"] = "disabled";
                }
            }
        }
        protected void getANo(string sql)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn1;
            cnn1 = new SqlConnection(connetionString);
            cnn1.Open();
            SqlCommand command = new SqlCommand(sql, cnn1);
            AuditNo.DataSource = command.ExecuteReader();
            AuditNo.DataTextField = "Audit_ID";
            AuditNo.DataValueField = "id";
            AuditNo.DataBind();
            AuditNo.Items.Insert(0, new ListItem("Select Here", "0"));
            AuditNo.Items[0].Attributes["selected"] = "selected";
            AuditNo.Items[0].Attributes["disabled"] = "disabled";
            cnn1.Close();
        }
        protected void getSiteNameDrp()
        {
            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            //Populate items in SiteName DrpList
            SqlConnection cnn3;
            cnn3 = new SqlConnection(connetionString1);
            cnn3.Open();
            SqlCommand command = new SqlCommand("SELECT Id,Project_name FROM dbo.mstProjectMaster", cnn3);
            sitename.DataSource = command.ExecuteReader();
            sitename.DataTextField = "Project_name";
            sitename.DataValueField = "Id";
            sitename.DataBind();
            sitename.Items.Insert(0, new ListItem("Select Here", "0"));
            sitename.Items[0].Attributes["disabled"] = "disabled";
            cnn3.Close();
        }
        protected void FrmSubmit_Click(object sender, EventArgs e)
        {
            string getMode = FrmSubmit.Text;
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            DataTable deserializedObj = JsonConvert.DeserializeObject<DataTable>(hdnfld1.Value);
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string AID = AuditNo.Items[AuditNo.SelectedIndex].Value;
            
            string selected_Sitename = sitename.Items[sitename.SelectedIndex].Text;
            if (getMode == "Submit")
            {
                SqlCommand cmd = new SqlCommand("sp_HSE_insert", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ic", SqlDbType.VarChar).Value = ic.Value;
                cmd.Parameters.AddWithValue("@site", SqlDbType.VarChar).Value = selected_Sitename;
                cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = datepicker.Value;
                cmd.Parameters.AddWithValue("@FKAID", SqlDbType.VarChar).Value = AID;
                cmd.Parameters.AddWithValue("@HSE_Table", SqlDbType.Text).Value = deserializedObj;
                cmd.Parameters.AddWithValue("@CBY", SqlDbType.VarChar).Value = "Dhvani";
                cmd.Parameters.AddWithValue("@CON", SqlDbType.Date).Value = DateTime.Now;
                cmd.ExecuteNonQuery();
                cnn.Close();
                string title = "Successfully Submitted!";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            else if(getMode== "UPDATE")
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Id FROM dbo.HSE_details_tbl WHERE FK_Audit_ID=" + AID, cnn);
                int HSEID = (int)sqlCommand.ExecuteScalar();
                SqlCommand cmd = new SqlCommand("sp_HSE_update", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HSEId", SqlDbType.Int).Value = HSEID;
                cmd.Parameters.AddWithValue("@ic", SqlDbType.VarChar).Value = ic.Value;
                cmd.Parameters.AddWithValue("@site", SqlDbType.VarChar).Value = selected_Sitename;
                DateTime date = Convert.ToDateTime(datepicker.Value);
                cmd.Parameters.AddWithValue("@date", SqlDbType.Date).Value = date;
                cmd.Parameters.AddWithValue("@MBY", SqlDbType.VarChar).Value = "Dhvani";
                cmd.Parameters.AddWithValue("@MON", SqlDbType.Date).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@HSE_Table", SqlDbType.Text).Value = deserializedObj;
                string gridchanges = hdn_gridChanges.Value;
                if (gridchanges == "true")
                {
                    cmd.Parameters.AddWithValue("@GridData", SqlDbType.VarChar).Value = "True";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GridData", SqlDbType.VarChar).Value = "False";
                }
                cmd.ExecuteNonQuery();
                cnn.Close();
                string title = "Successfully Updated!";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            else
            {
                string redirectScript = " window.location.href = 'AuditDetails.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
        }

        protected void modalButt_Click(object sender, EventArgs e)
        {
            if(Request.QueryString["EditID"] != null && Request.QueryString["EditID"] != string.Empty)
            {
                string redirectScript = " window.location.href = 'Audit.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
            else
            {
                string redirectScript = " window.location.href = 'NCReport.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
        }

        protected void AuditNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuditNo.Items[0].Attributes["disabled"] = "disabled";
            string message = AuditNo.SelectedItem.Text;
            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            getSiteNameDrp();
            SqlConnection scnn1;
            scnn1 = new SqlConnection(connetionString1);
            scnn1.Open();
            string sql1 = "SELECT siteName FROM dbo.AuditTable WHERE Audit_ID='" + message + "'";
            SqlCommand cmd1 = new SqlCommand(sql1, scnn1);
            string sitename1 = (string)cmd1.ExecuteScalar();
            if (sitename1 != null)
            {
                if (!string.IsNullOrEmpty(sitename1))
                {
                    if (sitename.Items.Count > 0)
                    {
                        sitename.Items.FindByText(sitename1).Selected = true;
                    }
                }
            }
            foreach (ListItem li in sitename.Items)
            {
                if (li.Selected == false)
                {
                    li.Attributes["disabled"] = "disabled";
                }
            }
            scnn1.Close();
        }
    }
}