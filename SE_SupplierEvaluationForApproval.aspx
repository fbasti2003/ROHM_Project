<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SE_SupplierEvaluationForApproval.aspx.cs" Inherits="REPI_PUR_SOFRA.SE_SupplierEvaluationForApproval" MasterPageFile="~/Sofra.Master" %>
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
            min-width: 700px;
            min-height: 100px;
            vertical-align: middle;
            position: relative;
            border-radius: 8px;
            padding: 15px 15px;
            left:-0px;
            top:-25%;
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
                                SUPPLIER EVALUATION APPROVAL FORM 
                            </p>
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
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="FISCAL YEAR" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFiscalYear" runat="server" Text='<%# Eval("Description") %>' Visible="false" />  
                                            <asp:LinkButton ID="lbFiscalYear" runat="server" Text='<%# Eval("Description") %>' CommandName="lbFiscalYear_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />                                          
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>' />
                                            <asp:Label ID="lblSupplierId" runat="server" Text='<%# Eval("SupplierId") %>' Visible="false" />
                                            <asp:Label ID="lblFYISupplierId" runat="server" Text='<%# Eval("Fy_SupplierId") %>' Visible="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>    
                                <%--<Columns>
                                    <asp:TemplateField HeaderText="RECEPIENT" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceipient" runat="server" Text='<%# Eval("Receipient") %>' />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>    
                                <Columns>
                                    <asp:TemplateField HeaderText="RESPONDED" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponded" runat="server" Text='<%# Eval("Responded") %>' />                                            
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns> --%>
                                 <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center" style="margin-left:1px;">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ToolTip="APPROVED BUTTON" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>
                                                    <td align="center" style="margin-left:1px;">
                                                        <asp:ImageButton ID="ibDisapproved" runat="server" ToolTip="DISAPPROVED BUTTON" ImageUrl="~/images/DA1.png" Width="20px" Height="20px" CommandName="DA_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>  
                                                    <td align="center" style="margin-left:1px;">
                                                        <asp:ImageButton ID="ibPreview" runat="server" ToolTip="PREVIEW BUTTON" ImageUrl="~/images/Report.png" Width="20px" Height="20px" CommandName="ibPreview_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                                    </td>                                                                                                     
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="REMARKS" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="200px" Height="16px" Enabled="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DIV. MANAGER INSTRUCTIONS" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDivManagerInstructions" runat="server" Width="200px" Height="16px" Enabled="false" />
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
                            <asp:Button ID="btnApproved" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnApproved_Click" />
                        </div>
                    </div>
                </div>
            </div>                                                                   
                       
            <div class="hover_bkgr_fricc">
                <span class="helper"></span>
                <div>
                    <td>
                        
                        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#00BCD4"                                                     
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                        
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCounter" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="28px" Font-Size="11px" />  
                                            <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("DetailsRefId") %>' Visible="false" />                                            
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="TRADER/ MAKER" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTraderMaker" runat="server" Text='<%# Eval("Classification") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="MAKER NAME" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMakerName" runat="server" Text='<%# Eval("MakerName") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="AUTO MOTIVE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblAutomotive" runat="server" Text='<%# Eval("AutomotiveRelated") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="ISO 9001" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblISO9001" runat="server" Text='<%# Eval("ISO9001") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ISO 14001" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblISO14001" runat="server" Text='<%# Eval("ISO14001") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="IATF 16949" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblATF16949" runat="server" Text='<%# Eval("IATF16949") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ITEM CLASSIFICATION" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemClassification" runat="server" Text='<%# Eval("ItemClassification") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>   
                                <Columns>
                                    <asp:TemplateField HeaderText="FINANCIAL ANALYSIS" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinancialAnalysis" runat="server" Text='<%# Eval("EI_FinancialAnalysis") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="QUALITY" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("EI_Quality") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="COST RESPONSE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostResponse" runat="server" Text='<%# Eval("EI_CostResponse") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="DELIVERY" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelivery" runat="server" Text='<%# Eval("EI_Delivery") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="COOPERA TION" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCooperation" runat="server" Text='<%# Eval("EI_Cooperation") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="CSR" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCSR" runat="server" Text='<%# Eval("EI_CSR") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="TOTAL SCORE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalScore" runat="server" Text='<%# Eval("EI_TotalScore") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="JUDGEMENT BY PERSON INCHARGE" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblJudgementByPerson" runat="server" Text='<%# Eval("JudgementByPerson") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>   
                                <Columns>
                                    <asp:TemplateField HeaderText="JUDGEMENT YEAR/MONTH" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblJudgementYearMonth" runat="server" Text='<%# Eval("JudgementYearMonth") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="DIVISION MANAGER EVALUATION RESULT" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivisionManagerEvaluationResult" runat="server" Text='<%# Eval("DivManEvaluationResult") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="SUB CON TRACTOR" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubContractor" runat="server" Text='<%# Eval("SubContractor") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>   
                                <Columns>
                                    <asp:TemplateField HeaderText="NOTEWORTHY POINTS IN THE PROCESS OF STARTING TRADING" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoteworthy" runat="server" Text='<%# Eval("NoteworthyPoints") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="(REASON) ※BE SURE TO RECORD THE REASON WHEN IT IS DIFFERENT WITH THE GUIDELINES FOR POINTS." HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="CIRCULATOR COMMENTS" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCircularComments" runat="server" Text='<%# Eval("CircularComments") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="DIVISION MANAGER INSTRUCTIONS" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDivisionManagerInstructions" runat="server" Text='<%# Eval("DivManInstructions") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>                                                                                              
                                
                            </asp:GridView>
                        
                    </td>
                    <td>
                        <br />
                        <asp:Button ID="btnOk" runat="server" Text="CLOSE" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();" />
                    </td>
                </div>
            </div>            
            

            
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID = "btnApproved" />
    </Triggers>
    </asp:UpdatePanel>    
    
</asp:Content>
