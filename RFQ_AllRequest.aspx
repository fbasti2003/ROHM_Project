<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_AllRequest.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_AllRequest" MasterPageFile="~/Sofra.Master" %>

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
        
        
        function getFileSize(input) {
            var file = input.files[0];                        
            var currentSize = (file.size / (1024*1024)).toFixed(2);
            
            document.getElementById("<%=lblAttachmentSize.ClientID%>").innerHTML = (parseFloat(document.getElementById("<%=lblAttachmentSize.ClientID%>").innerHTML) + parseFloat(currentSize)).toFixed(2);
            
            if (parseFloat(document.getElementById("<%=lblAttachmentSize.ClientID%>").innerHTML) > 4)
            {
                document.getElementById("<%=btnSubmit2.ClientID%>").disabled = true;
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
        
        <div id="divOpacity" runat="server" class="container-fluid" style="margin-top:0px; margin-left:-15px; width:1280px;">    
        
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">RFQ - ALL REQUEST</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                            <div style="margin-top:10px;">
                                <table style="width:950px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>ENTER ITEM YOU WANT TO SEARCH</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr> 
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="850px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
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
                        <div class="body" style="margin-top:-23px; height:130px; width:1280px;">
                            <p style="color:Gray; font-size:12px; font-weight:bold;">EXPORT TO EXCEL</p>
                            <p style="color:Red; font-size:12px;">NOTE: THIS WILL TAKE A FEW MINUTES TO EXTRACT RECORDS. ADVISABLE DATE RANGE IS 3 MONTHS.</p>
                            <div style="margin-top:10px;">
                                <table style="width:1200px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th>
                                    <th>CATEGORY</th> 
                                    <th>ITEM STATUS</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="130px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="130px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>   
                                    <td>
                                        <asp:DropDownList ID="ddCategoryExport" runat="server" Width="300px" Height="28px" class="form-control" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddItemStatus" runat="server" Font-Bold="true" Font-Size="14px" class="form-control" Width="450px" Height="28px" >
                                            <asp:ListItem Value="ALL" Text="ALL" />
                                            <asp:ListItem Value="FOR PRODUCTION MANAGER APPROVAL" Text="FOR PRODUCTION MANAGER APPROVAL" />
                                            <asp:ListItem Value="HOLD BY PRODUCTION MANAGER" Text="HOLD BY PRODUCTION MANAGER" />
                                            <asp:ListItem Value="FOR BUYER APPROVAL" Text="FOR BUYER APPROVAL" />
                                            <asp:ListItem Value="HOLD BY SC BUYER" Text="HOLD BY SC BUYER" />
                                            <asp:ListItem Value="QUOTATION SENT TO SUPPLIER(S)" Text="QUOTATION SENT TO SUPPLIER(S)" />
                                            <asp:ListItem Value="SUPPLIER RESPONDED / FOR BUYER APPROVAL" Text="SUPPLIER RESPONDED / FOR BUYER APPROVAL" />
                                            <asp:ListItem Value="FOR PURCHASING INCHARGE APPROVAL" Text="FOR PURCHASING INCHARGE APPROVAL" />
                                            <asp:ListItem Value="FOR PURCHASING DEPARTMENT MANAGER APPROVAL" Text="FOR PURCHASING DEPARTMENT MANAGER APPROVAL" />
                                            <asp:ListItem Value="FOR PURCHASING DIVISION MANAGER APPROVAL" Text="FOR PURCHASING DIVISION MANAGER APPROVAL" />
                                            <asp:ListItem Value="APPROVED" Text="APPROVED" />
                                            <asp:ListItem Value="DISAPPROVED" Text="DISAPPROVED" />
                                        </asp:DropDownList>
                                    </td> 
                                    <td>
                                        <asp:Button ID="btnExportToExcel" runat="server" Text="EXPORT TO EXCEL" Height="28px" CssClass="btn bg-blue waves-effect" OnClick="btnExportToExcel_Click" />
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
                            
                            <asp:GridView ID="gvData" runat="server"
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          OnRowCommand="gvData_RowCommand" OnRowDataBound="gvData_OnRowDataBound"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="RFQNO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("Requester") %>' />
                                                <asp:Label ID="lblTransactionDate" runat="server" Visible="false" Text='<%# Eval("TransactionDate") %>' />
                                                <asp:Label ID="lblCategoryName" runat="server" Visible="false" Text='<%# Eval("CategoryName") %>' />
                                                <asp:Label ID="lblStatDivManager" runat="server" Visible="false" Text='<%# Eval("StatDivManager") %>' />
                                                <asp:Label ID="lblSection" runat="server" Visible="false" Text='<%# Eval("RhSection") %>' />
                                                <asp:Label ID="lblDepartment" runat="server" Visible="false" Text='<%# Eval("RhDepartment") %>' />
                                                <asp:Label ID="lblDepartmentCode" runat="server" Visible="false" Text='<%# Eval("RhDepartmentCode") %>' />
                                                <asp:Label ID="lblCategory" runat="server" Visible="false" Text='<%# Eval("RhCategory") %>' /> 
                                                <asp:Label ID="lblDivision" runat="server" Visible="false" Text='<%# Eval("RhDivision") %>' />
                                                <asp:LinkButton ID="linkRFQNo" runat="server" Text='<%# Eval("RdRfqNo") %>' Width="100px" CommandName="linkRFQNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CATEGORY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryName2" runat="server" Width="150px" Text='<%# Eval("CategoryName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Width="150px" Text='<%# Eval("RdDescription") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecsDrawing" runat="server" Width="150px" Text='<%# Eval("RdSpecs") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAKER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Width="110px" Text='<%# Eval("RdMaker") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatAll" runat="server" Width="290px" Text='<%# Eval("StatAll") %>' ForeColor="White" Font-Bold="true" Visible="false" />
                                                <asp:Label ID="lblStatColor" runat="server" Width="50px" Text='<%# Eval("CssColorCode") %>' Visible="false" />
                                                
                                                <div style="margin-left:3px; margin-right:10px; float:left;">                                                            
                                                    <asp:Button ID="btnReceiving" runat="server" Height="20px" Font-Bold="true" Visible="false" CommandName="btnReceiving_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </div>
                                                <div style="margin-left:3px; margin-right:3px; float:left">
                                                    <asp:Button ID="btnApproval" runat="server" Height="20px" Font-Bold="true" Visible="false" CommandName="btnApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </div> 
                                                <div style="margin-left:3px; margin-right:3px; float:left">
                                                    <asp:Button ID="btnPreview" runat="server" Height="20px" Font-Bold="true" Width="110px" Visible="false" CommandName="btnPreview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="PRINT PREVIEW" />
                                                </div>
                                                <div style="margin-left:3px; margin-right:3px; float:left">
                                                    <asp:Button ID="btnReapply" runat="server" Height="20px" Font-Bold="true" Width="110px" Visible="false" OnClientClick="setOnLoad()" CommandName="btnReapply_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="RE-APPLY" />
                                                </div>
                                                <div style="margin-left:3px; margin-right:3px; float:left">
                                                    <asp:Button ID="btnManualApproved" runat="server" Height="20px" Font-Bold="true" Width="140px" Visible="false" CommandName="btnManualApproved_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="MANUAL APPROVED" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr id="trBuyerNotes" runat="server" style="height:40px;" visible="false">
                                                    <td colspan="5" style="background-color:White;">
                                                        <p style="font-size:10px; font-weight:bold;">BUYER NOTES : 
                                                            <asp:TextBox ID="txtBuyerNotes" runat="server" Width="800px" Height="20px" Font-Size="12px" Text='<%# Eval("RhBuyerNotes") %>' />  
                                                            <asp:LinkButton ID="linkBuyerNotes" runat="server" Text="UPDATE" CommandName="linkBuyerNotes_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClientClick="setOnLoad()" />
                                                        </p>                                                        
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
            
            <div class="row clearfix" id="divReapply" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-10px; min-height:80px; width:1280px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">RE-APPLY ITEMS</p>                            
                        </div>
                        
                        <div class="body" style="margin-top:-43px; min-height:80px; width:1280px;">
                            
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>CATEGORY</th>                       
                              </tr>
                              <tr>                                
                                <td>
                                    <asp:DropDownList ID="ddCategory" runat="server" Width="300px" />
                                </td>                          
                              </tr>
                            </table>
                            
                        </div>
                        
                        <div class="body" style="margin-top:-23px; min-height:80px; width:1280px;">
                        
                            <asp:GridView ID="gvReapply" runat="server" ShowFooter="false"
                                                  AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                  HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                  HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px"
                                                  OnRowDataBound="gvReapply_OnRowDataBound"
                                                  OnRowDeleting="gvReapply_RowDeleting"
                                                  DataKeyNames="RowNumber" OnRowCommand="gvReapply_RowCommand">
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
                                            <asp:FileUpload ID="fuAttachment" runat="server" Width="170px" EnableViewState="true" accept=".pdf" onchange="return validateAtt(this);" />
                                            <asp:LinkButton ID="btnUpload" runat="server" Text="Upload" CommandName="btnUpload_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Visible="false" />                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FILENAME" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFileName" runat="server" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-Height="22px" ControlStyle-CssClass="btn btn-block bg-green waves-effect" DeleteText="DELETE" ControlStyle-Font-Size="11px" />
                                    
                                </Columns>

                            </asp:GridView>
                        
                        </div>
                        
                        <div class="body" style="margin-top:-23px; min-height:80px; width:1280px;">
                        
                            <asp:Button ID="btnSubmit2" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSubmit2_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <b>ATTACHMENT FILE SIZE:</b> <asp:Label ID="lblAttachmentSize" runat="server" Text="0" Font-Bold="true" ForeColor="Red" /> <b>MB</b> &nbsp;&nbsp;&nbsp;
                            <b style="color:Red; font-size:25px; font-weight:bold;">***PDF FILE ONLY***</b>
                        
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
        <asp:PostBackTrigger ControlID = "btnSubmit2" />
        <asp:PostBackTrigger ControlID = "btnExportToExcel" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>