<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="URF_ReceivingEntry.aspx.cs" Inherits="REPI_PUR_SOFRA.URF_ReceivingEntry" MasterPageFile="~/Sofra.Master" %>

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
        function sendingRequest() {
            document.getElementById('<%=divLoader.ClientID %>').style.display = "block"; 
        }

    </script>
    
    <script type="text/javascript">
        function setOnLoad() {
            document.getElementById('<%=divOpacity.ClientID %>').style.opacity = "0.5";
            document.getElementById('<%=divLoader.ClientID %>').style.display = "block"; 
        }

    </script>
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        
        <div id="divOpacity" runat="server" class="container-fluid" style="margin-top:-50px; margin-left:-320px; opacity:0.1; width:1280px;"> 
        
        <div class="container-fluid" style="margin-left:-20px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1480px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">URF RECEIVING ENTRY</p>
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
                                        <asp:TextBox ID="txtURFNo" runat="server" Width="320px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" />
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
                                <p style="font-weight:bold; font-size:14px; color:Black; margin-left:30%;">PLEASE WAIT...</p>
                            </div>
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" Visible="false"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCounter" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="50px" Width="28px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbCTRLNo" runat="server" Height="15px" Width="130px" Font-Bold="true" Text='<%# Eval("RhCtrlNo") %>' CommandName="lbCTRLNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:Label ID="lblCTRLNo" runat="server" Height="15px" Width="130px" Font-Bold="true" Text='<%# Eval("RhCtrlNo") %>' Visible="false" />     
                                            <asp:Label ID="lblTransDate" runat="server" Text='<%# Eval("RhTransactionDate") %>' Visible="false" />  
                                            <asp:Label ID="lblReOpenRemarks2" runat="server" Text='<%# Eval("StatReOpenRemarks") %>' Visible="false" />       
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("RhSupplierName") %>' Visible="false" />     
                                            <asp:Label ID="lblReasonName" runat="server" Text='<%# Eval("RhReasonName") %>' Visible="false" />                                                                              
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="165px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("RhCategoryName") %>' Height="30px" Width="165px" Font-Size="11px" />                                              
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
                                    <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuppliers" runat="server" Text='<%# Eval("RhSupplierName") %>' Height="30px" Width="300px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("RhRequester") %>' Height="30px" Width="198px"  />                                               
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Height="60px" Width="148px" Font-Size="11px" ForeColor="White" /> 
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
                                                        <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  data-toggle="tooltip" data-placement="top" title="APPROVED REQUEST" />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibDisapproved" runat="server" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  data-toggle="tooltip" data-placement="top" title="DISAPPROVED REQUEST" />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibClosed" runat="server" ImageUrl="~/images/Close3.png" Width="18px" Height="18px" CommandName="Closed_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" data-toggle="tooltip" data-placement="top" title="CLOSED REQUEST" />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibPDF" runat="server" ImageUrl="~/images/pdf.png" Width="18px" Height="18px" CommandName="PDF_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" data-toggle="tooltip" data-placement="top" title="DOWNLOAD PDF" />  
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
                                            <asp:LinkButton ID="lblPreview" runat="server" Text="PREVIEW" CommandName="lblPreview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>                                                                                             
                                <Columns>
                                    <asp:TemplateField HeaderText="REMARKS" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="75px" Height="16px" Enabled="false" />
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
                                                    <div style="margin-left:20px; margin-top:5px; margin-bottom:5px;">                                                                                                        
                                                    
                                                    <div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblType" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" /></div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblSupplier" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" /></div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblReason" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" /></div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblAttention" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" /></div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblAttachment1" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" Text="ATTACHMENT 1 : " /><asp:LinkButton ID="lbAttachment1" runat="server" Font-Bold="true" Font-Size="13px" Visible="false" /></div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblAttachment2" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" Text="ATTACHMENT 2 : " /><asp:LinkButton ID="lbAttachment2" runat="server" Font-Bold="true" Font-Size="13px" Visible="false" /></div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblStockLifeAttachment" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" Text="STOCK LIFE : " /><asp:LinkButton ID="lbStockLifeAttachment" runat="server" Font-Bold="true" Font-Size="13px" Visible="false" /></div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblReOpenRemarks" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" /></div>
                                                        <div style="margin-top:2px; margin-bottom:2px;"><asp:Label ID="lblSupplierAttachment" runat="server" Font-Bold="true" Font-Size="12px" Visible="false" /></div>
                                                    </div>
                                                    
                                                    <div style="margin-top:3px; margin-bottom:3px;"></div>
                                                    <asp:GridView ID="gvDataDetails" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" Visible="false" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="NO." ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="detailsNum" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="30px" Width="30px" Font-Size="11px" />
                                                                    <asp:Label ID="lblDetailsRefId" runat="server" Text='<%# Eval("RdRefId") %>' Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>                                                        
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="PO.NO." ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPONO" runat="server" Text='<%# Eval("RdPONO") %>' Width="150px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="PR.NO." ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPRNO" runat="server" Text='<%# Eval("RdPRNO") %>' Width="150px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("RdItemName") %>' Width="230px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SPECIFICATION" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("RdSpecs") %>' Width="230px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="QTY." ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("RdQuantity") %>' Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>                                                        
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitOfMeasure" runat="server" Text='<%# Eval("RdUnitOfMeasure") %>' Width="50px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="PO DATE" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDeliveryConfirmationDate" runat="server" Text='<%# Eval("RdDeliveryConfirmationDate") %>' Width="130px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>     
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="REQ. DEL. DATE" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRequestedDeliveryDate" runat="server" Text='<%# Eval("RdRequestedDeliveryDate") %>' Width="130px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="REP. DEL. DATE" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReplyDeliveryDate" runat="server" Text='<%# Eval("RdReplyDeliveryDate") %>' Width="130px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>                                                                                                                                                                                                                                                                                                                                                                                                                         
                                                        
                                                    </asp:GridView>
                                                    
                                                    <div style="margin-top:3px; margin-bottom:3px;"></div>                                                                                                       
                                                    
                                                    <asp:GridView ID="gvDataStatus" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" Visible="false" >
                                                               
                                                               
                                                         <Columns>
                                                            <asp:TemplateField HeaderText="SEC. MANAGER" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProdSecManager" runat="server" Text='<%# Eval("StatProdSecManager") %>' Width="230px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         </Columns> 
                                                         
                                                         <Columns>
                                                            <asp:TemplateField HeaderText="PROD. DEPT. MANAGER" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProdDeptManager" runat="server" Text='<%# Eval("StatProdDeptManager") %>' Width="230px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         </Columns>
                                                         
                                                         <Columns>
                                                            <asp:TemplateField HeaderText="PROD. DIV. MANAGER" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProdDivManager" runat="server" Text='<%# Eval("StatProdDivManager") %>' Width="230px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         </Columns>
                                                         
                                                         <Columns>
                                                            <asp:TemplateField HeaderText="PUR. BUYER" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPurchasingBuyer" runat="server" Text='<%# Eval("StatPurchasingBuyer") %>' Width="230px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         </Columns>
                                                         
                                                         <Columns>
                                                            <asp:TemplateField HeaderText="PUR. MANAGER" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPurchasingManager" runat="server" Text='<%# Eval("StatPurchasingManager") %>' Width="230px" Height="30px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         </Columns>
                                                                
                                                    </asp:GridView>
                                                    
                                                    <!--
                                                         <Columns>
                                                            <asp:TemplateField HeaderText="PROD. HQ. MANAGER" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProdHQManager" runat="server" Text='<%# Eval("StatProdHQManager") %>' Width="180px" Height="20px" Font-Size="11px"/>                                                                                                                                            
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         </Columns>
                                                         -->
                                                    
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
                                            <asp:Label ID="lblCtrlNo" runat="server" Text='<%# Eval("RdCtrlNo") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("RhRequester") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TRANSACTION DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactinDate" runat="server" Text='<%# Eval("RhTransactionDate") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DEPARTMENT" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("LcDepartment") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DIVISION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivision" runat="server" Text='<%# Eval("LcDivision") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PO. NO." ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoNo" runat="server" Text='<%# Eval("RdPONO") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PR. NO." ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrNo" runat="server" Text='<%# Eval("RdPRNO") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("RdItemName") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SPECS" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecs" runat="server" Text='<%# Eval("RdSpecs") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="QUANTITY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("RdQuantity") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("RdUOMDesc") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DELIVERY CONFIRMATION DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryConfirmationDate" runat="server" Text='<%# Eval("RdDeliveryConfirmationDate") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTED DELIVERY DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequestedDeliveryDate" runat="server" Text='<%# Eval("RdRequestedDeliveryDate") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REPLY DELIVERY DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReplyDeliveryDate" runat="server" Text='<%# Eval("RdReplyDeliveryDate") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("RhSupplier") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REASON" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%# Eval("RhReason") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="OTHER REASON" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblOtherReason" runat="server" Text='<%# Eval("RhOtherReason") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TYPE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("RhType") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ATTENTION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttention" runat="server" Text='<%# Eval("RhAttention") %>' />
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
                            <asp:Button ID="btnSend" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSend_Click" OnClientClick="sendingRequest()" />
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
        <asp:PostBackTrigger ControlID = "btnSend" />
        <asp:PostBackTrigger ControlID = "btnExport" /> 
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>
