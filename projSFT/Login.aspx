<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="projSFT.Login" %>

<!DOCTYPE html>
<style type="text/css">
    #main{
        height:200px;
        width:600px;
        background-color:aliceblue;
        margin-left:400px;
        margin-right:auto;
        margin-top:200px;
        margin-bottom:auto;
        border:none;
        border-radius: 25px;
    }
    </style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>   
<body>  
    <form id="form1" runat="server">  
        <div id ="main" >  
            <table border="0" style="margin-left:30px; margin-top:20px;">  
                <tr>  
                    <td></td>  
                    <td>  
                        <asp:Label ID ="lblHeader" runat="server" Text="Log In" Font-Bold="true" style="margin-left:60px; margin-top:20px;"/>
                    </td>   
                </tr>
                <tr>   <td><asp:Label ID="lbl" runat="server"  Width="120px"/></td> </tr>
                <tr>  
                    <td><asp:Label ID="lblUsername" runat="server" Text ="Username:" Width="120px"/></td>  
                    <td>  
                        <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox>  
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"   
                             ControlToValidate="txtUsername" ErrorMessage="Please enter your Username"   
                            ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>  
                </tr>  
                <tr>                    
                    <td><asp:Label ID="lblPassword" runat="server" Text ="Password:" Width="120px"/></td>  
                    <td>  
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="200px"></asp:TextBox>  
                    </td> 
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"      
                                ControlToValidate="txtPassword" ErrorMessage="Please enter your Password"
                                    ForeColor="Red"/>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtPassword"  
                                    ErrorMessage="Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character." 
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$" ForeColor="Red"/> 
                    </td>
                    </tr>
                <tr>  
                    <td>  <asp:Label ID="lblError" Width="150px" runat="server"></asp:Label>  </td>  
                    <td class="style2" >  
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Style="margin-left:30px;margin-top:20px;" onclick="btnSubmit_Click" /> 
                        <asp:Button ID="btnRegister" runat="server" Text="Register"  Style="margin-left:20px;" OnClick="btnRegister_Click"/> 
                    </td>   
                </tr> 
            </table>  
        </div>  
    </form>  
</body> 
</html>
