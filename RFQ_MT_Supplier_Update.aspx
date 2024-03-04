<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ_MT_Supplier_Update.aspx.cs" Inherits="REPI_PUR_SOFRA.RFQ_MT_Supplier_Update" MasterPageFile="~/Sofra.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="plugins/sweetalert/sweetalert.min.js" type="text/javascript"></script>
    
    <style>
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
            max-width: 551px;
            min-height: 100px;
            vertical-align: middle;
            width: 60%;
            position: relative;
            border-radius: 8px;
            padding: 15px 5%;
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
    
    <script type="text/javascript">    
  
        $(function () {
            $("[id*=txtServiceDate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
    
    <script type="text/javascript">    
  
        $(function () {
            $("[id*=txtDeliveryDateToRepi]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'images/calendar.png'
            }).on('changeDate', function (e) {
                $(this).datepicker('hide')
            });
            
        });
    </script>
    
    <script type="text/javascript">
   
        function DisableSupplier()
        {
            
        }
     
    </script>
      
    
<asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   
        <section class="content">
        <div class="container-fluid" style="margin-top:-50px; margin-left:-320px; width:1075px;">
                                                  
            <!-- Exportable Table -->
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="header" style="height:30px;">
                            <p runat="server" id="pTitle" style="color:Gray; font-size:14px; font-weight:bold;"></p>
                        </div>                        
                    </div>
                </div>
            </div>            
            
            <div class="row clearfix">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="width:1280px;">
                    <div class="card"> 
                        <div class="body" style="margin-top:-23px; min-height:300px; width:1075px;">
                            <table style="width:100%; color:Gray;">
                              <tr>                                
                                <th>NAME</th>                               
                              </tr>
                              <tr>                                                               
                                <td><asp:TextBox ID="txtName" runat="server" Width="700px" Height="22px" /></td>                                                        
                              </tr>
                            </table>
                            <table style="width:100%; color:Gray;">
                                <tr>
                                    <th>ADDRESS</th>  
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="txtAddress" runat="server" Width="850px" Height="22px" /></td>
                                </tr>
                            </table>
                            <table style="width:100%; color:Gray;">
                                <tr>
                                    <th>EMAIL</th>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="txtEmail" runat="server" Width="500px" Height="22px" /></td>  
                                </tr>
                            </table>
                            <table style="width:100%; color:Gray;">
                                <tr>
                                    <th>SUPPLIER EVALUATION EMAIL</th>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="txtSupplierEvaluationEmail" runat="server" Width="500px" Height="22px" /></td>  
                                </tr>
                            </table>
                            <table style="width:100%; color:Gray;">
                                <tr>
                                    <th>REGISTERED / NOT REGISTERED</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddRegistered" runat="server" Width="300px" Height="22px" >
                                            <asp:ListItem Text="" Value="0" />
                                            <asp:ListItem Text="REGISTERED" Value="1" />
                                            <asp:ListItem Text="NOT REGISTERED" Value="2" />
                                        </asp:DropDownList>
                                    </td>  
                                </tr>
                            </table>
                            <table style="width:100%; color:Gray;">
                                <tr>
                                    <th>PEZA / NON PEZA</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddPeza" runat="server" Width="300px" Height="22px" >
                                            <asp:ListItem Text="" Value="0" />
                                            <asp:ListItem Text="PEZA" Value="1" />
                                            <asp:ListItem Text="NON PEZA" Value="2" />
                                        </asp:DropDownList>
                                    </td>  
                                </tr>
                            </table>
                            <table style="width:100%; color:Gray; margin-top:5px;">
                              <tr>                                                                 
                                <th>CATEGORY</th>                                 
                              </tr>
                              <tr>                                
                                <td><asp:CheckBoxList ID="cblCategory" runat="server" Width="750px" Height="22px" /></td> 
                              </tr>
                            </table>                            
                            <table style="width:200px; color:Gray; margin-top:5px;">
                              <tr>                                                                 
                                <th>&nbsp;</th>      
                                <th>&nbsp;</th>   
                                <th>&nbsp;</th>                          
                              </tr>
                              <tr>                                
                                <td><asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btn bg-light-blue waves-effect" onclick="btnSubmit_Click" /></td> 
                                <td><asp:Button ID="btnDisable" runat="server" Text="DISABLE" CssClass="btn bg-light-blue waves-effect" OnClientClick="showDialog(); return false;"/></td>
                                <td><asp:Button ID="btnCancel" runat="server" Text="BACK" CssClass="btn bg-light-blue waves-effect" onclick="btnCancel_Click" /></td>
                              </tr>
                            </table>
                        </div> 
                                                                        
                                               
                    </div>                                                                                 
                    
                </div>
            </div>         
            
            
            <div class="hover_bkgr_fricc">
                <span class="helper"></span>
                <div>
                    <h2>Are you sure you want to disble this record?</h2>
                    <td><asp:Button ID="btnDisable2" runat="server" Text="DISABLE" CssClass="btn bg-light-blue waves-effect" onclick="btnDisable2_Click" /></td>
                    <td><asp:Button ID="btnCancel2" runat="server" Text="CANCEL" CssClass="btn bg-light-blue waves-effect" OnClientClick="hideDialog();"/></td>
                </div>
            </div>
            
                                          
                                               
            

            
    </div>
            
    </section>
    </ContentTemplate>
    <Triggers>
       <asp:PostBackTrigger ControlID = "btnSubmit" />
       <asp:PostBackTrigger ControlID = "btnCancel" />
    </Triggers>
    </asp:UpdatePanel>    
    
</asp:Content>

