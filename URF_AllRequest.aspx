<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="URF_AllRequest.aspx.cs" Inherits="REPI_PUR_SOFRA.URF_AllRequest" MasterPageFile="~/Sofra.Master" %>

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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">URF - ALL REQUEST</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                            <div style="margin-top:10px;">
                                <table style="width:950px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>ENTER ITEM YOU WANT TO SEARCH</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr> 
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="850px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
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
                        <div class="body" style="margin-top:-23px; width:1060px;">
                            
                            <asp:GridView ID="gvData" runat="server"
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          OnRowCommand="gvData_RowCommand" OnRowDataBound="gvData_OnRowDataBound"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="CTRLNO" HeaderStyle-Width="140px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkCTRLNO" runat="server" Width="140px" Text='<%# Eval("RdCtrlNo") %>' CommandName="linkCTRLNO_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="140px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("RhCategoryName") %>' Width="140px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO.NO." HeaderStyle-Width="110px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPONO" runat="server" Text='<%# Eval("RdPONO") %>' Width="110px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PR.NO." HeaderStyle-Width="110px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPRNO" runat="server" Text='<%# Eval("RdPRNO") %>' Width="110px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ITEM NAME" HeaderStyle-Width="190px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("RdItemName") %>' Width="190px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="SPECS" HeaderStyle-Width="190px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecs" runat="server" Text='<%# Eval("RdSpecs") %>' Width="190px" />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="QTY." HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Width="40px" Text='<%# Eval("RdQuantity") %>' />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitOfMeasure" runat="server" Width="50px" Text='<%# Eval("RdUnitOfMeasure") %>' />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="REPLY DELIVERY DATE" HeaderStyle-Width="95px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReplyDeliveryDate" runat="server" Text='<%# Eval("RdDeliveryConfirmationDate") %>' />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="REQUESTED DELIVERY DATE" HeaderStyle-Width="95px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestedDeliveryDate" runat="server" Text='<%# Eval("RdRequestedDeliveryDate") %>' />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="URGENT REPLY DATE" HeaderStyle-Width="95px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUrgentReplyDate" runat="server" Text='<%# Eval("RdReplyDeliveryDate") %>' />                                                
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