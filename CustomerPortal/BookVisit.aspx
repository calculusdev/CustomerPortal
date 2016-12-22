<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BookVisit.aspx.vb" Inherits="CustomerPortal.BookVisit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>

<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>

<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>

<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
</head>
<body>
    <div id="BookVisitContainer" class="container">
        <form id="form1" runat="server">
        <h3>Book a Fitting</h3>

            <div class="form-group">
                <label for="AvailableDays">Next 7 Available Days</label>
                <asp:dropdownlist CssClass="form-control" runat="server" id="AvailableDays" />
            </div>

            <button runat="server" class="btn btn-default" type="submit" id="btnBook" >Confirm Booking</button>
        
            <button runat="server" class="btn btn-default" type="submit" id="btnReturn" >Return to Portal</button>

        

        <h4 runat="server" id="InfoText" />
    </form>
    </div>
    
</body>
</html>
