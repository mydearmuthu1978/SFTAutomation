<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Topmenu.aspx.cs" Inherits="projSFT.Topmenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">   
    <asp:menu id="NavigationMenu" disappearafter="2000" staticdisplaylevels="2" staticsubmenuindent="10" orientation="Vertical" font-names="Arial" target="_blank" datasourceid="SqlDataSource1" runat="server"></asp:menu>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cn %>" SelectCommand="SELECT [menuname] FROM [top_menu]"></asp:SqlDataSource>
        
        </form>
</body>
</html>
