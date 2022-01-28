<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Project2021.WebForm1" %>

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


        <script>
            $( function() {
                $( "#datepicker" ).datepicker();
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
                    "extendedTimeOut": "1500",
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
        </script>
    <script>
        i = 1;
        i1 = 1;
        i2 = 1;
        i3 = 1;
        function newinput() {
            i++;
            var newdata = '<div id="newinput"><input type="text" id="strengths' + i + '"/> ' +
                '<input type="button" id="rmvstrenghts' + i + '"  value="-" onclick="rmvinput()" class="btn btn-sm btn-light"/></div>';
            $('#div1').append(newdata);
        }
        function rmvinput() {
            i--;
            $('#newinput').remove();
        }
        function newinput1() {
            i1++;
            var newdata = '<div id="newinput1"><input type="text" id="majorNC' + i1 + '"/> ' +
                '<input type="button" id="rmvstrenghts' + i1 + '"  value="-" onclick="rmvinput1()" class="btn btn-sm btn-light"/></div>';
            $('#div2').append(newdata);
        }
        function rmvinput1() {
            i1--;
            $('#newinput1').remove();
        }
        function newinput2() {
            i2++;
            var newdata = '<div id="newinput2"><input type="text" id="minorNC' + i2 + '"/> ' +
                '<input type="button" id="rmvstrenghts' + i2 + '"  value="-" onclick="rmvinput2()" class="btn btn-sm btn-light"/></div>';
            $('#div3').append(newdata);
        }
        function rmvinput2() {
            i2--;
            $('#newinput2').remove();
        }
        function newinput3() {
            i3++;
            var newdata = '<div id="newinput3"><input type="text" id="OFIs' + i3 + '"/> ' +
                '<input type="button" id="rmvstrenghts' + i3 + '"  value="-" onclick="rmvinput3()" class="btn btn-sm btn-light"/></div>';
            $('#div4').append(newdata);
        }
        function rmvinput3() {
            i3--;
            $('#newinput3').remove();
        }
        function drp() {
            $("#sitename").select2();
            $(<%=lstAuditTeam.ClientID%>).SumoSelect();
            $(<%=lstObserverTeam.ClientID%>).SumoSelect();
        }
    </script>
    <title>Audit Form</title>
    <style>
             table.center{
                 margin-left: auto;
                 margin-right: auto;
             }
             hr.solid {
                border-top: 2px solid #bbb;
             }
             td{
                 padding: 5px;
                 color:darkblue;
             }
             body{
                 font-size: 16px;
                 background-color: #2C3E50;
             }
             .divClass{
                 border: 2px outset;
                 margin: auto;
                 padding: 10px;
                 margin-top:10px; 
                 text-align: center;
                 background-image:linear-gradient(to bottom right,whitesmoke,lightblue);
                 /*width: 1200px;*/
             }
             .divClass2{
                 border: 2px outset;
                 margin: auto;
                 padding: 10px;
                 margin-top:10px;
                 background-color: whitesmoke;
                 color: whitesmoke;
             }
             .divClass3{
                 margin: auto;
                 padding: 10px;
                 margin-top:10px;
                 text-align: center;
             }
             h4,h2{
                 text-align: center;
                 font-size: 20px;
             }
             .bottom{
                 width: 35px;
                 height: 35px;
                 border-radius: 50%;
                 position: fixed;
                 top: 91%;
                 left: 98%;
                 background-color:lightblue;
             }
             .header {
                overflow: hidden;
                padding: 20px 10px;
             }
             .header h1{
                 text-align:center;
             }
             .header img {
                 float: left;
                 width: 200px;
                 height: 70px;
             }
             .critical {
        background-color: #fdd;
      }
            .arrow {
  text-align: center;
  margin: 8% 0;
}
.bounce {
  -moz-animation: bounce 2s infinite;
  -webkit-animation: bounce 2s infinite;
  animation: bounce 2s infinite;
}

@keyframes bounce {
  0%, 20%, 50%, 80%, 100% {
    transform: translateY(0);
  }
  40% {
    transform: translateY(-30px);
  }
  60% {
    transform: translateY(-15px);
  }
}
        </style>
    <script>
        $(document).ready(function () {
            
            drp();
            var val = $('#<%=hdnjson.ClientID%>').val();
            var data = JSON.parse(val);

            $("#grid").kendoGrid({
                columns: [{
                    field: "Id",
                    title: "Sl.No.",
                    width: 30, attributes: {
                        " class": "# if(Number.isInteger(data.Ques_no)) { # grey # }  #"
                    }
                },
                {
                    field: "Ques_no",
                    title: "Ques. No.",
                    width: 40,
                    attributes: {
                        " class": "# if(Number.isInteger(data.Ques_no)) { # grey # }   #"
                    }
                },
                {
                    field: "Questions",
                    title: "Details",
                    width: 200, attributes: {
                        " class": "# if(Number.isInteger(data.Ques_no)) { # grey # }  #"
                    }

                },
                {
                    field: "Observation",
                    title: "Observation",
                    width: 200, attributes: {
                        " class": "# if(Number.isInteger(data.Ques_no)) { # grey # }  #"
                    }
                },
                {
                    field: "Score",
                    title: "Score",
                    width: 50,
                    attributes: {
                        " class": "# if(Number.isInteger(data.Ques_no)) { # grey # }  #"
                    }
                }
                ],
                //sortable: true,
                dataSource: {
                    data: data,
                    schema: {
                        model: {
                            fields: {
                                Id: { editable: false },
                                Ques_no: { editable: false },
                                Questions: { editable: false },
                                Observation: {
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
                     if (Number.isInteger(e.model.Ques_no)) {
                         var grid1 = $("#grid").data("kendoGrid");
                         grid1.closeCell();
                     }
                 },
             });
            var grid = $("#grid").data("kendoGrid");
            grid.saveChanges();
            
            $("#down").click(function () {
                $("html, body").animate({
                    scrollTop: $(
                        'html, body').get(0).scrollHeight
                }, 1500);
                $(".bottom").remove();
            });

            $("#FrmSubmit").click(function (e) {
                
                var arr = [];
                var arr1 = [];
                var arr2 = [];
                var arr3 = [];
                var x = $("[id^='strengths']");
                for (var a = 0; a < x.length; a++) {
                    arr.push($(x)[a].value);
                }
                var x1 = $("[id^='majorNC']");
                for (var a1 = 0; a1 < x1.length; a1++) {
                    arr1.push($(x1)[a1].value);
                }
                var x2 = $("[id^='minorNC']");
                for (var a2 = 0; a2 < x2.length; a2++) {
                    arr2.push($(x2)[a2].value);
                }
                var x3 = $("[id^='OFIs']");
                for (var a3 = 0; a3 < x3.length; a3++) {
                    arr3.push($(x3)[a3].value);
                }
                document.getElementById('<%=hdn_strengths.ClientID%>').value = arr;
                document.getElementById('<%=hdn_majorNC.ClientID%>').value = arr1;
                document.getElementById('<%=hdn_minorNC.ClientID%>').value = arr2;
                document.getElementById('<%=hdn_OFIs.ClientID%>').value = arr3;

                //getting data of a grid those are having a observation filled.
                var temp = $("#grid").data("kendoGrid").dataSource.data();
                var griddata = [];
                
                $(temp).each(function (key, val) {
                    var no = val.Ques_no;
                    //if (!Number.isInteger(no)) {
                    //    if (val.Observation == undefined || val.Score == null) {
                    //        showContent();
                    //        e.preventDefault();
                    //        return false;
                    //    }
                    //}
                    if (val.Observation != undefined) {
                        griddata.push({ "Id": val.Id, "Observation": val.Observation, "Score": val.Score });
                    }
                });
                document.getElementById('<%=hdnfld1.ClientID%>').value = JSON.stringify(griddata);

                //Check if there are any changes made in the kendo grid.
                for (idx = 0; idx < temp.length; idx++) {
                    if (temp[idx].dirty) {
                        document.getElementById('<%=hdn_gridChanges.ClientID%>').value = "true";
                    }
                }
            });
            <%--$(<%=lstAuditTeam.ClientID%>).SumoSelect({ okCancelInMulti: true });--%>
            
        });
    </script>
</head>
<body>
    <div class="bottom">
        <div class="arrow bounce">
            <a id="down" class="fa fa-arrow-down fa-2x"></a>
        </div>
    </div>
    <%--<img id="down" src="scroll_down.png" class="bottom"/> --%>
    <nav class="navbar navbar-default" style="padding:10px;background-color:whitesmoke">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#"><img src="L&t_Logo.jpg" style="height:80px;width:150px"/></a>
            </div><div class="header">
        <h1 style="color: darkblue;"><b>Internal Audit Evaluation Format</b></h1>
        </div>
            <ul class="nav navbar-nav" style="float:right;">
                <li class="active"><a href="#">Audit Form</a></li>
                <li><a href="Audit.aspx">Dashboard</a></li>
                <li><a href="HSE_Surveillance.aspx">HSE Surveillance</a></li>
                <li><a href="NCReport.aspx">NC Report</a></li>
                <li><a href="login.aspx">Log Out</a></li>
            </ul>
        </div>
    </nav>
    
    <div class="container-fluid">
        
    <form id="form1" runat="server">
        <div>
            <div class="divClass">

                <div class="row" style="padding:5px;">
                    <div class="col">
                        <b>Date of Audit</b>
                        <div id="date-picker-example" class="md-form md-outline input-with-post-icon datepicker">
                            <label class="form-label" for="datepicker" style="font-size:15px;">Date</label>
                            <input type="text" runat="server" id="datepicker" class="form-control"/>
                            <i class="fas fa-calendar input-prefix" tabindex="0"></i>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="datepicker" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                    </div>
                    <div class="col">
                        <b>Name of the Site</b>
                        <div style="margin-top:24px;">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>  
                                    <asp:DropDownList ID="sitename" runat="server" CssClass="custom-select" OnSelectedIndexChanged="sitename_SelectedIndexChanged" AutoPostBack="true" >
                                </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="sitename" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="sitename" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red" InitialValue="0"/>
                        </div>
                    </div>
                    <div class="col">
                        <b>Details of the Site</b>
                        <div class="md-form md-outline">
                            <label class="form-label" for="sitedetails" style="font-size:15px;">Details</label>
                            <textarea id="sitedetails" runat="server" class="form-control" rows="3"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="sitedetails" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        </div>
                    </div>
                <%--</div>
                
                <div class="row">--%>
                    <div class="col">
                        <p style="font-size:15px;margin-bottom:25px"><b>Audit Team</b></p>
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate> 
                                <asp:ListBox ID="lstAuditTeam" runat="server" SelectionMode="Multiple">
                                </asp:ListBox>
                            </ContentTemplate>
                         </asp:UpdatePanel>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="lstAuditTeam" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                    </div>
                    <div class="col">
                        <p style="font-size:15px;margin-bottom:25px"><b>Observer Team</b></p>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate> 
                                <asp:ListBox ID="lstObserverTeam" runat="server" SelectionMode="Multiple">
                                </asp:ListBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="lstObserverTeam" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                    </div>
                </div>
            
            <%--<hr />--%>
            <h4>Audit Results </h4>
            <br />
                <div class="row">
                    <div class="col">
                         Strengths (Positive Points)  &nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="strengths1" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        <div id="div1" class="md-form md-outline">
                            <input runat="server" type="text" id="strengths1" style="border-width:2px"/>
                            <input type="button" id="strenghts"  value="+" onclick="newinput()" class="btn btn-sm btn-light" />
                            
                        </div>
                        <asp:HiddenField ID="hdn_strengths" runat="server" />
                    </div>
                    <div class="col">
                         Major Non-Compliance (Major NC)  &nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="majorNC1" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        <div id="div2" class="md-form md-outline">
                            <input runat="server" type="text" id="majorNC1" style="border-width:2px"/>
                            <input type="button" id="ma_nc_butt" value="+" onclick="newinput1()" class="btn btn-sm btn-light"/>
                    
                        </div>
                        <asp:HiddenField ID="hdn_majorNC" runat="server" />
                    </div>
                <%--</div>
                <div class="row">--%>
                    <div class="col">
                        Minor Non-Compliance (Minor NC)  &nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="minorNC1" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        <div id="div3" class="md-form md-outline">
                            <input runat="server" type="text" id="minorNC1" style="border-width:2px"/>
                            <input type="button" id="mi_nc_butt" value="+" onclick="newinput2()" class="btn btn-sm btn-light"/>
                    
                        </div>
                        <asp:HiddenField ID="hdn_minorNC" runat="server" />
                    </div>
                    <div class="col">
                        Opportunity for Improvement (OFIs) &nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="OFIs1" Display="Dynamic" ErrorMessage="Required!" runat="server" ForeColor="Red"/>
                        <div id="div4" class="md-form md-outline">
                            <input runat="server" type="text" id="OFIs1" style="border-width:2px"/>
                            <input type="button" id="ofis" value="+" onclick="newinput3()" class="btn btn-sm btn-light"/>
                    
                        </div>
                        <asp:HiddenField ID="hdn_OFIs" runat="server" />
                    </div>
                </div>
             <hr />
                <asp:HiddenField runat="server" ID="hdn_gridChanges" />
                
            <div id="grid" runat="server">
            </div>
            <asp:HiddenField runat="server" ID="hdnjson" />

            <asp:HiddenField runat="server" ID="hdnfld1" />
            </div>
            
           <div class="divClass3" >
               <asp:Button ID="FrmSubmit" class="btn btn-info" runat="server" Text="submit" OnClick="Button1_Click1"/>
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
      </div>
    </form>
   </div>
</body>
</html>