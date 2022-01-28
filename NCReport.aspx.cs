using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2021
{
    public partial class NCReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getSiteNameDrp();
                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    string sqlq = "SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL";
                    getAuditDrp(sqlq);
                    FrmSubmit.Text = "BACK";
                    string id = Request.QueryString["id"];
                    GetDataFilled(id);
                }
                else if(Request.QueryString["EditID"] != null && Request.QueryString["EditID"] != string.Empty)
                {
                    string sqlq = "SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL";
                    getAuditDrp(sqlq);
                    getSiteNameDrp();
                    FrmSubmit.Text = "UPDATE";
                    string id = Request.QueryString["EditID"];
                    GetDataFilled(id);
                    //auditor.Disabled = true;
                    Date.Disabled = true; Date.Value = DateTime.Now.ToString("MM/dd/yyyy");
                    //RequiredFieldValidator20.Enabled = false;
                    //RequiredFieldValidator21.Enabled = false;
                }
                else if (Request.QueryString["AuditeeID"] != null && Request.QueryString["AuditeeID"] != string.Empty)
                {
                    string sqlq = "SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL";
                    getAuditDrp(sqlq);
                    getSiteNameDrp();
                    string id = Request.QueryString["AuditeeID"];
                    GetDataFilled(id);
                    RequiredFieldValidator19.Enabled = false;
                    RequiredFieldValidator20.Enabled = false;
                    RequiredFieldValidator21.Enabled = false;
                    department.Disabled = true;auditee.Disabled = true;ANCNo.Disabled = true;process.Disabled = true;
                    auditorName.Disabled = true;aperiod.Disabled = true;clause.Disabled = true;req.Disabled = true;
                    ObservedNC.Disabled = true;evidence.Disabled = true;proposedDate.Disabled = true;
                    comments.Disabled = true; CAANo.Enabled = false; CAAYes.Enabled = false; 
                    NCDNo.Enabled = false; NCDYes.Enabled = false;
                    MajorNo.Enabled = false;MajorYes.Enabled = false;MinorNo.Enabled = false;MinorYes.Enabled = false;
                    auditor.Disabled = true;
                    Date.Disabled = true;
                    Date.Value = DateTime.Now.ToString("MM/dd/yyyy");
                }
                else if(Request.QueryString["FillDetails"] != null && Request.QueryString["FillDetails"] != string.Empty)
                {
                    string sqlq = "SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL";
                    getAuditDrp(sqlq);
                    getSiteNameDrp();
                    string id = Request.QueryString["FillDetails"];
                    GetDataFilled(id);
                    department.Disabled = true; auditee.Disabled = true; ANCNo.Disabled = true; process.Disabled = true;
                    auditorName.Disabled = true; aperiod.Disabled = true; clause.Disabled = true; req.Disabled = true;
                    ObservedNC.Disabled = true; evidence.Disabled = true; proposedDate.Disabled = true; actualDate.Disabled = true;
                    auditor.Disabled = true; resPerson.Disabled = true;
                    correction.Disabled = true; MajorNo.Enabled = false; MajorYes.Enabled = false; MinorNo.Enabled = false; MinorYes.Enabled = false;
                    rootCause.Disabled = true;
                    Actionresponse.Disabled = true;
                    EviNC.Disabled = true;
                    Date.Disabled = true; RequiredFieldValidator20.Enabled = false;
                    Date.Value = DateTime.Now.ToString("MM/dd/yyyy");
                }
                else
                {
                    string sql = "SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL AND IsSubmittedToObserver='false'";
                    getAuditDrp(sql);
                    actualDate.Disabled = true;
                    RequiredFieldValidator13.Enabled = false;
                    RequiredFieldValidator14.Enabled = false;
                    RequiredFieldValidator15.Enabled = false;
                    RequiredFieldValidator16.Enabled = false;
                    RequiredFieldValidator17.Enabled = false;
                    RequiredFieldValidator18.Enabled = false;
                    RequiredFieldValidator19.Enabled = false;
                    RequiredFieldValidator20.Enabled = false;
                    RequiredFieldValidator21.Enabled = false;
                    resPerson.Disabled = true;
                    correction.Disabled = true;
                    rootCause.Disabled = true;
                    Actionresponse.Disabled = true;
                    EviNC.Disabled = true;
                    comments.Disabled = true;
                    auditor.Disabled = true;
                    Date.Disabled = true;CAANo.Enabled = false;CAAYes.Enabled = false;NCDNo.Enabled = false;NCDYes.Enabled = false;
                    Date.Value = DateTime.Now.ToString("MM/dd/yyyy");
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
            string sql1 = "SELECT * FROM dbo.NCReport_details WHERE FK_Audit_ID =" + id;
            SqlCommand cmd1 = new SqlCommand(sql1, scnn1);
            SqlDataReader dataReader1 = cmd1.ExecuteReader();
            if (!dataReader1.HasRows)
            {
                string title = "No Data Available";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            while (dataReader1.Read())
            {
                string sitename1 = dataReader1.GetValue(1).ToString();
                CheckDrp(sitename1, sitename);
                ObsTeam();
                department.Value = dataReader1.GetValue(2).ToString();
                auditee.Value = dataReader1.GetValue(3).ToString();
                ANCNo.Value = dataReader1.GetValue(4).ToString();
                process.Value = dataReader1.GetValue(5).ToString();
                auditorName.Value = dataReader1.GetValue(6).ToString();
                string auditperiod = dataReader1.GetValue(7).ToString();
                DateTime dt = Convert.ToDateTime(auditperiod);
                aperiod.Value = dt.ToString("dd/MM/yyyy");
                clause.Value = dataReader1.GetValue(8).ToString();
                string observerTeam = dataReader1.GetValue(9).ToString();
                if (observerTeam != null)
                {
                    string[] o_team = observerTeam.Split(';');
                    for (int i = 0; i < o_team.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(o_team[i]))
                        {
                            if (lstObserverTeam.Items.Count > 0)
                            {
                                lstObserverTeam.Items.FindByText(o_team[i]).Selected = true;
                            }
                        }
                    }
                }
                string Major = dataReader1.GetValue(10).ToString();
                string Minor = dataReader1.GetValue(11).ToString();
                if (Major == "True") { MajorYes.Checked = true; } else { MajorNo.Checked = true; }
                if (Minor == "True") { MinorYes.Checked = true; } else { MinorNo.Checked = true; }
                req.Value = dataReader1.GetValue(12).ToString();
                ObservedNC.Value = dataReader1.GetValue(13).ToString();
                evidence.Value = dataReader1.GetValue(14).ToString();
                string proDate = dataReader1.GetValue(15).ToString();
                string actDate = dataReader1.GetValue(16).ToString();
                DateTime dt1 = Convert.ToDateTime(proDate);
                //DateTime dt2 = Convert.ToDateTime(actDate);
                proposedDate.Value = dt1.ToString("dd/MM/yyyy");
                actualDate.Value = actDate;
                resPerson.Value = dataReader1.GetValue(17).ToString();
                correction.Value = dataReader1.GetValue(18).ToString();
                rootCause.Value = dataReader1.GetValue(19).ToString();
                Actionresponse.Value = dataReader1.GetValue(20).ToString();
                EviNC.Value = dataReader1.GetValue(21).ToString();
                string caa = dataReader1.GetValue(22).ToString();
                string ncd = dataReader1.GetValue(23).ToString();
                if (caa == "True") { CAAYes.Checked = true; } else { CAANo.Checked = true; }
                if (ncd == "True") { NCDYes.Checked = true; } else { NCDNo.Checked = true; }
                comments.Value = dataReader1.GetValue(24).ToString();
                auditor.Value = dataReader1.GetValue(25).ToString();
                string Date11 = dataReader1.GetValue(26).ToString();
                //DateTime dt3 = Convert.ToDateTime(Date11);
                Date.Value = Date11;
            }
            dataReader1.Close();
            scnn1.Close();
        }
        public void CheckDrp(string s,DropDownList drp)
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
        protected void getAuditDrp(string sql)
        {
            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            //Populate items in SiteName DrpList
            SqlConnection cnn3;
            cnn3 = new SqlConnection(connetionString1);
            cnn3.Open();
            SqlCommand command = new SqlCommand(sql, cnn3);
            AuditNo.DataSource = command.ExecuteReader();
            AuditNo.DataTextField = "Audit_ID";
            AuditNo.DataValueField = "id";
            AuditNo.DataBind();
            AuditNo.Items.Insert(0, new ListItem("Select Here", "0"));
            AuditNo.Items[0].Attributes["selected"] = "selected";
            AuditNo.Items[0].Attributes["disabled"] = "disabled";
            cnn3.Close();
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
        protected void ObsTeam()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DrpSumo", "drp();", true);
            int pid = int.Parse(sitename.SelectedItem.Value);
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn2;
            cnn2 = new SqlConnection(connetionString);
            cnn2.Open();
            SqlCommand sqlCommand1 = new SqlCommand("getAuditObserverTeam", cnn2);
            sqlCommand1.CommandType = CommandType.StoredProcedure;
            SqlParameter sparameter1 = new SqlParameter("@projectid", pid);
            sparameter1.Direction = ParameterDirection.Input;
            sparameter1.DbType = DbType.Int16;
            SqlParameter parameter11 = new SqlParameter("@team", "observer");
            parameter11.Direction = ParameterDirection.Input;
            parameter11.DbType = DbType.String;
            sqlCommand1.Parameters.Add(sparameter1);
            sqlCommand1.Parameters.Add(parameter11);
            string getOT = (string)sqlCommand1.ExecuteScalar();
            DataTable dtmain1 = new DataTable();
            dtmain1.Columns.Add("ID");
            dtmain1.Columns.Add("Name");
            string[] str_ot = getOT.Split(';');
            foreach (string str in str_ot)
            {
                SqlCommand sql = new SqlCommand("SELECT Name,ID FROM dbo.mstObserverTeam WHERE ID=" + str, cnn2);
                sql.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = sql;
                ad.Fill(dt);
                if (dt?.Rows?.Count > 0)
                {
                    DataRow dr = dtmain1.NewRow();
                    dr["ID"] = dt.Rows[0]["ID"].ToString();
                    dr["Name"] = dt.Rows[0]["Name"].ToString();
                    dtmain1.Rows.Add(dr);
                }
            }
            lstObserverTeam.DataSource = dtmain1;
            lstObserverTeam.DataTextField = "Name";
            lstObserverTeam.DataValueField = "ID";
            lstObserverTeam.DataBind();
            cnn2.Close();
        }
        protected void sitename_SelectedIndexChanged(object sender, EventArgs e)
        {
            sitename.Items[0].Attributes["disabled"] = "disabled";
            AuditNo.Items[0].Attributes["disabled"] = "disabled";
            string message = sitename.Items[sitename.SelectedIndex].Value;
            if (message == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Select Site Name!!');", true);
            }
            ObsTeam();
        }
        protected void FrmSubmit_Click(object sender, EventArgs e)
        {
            bool major, minor, caa, ncd;
            string selected_Sitename = sitename.Items[sitename.SelectedIndex].Text;
            if (MajorYes.Checked == true) { major = true; }
            else { major = false; }
            if (MinorYes.Checked == true) { minor = true; }
            else { minor = false; }
            if (CAAYes.Checked == true) { caa = true; }
            else { caa = false; }
            if (NCDYes.Checked == true) { ncd = true; }
            else { ncd = false; }
            string selected_Oteam = string.Empty;
            foreach (ListItem li in lstObserverTeam.Items)
            {
                if (li.Selected == true)
                {
                    selected_Oteam += li.Text + ";";
                }
            }
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            if(Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
            {
                string redirectScript = " window.location.href = 'AuditDetails.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
            else if(Request.QueryString["EditID"] != null && Request.QueryString["EditID"] != string.Empty)
            {
                SqlCommand cmd = new SqlCommand("sp_NCReport_update", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", SqlDbType.VarChar).Value = "UPDATE";
                cmd.Parameters.AddWithValue("@sitename", SqlDbType.VarChar).Value = selected_Sitename;
                cmd.Parameters.AddWithValue("@department", SqlDbType.VarChar).Value = department.Value;
                cmd.Parameters.AddWithValue("@auditee", SqlDbType.VarChar).Value = auditee.Value;
                cmd.Parameters.AddWithValue("@ACNo", SqlDbType.VarChar).Value = ANCNo.Value;
                cmd.Parameters.AddWithValue("@process", SqlDbType.VarChar).Value = process.Value;
                cmd.Parameters.AddWithValue("@auditorName", SqlDbType.VarChar).Value = auditorName.Value;
                DateTime date = Convert.ToDateTime(aperiod.Value);
                cmd.Parameters.AddWithValue("@aperiod", SqlDbType.Date).Value = date;
                cmd.Parameters.AddWithValue("@clause", SqlDbType.VarChar).Value = clause.Value;
                cmd.Parameters.AddWithValue("@ObserverTeam", SqlDbType.VarChar).Value = selected_Oteam;
                cmd.Parameters.AddWithValue("@Major", SqlDbType.Bit).Value = major;
                cmd.Parameters.AddWithValue("@Minor", SqlDbType.Bit).Value = minor;
                cmd.Parameters.AddWithValue("@req", SqlDbType.VarChar).Value = req.Value;
                cmd.Parameters.AddWithValue("@ObservedNC", SqlDbType.VarChar).Value = ObservedNC.Value;
                cmd.Parameters.AddWithValue("@evidence", SqlDbType.VarChar).Value = evidence.Value;
                DateTime date1 = Convert.ToDateTime(proposedDate.Value);
                DateTime date2 = Convert.ToDateTime(actualDate.Value);
                cmd.Parameters.AddWithValue("@proposedDate", SqlDbType.Date).Value = date1;
                cmd.Parameters.AddWithValue("@actualDate", SqlDbType.Date).Value = date2;
                cmd.Parameters.AddWithValue("@resPerson", SqlDbType.VarChar).Value = resPerson.Value;
                cmd.Parameters.AddWithValue("@correction", SqlDbType.VarChar).Value = correction.Value;
                cmd.Parameters.AddWithValue("@rootCause", SqlDbType.VarChar).Value = rootCause.Value;
                cmd.Parameters.AddWithValue("@Actionresponse", SqlDbType.VarChar).Value = Actionresponse.Value;
                cmd.Parameters.AddWithValue("@EviNC", SqlDbType.VarChar).Value = EviNC.Value;
                cmd.Parameters.AddWithValue("@CAA", SqlDbType.Bit).Value = caa;
                cmd.Parameters.AddWithValue("@NCD", SqlDbType.Bit).Value = ncd;
                cmd.Parameters.AddWithValue("@comments", SqlDbType.VarChar).Value = comments.Value;
                cmd.Parameters.AddWithValue("@auditor", SqlDbType.VarChar).Value = auditor.Value;
                //DateTime date3 = Convert.ToDateTime(Date.Value);
                cmd.Parameters.AddWithValue("@Date", SqlDbType.Date).Value = Date.Value;
                cmd.Parameters.AddWithValue("@MBY", SqlDbType.VarChar).Value = "Dhvani";
                cmd.Parameters.AddWithValue("@MON", SqlDbType.Date).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@AID", SqlDbType.Int).Value = AuditNo.SelectedItem.Value;
                cmd.ExecuteNonQuery();
                cnn.Close();
                string title = "Successfully Updated!";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            else if(Request.QueryString["AuditeeID"] != null && Request.QueryString["AuditeeID"] != string.Empty)
            {
                SqlCommand cmd = new SqlCommand("sp_NCReport_update", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", SqlDbType.VarChar).Value = "AuditeeData";
                //DateTime date2 = Convert.ToDateTime(actualDate.Value);
                cmd.Parameters.AddWithValue("@actualDate", SqlDbType.Date).Value = actualDate.Value;
                cmd.Parameters.AddWithValue("@resPerson", SqlDbType.VarChar).Value = resPerson.Value;
                cmd.Parameters.AddWithValue("@correction", SqlDbType.VarChar).Value = correction.Value;
                cmd.Parameters.AddWithValue("@rootCause", SqlDbType.VarChar).Value = rootCause.Value;
                cmd.Parameters.AddWithValue("@Actionresponse", SqlDbType.VarChar).Value = Actionresponse.Value;
                cmd.Parameters.AddWithValue("@EviNC", SqlDbType.VarChar).Value = EviNC.Value;
                cmd.Parameters.AddWithValue("@MBY", SqlDbType.VarChar).Value = "Dhvani";
                cmd.Parameters.AddWithValue("@MON", SqlDbType.Date).Value = DateTime.Now;
                string auditID = Request.QueryString["AuditeeID"];
                cmd.Parameters.AddWithValue("@AID", SqlDbType.Int).Value = auditID;
                cmd.ExecuteNonQuery();
                SqlCommand sqlCommand = new SqlCommand("SELECT ObserverTeam FROM dbo.Form4Details WHERE FK_AuditID=" + Request.QueryString["AuditeeID"], cnn);
                string observerTeam = (string)sqlCommand.ExecuteScalar();
                string getObserverMAilId = string.Empty;
                string[] sel_Observer_team = observerTeam.Split(';');
                for (int i = 0; i < sel_Observer_team.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(sel_Observer_team[i]))
                    {
                        SqlConnection connection = new SqlConnection(connetionString);
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT Mail_id FROM mstObserverTeam WHERE Name='" + sel_Observer_team[i] + "'", connection);
                        SqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            getObserverMAilId += dataReader.GetString(0).ToString() + ",";
                        }
                        connection.Close();
                    }
                }
                string auditNo = Request.QueryString["AuditeeID"];
                string body = "<a href='https://localhost:44367/NCReport" + "?FillDetails=" + auditNo + "'>Click here </a>to fill the Clearance Report Section of NC Report.";
                sendEmail(body, getObserverMAilId);
                string title = "Successfully Sent to Auditor!";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
                //cnn.Close();
            }
            else if (Request.QueryString["FillDetails"] != null && Request.QueryString["FillDetails"] != string.Empty)
            {
                SqlCommand cmd = new SqlCommand("sp_NCReport_update", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", SqlDbType.VarChar).Value = "AuditorData";
                cmd.Parameters.AddWithValue("@CAA", SqlDbType.Bit).Value = caa;
                cmd.Parameters.AddWithValue("@NCD", SqlDbType.Bit).Value = ncd;
                cmd.Parameters.AddWithValue("@comments", SqlDbType.VarChar).Value = comments.Value;
                cmd.Parameters.AddWithValue("@MBY", SqlDbType.VarChar).Value = "Dhvani";
                cmd.Parameters.AddWithValue("@MON", SqlDbType.Date).Value = DateTime.Now;
                string auditID = Request.QueryString["FillDetails"];
                cmd.Parameters.AddWithValue("@AID", SqlDbType.Int).Value = auditID;
                cmd.ExecuteNonQuery();
                string auditNo = Request.QueryString["FillDetails"];
                string body = "<p>Click the link given below to review the Audit Deatils and to Approve or Reject it.</p><br/><a href='https://localhost:44367/AuditDetails" + "?ApprovalID=" + auditNo + "'>Click here </a> ";
                sendEmail(body, "dhvanigolani2000@gmail.com");
                string title = "Done!";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("sp_NCReport_insert", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sitename", SqlDbType.VarChar).Value = selected_Sitename;
                cmd.Parameters.AddWithValue("@department", SqlDbType.VarChar).Value = department.Value;
                cmd.Parameters.AddWithValue("@auditee", SqlDbType.VarChar).Value = auditee.Value;
                cmd.Parameters.AddWithValue("@ACNo", SqlDbType.VarChar).Value = ANCNo.Value;
                cmd.Parameters.AddWithValue("@process", SqlDbType.VarChar).Value = process.Value;
                cmd.Parameters.AddWithValue("@auditorName", SqlDbType.VarChar).Value = auditorName.Value;
                cmd.Parameters.AddWithValue("@aperiod", SqlDbType.Date).Value = aperiod.Value;
                cmd.Parameters.AddWithValue("@clause", SqlDbType.VarChar).Value = clause.Value;
                cmd.Parameters.AddWithValue("@ObserverTeam", SqlDbType.VarChar).Value = selected_Oteam;
                cmd.Parameters.AddWithValue("@Major", SqlDbType.Bit).Value = major;
                cmd.Parameters.AddWithValue("@Minor", SqlDbType.Bit).Value = minor;
                cmd.Parameters.AddWithValue("@req", SqlDbType.VarChar).Value = req.Value;
                cmd.Parameters.AddWithValue("@ObservedNC", SqlDbType.VarChar).Value = ObservedNC.Value;
                cmd.Parameters.AddWithValue("@evidence", SqlDbType.VarChar).Value = evidence.Value;
                cmd.Parameters.AddWithValue("@proposedDate", SqlDbType.Date).Value = proposedDate.Value;
                cmd.Parameters.AddWithValue("@FKAuditID", SqlDbType.VarChar).Value = AuditNo.Items[AuditNo.SelectedIndex].Value;
                cmd.Parameters.AddWithValue("@CBY", SqlDbType.VarChar).Value = "Dhvani";
                cmd.Parameters.AddWithValue("@CON", SqlDbType.Date).Value = DateTime.Now;
                cmd.ExecuteNonQuery();
                cnn.Close();
                string title = "Successfully Submitted!";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
        }

        protected void modalButt_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EditID"] != null && Request.QueryString["EditID"] != string.Empty || 
                Request.QueryString["AuditeeID"] != null && Request.QueryString["AuditeeID"] != string.Empty ||
                Request.QueryString["FillDetails"] != null && Request.QueryString["FillDetails"] != string.Empty)
            {
                string redirectScript = " window.location.href = 'Audit.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
            else
            {
                string redirectScript = " window.location.href = 'AuditDetails.aspx';";
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
            string sql1 = "SELECT siteName FROM dbo.AuditTable WHERE Audit_ID='"+message+"'";
            SqlCommand cmd1 = new SqlCommand(sql1, scnn1);
            string sitename1 = (string)cmd1.ExecuteScalar();
            CheckDrp(sitename1, sitename);
            scnn1.Close();
            ObsTeam();
        }

        protected void sendEmail(string body, string ToEmail)
        {
            try
            {
                string FromMail = ConfigurationManager.AppSettings["FromMail"].ToString();
                string Pass = ConfigurationManager.AppSettings["Password"].ToString();
                string Host = ConfigurationManager.AppSettings["Host"].ToString();
                MailMessage message = new MailMessage();
                string[] Multi = ToEmail.Split(',');
                for (int i = 0; i < Multi.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(Multi[i]))
                    {
                        message.To.Add(new MailAddress(Multi[i]));
                    }
                }
                //message.To.Add("monicagolani1978@gmail.com");
                //message.To.Add("dhvanigolani2000@gmail.com");
                message.CC.Add(new MailAddress("dhvanigolani2000@gmail.com"));
                
                message.Subject = "Audit Details";
                message.From = new MailAddress(FromMail);// Email-ID of Sender  
                message.IsBodyHtml = true;
                //string body = "<b>Audit Details</b><br/><p>Click the link given below to review the Audit Deatils and then approve or reject it.</p><br/><a href='https://localhost:44367/AuditDetails" + "?id="+id+"' >Click here</a>";
                message.Body = body;
                SmtpClient SmtpMail = new SmtpClient();
                SmtpMail.Host = Host;
                SmtpMail.Port = 587;
                SmtpMail.EnableSsl = true;
                SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpMail.Credentials = new System.Net.NetworkCredential(FromMail, Pass);
                SmtpMail.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}