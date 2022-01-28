using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2021
{
    public partial class Audit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
                SqlConnection sqlcnn;

                sqlcnn = new SqlConnection(connetionString1);

                sqlcnn.Open();
                string sql = "SELECT * FROM dbo.AuditTable";
                SqlCommand cmd = new SqlCommand(sql, sqlcnn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                string json = JsonConvert.SerializeObject(dt);
                hdnjson.Value = json;

                SqlCommand sql1 = new SqlCommand("SELECT * FROM dbo.HSE_details_tbl WHERE FK_Audit_ID is not null", sqlcnn);
                SqlDataReader sqlDataReader1 = sql1.ExecuteReader();
                DataTable dataTable1 = new DataTable();
                dataTable1.Load(sqlDataReader1);
                string jsondata = JsonConvert.SerializeObject(dataTable1);
                HiddenField1.Value = jsondata;

                SqlCommand sql11 = new SqlCommand("SELECT Id,sitename,department,auditee,ObserverTeam,Major,Minor,auditor,FK_Audit_ID FROM dbo.NCReport_details", sqlcnn);
                SqlDataReader sqlDataReader11 = sql11.ExecuteReader();
                DataTable dataTable11 = new DataTable();
                dataTable11.Load(sqlDataReader11);
                string jsondata1 = JsonConvert.SerializeObject(dataTable11);
                HiddenField2.Value = jsondata1;

                string query = "SELECT dbo.AuditTable.id,dbo.Question_master.Ques_no,dbo.Question_master.Questions,dbo.QuesTransaction_table.Observation,dbo.QuesTransaction_table.Score " +
                    "FROM ((dbo.QuesTransaction_table INNER JOIN dbo.AuditTable " +
                    "ON dbo.AuditTable.id = dbo.QuesTransaction_table.AuditTable_ID)" +
                    "INNER JOIN dbo.Question_master ON dbo.QuesTransaction_table.Ques_ID = dbo.Question_master.Id)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlcnn);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                string newjson = JsonConvert.SerializeObject(dataTable);
                hdnfld1.Value = newjson;
                sqlcnn.Close();
            }
        }
        protected void edit_redirect_Click(object sender, EventArgs e)
        {
            //int id = int.Parse(hdn_get_ID_1.Value);
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("getAuditDetails", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter1 = new SqlParameter("@id", hdn_get_ID_1.Value);
            parameter1.Direction = ParameterDirection.Input;
            parameter1.DbType = DbType.Int16;
            cmd.Parameters.Add(parameter1);
            SqlDataReader dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            SqlCommand command = new SqlCommand("getData", cnn);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter("@id", hdn_get_ID_1.Value);
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.Int16;
            command.Parameters.Add(parameter);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            const int bufferLength = 10000;
            byte[] buffer = new Byte[bufferLength];
            int length = 0;
            
            string path = @"C:\Users\dhvan\source\repos\Project2021\Project2021\GeneartePDF.pdf";
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            Server.MapPath("GeneartePDF.pdf");
            string filename = "GeneartePDF.pdf";
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 15f);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create, FileAccess.Write));
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
                    document.Close();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                //Adds an HTTP header to the output stream
                Response.AddHeader("Content-Disposition", "attachment;filename=AuditDetails.pdf");
                Response.TransmitFile(path);
            }
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
    }
}