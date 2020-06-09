<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRUDNestedGrid.aspx.cs" Inherits="projSFT.CRUDNestedGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Editable Nested Grid View</title>
    <script type="text/javascript">
    function expandcollapse(obj,row)
    {
        var div = document.getElementById(obj);
        var img = document.getElementById('img' + obj);
        
        if (div.style.display == "none")
        {
            div.style.display = "block";
            if (row == 'alt')
            {
                img.src = "images/minus.png";
            }
            else
            {
                img.src = "images/minus.png";
            }
            img.alt = "Close to view other Section";
        }
        else
        {
            div.style.display = "none";
            if (row == 'alt')
            {
                img.src = "images/plus.png";
            }
            else
            {
                img.src = "images/plus.png";
            }
            img.alt = "Expand to show Task";
        }
    } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblApplicationName" runat="server" Text ="Application Name" Width="150px"/>
        <asp:DropDownList ID="ddlApplist" runat="server" AppendDataBoundItems="true">
            <asp:ListItem Text="All Application" Value="0">
                </asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click"/>
        <asp:GridView ID="GridView1" AllowPaging="True" BackColor="#f1f1f1" 
            AutoGenerateColumns="false" DataKeyNames="ID"
            style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 32px" ShowFooter="true"  Font-Size="Small"
            Font-Names="Verdana" runat="server" GridLines="None" OnRowDataBound="GridView1_RowDataBound" 
            OnRowCommand = "GridView1_RowCommand" OnRowUpdating = "GridView1_RowUpdating" BorderStyle="Outset"
            OnRowDeleting = "GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" >
            <RowStyle BackColor="Gainsboro" HorizontalAlign="Center"/>
            <AlternatingRowStyle BackColor="White" />
            <HeaderStyle BackColor="#0083C1" ForeColor="White"/>
            <FooterStyle BackColor="White" HorizontalAlign="Center"/>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="javascript:expandcollapse('div<%# Eval("ID") %>', 'one');">
                            <img id="imgdiv<%# Eval("ID") %>" alt="Click to show/hide task for Section <%# Eval("ID") %>"  width:"20px" border="0" src="images/plus.png"/>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  SortExpression="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblSectionID" Text='<%# Eval("ID") %>' Visible="false" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SectionName" >
                    <ItemTemplate><%# Eval("SectionName") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSectionName" Text='<%# Eval("SectionName") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtSectionName" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AppId" Visible="false" >
                    <ItemTemplate><%# Eval("AppId") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAppId" Text='<%# Eval("AppId") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtAppId" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>                  
                 <asp:TemplateField HeaderText="SectionType">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_SectionType" runat="server" Text='<%#Eval("SectionType") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:DropDownList ID="txtSectionType" runat="server" Text='<%#Eval("SectionType") %>' AppendDataBoundItems="True">
                               <asp:ListItem Value="fixed" Text ="fixed"></asp:ListItem>
                                <asp:ListItem Value="variable" Text ="variable"></asp:ListItem>
                        </asp:DropDownList>  
                    </EditItemTemplate> 
                     <FooterTemplate>
                        <asp:DropDownList ID="txtSectionType" runat="server">
                             <asp:ListItem Value="fixed" Text ="fixed"></asp:ListItem>
                                <asp:ListItem Value="variable" Text ="variable"></asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TotalTestcase" >
                    <ItemTemplate><%# Eval("TotalTestCase") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTotalTestCase" Text='<%# Eval("TotalTestCase") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtTotalTestCase" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
			    <asp:TemplateField HeaderText="ApplicationName">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ApplicationName" runat="server" Text='<%#Eval("ApplicationName") %>'></asp:Label>  
                    </ItemTemplate>
                    <%--<EditItemTemplate>  
                        <asp:DropDownList ID="txtApplicationName" runat="server" Text='<%#Eval("ApplicationName") %>' DataTextField="Applicationname" DataValueField="Applicationname" AppendDataBoundItems="True">
                                 <asp:ListItem Text="Select Application" Value="0"/>
                        </asp:DropDownList>  
                    </EditItemTemplate>--%> 
                     <FooterTemplate>
                        <asp:TextBox ID="txtApplicationName" Text="" Visible="false"  Enabled="false"  runat="server"></asp:TextBox>
                    </FooterTemplate>
                       </asp:TemplateField>
                <asp:TemplateField>
                        <FooterTemplate>
                       <asp:DropDownList ID="ddlApplicationName" runat="server" visible="false" AppendDataBoundItems="True">
                             <asp:ListItem Text="Select Application" Value="0"/>
                           </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
			    <asp:CommandField  ShowEditButton="True" />
			    <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDeleteSec" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="linkAddSec" CommandName="AddSection" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
			    
			    <asp:TemplateField>
			        <ItemTemplate>
			            <tr>
                            <td colspan="150">
                                <div id="div<%# Eval("Id") %>" style="display:none;position:relative;left:5px;OVERFLOW: auto;WIDTH:97%" >
                                    <asp:GridView ID="GridView2" AllowPaging="True"  BackColor="White" Width="100%" Font-Size="X-Small"
                                        AutoGenerateColumns="false" Font-Names="Verdana" runat="server" DataKeyNames="Sec_id" ShowFooter ="true"
                                        OnPageIndexChanging="GridView2_PageIndexChanging" OnRowUpdating = "GridView2_RowUpdating"
                                        OnRowCommand = "GridView2_RowCommand" OnRowEditing = "GridView2_RowEditing" GridLines="None"
                                        OnRowCancelingEdit = "GridView2_CancelingEdit" OnRowDataBound = "GridView2_RowDataBound"
                                        OnRowDeleting = "GridView2_RowDeleting" 
                                        BorderStyle="Double" BorderColor="#0083C1" >
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#9353A1" ForeColor="White"/>
                                        <FooterStyle BackColor="White" HorizontalAlign="Center" />
                                        <RowStyle HorizontalAlign="Center"/>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskID" Text='<%# Eval("TaskID") %>' Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>                            
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task Name">
                                                <ItemTemplate><%# Eval("Taskname")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTaskname" Text='<%# Eval("Taskname")%>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTaskname" Text='' runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time Taken" >
                                                <ItemTemplate><%# Eval("Timetaken")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTimetaken" Text='<%# Eval("Timetaken")%>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTimetaken" Text='' runat="server" ></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>                                                                                     
			                                <asp:CommandField  ShowEditButton="True" />
			                                <asp:TemplateField>
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="linkDeleteCus" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:LinkButton ID="linkAddTask" CommandName="AddTask" runat="server">Add</asp:LinkButton>
                                                 </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                   </asp:GridView>
                                </div>
                             </td>
                        </tr>
			        </ItemTemplate>			       
			    </asp:TemplateField>			    
			</Columns>
        </asp:GridView>      
    </div>              
    </form>
</body>
</html>
