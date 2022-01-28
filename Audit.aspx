<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Audit.aspx.cs" Inherits="Project2021.Audit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <link href='https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/ui-lightness/jquery-ui.css' rel='stylesheet' />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <!-- JQuery -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <!-- Bootstrap tooltips -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.4/umd/popper.min.js"></script>
    <!-- Bootstrap core JavaScript -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.5.0/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <style>
        table.center {
            margin-left: auto;
            margin-right: auto;
        }

        hr.solid {
            border-top: 2px solid #bbb;
        }

        td {
            padding: 5px;
            /*color:darkblue;*/
        }

        body {
            font-size: 15px;
        }

        .divClass {
            /*border: 2px outset;*/
            margin: auto;
            padding: 10px;
            margin-top: 10px;
        }

        h2 {
            text-align: center;
        }

        .header {
            overflow: hidden;
            background-color: #f1f1f1;
            padding: 20px 10px;
        }

            .header h1 {
                text-align: center;
            }

            .header img {
                float: left;
                width: 200px;
                height: 70px;
            }

            .header button:hover {
                background-color: #ddd;
                color: black;
            }

            .header button.active {
                background-color: dodgerblue;
                color: white;
            }

        .header-right {
            float: right;
            align-content: baseline;
        }
        #grid {
            /*width:95%;*/
            margin:5px;
        }
        #HSEgrid,#NCgrid{margin:5px;}
    </style>


    <link rel="stylesheet" href="https://kendo.cdn.telerik.com/2021.1.224/styles/kendo.default-v2.min.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2021.1.224/js/kendo.all.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>
    <script>
        var detailExportPromises = [];
        function exportPDF(id) {
            document.getElementById('<%=hdn_get_ID_1.ClientID%>').value = id;
            $('#<%=editing.ClientID%>').click();
        };
        function showDetails(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            document.getElementById('<%=hdn_get_ID.ClientID%>').value = dataItem.id;
            console.log('ID: ' + dataItem.id);
            window.location = "WebForm1.aspx?EditID=" + dataItem.id;
        };
        function editHSE(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            document.getElementById('<%=hdn_get_ID.ClientID%>').value = dataItem.FK_Audit_ID;
            console.log('ID: ' + dataItem.FK_Audit_ID);
            window.location = "HSE_Surveillance.aspx?EditID=" + dataItem.FK_Audit_ID;
        };
        function editNC(e) {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            document.getElementById('<%=hdn_get_ID.ClientID%>').value = dataItem.FK_Audit_ID;
            console.log('ID: ' + dataItem.FK_Audit_ID);
            window.location = "NCReport.aspx?EditID=" + dataItem.FK_Audit_ID;
        };
        function detailInit(e) {
            var val = $('#<%=hdnfld1.ClientID%>').val();
            var data = JSON.parse(val);
            $("<div/>").appendTo(e.detailCell).kendoGrid({
                columns: [{
                    field: "id",
                    title: "ID",
                    width: 40
                },
                {
                    field: "Ques_no",
                    title: "Ques. No.",
                    width: 100
                },
                {
                    field: "Questions",
                    title: "Details",
                },
                {
                    field: "Observation",
                    title: "Observation"
                },
                {
                    field: "Score",
                    itle: "Score"
                }
                ],
                dataSource: {
                    data: data,
                    filter: { field: 'id', operator: 'eq', value: e.data['id'] }
                }
            });
        }
        function exportChildData(id, rowIndex) {
            var val = $('#<%=hdnfld1.ClientID%>').val();
            var data = JSON.parse(val);
            var deferred = $.Deferred();
            detailExportPromises.push(deferred);
            var rows = [{
                cells: [
                    // First cell.
                    { value: "id" },
                    // Second cell.
                    { value: "Ques_no" },
                    // Third cell.
                    { value: "Questions" },
                    // Fourth cell.
                    { value: "Observation" },
                    // Fifth cell.
                    { value: "Score" }
                ]
            }];
            //dataSource.filter({ field: "id", operator: "eq", value: id });

            var exporter = new kendo.ExcelExporter({
                columns: [{
                    field: "id", title: "ID"
                }, {
                    field: "Ques_no"
                }, {
                    field: "Questions"
                }, {
                    field: "Observation"
                }, {
                    field: "Score"
                    }],
                //dataSource: data
                dataSource: {
                    data: data,
                    filter: { field: 'id', operator: 'eq', value: id }
                },
            });

            exporter.workbook().then(function (book, data) {
                deferred.resolve({
                    masterRowIndex: rowIndex,
                    sheet: book.sheets[0]
                });
            });
        }
        $(document).ready(function () {
            $("#HSEgrid").hide();
            var HSEval = $('#<%=HiddenField1.ClientID%>').val();
            var HSEdata = JSON.parse(HSEval);
            $("#HSEgrid").kendoGrid({
                columns: [{
                    field: "Id",
                    title: "ID"
                },
                {
                    field: "date",
                    title: "Date", format: "{0:dd-MM-yyyy}"
                },
                {
                    field: "ic",
                    title: "IC"
                },
                {
                    field: "site",
                    title: "Site"
                },
                {
                    field: "CreatedBy",
                    title: "CreatedBy"
                },
                {
                    field: "FK_Audit_ID",
                    title: "Audit_ID"
                },
                { 'command': { text: 'Edit', click: editHSE, iconClass: 'k-icon k-i-pencil' }, 'title': ' ', 'width': '80px' }
                ],
                //sortable: true,
                dataSource: {
                    data: HSEdata,
                    schema: {
                        model: {
                            fields: {
                                date: { type: "date", format: "dd-MM-yyyy" }
                            }
                        }
                    }
                },
                filterable: true,
                columnMenu: true,
                reorderable: true,
                resizable: true,
                sortable: true,
            });
            $("#one").click(function () {
                $("#one").addClass('btn btn-link');
                $("#audit").removeClass('btn btn-link');
                $("#audit").addClass('btn btn-light');
                $("#two").removeClass('btn btn-link');
                $("#two").addClass('btn btn-light');
                $("#grid").hide();
                $("#NCgrid").hide();
                $("#HSEgrid").show();
                
            });
            $("#two").click(function () {
                $("#two").addClass('btn btn-link');
                $("#audit").removeClass('btn btn-link');
                $("#audit").addClass('btn btn-light');
                $("#one").removeClass('btn btn-link');
                $("#one").addClass('btn btn-light');
                $("#grid").hide();
                $("#HSEgrid").hide();
                $("#NCgrid").show();
                var val = $('#<%=HiddenField2.ClientID%>').val();
                var data = JSON.parse(val);

                $("#NCgrid").kendoGrid({
                    columns: [{
                        field: "Id",
                        title: "ID"
                    },
                    {
                        field: "sitename",
                        title: "Site"
                    },
                    {
                        field: "department",
                        title: "Department"
                    },
                    {
                        field: "auditee",
                        title: "Auditee"
                    },
                    {
                        field: "ObserverTeam",
                        title: "ObserverTeam"
                    },
                    {
                        field: "Major",
                        title: "Major"
                    },
                    {
                        field: "Minor",
                        title: "Minor"
                    },
                    //{
                    //    field: "auditor",
                    //    title: "Auditor"
                    //    },
                        {
                            field: "FK_Audit_ID",
                            title: "Audit_ID"
                        },
                        { 'command': { text: 'Edit', click: editNC, iconClass: 'k-icon k-i-pencil' }, 'title': ' ', 'width': '80px' }
                    ],
                    //sortable: true,
                    dataSource: {
                        data: data
                    },
                    filterable: true,
                    columnMenu: true,
                    reorderable: true,
                    resizable: true,
                    sortable: true,
                });
            });
            $("#audit").click(function () {
                $("#audit").addClass('btn btn-link');
                $("#one").removeClass('btn btn-link');
                $("#one").addClass('btn btn-light');
                $("#two").removeClass('btn btn-link');
                $("#two").addClass('btn btn-light');
                $("#grid").show();
                $("#HSEgrid").hide();
                $("#NCgrid").hide();
                
            });
            var val = $('#<%=hdnjson.ClientID%>').val();
            var data = JSON.parse(val);
            $("#grid").kendoGrid({
                toolbar: ["excel"],
                excel: {
                    fileName: "Dashboard.xlsx",
                    allPages: true
                },
                columns: [
                    {
                        field: "id",
                        title: "ID",
                        width: 65
                    },
                    {
                        field: "dateOfAudit",
                        title: "Date of Audit",
                        format: "{0:dd-MM-yyyy}"
                    },
                    {
                        field: "siteName",
                        title: "Name of Site",
                    },
                    {
                        field: "siteDetails",
                        title: "Details of Site",
                    },
                    {
                        field: "auditTeam",
                        title: "Audit Team",
                    },
                    {
                        field: "observerTeam",
                        title: "Observer Team",
                    },
                    {
                        field: "strenghts",
                        title: "Strenghts",
                    },
                    {
                        field: "MajorNC",
                        title: "Major Non-Compliance ",
                    },
                    {
                        field: "MinorNC",
                        title: "Minor Non-Compliance ",
                    },
                    {
                        field: "OFIs",
                        title: "Opportunity for Improvement",
                    },

                    { 'command': { text: 'Edit', click: showDetails, iconClass: 'k-icon k-i-pencil' }, 'title': ' ', 'width': '80px' },
                    { template: "<input type='button' class='k-button' id='PDF' value='Export to PDF' name='PDF' onclick='exportPDF(#=id#)'/>", title: " ", width: "150px" }
                    
                ],
                filterable: true,
                columnMenu: true,
                reorderable: true,
                resizable: true,
                sortable: true,
                pdf: {
                    author: "Dhvani Golani",
                    creator: "dhvani",
                    date: new Date(),
                    allPages: true,
                    fileName: "Audit_Form_Dashboard.pdf",
                    subject: "Audit Form Dashboard",
                    title: "Audit Form Dashboard"
                },
                pdfExport: function (e) {
                    e.promise
                        .progress(function (e) {
                            console.log(kendo.format("{0:P} complete", e.progress));
                        })
                        .done(function () {
                            alert("Export completed!");
                        });
                },
                excelExport: function (e) {
                    e.preventDefault();

                    var workbook = e.workbook;
                    var sheet = e.workbook.sheets[0];

                    sheet.name = "Audit Details";
                    detailExportPromises = [];
                    var masterData = e.data;
                    for (var rowIndex = 0; rowIndex < masterData.length; rowIndex++) {
                        exportChildData(masterData[rowIndex].id, rowIndex);
                    }
                    $.when.apply(null, detailExportPromises)
                        .then(function () {
                            // Get the export results.
                            var detailExports = $.makeArray(arguments);

                            // Sort by masterRowIndex.
                            detailExports.sort(function (a, b) {
                                return a.masterRowIndex - b.masterRowIndex;
                            });

                            // Add an empty column.
                            workbook.sheets[0].columns.unshift({
                                width: 30
                            });

                            // Prepend an empty cell to each row.
                            for (var i = 0; i < workbook.sheets[0].rows.length; i++) {
                                workbook.sheets[0].rows[i].cells.unshift({});
                            }

                            // Merge the detail export sheet rows with the master sheet rows.
                            // Loop backwards so the masterRowIndex does not need to be updated.
                            for (var i = detailExports.length - 1; i >= 0; i--) {
                                var masterRowIndex = detailExports[i].masterRowIndex + 1; // compensate for the header row

                                var sheet = detailExports[i].sheet;

                                // Prepend an empty cell to each row.
                                for (var ci = 0; ci < sheet.rows.length; ci++) {
                                    if (sheet.rows[ci].cells[0].value) {
                                        sheet.rows[ci].cells.unshift({});
                                        sheet.rows[ci].cells.unshift({});
                                    }
                                }

                                // Insert the detail sheet rows after the master row.
                                [].splice.apply(workbook.sheets[0].rows, [masterRowIndex + 1, 0].concat(sheet.rows));
                            }

                            // Save the workbook.
                            kendo.saveAs({
                                dataURI: new kendo.ooxml.Workbook(workbook).toDataURL(),
                                fileName: "Dashboard.xlsx"
                            });
                        });
                },
                dataSource: {
                    data: data,
                    pageSize: 8,
                    schema: {
                        model: {
                            fields: {
                                dateOfAudit: { type: "date", format: "dd-MM-yyyy" }
                            }
                        }
                    }
                },
                pageable: true,
                scrollable: true,
                detailInit: detailInit
            });
        });
    </script>
    <title>Dashboard</title>
    </head>
