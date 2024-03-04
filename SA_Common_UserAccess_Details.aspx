<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SA_Common_UserAccess_Details.aspx.cs" Inherits="REPI_PUR_SOFRA.SA_Common_UserAccess_Details" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 

    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" type="text/css" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" type="text/css" />
    <link href="plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/materialize.css" rel="stylesheet" type="text/css" />
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
        
        function WarningMessage(msg) {
            swal({
                title: 'WARNING!',
                text: msg,
                type: 'warning'
            });
        }       
        
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
                        <div class="header" style="height:30px;">
                            <p style="color:Gray; font-size:14px; font-weight:bold;">USER ACCESS DETAILS</p>
                        </div>                        
                    </div>
                </div>
            </div>
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; height:1500px; width:1280px;">
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>USERNAME</th>
                                <th>FULLNAME</th> 
                                <th>SECTION</th>                                                                 
                              </tr>
                              <tr>
                                <td><asp:TextBox ID="txtUserName" runat="server" Width="380px" Height="22px" /></td>
                                <td><asp:TextBox ID="txtFullName" runat="server" Width="380px" Height="22px" /></td>
                                <td><asp:DropDownList ID="ddSection" runat="server" Width="380px" required /></td>                                                              
                              </tr>
                            </table>
                            
                            <table style="width:100%; color:Gray;">
                              <tr>
                                <th>DEPARTMENT</th> 
                                <th>DIVISION</th> 
                                <th>PC</th>                                                                                                
                              </tr>
                              <tr>
                                <td><asp:DropDownList ID="ddDepartment" runat="server" Width="380px" required /></td> 
                                <td><asp:DropDownList ID="ddDivision" runat="server" Width="380px" required /></td>
                                <td><asp:DropDownList ID="ddPC" runat="server" Width="380px" required /></td>                                                           
                              </tr>
                            </table>
                            
                            <table style="width:100%; color:Gray;">
                                <tr>
                                    <th>HQ</th> 
                                    <th>CATEGORY</th> 
                                    <th>LOCAL NO.</th>
                                </tr>
                                <tr>                                    
                                    <td><asp:DropDownList ID="ddHQ" runat="server" Width="380px" required /></td>
                                    <td><asp:DropDownList ID="ddCategory" runat="server" Width="380px" /></td> 
                                    <td><asp:TextBox ID="txtLocalNo" runat="server" Width="380px" Height="22px" /></td>
                                </tr>
                            </table>
                            
                            <table style="width:50%; color:Gray; margin-bottom:30px;">
                                <tr>
                                    <th>EMAIL ADDRESS</th> 
                                    <th>RESET PASSWORD</th> 
                                    <th>&nbsp;</th>
                                </tr>
                                <tr>                                    
                                    <td><asp:TextBox ID="txtEmailAddress" runat="server" Width="300px" Height="22px" /></td>
                                    <td><asp:CheckBox ID="cbResetPassword" runat="server" Text="RESET PASSWORD ?" /></td> 
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false" RowStyle-Font-Size="11px" HeaderStyle-Font-Size="11px"
                                                                     HeaderStyle-Font-Bold="false" HeaderStyle-BackColor="#00BCD4" PagerStyle-CssClass="pagination-ys"
                                                                     HeaderStyle-ForeColor="White" OnRowDataBound="gvData_OnRowDataBound" OnPageIndexChanging="gvData_PageIndexChanging" 
                                                                     OnRowCommand="gvData_RowCommand" FooterStyle-Font-Size="10px"
                                                                     EmptyDataText="No Record Found!" >
                                                                     
                                                                     
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("FormList_RefId") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>   
                                <Columns>
                                    <asp:TemplateField HeaderText="FORM TYPE" HeaderStyle-Width="350px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFormType" runat="server" Text='<%# Eval("FormList_FormType") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>                                       
                                <Columns>
                                    <asp:TemplateField HeaderText="FORM NAME" HeaderStyle-Width="736px" ItemStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFormName" runat="server" Text='<%# Eval("FormList_FormName") %>' />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns> 
                                <Columns>
                                    <asp:TemplateField HeaderText="ALLOWED" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibAllowed" runat="server" Width="20px" Height="20px" CommandName="ibaCommand" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />  
                                            <asp:Label ID="lblIsAllowed" runat="server" Text='<%# Eval("FormList_IsAllowed") %>' Visible="false" />
                                            <asp:Label ID="lblAccessValue" runat="server" Text='<%# Eval("FormList_AccessValue") %>' Visible="false" />
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                                                                      
                            </asp:GridView>
                            
                            <div style="margin-top:35px; width:100px;">
                                <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect"
                                    onclick="btnSubmit_Click" />
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

