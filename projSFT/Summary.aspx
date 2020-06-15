<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ProjectManagement.Master" CodeBehind="Summary.aspx.cs" Inherits="projSFT.Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

            <table style = "width:100% ">
            <tr>
               <td >
                    <asp:GridView ID="gv1" AllowPaging="True" BackColor="#f1f1f1" 
                        AutoGenerateColumns="false" DataKeyNames="Id" BorderStyle="Double" BorderColor="#0083C1"
                        style="Z-INDEX: 101; l: 8px; POSITION: absolute; TOP: 32px" Font-Size="Small"
                        Font-Names="Verdana" runat="server" GridLines="None" OnRowDataBound="gv1_RowDataBound"
                        OnRowUpdating = "gv1_RowUpdating" OnPageIndexChanging="gv1_PageIndexChanging"
                        OnRowDeleting = "gv1_RowDeleting" OnRowEditing="gv1_RowEditing" OnRowCancelingEdit="gv1_RowCancelingEdit">                        
			            <Columns>       
                                
                                <asp:TemplateField  SortExpression="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAppID" Text='<%# Eval("Id") %>' Visible="false" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="App Name">
                                    <ItemTemplate >
                                        <asp:Label ID="lblAppname" runat="server" Text='<%# Eval("Appname") %>' ></asp:Label>
                                    </ItemTemplate>                                    
                                        <EditItemTemplate >
                                           <asp:TextBox ID="txtAppName" runat="server" Text='<%# Eval("Appname")%>'  ></asp:TextBox>
                                        </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority">  
                                    <ItemTemplate>                                          
                                        <asp:Label ID="Priority" runat="server" Text='<%# Eval("Priority")%>'></asp:Label>
                                    </ItemTemplate>  
                                    <EditItemTemplate>  
                                          <asp:DropDownList ID="ddlPriority" runat="server" AutoPostBack="true" AppendDataBoundItems="True"></asp:DropDownList>  
                                    </EditItemTemplate>                                                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time Taken(Hrs)">
                                    <ItemTemplate >
                                        <asp:TextBox ID="txtTimeTaken" runat="server" Text='<%# Eval("TimeTaken") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                        <EditItemTemplate >
                                            <asp:TextBox ID="txtTimeTaken" runat="server" Text='<%# Eval("TimeTaken")%>'  ></asp:TextBox>
                                        </EditItemTemplate>
                                </asp:TemplateField>                                 
                                <asp:TemplateField>  
                                    <ItemTemplate>  
                                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                                    </ItemTemplate>  
                                    <EditItemTemplate>  
                                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                                    </EditItemTemplate>  
                                </asp:TemplateField>
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