<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_AllRequestFreeForm.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_AllRequestFreeForm" MasterPageFile="~/Sofra.Master" %>

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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SERVICE REPAIR ALL REQUEST FORM (FREE FORM)</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:1140px;">
                            <div style="margin-top:10px;">
                                <table style="width:1140px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>CTRLNO | Reference PRPO | SalesInvoice | Brand Machine Name | Item Name | Item Specification | LOA8106</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="880px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
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
                        <div class="body" style="margin-top:-23px; min-height:800px; width:1280px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">

                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblCtrl" runat="server" Font-Bold="true" Text='<%# Eval("CTRLNo") %>' CommandName="lblCtrl_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="MACHINE NAME" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMachineName" runat="server" Width="200px" Text='<%# Eval("BrandMachineName") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ITEM NAME" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Width="300px" Text='<%# Eval("ItemName") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SPECIFICATION" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Width="300px" Text='<%# Eval("Specification") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="190px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Width="190px" Text='<%# Eval("StatAll") %>' ForeColor="White" Font-Bold="true" />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbPrint" runat="server" Text="PRINT" CommandName="lbPrint_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
        <asp:PostBackTrigger ControlID = "btnExportToExcel" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>


