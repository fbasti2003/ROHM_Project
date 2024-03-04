<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="URF_RequestEntry_New.aspx.cs" Inherits="REPI_PUR_SOFRA.URF_RequestEntry_New" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="plugins/sweetalert/sweetalert.min.js" type="text/javascript"></script>
    
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
            padding: 15px 5%;
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
                html: true,
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
        
        function validateReason(ddl) {
            var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
            if (selectedText == 'SPECIAL REASON:')
            {
                document.getElementById('<%= txtOtherReason.ClientID %>').disabled = false;
                document.getElementById('<%= txtOtherReason.ClientID %>').value = "";
            } else{
                document.getElementById('<%= txtOtherReason.ClientID %>').disabled = true;
                document.getElementById('<%= txtOtherReason.ClientID %>').value = "";
            }
        } 
        
        function validateStockLife(ddl) {
        var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
        if (selectedText == 'SUB-MATERIALS' || selectedText == 'MAIN-MATERIALS' || selectedText == 'CHEMICALS')
        {
            document.getElementById('<%=tblStockLife.ClientID %>').style.display = "block";
        } else {
            document.getElementById('<%=tblStockLife.ClientID %>').style.display = "none";
        }
        
        
    }  
    
        function setOnLoad() {
            //alert('test');
           document.getElementById('<%=pSubmitting.ClientID %>').style.display = "block";
        }
        
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
        
    </script>
    
    
    <script type="text/javascript">
    
    $("[id*=txtPONO]").live("keyup", function () {
        $("[id*=btnPONO]").click();
    });
    
    $("[id*=txtPRNO]").live("keyup", function () {
        $("[id*=btnPRNO]").click();
    });
    
    function setFocus(con)
    {
        var ran = con.createTextRange();
        ran.move('character', con.value.length);
        ran.select();
        con.focus();
    }
    
    function setFocus2(con)
    {
        var ran = con.createTextRange();
        ran.move('character', con.value.length);
        ran.select();
        con.focus();
    }
    
        
    </script>
       
    
    <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
            
            <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1280px;">
                
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                        <div class="card"> 
                            <div class="header" style="height:30px;">
                                <p style="color:Gray; font-size:14px; font-weight:bold;">URGENT REQUEST ENTRY <%= displayCTRLNo %></p>
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
                                      </tr>
                                      <tr>
                                        <td colspan="4">
                                            <div style="width:1218px; background-color:#00C851;">
                                                <div id="divRequester" runat="server" style="width:1218px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblRequester" runat="server" Height="44px" Width="980px" ForeColor="White" />
                                                    <asp:Label ID="lblDepartmentDivision" runat="server" Visible="false" />
                                                </div>
                                            </div>
                                        </td>
                                      </tr>
                                      <tr>
                                        <th>PROD. SEC. MNGR.</th>
                                        <th>PROD. DEPT. MNGR.</th>
                                        <th>PROD. DIV. MNGR.</th>
                                        <!--<th>PROD. HQ. MNGR.</th> -->
                                        <th>&nbsp;</th>
                                      </tr>
                                      <tr>                                    
                                        <td>
                                            <div style="width:280px; background-color:White;">
                                                <div id="divSecManager" runat="server" style="width:280px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblSecManager" runat="server" Height="44px" Width="245px" ForeColor="White" />
                                                    <asp:Label ID="lblCtrlNo" runat="server" Visible="false" />
                                                    <asp:Label ID="lblSecManagerStat" runat="server" Visible="false" />
                                                    <asp:Label ID="lblDeptManagerStat" runat="server" Visible="false" />
                                                    <asp:Label ID="lblDivManagerStat" runat="server" Visible="false" />
                                                    <!--<asp:Label ID="lblHQManagerStat" runat="server" Visible="false" />-->                                                    
                                                    <asp:Label ID="lblPurchasingBuyerStat" runat="server" Visible="false" />
                                                    <asp:Label ID="lblPurchasingManagerStat" runat="server" Visible="false" />
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width:280px; background-color:White;">
                                                <div id="divDeptManager" runat="server" style="width:280px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblDeptManager" runat="server" Height="44px" Width="245px" ForeColor="White" />
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width:280px; background-color:White;">
                                                <div id="divDivManager" runat="server" style="width:280px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblDivManager" runat="server" Height="44px" Width="245px" ForeColor="White" />
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width:280px; background-color:White;">
                                                <!-- 
                                                <div id="divHQManager" runat="server" style="width:280px; border: 1px solid black; padding-left:5px;">
                                                    <asp:Label ID="lblHQManager" runat="server" Height="44px" Width="245px" ForeColor="White" /> 
                                                    
                                                </div>
                                                -->
                                                &nbsp;
                                            </div>
                                        </td>
                                      </tr>
                                    </table>
                                    <table style="width:1250px; color:Gray; font-size:12px;">
                                        <tr>
                                            <th>BUYER</th>
                                            <th>SENT TO SUPPLIER</th>
                                            <th>PURCHASING MANAGER</th>
                                            <th>SUPPLIER RESPONSE</th>                                            
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="width:280px; background-color:White;">
                                                    <div id="divBuyer" runat="server" style="width:280px; border: 1px solid black; padding-left:5px;">
                                                        <asp:Label ID="lblBuyer" runat="server" Height="44px" Width="280px" ForeColor="White" />
                                                    </div>
                                                </div>
                                             </td>
                                            <td>
                                                <div style="width:280px; background-color:White;">
                                                    <div id="divBuyerSend" runat="server" style="width:280px; border: 1px solid black; padding-left:5px;">
                                                        <asp:Label ID="lblBuyerSend" runat="server" Height="44px" Width="280px" ForeColor="White" />
                                                    </div>
                                                </div>
                                             </td>  
                                             <td>
                                                <div style="width:280px; background-color:White;">
                                                    <div id="divPurchasingManager" runat="server" style="width:280px; border: 1px solid black; padding-left:5px;">
                                                        <asp:Label ID="lblPurchasingManager" runat="server" Height="44px" Width="280px" ForeColor="White" />
                                                    </div>
                                                </div>
                                             </td> 
                                             <td>
                                                <div style="width:280px; background-color:White;">
                                                    <div id="divSupplier" runat="server" style="width:280px; border: 1px solid black; padding-left:5px;">
                                                        <asp:Label ID="lblSupplier" runat="server" Height="44px" Width="280px" ForeColor="White" />
                                                    </div>
                                                </div>
                                             </td> 
                                             
                                        </tr>
                                    </table>
                                    <table style="width:600px; color:Gray; font-size:12px; margin-top:5px;" id="table1" runat="server">
                                        <tr>
                                            <th colspan="4">SEND DATES</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                        </tr>
                                        <tr>  
                                            <td>
                                                <asp:DropDownList ID="ddSendDates" runat="server" Height="22px" Width="280px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width:600px; color:Gray; font-size:12px; display:none; margin-top:5px;" id="tableRemarks" runat="server">
                                        <tr>
                                            <th colspan="4">PURCHASING DISAPPROVAL REMARKS</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                            <th>&nbsp;</th>
                                        </tr>
                                        <tr>  
                                            <td>
                                                <asp:TextBox ID="txtRemarks" runat="server" Height="70px" Width="1218px" TextMode="MultiLine" />
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
                            <div class="body" style="margin-top:-23px; min-height:380px; width:1280px;">
                                <table style="width:1250px; color:Gray;">
                                  <tr>
                                    <th>SUPPLIER</th>
                                    <th>ATTENTION</th> 
                                    <th>REASON</th> 
                                    <th>SPECIAL REASON</th>                                     
                                  </tr>
                                  <tr>
                                    <td><asp:DropDownList ID="ddSupplier" runat="server" Width="280px" required /></td>
                                    <td><asp:TextBox ID="txtAttention" runat="server" Width="280px" Height="22px" /></td>
                                    <td><asp:DropDownList ID="ddReason" runat="server" Width="280px" onchange="validateReason(this)" /></td>
                                    <td><asp:TextBox ID="txtOtherReason" runat="server" Width="280px" Height="22px" /></td>                                 
                                  </tr>
                                </table>
                                <table style="width:1250px; color:Gray;">
                                    <tr>
                                       <th>CATEGORY</th>
                                       <th>TYPE</th> 
                                       <th>ATTACHMENT 1</th>
                                        <th>ATTACHMENT 2</th>                                    
                                    </tr>
                                    <tr>
                                        <td><asp:DropDownList ID="ddCategory" runat="server" Width="280px" required onchange="validateStockLife(this)" /></td>
                                        <td>
                                            <asp:DropDownList ID="ddType" runat="server" Width="280px" required >
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Text="LOCAL" Value="1" />                                               
                                                <asp:ListItem Text="OVERSEAS" Value="2" />
                                            </asp:DropDownList>
                                        </td>
                                        <td><asp:FileUpload ID="fu1" runat="server" Width="280px" EnableViewState="true" accept=".pdf" /></td>
                                        <td><asp:FileUpload ID="fu2" runat="server" Width="280px" EnableViewState="true" accept=".pdf" /></td>
                                    </tr>
                                </table>
                                <table id="tblStockLife" runat="server" style="width:1250px; color:Gray; margin-top:3px; display:none;">
                                    <tr>
                                       <th>REPI STOCK</th> 
                                       <th>DAILY USAGE</th>
                                       <th>STOCKLIFE (days)</th>
                                       <th>&nbsp;</th>                                      
                                    </tr>
                                    <tr>                                        
                                        <td><asp:TextBox ID="txtRepiStock" runat="server" Width="280px" Height="22px" /></td>
                                        <td><asp:TextBox ID="txtDailyUsage" runat="server" Width="280px" Height="22px" /></td>
                                        <td><asp:TextBox ID="txtStockLife" runat="server" Width="280px" Height="22px" /></td>
                                        <td><asp:FileUpload ID="fuStock" runat="server" Width="280px" EnableViewState="true" Enabled="false" accept=".pdf" /></td>
                                    </tr>
                                </table>
                                <table id="tblSupplierRemarks" runat="server" style="width:100%; margin-top:10px; color:Gray;">
                                  <tr>
                                    <th>SUPPLIER COMMENTS</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtSupplierComments" runat="server" TextMode="MultiLine" Width="1215px" Height="80px" Enabled="false" /></td>
                                  </tr>
                                </table>
                                <table id="tblPurchasingRemarks" runat="server" style="width:100%; margin-top:10px; color:Gray;">
                                  <tr>
                                    <th>PURCHASING REMARKS</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtPurchasingRemarks" runat="server" TextMode="MultiLine" Width="1215px" Height="80px" Enabled="false" /></td>
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
                                <table id="tblReOpenRemarks" runat="server" style="width:100%; margin-top:10px; color:Gray;" visible="false">
                                    <tr>
                                    <th>RE-OPEN REMARKS</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtReOpenRemarks" runat="server" TextMode="MultiLine" Width="1215px" Height="170px" /></td>
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
                                                <asp:TextBox ID="txtPONO" runat="server" Width="110px" Text='<%# Eval("RdPONO") %>' Enabled="false" onfocus="setFocus(this);" data-toggle="tooltip" data-placement="top" title='<%# Eval("RdPONO") %>'></asp:TextBox>
                                                <asp:Button ID="btnPONO" runat="server" Width="10px" Height="20px" Text="..." CommandName="btnPONO_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RdRefId") %>' Visible="false" />                                                
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PR.NO.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPRNO" runat="server" Text='<%# Eval("RdPRNO") %>' onfocus="setFocus2(this);" Width="110px" data-toggle="tooltip" data-placement="top" title='<%# Eval("RdPRNO") %>' ></asp:TextBox>
                                                <asp:Button ID="btnPRNO" runat="server" Width="10px" Height="20px" Text="..." CommandName="btnPRNO_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ITEM NAME">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemName" runat="server" Text='<%# Eval("RdItemName") %>' Enabled="false" Width="200px" data-toggle="tooltip" data-placement="top" title='<%# Eval("RdItemName") %>' ></asp:TextBox>
                                                <asp:Button ID="btnItemName" runat="server" Width="10px" Height="20px" Text="..." CommandName="btnItemName_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SPECIFICATION">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSpecification" runat="server" Text='<%# Eval("RdSpecs") %>' Enabled="false" Width="200px" data-toggle="tooltip" data-placement="top" title='<%# Eval("RdSpecs") %>' ></asp:TextBox>
                                                <asp:Button ID="btnSpecification" runat="server" Width="10px" Height="20px" Text="..." CommandName="btnSpecification_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("RdQuantity") %>' Enabled="false" Width="30px" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddUOM" runat="server" Width="40px" Enabled="false" >                                                
                                                </asp:DropDownList>
                                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("RdUnitOfMeasure") %>' Visible="false" />
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO DATE">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDeliveryConfirmationDate" runat="server" Text='<%# Eval("RdDeliveryConfirmationDate") %>' Enabled="false" Width="100px" ></asp:TextBox>
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REQ. DEL DATE">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRequestedDeliveryDate" runat="server" Text='<%# Eval("RdRequestedDeliveryDate") %>' Width="95px" ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="URGT REP. DATE">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReplyDeliveryDate" runat="server" Text='<%# Eval("RdReplyDeliveryDate") %>' Width="100px" Enabled="false" ></asp:TextBox>
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
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnLinkSelectPR" runat="server" Text="SELECT" CommandName="btnLinkSelectPR_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PO.NO.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkPONO" runat="server" Text='<%# Eval("RdPONO") %>' Width="120px" />  
                                                                            <asp:Label ID="lblDataIndex" runat="server" Visible="false" Text='<%# Eval("DataIndex") %>' />                                                                           
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PR.NO.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkPRNO" runat="server" Text='<%# Eval("RdPRNO") %>' Width="120px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ITEM NAME">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkItemName" runat="server" Text='<%# Eval("RdItemName") %>' Width="120px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SPECIFICATION">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkSpecs" runat="server" Text='<%# Eval("RdSpecs") %>' Width="120px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="QTY.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkQuantity" runat="server" Text='<%# Eval("RdQuantity") %>' Width="120px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UOM">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkUnitOfMeasure" runat="server" Text='<%# Eval("RdUnitOfMeasure") %>' Width="120px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="RD DATE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkReplyDeliveryDate" runat="server" Text='<%# Eval("RdDeliveryConfirmationDate") %>' Width="120px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PO DATE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLinkPODate" runat="server" Text='<%# Eval("RdPODate") %>' Width="120px" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnLinkSelectPR2" runat="server" Text="SELECT" CommandName="btnLinkSelectPR_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                            <div class="body" style="margin-top:-23px; height:65px; width:1075px;">
                                <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSubmit_Click" OnClientClick="setOnLoad()" />
                                <asp:Button ID="btnReOpen" runat="server" Text="RE-OPEN" CssClass="btn bg-light-blue waves-effect" OnClick="btnReOpen_Click" Visible="false" />
                                <asp:Button ID="btnPreview" runat="server" Text="PREVIEW" CssClass="btn bg-light-blue waves-effect" OnClick="btnPreview_Click" OnClientClick="setOnLoad()" />
                                <p runat="server" id="pReminder1" style="color:Red; margin-left:200px; margin-top:-55px; font-size:26px; display:none;">ATTENTION:</p>
                                <p runat="server" id="pReminder2" style="margin-left:200px; margin-top:-20px; font-size:14px; display:none;">System detected empty PONO, Click the 3(...) dots button beside PR.NO field to update accordingly. If no selection displayed, then Purchase Inquiry needs to be updated first before you can fill the correct PO Number.</p>
                                <p runat="server" id="pSubmitting" style="color:Red; margin-left:100px; margin-top:-20px; display:none;"><b>PROCESSING YOUR REQUEST... PLEASE WAIT...</b></p>                              
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="hover_bkgr_fricc">
                    <span class="helper"></span>
                    <div>
                        <asp:Label ID="lblSystemDetected" runat="server" Font-Size="18px" Font-Bold="true" /><br /> 
                        <asp:Label ID="lblSystemCheck" runat="server" Font-Size="14px" ForeColor="Red" /><br /><br /> 
                        <asp:Label ID="lblExistingPONumber" runat="server" />                 
                        <asp:Button ID="btnSend" runat="server" Text="YES" CssClass="btn bg-light-blue waves-effect" OnClick="btnSend_Click" Visible="false" />
                        <td><asp:Button ID="btnCancel2" runat="server" Text="OK" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();"/></td>
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


