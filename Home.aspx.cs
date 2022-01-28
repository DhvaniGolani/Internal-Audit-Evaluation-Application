using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace Project2021
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            SqlCommand cmd = new SqlCommand("formInsert", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PS_No", SqlDbType.Int).Value = PSNo.Value;
            cmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = Name.Value;
            cmd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = Email.Value;
            cmd.Parameters.AddWithValue("@Phone_No", SqlDbType.NChar).Value = PhoneNo.Value;
            cmd.ExecuteNonQuery();

            Response.Write("<script LANGUAGE='JavaScript' >alert('Login Successful')</script>");
            cnn.Close();
        }
    }
}