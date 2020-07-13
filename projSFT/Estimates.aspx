<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ProjectManagement.Master" CodeBehind="Estimates.aspx.cs" Inherits="projSFT.Estimates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<%--<script type="text/javascript" src="jquery-1.11.1.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.1.min.js"></script> 
<script src="jscript/Task.js" type="text/javascript"></script>--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
<style>
    .Hide {
        display:none;
    }
</style>

        <!--<div id ="main" style="width:100%">-->
            <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table border="0" class="tbl"> 
                        <tr>          
                            <td style =" width: 100%"> 
                                <b>Select Application: </b>
                                <asp:DropDownList ID="ddlApplication" runat="server" AutoPostBack = "true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                </asp:DropDownList>          
                            </td>   
                        </tr>
                        <tr>
                            <td align="right" width="70%">
                            <asp:Button ID="btnAddNew1" runat="server" Text="Add New" OnClick="btnAddNew_Click" />
                            <asp:Button ID="btnAddNewApp" runat="server" Visible="false" Text="Add New Application" />
                            <td></td>
                            <td></td>
                        </tr
                        <tr>
                       </tr>
                        <tr>
                            <td style=" width: 100%">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="OnItemDataBound">
                                    <ItemTemplate>
                                        <table border="1" style="width:100%; font-weight:bold; border:solid; border-color:black;">
                                            <tr>
                                                <td>
                                                    <div style="text-align:center">
                                                        <%--<asp:Button ID="lnkEdit" runat="server" Text="Edit Section" CommandName="update" style="color: #800000;font-size: large" />  --%>
                                                        <asp:Button ID="btnEdit" Text="EditSection" OnClick="Edit_Click" runat="server" style="color:darkmagenta;font-size: small"></asp:Button>
                                                        <asp:Button ID="btnDelete" Text="DeleteSection" OnClick="btnDel_Click" runat="server" style="color: darkmagenta;font-size: small"></asp:Button>                                                                  
                
                                                        <%--<asp:HiddenField ID="hfSectionId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id")%>'/>--%>
                                                        <%--//==== Here we have used CommandName property to distinguish which button is 
                                                            clicked and we have passed our primary key to CommandArgument property. ====//--%>
                                                        <%--<asp:imagebutton id="imgBtnEdit" commandname="Edit" tooltip="Edit a record." runat="server" imageurl="edit.png"></asp:imagebutton>
                                                        <asp:imagebutton tooltip="Delete a record." onclientclick="javascript:return confirm('Are you sure to delete record?')" id="imgBtnDelete" commandname="Delete" runat="server" imageurl="delete.png"></asp:imagebutton>--%>
                                                    </div>
                                                </td>
                                            </tr>                                                                                        
                                            <tr >
                                                <asp:HiddenField ID="hfSectionId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id")%>'/>
                                                <td style="width:100%">Section Name: <%# DataBinder.Eval(Container.DataItem, "SecName")%></td>
                                            </tr>
                                            <tr>
                                                <td style="width:100%">Section Type: <%# DataBinder.Eval(Container.DataItem, "SecType")%></td>
                                            </tr>
                                            <tr>
                                                <td style="width:100%">Total Test Case: <%# DataBinder.Eval(Container.DataItem, "TotalTestCases")%></td>
                                            </tr>                                           
                                            <tr style="border-color:black;border:2px">
                                                <td>
                                                    <br />
                                                    <asp:Repeater ID="Repeater2" runat="server">
                                                        <HeaderTemplate>
                                                            <table border="1" style="width:100%">
                                                                
                                                                    <tr>
                                                                        <asp:HiddenField ID="hfTaskId" runat="server" Visible="false" />
                                                                        <td><b>Task Name</b> </td>
                                                                        <td><b>Time Taken</b> </td>                                                                        
                                                                    </tr>
                                                                
                                                             </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table border="0" style="width:100%">
                                                                <tr>
                                                                    <td style="visibility:hidden"><%# DataBinder.Eval(Container.DataItem, "Id")%></td>
                                                                    <td><%# DataBinder.Eval(Container.DataItem, "TaskName")%></td>
                                                                    <td><%# DataBinder.Eval(Container.DataItem, "TimeTaken")%></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
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
              BackgroundCssClass="modalBackground">
 </ajaxToolkit:ModalPopupExtender>
 <asp:Panel ID="panelAddNew" ScrollBars="Vertical" runat="server" style="display:none; background-color:gray;" ForeColor="Black" Width="560" Height="380">
            <asp:Panel ID="panelAddNewTitle" runat="server" style="cursor:move;font-family:Tahoma;padding:2px;" HorizontalAlign="Center" BackColor="#cc00cc" ForeColor="White" Height="25" ><b>Add Section/Task</b></asp:Panel>
            <table width="100%" style="padding:5px">
                
                <tr>
                <td colspan="3">
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
                </tr>
                <tr>
                    <td style="visibility:hidden"><b>SectionId</b></td>                    
                    <td>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="Id" Visible="false"/>
                    </td>
                </tr>
                <tr>
                    <td><b>Section Name</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtSectionname" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Enter Section Name" Display="None"  ControlToValidate="txtSectionname"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce1" TargetControlID="rfv1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>                
                <tr>
                    <td><b>Section Type</b></td>
                    <td><b>:</b></td>
                    <td><asp:RadioButtonList ID="RadioButtonList1" runat="server" >
                            <asp:ListItem Enabled="true">Fixed</asp:ListItem>
                            <asp:ListItem>Variable</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfv3" runat="server"  ErrorMessage="Select Section Type" Display="None" ControlToValidate="RadioButtonList1" ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server"  ID="vce3" TargetControlID="rfv3" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Total Test Case</b></td>                    
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtTotaltestcase" runat="server"></asp:TextBox>                        
                        <asp:RequiredFieldValidator ID="rfv4" runat="server" InitialValue="0" ErrorMessage="txtTotaltestcase" Display="None" ControlToValidate="txtTotaltestcase"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="vce4" TargetControlID="rfv4" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                </table>
                <br />                
                 <table>
                     <tr>
                        <td colspan="3">                            
                            <asp:Label ID="lblStatus1" runat="server"></asp:Label>                           
                        </td>
                       </tr>
                     <div align="center">
                        <asp:Button ID="btnNewRow" runat="server" Text="Add New Row" OnClick="btnAddNewRow_Click" />
                        <asp:Button ID="btnAddNew2" runat="server" Width="70" Text="Add" OnClick="btnAddNew_Click" />&nbsp;                        
                        <asp:Button ID="btnCancel1" runat="server" Width="70" Text="Cancel" CausesValidation="false" OnClick="Cancel1_Click" ValidationGroup="add"/>
                    </div>
                 </table>
                <asp:Table ID="TabletaskHeader" runat="server" ForeColor="Black" Width="400" BorderColor="black" BorderWidth="2" CellPadding="2" CellSpacing="2">
                    
                    <asp:TableHeaderRow runat="server" Font-Bold="true">
                        <asp:TableHeaderCell>Task</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Time Taken</asp:TableHeaderCell>                        
                    </asp:TableHeaderRow>

                </asp:Table>  
                    <asp:PlaceHolder ID="plcholder" runat="server"></asp:PlaceHolder>                   
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
  <asp:Panel ID="panelEdit" ScrollBars="Vertical" runat="server" style="display:none; background-color:gray;" ForeColor="Black" Width="560" Height="380">
            <asp:Panel ID="panelEditTitle" runat="server" style="cursor:move;font-family:Tahoma;padding:2px;" HorizontalAlign="Center" BackColor="#cc00cc" ForeColor="White" Height="25" ><b>Edit Section/Task</b></asp:Panel>
            <table width="100%" style="padding:5px">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblStatus2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="visibility:hidden"><b>SectionId</b></td>                    
                    <td>
                        <asp:HiddenField ID="hfSectionIdEdit" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td><b>Section Name</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtEditSectionName" runat="server"></asp:TextBox>                        
                        <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="txtEditSectionName" Display="None" ErrorMessage="Enter Section Name"  ValidationGroup="edit"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce5" runat="server" TargetControlID="rfv5" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>                
                <tr>
                    <td><b>Section Type</b></td>
                    <td><b>:</b></td>
                    <td><asp:RadioButtonList ID="RadioButtonList2" runat="server" >
                            <asp:ListItem>Fixed</asp:ListItem>
                            <asp:ListItem>Variable</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ErrorMessage="Seclect Section Type" Display="None" ControlToValidate="RadioButtonList2" ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server"  ID="ValidatorCalloutExtender2" TargetControlID="rfv3" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Total Test Cases</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtEdittestcases" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="txtEdittestcases" Display="None" ErrorMessage="Enter Test Cases" ValidationGroup="edit"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce7" runat="server" TargetControlID="rfv7" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>            
            <table>
                <tr>
                    <td colspan="3">                            
                        <asp:Label ID="Label3" runat="server"></asp:Label>                           
                    </td>
                </tr>
                <div align="center">
                    <asp:Button ID="btnEditNewRow" runat="server" Text="Add New Row" OnClick="btnAddNewRow2_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Width="70" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="edit"/>                
                    <asp:Button ID="btnCancel2" runat="server" Width="70" Text="Cancel" CausesValidation="false" OnClick="Cancel2_Click"/>
               </div>
            </table>
           <asp:Table ID="Table1" runat="server" ForeColor="Black" Width="400" BorderColor="black" BorderWidth="2" CellPadding="2" CellSpacing="2">
               <asp:TableHeaderRow runat="server" Font-Bold="true">
                   <%--<asp:TableCell>
                       <asp:HiddenField ID="hfTaskIdEdit" runat="server"></asp:HiddenField>
                   </asp:TableCell> --%>                  
                   <asp:TableHeaderCell>Task</asp:TableHeaderCell>
                   <asp:TableHeaderCell>Time Taken</asp:TableHeaderCell>
                   <asp:TableHeaderCell>ChkDelete</asp:TableHeaderCell>
               </asp:TableHeaderRow>
           </asp:Table>
      <asp:PlaceHolder ID="EditPlaceHolder" runat="server"></asp:PlaceHolder>
