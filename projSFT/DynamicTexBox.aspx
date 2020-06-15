<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicTexBox.aspx.cs" Inherits="projSFT.DynamicTexBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
        
    <div>
      <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" 
            AutoGenerateColumns="false" onrowcreated="Gridview1_RowCreated">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
            <asp:TemplateField HeaderText="Header 1">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Header 2">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Header 3">
                <ItemTemplate>
                     <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                 <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" 
                        onclick="ButtonAdd_Click" />
                </FooterTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Header 3">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Click Me"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:gridview>
        <asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click" />
    </div>
    
    </form>
</body>
</html>
