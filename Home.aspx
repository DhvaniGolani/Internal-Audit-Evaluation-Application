<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Project2021.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
        <link rel="stylesheet" href="https://jqueryvalidation.org/files/demo/site-demos.css"/>
 
        <script src="https://code.jquery.com/jquery-3.5.1.js" integrity="sha256-QWo7LDvxbWT2tbbQ97B53yJnYU3WhH/C8ycbRAkjPDc=" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.79/jquery.form-validator.min.js"></script>
        <script src="https://cdn.jsdelivr.net/jquery.validation/1.16.0/jquery.validate.min.js"></script>
<script src="https://cdn.jsdelivr.net/jquery.validation/1.16.0/additional-methods.min.js"></script>
    <title>Home</title>
    <style>
            table.center{
                 margin-left: auto;
                 margin-right: auto;
             }
             td{
                 padding: 5px;
             }
             body{
                  text-align: center;
                  font-size: 20px;
             }
             .divClass{
                 border: 2px outset;
                 margin: auto;
                 
                 padding: 10px;
                 margin-top:10px; 
                 text-align: center;
             }
        </style>
        <script>
            $(function(){
                $("form").validate({
                    rules: {
                        Name: "required" , 
                        PSNo: "required",
                        Email: {
                            required: true,
                            email: true
                        },
                        PhoneNo: {
                            required: true,
                            minlength: 10
                        }
                    },
                    messages: {
                        Name: "This field is required!!" , 
                        PSNo: "This field is required!!",
                        Email: {
                            required: "This field is required!!",
                            email: "Enter valid email address"
                        },
                        PhoneNo: {
                            required: "This field is required!!",
                            minlength: "Minimum 10 digits"
                        }
                    },
                    submitHandler: function(form) {
                        //
                        alert("Valid Form Submitted!!!");
                        form.submit();
                        return false;
                    }
                });
            });
        </script>
        <script>
            $(document).ready(function(){
                $("input").focus(function(){
                    $(this).css("background-color", "aqua");
                });
            });
        </script>
</head>
<body>
    <div class="container">
        <div class="divClass">
            <h2>Form</h2>
    <form id="form1" runat="server">
        <div>
            
            <table  class="center">
                <tr>
                    <td>
                        <label>Name </label>
                    </td>
                    <td>
                        <input runat="server" type="text" name="Name" id="Name"/>
                    </td>
                </tr>
                <tr>
                    <td><label>PS No. </label></td>
                    <td><input runat="server" type="number" name="PSNo" id="PSNo"/></td>
                </tr>
                <tr>
                    <td><label>Email </label></td>
                    <td><input runat="server" type="email" name="Email" id="Email"/></td>
                </tr>
                <tr>
                    <td><label>Phone number </label></td>
                    <td><input runat="server" type="tel" name="PhoneNo" id="PhoneNo"/></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click1" />
                        
                    </td>
                </tr>
            </table>
        </div>
    </form>
            </div>
        </div>
</body>
</html>
