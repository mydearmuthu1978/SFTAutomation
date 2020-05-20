<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="programs.aspx.cs" Inherits="projSFT.programs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cn %>" SelectCommand="SELECT [name] FROM [programs]"></asp:SqlDataSource>
        <asp:Panel ID="Panel1" runat="server" Height="358px" Width="254px">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Height="345px" Width="240px">
                <Columns>
                    <asp:TemplateField HeaderText="PROGRAM" SortExpression="name">                        
                     <ItemTemplate>
                    <asp:LinkButton ID="Lnkbtn1" Text='<%# Bind("name") %>' runat="server"></asp:LinkButton>                     
                      </ItemTemplate>
                      <ItemStyle BorderStyle="None" />
                     </asp:TemplateField>                    
                        
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </form>
</body>
</html>
