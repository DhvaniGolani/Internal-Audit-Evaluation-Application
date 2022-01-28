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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace Project2021
{
    public partial class Kendo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("getAuditDetails", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter1 = new SqlParameter("@id", 25);
            parameter1.Direction = ParameterDirection.Input;
            parameter1.DbType = DbType.Int16;
            cmd.Parameters.Add(parameter1);
            SqlDataReader dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            SqlCommand command = new SqlCommand("getData", cnn);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter("@id", 25);
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.Int16;
            command.Parameters.Add(parameter);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            string filename = Server.MapPath("GeneartePDF.pdf");
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 15f);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                writer.PageEvent = new PDFFooter();
                if (document.IsOpen() == false)
                {
                    document.Open();
                }
                PdfPTable table = new PdfPTable(2);
                foreach (DataColumn dc in dt.Columns)
                {
                    table.AddCell(new Phrase(dc.ColumnName));
                }
                iTextSharp.text.Font font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
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
                        PdfPCell cell_1 = new PdfPCell(new Phrase(r[0].ToString().Substring(0, 10), font));
                        cell_1.Border = 0;
                        cell_1.Padding = 10f;
                        table.AddCell(cell_1);

                        pCell1 = new PdfPCell(new Phrase("Name of Site:", font));
                        pCell1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell1.Padding = 10f;
                        pCell1.Border = 0;
                        table.AddCell(pCell1);
                        PdfPCell cell_2 = new PdfPCell(new Phrase(r[1].ToString(), font));
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
                        cell_3.Padding = 10f;
                        table.AddCell(cell_3);

                        pCell3 = new PdfPCell(new Phrase("Audit Team:", font));
                        pCell3.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell3.Padding = 10f;
                        pCell3.Border = 0;
                        table.AddCell(pCell3);
                        PdfPCell cell_4 = new PdfPCell(new Phrase(r[3].ToString(), font));
                        cell_4.Border = 0;
                        cell_4.Padding = 10f;
                        table.AddCell(cell_4);

                        pCell4 = new PdfPCell(new Phrase("Observer Team:", font));
                        pCell4.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell4.Padding = 10f;
                        pCell4.Border = 0;
                        table.AddCell(pCell4);
                        PdfPCell cell_5 = new PdfPCell(new Phrase(r[4].ToString(), font));
                        cell_5.Border = 0;
                        cell_5.Padding = 10f;
                        table.AddCell(cell_5);

                        pCell5 = new PdfPCell(new Phrase("Strengths:", font));
                        pCell5.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell5.Padding = 10f;
                        pCell5.Border = 0;
                        table.AddCell(pCell5);
                        PdfPCell cell_6 = new PdfPCell(new Phrase(r[5].ToString(), font));
                        cell_6.Border = 0;
                        cell_6.Padding = 10f;
                        table.AddCell(cell_6);

                        pCell6 = new PdfPCell(new Phrase("Major Non-Compliance:", font));
                        pCell6.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell6.Padding = 10f;
                        pCell6.Border = 0;
                        table.AddCell(pCell6);
                        PdfPCell cell_7 = new PdfPCell(new Phrase(r[6].ToString(), font));
                        cell_7.Border = 0;
                        cell_7.Padding = 10f;
                        table.AddCell(cell_7);

                        pCell7 = new PdfPCell(new Phrase("Minor Non-Compliance:", font));
                        pCell7.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell7.Padding = 10f;
                        pCell7.Border = 0;
                        table.AddCell(pCell7);
                        PdfPCell cell_8 = new PdfPCell(new Phrase(r[7].ToString(), font));
                        cell_8.Border = 0;
                        cell_8.Padding = 10f;
                        table.AddCell(cell_8);

                        pCell8 = new PdfPCell(new Phrase("Opportunity for Improvement:", font));
                        pCell8.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell8.Padding = 10f;
                        pCell8.Border = 0;
                        table.AddCell(pCell8);
                        PdfPCell cell_9 = new PdfPCell(new Phrase(r[8].ToString(), font));
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


            }
            catch (Exception ex)
            {

            }
            finally
            {


                document.Close();

                //  Clears all content output from Buffer Stream
                Response.ClearContent();

                //Clears all headers from Buffer Stream
                Response.ClearHeaders();

                //Adds an HTTP header to the output stream
                Response.AddHeader("Content-Disposition", "inline;filename=" + filename);

                //Gets or Sets the HTTP MIME type of the output stream
                Response.ContentType = "application/pdf";

                //Writes the content of the specified file directory to an HTTP response output stream as a file block
                Response.WriteFile(filename);

                //sends all currently buffered output to the client
                Response.Flush();

                //Clears all content output from Buffer Stream
                Response.Clear();


            }

        }

        
        private MemoryStream PDFGenerate(int id)
        {
            MemoryStream output = new MemoryStream();
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;

            cnn = new SqlConnection(connetionString);

            cnn.Open();
            //        string sqlQuery = "SELECT dbo.Question_master.Id,dbo.Question_master.Ques_no,dbo.Question_master.Questions,dbo.QuesTransaction_table.Observation,dbo.QuesTransaction_table.Score " +
            //"FROM ((dbo.QuesTransaction_table INNER JOIN dbo.AuditTable " +
            //"ON dbo.AuditTable.id = dbo.QuesTransaction_table.AuditTable_ID)" +
            //"INNER JOIN dbo.Question_master ON dbo.QuesTransaction_table.Ques_ID = dbo.Question_master.Id) WHERE dbo.AuditTable.id= 25";
            //string sql = "SELECT * FROM dbo.AuditTable WHERE id=25";
            SqlCommand cmd = new SqlCommand("getAuditDetails", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter1 = new SqlParameter("@id", id);
            parameter1.Direction = ParameterDirection.Input;
            parameter1.DbType = DbType.Int16;
            cmd.Parameters.Add(parameter1);
            SqlDataReader dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            //SqlDataReader sdr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //SqlDataAdapter sda = new SqlDataAdapter(sql, cnn);
            //sda.Fill(dt);

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
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    table.AddCell(new Phrase(dc.ColumnName));  
            //}
            iTextSharp.text.Font font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
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
                PdfPCell cell = new PdfPCell(new Phrase("Audit Details " , FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 139))));
                cell.Border = 0;
                cell.Colspan = 2;
                cell.PaddingTop = 8f;
                cell.PaddingLeft = 80f;
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(cell);
                DateTime PrintTime = DateTime.Now;
                cell = new PdfPCell(new Phrase("Date: " + PrintTime.ToLongDateString(), FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
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
                //iTextSharp.text.pdf.draw.LineSeparator line1 = new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1);
                //document.Add(new Chunk(line1));
                
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                PdfPTable tabFot = new PdfPTable(1);
                tabFot.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell;
                tabFot.TotalWidth = document.PageSize.Width;
                cell = new PdfPCell(new Phrase("© 2021 LARSEN & TOUBRO LIMITED. All rights reserved.", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE)));
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
        public static void Capture(string CapturedFilePath)
        {
            Bitmap bitmap = new Bitmap
          (Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap as System.Drawing.Image);
            graphics.CopyFromScreen(25, 25, 25, 25, bitmap.Size);

            bitmap.Save(CapturedFilePath, ImageFormat.Bmp);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try {
                string FromMail = ConfigurationManager.AppSettings["FromMail"].ToString();
                string Pass = ConfigurationManager.AppSettings["Password"].ToString();
                string Host = ConfigurationManager.AppSettings["Host"].ToString();
                MailMessage message = new MailMessage();
                //message.To.Add("monicagolani1978@gmail.com");
                message.To.Add("dhvanigolani2000@gmail.com");
                message.CC.Add(new MailAddress("dhvanigolani2000@gmail.com"));
                message.Subject = "Audit Details";
                message.From = new MailAddress(FromMail);// Email-ID of Sender  
                message.IsBodyHtml = true;
                //int i ;
                //string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
                //SqlConnection connection1 = new SqlConnection(connetionString);
                //connection1.Open();
                //SqlCommand command1 = new SqlCommand("SELECT TOP (1) id FROM dbo.AuditTable ORDER BY id DESC", connection1);
                ////SqlDataReader sqlData = command1.ExecuteReader();
                //int d= (int)command1.ExecuteScalar();
                ////while (sqlData.Read())
                ////{
                ////    string dataf = sqlData.GetValue(0).ToString();
                //    i = Convert.ToInt16(d);
                //    MemoryStream file = new MemoryStream(PDFGenerate(i).ToArray());
                //    file.Seek(0, SeekOrigin.Begin);
                //    Attachment data = new Attachment(file, "Audit_Attachment.pdf", "application/pdf");
                //    ContentDisposition disposition = data.ContentDisposition;
                //    disposition.CreationDate = DateTime.Now;
                //    disposition.ModificationDate = DateTime.Now;
                //    disposition.DispositionType = DispositionTypeNames.Attachment;
                //    message.Attachments.Add(data);//Attach the file  
                ////}
                //connection1.Close();
                //string path = @"C:\Users\dhvan\source\repos\Project2021\Project2021\AuditDetails.aspx";

                //string body = string.Empty;
                //using (StreamReader reader = new StreamReader(Server.MapPath("~/AuditDetails.aspx")))
                //{
                //    body = reader.ToString();
                //}
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(sw);
                //Server.Execute("AuditDetails.aspx", htw);
                //string body = "<b>Audit Details</b><br/> <p>Click the link given below to review the Audit Deatils and then approve or reject it.</p><br/>";
                //string body1 = "<a href='https://localhost:44367/AuditDetails/"+ "view' >Click here</a>";
                //message.Body = body + body1;
                //message.Body = ;
                SmtpClient SmtpMail = new SmtpClient();
                SmtpMail.Host = Host;
                SmtpMail.Port = 587;
                SmtpMail.EnableSsl = true;
                SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpMail.Credentials = new System.Net.NetworkCredential(FromMail,Pass);
                SmtpMail.Send(message);
                Response.Write("Email has been sent");
            }
            catch(Exception ex)
            {
                Response.Write("Failed" + ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }
        
        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            using (Bitmap bitmap = new Bitmap(browser.Width, browser.Height))
            {
                browser.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, browser.Width, browser.Height));
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] bytes = stream.ToArray();
                    imgScreenShot.Visible = true;
                    imgScreenShot.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                }
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            //string url = "https://localhost:44367/AuditDetails";
            //Thread thread = new Thread(delegate ()
            //{
            //    using (WebBrowser browser = new WebBrowser())
            //    {
            //        browser.ScrollBarsEnabled = false;
            //        browser.AllowNavigation = true;
            //        browser.Navigate(url);
            //        browser.Width = 1024;
            //        browser.Height = 768;
            //        browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentCompleted);
            //        while (browser.ReadyState != WebBrowserReadyState.Complete)
            //        {
            //            System.Windows.Forms.Application.DoEvents();
            //        }
            //    }
            //});
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
            //thread.Join();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "see();", true);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.NCReport_details WHERE Id=1", cnn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            //SqlCommand command = new SqlCommand("getData", cnn);
            //command.CommandType = CommandType.StoredProcedure;
            //SqlParameter parameter = new SqlParameter("@id", 25);
            //parameter.Direction = ParameterDirection.Input;
            //parameter.DbType = DbType.Int16;
            //command.Parameters.Add(parameter);
            //SqlDataReader sqlDataReader = command.ExecuteReader();
            //DataTable dataTable = new DataTable();
            //dataTable.Load(sqlDataReader);

            string filename = Server.MapPath("GeneartePDF.pdf");
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 15f);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
                writer.PageEvent = new PDFFooter();
                if (document.IsOpen() == false)
                {
                    document.Open();
                }
                PdfPTable table = new PdfPTable(2);
                //Set columns names in the pdf file
                for (int k = 0; k < dataTable.Columns.Count; k++)
                {
                        PdfPCell cell = new PdfPCell(new Phrase(dataTable.Columns[k].ColumnName));

                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        //cell.BackgroundColor = new iTextSharp.text.BaseColor(51, 102, 102); 
                        cell.Padding = 10f;

                        table.AddCell(cell);
                    //Add values of DataTable in pdf file
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        //for (int j = 0; j < dataTable.Columns.Count; j++)
                        //{
                            PdfPCell cell1 = new PdfPCell(new Phrase(dataTable.Rows[i][k].ToString()));

                            //Align the cell in the center
                            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            cell1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            cell1.Padding = 10f;
                        table.AddCell(cell1);
                        //}
                    }
                }

                table.WidthPercentage = 100;
                //table.SpacingBefore = 70f;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                document.Add(table);
                document.NewPage();
                
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }
            finally
            {
                document.Close();

                //  Clears all content output from Buffer Stream
                Response.ClearContent();

                //Clears all headers from Buffer Stream
                Response.ClearHeaders();

                //Adds an HTTP header to the output stream
                Response.AddHeader("Content-Disposition", "inline;filename=" + filename);

                //Gets or Sets the HTTP MIME type of the output stream
                Response.ContentType = "application/pdf";

                //Writes the content of the specified file directory to an HTTP response output stream as a file block
                Response.WriteFile(filename);

                //sends all currently buffered output to the client
                Response.Flush();

                //Clears all content output from Buffer Stream
                Response.Clear();

            }
        }
    }
}