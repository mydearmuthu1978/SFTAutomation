<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="content.aspx.cs" Inherits="projSFT.content" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Appname" HeaderText="Appname" SortExpression="Appname" />
                <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority" />
                <asp:BoundField DataField="Testcases" HeaderText="Testcases" SortExpression="Testcases" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SFT_AutomationConnectionString %>" SelectCommand="SELECT [Appname], [Priority], [Testcases] FROM [App_testcases]"></asp:SqlDataSource>
    </form>
</body>
</html>
