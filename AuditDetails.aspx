<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditDetails.aspx.cs" Inherits="Project2021.AuditDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
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
     
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
        <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <link href='https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/ui-lightness/jquery-ui.css' rel='stylesheet'/> 
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>
    
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>
    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
   <script src="jquery.sumoselect.min.js"></script>
   <link href="sumoselect.css" rel="stylesheet"/>
    <!-- Kendo Grid -->      
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.224/styles/kendo.default-v2.min.css"/>
        <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.119/styles/kendo.common.min.css" />
        <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.119/styles/kendo.default.min.css" />
        <script src="https://kendo.cdn.telerik.com/2021.1.119/js/kendo.all.min.js"></script>

    <!--Toast -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet" />
 <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <style>
        table {
            width:50%;
            margin: 25px auto 25px auto;
        }
        .divClass{
                 margin: auto;
                 padding: 5px;
                 margin-top:10px; 
                 text-align: center;
                 background-image:linear-gradient(to bottom right,whitesmoke,lightblue);
                 /*width: 1200px;*/
        }
        .divClass3{
                 margin: auto;
                 padding: 10px;
                 margin-top:10px;
                 text-align: center;
             }
        body{
             background-color: #2C3E50;
        }
        .border{
            border:2px solid;
            width:100%;
            margin-top:25px;
        }
        th,td{
            font-size:25px;color:darkblue;padding: 10px;
            border:2px solid; width:50%;
        }

    </style>
    <script>
        function ShowPopup(title) {
            $("#myModal .modal-title").html(title);
            $("#myModal").modal("show");
        }
        function drp() {
            $(<%=lstObserverTeam.ClientID%>).SumoSelect();
            $(<%=lstauditee.ClientID%>).SumoSelect();
        }
        function popover() {
            $('#l3').popover({
                placement: 'right',
                trigger: 'manual',
                html: true,
                content: 'Open NC Report'
            });
            $('#l3').popover('show')
        }
        $(document).ready(function () {
            drp();
            //popover();
        });
    </script>
    <title>Audit Details</title>
</head>
<body>
    <nav class="navbar navbar-default" style="padding:10px;background-color:whitesmoke">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#" ><img src="L&t_Logo.jpg" style="height:80px;width:150px"/></a>
            </div><div class="header">
        <h1 style="color: darkblue;"><b>Internal Audit Details </b></h1>
        </div>
            <ul class="nav navbar-nav" style="float:right;">
                <li class="active"><a href="#">Audit Details</a></li>
                <li><a href="WebForm1.aspx">Audit Form</a></li>
                <li><a href="HSE_Surveillance.aspx">HSE Surveillance</a></li>
                <li><a href="login.aspx">Log Out</a></li>
            </ul>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="divClass">
                <div class="row" style="padding:10px">
                    <div class="col-md">
                        <b>Audit No.</b>
                        <div style="margin-top:25px;">
                        <asp:ScriptManager ID="ScriptManager2" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="AuditNo" CssClass="custom-select" runat="server" OnSelectedIndexChanged="AuditNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="AuditNo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="AuditNo" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red" InitialValue="0"/>
                    </div>
                    <div class="col-md">
                        <b>Site </b>
                        <div style="margin-top:25px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="sitename" runat="server" CssClass="custom-select" OnSelectedIndexChanged="sitename_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="sitename" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            </div>
                    </div>
                    <div class="col-md">
                        <b>Observer Team</b>
                        <div style="margin-top:25px;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate> 
                                <asp:ListBox ID="lstObserverTeam" runat="server" SelectionMode="Multiple">
                                </asp:ListBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="lstObserverTeam" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                    </div>
                </div>

                <table>
                    <tr>
                        <th colspan="2"><b>Audit Forms</b></th>
                    </tr>
                    <tr>
                        <th>Audit Form [PART-A]</th>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate> 
                                <asp:LinkButton id="Link1" Text="Click Here" OnClick="Link1_Click" runat="server" CausesValidation="false" Visible="false"/>
                            </ContentTemplate></asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <th>HSE Surveillance [PART-B]</th>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate> 
                                <asp:LinkButton id="Link2" Text="Click Here" OnClick="Link2_Click" CausesValidation="false" runat="server" Visible="false"/>
                            </ContentTemplate></asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <th id="NC">NC Report [PART-C]</th>
                        <td id="l3">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate> 
                                <asp:LinkButton id="Link3" Text="Click Here" OnClick="Link3_Click" CausesValidation="false" runat="server" Visible="false"/>
                            </ContentTemplate></asp:UpdatePanel>
                        </td>
                    </tr>
                </table>

                <div class="row" style="padding:10px;">
                    <div class="col-md">
                        <b id="approved" runat="server" visible="false">Approved</b>
                        <asp:RadioButton Text=" Yes" ID="AppYes" Checked="true" GroupName="App" runat="server" Visible="false"/>
                        <asp:RadioButton Text=" No" ID="AppNo" GroupName="App" runat="server" Visible="false"/>
                    </div>
                </div>
                <%--<div class="row" style="padding:10px;">
                    <div class="col-md">
                        <b id="AP" runat="server" visible="false">Action Plan</b>
                        <input type="text" runat="server" id="actPlan" class="form-control" visible="false"/>
                    </div>
                </div>--%>
            </div>

            <div class="divClass3">
                <asp:Button ID="FrmSubmit" class="btn btn-info" runat="server" Text="Submit" OnClick="FrmSubmit_Click"/>
            </div>

            <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">
            <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="align-self:center">
                        <h4 class="modal-title"></h4>
                    </div>
                    <div class="modal-body" style="align-self:center">
                        <asp:ListBox ID="lstauditee" runat="server" SelectionMode="Multiple">
                                </asp:ListBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="modalButt" runat="server" Text="Okay" CssClass="btn btn-info" OnClick="modalButt_Click"/>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
