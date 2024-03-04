<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PLM_MT_PermitCertificates.aspx.cs" Inherits="REPI_PUR_SOFRA.PLM_MT_PermitCertificates" MasterPageFile="~/Sofra.Master" %>

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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">PERMIT CERTIFICATES</p>
                        </div>                        
                    </div>
                </div>
            </div>
            <div class="body">
                <div class="table-responsive" style="margin-top:0px;">
                    <ul class="nav nav-tabs tab-nav-right" role="tablist">
                        <li role="presentation" class="active"><a href="#active" data-toggle="tab">ACTIVE</a></li>
                        <li role="presentation"><a href="#inactive" data-toggle="tab">INACTIVE</a></li>                        
                    </ul>
                    
                    <div style="float:right; margin-top:-29px;">
                        <asp:TextBox ID="txtSearch" runat="server" Width="310px" Height="23px" Font-Italic="true" ForeColor="Gray" Font-Size="12px" />
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" OnClick="btnSearch_Click" CssClass="btn btn-block bg-pink waves-effect" Height="25px" Width="90px" />
                        <asp:Button ID="btnAddNew" runat="server" Text="ADD NEW" OnClick="btnAddNew_Click" CssClass="btn btn-block bg-green waves-effect" Height="25px" Width="90px" />
                    </div>
                    
                    <div class="tab-content" style="margin-top:10px;">
                        
                        <div role="tabpanel" class="tab-pane fade in active" id="active">
                        
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PageSize="27" PagerStyle-Font-Size="13px"   
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     FooterStyle-Font-Size="10px"
                                                                     EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                <Columns>
                                    <asp:TemplateField HeaderText="PERMIT CERTIFICATES" HeaderStyle-Width="1200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RefId") %>' Visible="false" />
                                            <tr>
                                                <td colspan="100%" style="background-color:White; margin-bottom:5px;">
                                                    <div style="margin-left:7px; margin-bottom:5px;">
                                                        
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">PERMIT NAME</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtPermitName" runat="server" Width="1180px" Height="22px" Font-Bold="true" ForeColor="Red" Text='<%# Eval("PermitName") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">CHEMICAL CONTENT</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtChemicalContent" runat="server" Width="1180px" Height="22px" Text='<%# Eval("ChemicalContent") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">ITEM NAME(S)</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtItemName" runat="server" Width="1180px" Height="22px" Text='<%# Eval("ItemName") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">SPECIFICATION</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtSpecification" runat="server" Width="1180px" Height="22px" Text='<%# Eval("Specification") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">SPECIFIED REQUIREMENT(S)</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtSpecifiedRequirements" runat="server" Width="1180px" Height="22px" Text='<%# Eval("SpecifiedRequirements") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:1185px; color:Gray;">
                                                          <tr>
                                                            <th>GOV. AGENCY</th>
                                                            <th>FREQUENCY</th>    
                                                            <th>LEADTIME (WORKING DAYS)</th>    
                                                            <th>&nbsp;</th>                               
                                                          </tr>
                                                          <tr>
                                                            <td><asp:TextBox ID="txtGovernmentAgency" runat="server" Width="393px" Height="22px" Text='<%# Eval("GovernmentAgency") %>' /></td>
                                                            <td><asp:TextBox ID="txtFrequency" runat="server" Width="393px" Height="22px" Text='<%# Eval("Frequency") %>' /></td>
                                                            <td><asp:TextBox ID="txtLeadTime" runat="server" Width="393px" Height="22px" Text='<%# Eval("LeadTime") %>' /></td>
                                                            <td>&nbsp;</td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">&nbsp;</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2">
                                                                <asp:LinkButton ID="lbEditRecord" runat="server" Text="EDIT RECORD" ForeColor="Blue" CommandName="EditRecord_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                <asp:LinkButton ID="lbDisable" runat="server" Text="DISABLE RECORD" ForeColor="Green" CommandName="DisableRecord_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            </td>
                                                          </tr>
                                                        </table>
                                                        
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                
                                
                            </asp:GridView>
                            
                        </div>
                        
                        <div role="tabpanel" class="tab-pane fade" id="inactive">
                            
                            <asp:GridView ID="gvDisabled" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PageSize="27" PagerStyle-Font-Size="13px"   
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvDisabled_OnRowDataBound" OnPageIndexChanging="gvDisabled_PageIndexChanging" OnRowCommand="gvDisabled_RowCommand"                                                             
                                                                     FooterStyle-Font-Size="10px"
                                                                     EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                <Columns>
                                    <asp:TemplateField HeaderText="PERMIT CERTIFICATES" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%" style="background-color:White; margin-bottom:5px;">
                                                    <div style="margin-left:7px; margin-bottom:20px;">
                                                        
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">PERMIT NAME</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtPermitNameDisabled" runat="server" Width="975px" Height="22px" Font-Bold="true" ForeColor="Red" Text='<%# Eval("PermitName") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">CHEMICAL CONTENT</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtChemicalContentDisabled" runat="server" Width="975px" Height="22px" Text='<%# Eval("ChemicalContent") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">ITEM NAME(S)</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtItemNameDisabled" runat="server" Width="975px" Height="22px" Text='<%# Eval("ItemName") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">SPECIFICATION</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtSpecificationDisabled" runat="server" Width="975px" Height="22px" Text='<%# Eval("Specification") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">SPECIFIED REQUIREMENT(S)</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtSpecifiedRequirementsDisabled" runat="server" Width="975px" Height="22px" Text='<%# Eval("SpecifiedRequirements") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th>GOV. AGENCY</th>
                                                            <th>FREQUENCY</th>    
                                                            <th>LEADTIME (WORKING DAYS)</th>    
                                                            <th>&nbsp;</th>                               
                                                          </tr>
                                                          <tr>
                                                            <td><asp:TextBox ID="txtGovernmentAgencyDisabled" runat="server" Width="324px" Height="22px" Text='<%# Eval("GovernmentAgency") %>' /></td>
                                                            <td><asp:TextBox ID="txtFrequencyDisabled" runat="server" Width="324px" Height="22px" Text='<%# Eval("Frequency") %>' /></td>
                                                            <td><asp:TextBox ID="txtLeadTimeDisabled" runat="server" Width="324px" Height="22px" Text='<%# Eval("LeadTime") %>' /></td>
                                                            <td>&nbsp;</td>
                                                          </tr>
                                                        </table>
                                                        
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
        </div>
    </section>
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>




