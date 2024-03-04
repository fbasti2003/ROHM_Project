<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_SendToSuppliers.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_SendToSuppliers" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 

    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" type="text/css" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" type="text/css" />
    <link href="plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/themes/all-themes.css" rel="stylesheet" type="text/css" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" type="text/css" /> 
    
    <style type="text/css">
        /* Popup box BEGIN */
        .hover_bkgr_fricc{
            background:rgba(0,0,0,.4);
            cursor:pointer;
            display:none;
            height:100%;
            position:fixed;
            text-align:center;
            top:0;
            width:100%;
            z-index:10000;
            left:-50px;
        }
        .hover_bkgr_fricc .helper{
            display:inline-block;
            height:100%;
            vertical-align:middle;
        }
        .hover_bkgr_fricc > div {
            background-color: #fff;
            box-shadow: 10px 10px 60px #555;
            display: inline-block;
            height: auto;
            max-width: 551px;
            min-height: 100px;
            vertical-align: middle;
            width: 60%;
            position: relative;
            border-radius: 8px;
            padding: 15px 5%;
        }
        .popupCloseButton {
            background-color: #fff;
            border: 3px solid #999;
            border-radius: 50px;
            cursor: pointer;
            display: inline-block;
            font-family: arial;
            font-weight: bold;
            position: absolute;
            top: -20px;
            right: -20px;
            font-size: 25px;
            line-height: 30px;
            width: 30px;
            height: 30px;
            text-align: center;
        }
        .popupCloseButton:hover {
            background-color: #ccc;
        }
        .trigger_popup_fricc {
            cursor: pointer;
            font-size: 20px;
            margin: 20px;
            display: inline-block;
            font-weight: bold;
        }
        /* Popup box BEGIN */
    </style>
    
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
        function showDialog()
        {
            $('.hover_bkgr_fricc').show();
        }
        
        function hideDialog()
        {
            $('.hover_bkgr_fricc').hide();
        }
        
    </script>
    
    <script type="text/javascript">
        window.onload = function () {
            var h = document.getElementById("<%=keep.ClientID%>");
            document.getElementById("divTest").scrollTop = h.value;
        }
      function SetDivPosition(){
        var intY = document.getElementById("divTest").scrollTop;
        var h = document.getElementById("<%=keep.ClientID%>");
        h.value = intY;
        
      }
      
      function SetPositionOnClick() {
        var h = document.getElementById("<%=keep.ClientID%>");
        document.getElementById("divTest").scrollTop = h.value;
      }
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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">RFQ - SEND TO SUPPLIERS</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                            <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT RFQ NUMBER YOU WANT TO SEND TO SUPPLIERS</p>
                            <div style="margin-top:10px;">
                                <table style="width:460px; color:Gray; font-size:12px;">
                                  <tr>
                                    <th>RFQ NUMBERS</th>
                                    <th>AGING</th>
                                  </tr>
                                  <tr>
                                    <td><asp:DropDownList ID="ddRFQNo" runat="server" Height="28px" Width="300px" Font-Size="14px" class="form-control" OnSelectedIndexChanged="ddRFQNo_SelectedIndexChanged" AutoPostBack="true" /></td>     
                                    <td><asp:TextBox ID="txtAging" runat="server" Height="28px" Width="150px" Font-Size="14px" class="form-control" Text="5" /></td>                    
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
                            
                            <asp:GridView ID="gvData" runat="server"
                                          AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="15px"
                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                          HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" >
                                    <Columns>

                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefId" runat="server" Width="70px" Text='<%# Eval("RdRefId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Width="250px" Text='<%# Eval("RdDescription") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SPECS/DRAWING NO.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecsDrawing" runat="server" Width="180px" Text='<%# Eval("RdSpecs") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MAKER">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaker" runat="server" Width="100px" Text='<%# Eval("RdMaker") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Width="40px" Text='<%# Eval("RdQuantity") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUOM" runat="server" Width="40px" Text='<%# Eval("RdUnitOfMeasure") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REMARKS">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Width="150px" Height="22px" Text='<%# Eval("RdRemarks") %>' />                                       
                                        </ItemTemplate>                   
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PURCHASING REMARKS">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPurchasingRemarks" runat="server" TextMode="MultiLine" Width="370px" Height="22px" Text='<%# Eval("RdPurchasingRemarks") %>' />                                      
                                        </ItemTemplate>                   
                                    </asp:TemplateField>
                                    
                                </Columns>

                            </asp:GridView>
                            
                            <div style="width:1210px; margin-top:5px; text-align:right;">
                                <asp:Button ID="btnUpdateRemarks" runat="server" Text="UPDATE REMARKS" CssClass="btn bg-light-blue waves-effect" OnClick="btnUpdateRemarks_Click" />
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; width:1280px;">
                           
                            <input type="hidden" id="keep" name="keep" runat="server" />                             
                            
                            <div id="divTest" onscroll="SetDivPosition()" style="overflow-y:scroll; height: 250px; width: 1200px;" >
                            
                            
                                <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                         HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px" 
                                                                         HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                     
                                                                         OnRowDataBound="gvSuppliers_OnRowDataBound" OnRowCommand="gvSuppliers_RowCommand"                                                             
                                                                         EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                                                         
                                    <Columns>
                                        <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" > 
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />  
                                                        </td>                                                    
                                                    </tr>
                                                </table>                                                                                                                                    
                                            </ItemTemplate>                                
                                        </asp:TemplateField>
                                    </Columns>  
                                                                                                         
                                    <Columns>
                                        <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="900px" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierID" runat="server" Visible="false" Text='<%# Eval("ResponseSupplierID") %>'  />
                                                <asp:Label ID="lblWithResponse" runat="server" Visible="false" Text='<%# Eval("ReponseWithResponse") %>'  />
                                                <asp:Label ID="lblResponseCount" runat="server" Visible="false" Text='<%# Eval("ResponseCount") %>'  />
                                                <asp:Label ID="lblSupplierEmail" runat="server" Visible="false" Text='<%# Eval("ResponseSupplierEmail") %>'  />                                                                                            
                                                <asp:Label ID="lblSupplierName" runat="server" Height="15px" Width="900px" Text='<%# Eval("ResponseSupplierName") %>'  />                                                                                            
                                            </ItemTemplate> 
                                        </asp:TemplateField>
                                    </Columns>
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="REGISTERED / NOT REGISTERED" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>                                                                                         
                                                <asp:Label ID="lblRegistered" runat="server" Height="15px" Width="200px" Text='<%# Eval("Registered") %>'  />                                                                                            
                                            </ItemTemplate> 
                                        </asp:TemplateField>
                                    </Columns>
                                                                                                  
                                </asp:GridView>
                            
                          
                            </div>                           
                           
                            
                            <div id="divSendingDetails" runat="server" style="margin-top:5px;">
                                <p style="font-size:12px;">SENDING DETAILS</p>
                                <asp:GridView ID="gvSentDetails" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px" 
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                                                                               
                                                                     EmptyDataText="No Record Found!" PagerStyle-CssClass="pagination-ys">
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="RFQNO" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>                                                                                           
                                                <asp:Label ID="lblRFQNo" runat="server" Height="15px" Width="147px" Text='<%# Eval("ResponseRFQNo") %>'  />                                                                                            
                                            </ItemTemplate> 
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>                                                                                           
                                                <asp:Label ID="lblSupplierName" runat="server" Height="15px" Width="350px" Text='<%# Eval("ResponseSupplierName") %>'  />                                                                                            
                                            </ItemTemplate> 
                                        </asp:TemplateField>
                                    </Columns>                                    
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblSendStatus" runat="server" Height="15px" Width="485px" Text='<%# Eval("ResponseSendStatus") %>'  />                                                                                                                                  
                                            </ItemTemplate>                                
                                        </asp:TemplateField>
                                    </Columns>                                
                                </asp:GridView>
                                <div style="margin-top:5px; font-size:11px; width:1280px;">
                                    <%= errorSendingDetails %>
                                </div>
                            </div>
                            
                        </div>                        
                    </div>
                </div>
            </div>   
                    
            
            <div class="row clearfix" id="divActionButtons" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:65px; width:1280px;">                            
                            <asp:Button ID="btnSend2" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSend2_Click" />
                            <asp:Button ID="btnReceiving" runat="server" Text="BACK TO RECEIVING FORM" CssClass="btn bg-light-green waves-effect" OnClick="btnReceiving_Click" />                            
                        </div>
                    </div>
                </div>
            </div>    
                    
            <div class="hover_bkgr_fricc">
                <span class="helper"></span>
                <div>
                    <p style="font-size:18px;"><b>System detected that you selected supplier(s) with empty or null Registered field!</b></p>
                    <p style="font-size:14px; color:Red">*** Please find time to update suppliers Registered field.</p><br /><br /> 
                    <p style="font-size:18px;"><b>Proceed anyway?</b></p>                   
                    <asp:Button ID="btnSend" runat="server" Text="YES" CssClass="btn bg-light-blue waves-effect" OnClick="btnSend_Click" />
                    <td><asp:Button ID="btnCancel2" runat="server" Text="NO" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();"/></td>
                </div>
            </div>
                                                                       
            
    </div>


    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnSend" />
        <asp:PostBackTrigger ControlID = "btnSend2" />
        <asp:PostBackTrigger ControlID = "btnReceiving" />
    </Triggers>
    
    </asp:UpdatePanel>
    
</asp:Content>

