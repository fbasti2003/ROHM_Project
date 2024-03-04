<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_PO_Reporting.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_PO_Reporting" MasterPageFile="~/Sofra.Master" %>

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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SRF PULLOUT REPORT FORM</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:900px;">
                            <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT DATE RANGE YOU WANT TO SEARCH</p>
                            <div style="margin-top:10px;">
                                <table style="width:850px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>FROM</th>
                                    <th>TO</th>     
                                    <th>PULLOUT TYPE</th>                            
                                    <th style="color:White;">DUMMY</th>
                                    <th style="color:White;">DUMMY</th>
                                  </tr>
                                  <tr>
                                    <td><asp:TextBox ID="txtFrom" runat="server" Width="160px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="160px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>        
                                    <td>
                                        <asp:DropDownList ID="ddPullOutType" runat="server" Width="300px" Height="28px" OnSelectedIndexChanged="ddPullOutType_Change" class="form-control" AutoPostBack="true" required>
                                            <asp:ListItem Text="" Value="" />
                                            <asp:ListItem Text="CONTAINER TUBES" Value="CONTAINER TUBES" />
                                            <asp:ListItem Text="IC TRAYS" Value="IC TRAYS" />
                                            <asp:ListItem Text="EMPTY BLACK TRAY" Value="EMPTY BLACK TRAY" />
                                            <asp:ListItem Text="DANPLA BOX" Value="DANPLA BOX" />
                                            <asp:ListItem Text="ROBUST REEL" Value="ROBUST REEL" />
                                        </asp:DropDownList>
                                    </td>                            
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />                                        
                                    </td>  
                                    <td>
                                        <asp:Button ID="btnDownload" runat="server" Text="DOWNLOAD" Height="28px" CssClass="btn bg-purple waves-effect" OnClick="btnDownload_Click" />
                                    </td>                        
                                  </tr>
                                </table>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>   
            
            <div class="row clearfix" id="divContainerTube" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:65px; width:1250px;">                        
                            
                            <asp:GridView ID="gvContainerTube" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' />                                            
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNO" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCTRLNo" runat="server" Height="15px" Width="150px" Text='<%# Eval("Ctrlno") %>' Font-Bold="true" />                                          
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SPECIFICATION" HeaderStyle-Width="350px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("Specification") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="WEIGHT OF BOX" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeightOfBox" runat="server" Text='<%# Eval("WeightOfBox") %>' />                                                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="NO. OF BOXES" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate> 
                                            <asp:Label ID="lblNowOfBoxes" runat="server" Text='<%# Eval("NoOfBoxes") %>' />                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="GROSS WEIGHT (KG)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrossWeight" runat="server" Text='<%# Eval("GrossWeight") %>' />                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="NET WEIGHT (KG)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetWeight" runat="server" Width="120px" Text='<%# Eval("NetWeight") %>' />                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="MULTIPLIER (PCS) FORMULA" HeaderStyle-Width="280px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMultiplier" runat="server" Text='<%# Eval("Multiplier") %>' />                                               
                                        </ItemTemplate>                                                                          
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="QUANTITY (PCS)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate> 
                                            <asp:Label id="lblQuantity" runat="server" Width="120px" Text='<%# Eval("Quantity") %>' />                    
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("Head_Requester") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                 <Columns>
                                    <asp:TemplateField HeaderText="TRANSACTION DATE" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("Head_TransactionDate") %>' />                                                 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divICTrays" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:65px; width:1240px;">                        
                            
                            <asp:GridView ID="gvICTrays" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' />
                                            <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RefId") %>' Visible="false" />                                                                                        
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                 <Columns>
                                    <asp:TemplateField HeaderText="CTRLNO" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCTRLNo" runat="server" Height="15px" Width="150px" Text='<%# Eval("Ctrlno") %>' Font-Bold="true" />                                                   
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SPECIFICATION" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("Specification") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="BOX TYPE" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblBoxType" runat="server" Text='<%# Eval("BoxType") %>' />                                                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SIZE" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>' />                                                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="NO. OF BOXES" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate> 
                                            <asp:Label ID="lblNoOfBoxes" runat="server" Text='<%# Eval("NoOfBoxes") %>' />                      
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
                                    <asp:TemplateField HeaderText="QUANTITY (PCS)" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Width="120px" Text='<%# Eval("Quantity") %>' />                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                 <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("Head_Requester") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                 <Columns>
                                    <asp:TemplateField HeaderText="TRANSACTION DATE" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("Head_TransactionDate") %>' />                                                 
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                            
                        </div>
                    </div>
                </div>
            </div>  
            
            
            <div class="row clearfix" id="divOthers" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:65px; width:1075px;">                        
                            
                            <asp:GridView ID="gvOthers" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' />
                                            <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RefId") %>' Visible="false" /> 
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="CTRLNO" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCTRLNo" runat="server" Height="15px" Width="150px" Text='<%# Eval("Ctrlno") %>' Font-Bold="true" />                                                   
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SPECIFICATION" HeaderStyle-Width="390px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("Specification") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>                               
                                <Columns>
                                    <asp:TemplateField HeaderText="QUANTITY (PCS)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />                           
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SRF ITEM NAME" HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRFItemName" runat="server" Text='<%# Eval("Head_Type") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REQUESTER" HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequester" runat="server" Text='<%# Eval("Head_Requester") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                 <Columns>
                                    <asp:TemplateField HeaderText="TRANSACTION DATE" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("Head_TransactionDate") %>' />                                                 
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
        <asp:PostBackTrigger ControlID = "btnDownload" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>





