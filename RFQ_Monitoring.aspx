<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_Monitoring.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_Monitoring" MasterPageFile="~/Sofra.Master" %>


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
    
    <script type="text/javascript">
        function setOnLoad() {
            document.getElementById('<%=divOpacity.ClientID %>').style.opacity = "0.5";
            document.getElementById('<%=divLoader.ClientID %>').style.display = "block"; 
        }

    </script>
 
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        
        <div id="divOpacity" runat="server" class="container-fluid" style="margin-top:-70px; margin-left:-320px; opacity:0.1; width:1280px;"> 
        
        <div class="container-fluid" style="margin-top:-70px; margin-left:-15px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix" style="margin-top:90px;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">REQUEST FOR QUOTATION MONITORING</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                            <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT DATE RANGE YOU WANT TO SEARCH</p>
                            <div style="margin-top:10px;">
                                <table style="width:1000px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th>
                                    <th>STATUS</th> 
                                    <th>RFQNO TO SEARCH</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="150px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="150px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td> 
                                    <td><asp:DropDownList ID="ddStatus" runat="server" Width="350px" Font-Size="14px" Height="28px" class="form-control">
                                            <asp:ListItem Text="ALL" Value="ALL" />
                                            <asp:ListItem Text="FOR PRODUCTION MANAGER APPROVAL" Value="#f44336" />
                                            <asp:ListItem Text="FOR BUYER APPROVAL" Value="#9C27B0" />
                                            <asp:ListItem Text="QUOTATION SENT TO SUPPLIER(S)" Value="#673AB7" />
                                            <asp:ListItem Text="SUPPLIER RESPONDED / FOR BUYER APPROVAL" Value="#009688" />
                                            <asp:ListItem Text="FOR PURCHASING INCHARGE APPROVAL" Value="#8BC34A" />
                                            <asp:ListItem Text="FOR PURCHASING DEPARTMENT MANAGER APPROVAL" Value="#CDDC39" />
                                            <asp:ListItem Text="FOR PURCHASING DIVISION MANAGER APPROVAL" Value="#E91E63" />
                                            <asp:ListItem Text="APPROVED" Value="#00C851" />
                                            <asp:ListItem Text="DISAPPROVED" Value="#ffbb33" />
                                        </asp:DropDownList>
                                    </td>  
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="250px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" OnClientClick="setOnLoad()" />
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
                            
                            <div id="divLoader" runat="server" style="display:none; width:1280px;">
                                <p style="font-weight:bold; font-size:14px; color:Black; margin-left:30%;">LOADING DATA... PLEASE WAIT...</p>
                            </div>
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="170px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="170px" Text='<%# Eval("Rfqno") %>'  />     
                                            <asp:Label ID="lblRequester" runat="server" Visible="false" Text='<%# Eval("Requester") %>'  />       
                                            <asp:Label ID="lblDepartmentCode" runat="server" Visible="false" Text='<%# Eval("RhDepartmentCode") %>' />                                     
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="TRANSACTION DATE" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>   
                                            <asp:Label ID="lblTransDate" runat="server" Height="15px" Width="130px" Text='<%# Eval("TransactionDate") %>'  />                                         
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="APPROVED DATE" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>   
                                            <asp:Label ID="lblApprovedDate" runat="server" Height="15px" Width="130px" Text='<%# Eval("ApprovedDate") %>'  />                                         
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' Width="200px" Font-Size="11px" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="280px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatAll" runat="server" Text='<%# Eval("StatAll") %>' Width="280px" Font-Size="11px" ForeColor="White" /> 
                                            <asp:Label ID="lblStatColor" runat="server" Text='<%# Eval("CssColorCode") %>' Visible="false" />                                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="BUYER NOTES" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchasingRemarks" runat="server" Width="180px" Font-Size="11px" Text='<%# Eval("PurchasingRemarks") %>' />                                           
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblView" runat="server" Text="DETAILS" CommandName="lblView_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /> <br />
                                            <asp:LinkButton ID="lbViewInOtherTab" runat="server" Text="VIEW IN NEW TAB" CommandName="lbViewInOtherTab_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>   
                                
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            
                                            <tr>
                                                <td colspan="100%" style="background-color:White;">
                                                    <div style="margin-left:7px;">
                                                    <asp:GridView ID="gvResponseProd" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                        HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"         
                                                                        HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" Visible="false" OnRowCommand="gvResponseProd_Command">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="detailsNum" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="20px" Font-Size="11px" />
                                                                    <asp:Label ID="lblRFQNoProd" runat="server" Text='<%# Eval("RdRfqNo") %>' Visible="false" />  
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>                                                        
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescriptionProd" runat="server" Text='<%# Eval("RdDescription") %>' Width="350px" Height="20px" Font-Size="11px"/>                                                                        
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SPECIFICATIONS" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="true">                                                                
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSpecsProd" runat="server" Text='<%# Eval("RdSpecs") %>' Width="350px" Height="20px" Font-Size="11px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>   
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="MAKER" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMakerProd" runat="server" Text='<%# Eval("RdMaker") %>' Height="20px" Width="200px" Font-Size="11px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQTYProd" runat="server" Text='<%# Eval("RdQuantity") %>' Height="20px" Width="100px" Font-Size="11px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate> 
                                                                    <asp:TextBox ID="txtUOMProd" runat="server" Text='<%# Eval("RdUOMDesc") %>'  Height="20px" Width="70px" Font-Size="11px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>                                                                                                                
                                                        
                                                       
                                                    </asp:GridView>
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
    
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnSubmit" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>