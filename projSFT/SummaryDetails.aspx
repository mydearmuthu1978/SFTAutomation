<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryDetails.aspx.cs" Inherits="projSFT.SummaryDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">  
    <div>      
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="6" onrowdatabound="GridView1_RowDataBound">
            
            <Columns>  
                <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="ID">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Name">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Appname") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("Appname") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Priority">                       
                    <ItemTemplate>  
                        <asp:Label ID="lblPriority" runat="server" Text='<%# Eval("Priority")%>'></asp:Label>                         
                    </ItemTemplate>    
                    <EditItemTemplate>  
                        <asp:DropDownList ID="ddlPriority" runat="server"></asp:DropDownList>  
                    </EditItemTemplate> 
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="City">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_City" runat="server" Text='<%#Eval("TimeTaken") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txt_City" runat="server" Text='<%#Eval("TimeTaken") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                </asp:TemplateField>  
            </Columns>  
            <HeaderStyle BackColor="#663300" ForeColor="#ffffff"/>  
            <RowStyle BackColor="#e7ceb6"/>  
        </asp:GridView>  
      
    </div>  
</form> 
</body>
</html>
