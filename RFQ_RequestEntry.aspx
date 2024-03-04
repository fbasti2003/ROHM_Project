<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_RequestEntry.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_RequestEntry" MasterPageFile="~/Sofra.Master" %>

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
        
        function WarningMessage(msg) {
            swal({
                title: 'REMINDER - FILE ATTACHMENT',
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
        
        function getFileSize(input) {
            var file = input.files[0];                        
            var currentSize = (file.size / (1024*1024)).toFixed(2);
            
            document.getElementById("<%=lblAttachmentSize.ClientID%>").innerHTML = (parseFloat(document.getElementById("<%=lblAttachmentSize.ClientID%>").innerHTML) + parseFloat(currentSize)).toFixed(2);
            
            if (parseFloat(document.getElementById("<%=lblAttachmentSize.ClientID%>").innerHTML) > 4)
            {
                document.getElementById("<%=btnSubmit.ClientID%>").disabled = true;
            }
        }
        
        function validateAtt(input) {  
  
            getFileSize(input);
  
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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                RFQ REQUEST ENTRY 
                            </p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:80px; width:1280px;">
                            
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>CATEGORY</th> 
                                <th>PURCHASING REMARKS</th>                         
                              </tr>
                              <tr>                                
                                <td>
                                    <asp:DropDownList ID="ddCategory" runat="server" Width="300px" />
                                </td>     
                                <td>
                                    <asp:TextBox ID="txtPurchasingRemarks" runat="server" Width="895px" Enabled="false" />
                                </td>                      
                              </tr>
                            </table>
                            
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divHoldReason" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
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
                                                <asp:Label ID="lblHoldDate" runat="server" Width="250px" Text='<%# Eval("Hold_Date") %>' />
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        </Columns> 
                                    </asp:GridView>
                                </td>
                                <td>&nbsp;</td>  
                                <td>&nbsp;</td> 
                                <td>&nbsp;</td>                                                            
                              </tr>
                              <tr>
                                <th>HOLD REASON RESPONSE</th>
                                <th>&nbsp;</th>  
                                <th>&nbsp;</th> 
                                <th>&nbsp;</th>                                                              
                              </tr>
                              <tr>
                                <th><asp:TextBox ID="txtHoldReasonResponse" runat="server" Width="1214px" /></th>
                                <th>&nbsp;</th>  
                                <th>&nbsp;</th> 
                                <th>&nbsp;</th>                                                              
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
                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RdRefId") %>' Visible="false" />
                                            <asp:Label ID="lblUnitOfMeasure" runat="server" Text='<%# Eval("RdUnitOfMeasure") %>' Visible="false" />
                                            <asp:TextBox ID="txtDescription" runat="server" Width="142px" Text='<%# Eval("RdDescription") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSpecsDrawing" runat="server" Width="142px" Text='<%# Eval("RdSpecs") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MAKER/BRAND">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMakerBrand" runat="server" Width="122px" Text='<%# Eval("RdMaker") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" Width="37px" Text='<%# Eval("RdQuantity") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddUOM" runat="server" >                                                
                                            </asp:DropDownList>
                                        </ItemTemplate>                    
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="SIZE" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFileSize" runat="server" Width="40px"></asp:TextBox>   
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECIFIC PURPOSE OF THE<br />ITEM FOR LOI REGISTRATION">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPurpose" runat="server" Width="160px" Text='<%# Eval("RdPurpose") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FOR WHAT PROCESS AND<br />PARTICULAR MACHINE?">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtProcess" runat="server" Width="140px" Text='<%# Eval("RdProcess") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REMARKS">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="100px" Text='<%# Eval("RdRemarks") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ATT.">
                                        <ItemTemplate>  
                                            <%--<asp:FileUpload ID="FileUpload1" runat="server" Width="170px" EnableViewState="true" accept=".pdf" onchange="return validateAtt(this);" /> --%>                                          
                                            <asp:FileUpload ID="fuAttachment" runat="server" Width="170px" EnableViewState="true" accept=".pdf" onchange="return validateAtt(this);" />
                                            <asp:LinkButton ID="btnUpload" runat="server" Text="Upload" CommandName="btnUpload_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Visible="false" />                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FILENAME" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFileName" runat="server" Width="100px"></asp:TextBox>
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
                            
                        </div>                        
                    </div>
                </div>
            </div>                       
            
            <div class="row clearfix" id="divExistingDetails" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="header" style="height:20px; margin-top:-23px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                PLEASE CHECK BELOW EXISTING ITEMS
                            </p>
                        </div> 
                        <div class="body" style="margin-top:-23px; width:1075px;">
                        
                            <asp:GridView ID="gvExisting" runat="server"
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" 
                                          OnRowCommand="gvExisting_RowCommand">
                            <Columns>

                            <asp:TemplateField HeaderText="RFQ NUMBER">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkRFQNo" runat="server" Width="150px" Text='<%# Eval("RdRfqNo") %>' CommandName="linkRFQNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                    <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("RhRequester") %>' />
                                    <asp:Label ID="lblTransactionDate" runat="server" Visible="false" Text='<%# Eval("RhTransactionDate") %>' />
                                    <asp:Label ID="lblCategoryName" runat="server" Visible="false" Text='<%# Eval("RhCategory") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIPTION">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Width="300px" Text='<%# Eval("RdDescription") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecsDrawing" runat="server" Width="210px" Text='<%# Eval("RdSpecs") %>' />
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
                                    <asp:Label ID="lblRemarks" runat="server" Width="150px" Text='<%# Eval("RdRemarks") %>' />                                       
                                </ItemTemplate>                   
                            </asp:TemplateField>
                            
                        </Columns>

                    </asp:GridView>       
                               
                    
                                 
                        
                        </div>
                    </div>
                </div>
            </div>                                                         
            
            <div class="row clearfix" id="divExistingMessage" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card" style="margin-left:10px; margin-right:10px;">
                        <p style="color:Red;">System detected existing request. Please check those item if is/are still good or you can ask Purchasing buyer regarding the status of the items. Do you really want to proceed this request? If YES, then click the <b>SUBMIT</b> Button to process the request.</p>
                        <p style="color:Red; font-size:16px;"><b>NOTE</b> : The form has been refresh. If you have <b>ATTACHMENT</b> please try to re-attached it again.</p>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divActionButtons" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:65px; width:1075px; font-size:14px;">                        
                            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <b>ATTACHMENT FILE SIZE:</b> <asp:Label ID="lblAttachmentSize" runat="server" Text="0" Font-Bold="true" ForeColor="Red" /> <b>MB</b> &nbsp;&nbsp;&nbsp;
                            <b style="color:Red; font-size:25px; font-weight:bold;">***PDF FILE ONLY***</b>
                        </div>
                    </div>
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

