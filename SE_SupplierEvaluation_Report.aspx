<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SE_SupplierEvaluation_Report.aspx.cs" Inherits="REPI_PUR_SOFRA.SE_SupplierEvaluation_Report" MasterPageFile="~/Sofra.Master" %>

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
                                SUPPLIER EVALUATION REPORT
                            </p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:100px; width:1280px;">
                            
                            <table style="width:400px; color:Gray;">
                              <tr>
                                <th>FISCAL YEAR</th> 
                                <th>&nbsp;</th>                        
                              </tr>
                              <tr>                                
                                <td>
                                    <asp:DropDownList ID="ddFiscalYear" runat="server" Width="300px" CssClass="form-control" />
                                </td>   
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" CssClass="btn bg-light-blue waves-effect" OnClick="btnSubmit_Click" />
                                </td>                      
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
                                    <asp:TemplateField HeaderText="LIST OF SUPPLIERS AND EVALUATION SCORES" HeaderStyle-Width="600px" ItemStyle-HorizontalAlign="Left" >
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
                                                                <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="430px" ItemStyle-HorizontalAlign="Left" >
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
                                                                        <asp:Label ID="lblScoreClass" runat="server" Text='<%# Eval("EI_EvaluationPoints") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                          </Columns>
                                                          
                                            </asp:GridView>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="RANKING" HeaderStyle-Width="600px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:GridView ID="gvRanking" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                          HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd">
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNoRanking" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' />
                                                                    </ItemTemplate> 
                                                                </asp:TemplateField>
                                                          </Columns>
                                                          <Columns>
                                                                <asp:TemplateField HeaderText="SUPPLIER" HeaderStyle-Width="480px" ItemStyle-HorizontalAlign="Left" >
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
                        
                        <div class="body" style="min-height:100px; width:1280px;">
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
                                        <asp:TemplateField HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Left" >
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtChartScore" runat="server" Height="10px" />
                                                <asp:Label ID="lblChartScore" runat="server" Text='<%# Eval("CustomerChartWidth") %>' Visible="false" />
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



