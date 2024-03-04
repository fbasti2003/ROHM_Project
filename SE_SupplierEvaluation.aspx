<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SE_SupplierEvaluation.aspx.cs" Inherits="REPI_PUR_SOFRA.SE_SupplierEvaluation" MasterPageFile="~/Sofra.Master" %>

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
    
    <%--<script type="text/javascript">
    function computeScore(txt) {
        
//        var weight = document.getElementById('lblWeight').value;
//        var third = txt.value;
//        document.getElementById("lblTotalScore").value = parseFloat(weight) * parseFloat(txt);

    //var tbl = document.getElementById('<%=gvScore.ClientID %>')
    var RowIndex = txt.parentNode.parentNode;
    var weight = RowIndex.cells[1].getElementsByTagName("span")[0].value;
    var score = RowIndex.cells[1].innerHTML.toString();
    
    //var total = parseFloat(weight) * parseFloat(score);
    //var tbl_row = tbl.rows[parseInt(RowIndex) + 1];
    //var tbl_Cell = tbl_row.cells[0];
    //var value= tbl_Cell.innerHTML.toString();
    
    alert(score);
    
    } 
</script>--%>
    

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
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1910px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                SUPPLIER EVALUATION TABLE
                            </p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1910px;">
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
                                    <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
                                </td>                      
                              </tr>
                            </table>
                            
                        </div>
                    </div>
                </div>
            </div>   
            
            <div class="row clearfix">           
                        
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1910px;">
                    <div class="card">                
        
                        <div class="body" style="margin-top:-50px; height:118px; width:1884px; overflow:hidden; z-index: 100; position:absolute;">
                            
                            
                                
                                <asp:GridView ID="gridViewDummy" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#00BCD4"  
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                                                                        
                                                                     EmptyDataText="No Record Found!" Enabled="false" >
                                <Columns>
                                    <asp:TemplateField HeaderText="APRV?" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                           
                                                    </td>    
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibManual" runat="server" ImageUrl="~/images/ManualResponse.png" Width="20px" Height="20px" CommandName="ibManual_Command" ToolTip="MANUAL RESPONSE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                           
                                                    </td>                                                                                                   
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCounter" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="28px" Font-Size="11px" />  
                                            <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("DetailsRefId") %>' Visible="false" />    
                                            <asp:Label ID="lblFY_SupplierId" runat="server" Text='<%# Eval("FY_SupplierId") %>' Visible="false" />                                          
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>' Width="180px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="TRADER/ MAKER" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTraderMaker" runat="server" Text='<%# Eval("Classification") %>' Width="30px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="MAKER NAME" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMakerName" runat="server" Text='<%# Eval("MakerName") %>' Visible="false" Width="100px" />
                                            <asp:TextBox ID="txtMakerName" runat="server" Width="100px" />
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
                                            <asp:Label ID="lblISO9001" runat="server" Visible="false" Text='<%# Eval("ISO9001") %>' />
                                            <asp:DropDownList ID="ddISO9001" runat="server" >
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Text="YES" Value="YES" />
                                                <asp:ListItem Text="NO" Value="NO" />
                                            </asp:DropDownList>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ISO 14001" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblISO14001" runat="server" Visible="false" Text='<%# Eval("ISO14001") %>' />
                                            <asp:DropDownList ID="ddISO14001" runat="server" >
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Text="YES" Value="YES" />
                                                <asp:ListItem Text="NO" Value="NO" />
                                            </asp:DropDownList>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="IATF 16949" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblATF16949" runat="server" Visible="false" Text='<%# Eval("IATF16949") %>' />
                                            <asp:DropDownList ID="ddIATF16949" runat="server" >
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Text="YES" Value="YES" />
                                                <asp:ListItem Text="NO" Value="NO" />
                                            </asp:DropDownList>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ITEM CLASSIFICATION" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemClassification" runat="server" Text='<%# Eval("ItemClassification") %>' Width="110px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>   
                                <Columns>
                                    <asp:TemplateField HeaderText="FINANCIAL ANALYSIS" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinancialAnalysis" runat="server" Visible="false" Text='<%# Eval("EI_FinancialAnalysis") %>' />
                                            <asp:Label ID="lblFinancialAnalysisValue" runat="server" Visible="false" Text='<%# Eval("EI_FinancialAnalysis_Value") %>' />
                                            <asp:DropDownList ID="ddFinancialAnalysis" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="QUALITY" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuality" runat="server" Visible="false" Text='<%# Eval("EI_Quality") %>' />
                                            <asp:Label ID="lblQualityValue" runat="server" Visible="false" Text='<%# Eval("EI_Quality_Value") %>' />
                                            <asp:DropDownList ID="ddQuality" runat="server" Width="57px" Visible="false" />
                                            <asp:LinkButton ID="lbQuality" runat="server" Width="57px" Visible="false" Text='<%# Eval("EI_Quality") %>' CommandName="EI_Quality_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="COST RESPONSE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostResponse" runat="server" Visible="false" Text='<%# Eval("EI_CostResponse") %>' />
                                            <asp:Label ID="lblCostResponseValue" runat="server" Visible="false" Text='<%# Eval("EI_CostResponse_Value") %>' />
                                            <asp:DropDownList ID="ddCostResponse" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="DELIVERY" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelivery" runat="server" Visible="false" Text='<%# Eval("EI_Delivery") %>' />
                                            <asp:Label ID="lblDeliveryValue" runat="server" Visible="false" Text='<%# Eval("EI_Delivery_Value") %>' />
                                            <asp:DropDownList ID="ddDelivery" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="COOPERA TION" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCooperation" runat="server" Visible="false" Text='<%# Eval("EI_Cooperation") %>' />
                                            <asp:Label ID="lblCooperationValue" runat="server" Visible="false" Text='<%# Eval("EI_Cooperation_Value") %>' />
                                            <asp:DropDownList ID="ddCooperation" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="CSR" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCSR" runat="server" Visible="false" Text='<%# Eval("EI_CSR") %>' />
                                            <asp:Label ID="lblCSRValue" runat="server" Visible="false" Text='<%# Eval("EI_CSR_Value") %>' />
                                            <asp:DropDownList ID="ddCSR" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="TOTAL SCORE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalScore" runat="server" Visible="false" Text='<%# Eval("EI_TotalScore") %>' />
                                            <asp:TextBox ID="txtTotalScore" runat="server" Width="57px" />
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
                                    <asp:TemplateField HeaderText="NOTEWORTHY POINTS IN THE PROCESS OF STARTING TRADING" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoteworthy" runat="server" Text='<%# Eval("NoteworthyPoints") %>' Width="130px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="(REASON) ※BE SURE TO RECORD THE REASON WHEN IT IS DIFFERENT WITH THE GUIDELINES FOR POINTS." HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtReason" runat="server" Width="130px" Text='<%# Eval("Reason") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="CIRCULATOR COMMENTS" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCircularComments" runat="server" Width="130px" Text='<%# Eval("CircularComments") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="DIVISION MANAGER INSTRUCTIONS" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDivisionManagerInstructions" runat="server" Width="90px" Text='<%# Eval("DivManInstructions") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>                                                                                              
                                
                            </asp:GridView>
                                
                            
                            
                        </div>
                    </div>
                </div>
            </div>   
            
            <div class="row clearfix">           
                        
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1910px;">
                    <div class="card">                
        
                        <div class="body" style="margin-top:-110px; height:600px; width:1900px; overflow:auto;">
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#00BCD4"                                                     
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                        
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="APRV?" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" > 
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibApproved" runat="server" ImageUrl="~/images/A1.png" Width="20px" Height="20px" CommandName="A_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                           
                                                    </td>    
                                                    <td align="center">
                                                        <asp:ImageButton ID="ibManual" runat="server" ImageUrl="~/images/ManualResponse.png" Width="20px" Height="20px" CommandName="ibManual_Command" ToolTip="MANUAL RESPONSE" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />                                                           
                                                    </td>                                                                                                   
                                                </tr>
                                            </table>                                                                                                                                    
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="NO" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblCounter" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1%>' Height="15px" Width="28px" Font-Size="11px" />  
                                            <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("DetailsRefId") %>' Visible="false" />    
                                            <asp:Label ID="lblFY_SupplierId" runat="server" Text='<%# Eval("FY_SupplierId") %>' Visible="false" />                                          
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="SUPPLIER NAME" HeaderStyle-Width="180px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName") %>' Width="180px"  />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="TRADER/ MAKER" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTraderMaker" runat="server" Text='<%# Eval("Classification") %>' Width="30px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="MAKER NAME" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblMakerName" runat="server" Text='<%# Eval("MakerName") %>' Visible="false" Width="100px" />
                                            <asp:TextBox ID="txtMakerName" runat="server" Width="100px" />
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
                                            <asp:Label ID="lblISO9001" runat="server" Visible="false" Text='<%# Eval("ISO9001") %>' />
                                            <asp:DropDownList ID="ddISO9001" runat="server" >
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Text="YES" Value="YES" />
                                                <asp:ListItem Text="NO" Value="NO" />
                                            </asp:DropDownList>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ISO 14001" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblISO14001" runat="server" Visible="false" Text='<%# Eval("ISO14001") %>' />
                                            <asp:DropDownList ID="ddISO14001" runat="server" >
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Text="YES" Value="YES" />
                                                <asp:ListItem Text="NO" Value="NO" />
                                            </asp:DropDownList>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="IATF 16949" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblATF16949" runat="server" Visible="false" Text='<%# Eval("IATF16949") %>' />
                                            <asp:DropDownList ID="ddIATF16949" runat="server" >
                                                <asp:ListItem Text="" Value="" />
                                                <asp:ListItem Text="YES" Value="YES" />
                                                <asp:ListItem Text="NO" Value="NO" />
                                            </asp:DropDownList>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ITEM CLASSIFICATION" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemClassification" runat="server" Text='<%# Eval("ItemClassification") %>' Width="110px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>   
                                <Columns>
                                    <asp:TemplateField HeaderText="FINANCIAL ANALYSIS" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinancialAnalysis" runat="server" Visible="false" Text='<%# Eval("EI_FinancialAnalysis") %>' />
                                            <asp:Label ID="lblFinancialAnalysisValue" runat="server" Visible="false" Text='<%# Eval("EI_FinancialAnalysis_Value") %>' />
                                            <asp:DropDownList ID="ddFinancialAnalysis" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="QUALITY" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuality" runat="server" Visible="false" Text='<%# Eval("EI_Quality") %>' />
                                            <asp:Label ID="lblQualityValue" runat="server" Visible="false" Text='<%# Eval("EI_Quality_Value") %>' />
                                            <asp:DropDownList ID="ddQuality" runat="server" Width="57px" Visible="false" />
                                            <asp:LinkButton ID="lbQuality" runat="server" Width="57px" Visible="false" Text='<%# Eval("EI_Quality") %>' CommandName="EI_Quality_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="COST RESPONSE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostResponse" runat="server" Visible="false" Text='<%# Eval("EI_CostResponse") %>' />
                                            <asp:Label ID="lblCostResponseValue" runat="server" Visible="false" Text='<%# Eval("EI_CostResponse_Value") %>' />
                                            <asp:DropDownList ID="ddCostResponse" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="DELIVERY" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelivery" runat="server" Visible="false" Text='<%# Eval("EI_Delivery") %>' />
                                            <asp:Label ID="lblDeliveryValue" runat="server" Visible="false" Text='<%# Eval("EI_Delivery_Value") %>' />
                                            <asp:DropDownList ID="ddDelivery" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="COOPERA TION" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCooperation" runat="server" Visible="false" Text='<%# Eval("EI_Cooperation") %>' />
                                            <asp:Label ID="lblCooperationValue" runat="server" Visible="false" Text='<%# Eval("EI_Cooperation_Value") %>' />
                                            <asp:DropDownList ID="ddCooperation" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="CSR" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblCSR" runat="server" Visible="false" Text='<%# Eval("EI_CSR") %>' />
                                            <asp:Label ID="lblCSRValue" runat="server" Visible="false" Text='<%# Eval("EI_CSR_Value") %>' />
                                            <asp:DropDownList ID="ddCSR" runat="server" Width="57px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="TOTAL SCORE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalScore" runat="server" Visible="false" Text='<%# Eval("EI_TotalScore") %>' />
                                            <asp:TextBox ID="txtTotalScore" runat="server" Width="57px" />
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
                                    <asp:TemplateField HeaderText="NOTEWORTHY POINTS IN THE PROCESS OF STARTING TRADING" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoteworthy" runat="server" Text='<%# Eval("NoteworthyPoints") %>' Width="130px" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="(REASON) ※BE SURE TO RECORD THE REASON WHEN IT IS DIFFERENT WITH THE GUIDELINES FOR " HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtReason" runat="server" Width="130px" Text='<%# Eval("Reason") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="CIRCULATOR COMMENTS" HeaderStyle-Width="130px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCircularComments" runat="server" Width="130px" Text='<%# Eval("CircularComments") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="DIVISION MANAGER INSTRUCTIONS" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDivisionManagerInstructions" runat="server" Width="90px" Text='<%# Eval("DivManInstructions") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>                                                                                              
                                
                            </asp:GridView>
                            
                        </div>
                    </div>
                </div>
            </div> 
            
            <div class="row clearfix" id="divActionButtons" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1910px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:65px; width:1075px;">                        
                            <asp:Button ID="btnUpdate" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" OnClick="btnUpdate_Click" Visible="false" />
                            <asp:Button ID="btnForApproval" runat="server" Text="SEND FOR APPROVAL" CssClass="btn bg-pink waves-effect" OnClick="btnForApproval_Click" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>                                                                      
                                   
            <div class="hover_bkgr_fricc">
                <span class="helper"></span>
                <div>
                    <p style="font-size:28px; color:Red"><asp:Label ID="lblSupplierName2" runat="server" /><asp:Label ID="lblRow" runat="server" Visible="false"/><asp:Label ID="lblDetailsRefId" runat="server" Visible="false"/></p>
                    <p>
                        <asp:GridView ID="gvQuality" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" PageSize="27" PagerStyle-Font-Size="13px"   
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White" 
                                                                     OnRowDataBound="gvQuality_OnRowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="TYPE" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("Level") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>                                  
                                 <Columns>
                                    <asp:TemplateField HeaderText="ITEM" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server" Text='<%# Eval("ItemName") %>' />
                                        </ItemTemplate>                                   
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="EVALUATION CRITERIA" HeaderStyle-Width="650px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblEvaluationCriteria" runat="server" Text='<%# Eval("Criteria") %>' />
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="POINTS" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoints" runat="server" Text='<%# Eval("Points") %>' />
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="WEIGHT" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight") %>' />
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>   
                                        
                                
                            </asp:GridView>
                    </p>  
                    <p>
                        <asp:GridView ID="gvScore" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd"
                                                                     PagerSettings-Mode="NumericFirstLast" PageSize="27" PagerStyle-Font-Size="13px"   
                                                                     PagerStyle-Font-Bold="true" HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White">
                            <Columns>
                                <asp:TemplateField HeaderText="TYPE" HeaderStyle-Width="600px" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblLevel" runat="server" Text='<%# Eval("Level") %>' />
                                    </ItemTemplate> 
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="WEIGHT" HeaderStyle-Width="600px" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight") %>' />
                                    </ItemTemplate> 
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="SCORE" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtScore" runat="server" Width="100px" Text='<%# Eval("Score") %>' />
                                    </ItemTemplate> 
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="TOTAL SCORE" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalScore" runat="server" Width="100px" Text='<%# Eval("TotalScore") %>' />
                                    </ItemTemplate> 
                                </asp:TemplateField>
                            </Columns>                                                 
                                                                     
                        </asp:GridView>
                        
                    </p> 
                    <td>
                        <asp:Button ID="btnProceed" runat="server" Text="PROCEED" CssClass="btn bg-light-blue waves-effect" OnClick="btnProceed_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();" />
                    </td>
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