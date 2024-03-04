<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_PO_ApprovalForm.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_PO_ApprovalForm" MasterPageFile="~/Sofra.Master" %>


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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SRF PULLOUT REQUEST APPROVAL FORM</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:1280px;">
                            <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT DATE RANGE YOU WANT TO SEARCH</p>
                            <div style="margin-top:10px;">
                                <table style="width:730px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th> 
                                    <th>CTRLNO TO SEARCH</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="160px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="160px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>   
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="300px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
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
                        <div class="body" style="margin-top:-23px; min-height:200px; width:1280px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px" 
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCounter" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="28px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNo" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCTRLNo" runat="server" Height="15px" Width="150px" Text='<%# Eval("Head_Ctrlno") %>' Visible="false" Font-Bold="true" /> 
                                            <asp:Label ID="lblSupplierEmail" runat="server" Height="15px" Width="150px" Text='<%# Eval("SupplierEmail") %>' Visible="false" Font-Bold="true" />
                                            <asp:Label ID="lblSupplierName" runat="server" Height="15px" Width="150px" Text='<%# Eval("SupplierName") %>' Visible="false" Font-Bold="true" />
                                            <asp:Label ID="lblSupplier" runat="server" Height="15px" Width="150px" Text='<%# Eval("Supplier") %>' Visible="false" Font-Bold="true" /> 
                                            <asp:Label ID="lblTotalQuantity" runat="server" Height="15px" Width="150px" Text='<%# Eval("Head_TotalQuantity") %>' Visible="false" Font-Bold="true" /> 
                                            <asp:Label ID="lblTotalBoxes" runat="server" Height="15px" Width="150px" Text='<%# Eval("Head_TotalBoxes") %>' Visible="false" Font-Bold="true" /> 
                                            <asp:LinkButton ID="lbCTRLNo" runat="server" Height="15px" Width="150px" Text='<%# Eval("Head_Ctrlno") %>' CommandName="lbCTRLNo_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Font-Bold="true" />                                              
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TRANSDATE" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransDate" runat="server" Height="15px" Width="140px" Text='<%# Eval("Head_TransactionDate") %>'  />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TYPE" HeaderStyle-Width="280px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("Head_Type") %>' Height="15px" Width="280px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="360px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Height="15px" Width="360px" Font-Size="11px" ForeColor="White" />
                                            <asp:Label ID="lblStatColor" runat="server" Text='<%# Eval("CssColorCode") %>' Visible="false" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="45px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center" style="margin-left:1px;">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ToolTip="APPROVED BUTTON" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>
                                                    <td align="center" style="margin-left:1px;">
                                                        <asp:ImageButton ID="ibDisapproved" runat="server" ToolTip="DISAPPROVED BUTTON" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>                                                                                                         
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REMARKS" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="200px" Height="16px" Enabled="false" />
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
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>




