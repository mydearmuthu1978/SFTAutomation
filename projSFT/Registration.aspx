<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="projSFT.Registration" %>

<!DOCTYPE html>
<style type="text/css">
    </style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
     <link rel="Stylesheet" href="css/master.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" >  
                   <div class="header">  
                         <asp:Label ID ="lblHeader" runat="server" Text="Registration Form" Font-Bold="true" style="margin-left:40px; margin-top:20px;"/>
                   </div> 
          <div class="body"> 
               <div class="main">   
              <table border="0" class="tbl" >          
                <tr>  
                    <td><asp:Label ID="lblUsername" runat="server" Text ="Username:" Width="200px" ForeColor="#9353A1" Font-Bold="true"/></td>  
                    <td>  
                        <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox>  
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"   
                             ControlToValidate="txtUsername" ErrorMessage="&nbsp;&nbsp;&nbsp;Please enter your Username."  Font-Italic="true"
                            ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>  
                </tr>   
                <tr>  
                    <td><asp:Label ID="lblFirstname" runat="server" Text ="First Name:" Width="200px" ForeColor="#9353A1" Font-Bold="true"/></td>  
                    <td>  
                        <asp:TextBox ID="txtFirstname" runat="server" Width="200px"></asp:TextBox>  
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"   
                             ControlToValidate="txtFirstname" ErrorMessage="&nbsp;&nbsp;&nbsp;Please enter your First name."  Font-Italic="true" 
                            ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>  
                </tr>  
                <tr>  
                    <td><asp:Label ID="lblLastname" runat="server" Text ="Last Name:" Width="200px" ForeColor="#9353A1" Font-Bold="true"/></td>  
                    <td>  
                        <asp:TextBox ID="txtLastname" runat="server" Width="200px"></asp:TextBox>  
                    </td>  
                    <td>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  
                             ControlToValidate="txtLastname" ErrorMessage="&nbsp;&nbsp;&nbsp;Please enter your Last name."   Font-Italic="true"
                            ForeColor="Red"></asp:RequiredFieldValidator>  
                    </td>  
                </tr>
                    <tr>
                     <td><asp:Label ID="lblUserType" runat="server" Text ="User Type:" Width="200px" ForeColor="#9353A1" Font-Bold="true"/></td>   
                    <td>
                        <asp:DropDownList ID="drpUserType" runat="server" Width="200px">
                            <asp:ListItem Text="Select User Type" Selected="True"/>
                            <asp:ListItem Text="Mphasis_Admin"></asp:ListItem>
                            <asp:ListItem Text="Mphasis_Developer"></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                     <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"      
                                ControlToValidate="drpUserType" ErrorMessage="&nbsp;&nbsp;&nbsp;Please make a selection." Font-Italic="true" InitialValue="Select User Type"
                                    ForeColor="Red"/>
                         </td>
                </tr>
              <tr>                    
                    <td><asp:Label ID="lblPassword" runat="server" Text ="Password:" Width="200px" ForeColor="#9353A1" Font-Bold="true"/></td>  
                    <td>  
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="200px"></asp:TextBox>  
                    </td> 
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"      
                                ControlToValidate="txtPassword" ErrorMessage="&nbsp;&nbsp;Please enter your Password." Font-Italic="true"
                                    ForeColor="Red"/>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtPassword"           
                                ErrorMessage="</br>&nbsp;&nbsp;Minimum 8 characters atleast 1 Alphabet,</br>1 Number and 1 Special Character."   Font-Italic="true"
                                ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$" ForeColor="Red"/>                       
                        
                    </td>
                    </tr> 
                <tr>
                    <td><asp:Label ID="lblConfirmPassword" runat="server" Text ="Confirm Password:" Width="200px" ForeColor="#9353A1" Font-Bold="true"/></td>   
                    <td>  
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>  
                    </td> 
                     <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"      
                                ControlToValidate="txtConfirmPassword" ErrorMessage="&nbsp;&nbsp;&nbsp;Please re enter your Password." Font-Italic="true"
                                    ForeColor="Red"/>
                          <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare ="txtPassword" 
                              ErrorMessage="</br>&nbsp;&nbsp;&nbsp;Passwords do not match." Font-Italic="true"
                                    ForeColor="Red"/>
                    </td>
                    </tr>

                <tr>                  
                    <td colspan="3">  
                        <asp:Button ID="btnRegister" runat="server" CssClass="button" Text="Register" Style="margin-left:200px;"  onclick="btnRegister_Click"/>                      
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" Style="margin-left:20px;" CausesValidation="false" onclick="btnCancel_Click"/>  
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