<body>
    <nav class="navbar navbar-default" style="padding: 10px">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">
                    <label style="color: darkblue;">L&T Power</label></a>
            </div>
            <ul class="nav navbar-nav" style="float: right;">
                <li class="active"><a href="#">Audit</a></li>
                <li><a href="AuditDetails.aspx" >Audit Details</a></li>
                <li><a href="login.aspx">Log Out</a></li>
            </ul>
        </div>
    </nav>

    <form id="form1" runat="server">
        <div class="divClass">

            <div id="grid" runat="server">
            </div>
            <asp:HiddenField runat="server" ID="hdnjson" />
            <div id="HSEgrid" runat="server">
            </div>
            <asp:HiddenField runat="server" ID="HiddenField1" />
            <div id="NCgrid" runat="server">
            </div>
            <asp:HiddenField runat="server" ID="HiddenField2" />
            <asp:HiddenField runat="server" ID="hdnfld1" />
            <asp:HiddenField runat="server" ID="hdn_get_ID" />
            <asp:HiddenField runat="server" ID="hdn_get_ID_1" />
            <%--<input type="button" id="edit" runat="server" onclick="clickHdnButton()" visible="false" />--%>

            <asp:Button ID="editing" runat="server" OnClick="edit_redirect_Click" style="display:none;"/>

            <button id="audit" type="button" class="btn btn-link">Audit</button>
            <button id="one" type="button" class="btn btn-light">HSE_Surveillance</button>
            <button id="two" type="button" class="btn btn-light">NC Report</button>
        </div>
    </form>
</body>
</html>
