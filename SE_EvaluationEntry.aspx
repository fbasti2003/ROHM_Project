<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SE_EvaluationEntry.aspx.cs" Inherits="REPI_PUR_SOFRA.SE_EvaluationEntry" MasterPageFile="~/Sofra.Master" %>

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
            $("[id*=txtETDManila]").datepicker({
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
        
        function disabledProceedButton()
        {
            $('#btnProceed').prop('disabled', true);
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
            max-width: 700px;
            min-height: 100px;
            vertical-align: middle;
            width: 60%;
            position: relative;
            border-radius: 8px;
            padding: 15px 15px;
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
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                SUPPLIER EVALUATION REQUEST ENTRY 
                            </p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:100px; width:1280px;">
                            
                            <table style="width:90%; color:Gray;">
                              <tr>
                                <th>FISCAL YEAR</th>                         
                              </tr>
                              <tr>                                
                                <td>
                                    <asp:DropDownList ID="ddFiscalYear" runat="server" Width="300px" CssClass="form-control" />                                                                        
                                </td>   
                                <td>
                                    <asp:Button ID="btnSelect" runat="server" Width="100px" Text="Check All" OnClick="btnSelect_Click" />
                                </td>   
                                <td>
                                    <asp:Button ID="btnUncheck" runat="server" Width="100px" Text="Uncheck All" OnClick="btnUncheck_Click" />                                    
                                </td>   
                                <td><p style="color:Red;">*** Make sure that you selected the correct Fiscal Year before you click the <b>SUBMIT</b> Button</p></td>                
                              </tr>
                            </table>
                            
                        </div>
                    </div>
                </div>
            </div>   
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:100px; width:1280px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#00BCD4"                                                     
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                        
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("RefId") %>' />
                                            <asp:Label ID="lblReceipient" runat="server" Text='<%# Eval("Receipient") %>' Visible="false" />
                                            <asp:Label ID="lblResponded" runat="server" Text='<%# Eval("Responded") %>' Visible="false" />
                                            <asp:Label ID="lblStatDivManager" runat="server" Text='<%# Eval("StatDivManager") %>' Visible="false" />
                                            <asp:Label ID="lblFySupplierId" runat="server" Text='<%# Eval("Fy_SupplierId") %>' Visible="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="460px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>                                
                                <Columns>
                                    <asp:TemplateField HeaderText="EVALUATION EMAIL" HeaderStyle-Width="310px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EvaluationEmail") %>' />                                            
                                        </ItemTemplate>                                                                          
                                    </asp:TemplateField>
                                </Columns>                                                                
                                <Columns>
                                    <asp:TemplateField HeaderText="SEND?" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                           
                                                    </td>   
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibReport" runat="server" ImageUrl="~/images/excel.png" ToolTip="FINANCIAL ANALYSIS" Width="20px" Height="20px" CommandName="ibReport_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  /> 
                                                    </td>  
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibReport2" runat="server" ImageUrl="~/images/excel.png" ToolTip="SUPPLIER REVALUATION SHEET" Width="20px" Height="20px" CommandName="ibReport2_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  /> 
                                                    </td>                                               
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="320px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text="FOR SENDING" />                                            
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
                        <div class="body" style="margin-top:-23px; height:65px; width:1075px;">                        
                            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="hover_bkgr_fricc">
                <span class="helper"></span>
                <div>
                    <p style="font-size:28px; color:Red"><b>! REMINDER !</b></p>
                    <p style="font-size:14px; color:Black">1. Please make sure that you selected the correct fiscal year.</p>    
                    <p style="font-size:14px; color:Black">2. Please make sure that you review the listed suppliers or those suppliers that need to be included in the supplier evaluation.</p>  
                    <p style="font-size:14px; color:Black">3. Please take note that not all suppliers may receive the "Basic Information Sheet" and "Accounting/Credit Information Evaluation Sheet or "SUPPLIER EVALUATION RESULT" file because of possible intermittent connection. In this case, go to Evaluation Request Monitoring and do the resending.</p>         
                    <p>&nbsp;</p>
                    <p style="font-size:16px; color:Red;">JUST CLICK PROCEED BUTTON ONCE, IT WILL AUTOMATICALLY REDIRECT TO SUCCESS PAGE ONCE COMPLETED!</p>    
                    <td>
                        <asp:Button ID="btnProceed" runat="server" Text="PROCEED" CssClass="btn bg-light-blue waves-effect" OnClick="btnProceed_Click" OnClientClick="disabledProceedButton()" />
                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();" />
                    </td>
                </div>
            </div>

            
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnProceed" />
    </Triggers>
    </asp:UpdatePanel>    
    
</asp:Content>
