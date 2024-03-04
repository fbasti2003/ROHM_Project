<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="REPI_PUR_SOFRA.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>REPI_PUR_SOFRA Login Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE" />
    
    <script src="plugins/jquery/jquery.min.js" type="text/javascript"></script>
    <script src="plugins/bootstrap/js/bootstrap.js" type="text/javascript"></script>
    <script src="plugins/node-waves/waves.js" type="text/javascript"></script>
    <script src="plugins/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="js/admin.js" type="text/javascript"></script>
    <script src="js/pages/examples/sign-in.js" type="text/javascript"></script>
    
    <script src="plugins/sweetalert/sweetalert.min.js" type="text/javascript"></script>
    <script src="js/pages/ui/dialogs.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function InvalidCredentials() {
            swal({
                title: 'ERROR!',
                text: 'Invalid username or password.',
                type: 'warning'
            });
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
    
    <link href="plugins/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="plugins/node-waves/waves.css" rel="stylesheet" />
    <link href="plugins/animate-css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="plugins/sweetalert/sweetalert.css" rel="stylesheet" />       
    
</head>
<body class="login-page">
    <form id="form1" runat="server">
    
        <div class="login-box">
        <div class="logo">
            <a href="javascript:void(0);">REPI PURCHASING</a>
            <b><small>SOLUTION FRAMEWORK</small></b>
        </div>
        <div class="card">
            <div class="body">
                <form id="sign_in" method="POST">
                    <div class="msg">Change Password Form</div>
                    <div class="input-group">
                        <div class="form-line">
                            <input type="text" runat="server" class="form-control" id="username" name="username" placeholder="Username" required autofocus>                            
                        </div>
                    </div>
                    <div class="input-group">
                        <div class="form-line">
                            <input type="password" runat="server" class="form-control" id="password" name="password" placeholder="Old Password" required>
                        </div>
                    </div>
                    <div class="input-group">
                        <div class="form-line">
                            <input type="password" runat="server" class="form-control" id="newpassword" name="newpassword" placeholder="New Password" required>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">                            
                            <asp:Button id="btnChange" runat="server" CssClass="btn btn-block bg-pink waves-effect" Text="SUBMIT" OnClick="btnChange_Click" />
                        </div>
                    </div>
                    <div class="row m-t-15 m-b--20">
                        <div class="col-xs-6 align-left">
                            <a href="Default.aspx">Login Page</a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
    
    </form>        
    
</body>
</html>
