<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_AllRequest_ExportToExcel.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_AllRequest_ExportToExcel" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 

    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" type="text/css" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" type="text/css" />
    <link href="plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/themes/all-themes.css" rel="stylesheet" type="text/css" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" type="text/css" /> 
    
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
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" AsyncPostBackTimeout="360000" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix" id="divTitle" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">EXPORT TO EXCEL</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divCriteria" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                            <div style="margin-top:10px;">
                                <table style="width:820px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th> 
                                    <th>CATEGORY</th>
                                    <th>TYPE</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="130px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="130px" Height="31px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>  
                                    <td><asp:DropDownList ID="ddCategory" runat="server" Height="28px" Width="250px" Font-Size="14px" class="form-control" /></td>  
                                    <td>
                                        <asp:DropDownList ID="ddType" runat="server" Width="150px" Height="28px" class="form-control">
                                            <asp:ListItem Text="ALL" Value="ALL" />
                                            <asp:ListItem Text="FOR SENDING" Value="FOR SENDING" />
                                            <asp:ListItem Text="FOR RESEND" Value="FOR RESEND" />
                                            <asp:ListItem Text="ALL APPROVED" Value="ALL APPROVED" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="EXPORT TO EXCEL" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" OnClientClick="setOnLoad()" />
                                    </td>  
                                    <td>
                                        <asp:Button ID="btnExport" runat="server" Text="EXPORT TO EXCEL" Height="28px" CssClass="btn bg-blue waves-effect" OnClick="btnExport_Click" Visible="false" />
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
                        <div class="body" style="margin-top:-23px; width:1280px;">
                            
                            <asp:GridView ID="gvData" runat="server" Visible="false"
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          OnRowDataBound="gvData_OnRowDataBound"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="RFQNO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("RhRequester") %>' />
                                                <asp:Label ID="lblTransactionDate" runat="server" Visible="false" Text='<%# Eval("RhTransactionDate") %>' />                                                
                                                <asp:Label ID="lblStatDivManager" runat="server" Visible="false" Text='<%# Eval("StatDivManager") %>' />
                                                <asp:Label ID="lblRFQNo" runat="server" Width="130px" Text='<%# Eval("RhRfqNo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Width="180px" Text='<%# Eval("RdDescription") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecsDrawing" runat="server" Width="180px" Text='<%# Eval("RdSpecs") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAKER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Width="152px" Text='<%# Eval("RdMaker") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="CATEGORY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryName" runat="server" Width="150px" Text='<%# Eval("RhCategory") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatAll" runat="server" Width="200px" Text='<%# Eval("GroupBySupplierResponse") %>' Font-Bold="true" ForeColor="White" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                               
                                        
                                    </Columns>

                            </asp:GridView>       
                            
                            <asp:GridView ID="gvExport" runat="server"
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="RFQNO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("RhRequester") %>' />
                                                <asp:Label ID="lblRFQNo" runat="server" Width="130px" Text='<%# Eval("RhRfqNo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("RdDescription") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecsDrawing" runat="server" Text='<%# Eval("RdSpecs") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAKER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Text='<%# Eval("RdMaker") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="CATEGORY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("RhCategory") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("GroupBySupplierResponse") %>' Font-Bold="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="REQUESTED DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRequestedDate" runat="server" Text='<%# Eval("RhTransactionDate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        
                                        <asp:TemplateField HeaderText="SEND DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSendDate" runat="server" Text='<%# Eval("SendDate") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        
                                        <asp:TemplateField HeaderText="HAS SUPPLIER RESPONSE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHasSupplierResponse" runat="server" Text='<%# Eval("SupplierResponded") %>' />
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
