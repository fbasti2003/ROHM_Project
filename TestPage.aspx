<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="REPI_PUR_SOFRA.TestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtUsername" runat="server" />
    <asp:Button ID="btnShowPassword" runat="server" Text="SHOW" OnClick="btnShowPassword_Click" />
    
    <asp:GridView ID="gvForApproval" runat="server" AutoGenerateColumns="false" ShowHeader="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                                                                           
                             EmptyDataText="NO FOR APPROVAL YET">
        <Columns>
            <asp:TemplateField HeaderText="TRANSACTION NAME" HeaderStyle-Width="265px" ItemStyle-HorizontalAlign="Left" >
                <ItemTemplate>
                    <asp:Label ID="lblTransactionName" runat="server" Height="15px" Width="265px" Text='<%# Eval("TransactionName") %>'  />                                                
                </ItemTemplate> 
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="NO. OF FOR APPROVAL" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" > 
                <ItemTemplate>
                    <asp:Label ID="lblNoOfForApproval" runat="server" Text='<%# Eval("ForApprovalCount") %>' Height="15px" Width="50px" Font-Size="11px" Visible="false" /> 
                    <asp:LinkButton ID="linkNoOfForApproval" runat="server" Text='<%# Eval("ForApprovalCount") %>' Height="15px" Width="50px" Font-Size="11px" CommandName="linkNoOfForApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                              
                </ItemTemplate>                                
            </asp:TemplateField>
        </Columns>                                                                                                                                               
        
    </asp:GridView>
                                            
    </div>
    </form>
</body>
</html>
