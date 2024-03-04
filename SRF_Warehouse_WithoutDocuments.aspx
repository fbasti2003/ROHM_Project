<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_Warehouse_WithoutDocuments.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_Warehouse_WithoutDocuments" MasterPageFile="~/Sofra.Master" %>

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
    </style>
    
    
    
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
        left:0px;
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
        max-width: 400px;
        min-height: 50px;
        vertical-align: middle;
        width: 60%;
        position: relative;
        border-radius: 8px;
        padding: 15px 15px;
        text-align:center;
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
    


    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>   
    <script src="plugins/sweetalert/sweetalert.min.js" type="text/javascript"></script>
    <script src="js/pages/ui/tooltips-popovers.js" type="text/javascript"></script>  
    
    <script type="text/javascript">

        function showDialog() {
            $('.hover_bkgr_fricc').show();
        }

        function hideDialog() {
            $('.hover_bkgr_fricc').hide();
        }


    </script>
 
    
     
        
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <div class="hover_bkgr_fricc">
        <span class="helper"></span>
        <div>
            <p style="font-size:18px; color:Black; text-align:center;">DOWNLOADING... PLEASE WAIT...</p> 
        </div>
    </div>
   
        <section class="content">
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1800px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1800px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SRF WAREHOUSE RECEIVED ITEMS WITHOUT DOCUMENTS</p>
                            
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1800px;">
                    <div class="card"> 
                        <div class="header" style="height:70px; margin-top:-20px;">
                            <asp:Button ID="btnExportToExcel" runat="server" Text="EXPORT TO EXCEL" Width="200px" CssClass="btn bg-light-blue waves-effect" OnClick="btnExportToExcel_Click" OnClientClick="showDialog()" />
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1800px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:100px; width:1800px;">
                            <div style="margin-top:10px;">
                                
                                <div class="WordWrap" style="margin-top:10px;">
                                    <asp:GridView ID="gvActualDelivery" runat="server" OnRowDataBound="gvActualDelivery_OnRowDataBound" ShowHeader="true"
                                                                      AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px" RowStyle-Height="22px"
                                                                      HeaderStyle-Font-Bold="false" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" FooterStyle-Font-Size="10px">
                                        <Columns>  
                                            <asp:TemplateField HeaderText="NO." ItemStyle-CssClass="columnSpace" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemNo" runat="server" Width="30px" Text='<%# Eval("No") %>' ></asp:Label>
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
                                            <asp:TemplateField HeaderText="8105" ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="txt8105" runat="server" Width="230px" Text='<%# Eval("Warehouse_8105") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="8105 PROCESS DATE" ItemStyle-CssClass="columnSpace" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="txt8105ProcessDate" runat="server" Width="230px" Text='<%# Eval("Warehouse_LOA8105ProcessDate") %>'/>
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
            
    </section>
    </ContentTemplate>
    
    </asp:UpdatePanel>

</asp:Content>

