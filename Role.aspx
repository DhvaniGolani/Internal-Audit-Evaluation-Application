<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="Project2021.Role" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://code.jquery.com/jquery-3.5.1.js" integrity="sha256-QWo7LDvxbWT2tbbQ97B53yJnYU3WhH/C8ycbRAkjPDc=" crossorigin="anonymous"></script>
        
    <!-- Font Awesome -->
        <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css" rel="stylesheet" />
    <!-- Google Fonts -->
        <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" rel="stylesheet" />
    <!-- Bootstrap core CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/css/bootstrap.min.css" rel="stylesheet"/>
    <!-- MDB -->
        <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/3.3.0/mdb.min.css" rel="stylesheet" />--%>
    <!-- Material Design Bootstrap -->
        <link href="https://cdnjs.cloudflare.com/ajax/libs/mdbootstrap/4.19.1/css/mdb.min.css" rel="stylesheet"/>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/mdbootstrap/4.19.1/js/mdb.min.js"></script>
    <!-- JQuery -->
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <!-- Bootstrap tooltips -->
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.4/umd/popper.min.js"></script>
    <!-- Bootstrap core JavaScript -->
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/js/bootstrap.min.js"></script>
    <!-- MDB -->
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/3.3.0/mdb.min.js" ></script>
    
    <style>
        .divClass{
                 border: 2px outset;
                 margin: auto;
                 padding: 10px;
                 margin-top: 20px; 
                 text-align: center;
                 width:35%;
             }
        h3,h5{
           color: darkblue;
        }
    </style>
    <title>Role</title>   
</head>
<body>
    
    <form id="form1" runat="server">
        <div class="divClass">
            <h3></h3>
            <div class="row">
                <div class="col" style="padding-top:5px">
                    <h5>Role:</h5>
                </div>
                <div class="col">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="role" class="custom-select" runat="server" OnSelectedIndexChanged="role_SelectedIndexChanged" AutoPostBack="true" >
                                </asp:DropDownList>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    <asp:RequiredFieldValidator id="reqRole" Text="Required!!" Display="Dynamic" InitialValue="0" ControlToValidate="role" Runat="server" ForeColor="Red" />
                </div>
            </div>
            <div class="row">
                <div class="col" style="padding-top:30px">
                    <h5>PS No.:</h5>
                </div>
                <div class="col" style="padding-top:20px">
                    <%--<div class="md-form md-outline">--%>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="PS_no" class="custom-select" runat="server" >
                                </asp:DropDownList>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" Text="Required!!" Display="Dynamic" InitialValue="0" ControlToValidate="PS_no" Runat="server" ForeColor="Red" />
                        <%--<asp:TextBox ID="PSNo" runat="server" MaxLength="5"  class="form-control"/>
                        <label class="form-label" for="PSNo">PS No</label>
                        <asp:RequiredFieldValidator ID="reqPSNo" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="PSNo" Text="Required!!"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ErrorMessage="Accepts only numbers!" ControlToValidate="PSNo" ValidationExpression="^[0-9]*$" ForeColor="Red"/>--%>
                    <%--</div>--%>
                </div>
            </div>

            <asp:Button ID="submit" class="btn btn-primary btn-block" runat="server" Text="Submit" OnClick="submit_Click" style="margin-top:30px"/>
        </div>
    </form>
</body>
</html>
