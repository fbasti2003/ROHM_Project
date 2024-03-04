<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_PurchasingReceiving.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_PurchasingReceiving" MasterPageFile="~/Sofra.Master" %>

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
    
    <script type="text/javascript">
        function setOnLoad() {
            document.getElementById('<%=divOpacity.ClientID %>').style.opacity = "0.5";
            document.getElementById('<%=divLoader.ClientID %>').style.display = "block"; 
        }
        

    </script>
    
    <script type="text/javascript">
       var normalurl = '~/images/A1.png',
         selectedurl = '~/images/loader1.png';
       function swapImage(ctrl) {
           ctrl.src = (ctrl.src == normalurl) ? selectedurl : normalurl;
           return false;
       }
    </script>
    
   
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">                                
        
        <div id="divOpacity" runat="server" class="container-fluid" style="margin-top:-50px; margin-left:-320px; opacity:0.1; width:1280px;">                    
                                              
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <table style="width:1060px;">
                                <tr>
                                    <th><p style="color:Gray; font-size:14px; font-weight:bold;">RFQ - PURCHASING RECEIVING ENTRY</p></th>
                                </tr>
                            </table>                            
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
                                <table style="width:950px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th> 
                                    <th>CATEGORY</th>
                                    <th>TYPE</th>
                                    <th>ITEM TO SEARCH</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="120px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="120px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td> 
                                    <td><asp:DropDownList ID="ddCategory" runat="server" Height="28px" Width="250px" Font-Size="14px" class="form-control" /></td>  
                                    <td>
                                        <asp:DropDownList ID="ddType" runat="server" Width="150px" Height="28px" class="form-control">
                                            <asp:ListItem Text="ALL" Value="ALL" />
                                            <asp:ListItem Text="FOR SENDING" Value="FOR SENDING" />
                                            <asp:ListItem Text="FOR RESEND" Value="FOR RESEND" />
                                        </asp:DropDownList>
                                    </td>
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="200px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" OnClientClick="setOnLoad()" />
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
                        <div class="body" style="margin-top:-23px; width:1280px;">
                            
                            <div id="divLoader" runat="server" style="display:none; width:1280px;">
                                <p style="font-weight:bold; font-size:14px; color:Black; margin-left:30%;">LOADING DATA... PLEASE WAIT...</p>
                            </div>
        
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!" >
                                <Columns>
                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCounter" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="50px" Width="28px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="150px" Font-Bold="true" Text='<%# Eval("RhRFQNo") %>'  />                                                                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="MNGR APPROVED DATE" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransDate" runat="server" Height="15px" Width="140px" Text='<%# Eval("RhProdManagerApprovedDate") %>'  />    
                                            <asp:Label ID="lblTransDate2" runat="server" Visible="false" Text='<%# Eval("RhTransactionDate") %>'  />    
                                            <asp:Label ID="CntSuppResp" runat="server" Visible="false" Text='<%# Eval("CntSuppResp") %>'  />                                                                                    
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SEND DATES" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddSendDates" runat="server" Width="90px" Height="15px" />                                           
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="220px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("RhCategory") %>' Height="15px" Width="220px" Font-Size="11px" />   
                                            <asp:DropDownList ID="ddCategory" runat="server" Height="15px" Width="220px" Font-Size="11px" Visible="false" />                                           
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="230px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("RhRequester") %>' Height="15px" Width="230px" />                                               
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="RESPONSED" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblHasResponse" runat="server" Text='<%# Eval("ResponseNumberOfResponded") %>' Height="15px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQ.ATT." ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequesterAttachment" runat="server" Text='<%# Eval("RequesterAttachment") %>' Height="15px" />                                            
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="swapImage(this); setOnLoad();" ToolTip="APPROVED" />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibDisapproved" runat="server" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="swapImage(this); setOnLoad();" ToolTip="DISAPPROVED"  />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibPreview" runat="server" ImageUrl="~/images/Preview.png" Width="20px" Height="20px" CommandName="Preview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="PREVIEW"  />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibHold" runat="server" ImageUrl="~/images/hold.png" Width="20px" Height="20px" Visible="false" CommandName="Hold_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="HOLD" />  
                                                    </td>
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="REMARKS / CAUSE / NOTE" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="150px" Height="16px" Enabled="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>       
                                
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            
                                            <tr>
                                                <td colspan="100%" style="background-color:White;">
                                                    <div id="divDetails" runat="server" style="margin-left:7px;">
                                                        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                            HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                            HeaderStyle-BackColor="#009688" HeaderStyle-ForeColor="White" Visible="false"
                                                                            OnRowDataBound="gvDetails_OnRowDataBound" OnRowCommand="gvDetails_RowCommand">
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="detailsNum" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="30px" Font-Size="11px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblDescription" runat="server" Width="265px" Text='<%# Eval("RdDescription") %>' /> 
                                                                         <asp:LinkButton ID="linkDescription" runat="server" Width="250px" Visible="false" Text='<%# Eval("RdDescription") %>' CommandName="linkDescription_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad()" /> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                                  
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SPECS/DRAWING NO." ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblSpecsDrawing" runat="server" Width="265px" Text='<%# Eval("RdSpecs") %>' />
                                                                         <asp:LinkButton ID="linkSpecsDrawing" runat="server" Width="200px" Visible="false" Text='<%# Eval("RdSpecs") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>     
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="MAKER" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblMaker" runat="server" Width="100px" Text='<%# Eval("RdMaker") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>   
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblQty" runat="server" Width="40px" Text='<%# Eval("RdQuantity") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns> 
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                          <asp:Label ID="lblUOM" runat="server" Width="40px" Text='<%# Eval("RdUnitOfMeasure") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns> 
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="PURPOSE/USE & <br /> PICTURE OF ITEM" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                          <asp:Label ID="lblPurpose" runat="server" Width="150px" Text='<%# Eval("RdPurpose") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="PROCESS/MACHINE" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                          <asp:Label ID="lblProcess" runat="server" Width="150px" Text='<%# Eval("RdProcess") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                          <asp:Label ID="lblRemarks" runat="server" Width="150px" Text='<%# Eval("RdRemarks") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>                                                                                                                                                                                                                                                                                                                                                                                                               
                                                            
                                                        </asp:GridView>
                                                    </div>
                                                    
                                                    <div id="divPopUp" runat="server" style="margin-left:7px; overflow-y:auto; display:none;">
                                                        <asp:GridView ID="gvPopUpDetails" runat="server"
                                                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                          OnRowCommand="gvPopUpDetails_RowCommand"
                                                                          HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" >
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="RFQNO">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("Requester") %>' />
                                                                                <asp:Label ID="lblTransactionDate" runat="server" Visible="false" Text='<%# Eval("TransactionDate") %>' />
                                                                                <asp:Label ID="lblCategoryName" runat="server" Visible="false" Text='<%# Eval("CategoryName") %>' />
                                                                                <asp:Label ID="lblStatDivManager" runat="server" Visible="false" Text='<%# Eval("StatDivManager") %>' />
                                                                                <asp:LinkButton ID="linkRFQNo" runat="server" Width="150px" Text='<%# Eval("RdRfqNo") %>' CommandName="linkRFQNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDescription" runat="server" Width="250px" Text='<%# Eval("RdDescription") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSpecsDrawing" runat="server" Width="250px" Text='<%# Eval("RdSpecs") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="MAKER">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMaker" runat="server" Width="150px" Text='<%# Eval("RdMaker") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>                                  
                                                                        
                                                                    </Columns>

                                                            </asp:GridView>

                                                    </div>
                                                    
                                                    <div id="divSupplierDetails" runat="server" style="margin-left:7px;">
                                                        <asp:GridView ID="gvSupplierDetails" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                            HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                            HeaderStyle-BackColor="#009688" HeaderStyle-ForeColor="White" Visible="false" 
                                                                            OnRowDataBound="gvSupplierDetails_OnRowDataBound">
                                                            
                                                                                                                   
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SUPPLIER" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblSupplier" runat="server" Width="750px" Text='<%# Eval("ResponseSupplierName") %>' /> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                                  
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="RESPONSE DATE" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblResponseDate" runat="server" Width="225px" Text='<%# Eval("ResponseResponseDate") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>                                                                 
                                                            
                                                            
                                                        </asp:GridView>
                                                    </div>
                                                    
                                                    <div id="divStatus" runat="server" style="margin-left:7px;">
                                                        <asp:GridView ID="gvStatus" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                            HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                            HeaderStyle-BackColor="#009688" HeaderStyle-ForeColor="White" Visible="false" 
                                                                            OnRowDataBound="gvStatus_OnRowDataBound">
                                                            
                                                                                                                   
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="PROD MNGR." ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblProdManager" runat="server" Width="190px" Text='<%# Eval("StatProdManager") %>' Font-Bold="true" /> 
                                                                         <asp:Label ID="lblProdManagerStatus" runat="server" Visible="false" Text='<%# Eval("ProdManagerStatus") %>' /> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                                  
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BUYER" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblBuyer" runat="server" Width="190px" Text='<%# Eval("StatBuyer") %>' Font-Bold="true" /> 
                                                                         <asp:Label ID="lblBuyerStatus" runat="server" Visible="false" Text='<%# Eval("BuyerStatus") %>' /> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="INCHARGE" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblIncharge" runat="server" Width="190px" Text='<%# Eval("StatPurchasingIncharge") %>' Font-Bold="true" /> 
                                                                         <asp:Label ID="lblInchargeStatus" runat="server" Visible="false" Text='<%# Eval("PurchasingInchargeStatus") %>' /> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="DEPT. MNGR" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblDepartmentManager" runat="server" Width="190px" Text='<%# Eval("StatDeptManager") %>' Font-Bold="true" /> 
                                                                         <asp:Label ID="lblDepartmentManagerStatus" runat="server" Visible="false" Text='<%# Eval("DepartmentManagerStatus") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="DIV. MNGR" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblDivisionManager" runat="server" Width="190px" Text='<%# Eval("StatDivManager") %>' Font-Bold="true" /> 
                                                                         <asp:Label ID="lblDivisionManagerStatus" runat="server" Visible="false" Text='<%# Eval("DivisionManagerStatus") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="LEAD TIME" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lblLeadTime" runat="server" Width="220px" Text='<%# Eval("RhLeadTime") %>' /> 
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                                                                                        
                                                        </asp:GridView>
                                                    </div>
                                                    
                                                    <div id="divHoldReason" runat="server" style="display:none;">
                                                    
                                                        <div class="body" style="margin-top:-23px; min-height:80px; width:1280px;">
                            
                                                            <table style="width:100%; color:Gray; margin-top:10px;">
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
                                                                                <asp:Label ID="lblHoldDate" runat="server" Width="220px" Text='<%# Eval("Hold_Date") %>' />
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
                                                    
                                                </td>
                                            </tr>
                                            
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>                                                                                                                       
                                
                            </asp:GridView>
                            
                            <br />
                            
                            <p id="pRelatedResult" runat="server" style="display:none; font-size:12px;">RELATED SEARCH RESULT</p>
                            <asp:GridView ID="gvRelatedSearch" runat="server" Visible="false"
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          OnRowCommand="gvRelatedSearch_RowCommand" OnRowDataBound="gvRelatedSearch_OnRowDataBound"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="RFQNO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("Requester") %>' />
                                                <asp:Label ID="lblTransactionDate" runat="server" Visible="false" Text='<%# Eval("TransactionDate") %>' />
                                                <asp:Label ID="lblCategoryName" runat="server" Visible="false" Text='<%# Eval("CategoryName") %>' />
                                                <asp:Label ID="lblStatDivManager" runat="server" Visible="false" Text='<%# Eval("StatDivManager") %>' />
                                                <asp:LinkButton ID="linkRFQNo" runat="server" Text='<%# Eval("RdRfqNo") %>' Width="120px" CommandName="linkRFQNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Width="255px" Text='<%# Eval("RdDescription") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecsDrawing" runat="server" Width="255px" Text='<%# Eval("RdSpecs") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAKER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Width="152px" Text='<%# Eval("RdMaker") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatAll" runat="server" Width="200px" Text='<%# Eval("StatAll") %>' ForeColor="White" Font-Bold="true" />
                                                <asp:Label ID="lblStatColor" runat="server" Width="200px" Text='<%# Eval("CssColorCode") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                               
                                        
                                    </Columns>

                            </asp:GridView>
                            
                        </div>                        
                    </div>
                </div>
            </div>   
                    
            
            <div class="row clearfix" id="divActionButtons" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:65px; width:1075px;">
                            <asp:Button ID="btnSend" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSend_Click" />
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
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>