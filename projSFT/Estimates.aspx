<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ProjectManagement.Master" CodeBehind="Estimates.aspx.cs" Inherits="projSFT.Estimates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<%--<script type="text/javascript" src="jquery-1.11.1.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.11.1.min.js"></script> 
<script src="jscript/Task.js" type="text/javascript"></script>--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    


        <!--<div id ="main" style="width:100%">-->
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                            <td></td>
                            <td></td>
                        </tr
                        <tr>
                       </tr>
                        <tr>
                            <td style=" width: 100%">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="OnItemDataBound" >
                                    <ItemTemplate>
                                        <table border="1" style="width:100%; font-weight:bold; border:solid; border-color:black;">
                                            <tr>
                                                <td>
                                                    <div align="center">
                                                        <asp:Button runat="server" Text="Edit Section" />
                                                        <asp:Button runat="server" Text="Delete Section" />
                                                        <%--<asp:HiddenField ID="hfSectionId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id")%>'/>--%>
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
                                                            <table>
                                                                
                                                                    <tr>
                                                                        <td><b>Task Name</b> </td>
                                                                        <td><b>Time Taken</b> </td>
                                                                    </tr>
                                                                
                                                             </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table border="1" cellspacing="0" rules="all">
                                                            <tbody>
                                                                    <tr class="altRow">
                                                                <td>
                                                                    <%# DataBinder.Eval(Container.DataItem, "TaskName")%></td>
                                                                <td><%# DataBinder.Eval(Container.DataItem, "TimeTaken")%></td>
                                                            </tr></tbody></table>
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
              BackgroundCssClass="modalBackground"  >
 </ajaxToolkit:ModalPopupExtender>
 <asp:Panel ID="panelAddNew" ScrollBars="Auto" runat="server" style="display:none; background-color:gray;" ForeColor="Black" Width="520" Height="380">
            <asp:Panel ID="panelAddNewTitle" runat="server" style="cursor:move;font-family:Tahoma;padding:2px;" HorizontalAlign="Center" BackColor="#cc00cc" ForeColor="White" Height="25" ></asp:Panel>
            <table style="padding:5px" border="1">
                <tr>  
                    <td class="style1">  
                        <strong style="background-color: #999999">Section Block</strong>
                   </td>  
                </tr>
                <%--<tr>
                <td colspan="3">
                    <asp:Label ID="lblStatus1" runat="server"></asp:Label>
                </td>
                </tr>--%>
                <tr>
                    <td style="visibility:hidden"><b>SectionId</b></td>
                    <%--<td><b>:</b></td>--%>
                    <td><asp:TextBox ID="tblSectionId" runat="server" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Section Id" Display="None"  ControlToValidate="tblSectionId"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfv1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr>
                    <td><b>Section Name</b></td>
                    <%--<td><b>:</b></td>--%>
                    <td><asp:TextBox ID="txtSectionname" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Enter Section Name" Display="None"  ControlToValidate="txtSectionname"  ValidationGroup="add"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="vce1" TargetControlID="rfv1" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                </tr>                
                <tr>
                    <td><b>Section Type</b></td>
                    <%--<td><b>:</b></td>--%>
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
                    <%--<td><b>:</b></td>--%>
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
                     <div style="text-align:center">
                        <asp:Button ID="btnNewRow" runat="server" Text="Add New Row" OnClick="btnAddNewRow_Click" />
                        <asp:Button ID="btnAddNew2" runat="server" Width="70" Text="Update" OnClick="btnAddNew_Click" />&nbsp;                        
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
</ContentTemplate>
</asp:UpdatePanel>       
</asp:Content>

