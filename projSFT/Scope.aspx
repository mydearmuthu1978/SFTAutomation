<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scope.aspx.cs" Inherits="projSFT.Topmenu" %>

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
        .ParentMenu:hover 
        {  
            background-color: #ccc;  
        }  
        .ChildMenu, .ChildMenu:hover 
        {  
            width: 110px;  
            background-color: #fff;  
            color: #333;  
            text-align: center;  
            height: 30px;  
            line-height: 30px;  
            margin-top: 5px;  
        }  
        .ChildMenu:hover 
        {  
            background-color: #ccc;  
        }  
        .selected, .selected:hover 
        {  
            background-color: #A6A6A6 !important;  
            color: #fff;  
        }  
        .level2 
        {  
            background-color: #fff;  
        }  
        #footer
        {
            width: 100%;
            background-color: #C0C0C0;
            height: 50px;
            position:absolute;
            bottom: 0;
            color: White;
            text-align: center;
        }
        #content
        {
            width: 1000px;
            height: 100%;
            background-color: #F5FDEC;  
            margin: 0 auto 0 auto;
        }        
        .nav_style
         {
             list-style:none;
             font:14px calibri;
             background-color: none;
             padding: 10px;
         }
        .nav_style:hover
         {
             background-color:red;
         }
        .selected .nav_style{
            background-color: yellow;
         }
        a.navList{text-decoration: none; color: gray;}
        a.activePage{ color: green; border-bottom: solid; border-width: 3px;}
    </style>
</head>
<body id="Program">
    <form id="form1" runat="server"> 
        <div id ="main" >  
        <table border="0" class="tbl"> 
            <tr>  
                    <td style="width:30%"></td>  
                    <td style ="width:70%">                 
                        <asp:menu id="NavigationMenu" orientation="Horizontal" font-names="Arial" target="_blank" runat="server" OnMenuItemClick="NavigationMenu_MenuItemClick1" OnMenuItemDataBound="Menu1_MenuItemDataBound1">
                            <LevelMenuItemStyles>
                                <asp:MenuItemStyle CssClass="ParentMenu" />                                  
                             </LevelMenuItemStyles> 
                             <StaticSelectedStyle CssClass="selected" />
                             <DynamicSelectedStyle CssClass ="selected" />
                        </asp:menu>
                    </td>   
            </tr>
            <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="PROGRAM"></asp:Label>   
            </td>    
            </tr>
            <tr>
                <td>                    
                    <asp:Menu ID="VerticalMenu" runat="server" orientation="Vertical" font-names="Arial" target="_blank">
                        <LevelMenuItemStyles>                      
                            <asp:MenuItemStyle CssClass="ChildMenu" />                      
                        </LevelMenuItemStyles>  
                        <StaticSelectedStyle CssClass="selected" />
                        <DynamicSelectedStyle CssClass ="selected" />
                    </asp:Menu>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   
                        OnPageIndexChanging="GridView1_PageIndexChanging" 
                        OnRowCancelingEdit="GridView1_RowCancelingEdit" 
                        OnRowDeleting="GridView1_RowDeleting" 
                        OnRowEditing="GridView1_RowEditing" 
                        OnRowUpdating="GridView1_RowUpdating" 
                        StaticMenuItemStyle-CssClass="menuItem"
                        DynamicMenuItemStyle-CssClass="menuItem"
                        HorizontalAlign="Center"
                        PageSize="5"
                        Width="100%" 
                        BackColor="White" 
                        BorderColor="#CCCCCC" 
                        BorderStyle="Solid" 
                        BorderWidth="1px" 
                        CellPadding="4">
                        <Columns>                
                            <asp:TemplateField HeaderText="AppName">
                                 <ItemTemplate >
                                      <asp:Label runat="server" Text='<%# Eval("Appname") %>' ></asp:Label>
                                 </ItemTemplate>
                                       <EditItemTemplate >
                                           <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("Appname")%>'  ></asp:TextBox>
                                       </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Priority">
                                 <ItemTemplate >
                                      <asp:Label runat="server" Text='<%# Eval("Priority") %>' ></asp:Label>
                                 </ItemTemplate>
                                       <EditItemTemplate >
                                           <asp:DropDownList ID="DropDownList1" runat="server" >
                                           </asp:DropDownList>
                                       </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Test Cases">
                                 <ItemTemplate >
                                     <asp:Label runat="server" Text='<%# Eval("Testcases") %>' ></asp:Label>
                                 </ItemTemplate>                     
                                       <EditItemTemplate >
                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("Testcases")%>'  ></asp:TextBox>
                                       </EditItemTemplate>
                            </asp:TemplateField>                                     
                            
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Right" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                </td>
            </tr>
           </table>
        </div>
      <div id="footer" style="width:100%">
          <table border="0" class="tbl">
                <tr>
                    <td colspan="2"> 
                    <img src="images/mphasis_bottom-logo.png" /> 
                    </td>
                </tr>
          </table>
      </div>   
       </form>
</body>
</html>
