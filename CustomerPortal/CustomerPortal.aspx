<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerPortal.aspx.vb" Inherits="CustomerPortal.CustomerPortal" %>

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
    <div class="container">
        <h2>Customer Portal</h2>
        <form runat="server">
        <h3 id="notLoggedIn" runat="server" />
        <div runat="server" id="MainContent" visible="false">             
            <button runat="server" id="btnLogout" type="submit" class="btn btn-default" >Logout</button>
            <hr />
                <ul class="nav nav-tabs">
                    <li class="active"><a data-toggle="tab" href="#tabs-CustomerDetails" >Your Details</a></li>
                    <li><a data-toggle="tab" href="#tabs-ViewAgreements">View Agreements & Payments</a></li>
                    <li><a data-toggle="tab" href="#tabs-ViewApplications">View Applications & Statuses</a></li>
                </ul>
                

            <div class="tab-content" >
                <div id="tabs-CustomerDetails" class="tab-pane fade in active">
                    <dl class="dl-horizontal">
                        <dt>Name</dt>
                        <dd id="CustName" runat="server" />
                        
                        <dt>Address</dt>
                        <dd id="CustAddress" runat="server" />
                        
                        <dt>Email</dt>
                        <dd id="CustEmail" runat="server" />
                        
                        <dt>Primary Number</dt>
                        <dd id="CustTel1" runat="server" />

                        <dt>Secondary Number</dt>
                        <dd id="CustTel2" runat="server" />
                    </dl>

                </div>

                <div id="tabs-ViewAgreements" class="tab-pane fade">
                    <h3 runat="server" id="NoAgreementTxt" >You have no active agreements</h3>
                    <ul id="AgreementList" runat="server" class="list-unstyled">

                    </ul>
                </div>

                <div id="tabs-ViewApplications" class="tab-pane fade">
                    <h3 runat="server" id="NoApplicationsTxt" >You have no applications</h3>

                    <ul id="ApplicationsList" runat="server" class="list-unstyled">
                
                    </ul>
                </div>
            </div>


            
        </div>
    <h4 runat="server" id="ErrorText"></h4>
        
        <br />
            <hr />
        <br />
    </form>
    </div>
    
    
</body>
</html>
