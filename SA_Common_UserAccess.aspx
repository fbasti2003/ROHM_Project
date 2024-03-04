<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SA_Common_UserAccess.aspx.cs" Inherits="REPI_PUR_SOFRA.SA_Common_UserAccess" MasterPageFile="~/Sofra.Master" %>

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
            document.getElementById("txtLoaNo").style.display = "block";            
            document.getElementById("lblLoaNo").style.display = "none";  
            
            document.getElementById("txtLoaBalance").style.display = "block";            
            document.getElementById("lblLoaBalance").style.display = "none";
            
            document.getElementById("txtLoaPriceValue").style.display = "block";            
            document.getElementById("lblLoaPriceValue").style.display = "none";
            
            document.getElementById("txtMaturityDate").style.display = "block";            
            document.getElementById("lblMaturityDate").style.display = "none";
            
            document.getElementById("txtRemarks").style.display = "block";            
            document.getElementById("lblRemarks").style.display = "none";          
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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">USER ACCESS</p>
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
                    
                    <div style="float:right; margin-top:-29px; margin-left:-250px;">
                        <asp:TextBox ID="txtSearch" runat="server" Width="350px" Height="23px" Font-Italic="true" ForeColor="Gray" Font-Size="12px" />
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" OnClick="btnSearch_Click" CssClass="btn btn-block bg-pink waves-effect" Height="25px" Width="90px" />                        
                        <asp:Button ID="btnExport" runat="server" Text="EXPORT TO EXCEL" OnClick="btnExport_Click" CssClass="btn btn-block bg-green waves-effect" Height="25px" Width="160px" />
                    </div>
                    
                    <div class="tab-content" style="margin-top:10px;">
                        
                        <div role="tabpanel" class="tab-pane fade in active" id="active">
                        
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!" >
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("LcRefId") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="USERNAME" HeaderStyle-Width="290px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' />
                                            <asp:TextBox ID="txtUserName" runat="server" Text='<%# Eval("UserName") %>' Width="289px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewUserName" runat="server" Width="287px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="FULLNAME" HeaderStyle-Width="750px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>' CommandName="lblFullName_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            <asp:TextBox ID="txtFullName" runat="server" Text='<%# Eval("FullName") %>' Width="750px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />
                                            <asp:Label ID="lblPassword2" runat="server" Text='<%# Eval("Password2") %>' Visible="false"/>
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewFullName" runat="server" Width="750px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                                                                                                                                                                                 
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" >                                
                                        <ItemTemplate>
                                            <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lbDisabled" runat="server" Text="DISABLED" CommandName="StartDisabling_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                </td>
                                            </tr>
                                            </table>
                                        </ItemTemplate>   
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
                                            <asp:TemplateField HeaderText="USERNAME" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="FULLNAME" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>    
                                          <Columns>
                                            <asp:TemplateField HeaderText="SECTION" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSectionString" runat="server" Text='<%# Eval("SectionString") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>   
                                          <Columns>
                                            <asp:TemplateField HeaderText="DEPARTMENT" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartmentString" runat="server" Text='<%# Eval("DepartmentString") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>  
                                          <Columns>
                                            <asp:TemplateField HeaderText="DIVISION" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivisionString" runat="server" Text='<%# Eval("DivisionString") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="HQ" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQString" runat="server" Text='<%# Eval("HqString") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="PC" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPCString" runat="server" Text='<%# Eval("PcString") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoryString" runat="server" Text='<%# Eval("CategoryString") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>                                  
                                          <Columns>
                                            <asp:TemplateField HeaderText="ADDED BY" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddedBy" runat="server" Text='<%# Eval("AddedByString") %>' />
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
                                            <asp:TemplateField HeaderText="ACESS TYPE" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccessType" runat="server" Text='<%# Eval("AccessType") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>   
                                          <Columns>
                                            <asp:TemplateField HeaderText="LOCAL#" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocalNumber" runat="server" Text='<%# Eval("LocalNumber") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>   
                                          <Columns>
                                            <asp:TemplateField HeaderText="EMAIL ADDRESS" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Eval("EmailAddress") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>     
                                          <Columns>
                                            <asp:TemplateField HeaderText="URF PROD. SECTION MANAGER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblURFProdSectionManager" runat="server" Text='<%# Eval("Urf_Prod_SectionManager") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="URF PROD. DEPARTMENT MANAGER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblURFProdDepartmentManager" runat="server" Text='<%# Eval("Urf_Prod_DepartmentManager") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="URF PROD. DIVISION MANAGER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblURFProdDivisionManager" runat="server" Text='<%# Eval("Urf_Prod_DivisionManager") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="URF PROD. HQ MANAGER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblURFProdHQManager" runat="server" Text='<%# Eval("Urf_Prod_HQManager") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="SCD INCHARGE" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSCDIncharge" runat="server" Text='<%# Eval("Scd_Incharge") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="SCD DEPARTMENT MANAGER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSCDDepartmentManager" runat="server" Text='<%# Eval("Scd_DepartmentManager") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="SCD DIVISION MANAGER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSCDDivisionManager" runat="server" Text='<%# Eval("Scd_DivisionManager") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="PROFORMA PC MANAGER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProformanPCManager" runat="server" Text='<%# Eval("Proforma_PCManager") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns> 
                                          <Columns>
                                            <asp:TemplateField HeaderText="RFQ PROD. APPROVER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRFQProdApprover" runat="server" Text='<%# Eval("Rfq_Prod_Approver") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>    
                                          <Columns>
                                            <asp:TemplateField HeaderText="CRF PROD. APPROVER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCRFProdApprover" runat="server" Text='<%# Eval("Crf_Prod_Approver") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>    
                                          <Columns>
                                            <asp:TemplateField HeaderText="DRF PROD. APPROVER" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRFProdApprover" runat="server" Text='<%# Eval("Drf_Prod_Approver") %>' />
                                                </ItemTemplate>    
                                            </asp:TemplateField>
                                          </Columns>                                                                            
                                  
                        </asp:GridView>
                
                        </div>    
                        
                        <div role="tabpanel" class="tab-pane fade" id="inactive">
                            
                            <asp:GridView ID="gvDisabled" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"                                                    
                                                                     OnRowDataBound="gvDisabled_OnRowDataBound" OnPageIndexChanging="gvDisabled_PageIndexChanging" OnRowCommand="gvDisabled_RowCommand"                                                             
                                                                     FooterStyle-Font-Size="10px"
                                                                     EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("LcRefId") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="USERNAME" HeaderStyle-Width="290px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>' />
                                            <asp:TextBox ID="txtUserName" runat="server" Text='<%# Eval("UserName") %>' Width="289px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewUserName" runat="server" Width="287px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="FULLNAME" HeaderStyle-Width="750px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>' />                                            
                                            <asp:TextBox ID="txtFullName" runat="server" Text='<%# Eval("FullName") %>' Width="750px" Height="14px" BorderStyle="Solid" BorderWidth="1px" />
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewFullName" runat="server" Width="750px" Height="16px" Font-Size="11px" BorderStyle="Solid" BorderWidth="1px" />
                                        </FooterTemplate>
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
                    
                    <div style="margin-top:20px;">
                        <asp:Button ID="btnAddNew" runat="server" Text="ADD NEW" Height="25px" CssClass="btn bg-blue waves-effect" OnClick="btnAddNew_Click" Width="90px"  />
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
