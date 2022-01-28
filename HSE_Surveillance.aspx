<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSE_Surveillance.aspx.cs" Inherits="Project2021.L_T_EmpLogin" %>

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
                 padding: 10px;
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
    </style>
    <title>L&T HSE Surveillance Rating Annexure</title>
    <script>
        $(function () {
            $("#datepicker").datepicker();
        });
        function showContent() {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "progressBar": true,
                "preventDuplicates": false,
                "positionClass": "toast-bottom-right",
                "showDuration": "2500",
                "hideDuration": "1000",
                "timeOut": "7000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr["error"]("Please Fill all the fields!!");
        }
        function ShowPopup(title) {
            $("#myModal .modal-title").html(title);
            $("#myModal").modal("show");
        }
        $(document).ready(function () {
            var val = $('#<%=hdnjson.ClientID%>').val();
             var data = JSON.parse(val);
             $("#grid").kendoGrid({
                 columns: [
                     {
                         field: "Id", title: "Id", width: 50,
                         attributes: {
                             " class": "# if(data.Group_no.includes('-') == true) { # grey # } else if(data.Group_no.includes('Group') == true) {# teal #}  #"
                         }
                     }, {
                         field: "Group_no",
                         title: "Group", width: 90,
                         attributes: {
                             " class": "# if(data.Group_no.includes('-') == true) { # grey # } else if(data.Group_no.includes('Group') == true) {# teal #}  #"
                         }
                     },
                     {
                         field: "Question",
                         title: "Question",
                         attributes: {
                             " class": "# if(data.Group_no.includes('-') == true) { # grey # } else if(data.Group_no.includes('Group') == true) {# teal #}  #"
                         }
                     },
                     {
                         field: "Score",
                         title: "Score", width: 90,
                         attributes: {
                             " class": "# if(data.Group_no.includes('-') == true) { # grey # } else if(data.Group_no.includes('Group') == true) {# teal #}  #"
                         }
                     },
                     {
                         field: "Remarks",
                         title: "Remarks", width: 200,
                         attributes: {
                             " class": "# if(data.Group_no.includes('-') == true) { # grey # } else if(data.Group_no.includes('Group') == true) {# teal #}  #"
                         }
                     }],
                 dataSource: {
                     data: data,
                     pageSize: 20,
                     schema: {
                         model: {
                             fields: {
                                 Group_no: { editable: false },
                                 Question: { editable: false },
                                 Remarks: {
                                     type: "string", validation: {
                                         required: true
                                     }
                                 },
                                 Score: {
                                     type: "number", validation: {
                                         required: true, min: 0,
                                         validateTitle: function (input) {
                                             if (input.val() > 2) {
                                                 input.attr("data-validateTitle-msg", "Max Score is 2!");
                                                 return false;
                                             }
                                             return true;
                                         }
                                     }
                                 }
                             }
                         }
                     },
                 },
                 editable: "incell",
                 edit: function (e) {
                     var grp = e.model.Group_no.includes('Group');
                     var SubQue = e.model.Group_no.includes('-');
                     if (grp == true || SubQue == true) {
                         var grid1 = $("#grid").data("kendoGrid");
                         grid1.closeCell();
                     }
                 },
                 pageable: true,
                 scrollable: true,
             });
             var grid = $("#grid").data("kendoGrid");
            grid.saveChanges();


            $("#FrmSubmit").click(function (e) {
                var temp = $("#grid").data("kendoGrid").dataSource.data();
                var griddata = [];
                $(temp).each(function (key, val) {
                    //var no = val.Group_no;
                    //if (no.includes('-') || no.includes('Group')) {
                    //}
                    //else {
                    //    if (val.Remarks == undefined || val.Score == null) {
                    //        showContent();
                    //        e.preventDefault();
                    //        return false;
                    //    }
                    //}
                    if (val.Remarks != undefined) {
                        griddata.push({ "Id": val.Id, "Score": val.Score, "Reamrks": val.Remarks });
                    }
                });
                document.getElementById('<%=hdnfld1.ClientID%>').value = JSON.stringify(griddata);
                for (idx = 0; idx < temp.length; idx++) {
                    if (temp[idx].dirty) {
                        document.getElementById('<%=hdn_gridChanges.ClientID%>').value = "true";
                    }
                }
            });
        });

        
    </script>
</head>
<body>
    <nav class="navbar navbar-default" style="padding:10px;background-color:whitesmoke">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#" ><img src="L&t_Logo.jpg" style="height:80px;width:150px"/></a>
            </div><div class="header">
        <h1 style="color: darkblue;"><b>L&T HSE Surveillance Rating Annexure</b></h1>
        </div>
            <ul class="nav navbar-nav" style="float:right;">
                <li class="active"><a href="#">HSE Surveillance</a></li>
                <li><a href="WebForm1.aspx">Audit Form</a></li>
                <li><a href="NCReport.aspx">NC Report</a></li>
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
                                    <asp:DropDownList ID="AuditNo" CssClass="custom-select" runat="server" OnSelectedIndexChanged="AuditNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="AuditNo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="AuditNo" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red" InitialValue="0"/>
                    </div>
                </div>
                    <div class="col-md">
                        <b>IC / BU / SBG:</b>
                        <div class="md-form md-outline">
                            <label class="form-label" for="ic" style="font-size:15px;">IC / BU / SBG</label>
                            <input type="text" runat="server" id="ic" class="form-control" value="L&T Power IC"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ic" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                    </div>
                    <div class="col">
                        <b>Site / Facility :</b>
                        <div style="margin-top:25px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="sitename" runat="server" CssClass="custom-select">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="sitename" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red" InitialValue="0"/>
                        </div>
                    </div>
                    <div class="col">
                        <b>Date</b>
                        <div id="date-picker-example" class="md-form md-outline input-with-post-icon datepicker">
                            <label class="form-label" for="datepicker" style="font-size:15px;">Date</label>
                            <input type="text" runat="server" id="datepicker" class="form-control"/>
                            <i class="fas fa-calendar input-prefix" tabindex="0"></i>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="datepicker" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                    </div>
            </div>
            <b style="padding:10px;color:darkblue;">HSE SURVEILLANCE RATING (HSR)</b>
            <div id="grid" runat="server"> </div>
            <asp:HiddenField runat="server" ID="hdnjson" />
                <asp:HiddenField runat="server" ID="hdnfld1" />
                <asp:HiddenField runat="server" ID="hdn_gridChanges" />
       </div> 

        <div class="divClass3">
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
