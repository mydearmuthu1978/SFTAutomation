<%@ Page Title="" Language="C#" MasterPageFile="~/ProjectManagement.Master" AutoEventWireup="true" CodeBehind="Scope.aspx.cs" Inherits="projSFT.Scope" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <table style = "width:100% ">
            <tr>
               <td >
                    <asp:GridView ID="GridView1" AllowPaging="True" BackColor="#f1f1f1" 
                          AutoGenerateColumns="false" DataKeyNames="Id" BorderStyle="Double" BorderColor="#0083C1" EnableSortingAndPagingCallbacks ="false"
                        style="Z-INDEX: 101; l: 8px; POSITION: absolute; TOP: 32px" Font-Size="Small" HorizontalAlign="Right"
                        Font-Names="Verdana" runat="server" GridLines="None" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowDeleting = "GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"   OnRowUpdating = "GridView1_RowUpdating1" OnRowCancelingEdit="GridView1_RowCancelingEdit"  
                        >
			            <Columns>       
                                
                               <asp:TemplateField HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ID" runat="server" Text='<%# Eval("Id") %>' Visible="false" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AppId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_AppID" runat="server" Text='<%# Eval("Id") %>' Visible="false" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Appname" HeaderText="Appname" ReadOnly="true" SortExpression="Appname" />

                                <asp:TemplateField HeaderText="Priority">
                                    <ItemTemplate>
                                        <asp:Label ID="Priority" runat="server" Text='<%#Eval("Priority") %>'></asp:Label>
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

                                <asp:TemplateField HeaderText="TotalTestCases">
                                    <ItemTemplate><%# Eval("TotalTestCases") %></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTotalTestCases" Text='<%# Eval("TotalTestCases") %>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                             <asp:CommandField ShowEditButton="true"/>     
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
