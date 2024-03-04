<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DRF_ReceivingEntry.aspx.cs" Inherits="REPI_PUR_SOFRA.DRF_ReceivingEntry" MasterPageFile="~/Sofra.Master" %>

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
        
        function SuccessMessageClose(msg) {
            swal({
                title: "YOU ARE ABOUT TO CLOSE?",
                text: msg,
                type: "success"
            });
        }
        
        function divexpandcollapse(divname) {

            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "images/A1.png";
               
            } else {
                div.style.display = "none";
                img.src = "images/A2.png";
            }

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
        function setOnLoad() {
            document.getElementById('<%=divOpacity.ClientID %>').style.opacity = "0.5";
            document.getElementById('<%=divLoader.ClientID %>').style.display = "block"; 
        }

    </script>
    
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
            z-index:10000;
            left:-50px;
        }
        .hover_bkgr_fricc .helper{
            display:inline-block;
            height:100%;
            vertical-align:middle;
        }
        .hover_bkgr_fricc > div {
            background-color: #fff;
            box-shadow: 10px 10px 60px #555;
            display: inline-block;
            height: auto;
            max-width: 551px;
            min-height: 100px;
            vertical-align: middle;
            width: 60%;
            position: relative;
            border-radius: 8px;
            padding: 15px 1%;
            padding-left:-100px;
        }
        .popupCloseButton {
            background-color: #fff;
            border: 3px solid #999;
            border-radius: 50px;
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
        /* Popup box BEGIN */
    </style>
    
    <script type="text/javascript">
        function showDialog()
        {
            $('.hover_bkgr_fricc').show();
        }
        
        function hideDialog()
        {
            $('.hover_bkgr_fricc').hide();
        }
        
    </script>     
    
      
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        
        <div id="divOpacity" runat="server" class="container-fluid" style="margin-top:-50px; margin-left:-320px; opacity:0.1; width:1480px;"> 
        
        <div class="container-fluid" style="margin-left:-20px; width:1075px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1480px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">DRF RECEIVING ENTRY</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1480px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:140px; width:1480px;">
                            <!-- <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT DATE RANGE YOU WANT TO SEARCH</p> -->
                            <div style="margin-top:10px;">
                                <table id="tblApproval" runat="server" style="width:1000px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th>
                                    <th>STATUS</th> 
                                    <th id="thCategory" runat="server">CATEGORY</th>
                                    <th id="thRFQNo" runat="server">ITEM TO SEARCH</th>
                                  </tr>
                                  <tr>   
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="120px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="120px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td> 
                                    <td><asp:DropDownList ID="ddStatus" runat="server" Width="210px" Font-Size="14px" Height="28px" class="form-control">
                                            <asp:ListItem Text="ALL" Value="ALL" />
                                            <asp:ListItem Text="FOR SENDING" Value="FOR SENDING" />
                                            <asp:ListItem Text="FOR RESEND" Value="FOR RESEND" />
                                            <asp:ListItem Text="SUPPLIER RESPONDED" Value="SUPPLIER RESPONDED" />
                                            <asp:ListItem Text="RE-OPEN" Value="RE-OPEN" />
                                        </asp:DropDownList>
                                    </td>                                   
                                    <td><asp:DropDownList ID="ddCategory" runat="server" Width="200px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:TextBox ID="txtDRFNo" runat="server" Width="320px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" />
                                    </td>                                                                 
                                  </tr>
                                </table>
                                <table>
                                    <tr>
                                        <th style="color:White;">DUMMY</th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnExport" runat="server" Text="EXPORT TO EXCEL" OnClick="btnExport_Click" CssClass="btn bg-pink waves-effect" Height="28px" Width="160px" />
                                        </td> 
                                    </tr>
                                </table>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>  
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1480px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; width:1480px;">
                            
                            <div id="divLoader" runat="server" style="display:none; width:1060px;">
                                <p style="font-weight:bold; font-size:14px; color:Black; margin-left:30%;">LOADING DATA... PLEASE WAIT...</p>
                            </div>
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" Visible="false"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCounter" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="28px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCTRLNo" runat="server" Height="15px" Font-Bold="true" Visible="false" Text='<%# Eval("CTRLNo") %>'  />   
                                            <asp:LinkButton ID="lbCTRLNo" runat="server" Height="15px" Text='<%# Eval("CTRLNo") %>' CommandName="lbCTRLNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="true" />  
                                            <asp:Label ID="lblTransDate" runat="server" Text='<%# Eval("TransactionDate") %>' Visible="false" />   
                                            <asp:Label ID="lblAttachment1" runat="server" Text='<%# Eval("Attachment1") %>' Visible="false" />    
                                            <asp:Label ID="lblAttachment2" runat="server" Text='<%# Eval("Attachment2") %>' Visible="false" />  
                                            <asp:Label ID="lblAttachment3" runat="server" Text='<%# Eval("Attachment3") %>' Visible="false" />  
                                            <asp:Label ID="lblAttachment4" runat="server" Text='<%# Eval("Attachment4") %>' Visible="false" />      
                                            <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("Supplier") %>' Visible="false" />      
                                            <asp:Label ID="lblAttention" runat="server" Text='<%# Eval("Attention") %>' Visible="false" />     
                                            <asp:Label ID="lblSupplierEmail" runat="server" Text='<%# Eval("SupplierEmail") %>' Visible="false" />                                                                    
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="170px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>' Height="15px" Width="168px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SEND DATES" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddSendDates" runat="server" Height="22px" Width="200px" />
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuppliers" runat="server" Text='<%# Eval("Supplier") %>' Height="15px" Width="250px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="210px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("Requester") %>' Height="15px" Width="210px"  />
                                            <asp:Label ID="lblReqManager" runat="server" Text='<%# Eval("ReqManager") %>' Visible="false" /> 
                                            <asp:Label ID="lblPurIncharge" runat="server" Text='<%# Eval("PurIncharge") %>' Visible="false" /> 
                                            <asp:Label ID="lblPurManager" runat="server" Text='<%# Eval("PurManager") %>' Visible="false" /> 
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
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Height="15px" Width="148px" Font-Size="11px" ForeColor="White" /> 
                                            <asp:Label ID="lblStatColor" runat="server" Text='<%# Eval("CssColorCode") %>' Visible="false" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="55px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibClosed" runat="server" ImageUrl="~/images/Close3.png" Width="18px" Height="18px" CommandName="Closed_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>                                                   
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblView" runat="server" Text="OPEN DETAILS" CommandName="lblView_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad()" />
                                            <asp:LinkButton ID="lbManualResponse" runat="server" Visible="false" Text="MANUAL RESPONSE" CommandName="lbManualResponse_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:LinkButton ID="lbPreview" runat="server" Text="PREVIEW" CommandName="lbPreview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:LinkButton ID="lbAttachment" runat="server" Text="ATTACHMENT" CommandName="lbAttachment_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>                                                                                             
                                <Columns>
                                    <asp:TemplateField HeaderText="BUYER REMARKS" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="120px" Height="16px" Enabled="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                 <td colspan="100%" style="background-color:White;">                                                 
                                                    <div style="margin-left:5px; margin-top:2px; margin-bottom:0px;">
                                                        <asp:Label ID="lblBuyerRemarksNew" runat="server" Text="BUYER REMARKS" Height="16px" /> <asp:TextBox ID="txtBuyerRemarksNew" runat="server" Width="800px" Height="20px" Text='<%# Eval("BuyerRemarks") %>' /> <asp:Button ID="btnUpdateBuyerRemarks" runat="server" Text="UPDATE BUYER REMARKS" Height="22px" CommandName="UpdateBuyerRemarks_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </div>
                                                 </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 </Columns>
                                
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate> 
                                        
                                            <tr>
                                                <td colspan="100%" style="background-color:White;">
                                                    <div id="divDetails" runat="server" style="margin-left:7px; margin-bottom:5px; display:none;">
                                                        <table style="width:1180px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">INVOICE / DR No.</th>
                                                            <th>PR.No.</th>     
                                                            <th>PO.No.</th>                                  
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtInvoiceDRNo" runat="server" Width="390px" Height="22px" Text='<%# Eval("InvoiceDRNO") %>' /></td>
                                                            <td><asp:TextBox ID="txtPRNo" runat="server" Width="390px" Height="22px" Text='<%# Eval("PRNO") %>' /></td>
                                                            <td><asp:TextBox ID="txtPONo" runat="server" Width="390px" Height="22px" Text='<%# Eval("PONO") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:1180px; color:Gray;">
                                                          <tr>
                                                            <th>PO DATE</th> 
                                                            <th>RECEIVED DATE</th> 
                                                            <th colspan="2">CATEGORY</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td><asp:TextBox ID="txtPODate" runat="server" Width="390px" Height="22px" Text='<%# Eval("PODate") %>' /></td>
                                                            <td><asp:TextBox ID="txtReceivedDate" runat="server" Width="390px" Height="22px" Text='<%# Eval("ReceivedDate") %>' /></td>
                                                            <td colspan="2"><asp:TextBox ID="txtCategory" runat="server" Width="390px" Height="22px" Text='<%# Eval("Category") %>' /></td>                              
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="4">DESCRIPTION</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtDescription" runat="server" Width="1177px" Height="22px" Text='<%# Eval("Description") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="4">TYPE / DRAWING NO.</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtTypeDrawing" runat="server" Width="1177px" Height="22px" Text='<%# Eval("TypeDrawingNo") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table style="width:1180px; color:Gray;">
                                                          <tr>
                                                            <th>ORDER QUANTITY</th>
                                                            <th>ABNORMAL QUANTITY</th> 
                                                            <th colspan="2">TYPES OF ABNORMALITY</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td><asp:TextBox ID="txtOrderQuantity" runat="server" Width="390px" Height="22px" Text='<%# Eval("OrderQuantity") %>' /></td>
                                                            <td><asp:TextBox ID="txtAbnormalQuantity" runat="server" Width="390px" Height="22px" Text='<%# Eval("AbnormalQuantity") %>' /></td>
                                                            <td colspan="2"><asp:TextBox ID="txtTypesOfAbnormality" runat="server" Width="390px" Height="22px" Text='<%# Eval("TypesOfAbnormality") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="4">DETAILED REPORT</th>                                     
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtDetailedReport" runat="server" Width="1177px" Height="100px" TextMode="MultiLine" Text='<%# Eval("DetailedReport") %>' /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table id="tableSupplierResponseType" runat="server" style="width:980px; color:Gray; display:none;">
                                                          <tr style="color:Red;">
                                                            <th colspan="4">SUPPLIER RESPONSE TYPE</th>                                  
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtResponseType" runat="server" Width="1177px" Height="22px" /></td>                               
                                                          </tr>
                                                        </table>
                                                        <table id="tableSupplierResponseAnswer" runat="server" style="width:980px; color:Gray; display:none;">
                                                          <tr style="color:Red;">
                                                            <th colspan="4">SUPPLIER ANSWER & COUNTERMEASURE</th>                                  
                                                          </tr>
                                                          <tr>
                                                            <td colspan="4"><asp:TextBox ID="txtResponseAnswer" runat="server" Width="1177px" Height="100px" TextMode="MultiLine" /></td>                               
                                                          </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="100%" style="background-color:White;">
                                                    <div id="divAttachment" runat="server" style="margin-left:7px; margin-bottom:5px; display:none;">
                                                        <div><p><b>ATTACHMENT</b></p></div>
                                                        <%= displayAttachment.ToString() %>
                                                    </div>
                                                </td>
                                            </tr>
                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>  
                                
                            </asp:GridView>
                            
                            <asp:GridView ID="gvExport" runat="server" AutoGenerateColumns="false" Visible="false">
                            
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNO" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCtrlNo" runat="server" Text='<%# Eval("CTRLNo") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ATTENTION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttention" runat="server" Text='<%# Eval("Attention") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("Supplier") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("Requester") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TRANSACTION DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="INVOICE DR/NO" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceDRNo" runat="server" Text='<%# Eval("InvoiceDRNO") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PR. NO." ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrNo" runat="server" Text='<%# Eval("PrNO") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PO. NO." ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoNo" runat="server" Text='<%# Eval("PoNO") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PO. DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoDate" runat="server" Text='<%# Eval("PoDate") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="RECEIVING DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceivingDate" runat="server" Text='<%# Eval("ReceivedDate") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCtrlNo" runat="server" Text='<%# Eval("Description") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TYPE / DRAWING NO." ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypeDrawing" runat="server" Text='<%# Eval("TypeDrawingNo") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ORDER QUANTITY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderQuantity" runat="server" Text='<%# Eval("OrderQuantity") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ABNORMAL QUANTITY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAbnormalQuantity" runat="server" Text='<%# Eval("AbnormalQuantity") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TYPES OF ABNORMALITY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypesOfAbnormality" runat="server" Text='<%# Eval("TypesOfAbnormality") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DETAILED REPORT" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDetailedReport" runat="server" Text='<%# Eval("DetailedReport") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("StatAll") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                            
                            </asp:GridView>
                           
                         
                        </div>
                                                
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divActionButtons" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1480px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:65px; width:1480px;">
                            <asp:Button ID="btnSend" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSend_Click" />
                        </div>
                    </div>
                </div>
            </div>
                                
                                                                       
            
    </div>
    
    
    <div class="hover_bkgr_fricc">
        <span class="helper"></span>
        <div>
            <p style="font-size:18px;">ATTACHMENT OR RELATED DOCUMENTS FOR (<b> <asp:Label ID="lblAttachment_CtrlNo" runat="server" /> </b>)</p>
            <p>                                                
                <asp:FileUpload ID="fuProofAttachment" runat="server" EnableViewState="true" accept=".pdf" Width="510px" />                
            </p>  
            <p>OR</p>
            <p>ENTER RELATED DOCUMENT NUMBER OR EQUIVALENT</p>   
            <p>
                <asp:TextBox ID="txtRelatedDocument" runat="server" Width="510px" Height="22px" />                
            </p>  
            <p>
                <asp:Button ID="btnProofAttachment" runat="server" Text="ADD TO LIST" CssClass="btn bg-pink waves-effect" OnClick="btnProofAttachment_Click" Height="28px" />
            </p>
            <p>  
                <asp:ListBox ID="listProofAttachment" runat="server" Rows="20" SelectionMode="Single" Width="510px" >  
                </asp:ListBox>   
            </p>
                  
            <td>
                <asp:Button ID="btnCancel2" runat="server" Text="CLOSE" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();"/>
                <asp:Button ID="btnPreview2" runat="server" Text="PREVIEW" Visible="false" CssClass="btn bg-light-green waves-effect" OnClick="btnPreview2_Click" />
            </td>
            <td>
                <p style="color:Red;">*** TO ADD ATTACHMENT - SELECT FILE THEN CLICK ADD TO LIST BUTTON. CLICK CLOSE BUTTON IF DONE ADDING ATTACHMENT.</p>
                <p style="color:Red;">*** IF ATTACHMENT IS NOT YET AVAILABLE, YOU CAN ADD ANY RELATED DOCUMENTS EQUIVALENT TO ATTACHMENT THEN CLICK ADD TO LIST BUTTON</p>
                <p style="color:Blue;">*** TO VIEW ATTACHMENT - SELECT FILE IN THE LIST THEN CLICK PREVIEW BUTTON.</p>
            </td>
        </div>
    </div>
            
          
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnSubmit" />
        <asp:PostBackTrigger ControlID = "btnSend" />
        <asp:PostBackTrigger ControlID = "btnExport" /> 
        <asp:PostBackTrigger ControlID = "btnProofAttachment" /> 

    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>


