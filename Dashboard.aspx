<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="REPI_PUR_SOFRA.Dashboard" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
       
    <script src="plugins/sweetalert/sweetalert.min.js" type="text/javascript"></script>
    
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
        
        function WarningMessage(msg) {
            swal({
                title: 'WARNING!',
                text: msg,
                type: 'warning'
            });
        }                
        
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
        
        function showDialog2()
        {
            $('.hover_bkgr_fricc2').show();
        }
        
        function hideDialog2()
        {
            $('.hover_bkgr_fricc2').hide();
        }
        
        function openWarehouseReceiving(type,pastMonth,pezaNonPeza,totalPezaNonPeza)
        {
            window.open('SRF_Warehouse2.aspx?type=' + type + '&pastMonth=' + pastMonth + '&pezaNonPeza=' + pezaNonPeza + '&totalPezaNonPeza=' + totalPezaNonPeza, '_blank');
        }
        
    </script>
    

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
        /* Popup box BEGIN */
        .hover_bkgr_fricc2{
            background:rgba(0,0,0,.4);
            cursor:pointer;
            display:none;
            height:100%;
            position:fixed;
            text-align:center;
            top:0;
            width:100%;
            left:0px;
        }
        .hover_bkgr_fricc2 .helper{
            display:inline-block;
            height:100%;
            vertical-align:middle;
        }
        .hover_bkgr_fricc2 > div {
            background-color: #fff;
            display: inline-block;
            height: auto;
            width: 1300px;
            min-height: 100px;
            vertical-align: middle;
            position: relative;
            border-radius: 8px;
            padding: 15px 15px;
            margin-top: 0px;
        }
        .popupCloseButton2 {
            background-color: #fff;
            border: 3px solid #999;
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
        .popupCloseButton2:hover {
            background-color: #ccc;
        }
        .trigger_popup_fricc2 {
            cursor: pointer;
            font-size: 20px;
            margin: 20px;
            display: inline-block;
            font-weight: bold;
        }
        
        /* Popup box BEGIN */
    </style>
    
    <style type="text/css">
        .WordWrap
        {
            width: 100%;
            word-break: break-all;
        }
        .WordBreak
        {
            width: 100px;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        
        
        table.customTable {
          width: 100%;
          background-color: #FFFFFF;
          border-collapse: collapse;
          border-width: 1px;
          border-color: #7EA8F8;
          border-style: solid;
          color: #000000;
        }

        table.customTable td, table.customTable th {
          border-width: 1px;
          border-color: #7EA8F8;
          border-style: solid;
          padding: 5px;
        }

        table.customTable thead {
          background-color: #7EA8F8;
        }


    </style>
    
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1300px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:16px; font-weight:bold;">MY DASHBOARD</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1300px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:470px; width:1280px;">
                            
                            <div class="row clearfix">
                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="divMyApproval" runat="server" style="padding-left:15px; padding-top:10px; display:none;">
                                
                                    <div style="box-shadow: 6px 5px 11px 0px rgba(0,0,0,0.75);">
                                        <div style="height:20px; width:420px; background-color:#00BCD4; color:White; padding-left:5px; ">
                                            <p style="margin-top:-5px; font-family:Tahoma;">
                                                MY FOR APPROVALS
                                            </p>                            
                                        </div>
                                        <div style="width:420px; height:150px; background-color:White; font-family:Tahoma; padding:3px 3px 3px 3px; overflow-y:auto;">
                                            <asp:GridView ID="gvForApproval" runat="server" AutoGenerateColumns="false" ShowHeader="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" OnRowDataBound="gvForApproval_OnRowDataBound" OnRowCommand="gvForApproval_Command"                                                                                                             
                                                                     EmptyDataText="NO FOR APPROVAL YET">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="TRANSACTION NAME" HeaderStyle-Width="330px" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionName" runat="server" Height="15px" Width="330px" Text='<%# Eval("TransactionName") %>'  />                                                
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO. OF FOR APPROVAL" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" > 
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoOfForApproval" runat="server" Text='<%# Eval("ForApprovalCount") %>' Height="15px" Width="50px" Font-Size="11px" Visible="false" /> 
                                                            <asp:LinkButton ID="linkNoOfForApproval" runat="server" Text='<%# Eval("ForApprovalCount") %>' Height="15px" Width="50px" Font-Size="11px" CommandName="linkNoOfForApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                              
                                                        </ItemTemplate>                                
                                                    </asp:TemplateField>
                                                </Columns>                                                                                                                                               
                                                
                                            </asp:GridView>
                                        </div>
                                    </div>
                                                                                                            
                                </div> 
                                
                                <div class="col-lg-8 col-md-4 col-sm-6 col-xs-12" id="divTopResponded" runat="server" style="padding-left:30px; padding-top:10px; display:none; ">
                                
                                    <div style="box-shadow: 6px 5px 11px 0px rgba(0,0,0,0.75);">
                                        <div style="height:20px; width:800px; background-color:#00BCD4; color:White; padding-left:5px;">
                                            <p style="margin-top:-5px; font-family:Tahoma;">
                                                TODAY'S RFQ TOP RESPONDED SUPPLIERS
                                            </p>                            
                                        </div>
                                        <div style="width:780px; height:150px; background-color:White; font-family:Tahoma; padding-left:5px; overflow-y:auto;">
                                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_Command"                                                                                                               
                                                                     EmptyDataText="WAITING FOR TODAY'S SUPPLIER RESPONSE" ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SUPPLIERS" HeaderStyle-Width="450px" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSuppliers" runat="server" Height="15px" Width="525px" Text='<%# Eval("TodayTopSupplier_SupplierName") %>'  /> 
                                                            <asp:Label ID="lblSupplierId" runat="server" Text='<%# Eval("TodayTopSupplier_SupplierId") %>' Visible=false />                                               
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO. OF RESPONSE" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" > 
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoOfResponse" runat="server" Text='<%# Eval("TodayTopSupplier_SupplierResponseCount") %>' Height="15px" Width="120px" Font-Size="11px" />                                              
                                                        </ItemTemplate>                                
                                                    </asp:TemplateField>
                                                </Columns>  
                                                <Columns>
                                                    <asp:TemplateField HeaderText="DETAILS" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbDetails" runat="server" Text="VIEW DETAILS" Height="15px" Width="100px" Font-Size="11px" CommandName="lbDetails_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                             
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>                                                                                                                                             
                                                
                                            </asp:GridView>
                                        </div>
                                    </div>
                                                                                                            
                                </div>
                                
                                <div class="col-lg-9 col-md-4 col-sm-6 col-xs-12" id="divOtherBuyersRFQ" runat="server" style="padding-left:15px; padding-top:10px; display:none; ">
                                
                                    <div>
                                        <div style="height:20px; width:1240px; background-color:#00BCD4; color:White; padding-left:5px; padding-bottom:5px;">
                                            <p style="margin-top:-5px; font-family:Tahoma; text-align:center;">
                                                ITEMS OF OTHER BUYERS
                                            </p>                            
                                        </div>
                                        <div style="width:1240px; min-height:150px; background-color:White; font-family:Tahoma; padding-left:0px; padding-top:5px; overflow-y:auto;">
                                            <asp:GridView ID="gvForOtherBuyers" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvForOtherBuyers_OnRowCommand"                                                                                                                 
                                                                     ShowHeader="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CATEGORY" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("OtherBuyer_CategoryName") %>'  />    
                                                            <asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("OtherBuyer_Category") %>' Visible="false"  />                                              
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="RFQ FOR APPROVAL" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#f44336">
                                                        <ItemTemplate> 
                                                            <asp:LinkButton ID="lbForApproval" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_ForApproval") %>' CommandName="lbForApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>   
                                                 <Columns>
                                                    <asp:TemplateField HeaderText="RFQ FOR SENDING" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#f44336" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbForSending" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_ForSending") %>' CommandName="lbForSending_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                                  
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns> 
                                                 
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CRF FOR APPROVAL" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#00C851" >
                                                        <ItemTemplate> 
                                                            <asp:LinkButton ID="lbCRFApproval" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_CRFApproval") %>' CommandName="lbCRFApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="CRF FOR SENDING" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#00C851" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbCRFSending" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_CRFSending") %>' CommandName="lbCRFSending_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                                  
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns> 
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="DRF FOR APPROVAL" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffbb33" >
                                                        <ItemTemplate> 
                                                            <asp:LinkButton ID="lbDRFApproval" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_DRFApproval") %>' CommandName="lbDRFApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="DRF FOR SENDING" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffbb33" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbDRFSending" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_DRFSending") %>' CommandName="lbDRFSending_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                                  
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns> 
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="URF FOR APPROVAL" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" >
                                                        <ItemTemplate> 
                                                            <asp:LinkButton ID="lbURFApproval" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_URFApproval") %>' CommandName="lbURFApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="URF FOR SENDING" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbURFSending" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_URFSending") %>' CommandName="lbURFSending_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                                  
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns> 
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SRF FOR APPROVAL" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#f44336" >
                                                        <ItemTemplate> 
                                                            <asp:LinkButton ID="lbSRFApproval" runat="server" Height="15px" Width="80px" Text='<%# Eval("OtherBuyer_SRFApproval") %>' CommandName="lbSRFApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="PROFORMA FOR APPROVAL" HeaderStyle-Width="102px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#00C851" >
                                                        <ItemTemplate> 
                                                            <asp:LinkButton ID="lbProformaApproval" runat="server" Height="15px" Width="102px" Text='<%# Eval("OtherBuyer_PROFORMAApproval") %>' CommandName="lbProformaApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>
                                                
                                                <Columns>
                                                    <asp:TemplateField HeaderText="EQUIPMENT REQUEST FOR APPROVAL" HeaderStyle-Width="106px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffbb33" >
                                                        <ItemTemplate> 
                                                            <asp:LinkButton ID="lbERFOApproval" runat="server" Height="15px" Width="104px" Text='<%# Eval("OtherBuyer_ERFOApproval") %>' CommandName="lbERFOApproval_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>                                                   
                                                                                                                                                                                         
                                                
                                            </asp:GridView>
                                        </div>
                                    </div>
                                                                                                            
                                </div>
                                
                                                                                              
                            </div>
                            
                            
                            <div class="row clearfix" id="divWarehouse" runat="server" style="display:none;">
                            
                                <div style="height:20px; width:1256px; background-color:#00BCD4; color:White; padding-left:5px; padding-bottom:10px; ">
                                    <p style="font-family:Tahoma; text-align:center; ">
                                        SRF FARM IN/OUT MONITORING TABLE
                                    </p>                            
                                </div>
                                
                                <div class="col-lg-6 col-md-4 col-sm-6 col-xs-12" style="padding-top:10px;">
                                
                                    <div>
                                        
                                        <div style="width:1200px; height:510px; background-color:White; font-family:Tahoma; padding:3px 3px 1px 3px;">
                                            
                                            <div style="width:1225px; height:300px;">
                                                
                                                
                                                <table class="customTable">
                                                  <thead>
                                                    <tr style="color:White;">
                                                      <th style="background-color:White; color:Black; border-color:Black; font-size:16px;">PEZA SUPPLIERS</th>
                                                      <th colspan="3" style="background-color:#00C851; border-color:Black; text-align:center;">Past 1 month</th>
                                                      <th colspan="3" style="background-color:#FFBB33; border-color:Black; text-align:center;">Past 2 months</th>
                                                      <th colspan="3" style="background-color:Red; border-color:Black; text-align:center;">Past 3 months & above</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody>
                                                    <tr style="font-weight:bold; text-align:center;">
                                                      <td>STATUS</td>
                                                      <td style="width:110px;">8106/8112 count</td>
                                                      <td style="width:110px;">Pull out Qty</td>
                                                      <td style="width:110px;">Remaining Qty</td>
                                                      <td style="width:110px;">8106/8112 count</td>
                                                      <td style="width:110px;">Pull out Qty</td>
                                                      <td style="width:110px;">Remaining Qty</td>
                                                      <td style="width:110px;">8106/8112 count</td>
                                                      <td style="width:110px;">Pull out Qty</td>
                                                      <td style="width:110px;">Remaining Qty</td>
                                                    </tr>
                                                    <tr>
                                                      <td>Approved/Waiting for delivery</td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb1" runat="server" OnClientClick="openWarehouseReceiving(1,1,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb2" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb3" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb4" runat="server" OnClientClick="openWarehouseReceiving(1,2,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb5" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb6" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb7" runat="server" OnClientClick="openWarehouseReceiving(1,3,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb8" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb9" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                      <td>Delivered w/ pending items</td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb10" runat="server" OnClientClick="openWarehouseReceiving(2,1,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb11" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb12" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb13" runat="server" OnClientClick="openWarehouseReceiving(2,2,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb14" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb15" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb16" runat="server" OnClientClick="openWarehouseReceiving(2,3,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb17" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb18" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                      <td>Delivered</td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb19" runat="server" OnClientClick="openWarehouseReceiving(3,1,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb20" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb21" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb22" runat="server" OnClientClick="openWarehouseReceiving(3,2,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb23" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb24" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb25" runat="server" OnClientClick="openWarehouseReceiving(3,3,1,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb26" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb27" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                      <td>TOTAL</td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb28" runat="server" OnClientClick="openWarehouseReceiving(0,1,0,1);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb29" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb30" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb31" runat="server" OnClientClick="openWarehouseReceiving(0,2,0,1);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb32" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb33" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb34" runat="server" OnClientClick="openWarehouseReceiving(0,3,0,1);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb35" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb36" runat="server" /></td>
                                                    </tr>
                                                  </tbody>
                                                </table>
                                                
                                                <br />
                                                PEZA TOTAL NUMBER OF REQUEST : <asp:LinkButton ID="lbPezaTotal1" runat="server" Font-Bold="true" OnClientClick="openWarehouseReceiving(4,4,0,1);" /> / <asp:LinkButton ID="lbPezaTotal2" runat="server" Font-Bold="true" OnClientClick="openWarehouseReceiving(44,44,0,1);"  />
                                                <br /><br />
                                                
                                                <table class="customTable">
                                                  <thead>
                                                    <tr style="color:White;">
                                                      <th style="background-color:White; color:Black; border-color:Black; font-size:16px;">NON-PEZA SUPPLIERS</th>
                                                      <th colspan="3" style="background-color:#00C851; border-color:Black; text-align:center;">Past 1 month</th>
                                                      <th colspan="3" style="background-color:#FFBB33; border-color:Black; text-align:center;">Past 2 months</th>
                                                      <th colspan="3" style="background-color:Red; border-color:Black; text-align:center;">Past 3 months & above</th>
                                                    </tr>
                                                  </thead>
                                                  <tbody>
                                                    <tr style="font-weight:bold; text-align:center;">
                                                      <td>STATUS</td>
                                                      <td style="width:110px;">8106/8112 count</td>
                                                      <td style="width:110px;">Pull out Qty</td>
                                                      <td style="width:110px;">Remaining Qty</td>
                                                      <td style="width:110px;">8106/8112 count</td>
                                                      <td style="width:110px;">Pull out Qty</td>
                                                      <td style="width:110px;">Remaining Qty</td>
                                                      <td style="width:110px;">8106/8112 count</td>
                                                      <td style="width:110px;">Pull out Qty</td>
                                                      <td style="width:110px;">Remaining Qty</td>
                                                    </tr>
                                                    <tr>
                                                      <td>Approved/Waiting for delivery</td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb37" runat="server" OnClientClick="openWarehouseReceiving(1,1,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb38" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb39" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb40" runat="server" OnClientClick="openWarehouseReceiving(1,2,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb41" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb42" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb43" runat="server" OnClientClick="openWarehouseReceiving(1,3,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb44" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb45" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                      <td>Delivered w/ pending items</td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb46" runat="server" OnClientClick="openWarehouseReceiving(2,1,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb47" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb48" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb49" runat="server" OnClientClick="openWarehouseReceiving(2,2,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb50" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb51" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb52" runat="server" OnClientClick="openWarehouseReceiving(2,3,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb53" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb54" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                      <td>Delivered</td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb55" runat="server" OnClientClick="openWarehouseReceiving(3,1,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb56" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb57" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb58" runat="server" OnClientClick="openWarehouseReceiving(3,2,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb59" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb60" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb61" runat="server" OnClientClick="openWarehouseReceiving(3,3,2,0);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb62" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb63" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                      <td>TOTAL</td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb64" runat="server" OnClientClick="openWarehouseReceiving(0,1,0,2);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb65" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb66" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb67" runat="server" OnClientClick="openWarehouseReceiving(0,2,0,2);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb68" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb69" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:LinkButton ID="lb70" runat="server" OnClientClick="openWarehouseReceiving(0,3,0,2);" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb71" runat="server" /></td>
                                                      <td style="text-align:right;"><asp:Label ID="lb72" runat="server" /></td>
                                                    </tr>
                                                  </tbody>
                                                </table>
                                                
                                                <br />
                                                NON-PEZA TOTAL NUMBER OF REQUEST : <asp:LinkButton ID="lbNonPezaTotal1" runat="server" Font-Bold="true" OnClientClick="openWarehouseReceiving(5,5,0,2);" /> / <asp:LinkButton ID="lbNonPezaTotal2" runat="server" Font-Bold="true" OnClientClick="openWarehouseReceiving(55,55,0,2);" />
                                                <br /><br />
                                                
                                                
                                            </div>
                                            
                                        </div>                                                                                
                                        
                                    </div>
                                                                                                            
                                </div>   
                            
                            </div>
                            
                            
                            
                            <div class="row clearfix" id="divThisWeekRequest" runat="server" style="display:none;">
                            
                            <div style="height:20px; width:1256px; background-color:#00BCD4; color:White; padding-left:5px; padding-bottom:10px; ">
                                <p style="font-family:Tahoma; text-align:center; ">
                                    THIS WEEK REQUEST GRAPHICAL PRESENTATION (SUNDAY - SATURDAY)
                                </p>                            
                            </div>
                                        
                                <div class="col-lg-6 col-md-4 col-sm-6 col-xs-12" style="padding-top:10px;">
                                
                                    <div>
                                        
                                        <div style="width:330px; height:285px; background-color:White; font-family:Tahoma; padding:3px 3px 1px 3px;">
                                            
                                            <asp:GridView ID="gvGraphicalPresentation" runat="server" AutoGenerateColumns="false" BorderStyle="None" ShowHeader="false" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="TRANSACTION NAME" HeaderStyle-Width="350px" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="None" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionName" runat="server" Height="0px" Width="350px" Font-Bold="true" Font-Size="11px" Text='<%# Eval("TransactionName") %>'  /> 
                                                            <asp:Label ID="lblRequest" runat="server" Text='<%# Eval("ThisWeek_Request") %>' Visible="false" />   
                                                            <asp:Label ID="lblPending" runat="server" Text='<%# Eval("ThisWeek_Pending") %>' Visible="false" /> 
                                                            <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("ThisWeek_Approved") %>' Visible="false" /> 
                                                            <asp:Label ID="lblDisapproved" runat="server" Text='<%# Eval("ThiwWeek_Disapproved") %>' Visible="false" /> 
                                                            
                                                            <div style="padding-bottom:10px; padding-top:10px; color:White;">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Request") %>; height:15px; font-size:10px; background-color:#00BCD4; text-align:center;">REQUEST - <%# Eval("ThisWeek_Request") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Pending") %>; height:15px; font-size:10px; background-color:#f44336; text-align:center;">PENDING - <%# Eval("ThisWeek_Pending") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Approved") %>; height:15px; font-size:10px; background-color:#00C851; text-align:center;">APPROVED - <%# Eval("ThisWeek_Approved") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Disapproved") %>; height:15px; font-size:10px; background-color:#ffbb33; text-align:center;">DISAPPROVED - <%# Eval("ThiwWeek_Disapproved") %></div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                                                                     
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>                                               
                                                
                                            </asp:GridView>
                                            
                                        </div>                                                                                
                                        
                                    </div>
                                                                                                            
                                </div>   
                                
                                
                                <div class="col-lg-6 col-md-4 col-sm-6 col-xs-12" style="padding-top:10px; margin-left:-280px;">
                                
                                    <div>
                                        
                                        <div style="width:330px; height:285px; background-color:White; font-family:Tahoma; padding:3px 3px 1px 3px;">
                                            
                                            <asp:GridView ID="gvGraphicalPresentation2" runat="server" AutoGenerateColumns="false" BorderStyle="None" ShowHeader="false" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="TRANSACTION NAME" HeaderStyle-Width="350px" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="None" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionName" runat="server" Height="0px" Width="350px" Font-Bold="true" Font-Size="11px" Text='<%# Eval("TransactionName") %>'  /> 
                                                            <asp:Label ID="lblRequest" runat="server" Text='<%# Eval("ThisWeek_Request") %>' Visible="false" />   
                                                            <asp:Label ID="lblPending" runat="server" Text='<%# Eval("ThisWeek_Pending") %>' Visible="false" /> 
                                                            <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("ThisWeek_Approved") %>' Visible="false" /> 
                                                            <asp:Label ID="lblDisapproved" runat="server" Text='<%# Eval("ThiwWeek_Disapproved") %>' Visible="false" /> 
                                                            
                                                            <div style="padding-bottom:10px; padding-top:10px; color:White;">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Request") %>; height:15px; font-size:10px; background-color:#00BCD4; text-align:center;">REQUEST - <%# Eval("ThisWeek_Request") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Pending") %>; height:15px; font-size:10px; background-color:#f44336; text-align:center;">PENDING - <%# Eval("ThisWeek_Pending") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Approved") %>; height:15px; font-size:10px; background-color:#00C851; text-align:center;">APPROVED - <%# Eval("ThisWeek_Approved") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Disapproved") %>; height:15px; font-size:10px; background-color:#ffbb33; text-align:center;">DISAPPROVED - <%# Eval("ThiwWeek_Disapproved") %></div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                                                                     
                                                        </ItemTemplate> 
                                                    </asp:TemplateField>
                                                </Columns>                                               
                                                
                                            </asp:GridView>
                                            
                                        </div>                                                                                
                                        
                                    </div>
                                                                                                            
                                </div> 
                                       
                                       
                                <div class="col-lg-6 col-md-4 col-sm-6 col-xs-12" style="padding-top:10px; margin-left:-355px;">
                                
                                    <div>
                                        
                                        <div style="width:330px; height:285px; background-color:White; font-family:Tahoma; padding:3px 3px 1px 3px;">
                                            
                                            <asp:GridView ID="gvGraphicalPresentation3" runat="server" AutoGenerateColumns="false" BorderStyle="None" ShowHeader="false" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="TRANSACTION NAME" HeaderStyle-Width="350px" ItemStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="None" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionName" runat="server" Height="0px" Width="350px" Font-Bold="true" Font-Size="11px" Text='<%# Eval("TransactionName") %>'  /> 
                                                            <asp:Label ID="lblRequest" runat="server" Text='<%# Eval("ThisWeek_Request") %>' Visible="false" />   
                                                            <asp:Label ID="lblPending" runat="server" Text='<%# Eval("ThisWeek_Pending") %>' Visible="false" /> 
                                                            <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("ThisWeek_Approved") %>' Visible="false" /> 
                                                            <asp:Label ID="lblDisapproved" runat="server" Text='<%# Eval("ThiwWeek_Disapproved") %>' Visible="false" /> 
                                                            
                                                            <div style="padding-bottom:10px; padding-top:10px; color:White;">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Request") %>; height:15px; font-size:10px; background-color:#00BCD4; text-align:center;">REQUEST - <%# Eval("ThisWeek_Request") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Pending") %>; height:15px; font-size:10px; background-color:#f44336; text-align:center;">PENDING - <%# Eval("ThisWeek_Pending") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Approved") %>; height:15px; font-size:10px; background-color:#00C851; text-align:center;">APPROVED - <%# Eval("ThisWeek_Approved") %></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:503px;">
                                                                            <div style="width:<%# Eval("Percentage_Disapproved") %>; height:15px; font-size:10px; background-color:#ffbb33; text-align:center;">DISAPPROVED - <%# Eval("ThiwWeek_Disapproved") %></div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                                                                     
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
                </div>
            </div>      
            
            
            <div class="hover_bkgr_fricc2">
                <span class="helper"></span>
                <div>
                    <table style="text-align:left;">
                        <tr>
                            <td>
                                <p style="font-size:20px; color:Red"><b>!!! SRF WAREHOUSE NO DOCUMENTS NOTIFICATION !!!</b></p>
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <p style="font-size:12px; color:Black">PLEASE CHECK BELOW ITEMS THAT SUCCESSFULLY DELIVERED BUT STILL WAITING FOR DOCUMENTS. KINDLY FOLLOW WITH THE RESPECTIVE ASSIGNEE TO AVOID LIQUIDATION PENALTIES.</p>  
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="WordWrap" style="margin-top:10px;">
                                    <asp:GridView ID="gvActualDelivery" runat="server" OnRowDataBound="gvActualDelivery_OnRowDataBound" ShowHeader="true"
                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                                      HeaderStyle-Font-Bold="false" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px">
                                        <Columns>  
                                            <asp:TemplateField HeaderText="REFID" ItemStyle-CssClass="columnSpace" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRefId" runat="server" Width="30px" Text='<%# Eval("No") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                 
                                            <asp:TemplateField HeaderText="REFID" ItemStyle-CssClass="columnSpace" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRefId" runat="server" Width="60px" Text='<%# Eval("Warehouse_DetailsRefId") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CTRLNO" ItemStyle-CssClass="columnSpace" >
                                                <ItemTemplate>
                                                    <asp:Label ID="txtCTRLNo" runat="server" Width="150px" Text='<%# Eval("Warehouse_CtrlNo") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField HeaderText="ITEM NAME" ItemStyle-CssClass="columnSpace" >
                                                <ItemTemplate>
                                                    <asp:Label ID="txtItemName" runat="server" Width="400px" Text='<%# Eval("Warehouse_ItemName") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
                                            <asp:TemplateField HeaderText="DELIVERED QTY." ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:Label ID="txtActualQty" runat="server" Width="150px" Text='<%# Eval("Warehouse_TotalActualQuantity") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="DELIVERED DATE" ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDateDelivered" runat="server" Width="150px" Text='<%# Eval("Warehouse_DeliveredDate") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="ADDED BY" ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="txtAddedBy" runat="server" Width="320px" Text='<%# Eval("Warehouse_RequesterName") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="CLOSE" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog2();"/>
                                <asp:Button ID="btnMoreItems" runat="server" Text="CLICK TO VIEW MORE ITEMS THAT HAS NO SUBMITTED DOCUMENTS" Visible="false" CssClass="btn bg-light-green waves-effect" OnClick="btnMoreItems_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            
             <div class="hover_bkgr_fricc">
                <span class="helper"></span>
                <div>
                    <p style="font-size:26px; color:Red"><b>! REMINDER !</b></p>
                    <p style="font-size:16px; color:Black">There will be a system maintenance on April 26, 2021 (9:00h~12:00h)</p>             
                    <td><asp:Button ID="btnCancel2" runat="server" Text="CLOSE" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();"/></td>
                </div>
            </div>                 
                                                                       
            
    </div>
            
    </section>
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>


