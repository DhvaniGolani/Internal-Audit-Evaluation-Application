<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Role_Dashboard.aspx.cs" Inherits="Project2021.Role_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- JQuery -->
    <!-- Bootstrap tooltips -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.4/umd/popper.min.js"></script>
    <!-- Bootstrap core JavaScript -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>


    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.119/styles/kendo.common.min.css" />
    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.119/styles/kendo.default.min.css" />
    <script src="https://kendo.cdn.telerik.com/2021.1.119/js/kendo.all.min.js"></script>
    <title>Dashboard</title>
    <style>
        body{
            text-align:center;
        }
        .k-header .k-link{
            text-align: center;
        }
        #grid {
            /*width:95%;*/
            margin:30px;
        }
    </style>
    <script>
        function edit(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            document.getElementById('<%=hdn_get_ID.ClientID%>').value = dataItem.Id;
            //console.log('ID: ' + dataItem.Id);
            window.location = "Role.aspx?id=" + dataItem.Id;
        };
        function remove(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            document.getElementById('<%=HiddenField1.ClientID%>').value = dataItem.Id;
            document.getElementById('<%=PSno.ClientID%>').value = dataItem.PSNO;
            $('#<%=deleting.ClientID%>').click();
            //var grid = $("#grid").data("kendoGrid");
            //grid.removeRow($(e.currentTarget).closest("tr"));
        } 
        function ShowPopup(title, body) {
            $("#myModal .modal-title").html(title);
            $("#myModal .modal-body").html(body);
            $("#myModal").modal("show");
        }
        function callOnLoad() {
            var val = $('#<%=hdnjson.ClientID%>').val();
            var data = JSON.parse(val);
            $("#grid").kendoGrid({
                columns: [{
                    field: "Id",
                    title: "Id"
                },
                {
                    field: "PSNO",
                    title: "PSNO"
                },
                {
                    field: "Rolename",
                    title: "Rights"
                },
                //{ 'command': { text: "Edit", click: showDetails }, 'title': ' ', 'width': '80px' },
                { command: [{ className: "btn-edit", name: "edit", text: "Edit", click: edit }] },
                { command: [{ className: "btn-destroy", name: "delete", text: "Delete", click: remove, iconClass: 'k-icon k-i-delete' }] }
                ],
                dataSource: {
                    data: data
                },
                //dataBound: onDataBound,
                filterable: true,
                columnMenu: true,
                reorderable: true,
                sortable: true
            });
        }
        //$(document).ready(function () {
            
        //});
    </script>
</head>
<body>
    <nav class="navbar navbar-default" style="padding: 10px">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">
                    <label style="color: darkblue;">L&T Power</label></a>
            </div>
            <ul class="nav navbar-nav" style="float: right;">
                <li class="active"><a href="#">Dashboard</a></li>
                <li><a href="Role.aspx">Role</a></li>
                <li><a href="WebForm1.aspx">Audit Form</a></li>
            </ul>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div>
            
            <div id="grid" runat="server"> </div>
            <asp:HiddenField runat="server" ID="hdnjson" />
            <asp:HiddenField runat="server" ID="hdn_get_ID" />
            <asp:HiddenField runat="server" ID="HiddenField1" />
            <asp:HiddenField runat="server" ID="PSno" />
            <asp:Button ID="deleting" runat="server" OnClick="deleting_Click" style="display:none;"/>
            <%--<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="display:none;"/>--%>
        </div>

        <div class="container">

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>--%>
            <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" >
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"></h4>
                    </div>
                    <div class="modal-body">
                        <p></p>
                    </div>
                    <div class="modal-footer">
                    <%--<button type="button" class="btn btn-danger" data-dismiss="modal">Delete</button>--%>
                        <asp:Button ID="delete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="delete_Click" />
                    </div>
                </div>
      <%--</ContentTemplate></asp:UpdatePanel>--%>
    </div>
  </div>
        </div>
    </form>
</body>
</html>
