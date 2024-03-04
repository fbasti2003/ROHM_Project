<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PIPL_InvoiceEntry.aspx.cs"  Inherits="REPI_PUR_SOFRA.PIPL_InvoiceEntry" MasterPageFile="~/Sofra.Master" %>

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
            $("[id*=txtETDManila]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
    
    <script type="text/javascript">
    
    function validateNatureOfGoods(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        if (selectedText == 'PERISHABLE')
        {
            WarningMessage('For PERISHABLE, Please accomplish NON-DANGEROUS CERTIFICATION with attachment of SDS in GHS Format.');
        }
    }    
    
    function validatePurposeOthers(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        if (selectedText == 'OTHERS')
        {
            document.getElementById('<%= txtPurposeOthers.ClientID %>').disabled = false;
            document.getElementById('<%= txtPurposeOthers.ClientID %>').value = "";
        } else{
            document.getElementById('<%= txtPurposeOthers.ClientID %>').disabled = true;
            document.getElementById('<%= txtPurposeOthers.ClientID %>').value = "";
        }
    }
    
    function optionView(ddl)
    {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        if (selectedText == 'BIGGER VIEW')
        {
            $("#<%=btnBiggerView.ClientID%>").click();
        } else {
            $("#<%=btnBiggerView.ClientID%>").click();
        }
        
    }
    
    function validateSalesTypeAndBusinessUnitAndAccountCode(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        if (selectedText == 'WITH')
        {        
            document.getElementById('<%= ddBusinessUnit.ClientID %>').disabled = false;
            document.getElementById('<%= ddAccountCode.ClientID %>').disabled = false;
            document.getElementById('<%= ddSalesType.ClientID %>').disabled = false;
            document.getElementById('<%= ddSalesType.ClientID %>').options[document.getElementById('<%= ddSalesType.ClientID %>').selectedIndex].innerHTML = "";
            
            WarningMessage('For WITH COMMERCIAL VALUE, please check and confirm the BUSINESS UNIT and ACCOUNT CODE with ACCOUNTING DEPARTMENT.');
            
        } else{
            document.getElementById('<%= ddBusinessUnit.ClientID %>').disabled = true;
            document.getElementById('<%= ddAccountCode.ClientID %>').disabled = true;
            document.getElementById('<%= ddSalesType.ClientID %>').disabled = true;
            document.getElementById('<%= ddSalesType.ClientID %>').options[document.getElementById('<%= ddSalesType.ClientID %>').selectedIndex].innerHTML = "NON-SALES";
            document.getElementById('<%= ddBusinessUnit.ClientID %>').options[document.getElementById('<%= ddBusinessUnit.ClientID %>').selectedIndex].innerHTML = "";
            document.getElementById('<%= ddAccountCode.ClientID %>').options[document.getElementById('<%= ddAccountCode.ClientID %>').selectedIndex].innerHTML = "";
        }
    }
    
    function validateBusinessUnitAndAccountCode(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        if (selectedText == 'SALES')
        {
            document.getElementById('<%= ddBusinessUnit.ClientID %>').disabled = false;
            document.getElementById('<%= ddAccountCode.ClientID %>').disabled = false;
        } else {
            document.getElementById('<%= ddBusinessUnit.ClientID %>').disabled = true;
            document.getElementById('<%= ddAccountCode.ClientID %>').disabled = true;
        }
    }
    
    function validateBDNValue(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        if (selectedText == '')
        {
            document.getElementById('<%= ddBDNValue.ClientID %>').disabled = true;
            document.getElementById('<%= ddBDNValue.ClientID %>').value = "";
        } else{
            document.getElementById('<%= ddBDNValue.ClientID %>').disabled = false;
            document.getElementById('<%= ddBDNValue.ClientID %>').value = "";
        }
    }
    
    function validateInsurance(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        if (selectedText == 'CIF')
        {
            document.getElementById('<%= lblInsurance.ClientID %>').innerHTML = "ALL COVERED BY SHIPPER";
        }
        if (selectedText == 'FOB')
        {
            document.getElementById('<%= lblInsurance.ClientID %>').innerHTML = "ALL COVERED BY BUYER/CONSIGNEE";
        }
        if (selectedText == 'DDP (FREEHOUSE)')
        {
            document.getElementById('<%= lblInsurance.ClientID %>').innerHTML = "ALL COVERED BY SHIPPER";
        }
        if (selectedText == 'EX-WORKS')
        {
            document.getElementById('<%= lblInsurance.ClientID %>').innerHTML = "ALL COVERED BY BUYER/CONSIGNEE";
        }
        if (selectedText == 'DAP')
        {
            document.getElementById('<%= lblInsurance.ClientID %>').innerHTML = "ALL COVERED BY SHIPPER";
        }
        if (selectedText == '')
        {
            document.getElementById('<%= lblInsurance.ClientID %>').innerHTML = "";
        }
    }
    
    </script>
    
    <script type="text/javascript">
       
       nction openExcelForUpload() {
           $("#fuExcelData").click();
       }
       
       function setUploadFile()
       {          
           $("#<%=btnUpload.ClientID%>").click();
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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">PROFORMA REQUEST ENTRY</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:570px; width:1280px;">
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>CONSIGNEE</th>
                                <th>ATTENTION</th> 
                                <th>SECTION / DEPT.</th> 
                                <th>MODE OF SHIPMENT</th> 
                                
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddConsignee" runat="server" Width="270px" required /></td>
                                <td><asp:TextBox ID="txtAttention1" runat="server" Width="270px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtSectionDept1" runat="server" Width="270px" Height="22px" /></td>
                                <td><asp:DropDownList ID="ddModeOfShipment" runat="server" Width="270px" required /></td>                                 
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>PURPOSE</th>
                                <th>PURPOSE(OTHERS)</th>
                                <th>PACKING</th> 
                                <th>NATURE OF GOODS</th>                                
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddPurpose" runat="server" Width="270px" onchange="validatePurposeOthers(this)" required /></td>
                                <td><asp:TextBox ID="txtPurposeOthers" runat="server" Width="270px" Height="22px" Enabled="false" /></td>
                                <td><asp:DropDownList ID="ddPacking" runat="server" Width="270px" required /></td> 
                                <td><asp:DropDownList ID="ddNatureOfGoods" runat="server" Width="270px" onchange="validateNatureOfGoods(this)" required /></td>                                
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>CATEGORY</th>
                                <th>REQUESTER</th> 
                                <th>PORT OF DESTINATION</th>
                                <th>E.T.D. MANILA</th>
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddCategory" runat="server" Width="270px" required /></td>
                                <td><asp:Label ID="lblRequester" runat="server" Width="270px" /></td> 
                                <td><asp:TextBox ID="txtPortOfDestination" runat="server" Width="270px" Height="22px" /></td>
                                <td>
                                    <asp:TextBox ID="txtETDManila" runat="server" Width="270px" Height="22px" />                           
                                </td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>ESTIMATED VALUE IN YEN</th>
                                <th>ESTIMATED VALUE IN USD</th> 
                                <th>P.O. NO</th>
                                <th>COUNTRY OF ORIGIN</th> 
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtEstimatedYen" runat="server" Width="270px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtEstimatedUsd" runat="server" Width="270px" Height="22px" /></td> 
                                <td><asp:TextBox ID="txtPONumber" runat="server" Width="270px" Height="22px" /></td>
                                <td><asp:DropDownList ID="ddCountryOfOrigin" runat="server" Width="270px" required /></td> 
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>COMMERCIAL VALUE</th>
                                <th>TRADE TERMS</th>
                                <th>PICK-UP LOCATION</th>
                                <th>BDN</th>
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddCommercialValue" runat="server" Width="270px" onchange="validateSalesTypeAndBusinessUnitAndAccountCode(this);" required /> </td>
                                <td><asp:DropDownList ID="ddTradeTerms" runat="server" Width="270px" onchange="validateInsurance(this)" required /></td>
                                <td><asp:DropDownList ID="ddPickupLocation" runat="server" Width="270px" required />  </td>
                                <td><asp:DropDownList ID="ddBDN" runat="server" Width="270px" data-live-search="true" onchange="validateBDNValue(this);" >
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="BUYER" Value="0" />
                                        <asp:ListItem Text="DELIVERED TO" Value="1" />
                                        <asp:ListItem Text="NOTIFY PARTY" Value="2" />
                                    </asp:DropDownList> 
                                </td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>BDN VALUE</th>
                                <th>ATTENTION</th>
                                <th>REFERENCE NO.</th>
                                <th>INVOICE NO.</th>                               
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddBDNValue" runat="server" Width="270px" Enabled="false" /> </td>
                                <td><asp:TextBox ID="txtAttention2" runat="server" Width="270px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtReferenceNo" runat="server" Width="270px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtInvoiceNumber" runat="server" Width="270px" Height="22px" /> </td>                               
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>SALES TYPE</th>
                                <th>BUSINESS UNIT</th>
                                <th>ACCONT CODE</th>
                                <th>INSURANCE</th>
                              </tr>
                              <tr>
                                <td>
                                    <asp:DropDownList ID="ddSalesType" runat="server" Width="270px" Enabled="false" >
                                        <asp:ListItem Text="" Value="0" />
                                        <asp:ListItem Text="NON-SALES" Value="1" />
                                        <asp:ListItem Text="SALES" Value="2" />
                                    </asp:DropDownList>
                                </td>
                                <td><asp:DropDownList ID="ddBusinessUnit" runat="server" Width="270px" Enabled="false" /></td>
                                <td><asp:DropDownList ID="ddAccountCode" runat="server" Width="270px" Enabled="false" /></td>
                                <td><asp:Label ID="lblInsurance" runat="server" Font-Size="10px" Width="270px" ForeColor="Red" Font-Bold="true" /></td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>ATTACHMENT 1</th>
                                <th>ATTACHMENT 2</th>
                                <th>ATTACHMENT 3</th>
                                <th>ATTACHMENT 4</th>
                              </tr>
                              <tr>
                                <td><asp:FileUpload ID="fu1" runat="server" Width="270px" EnableViewState="true" accept=".pdf" /></td>
                                <td><asp:FileUpload ID="fu2" runat="server" Width="270px" EnableViewState="true" accept=".pdf" /></td>
                                <td><asp:FileUpload ID="fu3" runat="server" Width="270px" EnableViewState="true" accept=".pdf" /></td>
                                <td><asp:FileUpload ID="fu4" runat="server" Width="270px" EnableViewState="true" accept=".pdf" /></td>
                              </tr>
                            </table>
                            <table style="width:100%; margin-top:10px; color:Gray;">
                              <tr>
                                <th>REMARKS</th>
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="1200px" Height="100px" /></td>
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
                                <div class="body" style="margin-top:-23px; height:1700px; width:1280px;">
                                
                                    <div style="margin-bottom:10px; font-size:12px;">
                                        OPTION :
                                        <asp:DropDownList ID="ddBiggerView" runat="server" onchange="optionView(this);" >
                                            <asp:ListItem Text="NORMAL VIEW" Value="NORMAL VIEW" />
                                            <asp:ListItem Text="BIGGER VIEW" Value="BIGGER VIEW" />
                                        </asp:DropDownList>
                                    </div>
                                    <asp:GridView ID="gvData" runat="server" ShowFooter="True"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px"
                                                  OnRowDataBound="gvData_OnRowDataBound"
                                                  OnRowDeleting="gvData_RowDeleting"
                                                  DataKeyNames="RowNumber" OnRowCommand="gvData_RowCommand">
                                    <Columns>
                                    <asp:BoundField DataField="RowNumber" HeaderText="NO." />
                                    <asp:TemplateField HeaderText="CASE">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddCaseUnit" runat="server" Width="80px" >                                                
                                            </asp:DropDownList>
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CASE#">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCaseNumber" runat="server" Width="40px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" Width="270px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECIFICATION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSpecification" runat="server" Width="270px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" Width="40px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUOM" runat="server" Width="80px" >                                                
                                            </asp:DropDownList>
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CCY">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddCurrency" runat="server" Width="70px" >                                           
                                            </asp:DropDownList>
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U. PRICE.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUnitPrice" runat="server" Width="60px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NWT.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNetWeight" runat="server"  Width="40px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GWT.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGrossWeight" runat="server" Width="40px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MEAS (CM)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMeasurement" runat="server" Width="60px" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FILENAME" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFileName" runat="server" Width="130px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SIZE" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFileSize" runat="server" Width="40px" Height="22px" ></asp:TextBox>   
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
                            
                            <asp:GridView ID="gvDataUpload" runat="server" ShowFooter="True" Visible="false"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px"
                                                  OnRowDataBound="gvDataUpload_OnRowDataBound" >
                                    <Columns>

                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUploadRefId" runat="server" Width="50px" Height="22px" Text='<%# Eval("DetailsRefId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CASE">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUploadCaseUnit" runat="server" Width="80px" Height="22px" Font-Size="11px" /> 
                                            <asp:Label ID="lblUploadCaseUnit" runat="server" Visible="false" Text='<%# Eval("CaseUnit") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CASE#">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUploadCaseNumber" runat="server" Width="40px" Height="22px" Text='<%# Eval("CaseNumber") %>' class="form-control" required />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUploadDescription" runat="server" Width="205px" Height="22px" Text='<%# Eval("Description") %>' class="form-control" required />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECIFICATION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUploadSpecification" runat="server" Width="205px" Height="22px" Text='<%# Eval("Specification") %>' class="form-control" required />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUploadQuantity" runat="server" Width="40px" Height="22px" Text='<%# Eval("Quantity") %>' class="form-control" required />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUploadUOM" runat="server" Width="80px" Height="22px" Font-Size="11px" />   
                                            <asp:Label ID="lblUploadUOM" runat="server" Visible="false" Text='<%# Eval("UOM") %>' />                                       
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CCY">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUploadCurrency" runat="server" Width="70px" Height="22px" Font-Size="11px" /> 
                                            <asp:Label ID="lblUploadCurrency" runat="server" Visible="false" Text='<%# Eval("Currency") %>' />
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U. PRICE.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUploadUnitPrice" runat="server" Width="60px" Height="22px" Text='<%# Eval("UPrice") %>' class="form-control" required />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NWT.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUploadNetWeight" runat="server" Width="40px" Height="22px" Text='<%# Eval("NetWeight") %>' class="form-control" required />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GWT.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUploadGrossWeight" runat="server" Width="40px" Height="22px" Text='<%# Eval("GrossWeight") %>' class="form-control" required />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MEAS (CM)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUploadMeasurement" runat="server" Width="60px" Height="22px" Text='<%# Eval("Measurement") %>' class="form-control" required />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                </Columns>

                            </asp:GridView>                                                                         
                            
                            <asp:GridView ID="gvDataUpdate" runat="server" Visible="false" ShowFooter="true"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PageSize="200" PagerStyle-Font-Size="13px"   
                                                  PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px"
                                                  PagerStyle-CssClass="pagination-ys" OnRowDataBound="gvDataUpdate_OnRowDataBound" OnRowDeleting="gvDataUpdate_RowDeleting">
                                    <Columns>

                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpdatedRefId" runat="server" Width="50px" Text='<%# Eval("DetailsRefId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CASE">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUpdatedCaseUnit" runat="server" Width="80px" /> 
                                            <asp:Label ID="lblUpdatedCaseUnit" runat="server" Visible="false" Text='<%# Eval("CaseUnit") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CASE#">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpdatedCaseNumber" runat="server" Width="40px" Text='<%# Eval("CaseNumber") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpdatedDescription" runat="server" Width="150px" Text='<%# Eval("Description") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECIFICATION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpdatedSpecification" runat="server" Width="150px" Text='<%# Eval("Specification") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpdatedQuantity" runat="server" Width="40px" Text='<%# Eval("Quantity") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUpdatedUOM" runat="server" Width="80px" />   
                                            <asp:Label ID="lblUpdatedUOM" runat="server" Visible="false" Text='<%# Eval("UOM") %>' />                                       
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CCY">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUpdatedCurrency" runat="server" Width="70px" /> 
                                            <asp:Label ID="lblUpdatedCurrency" runat="server" Visible="false" Text='<%# Eval("Currency") %>' />
                                        </ItemTemplate>                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U. PRICE.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpdatedUnitPrice" runat="server" Width="60px" Text='<%# Eval("UPrice") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NWT.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpdatedNetWeight" runat="server" Width="40px" Text='<%# Eval("NetWeight") %>' class="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GWT.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpdatedGrossWeight" runat="server" Width="40px" Text='<%# Eval("GrossWeight") %>' class="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MEAS (CM)">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUpdatedMeasurement" runat="server" Width="60px" Text='<%# Eval("Measurement") %>' class="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ACTION">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="ButtonAdd2" runat="server" CssClass="btn btn-block bg-blue waves-effect" Font-Size="11px" Height="22px" Text="ADD" OnClick="ButtonAdd2_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-Height="22px" ControlStyle-CssClass="btn btn-block bg-green waves-effect" DeleteText="DELETE" ControlStyle-Font-Size="11px" />
                                    
                                </Columns>

                            </asp:GridView>                 
                            
                            <div style="margin-top:15px; width:350px;">
                                <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect"
                                    onclick="btnSubmit_Click" />
                                <asp:LinkButton ID="lbBiggerView" runat="server" Text="BIGGER VIEW" OnClick="lbBiggerView_Click" ForeColor="White" Font-Size="1px" />
                                <asp:Button ID="btnBiggerView" runat="server" OnClick="btnBiggerView_Click" BorderStyle="None" ForeColor="White" BackColor="White" BorderColor="White" />
                            </div>        
                            
                            <div style="margin-top:15px; width:200px; display:none;">
                                <asp:Button ID="btnUpload" runat="server" Text="UPLOAD DATA" OnClick="btnUpload_Click" BorderStyle="None" ForeColor="White" BackColor="White" BorderColor="White" />
                                <asp:FileUpload id="fuExcelData" runat="server" CssClass="btn bg-deep-purple waves-effect" onchange="setUploadFile();" />
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
        <asp:PostBackTrigger ControlID = "btnUpload" />
    </Triggers>
    </asp:UpdatePanel>    
    
</asp:Content>

