<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Topmenu.aspx.cs" Inherits="projSFT.Topmenu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">  
        body {  
            background-color: mediumaquamarine;  
            font-family: Arial;  
            font-size: 10pt;  
            color: #444;  
        }  
  
        .ParentMenu, .ParentMenu:hover {  
            width: 100px;  
            background-color: #fff;  
            color: #333;  
            text-align: center;  
            height: 30px;  
            line-height: 30px;  
            margin-right: 5px;  
        }  
  
            .ParentMenu:hover {  
                background-color: #ccc;  
            }  
  
        .ChildMenu, .ChildMenu:hover {  
            width: 110px;  
            background-color: #fff;  
            color: #333;  
            text-align: center;  
            height: 30px;  
            line-height: 30px;  
            margin-top: 5px;  
        }  
  
            .ChildMenu:hover {  
                background-color: #ccc;  
            }  
  
        .selected, .selected:hover {  
            background-color: #A6A6A6 !important;  
            color: #fff;  
        }  
  
        .level2 {  
            background-color: #fff;  
        }  
    </style>
</head>
<body id="Program">
    <form id="form1" runat="server">   
    <asp:menu id="NavigationMenu" orientation="Horizontal" font-names="Arial" target="_blank" runat="server" OnMenuItemClick="NavigationMenu_MenuItemClick" OnMenuItemDataBound="Menu1_MenuItemDataBound">
    <StaticMenuStyle CssClass="StaticMenuItem" />
                <StaticMenuItemStyle CssClass="StaticMenuItemStyle" />
                <StaticHoverStyle CssClass="StaticHoverStyle" />  
                <StaticSelectedStyle CssClass="StaticSelectedStyle" />              
                <DynamicMenuItemStyle CssClass="DynamicMenuItemStyle" />
                <DynamicHoverStyle CssClass="DynamicHoverStyle" />
        <StaticSelectedStyle CssClass="selected" />    
    </asp:menu>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Program"></asp:Label>
        <br />
        <br />
        <asp:ListBox ID="ProgramList" runat="server" Height="100%" Width="100"></asp:ListBox>
        
        <br />
        <br />
        
     <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" 
            OnPageIndexChanging="GridView1_PageIndexChanging" 
            OnRowCancelingEdit="GridView1_RowCancelingEdit" 
            OnRowDeleting="GridView1_RowDeleting" 
            OnRowEditing="GridView1_RowEditing" 
            OnRowUpdating="GridView1_RowUpdating" 
            StaticMenuItemStyle-CssClass="menuItem"
            DynamicMenuItemStyle-CssClass="menuItem"
            HorizontalAlign="Center"
            PageSize="5"
            Width="302px">
            <Columns>
                <asp:BoundField DataField="Appname" HeaderText="Appname" SortExpression="Appname" />
                <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority" />
                <asp:BoundField DataField="Testcases" HeaderText="Testcases" SortExpression="Testcases" />
                <asp:CommandField ShowEditButton="true" />             
                
            </Columns>
        </asp:GridView>
     </div>       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:myDbConnection %>" 
            SelectCommand="SELECT [Appname], [Priority], [Testcases] FROM [App_testcases]"            
            UpdateCommand="UPDATE App_testcases SET Appname = + txt_Appname + , Priority =  + txt_Priority + , Testcases =  + txt_Testcases + ">
            
        </asp:SqlDataSource>
        
        <br />
        
        </form>
</body>
</html>