</asp:Panel>
<!--End of Panel to edit record-->
<!--Panel to delete record-->
<asp:Button ID="btnDummy2" runat="server" style="display:none"/>           
  <ajaxToolkit:ModalPopupExtender ID="mpe3"  runat="server"  TargetControlID="btnDummy2" PopupControlID="panelDelete" CancelControlID="btnCancel3" RepositionMode="RepositionOnWindowResizeAndScroll" DropShadow="true" PopupDragHandleControlID="panelDeleteTitle" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
  <asp:Panel ID="panelDelete" runat="server" style="display:none; background-color:gray;" ForeColor="Black" Width="400" Height="160">
            <asp:Panel ID="panelDeleteTitle" runat="server" style="cursor:move;font-family:Tahoma;padding:2px;" HorizontalAlign="Center" BackColor="#cc00cc" ForeColor="White" Height="25" ><b>Delete Section & Related Task</b></asp:Panel>
            <table width="100%" style="padding:5px">
                <tr>
                <td>
                    <asp:Label ID="lblStatus3" runat="server"></asp:Label>
                </td>
                </tr>
                <tr>
                    <td style="visibility:hidden"><b>SectionId</b></td>                    
                    <td>
                        <asp:HiddenField ID="hfDectionIdDel" runat="server"/>
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
                <asp:Button ID="btnDelete" runat="server" Width="70" Text="Delete" OnClick="btnDelete_Click" CausesValidation="false"/>
                &nbsp;
                <asp:Button ID="btnCancel3" runat="server" Width="70" Text="Cancel" CausesValidation="false"/>
            </div>
           </asp:Panel>
        <asp:PlaceHolder ID="DeletePlaceHolder" runat="server"></asp:PlaceHolder>
