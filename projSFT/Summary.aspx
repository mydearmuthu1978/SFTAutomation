<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ProjectManagement.Master" CodeBehind="Summary.aspx.cs" Inherits="projSFT.Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

            <table style = "width:100% ">
            <tr>
               <td >
                    <asp:GridView ID="gv1" AllowPaging="True" BackColor="#f1f1f1" 
                        AutoGenerateColumns="false" DataKeyNames="Id" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 32px" 
                        ShowFooter="true"  Font-Size="Small" Font-Names="Verdana" runat="server" GridLines="None"
                        OnRowUpdating = "gv1_RowUpdating" OnPageIndexChanging="gv1_PageIndexChanging" BorderStyle="Outset"
                        OnRowDeleting = "gv1_RowDeleting" OnRowEditing="gv1_RowEditing" OnRowCancelingEdit="gv1_RowCancelingEdit"> 
                        <RowStyle BackColor="Gainsboro" HorizontalAlign="Center"/>
                        <AlternatingRowStyle BackColor="White" />
                        <HeaderStyle BackColor="#0083C1" ForeColor="White"/>
			            <Columns>       
                                
                                <asp:TemplateField HeaderText="Id" Visible="false" SortExpression="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ID" runat="server" Text='<%# Eval("Id") %>' Visible="false" />                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_TaskID" runat="server" Text='<%# Eval("Id") %>' Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Appname" HeaderText="Appname" ReadOnly="true" SortExpression="Appname" />
                                <asp:TemplateField HeaderText="Priority">  
                                    <ItemTemplate>                                          
                                        <asp:Label ID="Priority" runat="server" Text='<%# Eval("Priority")%>'></asp:Label>
                                    </ItemTemplate>  
                                    <EditItemTemplate>  
                                          <asp:DropDownList ID="ddlPriority" runat="server" Text='<%#Eval("Priority") %>' AppendDataBoundItems="True">
                                            <asp:ListItem Value="Full Functional" Text="Full Functional"></asp:ListItem>
                                            <asp:ListItem Value="Sanity" Text="Sanity"></asp:ListItem>
                                            <asp:ListItem Value="Regression" Text="Regression"></asp:ListItem>
                                            <asp:ListItem Value="PML" Text="PML"></asp:ListItem>
                                         </asp:DropDownList>
                                    </EditItemTemplate>                                                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time Taken(Hrs)">
                                    <ItemTemplate ><%# Eval("TimeTaken") %></ItemTemplate>
                                        <EditItemTemplate >
                                            <asp:TextBox ID="txtTimeTaken" runat="server" Text='<%# Eval("TimeTaken")%>'  ></asp:TextBox>
                                        </EditItemTemplate>
                                </asp:TemplateField>  
                                <asp:CommandField  ShowEditButton="True"/>                                
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
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
           
</asp:Content>