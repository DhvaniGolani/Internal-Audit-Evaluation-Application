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
    public partial class AuditDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                sitename.Items.Insert(0, new ListItem("Select Here", "0"));
                sitename.Items[0].Attributes["disabled"] = "disabled";
                lstObserverTeam.Items.Insert(0, new ListItem("Select Here", "0"));
                lstObserverTeam.Items[0].Attributes["disabled"] = "disabled";
                
                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    getAuditNoDrp();
                    FrmSubmit.Text = "SEND";
                    AppYes.Visible = true;
                    AppNo.Visible = true;
                    approved.Visible = true;
                    string AID = Request.QueryString["id"];
                    getFilledData(AID);
                }
                else if(Request.QueryString["AuditeeAppID"] != null && Request.QueryString["AuditeeAppID"] != string.Empty)
                {
                    getAuditNoDrp();
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "popover();", true);
                    //AP.Visible = true;
                    //actPlan.Visible = true;
                    FrmSubmit.Visible = false;
                    string AID = Request.QueryString["AuditeeAppID"];
                    getFilledData(AID);
                }
                else if (Request.QueryString["ApprovalID"] != null && Request.QueryString["ApprovalID"] != string.Empty)
                {
                    getAuditNoDrp();
                    AppYes.Visible = true;
                    AppNo.Visible = true;
                    approved.Visible = true;
                    string AID = Request.QueryString["ApprovalID"];
                    getFilledData(AID);
                }
                else
                {
                    string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
                    SqlConnection cnn3;
                    cnn3 = new SqlConnection(connetionString1);
                    cnn3.Open();
                    SqlCommand command = new SqlCommand("SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL AND IsSubmittedToObserver='false'", cnn3);
                    AuditNo.DataSource = command.ExecuteReader();
                    AuditNo.DataTextField = "Audit_ID";
                    AuditNo.DataValueField = "id";
                    AuditNo.DataBind();
                    AuditNo.Items.Insert(0, new ListItem("Select Here", "0"));
                    AuditNo.Items[0].Attributes["disabled"] = "disabled";
                    //AuditNo.Items[0].Attributes["selected"] = "selected";
                    sitename.Items[0].Attributes["selected"] = "selected";
                    lstObserverTeam.Items[0].Attributes["selected"] = "selected";
                    //getSiteNameDrp();
                }
            }
        }
        protected void getAuditNoDrp()
        {
            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            //Populate items in SiteName DrpList
            SqlConnection cnn3;
            cnn3 = new SqlConnection(connetionString1);
            cnn3.Open();
            SqlCommand command = new SqlCommand("SELECT id,Audit_ID FROM dbo.AuditTable WHERE Audit_ID IS NOT NULL AND IsSubmittedToObserver='true'", cnn3);
            AuditNo.DataSource = command.ExecuteReader();
            AuditNo.DataTextField = "Audit_ID";
            AuditNo.DataValueField = "id";
            AuditNo.DataBind();
            AuditNo.Items.Insert(0, new ListItem("Select Here", "0"));
            AuditNo.Items[0].Attributes["disabled"] = "disabled";
            cnn3.Close();
        }
        protected void getFilledData(string id)
        {
            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString1);
            cnn.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM dbo.Form4Details WHERE FK_AuditID=" + id, cnn);
            SqlDataReader dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {
                string ANo = dataReader1.GetValue(2).ToString();
                CheckDrp(ANo, AuditNo);
                ANOChange();
                string observerTeam = dataReader1.GetValue(5).ToString();
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
            }
            cnn.Close();
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
        protected void AuditeeTeam()
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn1;
            cnn1 = new SqlConnection(connetionString);
            //Populate listbox from database
            cnn1.Open();
            SqlCommand sqlCommand = new SqlCommand("getAuditObserverTeam", cnn1);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int pid = int.Parse(sitename.Items[sitename.SelectedIndex].Value);
            SqlParameter sparameter = new SqlParameter("@projectid", pid);
            sparameter.Direction = ParameterDirection.Input;
            sparameter.DbType = DbType.Int16;
            SqlParameter parameter1 = new SqlParameter("@team", "auditee");
            parameter1.Direction = ParameterDirection.Input;
            parameter1.DbType = DbType.String;
            sqlCommand.Parameters.Add(sparameter);
            sqlCommand.Parameters.Add(parameter1);
            string getAT = (string)sqlCommand.ExecuteScalar();
            string[] str_at = getAT.Split(';');
            DataTable dtmain = new DataTable();
            dtmain.Columns.Add("ID");
            dtmain.Columns.Add("Name");

            foreach (string str in str_at)
            {
                sqlCommand = new SqlCommand("SELECT ID,Name FROM dbo.mstAuditee WHERE ID=" + str, cnn1);
                sqlCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = sqlCommand;
                ad.Fill(dt);
                if (dt?.Rows?.Count > 0)
                {
                    DataRow dr = dtmain.NewRow();
                    dr["ID"] = dt.Rows[0]["ID"].ToString();
                    dr["Name"] = dt.Rows[0]["Name"].ToString();
                    dtmain.Rows.Add(dr);
                }
            }
            lstauditee.DataSource = dtmain;
            lstauditee.DataTextField = "Name";
            lstauditee.DataValueField = "ID";
            lstauditee.DataBind();
            cnn1.Close();
        }
        protected void ANOChange()
        {
            string message = AuditNo.SelectedItem.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DrpSumo", "drp();", true);
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
            ObsTeam();
            Link1.Visible = true;
            Link2.Visible = true;
            Link3.Visible = true;
        }
        protected void AuditNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuditNo.Items[0].Attributes["disabled"] = "disabled";
            ANOChange();
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
            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString1);
            cnn.Open();
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
            {
                bool app;
                if (AppYes.Checked == true) 
                { 
                    app = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "DrpSumo", "drp();", true);
                    //AuditeeTeam();
                    lstauditee.Visible = false;
                    string title1 = "Approved!!";
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title1 + "');", true);
                } 
                else { 
                    app = false;
                    string title1 = "Select Auditee Team";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "DrpSumo", "drp();", true);
                    AuditeeTeam();
                    lstauditee.Visible = true;
                    //lstauditee.Visible = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title1 + "');", true);
                }
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Form4Details SET IsApproved='"+app +"' WHERE FK_AuditID="+ Request.QueryString["id"], cnn);
                cmd.ExecuteNonQuery();
            }
            else if (Request.QueryString["ApprovalID"] != null && Request.QueryString["ApprovalID"] != string.Empty)
            {
                bool app; string isaproved;
                if (AppYes.Checked == true)
                {
                    app = true; isaproved = "Approved";
                }
                else
                {
                    app = false; isaproved = "Rejected";
                }
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Form4Details SET isAPApproved='" + app + "' WHERE FK_AuditID=" + Request.QueryString["ApprovalID"], cnn);
                cmd.ExecuteNonQuery();
                string body = "<b>Audit Details</b><br/> <p>Audit Number " + AuditNo.SelectedItem.Text + " has been " + isaproved + " .</p><br/>";
                sendEmail(body, "dhvanigolani2000@gmail.com", " ");
                string title1 = "Submitted Successfully!";
                lstauditee.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title1 + "');", true);
            }
            //else if (Request.QueryString["AuditeeAppID"] != null && Request.QueryString["AuditeeAppID"] != string.Empty)
            //{

                    //    lstauditee.Visible = false;
                    //    SqlCommand cmd = new SqlCommand("UPDATE dbo.Form4Details SET ActPlan='" + actPlan.Value + "' WHERE FK_AuditID=" + Request.QueryString["AuditeeAppID"], cnn);
                    //    cmd.ExecuteNonQuery();
                    //    //Send Mail to CreatedBy of Audit Form
                    //    string auditNo = AuditNo.SelectedItem.Value;
                    //    //SqlConnection sqlConnection = new SqlConnection(connetionString1);
                    //    //sqlConnection.Open();
                    //    //SqlCommand sqlCommand = new SqlCommand("SELECT CreatedBy FROM dbo.AuditTable WHERE id="+auditNo, sqlConnection);
                    //    //string getMail = (string)sqlCommand.ExecuteScalar();
                    //    //sqlConnection.Close();
                    //    string body = "<b>Audit Details</b><br/> <p>Click the link given below to review the Audit Deatils and then approve or reject it.</p><br/><a href='https://localhost:44367/AuditDetails" + "?APApprovalID=" + auditNo + "' >Click here</a>";
                    //    string cc = "";
                    //    sendEmail(body, "dhvanigolani2000@gmail.com",cc);
                    //    string t = "Submitted Successfully!";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + t + "');", true);
                    //}
                    //else if (Request.QueryString["APApprovalID"] != null && Request.QueryString["APApprovalID"] != string.Empty)
                    //{
                    //    lstauditee.Visible = false;
                    //    bool app;string isaproved;
                    //    if (AppYes.Checked == true)
                    //    {
                    //        app = true; isaproved = "Approved";
                    //    }
                    //    else
                    //    {
                    //        app = false; isaproved = "Rejected";
                    //    }
                    //    SqlCommand cmd = new SqlCommand("UPDATE dbo.Form4Details SET isAPApproved='"+app+"' WHERE FK_AuditID=" + Request.QueryString["APApprovalID"], cnn);
                    //    cmd.ExecuteNonQuery();
                    //    string body = "<b>Audit Details</b><br/> <p>Audit Number "+AuditNo.SelectedItem.Text+" has been "+isaproved+" .</p><br/>";
                    //    string cc = "";
                    //    sendEmail(body, "dhvanigolani2000@gmail.com",cc);
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Submitted Successfully!!');", true);
                    //}
            else
            {
                string selected_Oteam = string.Empty;
                foreach (ListItem li in lstObserverTeam.Items)
                {
                    if (li.Selected == true)
                    {
                        selected_Oteam += li.Text + ";";
                    }
                }
                SqlCommand cmd = new SqlCommand("sp_insertForm4Details", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FK_AID", SqlDbType.Int).Value = AuditNo.SelectedItem.Value;
                cmd.Parameters.AddWithValue("@ANO", SqlDbType.VarChar).Value = AuditNo.SelectedItem.Text;
                cmd.Parameters.AddWithValue("@isSentToObs", SqlDbType.Bit).Value = true;
                cmd.Parameters.AddWithValue("@isApproved", SqlDbType.Bit).Value = false;
                cmd.Parameters.AddWithValue("@ObsTeam", SqlDbType.VarChar).Value = selected_Oteam;
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("UPDATE dbo.AuditTable SET IsSubmittedToObserver='true' WHERE id=" + AuditNo.SelectedItem.Value, cnn);
                cmd1.ExecuteNonQuery();
                string getObserverMAilId = string.Empty;
                string[] sel_Observer_team = selected_Oteam.Split(';');
                for (int i = 0; i < sel_Observer_team.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(sel_Observer_team[i]))
                    {
                        SqlConnection connection = new SqlConnection(connetionString1);
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
                string auditNo = AuditNo.SelectedItem.Value;
                string body = "<b>Audit Details</b><br/> <p>Click the link given below to review the Audit Deatils and then approve or reject it.</p><br/><a href='https://localhost:44367/AuditDetails" + "?id=" + auditNo + "' >Click here</a>";
                string cc = "";
                sendEmail(body, getObserverMAilId,cc);
                string title = "Successfully Submitted!";
                lstauditee.Visible = false;
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            
        }

        protected void modalButt_Click(object sender, EventArgs e)
        {
            //string sub_butt = FrmSubmit.Text;
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
            {
                if (AppNo.Checked == true)
                {
                    //On Rejection--By Observer Team
                    string selected_Ateam = string.Empty;
                    foreach (ListItem li in lstauditee.Items)
                    {
                        if (li.Selected == true)
                        {
                            selected_Ateam += li.Text + ";";
                        }
                    }
                    string getAuditMailId = string.Empty;
                    string[] sel_Audit_team = selected_Ateam.Split(';');
                    for (int i = 0; i < sel_Audit_team.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(sel_Audit_team[i]))
                        {
                            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
                            SqlConnection connection = new SqlConnection(connetionString1);
                            connection.Open();
                            SqlCommand command = new SqlCommand("SELECT Mail_id FROM dbo.mstAuditee WHERE Name='" + sel_Audit_team[i] + "'", connection);
                            SqlDataReader dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                getAuditMailId += dataReader.GetString(0).ToString() + ",";
                            }
                            connection.Close();
                        }
                    }
                    string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
                    SqlConnection cnn = new SqlConnection(connetionString);
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE dbo.Form4Details SET AuditeeTeam='" + 
                        selected_Ateam + "' WHERE FK_AuditID=" + Request.QueryString["id"], cnn);
                    cmd.ExecuteNonQuery();
                    SqlCommand sqlCommand = new SqlCommand("SELECT ObserverTeam FROM dbo.Form4Details WHERE FK_AuditID=" + Request.QueryString["id"], cnn);
                    string observerTeam = (string)sqlCommand.ExecuteScalar();
                    string getObserverMAilId = string.Empty;
                    string[] sel_Observer_team = observerTeam.Split(';');
                    for (int i = 0; i < sel_Observer_team.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(sel_Observer_team[i]))
                        {
                            SqlConnection connection = new SqlConnection(connetionString);
                            connection.Open();
                            SqlCommand command = new SqlCommand("SELECT Mail_id FROM mstObserverTeam WHERE Name='" + sel_Observer_team[i]+"'", connection);
                            SqlDataReader dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                getObserverMAilId += dataReader.GetString(0).ToString() + ",";
                            }
                            connection.Close();
                        }
                    }
                    //SqlCommand sqlCommand1 = new SqlCommand("SELECT CreatedBy FROM dbo.AuditTable WHERE id="+ Request.QueryString["id"], cnn);
                    //string auditor = (string)sqlCommand1.ExecuteScalar();
                    cnn.Close();
                    string auditNo = AuditNo.SelectedItem.Value;
                    string body = "<b>Audit Details</b><br/> <p>Click the link given below to review the Audit Deatils and then Fill NC Report.</p><br/><a href='https://localhost:44367/AuditDetails" + "?AuditeeAppID=" + auditNo + "' >Click here</a>";
                    sendEmail(body, getAuditMailId,getObserverMAilId);
                    string redirectScript = " window.location.href = 'Audit.aspx';";
                    ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
                }
                else
                {
                    //OnApproval--By Observer Team
                    string getMailId = "dhvanigolani2000@gmail.com";
                    string auditNo = AuditNo.SelectedItem.Text;
                    string body = "<b>Audit Details</b><br/> <p>Audit No. " + auditNo + " has been Approved!</p><br/>";
                    string cc = "";
                    sendEmail(body, getMailId,cc);
                    string redirectScript = " window.location.href = 'Audit.aspx';";
                    ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
                }
            }
            else
            {
                string redirectScript = " window.location.href = 'Audit.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
        }

        protected void Link1_Click(object sender, EventArgs e)
        {
            string auditNo = AuditNo.SelectedItem.Value;
            string redirectScript = " window.location.href = 'WebForm1.aspx?id="+auditNo+ "';";
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
        }

        protected void Link2_Click(object sender, EventArgs e)
        {
            string auditNo = AuditNo.SelectedItem.Value;
            string redirectScript = " window.location.href = 'HSE_Surveillance.aspx?id=" + auditNo + "';";
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
        }

        protected void Link3_Click(object sender, EventArgs e)
        {
            if(Request.QueryString["AuditeeAppID"] != null && Request.QueryString["AuditeeAppID"] != string.Empty)
            {
                string auditNo = AuditNo.SelectedItem.Value;
                string redirectScript = " window.location.href = 'NCReport.aspx?AuditeeID=" + auditNo + "';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
            else
            {
                string auditNo = AuditNo.SelectedItem.Value;
                string redirectScript = " window.location.href = 'NCReport.aspx?id=" + auditNo + "';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
        }

        protected void sendEmail(string body, string ToEmail,string CCEmail)
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
                //message.CC.Add(new MailAddress("dhvanigolani2000@gmail.com"));
                if(CCEmail!=null || CCEmail!="") {
                    string[] cc = CCEmail.Split(',');
                    for (int i = 0; i < cc.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(cc[i]))
                        {
                            message.CC.Add(new MailAddress(cc[i]));
                        }
                    }
                }
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