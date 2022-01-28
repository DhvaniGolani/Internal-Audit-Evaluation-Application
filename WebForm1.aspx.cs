using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Project2021
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        /// <summary>
        /// Ispostback = true when button is clicked ,or any link is clicked.
        /// When my page is refreshed then postback will be false.
        ///means for the first time it will be postback = false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EditID"] != null && Request.QueryString["EditID"] != string.Empty)
                {
                    FrmSubmit.Text = "UPDATE";
                    string id = Request.QueryString["EditID"];
                    GetDataFilled(id);
                }
                else if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    FrmSubmit.Text = "BACK";
                    string id = Request.QueryString["id"];
                    GetDataFilled(id);
                }
                else
                {
                    //fill kendo grid with data
                    string connetionStr = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
                    SqlConnection cnn;
                    cnn = new SqlConnection(connetionStr);
                    cnn.Open();
                    string sql = "SELECT Id,Ques_no,Questions FROM dbo.Question_master";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dataReader);
                    string json = JsonConvert.SerializeObject(dt);
                    hdnjson.Value = json;
                    cnn.Close();
                    datepicker.Value = DateTime.Now.ToString("MM/dd/yyyy");
                    getSiteNameDrp();
                }
            }
        }
        protected void GetDataFilled(string id) {
            string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            getSiteNameDrp();
            SqlConnection scnn1;
            scnn1 = new SqlConnection(connetionString1);
            scnn1.Open();
            string sql1 = "SELECT * FROM dbo.AuditTable WHERE id=" + id;
            SqlCommand cmd1 = new SqlCommand(sql1, scnn1);
            SqlDataReader dataReader1 = cmd1.ExecuteReader();
            while (dataReader1.Read())
            {

                string dateVal = dataReader1.GetValue(0).ToString();
                DateTime dt = Convert.ToDateTime(dateVal);
                datepicker.Value = dt.ToString("dd/MM/yyyy");
                sitedetails.Value = dataReader1.GetValue(2).ToString();
                strengths1.Value = dataReader1.GetValue(5).ToString();
                majorNC1.Value = dataReader1.GetValue(6).ToString();
                minorNC1.Value = dataReader1.GetValue(7).ToString();
                OFIs1.Value = dataReader1.GetValue(8).ToString();
                string sitename1 = dataReader1.GetValue(1).ToString();
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
                getAuditObserverDrp();
                //Fetch audit team and observer team from database
                string auditTeam = dataReader1.GetValue(3).ToString();
                string observerTeam = dataReader1.GetValue(4).ToString();

                if (auditTeam != null)
                {
                    string[] a_team = auditTeam.Split(';');
                    for (int i = 0; i < a_team.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(a_team[i]))
                        {
                            if (lstAuditTeam.Items.Count > 0)
                            {
                                lstAuditTeam.Items.FindByText(a_team[i]).Selected = true;
                            }
                        }
                    }
                }
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
            dataReader1.Close();
            SqlCommand sqlcommand = new SqlCommand("getData", scnn1);
            sqlcommand.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter("@id", id);
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
            sitename.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Here", "0"));
            sitename.Items[0].Attributes["disabled"] = "disabled";
            cnn3.Close();
        }
        protected void getAuditObserverDrp()
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn1, cnn2;
            cnn1 = new SqlConnection(connetionString);
            cnn2 = new SqlConnection(connetionString);
            //Populate listbox from database
            cnn1.Open();
            SqlCommand sqlCommand = new SqlCommand("getAuditObserverTeam", cnn1);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            int pid = int.Parse(sitename.Items[sitename.SelectedIndex].Value);
            SqlParameter sparameter = new SqlParameter("@projectid", pid);
            sparameter.Direction = ParameterDirection.Input;
            sparameter.DbType = DbType.Int16;
            SqlParameter parameter1 = new SqlParameter("@team", "audit");
            parameter1.Direction = ParameterDirection.Input;
            parameter1.DbType = DbType.String;
            sqlCommand.Parameters.Add(sparameter);
            sqlCommand.Parameters.Add(parameter1);
            string getAT = (string)sqlCommand.ExecuteScalar();
            string[] str_at = getAT.Split(';');
            DataTable dtmain = new DataTable();
            dtmain.Columns.Add("ID");
            dtmain.Columns.Add("PS_No");

            foreach (string str in str_at)
            {
                sqlCommand = new SqlCommand("SELECT ID,PS_No FROM dbo.mstAuditTeam WHERE ID=" + str, cnn1);
                sqlCommand.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = sqlCommand;
                ad.Fill(dt);
                if (dt?.Rows?.Count > 0)
                {
                    DataRow dr = dtmain.NewRow();
                    dr["ID"] = dt.Rows[0]["ID"].ToString();
                    dr["PS_No"] = dt.Rows[0]["PS_No"].ToString();
                    dtmain.Rows.Add(dr);
                }
            }
            lstAuditTeam.DataSource = dtmain;
            lstAuditTeam.DataTextField = "PS_No";
            lstAuditTeam.DataValueField = "ID";
            lstAuditTeam.DataBind();
            cnn1.Close();

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
            dtmain1.Columns.Add("PS_No");
            string[] str_ot = getOT.Split(';');
            foreach (string str in str_ot)
            {
                SqlCommand sql = new SqlCommand("SELECT PS_No,ID FROM dbo.mstObserverTeam WHERE ID=" + str, cnn1);
                sql.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = sql;
                ad.Fill(dt);
                if (dt?.Rows?.Count > 0)
                {
                    DataRow dr = dtmain1.NewRow();
                    dr["ID"] = dt.Rows[0]["ID"].ToString();
                    dr["PS_No"] = dt.Rows[0]["PS_No"].ToString();
                    dtmain1.Rows.Add(dr);
                }
            }
            lstObserverTeam.DataSource = dtmain1;
            lstObserverTeam.DataTextField = "PS_No";
            lstObserverTeam.DataValueField = "ID";
            lstObserverTeam.DataBind();
            cnn2.Close();
        }

        protected void sitename_SelectedIndexChanged(object sender, EventArgs e)
        {
            sitename.Items[0].Attributes["disabled"] = "disabled";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DrpSumo", "drp();", true);
            string message = sitename.Items[sitename.SelectedIndex].Value;
            if (message == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Select Site Name!!');", true);
            }
            getAuditObserverDrp();
        }
        
        public void SendEmail(string ToEmail, int AuditId)
        {
            try
            {
                string FromMail = ConfigurationManager.AppSettings["FromMail"].ToString();
                string Pass = ConfigurationManager.AppSettings["Password"].ToString();
                string Host = ConfigurationManager.AppSettings["Host"].ToString();
                string cc = "girishgolani71@gmail.com";
                MailMessage message = new MailMessage();
                string[] Multi = ToEmail.Split(',');
                for (int i = 0; i < Multi.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(Multi[i]))
                    {
                        message.To.Add(new MailAddress(Multi[i]));
                    }
                }
                //message.To.Add("dhvanigolani2000@gmail.com");
                message.CC.Add(new MailAddress(cc));
                message.Subject = "Audit Details";
                message.From = new MailAddress(FromMail);// Email-ID of Sender  
                message.IsBodyHtml = true;
                //int i = 28;
                MemoryStream file = new MemoryStream(PDFGenerate(AuditId).ToArray());
                file.Seek(0, SeekOrigin.Begin);
                Attachment data = new Attachment(file, "Audit_Attachment.pdf", "application/pdf");
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = DateTime.Now;
                disposition.ModificationDate = DateTime.Now;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                message.Attachments.Add(data);//Attach the file  

                message.Body = "Audit Details submitted on " + DateTime.Now;
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

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            DataTable deserializedObj = JsonConvert.DeserializeObject<DataTable>(hdnfld1.Value);
            string selected_Ateam = string.Empty;
            foreach (System.Web.UI.WebControls.ListItem li in lstAuditTeam.Items)
            {
                if (li.Selected == true)
                {
                    selected_Ateam += li.Text + ";";
                }
            }
            string selected_Oteam = string.Empty;
            foreach (System.Web.UI.WebControls.ListItem li in lstObserverTeam.Items)
            {
                if (li.Selected == true)
                {
                    selected_Oteam += li.Text + ";";
                }
            }
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string selected_Sitename = sitename.Items[sitename.SelectedIndex].Text;
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT (siteName) FROM dbo.AuditTable WHERE siteName='"+selected_Sitename+"'", cnn);
            int counter = (int)sqlCommand.ExecuteScalar();
            counter += 1;
            if (counter == 0)
            {
                counter = 1;
            }
            SqlCommand cmd = new SqlCommand("insertAuditForm", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            string title;
            if (Request.QueryString["EditID"] != null && Request.QueryString["EditID"] != string.Empty)
            {
                cmd.Parameters.AddWithValue("@Action", SqlDbType.VarChar).Value = "UPDATE";
                cmd.Parameters.AddWithValue("@AuditID", SqlDbType.Int).Value = Request.QueryString["EditID"];
                DateTime date = Convert.ToDateTime(datepicker.Value);
                cmd.Parameters.AddWithValue("@dateOfAudit", SqlDbType.Date).Value = date;
                string gridchanges = hdn_gridChanges.Value;
                if (gridchanges == "true") 
                {
                    cmd.Parameters.AddWithValue("@GridData", SqlDbType.VarChar).Value = "True";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GridData", SqlDbType.VarChar).Value = "False";
                }
                SqlCommand sqlCommand1 = new SqlCommand("SELECT IsSubmittedToObserver FROM dbo.AuditTable WHERE id="+ Request.QueryString["EditID"], cnn);
                bool i = (bool)sqlCommand1.ExecuteScalar();
                SqlCommand sqlCommand2 = new SqlCommand("SELECT Audit_ID FROM dbo.AuditTable WHERE id=" + Request.QueryString["EditID"], cnn);
                string auID = (string)sqlCommand2.ExecuteScalar();
                cmd.Parameters.AddWithValue("@Audit_ID", SqlDbType.VarChar).Value = auID;
                cmd.Parameters.AddWithValue("@MBY", SqlDbType.VarChar).Value = "Dhvani";
                cmd.Parameters.AddWithValue("@MON", SqlDbType.Date).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@IsSubToObs", SqlDbType.Bit).Value = i;
                cmd.Parameters.AddWithValue("@siteName", SqlDbType.VarChar).Value = selected_Sitename;
                cmd.Parameters.AddWithValue("@siteDetails", SqlDbType.Text).Value = sitedetails.Value;
                cmd.Parameters.AddWithValue("@auditTeam", SqlDbType.VarChar).Value = selected_Ateam;
                cmd.Parameters.AddWithValue("@observerTeam", SqlDbType.VarChar).Value = selected_Oteam;
                cmd.Parameters.AddWithValue("@strenghts", SqlDbType.Text).Value = hdn_strengths.Value;
                cmd.Parameters.AddWithValue("@MajorNC", SqlDbType.Text).Value = hdn_majorNC.Value;
                cmd.Parameters.AddWithValue("@MinorNC", SqlDbType.Text).Value = hdn_minorNC.Value;
                cmd.Parameters.AddWithValue("@OFIs", SqlDbType.Text).Value = hdn_OFIs.Value;
                cmd.Parameters.AddWithValue("@AuditTable", SqlDbType.Text).Value = deserializedObj;
                cmd.ExecuteNonQuery();
                cnn.Close();
                title = "Updated Successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            else if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
            {
                string redirectScript = " window.location.href = 'AuditDetails.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.AddWithValue("@AuditID", SqlDbType.Int).Value = 0;
                cmd.Parameters.AddWithValue("@GridData", SqlDbType.VarChar).Value = "False";
                cmd.Parameters.AddWithValue("@dateOfAudit", SqlDbType.Date).Value = datepicker.Value;
                cmd.Parameters.AddWithValue("@Audit_ID", SqlDbType.VarChar).Value = selected_Sitename+"/20-21/"+counter;
                cmd.Parameters.AddWithValue("@CBY", SqlDbType.VarChar).Value = "Dhvani";
                cmd.Parameters.AddWithValue("@CON", SqlDbType.Date).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@siteName", SqlDbType.VarChar).Value = selected_Sitename;
                cmd.Parameters.AddWithValue("@siteDetails", SqlDbType.Text).Value = sitedetails.Value;
                cmd.Parameters.AddWithValue("@auditTeam", SqlDbType.VarChar).Value = selected_Ateam;
                cmd.Parameters.AddWithValue("@observerTeam", SqlDbType.VarChar).Value = selected_Oteam;
                cmd.Parameters.AddWithValue("@strenghts", SqlDbType.Text).Value = hdn_strengths.Value;
                cmd.Parameters.AddWithValue("@MajorNC", SqlDbType.Text).Value = hdn_majorNC.Value;
                cmd.Parameters.AddWithValue("@MinorNC", SqlDbType.Text).Value = hdn_minorNC.Value;
                cmd.Parameters.AddWithValue("@OFIs", SqlDbType.Text).Value = hdn_OFIs.Value;
                cmd.Parameters.AddWithValue("@AuditTable", SqlDbType.Text).Value = deserializedObj; 
                cmd.Parameters.AddWithValue("@IsSubToObs", SqlDbType.Bit).Value = "False";
                cmd.ExecuteNonQuery();
                cnn.Close();
                string getAuditMailId = string.Empty, getObserverMAilId = string.Empty;
                string[] sel_Audit_team = selected_Ateam.Split(';');
                for (int i = 0; i < sel_Audit_team.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(sel_Audit_team[i]))
                    {
                        SqlConnection connection = new SqlConnection(connetionString);
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT Mail_id FROM mstAuditTeam WHERE PS_No=" + sel_Audit_team[i], connection);
                        SqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            getAuditMailId += dataReader.GetString(0).ToString() + ",";
                        }
                        connection.Close();
                    }
                }
                string[] sel_Observer_team = selected_Oteam.Split(';');
                for (int i = 0; i < sel_Observer_team.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(sel_Observer_team[i]))
                    {
                        SqlConnection connection = new SqlConnection(connetionString);
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT Mail_id FROM mstObserverTeam WHERE PS_No=" + sel_Observer_team[i], connection);
                        SqlDataReader dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            getObserverMAilId += dataReader.GetString(0).ToString() + ",";
                        }
                        connection.Close();
                    }
                }
                string toEmail = getAuditMailId + getObserverMAilId;
                int AuditID;
                SqlConnection connection1 = new SqlConnection(connetionString);
                connection1.Open();
                SqlCommand command1 = new SqlCommand("SELECT TOP (1) id FROM dbo.AuditTable ORDER BY id DESC", connection1);
                int d = (int)command1.ExecuteScalar();
                AuditID = Convert.ToInt16(d);
                SendEmail(toEmail, AuditID);
                connection1.Close();
                title = "Successfully Submitted!";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            
        }
        
        //PDF Generation
        private MemoryStream PDFGenerate(int id)
        {
            MemoryStream output = new MemoryStream();
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("getAuditDetails", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter1 = new SqlParameter("@id", id);
            parameter1.Direction = ParameterDirection.Input;
            parameter1.DbType = DbType.Int16;
            cmd.Parameters.Add(parameter1);
            SqlDataReader dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);

            SqlCommand command = new SqlCommand("getData", cnn);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter("@id", id);
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.Int16;
            command.Parameters.Add(parameter);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            string filename = Server.MapPath("GeneartePDF.pdf");
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 15f);

            PdfWriter writer = PdfWriter.GetInstance(document, output);
            writer.PageEvent = new PDFFooter();
            if (document.IsOpen() == false)
            {
                document.Open();
            }
            PdfPTable table = new PdfPTable(2);
            Font font = new Font(FontFactory.GetFont("Arial", 14, Font.NORMAL, BaseColor.BLACK));
            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    PdfPCell pCell, pCell1, pCell2, pCell3, pCell4, pCell5, pCell6, pCell7, pCell8;
                    pCell = new PdfPCell(new Phrase("Date of Audit:", font));
                    pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell.Border = 0;
                    pCell.Padding = 10f;
                    table.AddCell(pCell);
                    //string date = String.Format("{0:d}", r[0].ToString());
                    PdfPCell cell_1 = new PdfPCell(new Phrase(r[0].ToString().Substring(0, 10), font));
                    cell_1.Border = 0;
                    //cell_1.Colspan = 2;
                    cell_1.Padding = 10f;
                    table.AddCell(cell_1);

                    pCell1 = new PdfPCell(new Phrase("Name of Site:", font));
                    pCell1.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell1.Padding = 10f;
                    pCell1.Border = 0;
                    table.AddCell(pCell1);
                    PdfPCell cell_2 = new PdfPCell(new Phrase(r[1].ToString(), font));
                    //cell_2.Colspan = 2;
                    cell_2.Border = 0;
                    cell_2.Padding = 10f;
                    table.AddCell(cell_2);

                    pCell2 = new PdfPCell(new Phrase("Details of Site:", font));
                    pCell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell2.Padding = 10f;
                    pCell2.Border = 0;
                    table.AddCell(pCell2);
                    PdfPCell cell_3 = new PdfPCell(new Phrase(r[2].ToString(), font));
                    cell_3.Border = 0;
                    //cell_3.Colspan = 2;
                    cell_3.Padding = 10f;
                    table.AddCell(cell_3);

                    pCell3 = new PdfPCell(new Phrase("Audit Team:", font));
                    pCell3.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell3.Padding = 10f;
                    pCell3.Border = 0;
                    table.AddCell(pCell3);
                    PdfPCell cell_4 = new PdfPCell(new Phrase(r[3].ToString(), font));
                    cell_4.Border = 0;
                    //cell_4.Colspan = 2;
                    cell_4.Padding = 10f;
                    table.AddCell(cell_4);

                    pCell4 = new PdfPCell(new Phrase("Observer Team:", font));
                    pCell4.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell4.Padding = 10f;
                    pCell4.Border = 0;
                    table.AddCell(pCell4);
                    PdfPCell cell_5 = new PdfPCell(new Phrase(r[4].ToString(), font));
                    //cell_5.Colspan = 2;
                    cell_5.Border = 0;
                    cell_5.Padding = 10f;
                    table.AddCell(cell_5);

                    pCell5 = new PdfPCell(new Phrase("Strengths:", font));
                    pCell5.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell5.Padding = 10f;
                    pCell5.Border = 0;
                    table.AddCell(pCell5);
                    PdfPCell cell_6 = new PdfPCell(new Phrase(r[5].ToString(), font));
                    //cell_6.Colspan = 2;
                    cell_6.Border = 0;
                    cell_6.Padding = 10f;
                    table.AddCell(cell_6);

                    pCell6 = new PdfPCell(new Phrase("Major Non-Compliance:", font));
                    pCell6.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell6.Padding = 10f;
                    pCell6.Border = 0;
                    table.AddCell(pCell6);
                    PdfPCell cell_7 = new PdfPCell(new Phrase(r[6].ToString(), font));
                    //cell_7.Colspan = 2;
                    cell_7.Border = 0;
                    cell_7.Padding = 10f;
                    table.AddCell(cell_7);

                    pCell7 = new PdfPCell(new Phrase("Minor Non-Compliance:", font));
                    pCell7.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell7.Padding = 10f;
                    pCell7.Border = 0;
                    table.AddCell(pCell7);
                    PdfPCell cell_8 = new PdfPCell(new Phrase(r[7].ToString(), font));
                    //cell_8.Colspan = 2;
                    cell_8.Border = 0;
                    cell_8.Padding = 10f;
                    table.AddCell(cell_8);

                    pCell8 = new PdfPCell(new Phrase("Opportunity for Improvement:", font));
                    pCell8.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pCell8.Padding = 10f;
                    pCell8.Border = 0;
                    table.AddCell(pCell8);
                    PdfPCell cell_9 = new PdfPCell(new Phrase(r[8].ToString(), font));
                    //cell_9.Colspan = 2;
                    cell_9.Border = 0;
                    cell_9.Padding = 10f;
                    table.AddCell(cell_9);

                }
            }

            table.WidthPercentage = 100;
            table.SpacingBefore = 70f;
            table.HorizontalAlignment = Element.ALIGN_RIGHT;
            document.Add(table);
            document.NewPage();
            if (dataTable != null)
            {
                PdfPTable pdfPTable = new PdfPTable(dataTable.Columns.Count);
                float[] widths = new float[] { 12f, 30f, 85f, 40f, 20f };
                pdfPTable.SetWidths(widths);
                PdfPCell pdfPCell, pdfPCell1, pdfPCell2, pdfPCell3, pdfPCell4, pdfPCell5 = null;

                pdfPCell1 = new PdfPCell(new Phrase(new Chunk("Id", font)));
                pdfPCell1.Padding = 5f;
                pdfPTable.AddCell(pdfPCell1);
                pdfPCell2 = new PdfPCell(new Phrase(new Chunk("Ques_No", font)));
                pdfPCell2.Padding = 5f;
                pdfPTable.AddCell(pdfPCell2);
                pdfPCell3 = new PdfPCell(new Phrase(new Chunk("Questions", font)));
                pdfPCell3.Padding = 5f;
                pdfPTable.AddCell(pdfPCell3);
                pdfPCell4 = new PdfPCell(new Phrase(new Chunk("Observation", font)));
                pdfPCell4.Padding = 5f;
                pdfPTable.AddCell(pdfPCell4);
                pdfPCell5 = new PdfPCell(new Phrase(new Chunk("Score", font)));
                pdfPCell5.Padding = 5f;
                pdfPTable.AddCell(pdfPCell5);
                for (int rows = 0; rows < dataTable.Rows.Count; rows++)
                {
                    for (int column = 0; column < dataTable.Columns.Count; column++)
                    {
                        pdfPCell = new PdfPCell(new Phrase(new Chunk(dataTable.Rows[rows][column].ToString())));
                        pdfPTable.AddCell(pdfPCell);
                    }
                }
                //pdfPTable.SpacingBefore = 50f;
                document.Add(pdfPTable);
            }

            writer.CloseStream = false;

            document.Close();
            output.Position = 0;
            return output;
        }
        public class PDFFooter : PdfPageEventHelper
        {
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);

            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
                //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                PdfPTable tabFot = new PdfPTable(3);
                tabFot.SpacingAfter = 20F;
                //PdfPCell cell;
                tabFot.TotalWidth = document.PageSize.Width - 20f;
                tabFot.WidthPercentage = 90;
                tabFot.HorizontalAlignment = Element.ALIGN_CENTER;
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/L&t_Logo.jpg"));
                jpg.ScaleAbsolute(110f, 60f);
                PdfPCell imagecell = new PdfPCell(jpg);
                imagecell.Border = 0;
                imagecell.Rowspan = 2;
                //cell = new PdfPCell(new Phrase("Header"));
                tabFot.AddCell(imagecell);
                PdfPCell cell = new PdfPCell(new Phrase("Audit Details ", FontFactory.GetFont("Arial", 18, Font.BOLD, new BaseColor(0, 0, 139))));
                cell.Border = 0;
                cell.Colspan = 2;
                cell.PaddingTop = 8f;
                cell.PaddingLeft = 80f;
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(cell);
                DateTime PrintTime = DateTime.Now;
                cell = new PdfPCell(new Phrase("Date: " + PrintTime.ToLongDateString(), FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                cell.Colspan = 2;
                cell.PaddingLeft = 80f;
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.PaddingTop = 10f;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(cell);
                tabFot.LockedWidth = true;
                //tabFot.WriteSelectedRows(0, -1, 10, document.Top, writer.DirectContent);
                document.Add(tabFot);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                //PdfPCell cell;
                //tabFot.TotalWidth = 300F;
                //cell = new PdfPCell(new Phrase("Footer"));
                //tabFot.AddCell(cell);
                //tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
                PdfPTable tabFot = new PdfPTable(1);
                tabFot.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell;
                tabFot.TotalWidth = document.PageSize.Width;
                cell = new PdfPCell(new Phrase("© 2021 LARSEN & TOUBRO LIMITED. All rights reserved.", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.WHITE)));
                cell.BackgroundColor = new BaseColor(0, 0, 139);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = 0;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 0, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }
        protected void modalButt_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EditID"] != null && Request.QueryString["EditID"] != string.Empty)
            {
                string redirectScript = " window.location.href = 'Audit.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
            else
            {
                string redirectScript = " window.location.href = 'HSE_Surveillance.aspx';";
                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", redirectScript, true);
            }
        }
    }
}