<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kendo.aspx.cs" Inherits="Project2021.Kendo" %>

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
    <!-- Kendo Grid -->      
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.224/styles/kendo.default-v2.min.css"/>
        <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.119/styles/kendo.common.min.css" />
        <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.119/styles/kendo.default.min.css" />
        <script src="https://kendo.cdn.telerik.com/2021.1.119/js/kendo.all.min.js"></script>
     <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/2.3.2/css/bootstrap.min.css" rel="stylesheet" />
    
 <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <style>
        #grid {
            /*width:95%;*/
            margin:20px;
        }
    </style>
    <script>
        function showContent() {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "progressBar": true,
                "preventDuplicates": false,
                "positionClass": "toast-bottom-right",
                "showDuration": "1400",
                "hideDuration": "1000",
                "timeOut": "7000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr["error"]("This is a message");

        }
    </script>
        <title>Supplier Interface System</title>
     <script>
         
         $(document).ready(function () {
             //alert('hi')
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
                    },
                    //{ 'command': { text: 'Edit', click: RemoveHeaderFooter, iconClass: 'k-icon k-i-pencil' }, 'title': ' ', 'width': '80px' }
                ],
                dataSource: {
                    data: data,
                    //serverPaging: true,
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
         });
         
         function RemoveHeaderFooter() {
             $('#print').css("display", "none");
             $('#<%=Upload.ClientID%>').css("display", "none");
                $('.navbar').css("display", "none");
                $('#footer').css("display", "none");
                kendo.drawing.drawDOM($("body"))
                    .then(function (group) {
                        // Render the result as a PDF file
                        return kendo.drawing.exportPDF(group, {
                            paperSize: "auto",
                            margin: { left: "1cm", right: "1cm" },
                            landscape: true
                        });
                    })
                    .done(function (data) {
                        // Save the PDF file
                        kendo.saveAs({
                            dataURI: data,
                            fileName: "dhvani"
                        });
                        $("export-pdf").show();
                        $('#print').css("display", "block");
                        $('#<%=Upload.ClientID%>').css("display", "block");
                        $('.navbar').css("display", "block");
                        $('#footer').css("display", "block");
                    });
         }
         $("#Button3").click(function (e) {
             $("#button").click();
         });
         //$("#butt").click(function (e) {
         function see() {
             RemoveHeaderFooter();
             //var temp = $("#grid").data("kendoGrid").dataSource.data();

             //$(temp).each(function (key, val) {
             //    var a = val.Remarks;
             //    if (val.Group_no.includes('-') == true) {
             //        //alert('Hi' + a);
             //        console.log(val.Group_no);
             //    }
             //});
         }
         //});
     </script>
</head>
<body>
   
   
    <form runat="server">
         <div id="grid" runat="server"> </div>
         <asp:HiddenField runat="server" ID="hdnjson" />

         <asp:Button ID="Button1" runat="server" Text="PDF" OnClick="Button1_Click"/>
        <asp:Button ID="Button4" runat="server" Text="PDF2" OnClick="Button4_Click"/>
         <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Mail" />
         <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
        <input type="button" id="butt" value="see" onclick="see()"/><br />
        <asp:HiddenField runat="server" ID="Upload" />
        <asp:Image ID="imgScreenShot" runat="server" Visible = "false" />
    </form>
</body>
</html>
