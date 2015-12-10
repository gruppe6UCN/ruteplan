<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteDetail.aspx.cs" Inherits="WebApp.RouteDetil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1><%=GetTitle() %></h1>
    <div>
        <asp:Table ID="Table1" runat="server">
        </asp:Table>
    </div>
    </form>
</body>
</html>
