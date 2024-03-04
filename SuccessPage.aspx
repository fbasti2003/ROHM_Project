<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuccessPage.aspx.cs" Inherits="REPI_PUR_SOFRA.SuccessPage" MasterPageFile="~/Sofra.Master" %>


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
            document.getElementById("txtName").style.display = "block";
            document.getElementById("txtAddress").style.display = "block";
            document.getElementById("txtPhone").style.display = "block";
            document.getElementById("txtFax").style.display = "block";
            
            document.getElementById("lblName").style.display = "none";
            document.getElementById("lblAddress").style.display = "none";
            document.getElementById("lblPhone").style.display = "none";
            document.getElementById("lblFax").style.display = "none";
            
        }
                
        
    </script>
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <section class="content">
        <div class="container-fluid" style="margin-top:-95px; margin-left:-320px;">
            <div class="card">                        
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">SUCCESS PAGE</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:500px; width:1280px;">
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <td style="font-weight:bold; width:400px;">MESSAGE</td>
                                <td style="font-weight:bold;">TRANSACTION NAME</td> 
                              </tr>
                              <tr>
                                <td style="font-size:12px;"><asp:Label ID="lblMessage" runat="server" /></td>
                                <td style="font-size:12px;"><asp:Label ID="lblTransactionName" runat="server" /></td> 
                              </tr>
                            </table>
                            <div style="margin-top:15px; width:100px;">
                                <asp:Button ID="btnSubmit" runat="server" Text="OK" CssClass="btn btn-block bg-pink waves-effect"
                                    onclick="btnSubmit_Click" />
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

