<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="projSFT.Registration" %>

<!DOCTYPE html>
<style type="text/css">
    #main{
        
        height:420px;
        width:720px;
        background-color:aliceblue;
        margin-left:350px;
        margin-right:400px;
        margin-top:100px;
        margin-bottom:auto;
        border:none;
        border-radius: 25px;
    }
    .tbl {
        border-collapse: separate;
        border-spacing: 0 20px;
        margin-left: 30px;
        margin-top: 20px
    }
    </style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
</head>
<body>
    <form id="form1" runat="server" >  
        <div  id ="main">  
            <table border="0" class="tbl" > 
                <tr>  
                    <td></td>  
                    <td>  
                        <asp:Label ID ="lblHeader" runat="server" Text="Registration Form" Font-Bold="true" style="margin-left:40px; margin-top:20px;"/>
                    </td>   
                </tr>  
                <tr>  
                    <td><asp:Label ID="lblUsername" runat="server" Text ="Username:" Width="200px"/></td>  
                    <td>  
                        <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox>  
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Width="200px"   
                             ControlToValidate="txtUsername" ErrorMessage="&nbsp;&nbsp;&nbsp;Please enter your Username."   
                            ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>  
                </tr>   
                <tr>  
                    <td><asp:Label ID="lblFirstname" runat="server" Text ="First name:" Width="120px"/></td>  
                    <td>  
                        <asp:TextBox ID="txtFirstname" runat="server" Width="200px"></asp:TextBox>  
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Width="200px"   
                             ControlToValidate="txtFirstname" ErrorMessage="&nbsp;&nbsp;&nbsp;Please enter your first name."   
                            ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>  
                </tr>  
                <tr>  
                    <td><asp:Label ID="lblLastname" runat="server" Text ="Last name:" Width="120px"/></td>  
                    <td>  
                        <asp:TextBox ID="txtLastname" runat="server" Width="200px"></asp:TextBox>  
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"   Width="200px"  
                             ControlToValidate="txtLastname" ErrorMessage="&nbsp;&nbsp;&nbsp;Please enter your last name."   
                            ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>  
                </tr>
              <tr>                    
                    <td><asp:Label ID="lblPassword" runat="server" Text ="Password:" Width="120px"/></td>  
                    <td>  
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="200px"></asp:TextBox>  
                    </td> 
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"      
                                ControlToValidate="txtPassword" ErrorMessage="&nbsp;&nbsp;&nbsp;Please enter your Password."
                                    ForeColor="Red"/>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtPassword"  width="400px" Height="50px"           
                                ErrorMessage="&nbsp;&nbsp;Minimum 8 characters atleast 1 Alphabet,</br>&nbsp;&nbsp;1 Number and 1 Special Character."   
                                ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$" ForeColor="Red"/> 
                    </td>
                    </tr> 
                <tr>
                    <td><asp:Label ID="lblConfirmPassword" runat="server" Text ="Confirm Password:" Width="120px"/></td>   
                    <td>  
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>  
                    </td> 
                     <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"      
                                ControlToValidate="txtConfirmPassword" ErrorMessage="&nbsp;&nbsp;&nbsp;Please re enter your Password."
                                    ForeColor="Red"/>
                          <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare ="txtPassword" 
                              ErrorMessage="</br>&nbsp;&nbsp;&nbsp;Passwords do not match."
                                    ForeColor="Red"/>
                    </td>
                </tr>
                <tr>                  
                    <td colspan="3">  
                        <asp:Button ID="btnRegister" runat="server" Text="Register" Style="margin-left:220px;" onclick="btnRegister_Click"/>                      
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="margin-left:20px;" CausesValidation="false" onclick="btnCancel_Click"/>  
                    </td>                      
                </tr>  
                 
            </table>  
        </div>  
    </form>  
</body>
</html>
