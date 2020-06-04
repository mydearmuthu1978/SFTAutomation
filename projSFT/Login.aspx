<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="projSFT.Login" %>

<!DOCTYPE html>
<style type="text/css">
    </style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="Stylesheet" href="css/master.css" type="text/css" />
</head>   
<body>  
    <form id="form1" runat="server" > 
           <div  class="header" > 
                  <asp:Label ID ="lblHeader" runat="server" Text="Log In"  Font-Bold="true" />
           </div>
        <div class="body">
        <div class="main">
            <table border="0" class="tbl">              
                <tr>                      
                    <td><asp:Label ID="lblUsername" runat="server" Text ="Username: " Width="180px" ForeColor="#9353A1" Font-Bold="true" /></td>  
                    <td>  
                        <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox>  
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"   
                             ControlToValidate="txtUsername" ErrorMessage="Please enter your Username." Font-Italic="true"
                            ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>  
                </tr>  
                 <tr>                    
                    <td><asp:Label ID="lblPassword" runat="server" Text ="Password: " Width="180px" ForeColor="#9353A1" Font-Bold="true"/></td>  
                    <td>  
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="200px"></asp:TextBox>  
                    </td> 
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"      
                                ControlToValidate="txtPassword" ErrorMessage="Please enter your Password." Font-Italic="true"
                                    ForeColor="Red"/>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtPassword"  
                                    ErrorMessage="</br>Minimum 8 characters atleast 1 Alphabet,</br> 1 Number and 1 Special Character." Font-Italic="true"
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$" ForeColor="Red"/> 
               </td>
                    </tr>
                <tr>  
                    <td>  <asp:Label ID="lblError"  runat="server" Font-Italic="true" ForeColor="Red"></asp:Label></td>  
                    <td>  
                        <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" Style="margin-top:20px;" onclick="btnSubmit_Click" /> 
                        <asp:Button ID="btnRegister" CssClass="button" runat="server" Text="Register"  Style="margin-left:10px;" CausesValidation="false" OnClick="btnRegister_Click"/> 
                    </td>   
                </tr> 
            </table>              
        </div>
            </div>
       <div class="footer">
           <img src ="images/mphasis_bottom-logo.png">
        </div>    
        </form> 
</body> 
</html>
