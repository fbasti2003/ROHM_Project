<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_8105Entry.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_8105Entry" MasterPageFile="~/Sofra.Master" %>

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
    
    <style type="text/css">
        /* Popup box BEGIN */
        .hover_bkgr_fricc{
            background:rgba(0,0,0,.4);
            cursor:pointer;
            display:none;
            height:100%;
            position:fixed;
            text-align:center;
            top:0;
            width:100%;
            left:0px;
        }
        .hover_bkgr_fricc .helper{
            display:inline-block;
            height:100%;
            vertical-align:middle;
        }
        .hover_bkgr_fricc > div {
            background-color: #fff;
            display: inline-block;
            height: auto;
            width: 1070px;
            min-height: 100px;
            vertical-align: middle;
            position: relative;
            border-radius: 8px;
            padding: 15px 15px;
            margin-top: 100px;
        }
        .popupCloseButton {
            background-color: #fff;
            border: 3px solid #999;
            cursor: pointer;
            display: inline-block;
            font-family: arial;
            font-weight: bold;
            position: absolute;
            top: -20px;
            right: -20px;
            font-size: 25px;
            line-height: 30px;
            width: 30px;
            height: 30px;
            text-align: center;
        }
        .popupCloseButton:hover {
            background-color: #ccc;
        }
        .trigger_popup_fricc {
            cursor: pointer;
            font-size: 20px;
            margin: 20px;
            display: inline-block;
            font-weight: bold;
        }
        
        .textHeight 
        {
        	min-height:20px;
        	verflow: hidden;
        }
        /* Popup box BEGIN */
    </style>
    
    <style type="text/css">
        /* Popup box BEGIN */
        .hover_bkgr_fricc2{
            background:rgba(0,0,0,.4);
            cursor:pointer;
            display:none;
            height:100%;
            position:fixed;
            text-align:center;
            top:0;
            width:100%;
            left:0px;
        }
        .hover_bkgr_fricc2 .helper{
            display:inline-block;
            height:100%;
            vertical-align:middle;
        }
        .hover_bkgr_fricc2 > div {
            background-color: #fff;
            display: inline-block;
            height: auto;
            min-width: 400px;
            min-height: 100px;
            vertical-align: middle;
            position: relative;
            border-radius: 8px;
            padding: 15px 15px;
            margin-top: 100px;
        }
        .popupCloseButton2 {
            background-color: #fff;
            border: 3px solid #999;
            cursor: pointer;
            display: inline-block;
            font-family: arial;
            font-weight: bold;
            position: absolute;
            top: -20px;
            right: -20px;
            font-size: 25px;
            line-height: 30px;
            width: 30px;
            height: 30px;
            text-align: center;
        }
        .popupCloseButton:hover2 {
            background-color: #ccc;
        }
        .trigger_popup_fricc {
            cursor: pointer;
            font-size: 20px;
            margin: 20px;
            display: inline-block;
            font-weight: bold;
        }
        
        .textHeight2 
        {
        	min-height:20px;
        	verflow: hidden;
        }
        
        .transparentButton
        {
            background-color: transparent;
            border: none;
        }
        /* Popup box BEGIN */
    </style>
    
    <style type="text/css">
        .WordWrap
        {
            width: 100%;
            word-break: break-all;
        }
        .WordBreak
        {
            width: 100px;
            overflow: hidden;
            text-overflow: ellipsis;
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
        
        function Close8105()
        {
            swal({
                title: 'Do you want to delete ?',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                confirmButtonClass: 'btn btn-success',
                cancelButtonClass: 'btn btn-danger',
                type: 'warning',
                buttonsStyling: false
            }).then(function (yes) {
                // Called if you click Yes.
                if (yes) {
                    // Make Ajax call.
                    swal('Deleted', '', 'success');
                }
            },
            function (no) {
                // Called if you click No.
                if (no == 'cancel') {
                    swal('Cancelled', '', 'error');
                }
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
    
    <script type="text/javascript">    
  
        $(function () {
            $("[id*=txt8105ProcessDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
    
     <script type="text/javascript">
        function showDialog()
        {
            $('.hover_bkgr_fricc').show();
        }
        
        function hideDialog()
        {
            $('.hover_bkgr_fricc').hide();
        }
        
        function showDialog2()
        {
            $('.hover_bkgr_fricc2').show();
        }
        
        function hideDialog2()
        {
            $('.hover_bkgr_fricc2').hide();
        }
        
        function isNumberKey(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

             return true;
        }
        
        function computeQty()
        {
            var gvDrv = document.getElementById('<%= gvActualDelivery.ClientID %>');
            var actualQty = '0';
            var totalQty = parseInt(document.getElementById('<%= txtTotalQuantity.ClientID %>').value);
            
            for (var i = 1; i < gvDrv.rows.length; i++) {
                    var cell = gvDrv.rows[i].cells;
                    actualQty = parseInt(actualQty) + parseInt(cell[2].innerText);
                
            }

            if (actualQty >= totalQty)
            {
            //lblAboutToClose
            
                showDialog2();
                document.getElementById('<%= lblAboutToClose.ClientID %>').innerHTML = document.getElementById('<%= lblCtrlNo2.ClientID %>').innerHTML;
                return false;
            } else {

                document.getElementById('<%= btnSubmitTemporary.ClientID %>').click();
            }
            
        }
        
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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SERVICE REPAIR 8105 ENTRY</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:1100px;">
                            <div style="margin-top:10px;">
                                <table style="width:1100px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>ENTER ITEM YOU WANT TO SEARCH (Control Number or 8106 Number)</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="460px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:DropDownList ID="ddStatus" runat="server" Width="320px" Height="28px" class="form-control">
                                            <asp:ListItem Text="" Value="" />
                                            <asp:ListItem Text="DELIVERED WITH PENDING ITEMS" Value="DELIVERED WITH PENDING ITEMS" />
                                            <asp:ListItem Text="DELIVERED" Value="DELIVERED" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
                                    </td>   
                                    <td><asp:Button ID="btnDownloadExcel" runat="server" Text="DOWNLOAD EXCEL REPORT" Height="28px"  CssClass="btn bg-light-blue waves-effect" OnClick="btnDownloadExcel_Click" /></td>                       
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
                        <div class="body" style="margin-top:-23px; min-height:100px; width:1280px;">
                            
                            <div class="WordWrap">
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">

                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblCtrl" runat="server" Font-Bold="true" Height="15px" Visible="false" Text='<%# Eval("Warehouse_CtrlNo") %>' CommandName="lblCtrl_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                            
                                            <asp:Label ID="lblCTRLNo" runat="server" Height="15px" Text='<%# Eval("Warehouse_CtrlNo") %>'  /> 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="LOA8106" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblLOA8106" runat="server" Height="15px" Width="150px" Text='<%# Eval("Warehouse_LOA8106") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Height="15px" Width="200px" Text='<%# Eval("Warehouse_RequesterName") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier" runat="server" Height="15px" Width="250px" Text='<%# Eval("Warehouse_SupplierName") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ORDER QTY." HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderQty" runat="server" Height="15px" Width="80px" Text='<%# Eval("Warehouse_TotalQuantity") %>' />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DEL. QTY." HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtyDelivered" runat="server" Height="15px" Width="60px" Text='<%# Eval("Warehouse_TotalActualQuantity") %>' />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REM. QTY." ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="txtRemainingQty" runat="server" Width="60px" Text='<%# Eval("Warehouse_RemainingQuantity") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="190px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Height="15px" Width="190px" Text='<%# Eval("StatAll") %>' ForeColor="White" Font-Bold="true" />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbPrint" runat="server" Text="VIEW" CommandName="lbPrint_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                            </div>
                            
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="hover_bkgr_fricc" style="overflow:auto;">
                <span class="helper"></span>
                <div>
                    
                    
                     <div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                            <div class="card">
                                <div class="body" style="margin-top:0px; min-height:20px; width:1060px;">
                                    <table style="width:1030px; color:Black; text-align:center;">
                                        <tr>
                                            <td style="font-size:18px;"><asp:Label ID="lblCtrlNo2" runat="server" Font-Bold="true" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                     </div>
                    
                    
                    <div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; min-height:80px; width:1060px;">
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
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblRequestor" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOARequestor" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td> 
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblManager" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOAManager" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblIncharge" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOAIncharge" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblSCDDeptManager" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOASCDDeptManager" runat="server" Text="-" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="width:180px; color:Gray; text-align:left;">
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblImpex" runat="server" Font-Bold="true" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size:14px;"><asp:Label ID="lblDOAImpex" runat="server" /></td>
                                                </tr>
                                            </table>
                                        </td>  
                                                                     
                                      </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    
                    <div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; height:290px; width:1060px;">
                                    <table style="width:100%; color:Gray; text-align:left;">
                                      <tr>
                                        <th>SUPPLIER</th>                                
                                      </tr>
                                      <tr>
                                        <td><asp:Label ID="lblSupplier" runat="server" /></td>
                                      </tr>                              
                                    </table>
                                    <table style="width:100%; color:Gray; text-align:left;">
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
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
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
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
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
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
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
                    
                    
                    
                    <div>
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; min-height:100px; width:1060px;">
                                
                                <div class="WordWrap">
                                <asp:GridView ID="gvDetails" runat="server"
                                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" OnRowDataBound="gvDetails_OnRowDataBound"
                                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px">
                                            <Columns>    
                                            <asp:TemplateField HeaderText="REFID" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRefId" runat="server" Width="40px" Height="22px" Text='<%# Eval("RefId") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                               
                                            <asp:TemplateField HeaderText="REF PR/PO" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRefPRPO" runat="server" Width="110px" Text='<%# Eval("RefPRPO") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SALES INVOICE" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSalesInvoice" runat="server" Width="110px" Text='<%# Eval("SalesInvoice") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BRAND / MACHINE" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtBrandMachine" runat="server" Width="110px" Text='<%# Eval("BrandMachineName") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtItemName" runat="server" Width="168px" Text='<%# Eval("ItemName") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SPECIFICATION" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSpecification" runat="server" Width="168px" Text='<%# Eval("Specification") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtQuantity" runat="server" Width="45px" Text='<%# Eval("TotalQuantity") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtUnitOfMeasure" runat="server" Width="60px" Text='<%# Eval("UOM_Description") %>' />
                                                </ItemTemplate>                    
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SERIAL NO" ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSerialNo" runat="server" Width="70px" Text='<%# Eval("SerialNo") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="DEL. QTY." ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtActualQty" runat="server" Width="60px" Text='<%# Eval("Warehouse_TotalActualQuantity") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="REM. QTY." ItemStyle-CssClass="columnSpace">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemainingQty" runat="server" Width="60px" Text='<%# Eval("Warehouse_RemainingQuantity") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                        </Columns>

                                    </asp:GridView>    
                                    </div>                                                                                 
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    
                    <div id="divAttachment" runat="server" visible="false">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1060px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; min-height:20px; width:1060px;">
                                    <table style="width:100%; margin-top:10px; color:Gray;">
                                      <tr>
                                        <p>ATTACHMENT FROM WAREHOUSE RECEIVING ENTRY</p>
                                      </tr>
                                      <tr>                                         
                                           <asp:GridView ID="gvAttachmentFromWarehouse" runat="server"
                                                              AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px" 
                                                              HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" 
                                                              FooterStyle-Font-Size="10px">
                                                              
                                                              <Columns>                                                    
                                                                <asp:TemplateField HeaderText="ATTACHMENT" ItemStyle-Width="500px" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbAttachment" runat="server" Width="500px" Text='<%# Eval("Warehouse_Attachment") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                              
                                            </asp:GridView>                        
                                      </tr>
                                    </table>
                                    
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
                                        <tr>
                                            <p style="font-size:14px; font-weight:bold;">8105 ENTRY LIST</p>
                                        </tr>
                                        <tr>
                                            <div class="WordWrap">
                                            <asp:GridView ID="gvActualDelivery" runat="server"
                                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px" OnRowDataBound="gvActualDelivery_OnRowDataBound"
                                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" 
                                                          FooterStyle-Font-Size="10px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="REFID" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDetailsRefId" runat="server" Width="80px" Text='<%# Eval("Warehouse_DetailsRefId") %>' ></asp:Label>
                                                            <asp:Label ID="lblRefid" runat="server" Width="80px" Text='<%# Eval("RefId") %>' Visible="false" ></asp:Label>
                                                            <asp:Label ID="lblAttachment" runat="server" Width="80px" Text='<%# Eval("Warehouse_Attachment") %>' Visible="false" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Width="250px" Text='<%# Eval("Warehouse_ItemName") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACTUAL QTY." ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActualQuantity" runat="server" Width="90px" Text='<%# Eval("Warehouse_TotalActualQuantity") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DELIVERED DATE" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveredDate" runat="server" Width="120px" Text='<%# Eval("Warehouse_DeliveredDate") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LOA8105 NUMBER" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtLOA8105Number" runat="server" Width="140px" Text='<%# Eval("Warehouse_8105") %>' required />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="8105 PROCESS DATE" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt8105ProcessDate" runat="server" Width="140px" Text='<%# Eval("Warehouse_LOA8105ProcessDate") %>' required />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ATTACHMENT" ItemStyle-Width="190px" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fuAttachment" runat="server" Width="190px" EnableViewState="true" accept=".pdf" required />
                                                            <asp:LinkButton ID="lbAttachment" runat="server" OnClick="lbAttachment_Click" Visible="false" Text='<%# Eval("Warehouse_Attachment") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            </div>
                                        </tr>
                                        
                                        
                                    </table>
                                    
                                    <table style="width:100%; margin-top:10px; color:Gray; text-align:left;">
                                        <tr>
                                            <asp:Button ID="btnInProgress" runat="server" Text="SET THE DELIVERY IN-PROGRESS" Visible="false" CssClass="btn bg-light-blue waves-effect" OnClick="btnInProgress_Click" />
                                            <asp:Button ID="btnConfirmDelivery" runat="server" Text="CONFIRM DELIVERY" Visible="false" CssClass="btn bg-light-green waves-effect" OnClick="btnConfirmDelivery_Click" />                                
                                            <asp:Button ID="btnSubmit2" runat="server" Text="SUBMIT" Width="100px" CssClass="btn bg-green waves-effect" OnClientClick="computeQty(); return false;" />
                                            <asp:Button ID="btnSubmitTemporary" runat="server" Width="100px" Text="" OnClick="btnSubmitTemporary_Click" CssClass="transparentButton" />
                                            <asp:Button ID="btnClose" runat="server" Width="100px" Text="CLOSE" CssClass="btn btn-block bg-pink waves-effect" OnClientClick="hideDialog();" OnClick="btnClose_Click" />
                                        </tr>
                                    </table>
                                    
                                    
                                    
                                    
                                </div>
                                
                                
                        
                            </div>
                            
                        </div>    
                                                                    
                        
                     </div>
                     
                     
                    
                                      
                </div>
                
                
                                    
            </div>
            
            
            
            <div class="hover_bkgr_fricc2">
                <span class="helper"></span>
                <div>
                    
                    <table style="width:100%; color:Gray; text-align:left;">
                      <tr>
                        <th>YOU ARE ABOUT TO CLOSE <asp:Label ID="lblAboutToClose" runat="server" Font-Bold="true" /> <br /> PROCEED ANYWAY?</th>                                
                      </tr>
                      <tr>
                        <td>
                            <asp:Button ID="btnYes" runat="server" Width="100px" Text="YES" CssClass="btn bg-green waves-effect" OnClick="btnYes_Click" />
                            <asp:Button ID="btnNo" runat="server" Width="100px" Text="NO" CssClass="btn btn-block bg-pink waves-effect" OnClientClick="hideDialog2();" />
                        </td>

                      </tr>                              
                    </table>
                
                </div>
            </div>
            
            
            
                 <asp:HiddenField ID="buyerCategory" runat="server" />                 
                                                                       
            
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnConfirmDelivery" />
        <asp:PostBackTrigger ControlID = "btnSubmit" />
        <asp:PostBackTrigger ControlID = "btnInProgress" />  
        <asp:PostBackTrigger ControlID = "btnSubmitTemporary" /> 
        <asp:PostBackTrigger ControlID = "btnYes" />
    </Triggers>
    
    </asp:UpdatePanel>

</asp:Content>