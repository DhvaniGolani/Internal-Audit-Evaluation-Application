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
    public partial class Role : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connetionString1 = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
                //Populate items in Role DrpList
                SqlConnection cnn3;
                cnn3 = new SqlConnection(connetionString1);
                cnn3.Open();
                SqlCommand command = new SqlCommand("SELECT Id,Rolename FROM dbo.tbl_Rolemaster", cnn3);
                role.DataSource = command.ExecuteReader();
                role.DataTextField = "Rolename";
                role.DataValueField = "Id";
                role.DataBind();
                role.Items.Insert(0, new ListItem("Select Here", "0"));
                role.Items[0].Attributes["disabled"] = "disabled";
                
                PS_no.Items.Insert(0, new ListItem("Select Here", "0"));
                PS_no.Items[0].Attributes["disabled"] = "disabled";
                cnn3.Close();
                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    string id = Request.QueryString["id"];
                    submit.Text = "Update";
                    SqlConnection cnn;
                    cnn = new SqlConnection(connetionString1);
                    cnn.Open();
                    string sqlquery = "SELECT RoleId FROM dbo.tbl_Adminmaster WHERE Id=" + id;
                    SqlCommand cmd = new SqlCommand(sqlquery, cnn);
                    int RoleId = (int)cmd.ExecuteScalar();
                    if (RoleId == 1)
                    {
                        SqlCommand sqlCommand2 = new SqlCommand("SELECT ID,PS_No FROM dbo.mstAuditTeam", cnn);
                        PS_no.DataSource = sqlCommand2.ExecuteReader();
                        PS_no.DataTextField = "PS_No";
                        PS_no.DataValueField = "ID";
                        PS_no.DataBind();
                    }
                    else if (RoleId == 2)
                    {
                        SqlCommand sqlCommand3 = new SqlCommand("SELECT ID,PS_No FROM dbo.mstObserverTeam", cnn);
                        PS_no.DataSource = sqlCommand3.ExecuteReader();
                        PS_no.DataTextField = "PS_No";
                        PS_no.DataValueField = "ID";
                        PS_no.DataBind();
                    }
                    cnn.Close();
                    cnn.Open();
                    string sqlquery2 = "SELECT Rolename FROM dbo.tbl_Rolemaster WHERE Id=" + RoleId;
                    SqlCommand sqlCommand = new SqlCommand(sqlquery2, cnn);
                    string rolename = (string)sqlCommand.ExecuteScalar();
                    if (rolename != null)
                    {
                        if (!string.IsNullOrEmpty(rolename))
                        {
                            if (role.Items.Count > 0)
                            {
                                role.Items.FindByText(rolename).Selected = true;
                            }
                        }
                    }

                    string sql1 = "SELECT PSNO FROM dbo.tbl_Adminmaster WHERE Id =" + id;
                    SqlCommand cmd1 = new SqlCommand(sql1, cnn);
                    string psno = (string)cmd1.ExecuteScalar();
                    if (psno != null)
                    {
                        if (!string.IsNullOrEmpty(psno))
                        {
                            if (PS_no.Items.Count > 0)
                            {
                                PS_no.Items.FindByText(psno).Selected = true;
                            }
                        }
                    }
                    cnn.Close();
                }
                else
                {
                    role.Items[0].Selected = true;
                    PS_no.Items[0].Selected = true;
                }
                
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            int r = Int16.Parse(role.SelectedValue);
            //string selectedRole = role.SelectedItem.Text;
            string selectedPSNO = PS_no.SelectedItem.Text;
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
            {
                string id = Request.QueryString["id"];
                string sql = "UPDATE dbo.tbl_Adminmaster SET RoleId=" + r + ", PSNO='" + selectedPSNO + "' WHERE Id="+ id; 
                adapter.UpdateCommand = new SqlCommand(sql, cnn);
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            else
            {
                string sql = "INSERT INTO dbo.tbl_Adminmaster(RoleId,PSNO,isDeleted) VALUES (" + r + ",'" + selectedPSNO + "',0)";
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            
            cnn.Close();

            string redirectScript = " window.location.href = 'Role_Dashboard.aspx';";
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('Successfully Submitted!');" + redirectScript, true);
        }

        protected void role_SelectedIndexChanged(object sender, EventArgs e)
        {
            role.Items[0].Attributes["disabled"] = "disabled";
            string message = role.Items[role.SelectedIndex].Value;
            if (message == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Select Role!!');", true);
            }
            string connetionString = ConfigurationManager.ConnectionStrings["DBStr"].ConnectionString;
            SqlConnection cnn1;
            cnn1 = new SqlConnection(connetionString);
            //Populate PSNO from database
            cnn1.Open();
            int r = Int16.Parse(role.SelectedValue);
            if (r == 1)
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT ID,PS_No FROM dbo.mstAuditTeam", cnn1);
                PS_no.DataSource = sqlCommand.ExecuteReader();
                PS_no.DataTextField = "PS_No";
                PS_no.DataValueField = "ID";
                PS_no.DataBind();
            }
            else if (r == 2) 
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT ID,PS_No FROM dbo.mstObserverTeam", cnn1);
                PS_no.DataSource = sqlCommand.ExecuteReader();
                PS_no.DataTextField = "PS_No";
                PS_no.DataValueField = "ID";
                PS_no.DataBind();
            }
            PS_no.Items.Insert(0, new ListItem("Select Here", "0"));
            PS_no.Items[0].Selected = true;
            PS_no.Items[0].Attributes["disabled"] = "disabled";
        }
    }
}