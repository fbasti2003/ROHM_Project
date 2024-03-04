<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SE_SupplierEvaluation_ReportDetails.aspx.cs" Inherits="REPI_PUR_SOFRA.SE_SupplierEvaluation_ReportDetails" MasterPageFile="~/Sofra.Master" %>

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
            max-width: 480px;
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
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1910px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                SUPPLIER EVALUATION REPORT                                
                            </p>                            
                        </div>                        
                    </div>
                </div>
            </div>
                           
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1910px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:100px; width:1910px;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#00BCD4"                                                     
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                        
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="LIST OF SUPPLIERS AND EVALUATION SCORES" HeaderStyle-Width="920px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:GridView ID="gvScores" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd">
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNoScores" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="28px" Font-Size="11px" />
                                                                    </ItemTemplate> 
                                                                </asp:TemplateField>
                                                          </Columns>
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="750px" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblScoreSupplier" runat="server" Text='<%# Eval("SupplierName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                          </Columns>
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="POINTS(%)" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblScorePoints" runat="server" Text='<%# Eval("EI_TotalScore") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                          </Columns>
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="CLASS" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblScoreClass" runat="server" Text='<%# Eval("ScoreClass") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                          </Columns>
                                                          
                                            </asp:GridView>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="RANKING" HeaderStyle-Width="920px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:GridView ID="gvRanking" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd">
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNoRanking" runat="server" Text='<%# Eval("EI_Ranking") %>' />
                                                                    </ItemTemplate> 
                                                                </asp:TemplateField>
                                                          </Columns>
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="800px" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRankingSupplier" runat="server" Text='<%# Eval("SupplierName") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                          </Columns>
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="POINTS(%)" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRankingPoints" runat="server" Text='<%# Eval("EI_TotalScore") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                          </Columns>
                                                         
                                            </asp:GridView>
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>      
                                
                            </asp:GridView>
                            
                        </div>
                        
                        <div class="body" style="min-height:100px; width:1910px;">
                            <div>  
                                <asp:GridView ID="gvChart" runat="server" AutoGenerateColumns="false" ShowHeader="false" AlternatingRowStyle-BorderStyle="None" GridLines="None"
                                                 OnRowDataBound="gvChart_OnRowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblChartSupplier" runat="server" Text='<%# Eval("SupplierName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="1000px" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChartScore" runat="server" Height="10px" />
                                                <asp:Label ID="lblChartScore" runat="server" Text='<%# Eval("CustomerChartWidthExtended") %>' Visible="false" />
                                                <asp:Label ID="lblJudgementByPerson" runat="server" Text='<%# Eval("JudgementByPerson") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblChartPoints" runat="server" Text='<%# Eval("EI_TotalScore") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        
                         <div class="body" style="min-height:100px; width:1910px;">
                            <div>
                                
                                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#00BCD4"                                                     
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
                                    <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="235px" ItemStyle-HorizontalAlign="Left" >
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
                                    <asp:TemplateField HeaderText="(REASON) ※BE SURE TO RECORD THE REASON WHEN IT IS DIFFERENT WITH THE GUIDELINES FOR POINTS." HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="CIRCULATOR COMMENTS" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Left" >
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
                                
                            </div>
                         </div>
                         
                         <div class="body" style="min-height:100px; width:1910px;">
                             <p style="margin-top:0px; text-align:left;">
                                <asp:Button ID="btnDownloadReport" runat="server" Text="DOWNLOAD REPORT" CssClass="btn bg-light-blue waves-effect" OnClick="btnDownloadReport_Click" OnClientClick="showDialog();" />
                            </p>
                         </div>
                        
                    </div>
                </div>
            </div>                                                                       
                                   
            <div class="hover_bkgr_fricc">
                <span class="helper"></span>
                <div>
                    <p style="font-size:14px; color:Black"><b>Reports successfully generated.</b></p>
                    <%--<p>
                        1. <asp:LinkButton ID="lbEvaluation" runat="server" Text="EVALUATION.xlsx" OnClick="lbEvaluation_Click" />   
                    </p>
                    <p>
                        2. <asp:LinkButton ID="lbFiscalYearEvaluation" runat="server" Text="EVALUATION FOR FISCAL YEAR.xlsx" OnClick="lbFiscalYearEvaluation_Click" /> 
                    </p>--%>
                    <p>
                        <asp:Button ID="btnViewReports" runat="server" Width="450px" Text="CLICK TO DOWNLOAD PERFORMANCE EVALUATION" OnClick="btnViewReports_Click" />
                    </p>
                    <p>
                        <asp:Button ID="btnSupplierEvaluationTable" runat="server" Width="450px" Text="CLICK TO DOWNLOAD SUPPLIER EVALUATION TABLE" OnClick="btnSupplierEvaluationTable_Click" />
                    </p>
                    <td>
                        <asp:Button ID="btnClose" runat="server" Text="CLOSE" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();" />
                    </td>
                </div>
            </div>

            
    </div>
            
    </section>
    </ContentTemplate>
    </asp:UpdatePanel>    
    
</asp:Content>





