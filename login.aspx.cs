using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project2021
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Obsolete]
        protected void Login_Click(object sender, EventArgs e)
        {
            if (FormsAuthentication.Authenticate(UserName.Text, UserPass.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(UserName.Text, chkboxPersist.Checked);
            }
            else
            {
                Msg.Text = "Invalid User Name and/or Password";
            }
        }

        protected void linkButt_Click(object sender, EventArgs e)
        {
            /*uncomment for lnt login */
            Session_Start();
            //Session["__Role"] = "Employee";

            //Response.Redirect("PODashboard.aspx");

            //uncomment for non lnt login

            string strWinSiteURL = string.Empty;

            string strURL = Request.Url.Query.ToString();

            if (strURL.Contains("ReturnUrl"))
            {
                strWinSiteURL = ConfigurationManager.AppSettings["WinSiteURL"].ToString() + strURL.Substring(strURL.IndexOf('=') + 1);
            }
            else
            {
                strWinSiteURL = ConfigurationManager.AppSettings["WinSiteURL"].ToString() + "Audit.aspx";
                //strWinSiteURL = ConfigurationManager.AppSettings["WinSiteURL"].ToString() + strURL.Substring(strURL.IndexOf('=') + 1);
            }

            Response.Redirect(strWinSiteURL);
        }

        protected void Session_Start()
        {
            string[] LoginUser = User.Identity.Name.Split('\\');

            string sDomain = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("UserDomain");

            string sUserLoginID = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("UserPSNo");
            if (sUserLoginID == "0")
            { sUserLoginID = LoginUser[LoginUser.Length - 1]; }

            string sessionId = Session.SessionID;

            if (!sDomain.Equals("Access denied."))
            {
                EmployeeDetails objEmp = new clsUserAuthenticationBAL().GetEmpDetailData_AD_SQL(sDomain, sUserLoginID);
                //objEmp.EmpName = "shruti";
                //objEmp.EmpPsNo = "vcollabera-shrutis";
                if (objEmp != null && objEmp.EmpName.Trim() != string.Empty)
                {
                    Session.Add("__UserDomain", sDomain);
                    Session.Add("__UserLoginID", sUserLoginID);
                    Session.Add("__UserPSNo", objEmp.EmpPsNo.Trim());
                    Session.Add("__UserName", objEmp.EmpName.Trim());
                    Session.Add("__UserIC", objEmp.EmpIC);
                    Session.Add("__UserEmail", objEmp.EmpEmail);
                    Session.Add("__UserDept", objEmp.EmpDept);

                    FormsAuthentication.RedirectFromLoginPage(sUserLoginID, true);
                }
                else
                {
                    Session.Add("__UserDomain", null);
                    Session.Add("__UserLoginID", null);
                    Session.Add("__UserPSNo", null);
                    Session.Add("__UserName", null);
                    Session.Add("__UserIC", null);
                    Session.Add("__UserEmail", null);
                    Session.Add("__UserDept", null);

                    //objLog.WriteUserLogFile("Unauthorized_User :- " + User.Identity.Name.ToString());
                    Uri uri = HttpContext.Current.Request.Url;
                    String host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                    // Response.Redirect(host + "/Pages/login.aspx");
                    string msg = "Unauthorized User - " + sUserLoginID;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + msg + "');window.location ='Login.aspx';", true);
                }
            }
            else
            {
                Session.Add("__UserDomain", null);
                Session.Add("__UserLoginID", null);
                Session.Add("__UserPSNo", null);
                Session.Add("__UserName", null);
                Session.Add("__UserIC", null);
                Session.Add("__UserEmail", null);
                Session.Add("__UserDept", null);
                //clsLog.WriteUserLogFile("Unauthorized_User :- " + User.Identity.Name.ToString());
                //objLog.WriteUserLogFile("Unauthorized_User :- " + User.Identity.Name.ToString());
                Uri uri = HttpContext.Current.Request.Url;
                String host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;
                // Response.Redirect(host + "/Pages/login.aspx");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + sDomain + "');window.location ='Login.aspx';", true);
            }
        }

    }
}