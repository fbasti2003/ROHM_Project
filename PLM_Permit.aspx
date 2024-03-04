<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PLM_Permit.aspx.cs" Inherits="REPI_PUR_SOFRA.PLM_Permit" MasterPageFile="~/Sofra.Master" %>

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
        
        function divexpandcollapse(divname) {

            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "images/A1.png";
               
            } else {
                div.style.display = "none";
                img.src = "images/A2.png";
            }

        }       
        
    </script>
    
    <script type="text/javascript">
        $(function () {
            $("[id*=txtExpirationDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
        
        $(function () {
            $("[id*=txtIssuedDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
    
    <script type="text/javascript">
        function setOnLoad() {
            document.getElementById('<%=divOpacity.ClientID %>').style.opacity = "0.5";
            document.getElementById('<%=divLoader.ClientID %>').style.display = "block"; 
        }

    </script>
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        
        <div id="divOpacity" runat="server" class="container-fluid" style="margin-top:-50px; margin-left:-320px; opacity:0.1; width:1280px;"> 
        
        <div class="container-fluid" style="margin-left:-20px; width:1280px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">PERMIT / CERTIFICATION / TEST RESULT</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:100px; width:950px;">
                            <p style="color:Gray; font-size:12px; font-weight:bold;">SELECT EXPIRATION DATE RANGE YOU WANT TO SEARCH</p>
                            <div style="margin-top:10px;">
                                <table style="width:950px; color:Gray; font-size:12px;">
                                  <tr>
                                    <%--<th>FROM</th>
                                    <th>TO</th> --%>
                                    <th>ITEM TO SEARCH</th>
                                    <th>&nbsp;</th>
                                    <th>&nbsp;</th>
                                  </tr>
                                  <tr>
                                    <%--<td><asp:TextBox ID="txtFrom" runat="server" Width="200px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>
                                    <td><asp:TextBox ID="txtTo" runat="server" Width="200px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td>  --%> 
                                    <td><asp:TextBox ID="txtSearch" runat="server" Width="750px" Height="28px" Font-Bold="true" Font-Size="14px" class="form-control" /></td> 
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="SEARCH" Height="28px" CssClass="btn bg-green waves-effect" OnClick="btnSubmit_Click" />
                                    </td>      
                                    <td>
                                        <asp:Button ID="btnAddNew" runat="server" Text="ADD NEW" Height="28px" CssClass="btn bg-blue waves-effect" OnClick="btnAddNew_Click" />
                                    </td>                      
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
                            
                            <div id="divLoader" runat="server" style="display:none; width:1280px;">
                                <p style="font-weight:bold; font-size:14px; color:Black; margin-left:30%;">LOADING DATA... PLEASE WAIT...</p>
                            </div>
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                     OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                     EmptyDataText="No Record Found!">
                                <Columns>
                                    <asp:TemplateField HeaderText="PERMIT NAME" HeaderStyle-Width="510px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblPermitName" runat="server" Text='<%# Eval("PermitName") %>' Height="15px" Width="510px" Font-Size="11px" />                                                      
                                            <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RefId") %>' Visible="false" />   
                                            <asp:Label ID="lblGovernmentAgency" runat="server" Text='<%# Eval("GovernmentAgency") %>' Visible="false" />  
                                            <asp:Label ID="lblPermitId" runat="server" Text='<%# Eval("PermitId") %>' Visible="false" />     
                                            <asp:Label ID="lblColorCode" runat="server" Text='<%# Eval("ColorCode") %>' Visible="false" />                              
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="ISSUED DATE" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssuedDate" runat="server" Text='<%# Eval("IssuedDate") %>' Height="15px" Width="110px" />                                               
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="EXPIRATION DATE" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpirationDate" runat="server" Text='<%# Eval("ExpirationDate") %>' Height="15px" Width="110px"  />                                               
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="LEAD TIME" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadTime" runat="server" Text='<%# Eval("LeadTime") %>' Height="15px" Width="110px"  />                                               
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="DAYS LEFT" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblDaysLeft" runat="server" Text='<%# Eval("DaysLeft") %>' Height="15px" Width="110px"  />                                               
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="STATUS" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" > 
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' Height="15px" Width="120px"  />                                               
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                </Columns>  
                                <Columns>
                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblView" runat="server" Text="DETAILS" CommandName="lblView_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="setOnLoad()" />
                                        </ItemTemplate>                                                                        
                                    </asp:TemplateField>
                                </Columns>                                                                                             
                                
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%" style="background-color:White;">
                                                    <div id="divDetails" runat="server" style="margin-left:7px; margin-bottom:5px; display:none;">
                                                        
                                                        <table style="width:1180px; color:Gray;">
                                                          <tr>
                                                            <th>ISSUED DATE</th>
                                                            <th>&nbsp;</th>  
                                                            <th>EXPIRATION DATE</th>                                                                  
                                                            <th>&nbsp;</th>                               
                                                          </tr>
                                                          <tr>
                                                            <td><asp:TextBox ID="txtIssuedDate" runat="server" Width="320px" Height="22px" Text='<%# Eval("IssuedDate") %>' /></td>
                                                            <td><p style="color:Maroon;">Ex: 01/25/2020 <b>(mm/dd/yyyy)</b></p></td>
                                                            <td><asp:TextBox ID="txtExpirationDate" runat="server" Width="320px" Height="22px" Text='<%# Eval("ExpirationDate") %>' /></td>                                                            
                                                            <td><p style="color:Maroon;">Ex: 01/25/2020 <b>(mm/dd/yyyy)</b></p></td>
                                                          </tr>
                                                        </table>
                                                        
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">NAME</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtPermitName" runat="server" Width="1180px" Height="22px" Font-Bold="true" ForeColor="Red" Text='<%# Eval("PermitName") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">CHEMICAL CONTENT</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtChemicalContent" runat="server" Width="1180px" Height="22px" Text='<%# Eval("ChemicalContent") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">ITEM NAME(S)</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtItemName" runat="server" Width="1180px" Height="22px" Text='<%# Eval("ItemName") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">SPECIFICATION</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtSpecification" runat="server" Width="1180px" Height="22px" Text='<%# Eval("Specification") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">SPECIFIED REQUIREMENT(S)</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2"><asp:TextBox ID="txtSpecifiedRequirements" runat="server" Width="1180px" Height="22px" Text='<%# Eval("SpecifiedRequirements") %>' /></td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:1187px; color:Gray;">
                                                          <tr>
                                                            <th>PERMIT CERTIFICATE</th>
                                                            <th>FREQUENCY</th>    
                                                            <th>LEADTIME (WORKING DAYS)</th>    
                                                            <th>&nbsp;</th>                               
                                                          </tr>
                                                          <tr>
                                                            <td><asp:DropDownList ID="ddGovernmentAgencies" runat="server" Width="391px" Height="22px" /></td>
                                                            <td><asp:TextBox ID="txtFrequency" runat="server" Width="391px" Height="22px" Text='<%# Eval("Frequency") %>' /></td>
                                                            <td><asp:TextBox ID="txtLeadTime" runat="server" Width="391px" Height="22px" Text='<%# Eval("LeadTime") %>' /></td>
                                                            <td>&nbsp;</td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:1187px; color:Gray;">
                                                          <tr>
                                                            <th>PROCESSOR NAME</th>
                                                            <th>SAFETY</th>    
                                                            <th>&nbsp;</th>    
                                                            <th>&nbsp;</th>                               
                                                          </tr>
                                                          <tr>
                                                            <td><asp:TextBox ID="txtProcessorName" runat="server" Width="590px" Height="22px" Text='<%# Eval("ProcessorName") %>' /></td>
                                                            <td><asp:TextBox ID="txtSafety" runat="server" Width="590px" Height="22px" Text='<%# Eval("Safety") %>' /></td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                          </tr>
                                                        </table>                                                                                                              
                                                        <table style="width:1187px; color:Gray;">
                                                          <tr>
                                                            <th>ATTACHMENT 1</th>
                                                            <th>ATTACHMENT 2</th>    
                                                            <th>ATTACHMENT 3</th>    
                                                            <th>&nbsp;</th>                               
                                                          </tr>
                                                          <tr>
                                                            <td><asp:FileUpload ID="fu1" runat="server" Width="391px" EnableViewState="true" /></td>
                                                            <td><asp:FileUpload ID="fu2" runat="server" Width="391px" EnableViewState="true" /></td>
                                                            <td><asp:FileUpload ID="fu3" runat="server" Width="391px" EnableViewState="true" /></td>
                                                            <td>&nbsp;</td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:1187px; color:Gray; margin-top:-18px;">
                                                          <tr>
                                                            <th>&nbsp;</th>
                                                            <th>&nbsp;</th>    
                                                            <th>&nbsp;</th>    
                                                            <th>&nbsp;</th>                               
                                                          </tr>
                                                          <tr>
                                                            <td><asp:LinkButton ID="lbAttachment1" runat="server" Width="391px" Text='<%# Eval("Attachment1") %>' CommandName="DLA1_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /></td>
                                                            <td><asp:LinkButton ID="lbAttachment2" runat="server" Width="391px" Text='<%# Eval("Attachment2") %>' CommandName="DLA2_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /></td>
                                                            <td><asp:LinkButton ID="lbAttachment3" runat="server" Width="391px" Text='<%# Eval("Attachment3") %>' CommandName="DLA3_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" /></td>
                                                            <td>&nbsp;</td>
                                                          </tr>
                                                        </table>
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">SEND NOTIFICATION TO?</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2">
                                                                <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                                                 HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                                                 HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                      
                                                                                                 OnRowDataBound="gvData_OnRowDataBound" OnRowCommand="gvData_RowCommand"                                                             
                                                                                                 EmptyDataText="No Record Found!">
                                                                                                 
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SUPPLIER / BROKER / BUYER" HeaderStyle-Width="360px" ItemStyle-HorizontalAlign="Left" > 
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("SupplierName") %>' Height="15px" Width="360px" />  
                                                                                <asp:Label ID="lblSupplierRefId" runat="server" Text='<%# Eval("RefId") %>' Visible="false" />    
                                                                                <asp:Label ID="lblSupplierCode" runat="server" Text='<%# Eval("SupplierCode") %>' Visible="false" />                                           
                                                                            </ItemTemplate>                                
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="EMAIL" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" > 
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("SupplierEmailAddress") %>' Height="15px" Width="300px" />                                               
                                                                            </ItemTemplate>                                
                                                                        </asp:TemplateField>
                                                                    </Columns>   
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="NOTIFICATION STATUS" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" > 
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNotificationStatus" runat="server" Text='<%# Eval("NotifiedAlready") %>' Height="15px" Width="300px" />                                               
                                                                            </ItemTemplate>                                
                                                                        </asp:TemplateField>
                                                                    </Columns>                                                                 
                                                                                                 
                                                                </asp:GridView>
                                                            </td>
                                                          </tr>
                                                        </table> 
                                                        <table style="width:980px; color:Gray;">
                                                          <tr>
                                                            <th colspan="2">&nbsp;</th>                              
                                                          </tr>
                                                          <tr>
                                                            <td colspan="2">
                                                                <asp:LinkButton ID="lbEditRecord" runat="server" Text="EDIT RECORD" ForeColor="Blue" CommandName="EditRecord_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                <asp:LinkButton ID="lblHistory" runat="server" Text="HISTORY" ForeColor="Green" CommandName="lblHistory_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                <asp:LinkButton ID="lbRenew" runat="server" Text="RENEW" ForeColor="Red" CommandName="lbRenew_Command" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            </td>
                                                          </tr>
                                                        </table>
                                                        
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%" style="background-color:White;">
                                                    <div id="divHistory" runat="server" style="margin-left:7px; margin-top:5px; margin-bottom:5px; display:none;">
                                                    
                                                        <asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" AlternatingRowStyle-BackColor="#dddddd" RowStyle-Height="15px"
                                                                     HeaderStyle-BackColor="#00BCD4" HeaderStyle-ForeColor="White"                                                                                                                                                                                       
                                                                     EmptyDataText="No Record Found!">
                                                                     
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ISSUED DATE" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIssuedDate_History" runat="server" Text='<%# Eval("IssuedDate") %>' Height="15px" Width="120px"  />                                               
                                                                    </ItemTemplate>                                
                                                                </asp:TemplateField>
                                                            </Columns> 
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="EXPIRATION DATE" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExpirationDate_History" runat="server" Text='<%# Eval("ExpirationDate") %>' Height="15px" Width="120px"  />                                               
                                                                    </ItemTemplate>                                
                                                                </asp:TemplateField>
                                                            </Columns> 
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ADDED BY" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Left" > 
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAddedBy_History" runat="server" Text='<%# Eval("AddedBy") %>' Height="15px" Width="120px"  />                                               
                                                                    </ItemTemplate>                                
                                                                </asp:TemplateField>
                                                            </Columns> 
                                                            
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ADDED DATE" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" > 
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAddedDate_History" runat="server" Text='<%# Eval("AddedDate") %>' Height="15px" Width="120px"  />                                               
                                                                    </ItemTemplate>                                
                                                                </asp:TemplateField>
                                                            </Columns> 
                                                        
                                                        </asp:GridView>
                                                        
                                                    </div>
                                                </td>
                                            </tr>
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
    
    
    </asp:UpdatePanel>
    
</asp:Content>









