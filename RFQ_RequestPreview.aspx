<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_RequestPreview.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_RequestPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%; font-family:Tahoma;">
          <tr>
            <td style="width:50%;"><p style="font-size:16px;"><b>ROHM Electronics Philippines, Inc.</b></p><p style="font-size:12px; margin-top:-15px;">TEL. :(632) 894-1536 FAX : (6346) 430-2892 / 430-2311</p></td>
            <td><p style="font-size:14px;"><b>RFQ No. <asp:Label ID="lblRFQNo" runat="server" /> </b></p></td>
          </tr>
          <tr>
            <td style="width:200px;">
                <table style="width:100%; font-family:Tahoma;">
                    <tr>
                        <td style="width:100%;">
                            <p style="font-size:18px;">REQUEST FOR QUOTATION (RFQ)</p> 
                            <p style="font-size:12px; margin-top:-15px;"><b>DIVISION</b> : <asp:Label ID="lblDivision" runat="server" /></p> 
                            <p style="font-size:12px; margin-top:-15px;"><b>DEPARTMENT</b> : <asp:Label ID="lblDepartment" runat="server" /></p> 
                            <p style="font-size:12px; margin-top:-15px;"><b>SECTION</b> : <asp:Label ID="lblSection" runat="server" /></p>
                            <p style="font-size:12px; margin-top:-15px;"><b>EMAIL</b> : <asp:Label ID="lblEmailAddress" runat="server" ForeColor="Blue" /></p>
                        </td>
                    </tr>
                </table>                   
            </td>
            <td>
                <table style="width:100%; font-family:Tahoma;">
                    <tr>
                        <td style="width:50%;">
                            <p style="font-size:16px;">REQUESTING DIVISION</p>
                            <p style="font-size:12px; margin-top:-10px; font-weight:bold;">REQUESTER</p> 
                            <p style="font-size:12px; margin-top:-10px;"><asp:Label ID="lblRequester" runat="server" /></p> 
                            <p style="font-size:12px; margin-top:-10px; font-weight:bold;">MANAGER</p>
                            <p style="font-size:12px; margin-top:-10px;"><asp:Label ID="lblManager" runat="server" /></p> 
                        </td>
                        <td style="width:50%;">
                            <p style="font-size:16px;">SUPPLY CHAIN MANAGEMENT DIVISION</p>
                            <p style="font-size:12px; margin-top:-10px; font-weight:bold;">INCHARGE</p> 
                            <p style="font-size:12px; margin-top:-10px;"><asp:Label ID="lblIncharge" runat="server" Text="-" /></p>
                            <p style="font-size:12px; margin-top:-10px; font-weight:bold;">BUYER-SENDER</p> 
                            <p style="font-size:12px; margin-top:-10px;"><asp:Label ID="lblSender" runat="server" /></p>
                        </td>
                    </tr>
                </table>
            </td>
            
          </tr>
          
          <tr>
            <td>
                <p style="font-size:12px; margin-top:5px;"><asp:Label ID="lblTransferDetails" Visible="false" runat="server" /></p>
            </td>
          </tr>
          
        </table>
    </div>
    
    <div style="font-family:Tahoma;">
    
        <asp:GridView ID="gvData" runat="server"
                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                      HeaderStyle-Font-Bold="false" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" >
            <Columns>

                <asp:TemplateField HeaderText="NO.">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server" Width="20px" Text='<%# Eval("RdNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DESCRIPTION" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Width="210px" Text='<%# Eval("RdDescription") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SPECS/DRAWING NO." HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSpecsDrawing" runat="server" Width="165px" Text='<%# Eval("RdSpecs") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MAKER" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblMaker" runat="server" Width="150px" Text='<%# Eval("RdMaker") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QTY">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Width="40px" Text='<%# Eval("RdQuantity") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" Width="30px" Text='<%# Eval("RdUnitOfMeasure") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRICE" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Width="110px" Text='<%# Eval("RdResponsePrice") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DELIVERY<br />LEAD TIME<br />(WORKING DAYS)">
                    <ItemTemplate>
                        <asp:Label ID="lblLeadTime" runat="server" Text='<%# Eval("RdResponseLead") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SUPPLIER |<br /> RESPONSE DATE" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplier" runat="server" Width="150px" Text='<%# Eval("RdSupplierName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SPECIFIC PURPOSE OF THE <br /> ITEM TO THE MACHINE FOR <br/> LIST OF IMPORTABLE" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSpecificPurpose" runat="server" Width="150px" Text='<%# Eval("RdPurpose") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WHAT PROCESS AND <br /> PARTICULAR MACHINE?" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblWhatProcess" runat="server" Width="150px" Text='<%# Eval("RdProcess") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WITH / <br /> ATTACHMENT" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblWithAttachment" runat="server" Width="120px" Text='<%# Eval("RdAttachmentYN") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SUPPLIER REMARKS" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplierRemarks" runat="server" Width="130px" Text='<%# Eval("RdResponseRemarks") %>' />                                       
                    </ItemTemplate>                   
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SCM REMARKS" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasingRemarks" runat="server" Width="130px" Text='<%# Eval("RdPurchasingRemarks") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            
            </Columns>

        </asp:GridView>
    
    </div>
    
    <div style="margin-top:20px; font-family:Tahoma; font-size:28px; color:Blue;">
        <table style="width:100%;">
            <tr>
                <td>
                    <div style="margin-left:40%;">
                        APPROVED QUOTATION
                    </div>
                </td>
            </tr>
        </table>
    </div>
    
    <div style="margin-top:20px; font-family:Tahoma; font-size:12px;">
        <table style="width:100%;">
            <tr>
                <td>
                    <%= approver_Buyer %>                   
                </td>
                <td>
                    <%= approver_Incharge %>
                </td>
                <td>
                    <%= approver_DeptManager %>
                </td>
                <td>
                    <%= approver_DivManager %>
                </td>
            </tr>            
        </table>
    </div>
    
    </form>
</body>
</html>
