<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_RequestPrint.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_RequestPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SRF Print Request</title>
    
    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" type="text/css" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" type="text/css" />
    <link href="plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/themes/all-themes.css" rel="stylesheet" type="text/css" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" type="text/css" /> 
    
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="plugins/sweetalert/sweetalert.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function SuccessMessage(msg) {
            swal({
                title: "SUCCESS",
                text: msg,
                type: "success"
            });
        }
        
        function ErrorMessage(msg) {
            swal({
                title: 'ERROR!',
                text: msg,
                type: 'warning'
            });
        }
        
        function WarningMessage(msg) {
            swal({
                title: 'WARNING!',
                text: msg,
                type: 'warning'
            });
        }       
        
    </script>
    
    <script type="text/javascript">    
  
        $(function () {
            $("[id*=txtServiceDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
    
    <script type="text/javascript">    
  
        $(function () {
            $("[id*=txtDeliveryDateToRepi]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
   
   <script type="text/javascript">
    
    function validateLOA(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        var loapricevalue = selectedText.substring(selectedText.indexOf("(") + 1,selectedText.lastIndexOf(")"))
        var totalquantity = document.getElementById('<%= txtTotalQuantity.ClientID %>').value;
            document.getElementById('<%= txtTotalValueInUsd.ClientID %>').value = totalquantity * loapricevalue;
    }
    
   </script>
   
   <style type="text/css">
        .columnSpace
        {
            padding-left:3px;
            padding-right:3px;
            padding-top:3px;
            padding-bottom:3px;
        }
        
   </style>
   
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:-245px; margin-top: -70px;">
        
        <section class="content">
        <div class="container-fluid" style="width:1075px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p id="SRF_CTRLNo" runat="server" style="color:Gray; font-size:14px; font-weight:bold;">SERVICE REPAIR PRINT FORM</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divApprover" runat="server" visible="false">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:85px; width:1075px;">
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>REQUESTOR</th>
                                <th>MANAGER</th> 
                                <th>INCHARGE</th> 
                                <th>SCD DEPT. MANAGER</th> 
                                <th>IMPEX</th> 
                                
                              </tr>
                              <tr>
                                <td>
                                    <table style="width:180px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblRequestor" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOARequestor" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td> 
                                <td>
                                    <table style="width:185px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblManager" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAManager" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:185px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblIncharge" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAIncharge" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:185px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblSCDDeptManager" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOASCDDeptManager" runat="server" Text="-" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:185px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblImpex" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAImpex" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>  
                                                             
                              </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:340px; width:1075px;">
                            <table style="width:100%; color:Gray; margin-bottom:10px;">
                              <tr>
                                <th>SUPPLIER</th>                                
                              </tr>
                              <tr>
                                <td><asp:Label ID="lblSupplier" runat="server" /></td>
                              </tr>                              
                            </table>
                            <table style="width:100%; color:Gray;">
                              <tr>                                
                                <th>REQUESTER</th> 
                                <th>LOCAL NUMBER</th>
                                <th>DIVISION</th> 
                                <th>DEPARTMENT</th>                                 
                              </tr>
                              <tr>                                                               
                                <td><asp:TextBox ID="txtRequester" runat="server" Width="200px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtLocalNumber" runat="server" Width="200px" Height="22px" /></td> 
                                <td><asp:TextBox ID="txtDivisionName" runat="server" Width="200px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtDepartment" runat="server" Width="200px" Height="22px" /></td>                                 
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>DELIVERY DATE TO REPI</th>
                                <th>PROBLEM ENCOUNTERED</th>
                                <th>PURPOSE OF PULL OUT</th> 
                                <th>TOTAL VALUE (USD)</th>                                
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtDeliveryDateToRepi" runat="server" Width="200px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtProblemEncountered" runat="server" Width="200px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtPurposeOfPullOut" runat="server" Width="200px" Height="22px" /></td> 
                                <td><asp:TextBox ID="txtTotalValueInUsd" runat="server" Width="200px" Height="22px" /></td>                                
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>LOA NO.</th>
                                <th>SURETY BOND NO.</th>
                                <th>LOA 8106 NO.</th> 
                                <th>CATEGORY</th>                                
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtLoaNumber" runat="server" Width="200px" Height="22px" /></td> 
                                <td><asp:TextBox ID="txtSuretyBond" runat="server" Width="200px" Enabled="false" Height="22px" /></td>   
                                <td><asp:TextBox ID="txtLoa8106" runat="server" Width="200px" Enabled="false" Height="22px" /></td>
                                <td><asp:TextBox ID="txtCategory" runat="server" Width="200px" Height="22px" /></td>                         
                              </tr>
                            </table>     
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>PICKUP POINT</th> 
                                <!--<th>GATE PASS NO.</th> -->
                                <th>TOTAL QUANTITY</th>
                                <th>PULLOUT DATE</th>  
                                <th>&nbsp;</th>                              
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtPickUpPoint" runat="server" Width="200px" Height="22px" /></td>
                                <!--<td><asp:TextBox ID="txtGatePassNo" runat="server" Width="200px" Height="22px" /></td>-->
                                <td><asp:TextBox ID="txtTotalQuantity" runat="server" Width="200px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtServiceDate" runat="server" Width="200px" Height="22px" /></td> 
                                <td style="width:260px;">&nbsp;</td>
                              </tr>
                            </table>                                                                           
                            <table style="width:100%; margin-top:10px; color:Gray; display:none;">
                              <tr>
                                <th>REMARKS</th>                                
                              </tr>
                              <tr>                                                                                                
                                <td><asp:TextBox ID="txtRemarks" runat="server" Width="977px" Height="22px" /></td>                              
                              </tr>
                            </table>
                            
                        </div>                                                     
                                               
                    </div>                                         
                    
                </div>
            </div>         
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:1700px; width:1075px;">
                        
                        <asp:GridView ID="gvData" runat="server"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" OnRowDataBound="gvData_OnRowDataBound"
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px">
                                    <Columns>                                   
                                    <asp:TemplateField HeaderText="REF PR/PO" ItemStyle-CssClass="columnSpace">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRefPRPO" runat="server" Width="105px" Height="22px" Text='<%# Eval("RefPRPO") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SALES INVOICE" ItemStyle-CssClass="columnSpace">
                                        <ItemTemplate>
                                            <asp:Label ID="txtSalesInvoice" runat="server" Width="115px" Height="22px" Text='<%# Eval("SalesInvoice") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BRAND / MACHINE" ItemStyle-CssClass="columnSpace">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBrandMachine" runat="server" Width="170px" Height="22px" Text='<%# Eval("BrandMachineName") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-CssClass="columnSpace">
                                        <ItemTemplate>
                                            <asp:Label ID="txtItemName" runat="server" Width="170px" Height="22px" Text='<%# Eval("ItemName") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECIFICATION" ItemStyle-CssClass="columnSpace">
                                        <ItemTemplate>
                                            <asp:Label ID="txtSpecification" runat="server" Width="185px" Height="22px" Text='<%# Eval("Specification") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="txtQuantity" runat="server" Width="45px" Height="22px" Text='<%# Eval("TotalQuantity") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="txtUnitOfMeasure" runat="server" Width="60px" Height="22px" Text='<%# Eval("UOM_Description") %>' />
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SERIAL NO" ItemStyle-CssClass="columnSpace">
                                        <ItemTemplate>
                                            <asp:Label ID="txtSerialNo" runat="server" Width="100px" Height="22px" Text='<%# Eval("SerialNo") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                   
                                </Columns>

                            </asp:GridView>                                                                                     
                            
                        </div>
                    </div>
                </div>
            </div>
                                          
                                               
            

            
    </div>
            
    </section>
        
    </div>
    </form>
</body>
</html>
