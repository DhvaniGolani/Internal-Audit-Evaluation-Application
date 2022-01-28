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
    public partial class Role_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "load", "callOnLoad();", true);
            GetData();
        }
        protected void GetData()
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //string sql = "SELECT Id,PSNO,RoleId FROM dbo.tbl_Adminmaster";
            string sql = "SELECT dbo.tbl_Adminmaster.Id,dbo.tbl_Adminmaster.PSNO,dbo.tbl_Rolemaster.Rolename FROM dbo.tbl_Adminmaster INNER JOIN dbo.tbl_Rolemaster ON dbo.tbl_Adminmaster.RoleId=dbo.tbl_Rolemaster.Id WHERE dbo.tbl_Adminmaster.isDeleted=0";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            string json = JsonConvert.SerializeObject(dt);
            hdnjson.Value = json;
        }
        protected void deleting_Click(object sender, EventArgs e)
        {
            string title = "Delete";
            string body = "Delete Rights of PSNo.: " + PSno.Value + "?";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "UPDATE dbo.tbl_Adminmaster SET isDeleted=1 WHERE Id=" + HiddenField1.Value;
            //SqlCommand cmd = new SqlCommand(sql, cnn);
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            cnn.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#myModal').modal('hide')", true);
            GetData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "load", "callOnLoad();", true);
        }
    }
}