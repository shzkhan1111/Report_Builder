<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ExtractReport.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/PageScript/jquery-1.12.4.js"></script>
    <script src="Scripts/PageScript/jquery-ui.js"></script>
    <link href="Scripts/PageScript/jquery-ui.css" rel="stylesheet" />
    <%--<script src="https://code.jquery.com/jquery-1.12.4.js"></script>--%>
    <%--<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />--%>
    <script>
        $(function () {
            $(".datepicker").datepicker(
                {
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'yy/mm/dd'
                });
        });
    </script>
    <style>
        body {
            font-family: sans-serif;
            background-color: lightgoldenrodyellow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <label>format</label>
        <asp:DropDownList ID="format" runat="server">
            <asp:ListItem Text="CSV" Value=".csv"></asp:ListItem>
            <asp:ListItem Text="HTML" Value=".html"></asp:ListItem>
            <asp:ListItem Text="XML" Value=".xml"></asp:ListItem>
            <asp:ListItem Text="Notepad" Value=".txt"></asp:ListItem>
        </asp:DropDownList>
        <br />

        <asp:CheckBox ID="Product_Title" runat="server" Checked="true" /><label>Product Title</label>
        <asp:CheckBox ID="Quantity" runat="server" Checked="true" /><label>Quantity</label>
        <asp:CheckBox ID="Total_Amount" runat="server" Checked="true" /><label>Total Amount</label>
        <br />
        <table>
            <tr>
                <td>
                    <label>Start Date : </label>
                </td>
                <td>
                    <asp:TextBox class="datepicker" runat="server" ID="strtDate" />
                </td>
            </tr>
            <tr>
                <td>
                     <label>End Date :  </label>
                </td>
                <td>
                    <asp:TextBox class="datepicker" runat="server" ID="EndDate" />
                </td>
            </tr>
        </table>


        <br />
       
        

        <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
    </form>
    <asp:Label runat="server" ID="message" style="color:black"></asp:Label>
</body>
</html>
