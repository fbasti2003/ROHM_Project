<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PLM_PermitEntry.aspx.cs" Inherits="REPI_PUR_SOFRA.PLM_PermitEntry" MasterPageFile="~/Sofra.Master" %>

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
        
        function StartEditing()
        {
            document.getElementById("txtName").style.display = "block";            
            document.getElementById("lblName").style.display = "none";            
        }
                
        
    </script>
    
    <script type="text/javascript">
        $(function () {
            $("[id*=txtExpirationDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
        
        $(function () {
            $("[id*=txtIssuedDate]").datepicker({
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
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px;">
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card">                        
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">PERMIT / CERTIFICATION / TEST RESULT (NEW ENTRY)</p>
                        </div>                        
                    </div>
                </div>
            </div>
            <div class="body">
                <div class="table-responsive" style="margin-top:-45px;">                   
                    
                    <div class="tab-content" style="margin-top:10px;">
                        
                        <div style="margin-left:7px; margin-bottom:20px;">
                            <table style="width:1180px; color:Gray;">
                              <tr>
                                <th>ISSUED DATE</th>
                                <th>&nbsp;</th>  
                                <th>EXPIRATION DATE</th>                                                                  
                                <th>&nbsp;</th>                               
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtIssuedDate" runat="server" Width="320px" Height="22px" required /></td>
                                <td><p style="color:Maroon;">Ex: 01/25/2020 <b>(mm/dd/yyyy)</b></p></td>
                                <td><asp:TextBox ID="txtExpirationDate" runat="server" Width="320px" Height="22px" required/></td>                                                            
                                <td><p style="color:Maroon;">Ex: 01/25/2020 <b>(mm/dd/yyyy)</b></p></td>
                              </tr>
                            </table>                            
                            <table style="width:980px; color:Gray;">
                              <tr>
                                <th colspan="2">PERMIT NAME</th>                              
                              </tr>
                              <tr>
                                <td colspan="2"><asp:DropDownList ID="ddPermitName" runat="server" Width="1180px" Height="22px" AutoPostBack="true" onselectedindexchanged="ddPermitName_SelectedIndexChanged" /></td>
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
                            <table style="width:1187px; color:Gray;">
                              <tr>
                                <th>PROCESSOR NAME</th>
                                <th>SAFETY</th>    
                                <th>&nbsp;</th>    
                                <th>&nbsp;</th>                               
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtProcessorName" runat="server" Width="590px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtSafety" runat="server" Width="590px" Height="22px" /></td>
                                <td>&nbsp</td>
                                <td>&nbsp;</td>
                              </tr>
                            </table>                                                        
                            <table style="width:1187px; color:Gray;">
                              <tr>
                                <th>ATTACHMENT 1</th>
                                <th>ATTACHMENT 2</th>    
                                <th>ATTACHMENT 3</th>    
                                <th>&nbsp;</th>                               
                              </tr>
                              <tr>
                                <td><asp:FileUpload ID="fu1" runat="server" Width="391px" EnableViewState="true" /></td>
                                <td><asp:FileUpload ID="fu2" runat="server" Width="391px" EnableViewState="true" /></td>
                                <td><asp:FileUpload ID="fu3" runat="server" Width="391px" EnableViewState="true" /></td>
                                <td>&nbsp;</td>
                              </tr>
                            </table>
                            <table style="width:1187px; color:Gray; margin-top:-18px;">
                              <tr>
                                <th>&nbsp;</th>
                                <th>&nbsp;</th>    
                                <th>&nbsp;</th>    
                                <th>&nbsp;</th>                               
                              </tr>
                              <tr>
                                <td><asp:LinkButton ID="lbAttachment1" runat="server" Width="391px" OnClick="lbAttachment1_OnClick" /></td>
                                <td><asp:LinkButton ID="lbAttachment2" runat="server" Width="391px" OnClick="lbAttachment2_OnClick" /></td>
                                <td><asp:LinkButton ID="lbAttachment3" runat="server" Width="391px" OnClick="lbAttachment3_OnClick" /></td>
                                <td>&nbsp;</td>
                              </tr>
                            </table>
                            <table style="width:980px; color:Gray;">
                              <tr>
                                <th colspan="2">SEND NOTIFICATION TO?</th>                              
                              </tr>
                              <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="55px" ItemStyle-HorizontalAlign="Center" > 
                                                <ItemTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                            </td>
                                                        </tr>
                                                    </table>                                                                                                                                    
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>                               
                                        <Columns>
                                            <asp:TemplateField HeaderText="SUPPLIER / BROKER / BUYER" HeaderStyle-Width="733px" ItemStyle-HorizontalAlign="Left" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("SupplierName") %>' Height="15px" Width="733px" />  
                                                    <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RefId") %>' Visible="false" />  
                                                    <asp:Label ID="lblSelected" runat="server" Text='<%# Eval("SupplierSelected") %>' Visible="false" />                                             
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>
                                        <Columns>
                                            <asp:TemplateField HeaderText="EMAIL" HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Left" > 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("SupplierEmailAddress") %>' Height="15px" Width="400px" />                                               
                                                </ItemTemplate>                                
                                            </asp:TemplateField>
                                        </Columns>                                        
                                                                     
                                    </asp:GridView>
                                </td>
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
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnSubmit" />
    </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>
