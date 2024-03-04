<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_ApprovalForm.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_ApprovalForm" MasterPageFile="~/Sofra.Master" %>

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
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1370px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SERVICE REPAIR REQUEST APPROVAL</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" style="display:block;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1370px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:105px; width:500px;">
                            <div style="margin-top:10px;">
                                <table style="width:500px; color:Gray; font-size:12px;">
                                  <tr>
                                    <%--<th>FROM</th>
                                    <th>TO</th>--%> 
                                    <th>CATEGORY</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:DropDownList ID="ddCategory" runat="server" Width="400px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <%--<td><asp:TextBox ID="txtFrom" runat="server" Width="200px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="200px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>--%>    
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
                                    </td>                          
                                  </tr>
                                </table>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>   
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1370px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:300px; width:1350px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" RowStyle-Height="15px" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("RefId") %>' Height="50px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblCtrl" runat="server" Font-Bold="true" Height="15px" Text='<%# Eval("CTRLNo") %>' CommandName="lblCtrl_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier" runat="server" Height="15px" Width="160px" Text='<%# Eval("Supplier") %>'  />    
                                            <asp:Label ID="lblSupplierEmail" runat="server" Height="15px" Width="160px" Text='<%# Eval("SupplierEmail") %>' Visible="false"  />   
                                            <asp:Label ID="lblPezaNonPeza" runat="server" Height="15px" Width="160px" Text='<%# Eval("Warehouse_PezaNonPeza") %>' Visible="false"  />                                         
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="220px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Height="15px" Width="220px" Text='<%# Eval("Requester") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Height="100px" Width="120px" Text='<%# Eval("CategoryDescription") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TRANSACTION DATE" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDate" runat="server" Height="15px" Width="120px" Text='<%# Eval("ReqInchargeDOA") %>' />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PROD. MNGR. AD" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblProdManagerApprovalDate" runat="server" Height="15px" Width="120px" Text='<%# Eval("ReqManagerDOA") %>' />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibDisapproved" runat="server" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="GATEPASS NO" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGPNumber" runat="server" Width="85px" Height="16px" Enabled="false" Text='<%# Eval("GatePassNo") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="LOA 8106 NO." HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLOA8106No" runat="server" Width="85px" Height="16px" Enabled="false" Text='<%# Eval("GatePassNo") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REMARKS / CAUSE / NOTE" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="145px" Height="16px" Enabled="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                
                                                <td colspan="100%" style="background-color:White;">
                                                
                                                    <div style="margin-left:20px; margin-top:5px; margin-bottom:5px;">
                                                        
                                                        <asp:GridView ID="gvDetails1" runat="server" Visible="false"
                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                      HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" >
                                                                <Columns>                                 
                                                                    <asp:TemplateField HeaderText="REQUESTER">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequester" runat="server" Width="230px" Height="30px" Text='<%# Eval("ReqInchargeName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MANAGER">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblManager" runat="server" Width="230px" Height="30px" Text='<%# Eval("ReqManagerName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="INCHARGE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIncharge" runat="server" Width="230px" Height="30px" Text='<%# Eval("PurInchargeName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SCD DEPT. MANAGER">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDeptManager" runat="server" Width="230px" Height="30px" Text='<%# Eval("PurDeptManagerName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                               
                                                                    <asp:TemplateField HeaderText="IMPEX">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblImpex" runat="server" Width="230px" Height="30px" Text='<%# Eval("PurManagerName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                </Columns>

                                                        </asp:GridView>
                                                        
                                                        <asp:GridView ID="gvDetails2" runat="server" Visible="false"
                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                      HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" >
                                                                <Columns>                                 
                                                                    <asp:TemplateField HeaderText="SUPPLIER">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSupplier" runat="server" Width="230px" Height="30px" Text='<%# Eval("SupplierName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DELIVERY DATE TO REPI">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDeliveryDateToRepi" runat="server" Width="230px" Height="30px" Text='<%# Eval("DeliveryDateToRepi") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PROBLEM ENCOUNTERED">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProblemEncountered" runat="server" Width="230px" Height="30px" Text='<%# Eval("ProblemEncountered") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PURPOSE OF PULL OUT">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPurposeOfPullout" runat="server" Width="230px" Height="30px" Text='<%# Eval("PurposeOfPullOutName") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                               
                                                                    <asp:TemplateField HeaderText="TOTAL VALUE (USD)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotalValue" runat="server" Width="230px" Height="30px" Text='<%# Eval("TotalValueInUsd") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                </Columns>

                                                        </asp:GridView>
                                                        
                                                        <asp:GridView ID="gvDetails3" runat="server" Visible="false"
                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                      HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" >
                                                                <Columns>                                 
                                                                    <asp:TemplateField HeaderText="LOA NO.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbLoaNo" runat="server" Width="230px" Height="30px" Text='<%# Eval("LoaNo") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SURETY BOND NO.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSuretyBondNo" runat="server" Width="230px" Height="30px" Text='<%# Eval("LoaSuretyBond") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="LOA 8106 NO.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLoa8106No" runat="server" Width="230px" Height="30px" Text='<%# Eval("Loa8106") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CATEGORY">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCategory" runat="server" Width="230px" Height="30px" Text='<%# Eval("CategoryDescription") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                               
                                                                    <asp:TemplateField HeaderText="PICKUP POINT">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPickupPoint" runat="server" Width="230px" Height="30px" Text='<%# Eval("PickUpPoint") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                </Columns>

                                                        </asp:GridView>
                                                        
                                                        <asp:GridView ID="gvDetails4" runat="server" Visible="false"
                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                      HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" >
                                                                <Columns>                                 
                                                                    <asp:TemplateField HeaderText="GATE PASS NO.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGatePassNo" runat="server" Width="230px" Height="30px" Text='<%# Eval("GatePassNo") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="TOTAL QUANTITY">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotalQuantity" runat="server" Width="230px" Height="30px" Text='<%# Eval("TotalQuantity") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SERVICE DATE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblServiceDate" runat="server" Width="230px" Height="30px" Text='<%# Eval("PullOutServiceDate") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="REMARKS">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRemarks" runat="server" Width="460px" Height="30px" Text='<%# Eval("Remarks") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                               

                                                                </Columns>

                                                        </asp:GridView>                                                        
                                                        
                                                        <asp:GridView ID="gvDataDetails" runat="server" Visible="false" 
                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                      HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" >
                                                                <Columns>
                                                                <asp:TemplateField HeaderText="NO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNo" runat="server" Width="40px" Height="36px" Text='<%# Eval("No") %>' ></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                   
                                                                <asp:TemplateField HeaderText="REF PR/PO/RFQ">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRefPRPO" runat="server" Width="150px" Height="36px" Text='<%# Eval("RefPRPO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SALES INVOICE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSalesInvoice" runat="server" Width="150px" Height="36px" Text='<%# Eval("SalesInvoice") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BRAND / MACHINE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrandMachine" runat="server" Width="200px" Height="36px" Text='<%# Eval("BrandMachineName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ITEM NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblItemName" runat="server" Width="200px" Height="36px" Text='<%# Eval("ItemName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SPECIFICATION">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSpecification" runat="server" Width="200px" Height="36px" Text='<%# Eval("Specification") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="QUANTITY">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuantity" runat="server" Width="70px" Height="36px" Text='<%# Eval("TotalQuantity") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UOM">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM_Description") %>' Width="70px" ></asp:Label>   
                                                                    </ItemTemplate>                    
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SERIAL NO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSerialNo" runat="server" Width="100px" Height="36px" Text='<%# Eval("SerialNo") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>

                                                        </asp:GridView>
                                                        
                                                        <asp:GridView ID="gvPullOut" runat="server" Visible="false"
                                                                              AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                                              HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                              HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White">
                                                                <Columns>                                   
                                                                <asp:TemplateField HeaderText="CTRLNO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCtrlNo" runat="server" Height="22px" Width="150px" Text='<%# Eval("Head_Ctrlno") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TOTAL QUANTITY">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalQuantity" runat="server" Height="22px" Width="150px" Text='<%# Eval("Head_TotalQuantity") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TOTAL BOXES">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalBoxes" runat="server" Height="22px" Width="150px" Text='<%# Eval("Head_TotalBoxes") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TYPE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblType" runat="server" Height="22px" Width="200px" Text='<%# Eval("Head_Type") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="REQUESTER">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRequester" runat="server" Height="22px" Width="300px" Text='<%# Eval("Head_Requester") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TRANSACTION DATE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTransactionDate" runat="server" Height="22px" Width="130px" Text='<%# Eval("Head_TransactionDate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>

                                                        </asp:GridView>
                                                    
                                                    </div>
                                                
                                                </td>
                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                            <div style="margin-top:10px;">
                                <asp:Button ID="btnOk" runat="server" Text="SUBMIT" Width="100px" Visible="false" OnClick="btnOk_Click" CssClass="btn bg-light-blue waves-effect" />
                            </div>
                        
                        </div>
                                                
                    </div>
                </div>
            </div>
                                
                                                                       
            
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnSubmit" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>
