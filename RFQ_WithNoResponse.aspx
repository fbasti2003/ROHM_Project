<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_WithNoResponse.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_WithNoResponse" MasterPageFile="~/Sofra.Master" %>

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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">RFQ WITH NO RESPONSE</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:105px; width:1280px;">
                            <!-- <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT DATE RANGE YOU WANT TO SEARCH</p> -->
                            <div style="margin-top:10px;">
                                <table id="tblApproval" runat="server" style="width:1160px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th id="thCategory" runat="server">CATEGORY</th>
                                    <th id="thApprover" runat="server">APPROVER</th>
                                    <th id="thRFQNo" runat="server">ITEM TO SEARCH</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>                                    
                                    <td><asp:DropDownList ID="ddCategory" runat="server" Width="200px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:DropDownList ID="ddApprover" runat="server" Width="300px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" >
                                            <asp:ListItem Text="FOR PROD. MANAGER APPROVAL" Value="0" />
                                            <asp:ListItem Text="FOR BUYER APPROVAL" Value="1" />
                                            <asp:ListItem Text="FOR INCHARGE APPROVAL" Value="2" />
                                            <asp:ListItem Text="FOR DEPARTMENT MANAGER APPROVAL" Value="3" />
                                            <asp:ListItem Text="FOR DIVISION MANAGER APPROVAL" Value="4" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRFQNo" runat="server" Width="370px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" />
                                    </td>    
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnExportToExcel" runat="server" Text="EXPORT TO EXCEL" Visible="false" CssClass="btn bg-blue waves-effect" OnClick="btnExportToExcel_Click" />
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
                        <div class="body" style="margin-top:-23px; width:1060px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" Visible="false"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="200px" Font-Bold="true" Text='<%# Eval("Rfqno") %>'  />    
                                            <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Visible="false" />  
                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate") %>' Visible="false" />                                                                                       
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' Height="30px" Width="198px" Font-Size="11px" />                                              
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
                                                        <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
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
                                    <asp:TemplateField HeaderText="REMARKS / CAUSE / NOTE" HeaderStyle-Width="305px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="305px" Height="30px" Enabled="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>                                                               
                                
                            </asp:GridView>
                           
                           <asp:GridView ID="gvPurchasing" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" Visible="false"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvPurchasing_OnRowDataBound" OnRowCommand="gvPurchasing_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!" >
                                <Columns>
                                    <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="200px" Font-Bold="true" Text='<%# Eval("Rfqno") %>'  />    
                                            <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Visible="false" />  
                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate") %>' Visible="false" />                                                                                       
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' Height="30px" Width="198px" Font-Size="11px" />                                              
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
                                    <asp:TemplateField HeaderText="SEND DATE(S)" HeaderStyle-Width="145px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierResponse" runat="server" Text='<%# Eval("NumberOfSuppliers_WithResponse") %>' Height="15px" Width="143px" Visible="false"  />  
                                            <asp:DropDownList ID="ddSendDates" runat="server" Width="143px" Height="20px" />                                             
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
                                    <asp:TemplateField HeaderText="REMARKS / CAUSE / NOTE" HeaderStyle-Width="305px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="305px" Height="30px" Enabled="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>    
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            
                                            <tr>
                                                <td colspan="100%" style="background-color:White;">
                                                    <div style="margin-left:7px;">
                                                    <asp:GridView ID="gvResponse" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                        HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                        HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" Visible="false"
                                                                        OnRowDataBound="gvResponse_OnRowDataBound" OnRowCommand="gvResponse_RowCommand">
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
                                                                    <asp:ImageButton ID="ibApprovedResponse" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="AResponse_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />
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
                        <div class="body" style="margin-top:-23px; height:65px; width:1280px;">
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
        <asp:PostBackTrigger ControlID = "btnExportToExcel" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>
