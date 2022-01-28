<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NCReport.aspx.cs" Inherits="Project2021.NCReport" %>

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
        
        td{
            padding: 5px;
            color:darkblue;
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
            font-size:20px;
        }
        .lbl{
            text-align:center;margin-top:15px;width:100%;
            /*background-color:whitesmoke;*/
        }
        .lbl_left{
            text-align:left;margin-top:15px;width:100%;
        }
        .form-control{
            border:1px solid grey;
        }
    </style>
    <script>
        $(function () {
            $("#aperiod").datepicker();
            $("#proposedDate").datepicker();
            $("#actualDate").datepicker(); 
            $("#Date").datepicker(); 
        });
        function drp() {
            //$("#sitename").select2();
            $(<%=lstObserverTeam.ClientID%>).SumoSelect();
        }
        function ShowPopup(title) {
            $("#myModal .modal-title").html(title);
            $("#myModal").modal("show");
        }
        $(document).ready(function () {
            drp();
        });
    </script>
    <title>Non Conformance Report</title>
</head>
<body>
    <nav class="navbar navbar-default" style="padding:10px;background-color:whitesmoke">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#" ><img src="L&t_Logo.jpg" style="height:80px;width:150px"/></a>
            </div><div class="header">
        <h1 style="color: darkblue;"><b>Internal Audit Non Conformance Report</b></h1>
        </div>
            <ul class="nav navbar-nav" style="float:right;">
                <li class="active"><a href="#">NC Report</a></li>
                <li><a href="WebForm1.aspx">Audit Form</a></li>
                <li><a href="HSE_Surveillance.aspx">HSE Surveillance</a></li>
                <li><a href="login.aspx">Log Out</a></li>
            </ul>
        </div>
    </nav>
    <div class="container-fluid">
    <form id="form1" runat="server">
        <div class="divClass">
            <div class="row" style="padding:10px">
                <div class="col-md">
                    <b>Audit No.</b>
                    <div style="margin-top:25px;">
                        <asp:ScriptManager ID="ScriptManager2" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList style="border:1px solid grey;" ID="AuditNo" CssClass="custom-select" runat="server" OnSelectedIndexChanged="AuditNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="AuditNo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="AuditNo" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red" InitialValue="0"/>
                        <%--<asp:DropDownList ID="AuditNo" CssClass="custom-select" runat="server"></asp:DropDownList>--%>
                    </div>
                </div>
                <div class="col-md">
                    <b>Project Name</b>
                    <div style="margin-top:25px;">
                        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="sitename" runat="server" CssClass="custom-select" style="border:1px solid grey;" OnSelectedIndexChanged="sitename_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="sitename" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="sitename" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red" InitialValue="0"/>
                    </div>
                </div>
                <div class="col-md">
                    <b>Department</b>
                        <div class="md-form md-outline">
                            <label class="form-label" for="department" style="font-size:15px;">Department</label>
                            <input type="text" runat="server" id="department" class="form-control" style="border:1px solid grey;"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="department" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                </div>
                </div>
            <div class="row" style="padding:10px">
                <div class="col-md">
                    <b>Auditee Name(s)</b>
                        <div class="md-form md-outline">
                            <label class="form-label" for="auditee" style="font-size:15px;">Auditee Name(s)</label>
                            <input type="text" runat="server" id="auditee" class="form-control" style="border:1px solid grey;"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="auditee" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                </div>
            <%--</div>--%>

            <%--<div class="row" style="padding:10px">--%>
                <div class="col-md">
                    <b>Audit NC No.</b>
                        <div class="md-form md-outline">
                            <label class="form-label" for="ANCNo" style="font-size:15px;">Audit NC No.</label>
                            <input type="text" runat="server" id="ANCNo" class="form-control" style="border:1px solid grey;"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ANCNo" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                </div>
                <div class="col-md">
                    <b>Process</b>
                        <div class="md-form md-outline">
                            <label class="form-label" for="process" style="font-size:15px;">Process</label>
                            <textarea id="process" runat="server" class="form-control" rows="3"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="process" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                </div>
                </div>

            <div class="row" style="padding:10px">
                <div class="col-md">
                    <b>Auditor Name(s)</b>
                        <div class="md-form md-outline">
                            <label class="form-label" for="auditorName" style="font-size:15px;">Auditor Name(s)</label>
                            <input type="text" runat="server" id="auditorName" class="form-control" style="border:1px solid grey;"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="auditorName" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                </div>
            <%--</div>

            <div class="row" style="padding:10px">--%>
                <div class="col-md">
                    <b>Audit Period</b>
                        <div id="date-picker-example" class="md-form md-outline input-with-post-icon datepicker">
                            <label class="form-label" for="aperiod" style="font-size:15px;">Audit Period</label>
                            <input type="text" runat="server" id="aperiod" class="form-control" style="border:1px solid grey;"/>
                            <i class="fas fa-calendar input-prefix" tabindex="0"></i>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="aperiod" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                </div>
                <div class="col-md">
                    <b>Applicable Standard and Clause</b>
                        <div class="md-form md-outline">
                            <label class="form-label" for="clause" style="font-size:15px;">Applicable Standard and Clause</label>
                            <textarea id="clause" runat="server" class="form-control" rows="1"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="clause" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                </div>
                </div>

            <div class="row" style="padding:10px">
                <div class="col-md">
                    <b>Observer Team</b>
                    <%--<div style="margin-top:24px;">--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate> 
                                <asp:ListBox ID="lstObserverTeam" runat="server" SelectionMode="Multiple">
                                </asp:ListBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="lstObserverTeam" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red" />
                    <%--</div>--%>
                </div>
            <%--</div>

            <div class="row" style="padding:10px">--%>
                <div class="col-md">
                    <b>Major</b>
                    <div class="form-check">
                        <asp:RadioButton Text=" Yes" ID="MajorYes" Checked="true" GroupName="Major" runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton Text=" No" ID="MajorNo" GroupName="Major" runat="server" />
                    </div>
                </div>
                <div class="col-md">
                    <b>Minor</b>
                    <div class="form-check">
                        <asp:RadioButton Text=" Yes" ID="MinorYes" Checked="true" GroupName="Minor" runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton Text=" No" ID="MinorNo" GroupName="Minor" runat="server" />
                    </div>
                </div>
                <%--<div class="col-md">
                    <b>Other Documents (if applicable)</b>
                </div>--%>
            </div>

            <label class="lbl"><b>Requirement of Audited Standard</b></label>
            <div class="md-form md-outline">
                 <label class="form-label" for="req" style="font-size:15px;">Requirement</label>
                 <textarea id="req" runat="server" class="form-control" rows="5" style="border-color:slategray;"></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="req" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
            </div>

            <label class="lbl"><b>Observed NonConformity</b></label>
            <div class="md-form md-outline">
                 <label class="form-label" for="ObservedNC" style="font-size:15px;">Observed NonConformity</label>
                 <textarea id="ObservedNC" runat="server" class="form-control" rows="2" style="border-color:slategray;"></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ObservedNC" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
            </div>

            <label class="lbl_left"><b>The objective evidence on which the non conformity is based.</b></label>
            <div class="md-form md-outline">
                 <label class="form-label" for="evidence" style="font-size:15px;">Objective evidence</label>
                 <textarea id="evidence" runat="server" class="form-control" rows="1" style="border-color:slategray;"></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="evidence" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
            </div>

            <table class="border">
                <tr>
                    <th colspan="3" style="border:2px solid;"><b>Root Cause Analysis and Corrective Action</b></th>
                </tr>
                <tr>
                    <th style="border:2px solid;"><b>Proposed Completion Date</b></th>
                    <th style="border:2px solid;"><b>Actual Completion Date</b></th>
                    <th style="border:2px solid;"><b>Responsible Person</b></th>
                </tr>
                <tr>
                    <td style="border:2px solid;">
                        <div class="md-form md-outline input-with-post-icon datepicker">
                            <label class="form-label" for="proposedDate" style="font-size:15px;">Proposed Completion Date</label>
                            <input type="text" runat="server" id="proposedDate" class="form-control" style="border:1px solid grey;"/>
                            <i class="fas fa-calendar input-prefix" tabindex="0"></i>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="proposedDate" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                    </td>
                    <td style="border:2px solid;">
                        <div class="md-form md-outline input-with-post-icon datepicker">
                            <label class="form-label" for="actualDate" style="font-size:15px;">Actual Completion Date</label>
                            <input type="text" runat="server" id="actualDate" class="form-control" style="border:1px solid grey;"/>
                            <i class="fas fa-calendar input-prefix" tabindex="0"></i>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="actualDate" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                    </td>
                    <td style="border:2px solid;">
                        <div class="md-form md-outline">
                            <label class="form-label" for="resPerson" style="font-size:15px;">Responsible Person</label>
                            <input type="text" runat="server" id="resPerson" class="form-control" style="border:1px solid grey;"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="resPerson" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                    </td>
                </tr>
            </table>

            <label class="lbl"><b>Root Cause Analysis and Corrective Action Response</b></label>
            <label style="text-align:left;width:100%;">Correction (for what needs immediate correction)</label>
            <div class="md-form md-outline">
                 <label class="form-label" for="correction" style="font-size:15px;">Correction</label>
                 <textarea id="correction" runat="server" class="form-control" rows="1" ></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="correction" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
            </div>
            <label style="text-align:left;width:100%;">Root Cause Analysis</label>
            <div class="md-form md-outline">
                 <label class="form-label" for="rootCause" style="font-size:15px;">Root Cause Analysis</label>
                 <textarea id="rootCause" runat="server" class="form-control" rows="1" ></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="rootCause" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
            </div>
            <label style="text-align:left;width:100%;">Corrective Action Response</label>
            <div class="md-form md-outline">
                 <label class="form-label" for="Actionresponse" style="font-size:15px;">Corrective Action Response</label>
                 <textarea id="Actionresponse" runat="server" class="form-control" rows="1" ></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="Actionresponse" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
            </div>
            <label class="lbl_left"><b>The evidence to support the resolution of nonconformities shall be recorded</b></label>
            <div class="md-form md-outline">
                 <label class="form-label" for="EviNC" style="font-size:15px;">Evidence</label>
                 <textarea id="EviNC" runat="server" class="form-control" rows="1" ></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="EviNC" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
            </div>

            <table class="border">
                <tr>
                    <th colspan="4" style="border:2px solid;"><b>Clearance Report</b></th>
                </tr>
                <tr>
                    <td>Corrective Action Accepted</td>
                    <td>
                        <asp:RadioButton Text=" Yes" ID="CAAYes" Checked="true" GroupName="CAA" runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton Text=" No" ID="CAANo" GroupName="CAA" runat="server" />
                    </td>
                    <td>Non-Conformance Downgraded</td>
                    <td>
                        <asp:RadioButton Text=" Yes" ID="NCDYes" Checked="true" GroupName="NCD" runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton Text=" No" ID="NCDNo" GroupName="NCD" runat="server" />
                    </td>
                </tr>
            </table>
            <label class="lbl_left"><b>Follow Up Comments</b></label>
            <div class="md-form md-outline">
                 <label class="form-label" for="comments" style="font-size:15px;">Comments</label>
                 <textarea id="comments" runat="server" class="form-control" rows="1" ></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="comments" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
            </div>

            <div class="row" style="padding:10px;">
                <div class="col-2" style="text-align:center;padding-top:30px;">
                    <b>Auditor</b>
                </div>
                <div class="col-4" >
                    <div class="md-form md-outline">
                        <label class="form-label" for="auditor" style="font-size:15px;">Auditor</label>
                        <input type="text" runat="server" id="auditor" class="form-control" style="border:1px solid grey;" value="Dhvani Golani"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="auditor" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                    </div>
                </div>
                <div class="col-2" style="text-align:center;padding-top:30px;">
                    <b>Date</b>
                </div>
                <div class="col-4" >
                    <div class="md-form md-outline input-with-post-icon datepicker">
                        <label class="form-label" for="Date" style="font-size:15px;">Date</label>
                        <input type="text" runat="server" id="Date" class="form-control" style="border:1px solid grey;"/>
                        <i class="fas fa-calendar input-prefix" tabindex="0"></i>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="Date" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                    </div>
                </div>
            </div>
        </div>

        <div class="divClass3" >
            <asp:Button ID="FrmSubmit" class="btn btn-info" runat="server" Text="Submit" OnClick="FrmSubmit_Click"/>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">
            <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" >
                        <h4 class="modal-title"></h4>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="modalButt" runat="server" Text="Okay" CssClass="btn btn-info" OnClick="modalButt_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    </div>
</body>
</html>
