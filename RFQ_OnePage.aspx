<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_OnePage.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_OnePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <title>RFQ OnePage Form</title>
    
    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" type="text/css" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" type="text/css" />
    <link href="plugins/morrisjs/morris.css" rel="stylesheet" type="text/css" />            
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/themes/all-themes.css" rel="stylesheet" type="text/css" />
    
    <link href="plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" type="text/css" /> 
    <link href="plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" type="text/css" />
    <link href="plugins/nouislider/nouislider.min.css" rel="stylesheet" type="text/css" />
    <link href="plugins/bootstrap-tagsinput/bootstrap-tagsinput.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-datepicker.css" rel="stylesheet" type="text/css" />
    
    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" type="text/css" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" type="text/css" />
    <link href="plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/themes/all-themes.css" rel="stylesheet" type="text/css" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" type="text/css" /> 
    


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
    
    <script type="text/javascript">
        function setOnLoad() {
            document.getElementById('<%=divOpacity.ClientID %>').style.opacity = "0.5";
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
    
    <script type="text/javascript">
        function loadSearch() {
            // Get the input field
            var input = document.getElementById('<%=txtSearch.ClientID %>');

            // Execute a function when the user releases a key on the keyboard
            input.addEventListener("keyup", function(event) {
              // Number 13 is the "Enter" key on the keyboard
              if (event.keyCode === 13) {
                // Cancel the default action, if needed
                event.preventDefault();
                // Trigger the button element with a click
                document.getElementById('<%=btnSubmit.ClientID %>').click();
              }
            });
        }
    </script>
    
    <script type="text/javascript">
        function handleEnter(obj, event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
            
                document.getElementById('<%=btnSubmit.ClientID %>').click();
                
                return false;
            }
            else {
                return true;
            }
        }
        function handleEnterReceiving(obj, event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
            
                document.getElementById('<%=btnSubmitReceiving.ClientID %>').click();
                
                return false;
            }
            else {
                return true;
            }
        }
        function handleEnterApproval(obj, event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
            
                document.getElementById('<%=btnSubmitForApproval.ClientID %>').click();
                
                return false;
            }
            else {
                return true;
            }
        }
        
        function handleEnterSendingQuotation(obj, event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
            
                document.getElementById('<%=btnSubmitForSendingQuotation.ClientID %>').click();
                
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    
    
</head>
<body class="theme-red">
    <form id="form1" runat="server">
    
    <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    
    <div id="divOpacity" runat="server">
    
        <div style="border: none; position: fixed; top: 0; left: 0; z-index: 12; width: 100%; background-color: #00BCD4; height:25px;">
            <div style="height:25px;">
                <div style="height:25px;">
                    <a style="padding:30px 0 0 5px; color:White;" href="Dashboard.aspx">PURCHASING SOLUTION FRAMEWORK</a>
                    <div style="float:right; padding-right:60px;">
                        <asp:Label ID="lblUser" runat="server" ForeColor="White" Font-Size="14px" />
                        <asp:LinkButton ID="lbLogOut" runat="server" Text="LOG-OUT" Font-Size="11px" Font-Bold="true" OnClick="lbLogOut_Click" />
                    </div>
                </div>            
            </div>
        </div>
        
        
        <div class="table-responsive" style="margin-top:40px;">
        
            <ul class="nav nav-tabs tab-nav-right" role="tablist">
                <li style="margin-right:3px; margin-left:10px;"><asp:Button ID="btnAllRequest" runat="server" OnClick="btnAllRequest_Click" OnClientClick="setOnLoad();" Text="ALL REQUEST" Height="28px" CssClass="btn bg-orange waves-effect" /></li>
                <li style="margin-right:3px;"><asp:Button ID="btnReceivingSending" runat="server" OnClick="btnReceivingSending_Click" OnClientClick="setOnLoad();" Text="RFQ QUOTATION - FOR SENDING" Height="28px" CssClass="btn bg-orange waves-effect" /></li>       
                <li style="margin-right:3px;"><asp:Button ID="btnReceivingResend" runat="server" OnClick="btnReceivingResend_Click" OnClientClick="setOnLoad();" Text="RFQ QUOTATION - FOR RESEND" Height="28px" CssClass="btn bg-orange waves-effect" /></li>       
                <li style="margin-right:3px;"><asp:Button ID="btnApproval" runat="server" OnClick="btnApproval_Click" OnClientClick="setOnLoad();" Text="RFQ REQUEST APPROVAL" Height="28px" CssClass="btn bg-orange waves-effect" /></li>                 
                <li><asp:Button ID="btnHomePage" runat="server" OnClick="btnHomePage_Click" OnClientClick="setOnLoad();" Text="HOME PAGE" Height="28px" CssClass="btn bg-red waves-effect" /></li>                 
            </ul>
                    
            <div style="margin-top:30px;">
                
                <div id="tabAllRequest" runat="server">
                
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-10px; height:80px; width:950px;">
                                    <div>
                                        <table style="width:950px; color:Gray; font-size:12px;">
                                          <tr>
                                            <th>ENTER ITEM YOU WANT TO SEARCH</th>
                                            <th style="color:White;">DUMMY</th>
                                          </tr>
                                          <tr> 
                                            <td><asp:TextBox ID="txtSearch" runat="server" Width="850px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" onkeypress="return handleEnter('btnSubmit',event);" /></td>
                                            <td>
                                                <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" OnClientClick="setOnLoad();" />
                                            </td>                          
                                          </tr>
                                        </table>
                                    </div>
                                </div>                        
                            </div>
                        </div>
                    </div>
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; width:1360px;">
                                    
                                    <asp:GridView ID="gvData" runat="server"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  OnRowCommand="gvData_RowCommand" OnRowDataBound="gvData_OnRowDataBound"
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                            <Columns>

                                                <asp:TemplateField HeaderText="RFQNO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("Requester") %>' />
                                                        <asp:Label ID="lblSection" runat="server" Visible="false" Text='<%# Eval("RhSection") %>' />
                                                        <asp:Label ID="lblDepartment" runat="server" Visible="false" Text='<%# Eval("RhDepartment") %>' />
                                                        <asp:Label ID="lblDivision" runat="server" Visible="false" Text='<%# Eval("RhDivision") %>' />
                                                        <asp:Label ID="lblTransactionDate" runat="server" Visible="false" Text='<%# Eval("TransactionDate") %>' />   
                                                        <asp:Label ID="lblCategory" runat="server" Visible="false" Text='<%# Eval("RhCategory") %>' />                                                       
                                                        <asp:Label ID="lblStatDivManager" runat="server" Visible="false" Text='<%# Eval("StatDivManager") %>' />
                                                        <asp:LinkButton ID="linkRFQNo" runat="server" Text='<%# Eval("RdRfqNo") %>' Width="120px" CommandName="linkRFQNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DESCRIPTION">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Width="155px" Text='<%# Eval("RdDescription") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpecsDrawing" runat="server" Width="155px" Text='<%# Eval("RdSpecs") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RESPONDED SUPPLIER">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddRespondedSupplier" runat="server" Width="200px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAKER">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaker" runat="server" Width="142px" Text='<%# Eval("RdMaker") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="CATEGORY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategoryName" runat="server" Width="130px" Text='<%# Eval("CategoryName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                                <asp:TemplateField HeaderText="STATUS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatAll" runat="server" Width="145px" Text='<%# Eval("StatAll") %>' ForeColor="White" Font-Bold="true" />
                                                        <asp:Label ID="lblStatColor" runat="server" Width="145px" Text='<%# Eval("CssColorCode") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="230px">
                                                    <ItemTemplate>
                                                        <div style="margin-left:3px; margin-right:10px; float:left;">                                                            
                                                            <asp:Button ID="btnReceiving" runat="server" Height="20px" Font-Bold="true" Width="100px" CommandName="btnReceiving_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();" />
                                                        </div>
                                                        <div style="margin-left:3px; margin-right:3px; float:left">
                                                            <asp:Button ID="btnApproval" runat="server" Height="20px" Font-Bold="true" Width="110px" CommandName="btnApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();" />
                                                        </div> 
                                                        <div style="margin-left:3px; margin-right:3px; float:left">
                                                            <asp:Button ID="btnPreview" runat="server" Height="20px" Font-Bold="true" Width="110px" CommandName="btnPreview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="PRINT PREVIEW" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                                <asp:TemplateField>
                                                    <ItemTemplate> 
                                                    
                                                            <tr>
                                                                <td colspan="100%" style="background-color:White;">
                                                                
                                                                    <div style="margin-left:7px; margin-bottom:5px;">
                                                                        <asp:Label ID="lblRequestDetails" runat="server" Visible="false" Font-Bold="true" Font-Size="11px" />
                                                                    </div>
                                                                    
                                                                    <div style="margin-left:7px; margin-top:10px; margin-bottom:5px;">
                                                                    
                                                                        <asp:Label ID="lblRequesterAttachment" runat="server" Text="REQUEST ATTACHMENT" Font-Bold="true" Visible="false" />
                                                                        <asp:GridView ID="gvDataRequesterAttachment" runat="server" Visible="false"
                                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                                      HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDescription" runat="server" Width="243px" Text='<%# Eval("RdDescription") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="SPECIFICATION">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSpecification" runat="server" Width="243px" Text='<%# Eval("RdSpecs") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="ATTACHMENT" HeaderStyle-Width="300px">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("RdAttachmentLink")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>                                                                                   
                                                                                
                                                                                </Columns>

                                                                        </asp:GridView>                                                                        
                                                                    
                                                                    </div>
                                                                    
                                                                    <div style="margin-left:7px; margin-top:10px; margin-bottom:5px;">
                                                                    <asp:Label ID="lblResponseAllRequest" runat="server" Text="SUPPLIER RESPONSE" Font-Bold="true" Visible="false" />
                                                                    <asp:GridView ID="gvResponseAllRequest" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                                        HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                                        HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" Visible="false"
                                                                                        OnRowDataBound="gvResponseAllRequest_OnRowDataBound">
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
                                                                                    <asp:Label ID="lblResponseDescription" runat="server" Text='<%# Eval("ResponseDescription") %>' Width="270px" Height="20px" Font-Size="11px"/>                                                                        
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
                                                                                    <asp:Label ID="lblResponseSupplier" runat="server" Text='<%# Eval("ResponseSupplierName") %>' Height="15px" Width="400px" Font-Size="11px" />
                                                                                    <asp:Label ID="lblResponseSupplierID" runat="server" Text='<%# Eval("ResponseSupplierID") %>' Height="15px" Width="210px" Font-Size="11px" Visible="false" />
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
                                                                                    <asp:TextBox ID="txtResponsePurchasingRemarks" runat="server" Text='<%# Eval("ResponsePurchasingRemarks") %>' Height="20px" TextMode="MultiLine" Width="170px" Font-Size="11px" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>  
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
                                                                        
                                                                    </asp:GridView>
                                                                    </div>
                                                                    
                                                                    <div style="margin-left:7px; margin-top:10px; margin-bottom:5px;">
                                                                        <asp:Label ID="lblAllRequestSupplierHistory" runat="server" Text="SUPPLIER HISTORY" Font-Bold="true" Visible="false" />
                                                                        <asp:GridView ID="gvSuppliers" runat="server" Visible="false"
                                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                                      HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="SUPPLIER">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSupplier" runat="server" Width="485px" Text='<%# Eval("ResponseSupplierName") %>' />
                                                                                            <asp:Label ID="lblSupplierID" runat="server" Visible="false" Text='<%# Eval("ResponseSupplierID") %>' />
                                                                                            <asp:Label ID="lblResponseCount" runat="server" Visible="false" Text='<%# Eval("ResponseCount") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="RESPONSE DATE">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblResponseDate" runat="server" Width="165px" Text='<%# Eval("ResponseResponseDate") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>                                                                                   
                                                                                
                                                                                </Columns>

                                                                        </asp:GridView>
                                                                    
                                                                    </div>
                                                                    
                                                                    <div style="margin-left:7px; margin-top:10px; margin-bottom:5px;">
                                                                        <table>
                                                                            <tr>
                                                                                <td><asp:Label ID="lblSendDates" runat="server" Text="SEND DATES" Font-Bold="true" Visible="false" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><asp:DropDownList ID="ddSendDates" runat="server" Width="180px" Height="20px" Visible="false"  /></td>
                                                                            </tr>
                                                                        </table>                                                                                                                                                
                                                                    </div>
                                                                    
                                                                    <div style="margin-left:7px; margin-top:10px; margin-bottom:5px;">
                                                                    
                                                                        <asp:Label ID="lblSupplierAttachment" runat="server" Text="SUPPLIER ATTACHMENT" Font-Bold="true" Visible="false" />
                                                                        <asp:GridView ID="gvSupplierAttachment" runat="server" Visible="false"
                                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                                      HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="SUPPLIER">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSupplierName" runat="server" Width="485px" Text='<%# Eval("ResponseSupplierName") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="ATTACHMENT" HeaderStyle-Width="300px">
                                                                                        <ItemTemplate>
                                                                                            <%# Eval("ResponseSupplierAttachment") %>
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
                                    
                                </div>                        
                            </div>
                        </div>
                        
                    </div>
                
                </div>
                
                <div id="tabReceivingEntry" runat="server">
                
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-10px; height:100px; width:1360px;">
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
                                                <asp:DropDownList ID="ddType" runat="server" Width="150px" Height="28px" class="form-control" Enabled="false" Visible="false">
                                                    <asp:ListItem Text="ALL" Value="ALL" />
                                                    <asp:ListItem Text="FOR SENDING" Value="FOR SENDING" />
                                                    <asp:ListItem Text="FOR RESEND" Value="FOR RESEND" />
                                                </asp:DropDownList>
                                                <asp:Label ID="lblType" runat="server" Width="150px" Height="28px" class="form-control" />
                                            </td>
                                            <td><asp:TextBox ID="txtSearchReceiving" runat="server" Width="200px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" onkeypress="return handleEnterReceiving('btnSubmitReceiving',event);" /></td>
                                            <td>
                                                <asp:Button ID="btnSubmitReceiving" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmitReceiving_Click" OnClientClick="setOnLoad();" />
                                            </td>                          
                                          </tr>
                                        </table>
                                    </div>
                                </div>                        
                            </div>
                        </div>
                    </div> 
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; width:1060px;">
                                    
                                    <div id="divLoader" runat="server" style="display:none; width:1060px;">
                                        <p style="font-weight:bold; font-size:14px; color:Black; margin-left:30%;">LOADING DATA... PLEASE WAIT...</p>
                                    </div>
                
                                    <asp:GridView ID="gvDataReceiving" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                             HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                             HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                             OnRowDataBound="gvDataReceiving_OnRowDataBound" OnPageIndexChanging="gvDataReceiving_PageIndexChanging" OnRowCommand="gvDataReceiving_RowCommand"                                                             
                                                                             EmptyDataText="No Record Found!" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="120px" Font-Bold="true" Text='<%# Eval("RhRFQNo") %>'  />                                                                                            
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
                                            <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("RhCategory") %>' Height="15px" Width="180px" Font-Size="11px" />                                              
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="190px" ItemStyle-HorizontalAlign="Left" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("RhRequester") %>' Height="15px" Width="190px"  />                                               
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="HAS RESPONSE" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHasResponse" runat="server" Text='<%# Eval("ResponseNumberOfResponded") %>' Height="15px" Width="100px" />                                              
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="REQ. ATT." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequesterAttachment" runat="server" Text='<%# Eval("RdAttachment") %>' Height="15px" Width="100px" />                                              
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" > 
                                                <ItemTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="swapImage(this); setOnLoad();" />  
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibDisapproved" runat="server" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibPreview" runat="server" ImageUrl="~/images/Preview.png" Width="20px" Height="20px" CommandName="Preview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                            </td>
                                                        </tr>
                                                    </table>                                                                                                                                    
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns> 
                                        <Columns>
                                            <asp:TemplateField HeaderText="REMARKS / CAUSE / NOTE" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Width="165px" Height="16px" Enabled="false" />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                        </Columns>       
                                        
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    
                                                    <tr>
                                                        <td colspan="100%" style="background-color:White;">
                                                            <div id="divDetails" runat="server" style="margin-left:7px;">
                                                                <asp:GridView ID="gvDetailsReceiving" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                                    HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                                    HeaderStyle-BackColor="#009688" HeaderStyle-ForeColor="White" Visible="false"
                                                                                    OnRowDataBound="gvDetailsReceiving_OnRowDataBound" OnRowCommand="gvDetailsReceiving_RowCommand">
                                                                    
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
                                                                                 <asp:Label ID="lblDescription" runat="server" Width="300px" Text='<%# Eval("RdDescription") %>' /> 
                                                                                 <asp:LinkButton ID="linkDescription" runat="server" Width="300px" Visible="false" Text='<%# Eval("RdDescription") %>' CommandName="linkDescription_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad()" /> 
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                          
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SPECS/DRAWING NO." ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                 <asp:Label ID="lblSpecsDrawing" runat="server" Width="210px" Text='<%# Eval("RdSpecs") %>' />
                                                                                 <asp:LinkButton ID="linkSpecsDrawing" runat="server" Width="210px" Visible="false" Text='<%# Eval("RdSpecs") %>' />
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
                                                                        <asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                  <asp:Label ID="lblRemarks" runat="server" Width="250px" Text='<%# Eval("RdRemarks") %>' />
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
                                                                                 <asp:Label ID="lblProdManager" runat="server" Width="145px" Text='<%# Eval("StatProdManager") %>' Font-Bold="true" /> 
                                                                                 <asp:Label ID="lblProdManagerStatus" runat="server" Visible="false" Text='<%# Eval("ProdManagerStatus") %>' /> 
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                          
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="BUYER" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                 <asp:Label ID="lblBuyer" runat="server" Width="145px" Text='<%# Eval("StatBuyer") %>' Font-Bold="true" /> 
                                                                                 <asp:Label ID="lblBuyerStatus" runat="server" Visible="false" Text='<%# Eval("BuyerStatus") %>' /> 
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="INCHARGE" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                 <asp:Label ID="lblIncharge" runat="server" Width="145px" Text='<%# Eval("StatPurchasingIncharge") %>' Font-Bold="true" /> 
                                                                                 <asp:Label ID="lblInchargeStatus" runat="server" Visible="false" Text='<%# Eval("PurchasingInchargeStatus") %>' /> 
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="DEPT. MNGR" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                 <asp:Label ID="lblDepartmentManager" runat="server" Width="145px" Text='<%# Eval("StatDeptManager") %>' Font-Bold="true" /> 
                                                                                 <asp:Label ID="lblDepartmentManagerStatus" runat="server" Visible="false" Text='<%# Eval("DepartmentManagerStatus") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="DIV. MNGR" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                 <asp:Label ID="lblDivisionManager" runat="server" Width="145px" Text='<%# Eval("StatDivManager") %>' Font-Bold="true" /> 
                                                                                 <asp:Label ID="lblDivisionManagerStatus" runat="server" Visible="false" Text='<%# Eval("DivisionManagerStatus") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="LEAD TIME" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                 <asp:Label ID="lblLeadTime" runat="server" Width="246px" Text='<%# Eval("RhLeadTime") %>' /> 
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
                                                                        
                                </div>                        
                            </div>
                        </div>
                    </div> 
                    
                    <div class="row clearfix" id="divActionButtons" runat="server">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; height:65px; width:1075px;">
                                    <asp:Button ID="btnSend" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSend_Click" OnClientClick="setOnLoad();" />
                                </div>
                            </div>
                        </div>
                    </div>   
                
                </div>
                
                <div id="divSendToSupplier" runat="server">
                                        
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                                    <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT RFQ NUMBER YOU WANT TO SEND TO SUPPLIERS</p>
                                    <div style="margin-top:10px;">
                                        <table style="width:900px; color:Gray; font-size:12px;">
                                          <tr>
                                            <th>RFQ NUMBERS</th>
                                            <th>AGING</th>
                                            <th>ENTER ITEM YOU WANT TO SEARCH</th>
                                            <th>&nbsp;</th>
                                          </tr>
                                          <tr>
                                            <td><asp:DropDownList ID="ddRFQNo" runat="server" Height="28px" Width="300px" Font-Size="14px" class="form-control" OnSelectedIndexChanged="ddRFQNo_SelectedIndexChanged" AutoPostBack="true" /></td>     
                                            <td><asp:TextBox ID="txtAging" runat="server" Height="28px" Width="150px" Font-Size="14px" class="form-control" Text="5" /></td>   
                                            <td><asp:TextBox ID="txtSearchSending" runat="server" Width="350px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" onkeypress="return handleEnterSendingQuotation('btnSubmitForSendingQuotation',event);" /></td>                                                             
                                            <td><asp:Button ID="btnSubmitForSendingQuotation" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmitForSendingQuotation_Click" OnClientClick="setOnLoad();" /></td>
                                          </tr>
                                        </table>
                                    </div>
                                </div>                        
                            </div>
                        </div>
                    </div>
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; width:1360px;">
                                    
                                    <asp:GridView ID="gvDataSendToSupplier" runat="server"
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
                                                    <asp:Label ID="lblDescription" runat="server" Width="250px" Text='<%# Eval("RdDescription") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpecsDrawing" runat="server" Width="180px" Text='<%# Eval("RdSpecs") %>' />
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
                                            <asp:TemplateField HeaderText="REMARKS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Width="150px" Height="70px" Text='<%# Eval("RdRemarks") %>' />                                       
                                                </ItemTemplate>                   
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PURCHASING REMARKS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPurchasingRemarks" runat="server" TextMode="MultiLine" Width="148px" Height="70px" Text='<%# Eval("RdPurchasingRemarks") %>' />                                      
                                                </ItemTemplate>                   
                                            </asp:TemplateField>
                                            
                                        </Columns>

                                    </asp:GridView>
                                    
                                    <div style="width:986px; margin-top:5px; text-align:right;">
                                        <asp:Button ID="btnUpdateRemarks" runat="server" Text="UPDATE REMARKS" CssClass="btn bg-light-blue waves-effect" OnClick="btnUpdateRemarks_Click" />
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; width:1360px;">
                                    
                                    <asp:GridView ID="gvSuppliersSendToSupplier" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                             HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px" 
                                                                             HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                             OnRowDataBound="gvSuppliersSendToSupplier_OnRowDataBound" OnRowCommand="gvSuppliersSendToSupplier_RowCommand"                                                             
                                                                             EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplierID" runat="server" Visible="false" Text='<%# Eval("ResponseSupplierID") %>'  />
                                                    <asp:Label ID="lblWithResponse" runat="server" Visible="false" Text='<%# Eval("ReponseWithResponse") %>'  />
                                                    <asp:Label ID="lblResponseCount" runat="server" Visible="false" Text='<%# Eval("ResponseCount") %>'  />
                                                    <asp:Label ID="lblSupplierEmail" runat="server" Visible="false" Text='<%# Eval("ResponseSupplierEmail") %>'  />                                                                                            
                                                    <asp:Label ID="lblSupplierName" runat="server" Height="15px" Width="935px" Text='<%# Eval("ResponseSupplierName") %>'  />                                                                                            
                                                </ItemTemplate> 
                                            </asp:TemplateField>
                                        </Columns>
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" > 
                                                <ItemTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                            </td>                                                    
                                                        </tr>
                                                    </table>                                                                                                                                    
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>                                
                                    </asp:GridView>
                                    
                                    <div id="divSendingDetails" runat="server" style="margin-top:5px;">
                                        <p style="font-size:12px;">SENDING DETAILS</p>
                                        <asp:GridView ID="gvSentDetailsSendToSupplier" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                             HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px" 
                                                                             HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                                                                               
                                                                             EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                            
                                            <Columns>
                                                <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>                                                                                           
                                                        <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="147px" Text='<%# Eval("ResponseRFQNo") %>'  />                                                                                            
                                                    </ItemTemplate> 
                                                </asp:TemplateField>
                                            </Columns>
                                            <Columns>
                                                <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>                                                                                           
                                                        <asp:Label ID="lblSupplierName" runat="server" Height="15px" Width="350px" Text='<%# Eval("ResponseSupplierName") %>'  />                                                                                            
                                                    </ItemTemplate> 
                                                </asp:TemplateField>
                                            </Columns>                                    
                                            
                                            <Columns>
                                                <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" > 
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSendStatus" runat="server" Height="15px" Width="485px" Text='<%# Eval("ResponseSendStatus") %>'  />                                                                                                                                  
                                                    </ItemTemplate>                                
                                                </asp:TemplateField>
                                            </Columns>                                
                                        </asp:GridView>
                                        <div style="margin-top:5px; font-size:11px; width:1060px;">
                                            <%= errorSendingDetails %>
                                        </div>
                                    </div>
                                    
                                </div>                        
                            </div>
                        </div>
                    </div>
                    
                    <div class="row clearfix" id="div1" runat="server">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; height:65px; width:1075px;">
                                    <asp:Button ID="btnSendToSupplier" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSendToSupplier_Click" OnClientClick="setOnLoad();" />
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    
                </div>
                
                <div id="tabForApproval" runat="server">
                
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; height:105px; width:1060px;">
                                    <!-- <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT DATE RANGE YOU WANT TO SEARCH</p> -->
                                    <div style="margin-top:10px;">
                                        <table id="tblApproval" runat="server" style="width:960px; color:Gray; font-size:12px;">
                                          <tr>
                                            <th id="thCategory" runat="server">CATEGORY</th>
                                            <th id="thApprover" runat="server">APPROVER</th>
                                            <th id="thRFQNo" runat="server">ITEM TO SEARCH</th>
                                            <th style="color:White;">DUMMY</th>
                                          </tr>
                                          <tr>                                    
                                            <td><asp:DropDownList ID="ddCategoryForApproval" runat="server" Width="200px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                            <td>
                                                <asp:DropDownList ID="ddApprover" runat="server" Width="300px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" >
                                                    <asp:ListItem Text="FOR BUYER APPROVAL" Value="1" />
                                                    <asp:ListItem Text="FOR INCHARGE APPROVAL" Value="2" />
                                                    <asp:ListItem Text="FOR DEPARTMENT MANAGER APPROVAL" Value="3" />
                                                    <asp:ListItem Text="FOR DIVISION MANAGER APPROVAL" Value="4" />
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRFQNo" runat="server" Width="370px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" onkeypress="return handleEnterApproval('btnSubmitForApproval',event);" />
                                            </td>    
                                            <td>
                                                <asp:Button ID="btnSubmitForApproval" runat="server" Text="SEARCH" CssClass="btn bg-green waves-effect" OnClick="btnSubmitForApproval_Click" OnClientClick="setOnLoad();" />
                                            </td>                          
                                          </tr>
                                        </table>
                                    </div>
                                </div>                        
                            </div>
                        </div>
                    </div>
                    
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; width:1360px;">
                                    
                                    <asp:GridView ID="gvDataForApproval" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" Visible="false"
                                                                             HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                             HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                             OnRowDataBound="gvDataForApproval_OnRowDataBound" OnPageIndexChanging="gvDataForApproval_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                             EmptyDataText="No Record Found!">
                                        <Columns>
                                            <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="118px" Font-Bold="true" Text='<%# Eval("Rfqno") %>'  />    
                                                    <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Visible="false" />  
                                                    <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate") %>' Visible="false" />                                                                                       
                                                </ItemTemplate> 
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' Height="15px" Width="198px" Font-Size="11px" />                                              
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="420px" ItemStyle-HorizontalAlign="Left" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("RhRequester") %>' Height="15px" Width="418px"  />                                               
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" > 
                                                <ItemTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();"  />  
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibDisapproved" runat="server" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();"  />  
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibPreview" runat="server" ImageUrl="~/images/Preview.png" Width="20px" Height="20px" CommandName="Preview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();"  />  
                                                            </td>
                                                        </tr>
                                                    </table>                                                                                                                                    
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns> 
                                        <Columns>
                                            <asp:TemplateField HeaderText="REMARKS / CAUSE / NOTE" HeaderStyle-Width="170px" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Width="165px" Height="16px" Enabled="false" />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                        </Columns>                                                               
                                        
                                    </asp:GridView>
                                   
                                   <asp:GridView ID="gvPurchasingForApproval" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" Visible="false"
                                                                             HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                             HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                             OnRowDataBound="gvPurchasingForApproval_OnRowDataBound" OnRowCommand="gvPurchasingForApproval_RowCommand"                                                             
                                                                             EmptyDataText="No Record Found!" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="118px" Font-Bold="true" Text='<%# Eval("Rfqno") %>'  />    
                                                    <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Visible="false" />  
                                                    <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate") %>' Visible="false" />                                                                                       
                                                </ItemTemplate> 
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' Height="15px" Width="198px" Font-Size="11px" />                                              
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("RhRequester") %>' Height="15px" Width="298px"  />                                               
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="NO. OF RESPONSES" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplierResponse" runat="server" Text='<%# Eval("NumberOfSuppliers_WithResponse") %>' Height="15px" Width="118px"  />                                               
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" > 
                                                <ItemTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();" />  
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibDisapproved" runat="server" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();" />  
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibPreview" runat="server" ImageUrl="~/images/Preview.png" Width="20px" Height="20px" CommandName="Preview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();" />  
                                                            </td>
                                                        </tr>
                                                    </table>                                                                                                                                    
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns> 
                                        <Columns>
                                            <asp:TemplateField HeaderText="REMARKS / CAUSE / NOTE" HeaderStyle-Width="170px" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Width="165px" Height="16px" Enabled="false" />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                        </Columns>    
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    
                                                    <tr>
                                                        <td colspan="100%" style="background-color:White;">
                                                            <div style="margin-left:7px;">
                                                            <asp:GridView ID="gvResponseForApproval" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                                HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                                HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" Visible="false"
                                                                                OnRowDataBound="gvResponseForApproval_OnRowDataBound" OnRowCommand="gvResponseForApproval_RowCommand">
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
                                                                            <asp:Label ID="lblResponseDescription" runat="server" Text='<%# Eval("ResponseDescription") %>' Width="250px" Height="20px" Font-Size="11px"/>                                                                        
                                                                            <asp:Label ID="lblResponseSpecs" runat="server" Text='<%# Eval("ResponseSpecs") %>' Visible="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIsGranted" runat="server" Visible="false" Text='<%# Eval("ResponseIsGranted") %>' />
                                                                            <asp:Label ID="lblSupplierId" runat="server" Visible="false" Text='<%# Eval("ResponseSupplierID") %>' />
                                                                            <asp:ImageButton ID="ibApprovedResponse" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="AResponse_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad();"  />
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
                                                                            <asp:TextBox ID="txtResponsePurchasingRemarks" runat="server" Text='<%# Eval("ResponsePurchasingRemarks") %>' Height="20px" TextMode="MultiLine" Width="170px" Font-Size="11px" />
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
                                    <br />
                                    <asp:GridView ID="gvRelatedSearchForApproval" runat="server" Visible="false"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  OnRowCommand="gvRelatedSearchForApproval_RowCommand" OnRowDataBound="gvRelatedSearchForApproval_OnRowDataBound"
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
                    
                    <div class="row clearfix" id="div2" runat="server">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card">
                                <div class="body" style="margin-top:-23px; height:65px; width:1075px;">
                                    <asp:Button ID="btnSendForApproval" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSendForApproval_Click" OnClientClick="setOnLoad();" />
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    
                    
                
                </div>
                
                <div id="tabSuccess" runat="server">
                
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1360px; margin-left:10px;">
                            <div class="card"> 
                                <div class="body" style="margin-top:-23px; height:500px; width:1360px;">
                                    <table style="width:100%; color:Gray;">
                                      <tr>
                                        <td style="font-weight:bold; width:400px;">MESSAGE</td>
                                        <td style="font-weight:bold;">TRANSACTION NAME</td> 
                                      </tr>
                                      <tr>
                                        <td style="font-size:12px;"><asp:Label ID="lblMessage" runat="server" /></td>
                                        <td style="font-size:12px;"><asp:Label ID="lblTransactionName" runat="server" /></td> 
                                      </tr>
                                    </table>
                                    <div style="margin-top:15px; width:100px;">
                                        <asp:Button ID="btnSubmitSuccess" runat="server" Text="OK" CssClass="btn btn-block bg-pink waves-effect"
                                            onclick="btnSubmitSuccess_Click" OnClientClick="setOnLoad();" />
                                    </div> 
                                </div>                        
                            </div>
                        </div>
                    </div>
                
                </div>
                
                
                
                
            
            </div>
        
        </div>
        
        
        
    
    </div>
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
    
    <script src="plugins/jquery/jquery.min.js" type="text/javascript"></script>
    <script src="plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="plugins/jquery-slimscroll/jquery.slimscroll.js" type="text/javascript"></script>
    <script src="plugins/node-waves/waves.js" type="text/javascript"></script>
    <script src="plugins/multi-select/js/jquery.multi-select.js" type="text/javascript"></script>    
    <script src="plugins/jquery-countto/jquery.countTo.js" type="text/javascript"></script>
    <script src="plugins/raphael/raphael.min.js" type="text/javascript"></script>
    <script src="plugins/morrisjs/morris.js" type="text/javascript"></script>
    <script src="plugins/chartjs/Chart.bundle.js" type="text/javascript"></script>
    <script src="plugins/jquery-sparkline/jquery.sparkline.js" type="text/javascript"></script>
    <script src="js/admin.js" type="text/javascript"></script>
    <script src="js/demo.js" type="text/javascript"></script>    
    <script src="js/bootstrap-datepicker.js" type="text/javascript"></script>
    
    </form>
</body>
</html>
