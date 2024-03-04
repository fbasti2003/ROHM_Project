﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PLM_MT_PermitCertificates_Entry.aspx.cs" Inherits="REPI_PUR_SOFRA.PLM_MT_PermitCertificates_Entry" MasterPageFile="~/Sofra.Master" %>

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


    <script src="plugins/jquery-datatable/jquery.dataTables.js" type="text/javascript"></script>
    <script src="plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js" type="text/javascript"></script>
    <script src="plugins/jquery-datatable/extensions/export/dataTables.buttons.min.js" type="text/javascript"></script>
    <script src="plugins/jquery-datatable/extensions/export/buttons.flash.min.js" type="text/javascript"></script>
    <script src="plugins/jquery-datatable/extensions/export/jszip.min.js" type="text/javascript"></script>
    <script src="plugins/jquery-datatable/extensions/export/pdfmake.min.js" type="text/javascript"></script>
    <script src="plugins/jquery-datatable/extensions/export/vfs_fonts.js" type="text/javascript"></script>
    <script src="plugins/jquery-datatable/extensions/export/buttons.html5.min.js" type="text/javascript"></script>
    <script src="plugins/jquery-datatable/extensions/export/buttons.print.min.js" type="text/javascript"></script>
    <script src="js/pages/tables/jquery-datatable.js" type="text/javascript"></script>
    <script src="plugins/sweetalert/sweetalert.min.js" type="text/javascript"></script>
    <script src="js/pages/ui/dialogs.js" type="text/javascript"></script>
    
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
        
        function StartEditing()
        {
            document.getElementById("txtName").style.display = "block";            
            document.getElementById("lblName").style.display = "none";            
        }
                
        
    </script>
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <section class="content">
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px;">
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card">                        
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">PERMIT CERTIFICATES (NEW ENTRY)</p>
                        </div>                        
                    </div>
                </div>
            </div>
            <div class="body">
                <div class="table-responsive" style="margin-top:0px;">                   
                    
                    <div class="tab-content" style="margin-top:10px;">
                        
                        <div style="margin-left:7px; margin-bottom:20px;">
                                                        
                            <table style="width:980px; color:Gray;">
                              <tr>
                                <th colspan="2">PERMIT NAME</th>                              
                              </tr>
                              <tr>
                                <td colspan="2"><asp:TextBox ID="txtPermitName" runat="server" Width="1180px" Height="22px" required /></td>
                              </tr>
                            </table>
                            <table style="width:980px; color:Gray;">
                              <tr>
                                <th colspan="2">CHEMICAL CONTENT</th>                              
                              </tr>
                              <tr>
                                <td colspan="2"><asp:TextBox ID="txtChemicalContent" runat="server" Width="1180px" Height="22px" required /></td>
                              </tr>
                            </table>
                            <table style="width:980px; color:Gray;">
                              <tr>
                                <th colspan="2">ITEM NAME(S)</th>                              
                              </tr>
                              <tr>
                                <td colspan="2"><asp:TextBox ID="txtItemName" runat="server" Width="1180px" Height="22px" required /></td>
                              </tr>
                            </table>
                            <table style="width:980px; color:Gray;">
                              <tr>
                                <th colspan="2">SPECIFICATION</th>                              
                              </tr>
                              <tr>
                                <td colspan="2"><asp:TextBox ID="txtSpecification" runat="server" Width="1180px" Height="22px" required /></td>
                              </tr>
                            </table>
                            <table style="width:980px; color:Gray;">
                              <tr>
                                <th colspan="2">SPECIFIED REQUIREMENT(S)</th>                              
                              </tr>
                              <tr>
                                <td colspan="2"><asp:TextBox ID="txtSpecifiedRequirements" runat="server" Width="1180px" Height="22px" required /></td>
                              </tr>
                            </table>
                            <table style="width:1187px; color:Gray;">
                              <tr>
                                <th>PERMIT CERTIFICATE</th>
                                <th>FREQUENCY</th>    
                                <th>LEADTIME (WORKING DAYS)</th>    
                                <th>&nbsp;</th>                               
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddGovernmentAgencies" runat="server" Width="391px" Height="22px" required /></td>
                                <td><asp:TextBox ID="txtFrequency" runat="server" Width="391px" Height="22px" required /></td>
                                <td><asp:TextBox ID="txtLeadTime" runat="server" Width="391px" Height="22px" required /></td>
                                <td>&nbsp;</td>
                              </tr>
                            </table>
                            
                        </div>
                        
                            
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divActionButtons" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1209px; margin-left:28px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:65px; width:980px;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSubmit_Click" />
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
    </asp:UpdatePanel>
    
</asp:Content>
