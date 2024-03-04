<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_Monitoring_Details.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_Monitoring_Details" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 

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
        
        function ApproverMessage(msg) {
            swal({
                title: "POSSIBLE APPROVER(S)",
                text: msg,
                type: "success"
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
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                REQUEST DETAILS FOR RFQ NUMBER 
                                <asp:Label ID="lblRFQNo" runat="server" Font-Bold="true" Font-Size="13px" ForeColor="#E91E63" />
                            </p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:160px; width:1280px;">
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>REQUESTER</th>
                                <th>EMAIL ADDRESS</th>
                                <th>PROD-MNGR <asp:ImageButton ID="imgWho" runat="server" ImageUrl="~/images/who.png" Height="20px" Width="20px" OnClick="imgWho_Click" Visible="false" /></th>    
                                <th>SEND-TO-SUPPLIER(S)</th>                             
                              </tr>
                              <tr>
                                <td>
                                    <table style="width:250px; color:White;">
                                        <tr>
                                            <th style="font-size:12px;"><asp:Label ID="lblRequester" runat="server" Height="18px" Width="250px" ForeColor="Black" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:12px;"><asp:Label ID="lblDOARequester" runat="server" Height="18px" Width="250px" ForeColor="Black" /></td>
                                        </tr>
                                    </table>
                                </td> 
                                <td>
                                    <table style="width:250px; color:White;">
                                        <tr>
                                            <th style="font-size:12px;"><asp:Label ID="lblEmailAddress" runat="server" Height="18px" Width="250px" ForeColor="Blue" /></th>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:250px; color:White;">
                                        <tr>
                                            <th style="font-size:14px;"><asp:Label ID="lblProdManager" runat="server" Height="18px" Width="250px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:12px;"><asp:Label ID="lblDOAProdManager" runat="server" Height="18px" Width="250px" ForeColor="Black" /></td>
                                        </tr>
                                    </table>
                                </td> 
                                <td>
                                    <table style="width:250px; color:White;">
                                        <tr>
                                            <th style="font-size:12px;"><asp:Label ID="lblBuyerSend" runat="server" Height="18px" Width="250px" ForeColor="Black" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:12px;"><asp:Label ID="lblDOABuyerSend" runat="server" Height="18px" Width="250px" ForeColor="Black" /></td>
                                        </tr>
                                    </table>
                                </td>                                
                                                             
                              </tr>
                            </table>
                            
                            <table style="width:100%; color:Gray; margin-top:10px;">
                                <tr>
                                    <th>BUYER</th> 
                                    <th>INCHARGE</th> 
                                    <th>DEPT-MNGR</th>
                                    <th>DIV-MNGR</th>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width:220px; color:White;">
                                            <tr>
                                                <th style="font-size:14px;"><asp:Label ID="lblBuyer" runat="server" Height="18px" Width="220px" /></th>
                                            </tr>
                                            <tr>
                                                <td style="font-size:12px;"><asp:Label ID="lblDOABuyer" runat="server" Height="18px" Width="220px" ForeColor="Black" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table style="width:220px; color:White;">
                                            <tr>
                                                <th style="font-size:14px;"><asp:Label ID="lblIncharge" runat="server" Height="18px" Width="220px" /></th>
                                            </tr>
                                            <tr>
                                                <td style="font-size:12px;"><asp:Label ID="lblDOAIncharge" runat="server" Height="18px" Width="220px" ForeColor="Black" /></td>
                                            </tr>
                                        </table>
                                    </td> 
                                    <td>
                                        <table style="width:220px; color:White;">
                                            <tr>
                                                <th style="font-size:14px;"><asp:Label ID="lblDeptManager" runat="server" Height="18px" Width="220px" /></th>
                                            </tr>
                                            <tr>
                                                <td style="font-size:12px;"><asp:Label ID="lblDOADeptManager" runat="server" Height="18px" Width="220px" ForeColor="Black" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table style="width:220px; color:White;">
                                            <tr>
                                                <th style="font-size:14px;"><asp:Label ID="lblDivisionManager" runat="server" Height="18px" Width="220px" /></th>
                                            </tr>
                                            <tr>
                                                <td style="font-size:12px;"><asp:Label ID="lblDOADivisionManager" runat="server" Height="18px" Width="220px" ForeColor="Black" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divCause" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:70px; width:1280px;">  
                                                  
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>DISAPPROVAL CAUSE</th>                                                            
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtDisapprovalCause" runat="server" Width="1190px" Height="60px" TextMode="MultiLine" /></td>                                                         
                              </tr>
                            </table>                                                        
 
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:170px; width:1280px;">  
                                                  
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>CATEGORY</th>
                                <th>BUYER NOTES</th>  
                                <th>SEND DATE(S)</th> 
                                <th>SUPPLIER</th>                                                              
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddCategory" runat="server" Width="200px" Height="20px" /></td>
                                <td><asp:TextBox ID="txtPurchasingRemarks" runat="server" Width="360px" Height="20px" /></td>  
                                <td><asp:DropDownList ID="ddSendDates" runat="server" Width="180px" Height="20px" /></td> 
                                <td><asp:Label ID="lblSuppliers" runat="server" Width="220px" Height="20px" /></td>       
                                <td style="width:45px;">&nbsp;</td>                                                     
                              </tr>
                            </table>   
                            
                            <table style="width:100%; color:Gray; margin-top:10px;">
                              <tr>
                                <th>TRANSFER DETAILS</th>
                                <th>&nbsp;</th>  
                                <th>&nbsp;</th> 
                                <th>&nbsp;</th>                                                              
                              </tr>
                              <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvTransferDetails" runat="server" Width="988px"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" 
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White">
                                        <Columns>     
                                         <asp:TemplateField HeaderText="TRANSFER FROM / TRANSFER BY / TRANSFER DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHistoryDetails" runat="server" Width="900px" Text='<%# Eval("HistoryUpdateWholeDetails") %>' />
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        </Columns> 
                                    </asp:GridView>
                                </td>
                                <td>&nbsp;</td>  
                                <td>&nbsp;</td> 
                                <td>&nbsp;</td>                                                            
                              </tr>
                            </table>  
                            
                            <table style="width:100%; color:Gray; margin-top:10px; display:none;">
                              <tr>
                                <th>HOLD REASON</th>
                                <th>&nbsp;</th>  
                                <th>&nbsp;</th> 
                                <th>&nbsp;</th>                                                              
                              </tr>
                              <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvHoldReason" runat="server" Width="988px"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" 
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White">
                                        <Columns>     
                                         <asp:TemplateField HeaderText="REASON">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHoldReason" runat="server" Width="600px" Text='<%# Eval("Hold_Reason") %>' />
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="CREATED BY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHoldBy" runat="server" Width="360px" Text='<%# Eval("Hold_By") %>' />
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="CREATED DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHoldDate" runat="server" Width="250px" Text='<%# Eval("Hold_Date") %>' />
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        </Columns> 
                                    </asp:GridView>
                                </td>
                                <td>&nbsp;</td>  
                                <td>&nbsp;</td> 
                                <td>&nbsp;</td>                                                            
                              </tr>
                            </table>                                                     
 
                        </div>                        
                    </div>
                </div>
            </div>                       
                    
            <div class="row clearfix" id="divSupplierResponse" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="header" style="height:20px; margin-top:-23px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                SUPPLIER RESPONSE DETAILS  RFQNo. : <asp:Label ID="lblSupplierResponseRFQNo" runat="server" Font-Bold="true" Font-Size="13px" ForeColor="#E91E63" />
                            </p>
                        </div> 
                        <div class="body" style="margin-top:-23px; width:1075px;">
                            <div style="margin-top:5px;">
                                AWARDED SUPPLIER(S) DETAILS
                                <asp:GridView ID="gvResponse" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                        HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                        HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" Visible="false"
                                                        OnRowDataBound="gvResponse_OnRowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ITM" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" Height="15px" Width="20px" Text='<%# Eval("ItemNumber") %>' />
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="detailsNum" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="30px" Font-Size="11px" />
                                                    <asp:Label ID="lblResponseRefId" runat="server" Text='<%# Eval("ResponseRefId") %>' Visible="false" />
                                                    <asp:Label ID="lblDetailsRefId" runat="server" Text='<%# Eval("RdRefId") %>' Visible="false" />
                                                    <asp:Label ID="lblResponseRFQNo" runat="server" Text='<%# Eval("ResponseRFQNo") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>                                                        
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponseDescription" runat="server" Text='<%# Eval("ResponseDescription") %>' Width="235px" Height="20px" Font-Size="11px"/>                                                                        
                                                    <asp:Label ID="lblResponseSpecs" runat="server" Text='<%# Eval("ResponseSpecs") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsGranted" runat="server" Visible="false" Text='<%# Eval("ResponseIsGranted") %>' />
                                                    <asp:Label ID="lblSupplierId" runat="server" Visible="false" Text='<%# Eval("ResponseSupplierID") %>' />
                                                    <asp:ImageButton ID="ibApprovedResponse" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="AResponse_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>   
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="SUPPLIER" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponseSupplier" runat="server" Text='<%# Eval("ResponseSupplierName") %>' Height="15px" Width="160px" Font-Size="11px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="CUR" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponseCurrency" runat="server" Text='<%# Eval("ResponseSupplierCurrency") %>' Height="15px" Width="35px" Font-Size="11px" Visible="false" />
                                                    <asp:DropDownList ID="ddResponseCurrency" runat="server" Height="15px" Width="50px" Font-Size="11px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="PRICE" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate> 
                                                    <asp:TextBox ID="txtResponseCurrencyPrice" runat="server" Text='<%# Eval("ResponsePrice") %>'  Height="20px" Width="80px" Font-Size="11px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="LEAD" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>                                                                        
                                                    <asp:TextBox ID="txtResponseLeadTime" runat="server" Text='<%# Eval("ResponseLead") %>' Height="20px" Width="40px" Font-Size="11px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="SUPPLIER REMARKS" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>                                                                        
                                                    <asp:TextBox ID="txtResponseSupplierRemarks" runat="server" Text='<%# Eval("ResponseSupplierRemarks") %>' TextMode="MultiLine" Height="20px" Width="170px" Font-Size="11px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>   
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="PURCHASING REMARKS" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtResponsePurchasingRemarks" runat="server" Text='<%# Eval("ResponsePurchasingRemarks") %>' Height="20px" TextMode="MultiLine" Width="178px" Font-Size="11px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>                                                                                                                                                                                                                                                                                                                                                                                                                                
                                        
                                    </asp:GridView>
                            </div>
                            
                            <div style="margin-top:5px;">
                                <asp:GridView ID="gvSuppliers" runat="server"
                                              AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                              HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" OnRowCommand="gvSuppliers_RowCommand" OnRowDataBound="gvSuppliers_OnRowDataBound"
                                              HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                        <Columns>

                                            <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="535px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Width="535px" Text='<%# Eval("ResponseSupplierName") %>' />
                                                    <asp:Label ID="lblSupplierID" runat="server" Visible="false" Text='<%# Eval("ResponseSupplierID") %>' />
                                                    <asp:Label ID="lblResponseCount" runat="server" Visible="false" Text='<%# Eval("ResponseCount") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="RESPONSE DATE" HeaderStyle-Width="280px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponseDate" runat="server" Width="280px" Text='<%# Eval("ResponseResponseDate") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="225px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblView" runat="server" Text="DETAILS" CommandName="lblView_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> / 
                                                    <asp:LinkButton ID="lblManualResponse" runat="server" Text="MANUAL RESPONSE" CommandName="lblManualResponse_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </ItemTemplate>                                                                        
                                            </asp:TemplateField>
                                        
                                        </Columns>

                                </asp:GridView>
                            </div>
                            
                            <div style="margin-top:5px;">
                                <%= supplierResponseDetails %>
                                <asp:GridView ID="gvSupplierResponse" runat="server"
                                              AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                              HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                              HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                        <Columns>

                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Width="250px" Text='<%# Eval("ResponseDescription") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecsDrawing" runat="server" Width="150px" Text='<%# Eval("ResponseSpecs") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAKER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Width="150px" Text='<%# Eval("ResponseMaker") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PRICE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResponsePrice" runat="server" Width="123px" Text='<%# Eval("ResponsePrice") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LEAD TIME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResponseLeadTime" runat="server" Width="40px" Text='<%# Eval("ResponseLead") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SUPPLIER REMAKS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierRemarks" runat="server" Width="250PX" Text='<%# Eval("ResponseRemarks") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>

                                </asp:GridView>
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>           
            
            <div class="row clearfix" id="divSupplierAttachment" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:1050px; width:1280px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                SUPPLIER ATTACHMENT 
                            </p>
                            
                            <ul class="nav nav-tabs tab-nav-right" role="tablist">
                                <%= tabAttachmentSupplier %>
                            </ul>
                            
                            <div class="tab-content">
                                <%= tabAttachmentContentsSupplier %>
                            </div>    
                            
                            <ul class="nav nav-tabs tab-nav-right" role="tablist">
                                <%= tabAttachmentSupplier %>
                            </ul>                        
                            
                        </div>
                    </div>
                </div>
            </div>                
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="header" style="height:20px; margin-top:-23px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                REQUEST DETAILS &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; RFQNo : <asp:Label ID="lblRFQNoForDetails" runat="server" Font-Bold="true" Font-Size="13px" ForeColor="#E91E63" />
                            </p>
                        </div> 
                        <div class="body" style="margin-top:-23px; width:1075px;">
                        
                            <asp:GridView ID="gvData" runat="server"
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                            <Columns>

                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefId" runat="server" Width="70px" Text='<%# Eval("RdRefId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIPTION">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Width="230px" Text='<%# Eval("RdDescription") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecsDrawing" runat="server" Width="270px" Text='<%# Eval("RdSpecs") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MAKER">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaker" runat="server" Width="100px" Text='<%# Eval("RdMaker") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Width="40px" Text='<%# Eval("RdQuantity") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Width="40px" Text='<%# Eval("RdUnitOfMeasure") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PURPOSE/USE & <br /> PICTURE OF ITEM">
                                <ItemTemplate>
                                    <asp:Label ID="lblPurpose" runat="server" Width="150px" Text='<%# Eval("RdPurpose") %>' />                                       
                                </ItemTemplate>                   
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PROCESS/MACHINE">
                                <ItemTemplate>
                                    <asp:Label ID="lblProcess" runat="server" Width="150px" Text='<%# Eval("RdProcess") %>' />                                       
                                </ItemTemplate>                   
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REMARKS">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Width="155px" Text='<%# Eval("RdRemarks") %>' />                                       
                                </ItemTemplate>                   
                            </asp:TemplateField>
                            
                        </Columns>

                    </asp:GridView>       
                               
                    
                                 
                        
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divAttachment" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:1050px; width:1060px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                REQUEST ATTACHMENT 
                            </p>
                            
                            <ul class="nav nav-tabs tab-nav-right" role="tablist">
                                <%= tabAttachment %>
                            </ul>
                            
                            <div class="tab-content">
                                <%= tabAttachmentContents %>
                            </div>
                            
                            <ul class="nav nav-tabs tab-nav-right" role="tablist">
                                <%= tabAttachment %>
                            </ul>
                            
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divActionButtons" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:65px; width:1075px;">
                            <asp:Button ID="btnPreview" runat="server" Text="PRINT PREVIEW" CssClass="btn bg-light-blue waves-effect" onclick="btnPreview_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="UPDATE RECORD" CssClass="btn bg-light-blue waves-effect" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>

            
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnUpdate" />
    </Triggers>
    </asp:UpdatePanel>    
    
</asp:Content>
