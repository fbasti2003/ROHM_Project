<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DRF_AllRequest.aspx.cs" Inherits="REPI_PUR_SOFRA.DRF_AllRequest" MasterPageFile="~/Sofra.Master" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

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
    <script src="js/pages/ui/tooltips-popovers.js" type="text/javascript"></script>  
    
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
            $("[id*=txtFrom]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
        
        $(function () {
            $("[id*=txtTo]").datepicker({
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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">DISCREPANCY REQUEST ALL REQUEST FORM</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                            <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT DATE RANGE YOU WANT TO SEARCH</p>
                            <div style="margin-top:10px;">
                                <table style="width:1100px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th> 
                                    <th>ITEM TO SEARCH</th>
                                    <th>ITEM STATUS</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="200px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="200px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>   
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="310px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:DropDownList ID="ddItemStatus" runat="server" Font-Bold="true" Font-Size="14px" class="form-control" Width="250px" Height="28px" >
                                            <asp:ListItem Value="pending" Text="PENDING" />
                                            <asp:ListItem Value="approved" Text="APPROVED" />
                                            <asp:ListItem Value="disapproved" Text="DISAPPROVED" />
                                            <asp:ListItem Value="closed" Text="CLOSED TRANSACTION" />
                                            <asp:ListItem Value="all" Text="ALL" Selected="True" />
                                        </asp:DropDownList>
                                    </td> 
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
                                    </td>                          
                                  </tr>
                                </table>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>   
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:300px; width:1280px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" RowStyle-Height="15px"
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("RefId") %>' ForeColor="White" BackColor="Black" Width="50px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblCtrl" runat="server" Font-Bold="true" Height="15px" Text='<%# Eval("CTRLNo") %>' CommandName="lblCtrl_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ForeColor="White" BackColor="Black" Width="150px"   />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier" runat="server" Height="15px" Width="400px" Text='<%# Eval("Supplier") %>' ForeColor="White" BackColor="Black"/>                                            
                                            <asp:Label ID="lblAttention" runat="server" Text='<%# Eval("Attention") %>' Visible="false" /> 
                                            <asp:Label ID="lblSendDates" runat="server" Text='<%# Eval("SendDates") %>' Visible="false" /> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="240px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Height="15px" Text='<%# Eval("Requester") %>' ForeColor="White" BackColor="Black" Width="240px" />                                            
                                            <asp:Label ID="lblReqManager" runat="server" Text='<%# Eval("ReqManager") %>' Visible="false" /> 
                                            <asp:Label ID="lblPurIncharge" runat="server" Text='<%# Eval("PurIncharge") %>' Visible="false" /> 
                                            <asp:Label ID="lblPurManager" runat="server" Text='<%# Eval("PurManager") %>' Visible="false" /> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PUR MANAGER" HeaderStyle-Width="285px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Width="285px" ForeColor="White" Visible="false" /> 
                                            <asp:Label ID="lblReqManagerStat" runat="server" Text='<%# Eval("ReqManagerStat") %>' Visible="false" /> 
                                            <asp:Label ID="lblPurInchargeStat" runat="server" Text='<%# Eval("PurInchargeStat") %>' Visible="false" /> 
                                            <asp:Label ID="lblPurManagerStat" runat="server" Text='<%# Eval("PurManagerStat") %>' Visible="false" />  
                                            <asp:Label ID="lblBuyerSendStat" runat="server" Text='<%# Eval("BuyerSendStat") %>' Visible="false" />  
                                            <asp:Label ID="lblSupplierResponded" runat="server" Text='<%# Eval("SupplierResponded") %>' Visible="false" />  
                                            <asp:Label ID="lblReqManagerStatColor" runat="server" Text='<%# Eval("ReqManagerStatColor") %>' Visible="false" />  
                                            <asp:Label ID="lblPurInchargeStatColor" runat="server" Text='<%# Eval("PurInchargeStatColor") %>' Visible="false" />  
                                            <asp:Label ID="lblPurManagerStatColor" runat="server" Text='<%# Eval("PurManagerStatColor") %>' Visible="false" /> 
                                            <asp:Label ID="lblReqManagerDOAStat" runat="server" Text='<%# Eval("ReqManagerDOA") %>' Visible="false" /> 
                                            <asp:Label ID="lblPurInchargeDOAStat" runat="server" Text='<%# Eval("PurInchargeDOA") %>' Visible="false" /> 
                                            <asp:Label ID="lblPurManagerDOAStat" runat="server" Text='<%# Eval("PurManagerDOA") %>' Visible="false" />  
                                            <asp:Label ID="lblRequesterDOAStat" runat="server" Text='<%# Eval("RequesterSDOA") %>' Visible="false" />  
                                            <asp:Label ID="lblPosted" runat="server" Text='<%# Eval("Posted") %>' Visible="false" /> 
                                            <asp:Button ID="btnStatus" runat="server" Width="285px" Height="18px" CommandName="btnStatus_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ForeColor="White" Visible="false" />                                           
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbPreview" runat="server" Text="PREVIEW" CommandName="lbPreview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:LinkButton ID="lbPrint" runat="server" Visible="false" Text="PRINT" CommandName="lbPrint_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%" style="background-color:White;">
                                                    <div style="margin-left:7px; margin-bottom:5px;">
                                                        <table style="width:1170px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">INVOICE / DR No.</th>
                                                            <th>PR.No.</th>    
                                                            <th>PO.No.</th>                                 
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtInvoiceDRNo" runat="server" Width="579px" Height="22px" Text='<%# Eval("InvoiceDRNO") %>' /></td>
                                                            <td><asp:TextBox ID="txtPRNo" runat="server" Width="280px" Height="22px" Text='<%# Eval("PRNo") %>' /></td>
                                                            <td><asp:TextBox ID="txtPONo" runat="server" Width="280px" Height="22px" Text='<%# Eval("PONo") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:1170px; color:Gray;">
                                                          <tr>
                                                            <th>PO DATE</th> 
                                                            <th>RECEIVED DATE</th> 
                                                            <th colspan="2">CATEGORY</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td><asp:TextBox ID="txtPODate" runat="server" Width="280px" Height="22px" Text='<%# Eval("PODate") %>' /></td>
                                                            <td><asp:TextBox ID="txtReceivedDate" runat="server" Width="280px" Height="22px" Text='<%# Eval("ReceivedDate") %>' /></td>
                                                            <td colspan="2"><asp:TextBox ID="txtCategory" runat="server" Width="594px" Height="22px" Text='<%# Eval("Category") %>' /></td>                              
                                                          </tr>
                                                        </table>
                                                        <table style="width:1170px; color:Gray;">
                                                          <tr>
                                                            <th colspan="4">DESCRIPTION</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtDescription" runat="server" Width="1163px" Height="22px" Text='<%# Eval("Description") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table style="width:1170px; color:Gray;">
                                                          <tr>
                                                            <th colspan="3">TYPE / DRAWING NO.</th>  
                                                            <th>SEND DATES</th>                                   
                                                          </tr>
                                                          <tr>
                                                            <td colspan="3"><asp:TextBox ID="txtTypeDrawing" runat="server" Width="935px" Height="22px" Text='<%# Eval("TypeDrawingNo") %>' /></td>    
                                                            <td><asp:DropDownList ID="ddSendDates" runat="server" Width="200px" Height="22px" /></td>                           
                                                          </tr>
                                                        </table>
                                                        <table style="width:1170px; color:Gray;">
                                                          <tr>
                                                            <th>ORDER QUANTITY</th>
                                                            <th>ABNORMAL QUANTITY</th> 
                                                            <th colspan="2">TYPES OF ABNORMALITY</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td><asp:TextBox ID="txtOrderQuantity" runat="server" Width="280px" Height="22px" Text='<%# Eval("OrderQuantity") %>' /></td>
                                                            <td><asp:TextBox ID="txtAbnormalQuantity" runat="server" Width="280px" Height="22px" Text='<%# Eval("AbnormalQuantity") %>' /></td>
                                                            <td colspan="2"><asp:TextBox ID="txtTypesOfAbnormality" runat="server" Width="596px" Height="22px" Text='<%# Eval("TypesOfAbnormality") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table style="width:1170px; color:Gray;">
                                                          <tr>
                                                            <th colspan="4">DETAILED REPORT</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtDetailedReport" runat="server" Width="1163px" Height="78px" TextMode="MultiLine" Text='<%# Eval("DetailedReport") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table style="width:1170px; color:Gray;">
                                                          <tr>
                                                            <th colspan="4" style="color:Red;">BUYER REMARKS</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtClosedRemarks" runat="server" Width="1163px" Height="22px" TextMode="MultiLine" Text='<%# Eval("PostingRemarks") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table id="tableSupplierResponseType" runat="server" style="width:1170px; color:Gray; display:none;">
                                                          <tr style="color:Red;">
                                                            <th colspan="4">SUPPLIER RESPONSE TYPE</th>                                  
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtResponseType" runat="server" Width="1163px" Height="22px" Text='<%# Eval("ResponseType") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table id="tableSupplierResponseAnswer" runat="server" style="width:1170px; color:Gray; display:none;">
                                                          <tr style="color:Red;">
                                                            <th colspan="4">SUPPLIER ANSWER & COUNTERMEASURE</th>                                  
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtResponseAnswer" runat="server" Width="1163px" Height="100px" TextMode="MultiLine" Text='<%# Eval("ResponseAnswer") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
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
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnSubmit" />
        <asp:PostBackTrigger ControlID = "txtSearch" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>


