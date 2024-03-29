﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_MT_ContainerTubes.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_MT_ContainerTubes" MasterPageFile="~/Sofra.Master" %>

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
            document.getElementById("txtAddress").style.display = "block";
            document.getElementById("txtPhone").style.display = "block";
            document.getElementById("txtFax").style.display = "block";
            
            document.getElementById("lblName").style.display = "none";
            document.getElementById("lblAddress").style.display = "none";
            document.getElementById("lblPhone").style.display = "none";
            document.getElementById("lblFax").style.display = "none";
            
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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">PULL OUT OF CONTAINER TUBES MAINTENANCE TABLE</p>
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
                        <asp:TextBox ID="txtSearch" runat="server" Width="400px" Height="23px" Font-Italic="true" ForeColor="Gray" Font-Size="12px" />
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" OnClick="btnSearch_Click" CssClass="btn btn-block bg-pink waves-effect" Height="25px" Width="90px" />
                    </div>
                    
                    <div class="tab-content" style="margin-top:10px;">
                        
                        <div role="tabpanel" class="tab-pane fade in active" id="active">
                        
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" PageSize="27" PagerStyle-Font-Size="13px"   
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     FooterStyle-Font-Size="10px"
                                                                     EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("RefId") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SPECIFICATION" HeaderStyle-Width="290px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("Specification") %>' />      
                                            <asp:TextBox ID="txtSpecification" runat="server" Text='<%# Eval("Specification") %>' Width="290px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />                                      
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewSpecification" runat="server" Width="290px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="WEIGHT OF BOX" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeightOfBox" runat="server" Text='<%# Eval("WeightOfBox") %>' />         
                                            <asp:TextBox ID="txtWeightOfBox" runat="server" Text='<%# Eval("WeightOfBox") %>' Width="100px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />                                   
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewWeightOfBox" runat="server" Width="100px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="MULTIPLIER (PCS) FORMULA" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMultiplier" runat="server" Text='<%# Eval("Multiplier") %>' />   
                                            <asp:TextBox ID="txtMultiplier" runat="server" Text='<%# Eval("Multiplier") %>' Width="180px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />                                                                            
                                        </ItemTemplate>  
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewMultiplier" runat="server" Width="180px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" >                                
                                        <ItemTemplate>
                                            <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbEdit" runat="server" Text="EDIT" CommandName="StartEditing_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:LinkButton ID="lbDisabled" runat="server" Text="DISABLED" CommandName="StartDisabling_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:LinkButton ID="lbSave" runat="server" Text="SAVE" CommandName="StartUpdating_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </td>
                                            </tr>
                                            </table>
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbAddNew" runat="server" Text="ADD NEW" Font-Size="11px" CommandName="AddNew_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:LinkButton ID="lbSaveNew" runat="server" Text="SAVE" Font-Size="11px" CommandName="SaveNew_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </td>
                                            </tr>
                                            </table>
                                        </FooterTemplate>    
                                    </asp:TemplateField>                            
                                </Columns>
                                
                            </asp:GridView>
                        </div>    
                        
                        <div role="tabpanel" class="tab-pane fade" id="inactive">
                            
                            <asp:GridView ID="gvDisabled" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" PageSize="27" PagerStyle-Font-Size="13px"   
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvDisabled_OnRowDataBound" OnPageIndexChanging="gvDisabled_PageIndexChanging" OnRowCommand="gvDisabled_RowCommand"                                                             
                                                                     FooterStyle-Font-Size="10px"
                                                                     EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("RefId") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SPECIFICATION" HeaderStyle-Width="290px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("Specification") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="WEIGHT OF BOX" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeightOfBox" runat="server" Text='<%# Eval("WeightOfBox") %>' />                                                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="MULTIPLIER (PCS) FORMULA" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMultiplier" runat="server" Text='<%# Eval("Multiplier") %>' />                                               
                                        </ItemTemplate>                                                                          
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" >                                
                                        <ItemTemplate>
                                            <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbEnabled" runat="server" Text="ENABLED" CommandName="StartEnabling_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </td>                                                
                                            </tr>
                                            </table>
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
