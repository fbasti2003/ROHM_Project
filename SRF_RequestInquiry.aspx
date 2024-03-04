<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_RequestInquiry.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_RequestInquiry" MasterPageFile="~/Sofra.Master" %>

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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SERVICE REPAIR REQUEST MONITORING</p>
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
                                    <th>CTRLNO TO SEARCH</th>
                                    <th>ITEM STATUS</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="150px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="150px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>   
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="250px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:DropDownList ID="ddItemStatus" runat="server" Font-Bold="true" Font-Size="14px" class="form-control" Width="150px" Height="28px" >
                                            <asp:ListItem Value="pending" Text="PENDING" Selected="True" />
                                            <asp:ListItem Value="approved" Text="APPROVED" />
                                            <asp:ListItem Value="disapproved" Text="DISAPPROVED" />
                                            <asp:ListItem Value="all" Text="ALL" />
                                        </asp:DropDownList>
                                    </td> 
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
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
                        <div class="body" style="margin-top:-23px; min-height:50px; width:1280px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PageSize="30" PagerStyle-Font-Size="13px"   
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("RefId") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblCtrl" runat="server" Font-Bold="true" Height="15px" Text='<%# Eval("CTRLNo") %>' CommandName="lblCtrl_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQ INCHARGE" HeaderStyle-Width="188px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqIncharge" runat="server" Text='<%# Eval("ReqIncharge") %>' Height="15px" Width="188px" Font-Size="11px" Font-Bold="false" ForeColor="White" data-toggle="tooltip" data-placement="top" title='<%# Eval("StatRemarks") %>' />  
                                            <asp:Label ID="lblReqInchargeStat" runat="server" Text='<%# Eval("ReqInchargeStat") %>' Visible="false" />                                                                               
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQ MANAGER" HeaderStyle-Width="188px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReqManager" runat="server" Text='<%# Eval("ReqManager") %>' Height="15px" Width="188px" Font-Size="11px" Font-Bold="false" ForeColor="White" data-toggle="tooltip" data-placement="top" title='<%# Eval("StatRemarks") %>' />      
                                            <asp:Label ID="lblReqManagerStat" runat="server" Text='<%# Eval("ReqManagerStat") %>' Visible="false" />                                       
                                        </ItemTemplate>                                                             
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PUR INCHARGE" HeaderStyle-Width="188px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurIncharge" runat="server" Text='<%# Eval("PurIncharge") %>' Height="15px" Width="188px" Font-Size="11px" Font-Bold="false" ForeColor="White" data-toggle="tooltip" data-placement="top" title='<%# Eval("StatRemarks") %>' />    
                                            <asp:Label ID="lblPurInchargeStat" runat="server" Text='<%# Eval("PurInchargeStat") %>' Visible="false" />                                         
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="PUR DEPT. MNGR." HeaderStyle-Width="188px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurDeptManager" runat="server" Text='<%# Eval("PurDeptManager") %>' Height="15px" Width="188px" Font-Size="11px" Font-Bold="false" ForeColor="White" data-toggle="tooltip" data-placement="top" title='<%# Eval("StatRemarks") %>' /> 
                                            <asp:Label ID="lblPurDeptManagerStat" runat="server" Text='<%# Eval("PurDeptManagerStat") %>' Visible="false" />                                            
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="IMPEX" HeaderStyle-Width="188px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblImpex" runat="server" Text='<%# Eval("PurImpex") %>' Height="15px" Width="188px" Font-Size="11px" Font-Bold="false" ForeColor="White" data-toggle="tooltip" data-placement="top" title='<%# Eval("StatRemarks") %>' /> 
                                            <asp:Label ID="lblImpexStat" runat="server" Text='<%# Eval("PurImpexStat") %>' Visible="false" />                                            
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                            
                            <asp:GridView ID="gvExport" runat="server" AutoGenerateColumns="false" Visible="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNO" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCtrlNo" runat="server" Text='<%# Eval("CTRLNo") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="REFERENCE PR/PO" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefPRPO" runat="server" Text='<%# Eval("RefPRPO") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SALES INVOICE" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalesInvoice" runat="server" Text='<%# Eval("SalesInvoice") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="BRAND MACHINE NAME" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrandMachineName" runat="server" Text='<%# Eval("BrandMachineName") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SPECIFICATION" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("Specification") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="QUANTITY" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalQuantity" runat="server" Text='<%# Eval("TotalQuantity") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblUOM_Description" runat="server" Text='<%# Eval("UOM_Description") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SERIAL NO." ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SerialNo") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("Supplier") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                            </asp:GridView>
                            
                        </div>                        
                    </div>
                </div>
            </div>
                                
                                                                       
            
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnSubmit" />
        <asp:PostBackTrigger ControlID = "btnExport" />        
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>
