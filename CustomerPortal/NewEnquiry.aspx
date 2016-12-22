<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewEnquiry.aspx.vb" Inherits="CustomerPortal.NewEnquiry" %>

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
        <form id="form1" runat="server">
    <h3>Create an Account</h3>

            <h3 id="InfoText" runat="server" />

            <div id="MainContent" runat="server">
                <div class="form-group">
            <label for="EnquiryTitle">Title</label>
            <asp:DropDownList CssClass="form-control" runat="server" id="EnquiryTitle">
                        <asp:ListItem Value="Mr">Mr</asp:ListItem>
                        <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                        <asp:ListItem Value="Ms">Ms</asp:ListItem>
                        <asp:ListItem Value="Miss">Miss</asp:ListItem>
                    </asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="EnquiryForename">Forename *</label>
            <input class="form-control" type="text" id="EnquiryForename" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquirySurname">Surname *</label>
            <input class="form-control" type="text" id="EnquirySurname" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryAddress1">Address Line 1 *</label>
            <input class="form-control" type="text" id="EnquiryAddress1" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryAddress2">Address Line 2</label>
            <input class="form-control" type="text" id="EnquiryAddress2" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryAddress3">Address Line 3</label>
            <input class="form-control" type="text" id="EnquiryAddress3" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryAddress4">City *</label>
            <input class="form-control" type="text" id="EnquiryAddress4" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryAddress5">County *</label>
            <input class="form-control" type="text" id="EnquiryAddress5" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryPostcode">Postal Code *</label>
            <input class="form-control" type="text" id="EnquiryPostcode" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryTel1">Main Contact Number *</label>
            <input class="form-control" type="text" id="EnquiryTel1" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryTel2">Secondary Contact Number</label>
            <input class="form-control" type="text" id="EnquiryTel2" runat="server" />
        </div>

            <div class="form-group">
                <label for="AvailableDays">Preferred Visit Date *</label>
                <button class="btn btn-default" id="btnLoadDays" runat="server" >Load Available Days</button>
                <asp:DropDownList runat="server" ID="AvailableDays" CssClass="form-control">
                    <asp:ListItem Text="Click Load" />
                </asp:DropDownList>
            </div>

        <div class="form-group">
            <label for="EnquiryEmail">Email Address (this will be your username) *</label>
            <input class="form-control" type="text" id="EnquiryEmail" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryPassword">Requested Password *</label>
            <input class="form-control" type="password" id="EnquiryPassword" runat="server" />
        </div>

        <div class="form-group">
            <label for="EnquiryMainText">Enquiry *</label>
            <textarea class="form-control" id="EnquiryMainText"  runat="server"/>            
        </div>

            

        <button class="btn btn-default" type="submit" id="btnSubmit" runat="server">Submit Enquiry</button>

        <br />
            <br />
            </div>

        

    </form>
    </div>
    
</body>
</html>
