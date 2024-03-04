<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_RequestEntry.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_RequestEntry" MasterPageFile="~/Sofra.Master" %>

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
        
    </script>
    
    <script type="text/javascript">    
  
        $(function () {
            $("[id*=txtServiceDate]").datepicker({
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
            $("[id*=txtDeliveryDateToRepi]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
   
   <script type="text/javascript">
    
    function validateLOA(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        var loapricevalue = selectedText.substring(selectedText.indexOf("(") + 1,selectedText.lastIndexOf(")"))
        var totalquantity = document.getElementById('<%= txtTotalQuantity.ClientID %>').value;
            document.getElementById('<%= txtTotalValueInUsd.ClientID %>').value = totalquantity * loapricevalue;
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
                            <p id="SRF_CTRLNo" runat="server" style="color:Gray; font-size:14px; font-weight:bold;">SERVICE REPAIR REQUEST ENTRY</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divApprover" runat="server" visible="false">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:85px; width:1280px;">
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
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblRequestor" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOARequestor" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td> 
                                <td>
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblManager" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAManager" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblIncharge" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAIncharge" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblSCDDeptManager" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOASCDDeptManager" runat="server" Text="-" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table style="width:250px; color:Gray;">
                                        <tr>
                                            <th style="font-size:15px;"><asp:Label ID="lblImpex" runat="server" Height="18px" Width="188px" /></th>
                                        </tr>
                                        <tr>
                                            <td style="font-size:15px;"><asp:Label ID="lblDOAImpex" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>  
                                                             
                              </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:380px; width:1280px;">
                            <table style="width:100%; color:Gray; margin-bottom:10px;">
                              <tr>
                                <th>SUPPLIER</th>                                
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddSupplier" runat="server" Width="1180px" required /></td>
                              </tr>                              
                            </table>
                            <table style="width:100%; color:Gray;">
                              <tr>                                
                                <th>REQUESTER</th> 
                                <th>LOCAL NUMBER</th>
                                <th>DIVISION</th> 
                                <th>DEPARTMENT</th>                                 
                              </tr>
                              <tr>                                                               
                                <td><asp:TextBox ID="txtRequester" runat="server" Width="250px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtLocalNumber" runat="server" Width="250px" Height="22px" /></td> 
                                <td><asp:TextBox ID="txtDivisionName" runat="server" Width="250px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtDepartment" runat="server" Width="250px" Height="22px" /></td>                                 
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>DELIVERY DATE TO REPI</th>
                                <th>PROBLEM ENCOUNTERED</th>
                                <th>PURPOSE OF PULL OUT</th> 
                                <th>TOTAL VALUE (USD)</th>                                
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtDeliveryDateToRepi" runat="server" Width="250px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtProblemEncountered" runat="server" Width="250px" Height="22px" /></td>
                                <td><asp:DropDownList ID="ddPurposeOfPullOut" runat="server" Width="250px" required /></td>
                                <td><asp:TextBox ID="txtTotalValueInUsd" runat="server" Width="250px" Height="22px" Enabled="false" /></td>                                
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>LOA NO.</th>
                                <th>SURETY BOND NO.</th>
                                <th>LOA 8106 NO.</th> 
                                <th>CATEGORY</th>                                
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddLoaNo" runat="server" Width="250px" Enabled="false" onchange="validateLOA(this);" /></td>
                                <td><asp:TextBox ID="txtSuretyBond" runat="server" Width="250px" Enabled="false" Height="22px" /></td>   
                                <td><asp:TextBox ID="txtLoa8106" runat="server" Width="250px" Enabled="false" Height="22px" /></td>
                                <td><asp:DropDownList ID="ddCategory" runat="server" Width="250px" required /></td>                          
                              </tr>
                            </table>     
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>PICKUP POINT</th> 
                                <th>GATE PASS NO.</th>
                                <th>TOTAL QUANTITY</th>
                                <th>PULLOUT DATE</th>                                
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtPickUpPoint" runat="server" Width="250px" Height="22px" required /></td>
                                <td><asp:TextBox ID="txtGatePassNo" runat="server" Width="250px" Height="22px" Enabled="false" /></td>
                                <td><asp:TextBox ID="txtTotalQuantity" runat="server" Width="250px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtServiceDate" runat="server" Width="250px" Height="22px" required /></td> 
                              </tr>
                            </table>                                                                           
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>REMARKS</th> 
                                <th>ATTACHMENT 1</th>
                                <th>ATTACHMENT 2</th>
                                <th>ATTACHMENT 3</th>                                
                              </tr>
                              <tr>   
                                                                                             
                                <td><asp:TextBox ID="txtRemarks" runat="server" Width="250px" Height="22px" /></td>
                                <td><asp:FileUpload ID="fu1" runat="server" Width="250px" EnableViewState="true" accept=".pdf" /><asp:LinkButton ID="linkAttachment1" runat="server" OnClick="linkAttachment1_Click" /></td>   
                                <td><asp:FileUpload ID="fu2" runat="server" Width="250px" EnableViewState="true" accept=".pdf" /><asp:LinkButton ID="linkAttachment2" runat="server" OnClick="linkAttachment2_Click"  /></td>   
                                <td><asp:FileUpload ID="fu3" runat="server" Width="250px" EnableViewState="true" accept=".pdf" /><asp:LinkButton ID="linkAttachment3" runat="server" OnClick="linkAttachment3_Click"  /></td>                               
                              </tr>
                            </table>
                            
                            <table style="width:100%; color:Gray; margin-top:10px;">
                              <tr>
                                <td><asp:CheckBox ID="chkNoNeed8105" runat="server" Text="CHECK IF 8105 DOCUMENTS IS NOT NEEDED" /> </td>
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
                            
                            
                        </div>                                                     
                                               
                    </div>                                         
                    
                </div>
            </div>         
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:200px; width:1075px;">
                        
                        <asp:GridView ID="gvData" runat="server" ShowFooter="True"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px"
                                                  OnRowDataBound="gvData_OnRowDataBound"
                                                  OnRowDeleting="gvData_RowDeleting"
                                                  DataKeyNames="RowNumber" OnRowCommand="gvData_RowCommand">
                                    <Columns>
                                    <asp:BoundField DataField="RowNumber" HeaderText="NO." />                                    
                                    <asp:TemplateField HeaderText="REF PR/PO/RFQ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRefPRPO" runat="server" Width="100px" Height="22px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SALES INVOICE">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSalesInvoice" runat="server" Width="100px" Height="22px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BRAND / MACHINE">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBrandMachine" runat="server" Width="210px" Height="22px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ITEM NAME">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemName" runat="server" Width="210px" Height="22px" required ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECIFICATION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSpecification" runat="server" Width="210px" Height="22px" required ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QUANTITY">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" Width="70px" Height="22px" required ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUOM" runat="server" Width="60px" Height="22px" Font-Size="11px" required >                                                
                                            </asp:DropDownList>
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SERIAL NO">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSerialNo" runat="server" Width="100px" Height="22px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ACTION">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="ButtonAdd" runat="server" CssClass="btn btn-block bg-blue waves-effect" Font-Size="11px" Height="22px" Text="ADD" OnClick="ButtonAdd_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-Height="22px" ControlStyle-CssClass="btn btn-block bg-green waves-effect" DeleteText="DELETE" ControlStyle-Font-Size="11px" />
                                    
                                </Columns>

                            </asp:GridView> 
                            
                            
                            <asp:GridView ID="gvDataUpdate" runat="server" ShowFooter="True" Visible="false"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px"
                                                  OnRowDataBound="gvDataUpdate_OnRowDataBound">
                                    <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefId" runat="server" Width="40px" Height="22px" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField HeaderText="REF PR/PO/RFQ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRefPRPO" runat="server" Width="90px" Height="22px" Text='<%# Eval("RefPRPO") %>' data-toggle="tooltip" data-placement="top" title='<%# Eval("RefPRPO") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SALES INVOICE">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSalesInvoice" runat="server" Width="100px" Height="22px" Text='<%# Eval("SalesInvoice") %>' data-toggle="tooltip" data-placement="top" title='<%# Eval("SalesInvoice") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BRAND / MACHINE">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBrandMachine" runat="server" Width="210px" Height="22px" Text='<%# Eval("BrandMachineName") %>' data-toggle="tooltip" data-placement="top" title='<%# Eval("BrandMachineName") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ITEM NAME">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemName" runat="server" Width="210px" Height="22px" Text='<%# Eval("ItemName") %>' data-toggle="tooltip" data-placement="top" title='<%# Eval("ItemName") %>' required ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECIFICATION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSpecification" runat="server" Width="210px" Height="22px" Text='<%# Eval("Specification") %>' data-toggle="tooltip" data-placement="top" title='<%# Eval("Specification") %>'  required ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QUANTITY">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" Width="70px" Height="22px" Text='<%# Eval("TotalQuantity") %>' required ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUOM" runat="server" Width="60px" Height="22px" Font-Size="11px" required >                                                                                         
                                            </asp:DropDownList>
                                            <asp:Label ID="lblUpdatedUOM" runat="server" Visible="false" Text='<%# Eval("UnitOfMeasure") %>' ></asp:Label>   
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SERIAL NO">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSerialNo" runat="server" Width="100px" Height="22px" Text='<%# Eval("SerialNo") %>'  data-toggle="tooltip" data-placement="top" title='<%# Eval("SerialNo") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ACTION">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="ButtonAdd" runat="server" CssClass="btn btn-block bg-blue waves-effect" Font-Size="11px" Height="22px" Text="ADD" OnClick="ButtonAdd_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-Height="22px" ControlStyle-CssClass="btn btn-block bg-green waves-effect" DeleteText="DELETE" ControlStyle-Font-Size="11px" />
                                    
                                </Columns>

                            </asp:GridView>
                            <br />
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

                        
                            <div style="margin-top:25px; width:300px;">
                                <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect"
                                    onclick="btnSubmit_Click" />
                                <asp:Button ID="btnCancelTransaction" runat="server" Text="CANCEL TRANSACTION" Visible="false" CssClass="btn bg-orange waves-effect" 
                                    onclick="btnCancelTransaction_Click" />
                                <asp:HiddenField ID="hfTotalQuantity" runat="server" />
                                <asp:HiddenField ID="hfLOANumber" runat="server" />
                                <asp:HiddenField ID="hfCTRLNo" runat="server" />
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
        <asp:PostBackTrigger ControlID = "btnCancelTransaction" />
        <asp:PostBackTrigger ControlID = "linkAttachment1" />
        <asp:PostBackTrigger ControlID = "linkAttachment2" />
        <asp:PostBackTrigger ControlID = "linkAttachment3" />
    </Triggers>
    </asp:UpdatePanel>    
    
</asp:Content>
