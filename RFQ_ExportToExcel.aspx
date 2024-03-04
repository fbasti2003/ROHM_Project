<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_ExportToExcel.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_ExportToExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EXPORT TO EXCEL</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvExport" runat="server"
                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                      HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                <Columns>

                    <asp:TemplateField HeaderText="RFQNO">
                        <ItemTemplate>
                            <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("RhRequester") %>' />
                            <asp:Label ID="lblRFQNo" runat="server" Width="130px" Text='<%# Eval("RhRfqNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESCRIPTION">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("RdDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                        <ItemTemplate>
                            <asp:Label ID="lblSpecsDrawing" runat="server" Text='<%# Eval("RdSpecs") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MAKER">
                        <ItemTemplate>
                            <asp:Label ID="lblMaker" runat="server" Text='<%# Eval("RdMaker") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="CATEGORY">
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("RhCategory") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("GroupBySupplierResponse") %>' Font-Bold="true" />
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="REQUESTED DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblRequestedDate" runat="server" Text='<%# Eval("RhTransactionDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>  
                    
                    <asp:TemplateField HeaderText="SEND DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblSendDate" runat="server" Text='<%# Eval("SendDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField HeaderText="HAS SUPPLIER RESPONSE">
                        <ItemTemplate>
                            <asp:Label ID="lblHasSupplierResponse" runat="server" Text='<%# Eval("SupplierResponded") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>                                
                    
                </Columns>

        </asp:GridView>
    </div>
    </form>
</body>
</html>
