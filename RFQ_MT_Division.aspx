<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_MT_Division.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_MT_Division" MasterPageFile="~/Sofra.Master" %>

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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">DIVISION TABLE</p>
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
                        <asp:Button ID="btnExport" runat="server" Text="EXPORT TO EXCEL" OnClick="btnExport_Click" CssClass="btn btn-block bg-green waves-effect" Height="25px" Width="160px" />
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
                                    <asp:TemplateField HeaderText="CODE" HeaderStyle-Width="225px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>' />
                                            <asp:TextBox ID="txtCode" runat="server" Text='<%# Eval("Code") %>' Width="222px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewCode" runat="server" Width="222px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="ROPROS CODE" HeaderStyle-Width="225px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoprosCode" runat="server" Text='<%# Eval("RoprosCode") %>' />
                                            <asp:TextBox ID="txtRoprosCode" runat="server" Text='<%# Eval("RoprosCode") %>' Width="222px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewRoprosCode" runat="server" Width="222px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="DESCRIPTION" HeaderStyle-Width="585px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' Width="585px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewDescription" runat="server" Width="810px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
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
                            
                            <asp:GridView ID="gvExport" AutoGenerateColumns="false" runat="server"
                                           HeaderStyle-BackColor="Green" AllowPaging="false" Visible="true"
                                           EmptyDataText="No Record Found!" PagerStyle-Font-Names="Tahoma" PageSize="1000" > 
                                           
                                          <Columns>
                                            <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("RefId") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="CODE" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="ROPROS CODE" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRoprosCode" runat="server" Text='<%# Eval("RoprosCode") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>
                                          <Columns>
                                            <asp:TemplateField HeaderText="DESCRIPTION" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="ADDED BY" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddedBy" runat="server" Text='<%# Eval("AddedBy") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>
                                          <Columns>
                                            <asp:TemplateField HeaderText="DATE ADDED" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddedDate" runat="server" Text='<%# Eval("AddedDate") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>
                                          <Columns>
                                            <asp:TemplateField HeaderText="UPDATED BY" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedBy" runat="server" Text='<%# Eval("UpdatedBy") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>
                                          <Columns>
                                            <asp:TemplateField HeaderText="DATE UPDATED" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUpdatedDate" runat="server" Text='<%# Eval("UpdatedDate") %>' />
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
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("RefId") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="CODE" HeaderStyle-Width="220px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>' Width="220px" />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ROPROS CODE" HeaderStyle-Width="220px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoprosCode" runat="server" Text='<%# Eval("RoprosCode") %>' Width="220px" />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DESCRIPTION" HeaderStyle-Width="585px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' Width="585px" />                                            
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
    
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnExport" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>