<!--End of Panel to edit record-->
<!--Panel to add new application-->
<asp:Button ID="btnDummy3" runat="server" style="display:none"/> 
 <ajaxToolkit:ModalPopupExtender ID="mpe4" runat="server" 
              TargetControlID="btnAddNewApp" 
              PopupControlID="panelAddNewApplication" 
              RepositionMode="RepositionOnWindowResizeAndScroll" 
              DropShadow="true" 
              PopupDragHandleControlID="panelAddNewAppTitle" 
              BackgroundCssClass="modalBackground"  >
 </ajaxToolkit:ModalPopupExtender>
 <asp:Panel ID="panelAddNewApplication" ScrollBars="Vertical" runat="server" style="display:none; background-color:gray;" ForeColor="Black" Width="560" Height="380">
            <asp:Panel ID="panelAddNewAppTitle" runat="server" style="cursor:move;font-family:Tahoma;padding:2px;" HorizontalAlign="Center" BackColor="#cc00cc" ForeColor="White" Height="25" ><b>Add New Application</b></asp:Panel>
            <table width="100%" style="padding:5px">
                
                <tr>
                <td colspan="3">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                </tr>
                <tr>
                    <td style="visibility:hidden"><b>AppId</b></td>                    
                    <td>
                        <asp:HiddenField ID="hfAppId" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td><b>Application Name</b></td>  
                    <td><b>:</b></td>
                    <td>
                        <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Application Name" Display="None"  ControlToValidate="txtAppName"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="rfv1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                    </td>                   
                </tr>
                <tr>
                    <td><b>Priority</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:DropDownList ID="ddlPriority1" runat="server" Text='<%#Eval("Priority") %>' AppendDataBoundItems="True">
                            <asp:ListItem Value="Full Functional" Text="Full Functional"></asp:ListItem>
                            <asp:ListItem Value="Sanity" Text="Sanity"></asp:ListItem>
                            <asp:ListItem Value="Regression" Text="Regression"></asp:ListItem>
                            <asp:ListItem Value="PML" Text="PML"></asp:ListItem>
                        </asp:DropDownList>
                    </td>                    
                </tr>
                <tr>
                    <td style="visibility:hidden"><b>SectionId</b></td>                    
                    <td>
                        <asp:HiddenField ID="hfSectionIdApp" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td><b>Section Name</b></td>
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtAppSectionName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Section Name" Display="None"  ControlToValidate="txtAppSectionName"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfv1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>                
                <tr>
                    <td><b>Section Type</b></td>    
                    <td><b>:</b></td>
                    <td><asp:RadioButtonList ID="rblSectionType" runat="server" >
                            <asp:ListItem Enabled="true">Fixed</asp:ListItem>
                            <asp:ListItem>Variable</asp:ListItem>
                        </asp:RadioButtonList>                        
                        <ajaxToolkit:ValidatorCalloutExtender runat="server"  ID="ValidatorCalloutExtender3" TargetControlID="rfv3" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td style="visibility:hidden"><b>AppId</b></td>                    
                    <td>
                        <asp:Label ID="lbl_AppID" runat="server" Text='<%# Eval("Id") %>' Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td><b>Total Test Cases</b></td>  
                    <td><b>:</b></td>
                    <td><asp:TextBox ID="txtAppTotalTestCases" runat="server"></asp:TextBox>                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" ErrorMessage="Enter Total test cases" Display="None" ControlToValidate="txtAppTotalTestCases"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4" TargetControlID="rfv4" ></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                </table>
                <br />                
                 <table>
                     <tr>
                        <td colspan="3">                            
                            <asp:Label ID="Label2" runat="server"></asp:Label>                           
                        </td>
                       </tr>
                     <div align="center">
                        <asp:Button ID="btnAddNewAppRow" runat="server" Text="Add New Row" OnClick="btnAddNewAppRow_Click" />
                        <asp:Button ID="btnAddAppSectionTask" runat="server" Width="70" Text="Add" CausesValidation="false" OnClick="btnAddAppSectionTask_Click"  ValidationGroup="add"/>
                        <asp:Button ID="btnAppSectionTaskCancel" runat="server" Width="70" Text="Cancel" CausesValidation="false" OnClick="AppSectionTaskCancel_Click" ValidationGroup="add"/>
                    </div>
                 </table>
                <asp:Table ID="TabletaskHeader4" runat="server" ForeColor="Black" Width="400" BorderColor="black" BorderWidth="2" CellPadding="2" CellSpacing="2">                    
                    <asp:TableHeaderRow runat="server" Font-Bold="true">
                        <asp:TableHeaderCell>Task</asp:TableHeaderCell>
                        <asp:TableCell Visible="false" Text='<%# Eval("Id") %>'>SecId</asp:TableCell>
                        <asp:TableHeaderCell>Time Taken</asp:TableHeaderCell>                        
                    </asp:TableHeaderRow>
                </asp:Table>  
                    <asp:PlaceHolder ID="NewAppPlaceHolder" runat="server"></asp:PlaceHolder>                   
</asp:Panel>
<!--End of Panel to add new record-->
</ContentTemplate>
</asp:UpdatePanel>       
</asp:Content>

