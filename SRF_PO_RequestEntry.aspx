<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SRF_PO_RequestEntry.aspx.cs" Inherits="REPI_PUR_SOFRA.SRF_PO_RequestEntry" MasterPageFile="~/Sofra.Master" %>

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
    
    
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="min-height:10px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">
                                PULL OUT REQUEST ENTRY 
                            </p>
                            <p style="color:Red; font-size:14px; font-weight:bold;">
                                <%= displayCTRLNo %>
                            </p>
                        </div>                        
                    </div>
                </div>
            </div>
            
             <div class="row clearfix" id="divApproval" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:65px; width:1075px;">                        
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>REQUESTER</th>
                                <th>PROD. MANAGER</th>
                                <th>SCD BUYER</th>
                                <th>SCD INCHARGE</th>
                              </tr>
                              <tr>
                                <td>
                                    <div style="width:280px; background-color:White;">
                                        <div id="divRequester" runat="server" style="width:260px; border: 1px solid black; padding-left:5px; background-color:#00C851;">
                                            <asp:Label ID="lblRequester" runat="server" Height="44px" Width="245px" ForeColor="White" />
                                        </div>
                                    </div>
                                 </td>
                                <td>
                                    <div style="width:280px; background-color:White;">
                                        <div id="divProdManager" runat="server" style="width:260px; border: 1px solid black; padding-left:5px;">
                                            <asp:Label ID="lblProdManager" runat="server" Height="44px" Width="245px" ForeColor="White" />
                                        </div>
                                    </div>
                                 </td>
                                <td>
                                    <div style="width:280px; background-color:White;">
                                        <div id="divBuyer" runat="server" style="width:260px; border: 1px solid black; padding-left:5px;">
                                            <asp:Label ID="lblBuyer" runat="server" Height="44px" Width="245px" ForeColor="White" />
                                        </div>
                                    </div>
                                 </td>
                                <td>
                                    <div style="width:280px; background-color:White;">
                                        <div id="divSCIncharge" runat="server" style="width:260px; border: 1px solid black; padding-left:5px;">
                                            <asp:Label ID="lblSCIncharge" runat="server" Height="44px" Width="245px" ForeColor="White" />
                                        </div>
                                    </div>
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
                        <div class="body" style="margin-top:-23px; height:80px; width:1280px;">
                            
                            <table style="width:950px; color:Gray;">
                              <tr>
                                <th>PULL OUT TYPE</th>
                                <th>CONTRACTOR</th> 
                                <th>DIVISION</th>                       
                              </tr>
                              <tr>                                
                                <td>
                                    <asp:DropDownList ID="ddPullOutType" runat="server" Width="300px" OnSelectedIndexChanged="ddPullOutType_Change" AutoPostBack="true" required>
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="CONTAINER TUBES" Value="CONTAINER TUBES" />
                                        <asp:ListItem Text="IC TRAYS" Value="IC TRAYS" />
                                        <asp:ListItem Text="EMPTY BLACK TRAY" Value="EMPTY BLACK TRAY" />
                                        <asp:ListItem Text="DANPLA BOX" Value="DANPLA BOX" />
                                        <asp:ListItem Text="ROBUST REEL" Value="ROBUST REEL" />
                                    </asp:DropDownList>
                                </td> 
                                <td>
                                    <asp:DropDownList ID="ddSupplier" runat="server" Width="510px" required />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddDivision" runat="server" Width="125px" required>
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="LSI 1" Value="LSI 1" />
                                        <asp:ListItem Text="LSI 2" Value="LSI 2" />
                                        <asp:ListItem Text="LSI 3" Value="LSI 3" />
                                        <asp:ListItem Text="MCR" Value="MCR" />
                                        <asp:ListItem Text="TR" Value="TR" />
                                    </asp:DropDownList>
                                </td>                                                         
                              </tr>
                            </table>
                            
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divContainerTube" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:65px; width:1075px;">                        
                            
                            <asp:GridView ID="gvContainerTube" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
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
                                            <asp:TextBox ID="txtNoOfBoxes" runat="server" Width="100px" Text='<%# Eval("NoOfBoxes") %>' />                       
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="GROSS WEIGHT (KG)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGrossWeight" runat="server" Width="120px" Text='<%# Eval("GrossWeight") %>' />                       
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
                                
                            </asp:GridView>
                            
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divICTrays" runat="server" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:65px; width:1075px;">                        
                            
                            <asp:GridView ID="gvICTrays" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
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
                                    <asp:TemplateField HeaderText="SPECIFICATION" HeaderStyle-Width="290px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("Specification") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="BOX TYPE" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblBoxType" runat="server" Text='<%# Eval("BoxType") %>' />                                                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SIZE" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size") %>' />                                                     
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="NO. OF BOXES" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNoOfBoxes" runat="server" Width="100px" Text='<%# Eval("NoOfBoxes") %>' />                       
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
                                    <asp:TemplateField HeaderText="QUANTITY (PCS)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Width="120px" Text='<%# Eval("Quantity") %>' />                     
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
                                    <asp:TemplateField HeaderText="SPECIFICATION" HeaderStyle-Width="390px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Eval("Specification") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>                               
                                <Columns>
                                    <asp:TemplateField HeaderText="QUANTITY (PCS)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' />                           
                                        </ItemTemplate>                                                                         
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="SRF ITEM NAME" HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRFItemName" runat="server" Text='<%# Eval("SRFItemName") %>' />                                                  
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>
                            
                            
                        </div>
                    </div>
                </div>
            </div>        
      
            <div class="row clearfix" runat="server" id="divAttachment" style="display:none;">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; min-height:65px; width:1280px;">
                            <table style="width:700px; color:Gray;">
                              <tr>
                                <th>REQUEST FORM</th>     
                              </tr>
                              <tr>
                                <td><asp:ImageButton ID="ibAttachment1" runat="server" ImageUrl="~/images/excel.png" Width="60px" Height="60px" OnClick="ibAttachment1_Click" /></td>                           
                              </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="row clearfix" id="divActionButtons" runat="server">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card">
                        <div class="body" style="margin-top:-23px; height:65px; width:1075px;">                        
                            <asp:Button ID="btnSubmit" runat="server" Text="PREVIEW" CssClass="btn bg-light-blue waves-effect" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="btn bg-light-blue waves-effect" Visible="false" OnClick="btnBack_Click" />
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




