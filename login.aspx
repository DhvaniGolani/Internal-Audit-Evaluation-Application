<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Project2021.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet"/>
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <title>Login Page</title>
    <style>
        body{
  /*background-image:url(lbg.png);*/
        }
        .divClass{
                 border: 2px outset;
                 margin: auto;
                 padding: 10px;
                 margin-top: 20px; 
                 text-align: center;
                 width:35%;
             }
        table.center{
                 margin-left: auto;
                 margin-right: auto;
                 font-weight: bold;
                  text-emphasis-color: white;
             }
        td{
                 padding: 5px;
             }
        
        h3{
           color: darkblue;
           padding:20px;
        }
        .fadeInDown {
  -webkit-animation-name: fadeInDown;
  animation-name: fadeInDown;
  -webkit-animation-duration: 2s;
  animation-duration: 2s;
  -webkit-animation-fill-mode: both;
  animation-fill-mode: both;
}
        @-webkit-keyframes fadeInDown {
  0% {
    opacity: 0;
    -webkit-transform: translate3d(0, -100%, 0);
    transform: translate3d(0, -100%, 0);
  }
  100% {
    opacity: 1;
    -webkit-transform: none;
    transform: none;
  }
}

@keyframes fadeInDown {
  0% {
    opacity: 0;
    -webkit-transform: translate3d(0, -100%, 0);
    transform: translate3d(0, -100%, 0);
  }
  100% {
    opacity: 1;
    -webkit-transform: none;
    transform: none;
  }
}

/* Simple CSS3 Fade-in Animation */
@-webkit-keyframes fadeIn { from { opacity:0; } to { opacity:1; } }
@-moz-keyframes fadeIn { from { opacity:0; } to { opacity:1; } }
@keyframes fadeIn { from { opacity:0; } to { opacity:1; } }

        .wrapper {
  display: flex;
  align-items: center;
  flex-direction: column; 
  justify-content: center;
  width: 100%;
  min-height: 100%;
  padding: 10px;
}
        #formContent {
  -webkit-border-radius: 10px 10px 10px 10px;
  border-radius: 10px 10px 10px 10px;
  background: #fff;
  padding: 30px;
  width: 90%;
  max-width: 450px;
  position: relative;
  padding: 0px;
  -webkit-box-shadow: 0 30px 60px 0 rgba(0,0,0,0.3);
  box-shadow: 0 30px 60px 0 rgba(0,0,0,0.3);
  text-align: center;
}
    </style>
</head>
<body>
    <div class="wrapper fadeInDown">
        <div id="formContent">
    <form id="form1" runat="server">
        
       <%-- <div class="divClass">--%>
            
            <h3> Login </h3>
        <b>  
                <asp:Label ID="Msg" ForeColor="red" runat="server" />  
            </b>
            <table class="center">  
                
                <tr> 
                    
                    <td> UserName: </td>  
                    <td> 
                        <div class="form-group">
                            <asp:TextBox ID="UserName" class="form-control" runat="server" />  
                        </div>
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>  
                    </td>  
                    
                </tr> 
                <tr>  
                    <td> Password:</td>  
                    <td>  
                        <div class="form-group">
                            <asp:TextBox ID="UserPass" TextMode="Password" class="form-control" runat="server" /> 
                        </div>
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="UserPass" ErrorMessage="Required!" runat="server" ForeColor="Red"/>  
                    </td>  
                </tr> 
                <tr>  
                    <td colspan="3"> <%--Remember me?--%>
                        <asp:CheckBox ID="chkboxPersist" runat="server" style="display:none;"/>  
                    </td>  
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:LinkButton runat="server" ID="linkButt" Text="L&T Employee Login" OnClick="linkButt_Click" CausesValidation="false"/>
                        <%--<asp:HyperLink ID="Hyperlink" runat="server" Text="L&T Employee Login" NavigateUrl="~/L&T_EmpLogin.aspx" />--%>
                    </td>
                </tr>
                <tr>   
                    <td colspan="3">  
                        <asp:Button ID="Submit1" OnClick="Login_Click" class="btn btn-primary" Text="Log In" runat="server" />
                    </td>  
                </tr> 
                    
            </table>  
              
            
            
        <%--</div>--%>
    </form>
            </div>
        </div>
</body>
</html>
