<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="CustomerPortal.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--<link rel="stylesheet" href="styles.css" />-->
    <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>

<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>

<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>

<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">

            <h2>Please enter your Username and Password to proceed</h2>

            <div class="form-group">
                <label for="usernameBox">Username</label>
                <input runat="server" class="form-control" id="usernameBox" type="text" placeholder="Username"/>
            </div>

            <div class="form-group">
                <label for="passwordBox">Password</label>
                <input runat="server" class="form-control" id="passwordBox" type="password" placeholder="Password" />
            </div>

            <button type="submit" id="btnSubmitLogin" runat="server" class="btn btn-default">Login</button>


        </form>
        
        <br />
        <p>Don't yet have an account? Create one <a href="NewEnquiry.aspx">here</a>!</p>

    <h3 runat="server" id="loginErrorText"></h3>
    </div>

        
</body>
</html>
