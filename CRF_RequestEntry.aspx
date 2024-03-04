<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRF_RequestEntry.aspx.cs" Inherits="REPI_PUR_SOFRA.CRF_RequestEntry" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
        
        function WarningMessageCutOff(msg) {
            swal({
                title: 'CUT-OFF TIME NOTIFICATION!',
                text: msg,
                type: 'warning'
            });
        }    
        
        function isNumberKey(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

             return true;
        }
        
    </script>
    
    <script type="text/javascript">    
  
        $(function () {
            $("[id*=txtPODate]").datepicker({
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
            $("[id*=txtDISupplier]").datepicker({
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
            $("[id*=txtDIRequester]").datepicker({
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
            $("[id*=txtReceivedDate]").datepicker({
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
            $("[id*=txtDateInformedSupplier]").datepicker({
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
            $("[id*=txtDateInformedRequestor]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
    
    

    <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="content">
            
            <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1280px;">
                
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                        <div class="card"> 
                            <div class="header" style="height:30px;">
                                <p style="color:Gray; font-size:14px; font-weight:bold;">CANCEL REQUEST <%= displayCTRLNo %></p>
                            </div>                        
                        </div>
                    </div>
                </div>
                
                <div class="row clearfix" id="divApprover" runat="server" style="display: none;">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                        <div class="card"> 
                            <div class="body" style="margin-top:-23px; min-height:210px; width:1280px;">
                                <div style="margin-top:10px;">
                                    <table style="width:1250px; color:Gray; font-size:12px;">
                                      <tr>
                                        <th>REQUESTER</th>
                                        <th>PROD. MNGR.</th>
                                        <th>BUYER</th>
                                        <th>PUR. MANAGER</th>
                                      </tr>
                                      <tr>                                    
                                        <td>
                                            <div style="width:250px; background-color:White;">
                                                <div id="divRequester" runat="server" style="width:250px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblRequester" runat="server" Height="22px" Width="250px" ForeColor="White" /><br />
                                                    <asp:Label ID="lblRequesterDOA" runat="server" Height="22px" Width="250px" ForeColor="White" />
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width:250px; background-color:White;">
                                                <div id="divProdManager" runat="server" style="width:250px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblProdManager" runat="server" Height="22px" Width="250px" ForeColor="White" /><br />
                                                    <asp:Label ID="lblProdManagerDOA" runat="server" Height="22px" Width="250px" ForeColor="White" />
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width:250px; background-color:White;">
                                                <div id="divBuyer" runat="server" style="width:250px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblBuyer" runat="server" Height="22px" Width="250px" ForeColor="White" /><br />
                                                    <asp:Label ID="lblBuyerDOA" runat="server" Height="22px" Width="250px" ForeColor="White" />
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width:250px; background-color:White;">
                                                <div id="divPurManager" runat="server" style="width:250px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblPurManager" runat="server" Height="22px" Width="250px" ForeColor="White" /><br />
                                                    <asp:Label ID="lblPurManagerDOA" runat="server" Height="22px" Width="250px" ForeColor="White" />
                                                </div>
                                            </div>
                                        </td>
                                      </tr>
                                    </table>
                                    <table style="width:1250px; color:Gray; font-size:12px;">
                                        <tr>
                                            <th>BUYER SEND</th>
                                            <th>SUPPLIER</th>
                                            <th>ITEM STATUS</th>
                                            <th>SEND DATES</th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="width:250px; background-color:White;">
                                                    <div id="divBuyerSend" runat="server" style="width:250px; border: 1px solid black; padding-left:5px;">
                                                        <asp:Label ID="lblBuyerSend" runat="server" Height="22px" Width="250px" ForeColor="White" /><br />
                                                        <asp:Label ID="lblBuyerSendDOA" runat="server" Height="22px" Width="250px" ForeColor="White" />
                                                    </div>
                                                </div>
                                             </td>   
                                             <td>
                                                <div style="width:250px; background-color:White;">
                                                    <div id="divSupplier" runat="server" style="width:250px; border: 1px solid black; padding-left:5px;">
                                                        <asp:Label ID="lblSupplier" runat="server" Height="22px" Width="250px" ForeColor="White" /><br />
                                                        <asp:Label ID="lblSupplierDOA" runat="server" Height="22px" Width="250px" ForeColor="White" />
                                                    </div>
                                                </div>
                                             </td> 
                                             <td>
                                                <div style="width:250px; background-color:White;">
                                                    <div id="divItemStatus" runat="server" style="width:250px; border: 1px solid black; padding-left:5px;">
                                                        <asp:Label ID="lblItemStatus" runat="server" Height="22px" Width="250px" ForeColor="White" /><br />
                                                        <asp:Label ID="lblItemStatusDOA" runat="server" Height="22px" Width="250px" ForeColor="White" />
                                                    </div>
                                                </div>
                                             </td>
                                              <td>
                                                <div style="width:250px; background-color:White;">
                                                    <asp:DropDownList ID="ddSendDates" runat="server" Height="22px" Width="250px" />
                                                </div>
                                             </td>
                                        </tr>
                                    </table>
                                    <table style="width:600px; color:Gray; font-size:12px; display:none; margin-top:5px;" id="tableRemarks" runat="server">
                                        <tr>
                                            <th colspan="4">REASON OF DISAPPROVAL</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                        </tr>
                                        <tr>  
                                            <td>
                                                <asp:TextBox ID="txtRemarks" runat="server" Height="70px" Width="995px" TextMode="MultiLine" />
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
                            <div class="body" style="margin-top:-23px; min-height:130px; width:1280px;">
                                <table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th colspan="3">SUPPLIER</th>
                                    <th>ATTENTION</th>                                     
                                  </tr>
                                  <tr>
                                    <td colspan="3"><asp:DropDownList ID="ddSupplier" runat="server" Width="760px" /></td>
                                    <td><asp:TextBox ID="txtAttention" runat="server" Width="380px" Height="22px" /></td>                              
                                  </tr>
                                </table>
                                <table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th>CATEGORY</th>
                                    <th>REQUESTER NAME</th>
                                    <th>REQUESTER EMAIL</th> 
                                    <th>REQUESTER LOCAL NO.</th>                                     
                                  </tr>
                                  <tr>
                                    <td><asp:DropDownList ID="ddCategory" runat="server" Width="275px" Height="22px" /></td>
                                    <td><asp:TextBox ID="txtRequesterName" runat="server" Width="275px" Height="22px" /></td>
                                    <td><asp:TextBox ID="txtRequesterEmail" runat="server" Width="275px" Height="22px" /></td>
                                    <td><asp:TextBox ID="txtRequesterLocalNumber" runat="server" Width="275px" Height="22px" /></td>                                 
                                  </tr>
                                </table>
                                <table id="tableCloseRemarks" runat="server" style="width:1213px; color:Gray; display:none;">
                                  <tr style="color:Gray;">
                                    <th colspan="4">BUYER CLOSE REMARKS</th>                                  
                                  </tr>
                                  <tr>
                                    <td colspan="4"><asp:TextBox ID="txtBuyerCloseRemarks" runat="server" Width="1213px" Height="100px" TextMode="MultiLine" /></td>                               
                                  </tr>
                                </table>
                                <table id="tableSupplierAttachment" runat="server" style="width:1213px; color:Gray; display:none;">
                                  <tr style="color:Gray;">
                                    <th colspan="4">SUPPLIER ATTACHMENT</th>                                  
                                  </tr>
                                  <tr>
                                    <td colspan="4"><asp:LinkButton ID="lbSupplierAttachment" runat="server" OnClick="lbSupplierAttachment_Click" /></td>                               
                                  </tr>
                                </table>
                                
                              <table id="tableTransferDetails" runat="server" style="width:100%; color:Gray; margin-top:10px; display:none;">
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
                                <%--<table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th colspan="2">PO No.</th>
                                    <th colspan="2">PR No.</th>                                     
                                  </tr>
                                  <tr>
                                    <td colspan="2"><asp:TextBox ID="txtPONO" runat="server" Width="561px" Height="22px" /><asp:Button ID="btnPONO" runat="server" Width="25px" Height="23px" Text="..." OnClick="btnPONO_Click" /></td>
                                    <td colspan="2"><asp:TextBox ID="txtPRNO" runat="server" Width="561px" Height="22px" /><asp:Button ID="btnPRNO" runat="server" Width="25px" Height="23px" Text="..." OnClick="btnPRNO_Click" /></td>                             
                                  </tr>
                                </table>--%>
                                <%--<table id="tableDataLinkToPR" runat="server" style="width:980px; color:Gray; display:none; min-height:200px;">
                                    <tr>
                                        <th colspan="4">DATA LINK TO PR</th> 
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvDataLinkToPR" runat="server" AutoGenerateColumns="false" 
                                                 RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" 
                                                 RowStyle-Height="15px" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"
                                                 OnRowCommand="gvDataLinkToPR_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="PO.NO.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLinkPONO" runat="server" Text='<%# Eval("PONO") %>' Width="300px" />  
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' Visible="false" />  
                                                                <asp:Label ID="lblTypeDrawingNo" runat="server" Text='<%# Eval("TypeDrawingNo") %>' Visible="false" />  
                                                                <asp:Label ID="lblOrderQuantity" runat="server" Text='<%# Eval("OrderQuantity") %>' Visible="false" />  
                                                                <asp:Label ID="lblUnitOfMeasure" runat="server" Text='<%# Eval("UnitOfMeasure") %>' Visible="false" />   
                                                                <asp:Label ID="lblPODATE" runat="server" Text='<%# Eval("PoDate") %>' Visible="false" />                                                                        
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PR.NO.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLinkPRNO" runat="server" Text='<%# Eval("PRNO") %>' Width="300px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        <asp:TemplateField HeaderText="ACTION">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnLinkSelectPR" runat="server" Text="SELECT" Width="200px" CommandName="btnLinkSelectPR_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                    </Columns>
                                                </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" OnClick="btnCancel_Click" />                                        
                                        </td>
                                    </tr>
                                </table>--%>
                                <%--<table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th>PO DATE</th> 
                                    <th colspan="3">CATEGORY</th>                                     
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtPODate" runat="server" Width="380px" Height="22px" /></td>
                                    <td colspan="3"><asp:DropDownList ID="ddCategory" runat="server" Width="815px" Height="22px" /></td>                               
                                  </tr>
                                </table>
                                <table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th colspan="4">DESCRIPTION</th>                                     
                                  </tr>
                                  <tr>
                                    <td colspan="4"><asp:TextBox ID="txtDescription" runat="server" Width="1212px" Height="22px" /></td>                               
                                  </tr>
                                </table>
                                <table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th colspan="4">TYPE / DRAWING NO.</th>                                     
                                  </tr>
                                  <tr>
                                    <td colspan="4"><asp:TextBox ID="txtTypeDrawing" runat="server" Width="1212px" Height="22px" /></td>                               
                                  </tr>
                                </table>
                                <table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th>ORDER QUANTITY</th>
                                    <th colspan="3">REASON</th>                                     
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtOrderQuantity" runat="server" Width="340px" Height="22px" onkeypress="return isNumberKey(event)" /></td>
                                    <td colspan="2"><asp:DropDownList ID="ddReason" runat="server" Width="857px" Height="22px" /></td>                               
                                  </tr>
                                </table>
                                <table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th>DATE INFORMED (SUPPLIER)</th> 
                                    <th colspan="3">DATE INFORMED (REQUESTOR)</th>                                     
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtDateInformedSupplier" runat="server" Width="587px" Height="22px" /></td>
                                    <td><asp:TextBox ID="txtDateInformedRequestor" runat="server" Width="587px" Height="22px" /></td>                            
                                  </tr>
                                </table>
                                <table id="tableSupplierResponseConfirmedBy" runat="server" style="width:1212px; color:Gray; display:none;">
                                  <tr style="color:Red;">
                                    <th colspan="4">SUPPLIER RESPONSE (CONFIRMED BY)</th>                                  
                                  </tr>
                                  <tr>
                                    <td colspan="4"><asp:TextBox ID="txtResponseConfirmedBy" runat="server" Width="1212px" Height="22px" /></td>                               
                                  </tr>
                                </table>
                                <table id="tableSupplierResponseDateConfirmed" runat="server" style="width:1212px; color:Gray; display:none;">
                                  <tr style="color:Red;">
                                    <th colspan="4">SUPPLIER RESPONSE (DATE CONFIRMED)</th>                                  
                                  </tr>
                                  <tr>
                                    <td colspan="4"><asp:TextBox ID="txtResponseDateConfirmed" runat="server" Width="1212px" Height="22px" /></td>                               
                                  </tr>
                                </table>
                                <table id="tableSupplierResponseNotes" runat="server" style="width:1212px; color:Gray; display:none;">
                                  <tr style="color:Red;">
                                    <th colspan="4">SUPPLIER RESPONSE (NOTES)</th>                                  
                                  </tr>
                                  <tr>
                                    <td colspan="4"><asp:TextBox ID="txtResponseNotes" runat="server" Width="1212px" Height="100px" TextMode="MultiLine" /></td>                               
                                  </tr>
                                </table>
                                <table id="tableSupplierResponseAttachment" runat="server" style="width:1212px; color:Gray; display:none;">
                                  <tr style="color:Red;">
                                    <th colspan="4">SUPPLIER ATTACHMENT</th>                                  
                                  </tr>
                                  <tr>
                                    <td><asp:DropDownList ID="ddSupplierAttachment" runat="server" Height="30px" Width="1050px" /><asp:Button ID="btnSupplierAttachment" runat="server" Text="DOWNLOAD" CssClass="btn bg-cyan waves-effect" OnClick="btnSupplierAttachment_Click" Width="160px" PostBackUrl="~/CRF_RequestEntry.aspx" /> </td>                                    
                                  </tr>
                                </table>
                                <table id="tableAttachment" runat="server" style="width:1250px; color:Gray; display:none;">
                                    <tr>
                                       <th>ATTACHMENT 1</th>
                                       <th>ATTACHMENT 2</th>   
                                       <th>ATTACHMENT 3</th> 
                                       <th>ATTACHMENT 4</th>                                  
                                    </tr>
                                    <tr>
                                        <td><asp:FileUpload ID="fu1" runat="server" Width="302px" EnableViewState="true"  /></td>
                                        <td><asp:FileUpload ID="fu2" runat="server" Width="302px" EnableViewState="true"  /></td>
                                        <td><asp:FileUpload ID="fu3" runat="server" Width="302px" EnableViewState="true"  /></td>
                                        <td><asp:FileUpload ID="fu4" runat="server" Width="302px" EnableViewState="true"  /></td>
                                    </tr>
                                </table>--%>
                                <!--
                                <table style="width:980px; color:Gray;">
                                    <tr>
                                       <th>ATTACHMENT 5</th>
                                       <th>ATTACHMENT 6</th>   
                                       <th>ATTACHMENT 7</th> 
                                       <th>ATTACHMENT 8</th>                                  
                                    </tr>
                                    <tr>
                                        <td><asp:FileUpload ID="fu5" runat="server" Width="240px" EnableViewState="true"  /></td>
                                        <td><asp:FileUpload ID="fu6" runat="server" Width="240px" EnableViewState="true"  /></td>
                                        <td><asp:FileUpload ID="fu7" runat="server" Width="240px" EnableViewState="true"  /></td>
                                        <td><asp:FileUpload ID="fu8" runat="server" Width="240px" EnableViewState="true"  /></td>
                                    </tr>
                                </table>
                                -->
                                
                            </div>                        
                        </div>
                    </div>
                </div>
                
                 <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                        <div class="card">                                
                            <div class="body" style="margin-top:-23px; width:1280px;">
                            
                                <asp:GridView ID="gvData" runat="server" ShowFooter="True"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px"
                                                  OnRowDataBound="gvData_OnRowDataBound"
                                                  OnRowDeleting="gvData_RowDeleting"
                                                  DataKeyNames="RowNumber" OnRowCommand="gvData_RowCommand">
                                        <Columns>
                                        <asp:BoundField DataField="RowNumber" HeaderText="NO." />
                                        <asp:TemplateField HeaderText="PO.NO.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPONO" runat="server" Width="90px" Text='<%# Eval("RdPONO") %>' data-toggle="tooltip" data-placement="top" title='<%# Eval("RdPONO") %>'></asp:TextBox>
                                                <asp:Button ID="btnPONO" runat="server" Width="10px" Height="20px" Text="..." CommandName="btnPONO_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RdRefId") %>' Visible="false" />                                                
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PR.NO.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPRNO" runat="server" Text='<%# Eval("RdPRNO") %>' Width="90px" data-toggle="tooltip" data-placement="top" title='<%# Eval("RdPRNO") %>' ></asp:TextBox>
                                                <asp:Button ID="btnPRNO" runat="server" Width="10px" Height="20px" Text="..." CommandName="btnPRNO_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemName" runat="server" Text='<%# Eval("RdItemName") %>' Width="120px" data-toggle="tooltip" data-placement="top" title='<%# Eval("RdItemName") %>' ></asp:TextBox>
                                                <asp:Button ID="btnItemName" runat="server" Width="10px" Height="20px" Text="..." CommandName="btnItemName_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TYPE / DRAWING NO.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSpecification" runat="server" Text='<%# Eval("RdSpecs") %>' Width="120px" data-toggle="tooltip" data-placement="top" title='<%# Eval("RdSpecs") %>' ></asp:TextBox>
                                                <asp:Button ID="btnSpecification" runat="server" Width="10px" Height="20px" Text="..." CommandName="btnSpecification_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("RdQuantity") %>' Width="30px" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddUOM" runat="server" Width="40px" >                                                
                                                </asp:DropDownList>
                                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("RdUnitOfMeasure") %>' Visible="false" />
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO DATE">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPODate" runat="server" Text='<%# Eval("RdPODate") %>' Width="100px" ></asp:TextBox>
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REASON">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddReason" runat="server" Width="100px" />
                                                <asp:Label ID="lblReason" runat="server" Text='<%# Eval("RdReason") %>' Visible="false" />
                                            </ItemTemplate>                    
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="DI SUPPLIER">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDISupplier" runat="server" Text='<%# Eval("RdDateInformedSupplier") %>' Width="90px" ></asp:TextBox>
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DI REQUESTER">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDIRequester" runat="server" Text='<%# Eval("RdDateInformedRequester") %>' Width="90px" ></asp:TextBox>
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ATT.">
                                            <ItemTemplate>                                             
                                                <asp:FileUpload ID="fuAttachment" runat="server" Width="113px" EnableViewState="true" accept=".pdf" />
                                                <asp:LinkButton ID="btnUpload" runat="server" Text="..." CommandName="btnUpload_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Visible="false" />                        
                                            </ItemTemplate>
                                         </asp:TemplateField>                                                                                                    
                                        <asp:TemplateField HeaderText="ACTION">
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Button ID="ButtonAdd" runat="server" CssClass="btn btn-block bg-blue waves-effect" Font-Size="11px" Height="22px" Text="ADD" OnClick="ButtonAdd_Click" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-Height="22px" ControlStyle-CssClass="btn btn-block bg-green waves-effect" DeleteText="DEL" ControlStyle-Font-Size="11px" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="background-color:White;">
                                                        <div id="divDataLinkToPR" runat="server" style="margin-top:3px; display:none;">                                                                                                                       
                                                            <asp:GridView ID="gvDataLinkToPR" runat="server" AutoGenerateColumns="false" 
                                                             RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" 
                                                             OnRowCommand="gvDataLinkToPR_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="PO.NO.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkPONO" runat="server" Text='<%# Eval("RdPONO") %>' Width="140px" />  
                                                                            <asp:Label ID="lblDataIndex" runat="server" Visible="false" Text='<%# Eval("DataIndex") %>' />                                                                           
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PR.NO.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkPRNO" runat="server" Text='<%# Eval("RdPRNO") %>' Width="140px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ITEM NAME">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkItemName" runat="server" Text='<%# Eval("RdItemName") %>' Width="250px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SPECIFICATION">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkSpecs" runat="server" Text='<%# Eval("RdSpecs") %>' Width="250px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="QTY.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkQuantity" runat="server" Text='<%# Eval("RdQuantity") %>' Width="100px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UOM">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkUnitOfMeasure" runat="server" Text='<%# Eval("RdUnitOfMeasure") %>' Width="80px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                    
                                                                    <asp:TemplateField HeaderText="PO DATE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkPODate" runat="server" Text='<%# Eval("RdPODate") %>' Width="140px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ACTION">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnLinkSelectPR" runat="server" Text="SELECT" CommandName="btnLinkSelectPR_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                
                <div class="row clearfix" id="divAttachment" runat="server" style="display:none;">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                        <div class="card">
                            <div class="body" style="margin-top:-23px; min-height:0px; width:1280px;">
                                <%= displayAttachment.ToString() %>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="row clearfix" id="divActionButtons" runat="server">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                        <div class="card">
                            <div class="body" style="margin-top:-23px; height:65px; width:1280px;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn bg-light-blue waves-effect" OnClick="btnSubmit_Click" Visible="false" />
                                        </td>
                                        <td>
                                            <p runat="server" id="pRequired" style="display:none; color:Red; margin-left:10px;">**All items in red are required fields.</p>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            
                
            </div>


        </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

