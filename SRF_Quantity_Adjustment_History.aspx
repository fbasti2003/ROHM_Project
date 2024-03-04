<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_Quantity_Adjustment_History.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_Quantity_Adjustment_History" MasterPageFile="~/Sofra.Master" %>

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
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1525px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1525px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SERVICE REPAIR QUANTITY ADJUSTMENT HISTORY</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1525px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                            <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT UPDATED DATE RANGE YOU WANT TO SEARCH</p>
                            <div style="margin-top:10px;">
                                <table style="width:650px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th> 
                                    <th>CTRLNO TO SEARCH</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="150px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="150px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>   
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="250px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
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
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1525px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:50px; width:1280px;">
                            
                            <asp:GridView ID="gvDetailsQuantityCorrections" runat="server" 
                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px" OnRowCommand="gvDetailsQuantityCorrections_RowCommand"
                                                      HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" OnRowDataBound="gvDetailsQuantityCorrections_OnRowDataBound"
                                                      HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px">
                                        <Columns>      
                                        <asp:TemplateField HeaderText="NO." ItemStyle-CssClass="columnSpace">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Width="25px" Text='<%# Eval("No") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                             
                                        <asp:TemplateField HeaderText="CTRLNO" ItemStyle-CssClass="columnSpace">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCtrlNo" runat="server" Width="150px" Text='<%# Eval("CtrlNo") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REF PR/PO" ItemStyle-CssClass="columnSpace">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRefPRPO" runat="server" Width="110px" Text='<%# Eval("RefPRPO") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SALES INVOICE" ItemStyle-CssClass="columnSpace">
                                            <ItemTemplate>
                                                <asp:Label ID="txtSalesInvoice" runat="server" Width="110px" Text='<%# Eval("SalesInvoice") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BRAND / MACHINE" ItemStyle-CssClass="columnSpace">
                                            <ItemTemplate>
                                                <asp:Label ID="txtBrandMachine" runat="server" Width="110px" Text='<%# Eval("BrandMachineName") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-CssClass="columnSpace">
                                            <ItemTemplate>
                                                <asp:Label ID="txtItemName" runat="server" Width="102px" Text='<%# Eval("ItemName") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SPECIFICATION" ItemStyle-CssClass="columnSpace">
                                            <ItemTemplate>
                                                <asp:Label ID="txtSpecification" runat="server" Width="134px" Text='<%# Eval("Specification") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="txtUnitOfMeasure" runat="server" Width="60px" Text='<%# Eval("UOM_Description") %>' />
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORIGINAL QTY." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="txtQuantity" runat="server" Width="90px" Text='<%# Eval("Uq_OriginalQuantity") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NEW QTY." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNewQuantity" runat="server" Width="80px" Text='<%# Eval("Uq_UpdatedQuantity") %>' ></asp:Label>
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REASON OF CORRECTION" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReasonOfCorrection" runat="server" Width="150px" Text='<%# Eval("Uq_Reason") %>' ></asp:Label>
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UPDATED BY" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpdatedBy" runat="server" Width="200px" Text='<%# Eval("Uq_UpdatedBy") %>' ></asp:Label>
                                            </ItemTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UPDATED DATE" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpdatedDate" runat="server" Width="130px" Text='<%# Eval("Uq_UpdatedDate") %>' ></asp:Label>
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
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>

