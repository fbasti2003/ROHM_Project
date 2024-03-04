<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PIPL_InvoicePrint.aspx.cs" Inherits="REPI_PUR_SOFRA.PIPL_InvoicePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PROFORMA Print Invoice</title>
    
    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" type="text/css" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" type="text/css" />
    <link href="plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/themes/all-themes.css" rel="stylesheet" type="text/css" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" type="text/css" /> 
    
    <style type="text/css">
        .pagination-ys 
        {
          padding-left: 0;
          margin: 20px 0;
          border-radius: 4px;         
        }  
        .pagination-ys table > tbody > tr > td 
        {
          display: inline;         
        }  
        .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span 
        {
          position: relative;
          float: left;
          padding: 3px 5px;
          line-height: 1.42857143;
          text-decoration: none;
          color: white;
          background-color: #00BCD4;
          border: 1px solid #dddddd;
          margin-left: -1px;         
        }  
        .pagination-ys table > tbody > tr > td > span 
        {
          position: relative;
          float: left;
          padding: 3px 5px;
          line-height: 1.42857143;
          text-decoration: none;         
          margin-left: -1px;
          z-index: 2;
          color: white;
          background-color: #FF5722;
          border-color: white;
          cursor: default;         
        }  
        .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span 
        {
          margin-left: 0;
          border-bottom-left-radius: 4px;
          border-top-left-radius: 4px;        
        }  
        .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span 
        {
          border-bottom-right-radius: 4px;
          border-top-right-radius: 4px;         
        }  
        .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus 
        {
          color: #97310e;
          background-color: #eeeeee;
          border-color: #dddddd;
        } 
    </style>

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
        
               
        
    </script>
    
    <script type="text/javascript">
        $(function () {
            $("[id*=txtETDManila]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
        
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:5%;">
        
        <div class="container-fluid" style="margin-top:10px; margin-left:-20px; width:1075px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                INVOICE DETAILS FOR CONTROL NUMBER 
                                <asp:Label ID="lblControlNo" runat="server" Font-Bold="true" Font-Size="13px" ForeColor="#E91E63" />
                            </p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divWith" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:115px; width:1065px;">
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>MANAGER</th>
                                <th>PC MANAGER</th> 
                                <th>ACCOUNTING</th> 
                                <th>INCHARGE</th> 
                                <th>IMPEX</th> 
                                
                              </tr>
                              <tr>
                                <td>
                                    <table style="width:180px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblManagerWith" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAManagerWith" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td> 
                                <td>
                                    <table style="width:180px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblPCManagerWith" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAPCManagerWith" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:180px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblAccountingWith" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAAccountingWith" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:180px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblInchargeWith" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAInchargeWith" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>  
                                <td>
                                    <table style="width:180px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblImpexWith" runat="server" Height="18px" Width="180px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAImpexWith" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                                             
                              </tr>
                              <tr>
                                <td style="padding-top:5px;">
                                    <p style="font-size:14px; font-weight:bold; color:Gray;">REQUESTER LOCAL/EMAIL :</p>                                    
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblRequesterLocalEmailWith" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="14px" Text="(112/frmangaliman@rohmphil.com)" />
                                </td>
                              </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divWithout" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:115px; width:1075px;">
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>MANAGER</th>
                                <th>PC MANAGER</th> 
                                <th>INCHARGE</th> 
                                <th>IMPEX</th> 
                                
                              </tr>
                              <tr>
                                <td>
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblManager" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAManager" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td> 
                                <td>
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblPCManager" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAPCManager" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblIncharge" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAIncharge" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblImpex" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAImpex" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>  
                                                             
                              </tr>
                              <tr>
                                <td style="padding-top:5px;">
                                    <p style="font-size:14px; font-weight:bold; color:Gray;">REQUESTER LOCAL/EMAIL :</p>                                    
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblRequesterLocalEmail" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="14px" Text="(112/frmangaliman@rohmphil.com)" />
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
                        <div class="body" style="margin-top:-23px; height:570px; width:1075px;">                            
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>CONSIGNEE</th>
                                <th>ATTENTION</th> 
                                <th>SECTION / DEPT.</th> 
                                <th>MODE OF SHIPMENT</th> 
                                
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="lblConsignee" runat="server" Width="200px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblAttention1" runat="server" Width="200px" Height="22px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblSectionDept1" runat="server" Width="200px" Height="22px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblModeOfShipment" runat="server" Width="200px" ReadOnly="true"/></td>                                 
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>PURPOSE</th>
                                <th>PURPOSE(OTHERS)</th>
                                <th>PACKING</th> 
                                <th>NATURE OF GOODS</th>                                
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="lblPurpose" runat="server" Width="200px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblPurposeOthers" runat="server" Width="200px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblPacking" runat="server" Width="200px" ReadOnly="true" /></td> 
                                <td><asp:TextBox ID="lblNatureOfGoods" runat="server" Width="200px" ReadOnly="true" /></td>                                
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>CATEGORY</th>
                                <th>REQUESTER</th> 
                                <th>PORT OF DESTINATION</th>
                                <th>E.T.D. MANILA</th>
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="lblCategory" runat="server" Width="200px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblRequester" runat="server" Width="200px" ReadOnly="true" /></td> 
                                <td><asp:TextBox ID="lblPortOfDestination" runat="server" Width="200px" Height="22px" ReadOnly="true" /></td>
                                <td>
                                    <asp:TextBox ID="lblETDManila" runat="server" Width="200px" Height="22px" ReadOnly="true" />                                    
                                </td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>ESTIMATED VALUE IN YEN</th>
                                <th>ESTIMATED VALUE IN USD</th> 
                                <th>P.O. NO</th>
                                <th>COUNTRY OF ORIGIN</th>
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="lblEstimatedYen" runat="server" Width="200px" Height="22px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblEstimatedUsd" runat="server" Width="200px" Height="22px" ReadOnly="true" /></td> 
                                <td><asp:TextBox ID="lblPONumber" runat="server" Width="200px" Height="22px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblCountryOfOrigin" runat="server" Width="200px" ReadOnly="true" /></td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>COMMERCIAL VALUE</th>
                                <th>TRADE TERMS</th>
                                <th>PICK-UP LOCATION</th>
                                <th>BDN</th>
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="lblCommercialValue" runat="server" Width="200px" ReadOnly="true" /> </td>
                                <td><asp:TextBox ID="lblTradeTerms" runat="server" Width="200px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblPickupLocation" runat="server" Width="200px" ReadOnly="true" />  </td>
                                <td><asp:TextBox ID="lblBDN" runat="server" Width="200px" /></td> 
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>BDN VALUE</th>
                                <th>ATTENTION</th>
                                <th>REFERENCE NO.</th>
                                <th>INVOICE NO.</th>
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="lblBDNValue" runat="server" Width="200px" ReadOnly="true" /> </td>
                                <td><asp:TextBox ID="lblAttention2" runat="server" Width="200px" Height="22px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="lblReferenceNo" runat="server" Width="200px" Height="22px" ReadOnly="true" /> </td>
                                <td><asp:TextBox ID="lblInvoiceNumber" runat="server" Width="200px" Height="22px" ReadOnly="true" /> </td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>SALES TYPE</th>
                                <th>BUSINESS UNIT</th>
                                <th>ACCOUNT CODE.</th>
                                <th>INSURANCE</th>
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtSalesType" runat="server" Width="200px" Height="22px" ReadOnly="true" /> </td>
                                <td><asp:TextBox ID="txtBusinessUnit" runat="server" Width="200px" Height="22px" ReadOnly="true" /></td>
                                <td><asp:TextBox ID="txtAccountCode" runat="server" Width="200px" Height="22px" ReadOnly="true" /> </td>
                                <td><asp:TextBox ID="txtInsurance" runat="server" Width="200px" Height="22px" ReadOnly="true" /> </td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>ATTACHMENT 1</th>
                                <th>ATTACHMENT 2</th>
                                <th>ATTACHMENT 3</th>
                                <th>ATTACHMENT 4</th>
                              </tr>
                              <tr>
                                <td><asp:LinkButton ID="lblFu1" runat="server" Width="200px" ReadOnly="true" /></td>
                                <td><asp:LinkButton ID="lblFu2" runat="server" Width="200px" ReadOnly="true" /></td>
                                <td><asp:LinkButton ID="lblFu3" runat="server" Width="200px" ReadOnly="true" /></td>
                                <td><asp:LinkButton ID="lblFu4" runat="server" Width="200px" ReadOnly="true" /></td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>REMARKS</th>
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="975px" Height="70px" ReadOnly="true" /></td>
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
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                            <Columns>

                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefId" runat="server" Width="50px" Height="15px" Text='<%# Eval("DetailsRefId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CASE">
                                <ItemTemplate>
                                    <asp:Label ID="lblCaseUnit" runat="server" Width="80px" Height="15px" Text='<%# Eval("CaseUnit") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CASE#">
                                <ItemTemplate>
                                    <asp:Label ID="lblCaseNumber" runat="server" Width="40px" Height="15px" Text='<%# Eval("CaseNumber") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIPTION">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Width="205px" Height="15px" Text='<%# Eval("Description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SPECIFICATION">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecification" runat="server" Width="205px" Height="15px" Text='<%# Eval("Specification") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY.">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Width="50px" Height="15px" Text='<%# Eval("Quantity") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Width="50px" Height="15px" Text='<%# Eval("UOM") %>' />                                         
                                </ItemTemplate>                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CCY">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Width="50px" Height="15px" Text='<%# Eval("Currency") %>' />
                                </ItemTemplate>                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="U. PRICE.">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitPrice" runat="server" Width="70px" Height="15px" Text='<%# Eval("UPrice") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NWT.">
                                <ItemTemplate>
                                    <asp:Label ID="lblNetWeight" runat="server" Width="50px" Height="15px" Text='<%# Eval("NetWeight") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GWT.">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrossWeight" runat="server" Width="50px" Height="15px" Text='<%# Eval("GrossWeight") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MEAS (CM)">
                                <ItemTemplate>
                                    <asp:Label ID="lblMeasurement" runat="server" Width="80px" Height="15px" Text='<%# Eval("Measurement") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                        </Columns>

                    </asp:GridView>                                                                                          
                        
                        </div>
                    </div>
                </div>
            </div>
            
            
    </div>
        
    </div>
    </form>
</body>
</html>
