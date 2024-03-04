<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRF_ApprovalForm.aspx.cs" Inherits="REPI_PUR_SOFRA.CRF_ApprovalForm" MasterPageFile="~/Sofra.Master" %>

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
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        
        <div id="divOpacity" runat="server" class="container-fluid" style="margin-top:-50px; margin-left:-320px; opacity:0.1; width:1280px;"> 
        
        <div class="container-fluid" style="margin-left:-20px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">CRF REQUEST APPROVAL</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:140px; width:1280px;">
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
                                            <asp:ListItem Text="FOR PROD. MNGR APPROVAL" Value="FOR PROD. MNGR APPROVAL" />
                                            <asp:ListItem Text="FOR SUPPLY CHAIN BUYER APPROVAL" Value="FOR SUPPLY CHAIN BUYER APPROVAL" />
                                            <asp:ListItem Text="FOR SUPPLY CHAIN INCHARGE APPROVAL" Value="FOR SUPPLY CHAIN INCHARGE APPROVAL" />
                                        </asp:DropDownList>
                                    </td>                                   
                                    <td><asp:DropDownList ID="ddCategory" runat="server" Width="200px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:TextBox ID="txtCRFNo" runat="server" Width="320px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" />
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
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:100px; width:1280px;">
                            
                            <div id="divLoader" runat="server" style="display:none; width:1280px;">
                                <p style="font-weight:bold; font-size:14px; color:Black; margin-left:30%;">LOADING DATA... PLEASE WAIT...</p>
                            </div>
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     PagerSettings-Mode="NumericFirstLast" PageSize="30" PagerStyle-Font-Size="13px"   
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                <Columns>
                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCounter" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="50px" Width="28px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCTRLNo" runat="server" Height="15px" Width="130px" Text='<%# Eval("CTRLNo") %>' Visible="false" Font-Bold="true" /> 
                                            <asp:LinkButton ID="lbCTRLNo" runat="server" Height="15px" Width="120px" Text='<%# Eval("CTRLNo") %>' CommandName="lbCTRLNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="true" />   
                                            <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("RequesterS") %>'  />  
                                            <asp:Label ID="lblSupplierName" runat="server" Visible="false" Text='<%# Eval("Supplier") %>'  />     
                                            <asp:Label ID="lblAttentionName" runat="server" Visible="false" Text='<%# Eval("Attention") %>'  />  
                                            <asp:Label ID="lblSupplierEmail" runat="server" Visible="false" Text='<%# Eval("SupplierEmail") %>'  />                                      
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>                                
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="170px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>' Height="15px" Width="170px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="220px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier2" runat="server" Height="15px" Width="220px" Text='<%# Eval("Supplier") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="210px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester2" runat="server" Height="15px" Width="210px" Text='<%# Eval("Requester") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="215px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Height="16px" Width="215px" Font-Size="11px" ForeColor="White" data-toggle="tooltip" data-placement="top" title='<%# Eval("StatRemarks") %>' />
                                            <asp:Label ID="lblStatColor" runat="server" Text='<%# Eval("CssColorCode") %>' Visible="false" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="FOR SENDING?" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddForSending" runat="server" Width="100px" >
                                                <asp:ListItem Text="" Value="0" /> 
                                                <asp:ListItem Text="YES" Value="1" />
                                                <asp:ListItem Text="NO" Value="2" />
                                            </asp:DropDownList>
                                            <asp:Label ID="lblForSending" runat="server" Visible="false" Text='<%# Eval("ForSending") %>' />                                   
                                        </ItemTemplate> 
                                    </asp:TemplateField>                                   
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="55px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center" style="margin-left:1px;">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ToolTip="APPROVED BUTTON" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>
                                                    <td align="center" style="margin-left:1px;">
                                                        <asp:ImageButton ID="ibDisapproved" runat="server" ToolTip="DISAPPROVED BUTTON" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>  
                                                    <td align="center" style="margin-left:1px;">
                                                        <asp:ImageButton ID="ibPreview" runat="server" ToolTip="PREVIEW BUTTON" ImageUrl="~/images/Report.png" Width="20px" Height="20px" CommandName="ibPreview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibManualResponse" runat="server" ToolTip="MANUAL RESPONSE BUTTON" Visible="false" ImageUrl="~/images/ManualResponse.png" Width="20px" Height="20px" CommandName="ibManualResponse_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>                                                    
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REMARKS" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="80px" Height="16px" Enabled="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <%--<Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblView" runat="server" Text="OPEN DETAILS" CommandName="lblView_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad()" />&nbsp;/&nbsp;
                                            <asp:LinkButton ID="lbReOpen" runat="server" Text="RE-OPEN" CommandName="lbReOpen_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Visible="false" />
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns> --%> 
                                
                                 <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                 <td colspan="100%" style="background-color:White;">
                                                 
                                                    <div style="margin-left:20px; margin-top:5px; margin-bottom:5px;">
                                                    
                                                            <asp:GridView ID="gvDataStatus" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                    HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                    HeaderStyle-BackColor="DarkGray" HeaderStyle-ForeColor="Black" Visible="false" OnRowDataBound="gvDataStatus_OnRowDataBound" >                                                                                                               
                                                            
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="PRODUCTION MANAGER" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProdManagerStat" runat="server" Text='<%# Eval("ReqManagerStatDisplay") %>' ForeColor="White" Width="392px" Height="15px" Font-Size="11px"/>   
                                                                            <asp:Label ID="lblProdManagerStatColor" runat="server" Text='<%# Eval("ReqManagerStatColor") %>' Visible="false"/>                                                                                                                                          
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SUPPLY CHAIN BUYER" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBuyerStat" runat="server" Text='<%# Eval("BuyerStatDisplay") %>' ForeColor="White" Width="392px" Height="15px" Font-Size="11px"/>      
                                                                            <asp:Label ID="lblBuyerStatColor" runat="server" Text='<%# Eval("PurInchargeStatColor") %>' Visible="false"/>                                                                                                                                        
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SUPPLY CHAIN MANAGER" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSCDManagerStat" runat="server" Text='<%# Eval("ScInchargeStatDisplay") %>' ForeColor="White" Width="392px" Height="15px" Font-Size="11px"/> 
                                                                            <asp:Label ID="lblSCDManagerStatColor" runat="server" Text='<%# Eval("PurManagerStatColor") %>' Visible="false"/>                                                                                                                                             
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>

                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                                            
                                                            </asp:GridView>                                                                                                                                                                                   
                                                    
                                                    </div> 
                                                    
                                                    <div style="margin-left:20px; margin-top:5px; margin-bottom:5px;">
                                                    
                                                            <asp:GridView ID="gvDataHead" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                    HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                    HeaderStyle-BackColor="DarkGray" HeaderStyle-ForeColor="Black" Visible="false" >                                                                                                               
                                                            
                                                                <%--<Columns>
                                                                    <asp:TemplateField HeaderText="REQUESTER" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("Requester") %>' Width="250px" Height="15px" Font-Size="11px"/>                                                                                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>--%>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="TRANSACTION DATE" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTransactionDateHead" runat="server" Text='<%# Eval("TransactionDate") %>' Width="350px" Height="15px" Font-Size="11px"/>                                                                                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <%--<Columns>
                                                                    <asp:TemplateField HeaderText="CATEGORY" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCategoryHead" runat="server" Text='<%# Eval("Category") %>' Width="200px" Height="15px" Font-Size="11px"/>                                                                                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SUPPLIER" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSupplierHead" runat="server" Text='<%# Eval("SupplierName") %>' Width="395px" Height="15px" Font-Size="11px"/>                                                                                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>--%>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ATTENTION" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAttentionHead" runat="server" Text='<%# Eval("Attention") %>' Width="827px" Height="15px" Font-Size="11px"/>                                                                                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                                                            
                                                            </asp:GridView>                                                                                                                                                                                   
                                                    
                                                    </div>         
                                                    
                                                    
                                                    <div style="margin-left:20px;">
                                                        
                                                            <asp:GridView ID="gvDataDetails" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                        HeaderStyle-Font-Bold="false" RowStyle-Height="25px" HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvDataDetails_RowCommand"
                                                                        HeaderStyle-BackColor="DarkGray" HeaderStyle-ForeColor="Black" Visible="false" OnRowDataBound="gvDataDetails_OnRowDataBound">                                                                                                               
                                                                
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="DETAILS" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1180px" ItemStyle-Height="10px">
                                                                            <ItemTemplate>   
                                                                            <asp:Label ID="lblCTRLNoLabel" runat="server" Text='<%# Eval("RdCtrlNo") %>' Visible="false" />    
                                                                            <asp:Label ID="lblDisapprovalCause2" runat="server" Text='<%# Eval("StatRemarks") %>' Visible="false" />
                                                                                <tr id="trDisapprovalCause" runat="server" style="display:none;">
                                                                                    <td>
                                                                                        <asp:Label ID="lblDisapprovalCause" runat="server" Text="DISAPPROVAL CAUSE: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtDisapprovalCause" ReadOnly="true" runat="server" Text='<%# Eval("StatRemarks") %>' Width="1044px" Height="18px" Font-Size="11px" />
                                                                                    </td>
                                                                                </tr>                                                                             
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblPONOLabel" runat="server" Text="PO. NUMBER: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtPONOHead" runat="server" ReadOnly="true" Text='<%# Eval("RdPONO") %>' Width="200px" Height="18px" Font-Size="11px" />
                                                                                        <asp:Label ID="lblPRNOLabel" runat="server" Text="PR. NUMBER: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtPRNOHead" runat="server" ReadOnly="true" Text='<%# Eval("RdPRNO") %>' Width="200px" Height="18px" Font-Size="11px" />
                                                                                        <asp:Label ID="lblDescriptionLabel" runat="server" Text="DESCRIPTION: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtItemNameHead" runat="server" ReadOnly="true" Text='<%# Eval("RdItemName") %>' Width="527px" Height="18px" Font-Size="11px" />
                                                                                    </td>                                                                                    
                                                                                </tr>    
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblQTYLabel" runat="server" Text="QTY.: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtQtyHead" runat="server" ReadOnly="true" Text='<%# Eval("RdQuantity") %>' Width="245px" Height="18px" Font-Size="11px" /> 
                                                                                        <asp:Label ID="lblUOMLabel" runat="server" Text="UNIT OF MEASURE: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtUOMHead" runat="server" ReadOnly="true" Text='<%# Eval("RdUOMDesc") %>' Width="167px" Height="18px" Font-Size="11px" />
                                                                                        <asp:Label ID="lblTypeDrawingLabel" runat="server" Text="TYPE / DRAWING NO.: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtSpecificationHead" ReadOnly="true" runat="server" Text='<%# Eval("RdSpecs") %>' Width="489px" Height="18px" Font-Size="11px" />                                                                                        
                                                                                    </td>
                                                                                </tr> 
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblPODateLabel" runat="server" Text="PO. DATE: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtPODateHead" runat="server" ReadOnly="true" Text='<%# Eval("RdPODate") %>' Width="218px" Height="18px" Font-Size="11px" />
                                                                                        <asp:Label ID="lblReasonLabel" runat="server" Text="REASON: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtReasonHead" runat="server" ReadOnly="true" Text='<%# Eval("RdReasonName") %>' Width="223px" Height="18px" Font-Size="11px" />
                                                                                        <asp:Label ID="lblDISupplierSupplierLabel" runat="server" Text="DI. SUPPLIER: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtDISupplier" ReadOnly="true" runat="server" Text='<%# Eval("RdDateInformedSupplier") %>' Width="209px" Height="18px" Font-Size="11px" />
                                                                                        <asp:Label ID="lblDISupplierRequesterLabel" runat="server" Text="DI. REQUESTER: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtDIRequester" ReadOnly="true" runat="server" Text='<%# Eval("RdDateInformedRequester") %>' Width="224px" Height="18px" Font-Size="11px" /> 
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblRequesterAttachmentLabel" runat="server" Text="REQUESTER ATTACHMENT: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:LinkButton ID="lbRequesterAttachment" runat="server" Text='<%# Eval("RdAttachment") %>' CommandName="lbRequesterAttachment_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="14px" Font-Size="11px" />  
                                                                                    </td>
                                                                                </tr>  
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblConfirmedByLabel" runat="server" Text="SUPPLIER RESPONSE (CONFIRMED BY): " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtConfirmedBy" ReadOnly="true" runat="server" Text='<%# Eval("ResponseConfirmedBy") %>' Width="337px" Height="18px" Font-Size="11px" />
                                                                                        <asp:Label ID="lblDateConfirmedLabel" runat="server" Text="SUPPLIER RESPONSE (DATE CONFIRMED): " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtDateConfirmed" ReadOnly="true" runat="server" Text='<%# Eval("ResponseDateConfirmed") %>' Width="373px" Height="18px" Font-Size="11px" />
                                                                                    </td>
                                                                                </tr>  
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblNotesLabel" runat="server" Text="SUPPLIER RESPONSE (NOTES): " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtNotes" runat="server" ReadOnly="true" Text='<%# Eval("ResponseNotes") %>' Width="677px" Height="18px" Font-Size="11px" />  
                                                                                        <asp:Label ID="lblDateReceivedLable" runat="server" Text="DATE RECEIVED: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:TextBox ID="txtDateReceived" runat="server" ReadOnly="true" Text='<%# Eval("ResponseDateReceived") %>' Width="220px" Height="18px" Font-Size="11px" />                                                                                          
                                                                                    </td>
                                                                                </tr> 
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblAttachmentLabel" runat="server" Text="SUPPLIER ATTACHMENT: " ForeColor="Black" Font-Bold="true" Font-Size="11px" Height="14px" />&nbsp;<asp:LinkButton ID="lbSupplierAttachment" runat="server" Text='<%# Eval("RdSupplierAttachment") %>' CommandName="lbSupplierAttachment_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="14px" Font-Size="11px" />  
                                                                                    </td>
                                                                                </tr>                                                                     
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
                            
                            <asp:GridView ID="gvExport" runat="server" AutoGenerateColumns="false" Visible="false">
                            
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNO" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCtrlNo" runat="server" Text='<%# Eval("RdCtrlNo") %>' />
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
                                            <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("SupplierName") %>' />
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
                                    <asp:TemplateField HeaderText="PR. NO." ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrNo" runat="server" Text='<%# Eval("RdPRNO") %>' />
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
                                    <asp:TemplateField HeaderText="PO. DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoDate" runat="server" Text='<%# Eval("RdPODate") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("RdUOMDesc") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("RdItemName") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TYPE / DRAWING NO." ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypeDrawing" runat="server" Text='<%# Eval("RdSpecs") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ORDER QUANTITY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderQuantity" runat="server" Text='<%# Eval("RdQuantity") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DATE INFORMED (SUPPLIER)" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateInformedSupplier" runat="server" Text='<%# Eval("RdDateInformedSupplier") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DATE INFORMED (REQUESTOR)" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateInformedRequestor" runat="server" Text='<%# Eval("RdDateInformedRequester") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="RESPONSE CONFIRMED BY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponseConfirmedBy" runat="server" Text='<%# Eval("ResponseConfirmedBy") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="RESPONSE CONFIRMED DATE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponseDateConfirmed" runat="server" Text='<%# Eval("ResponseDateConfirmed") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="RESPONSE NOTES" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponseNotes" runat="server" Text='<%# Eval("ResponseNotes") %>' />
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
                        <div class="body" style="margin-top:-23px; height:65px; width:1280px;">
                            <asp:Button ID="btnSend" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSend_Click" />
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


