<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SectionDetails.aspx.cs" Inherits="projSFT.SectionDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .modalBackground {
            background-color:silver;
            opacity:0.7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <table width="100%">
                         <tr>
                            <td align="right" width="70%">
                            <asp:Button ID="btnAddNew1" runat="server" Text="Add New Section" OnClick="btnAddNew_Click" />
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td >
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="ID" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                             <asp:TemplateField HeaderText="Edit">
                                                  <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" Text="Edit" OnClick="lnkEdit_Click" runat="server"></asp:LinkButton>
                                                  </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Delete">
                                                  <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDel" Text="Delete" OnClick="lnkDel_Click" runat="server"></asp:LinkButton>
                                                  </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="ID">
                                               <ItemTemplate >
                                                 <asp:Label runat="server" Text='<%# Eval("ID") %>' ></asp:Label>
                                               </ItemTemplate>
                                             <EditItemTemplate >
                                                 <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("ID")%>'  ></asp:TextBox>
                                             </EditItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="App Name">
                                               <ItemTemplate >
                                                 <asp:Label runat="server" Text='<%# Eval("AppName") %>' ></asp:Label>
                                               </ItemTemplate>
                                             <EditItemTemplate >
                                                 <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("AppName")%>'  ></asp:TextBox>
                                             </EditItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Section">
                                               <ItemTemplate >
                                                 <asp:Label runat="server" Text='<%# Eval("Section") %>' ></asp:Label>
                                               </ItemTemplate>
                                             <EditItemTemplate >
                                                 <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("Section")%>'  ></asp:TextBox>
                                             </EditItemTemplate>
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section Type">
                                               <ItemTemplate >
                                                 <asp:Label ID="Label4" runat="server" Text='<%# Eval("SectionType") %>'></asp:Label>
                                               </ItemTemplate>
                                            <EditItemTemplate >
                                                 <asp:RadioButtonList ID="RadioButtonList1" runat="server" >
                                                   <asp:ListItem>Single</asp:ListItem>
                                                   <asp:ListItem>Married</asp:ListItem>
                                                 </asp:RadioButtonList>
                                            </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time Taken">
                                               <ItemTemplate >
                                                 <asp:Label runat="server" Text='<%# Eval("TimeTaken") %>' ></asp:Label>
                                               </ItemTemplate>
                                             <EditItemTemplate >
                                                 <asp:TextBox ID="TextBox4" runat="server" Text='<%# Eval("TimeTaken")%>'  ></asp:TextBox>
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
 <!--Panel to add new record-->
 <ajaxToolkit:ModalPopupExtender ID="mpe1" runat="server" 
              TargetControlID="btnAddNew1" 
              PopupControlID="panelAddNew" 
              RepositionMode="RepositionOnWindowResizeAndScroll" 
              DropShadow="true" 
              PopupDragHandleControlID="panelAddNewTitle" 
              BackgroundCssClass="modalBackground"  >
 </ajaxToolkit:ModalPopupExtender>
 <asp:Panel ID="panelAddNew" runat="server" style="display:none; background-color:gray;" ForeColor="Black" Width="500" Height="210">
            <asp:Panel ID="panelAddNewTitle" runat="server" style="cursor:move;font-family:Tahoma;padding:2px;" HorizontalAlign="Center" BackColor="Blue" ForeColor="White" Height="25" ><b>Add New</b></asp:Panel>
            <table width="100%" style="padding:5px">
                <tr>
                <td colspan="3">
                    <asp:Label ID="lblStatus1" runat="server"></asp:Label>
                </td>
                </tr>
                <tr>
                    <td><b>Enter App Name</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtName1" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Enter Name" Display="None"  ControlToValidate="txtName1"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce1" TargetControlID="rfv1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Enter Section</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtSection" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Enter Section"  Display="None" ControlToValidate="txtSection"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce2" runat="server" TargetControlID="rfv2" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Select Section Type</b></td>
                    <td><b>:</b></td>
                    <td><asp:RadioButtonList ID="RadioButtonList1" runat="server" >
                            <asp:ListItem>Fixed</asp:ListItem>
                            <asp:ListItem>Variable</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfv3" runat="server"  ErrorMessage="Seclect Section Type" Display="None" ControlToValidate="RadioButtonList1" ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server"  ID="vce3" TargetControlID="rfv3" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Enter Time Taken</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtTimeTaken" runat="server"></asp:TextBox>                        
                        <asp:RequiredFieldValidator ID="rfv4" runat="server" InitialValue="0" ErrorMessage="txtTimeTaken" Display="None" ControlToValidate="txtTimeTaken"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="vce4" TargetControlID="rfv4" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
            <br />
                <div align="center">
                <asp:Button ID="btnAddNew2" runat="server" Width="70" Text="Add" CausesValidation="false" OnClick="btnAddNew_Click"  ValidationGroup="add"/>
                &nbsp;
                <asp:Button ID="btnCancel1" runat="server" Width="70" Text="Cancel" CausesValidation="false" OnClick="Cancel1_Click" ValidationGroup="add"/>
            </div>
       </asp:Panel>
        <!--End of Panel to add new record-->
        <!--Panel to Edit record-->
 <asp:Button ID="btnDummy1" runat="server" style="display:none"/>   
  <ajaxToolkit:ModalPopupExtender ID="mpe2" runat="server" 
               TargetControlID="btnDummy1" 
               PopupControlID="panelEdit" 
               RepositionMode="RepositionOnWindowResizeAndScroll" 
               DropShadow="true" 
               PopupDragHandleControlID="panelEditTitle" 
               BackgroundCssClass="modalBackground" >
  </ajaxToolkit:ModalPopupExtender>
  <asp:Panel ID="panelEdit" runat="server" style="display:none; background-color:gray;" ForeColor="Black" Width="500" Height="210">
            <asp:Panel ID="panelEditTitle" runat="server" style="cursor:move;font-family:Tahoma;padding:2px;" HorizontalAlign="Center" BackColor="Blue" ForeColor="White" Height="25" ><b>Edit</b></asp:Panel>
            <table width="100%" style="padding:5px">
                <tr>
                <td colspan="3">
                    <asp:Label ID="lblStatus2" runat="server"></asp:Label>
                </td>
                </tr>
                <tr>
                    <td><b>Enter App Name</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtName2" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="txtName2" Display="None" ErrorMessage="Enter Name"  ValidationGroup="edit"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce5" runat="server" TargetControlID="rfv5" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Enter Section</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtSection2" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv6" runat="server"  ControlToValidate="txtSection2" Display="None" ErrorMessage="Enter Section" ValidationGroup="edit"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce6" runat="server" TargetControlID="rfv6"></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Select Section Type</b></td>
                    <td><b>:</b></td>
                    <td><asp:RadioButtonList ID="RadioButtonList2" runat="server" >
                            <asp:ListItem>Fixed</asp:ListItem>
                            <asp:ListItem>Variable</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage="Seclect Section Type" Display="None" ControlToValidate="RadioButtonList2" ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server"  ID="ValidatorCalloutExtender1" TargetControlID="rfv3" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Enter Time Taken</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtTimeTaken2" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="txtTimeTaken2" Display="None" ErrorMessage="Enter Time Taken" ValidationGroup="edit"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce7" runat="server" TargetControlID="rfv7" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
            <br />
                <div align="center">
                <asp:Button ID="btnUpdate" runat="server" Width="70" Text="Update" CausesValidation="false" OnClick="btnUpdate_Click"  ValidationGroup="edit"/>
                &nbsp;
                <asp:Button ID="btnCancel2" runat="server" Width="70" Text="Cancel" CausesValidation="false" OnClick="Cancel2_Click"/>
            </div>
        </asp:Panel>
        <!--End of Panel to edit record-->   
                                        
        <!--Panel to delete record-->
<asp:Button ID="btnDummy2" runat="server" style="display:none"/>           
  <ajaxToolkit:ModalPopupExtender ID="mpe3"  runat="server"  TargetControlID="btnDummy2" PopupControlID="panelDelete" CancelControlID="btnCancel3" RepositionMode="RepositionOnWindowResizeAndScroll" DropShadow="true" PopupDragHandleControlID="panelDeleteTitle" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
  <asp:Panel ID="panelDelete" runat="server" style="display:none; background-color:gray;" ForeColor="Black" Width="400" Height="160">
            <asp:Panel ID="panelDeleteTitle" runat="server" style="cursor:move;font-family:Tahoma;padding:2px;" HorizontalAlign="Center" BackColor="Blue" ForeColor="White" Height="25" ><b>Delete</b></asp:Panel>
            <table width="100%" style="padding:5px">
                <tr>
                <td>
                    <asp:Label ID="lblStatus3" runat="server"></asp:Label>
                </td>
                </tr>
                <tr>
                <td>
                    <b>Are you sure you want to delete section details &nbsp;<asp:Label ID="lblId" runat="server"></asp:Label> &nbsp;record?</b>
                </td>
                </tr>
            </table>
            <br />
                <div align="center">
                <asp:Button ID="btnDelete" runat="server" Width="70" Text="Ok" OnClick="btnDelete_Click" CausesValidation="false"/>
                &nbsp;
                <asp:Button ID="btnCancel3" runat="server" Width="70" Text="Cancel" CausesValidation="false"/>
            </div>
           </asp:Panel>
                       <!--End of Panel to edit record-->
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
