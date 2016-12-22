<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreateAgreement.aspx.vb" Inherits="CustomerPortal.CreateAgreement" %>

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
    <div id="CreateAgreementContainer" class="container">
        <h3>Create a New Rental Agreement</h3>
        <h3 runat="server" id="CreateAgreement_Result" />
        <form id="form1" runat="server">

            <div class="form-group">
                <label for="CreateAgreement_AgreementNumber">Agreement Number (leave blank for AutoGenerate)</label>
                <input type="text" id="CreateAgreement_AgreementNumber" runat="server" class="form-control"/>
            </div>
            <hr />
                <h4>Add Cabling</h4>
                                   
                <input type="checkbox" visible="false" value="100000846" checked="checked" id="CreateAgreement_Cabling" runat="server" />
                <div class="form-group">
                    <label for="CreateAgreement_CablingQty">Quantity (metres)</label>
                    <input type="text" id="CreateAgreement_CablingQty" value="10" runat="server" class="form-control" />
                        
                    <label for="CreateAgreement_CablingValue">Total Value (£)</label>
                    <input type="text" id="CreateAgreement_CablingValue" value="50" runat="server" class="form-control" />

                </div>
                <hr />

            <h4>Add Network Points</h4>

            <input type="checkbox" visible="false" value="100000849" checked="checked" id="CreateAgreement_Points" runat="server" />
            <div class="form-group">
                <label for="CreateAgreement_PointsQty">Number of new Points Required</label>
                    <input type="text" id="CreateAgreement_PointsQty" value="4" runat="server" class="form-control" />
                        
                    <label for="CreateAgreement_PointsValue">Total Value (£)</label>
                    <input type="text" id="CreateAgreement_PointsValue" value="60" runat="server" class="form-control" />
            </div>
            <hr />

            <h4>Add Sundries</h4>
                <input type="checkbox" visible ="false" value="100000847" checked="checked" id="CreateAgreement_Sundries" runat="server" />
                <div class="form-group">
                    <label for="CreateAgreement_SundriesQty">Quantity</label>
                    <input type="text" id="CreateAgreement_SundriesQty" value="10" runat="server" class="form-control" />

                    <label for="CreateAgreement_SundriesValue">Total Value (£)</label>
                    <input type="text" id="CreateAgreement_SundriesValue" value="10" runat="server" class="form-control" />
                </div>                               
            <hr />

            <div class="form-group">
                <label for="CreateAgreement_OpeningCharge">Opening Charge</label>
                <input type="text" id="CreateAgreement_OpeningCharge" value="0" runat="server" class="form-control" />
            </div>

            <div class="form-group">
                <label for="CreateAgreement_StartChargesFrom">Start Charges From</label>
                <input type="text" value="12/10/2016" id="CreateAgreement_StartChargesFrom" runat="server" class="form-control" />
            </div>

            <div class="form-group">
                <label for="CreateAgreement_InitFrequency">Initial Frequency</label>
                <asp:DropDownList id="CreateAgreement_InitFrequency" runat="server" class="form-control">
                            <asp:ListItem value="-1">Daily</asp:ListItem>
                            <asp:ListItem value="-2" >Weekly</asp:ListItem>
                            <asp:ListItem value="-3" selected="True">Monthly</asp:ListItem>
                            <asp:ListItem value="-4">BiMonthly</asp:ListItem>
                            <asp:ListItem value="-5">Quarterly</asp:ListItem>
                            <asp:ListItem value="-6">Six Monthly</asp:ListItem>
                            <asp:ListItem value="-7" >Annually</asp:ListItem>
                            <asp:ListItem value="-8">Fortnightly</asp:ListItem>
                            <asp:ListItem value="-9">Four Weekly</asp:ListItem>

                        </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="CreateAgreement_InitRate">Initial Rate</label>
                <button runat="server" class="btn btn-default" id="btnRecalculate" type="submit">Recalculate Rate</button>
                <input type="text" id="CreateAgreement_InitRate" value="10" runat="server" class="form-control" />
            </div>

            <div class="form-group">
                <label for="CreateAgreement_NumPayments">Number Of Payments</label>
                <input type="text" id="CreateAgreement_NumPayments" value="12" runat="server" class="form-control"/>
            </div>

            <div class="form-group">
                <label for="CreateAgreement_FollowOnFrequency">Follow-On Frequency</label>
                <asp:DropDownList id="CreateAgreement_FollowOnFrequency" runat="server" class="form-control">
                            <asp:ListItem value="-1" >Daily</asp:ListItem>
                            <asp:ListItem value="-2" >Weekly</asp:ListItem>
                            <asp:ListItem value="-3">Monthly</asp:ListItem>
                            <asp:ListItem value="-4">BiMonthly</asp:ListItem>
                            <asp:ListItem value="-5">Quarterly</asp:ListItem>
                            <asp:ListItem value="-6">Six Monthly</asp:ListItem>
                            <asp:ListItem value="-7" selected="True">Annually</asp:ListItem>
                            <asp:ListItem value="-8">Fortnightly</asp:ListItem>
                            <asp:ListItem value="-9">Four Weekly</asp:ListItem>

                        </asp:DropDownList>
            </div>

            
            <div class="form-group">
                <label for="CreateAgreement_FollowOnRate">Follow-On Rate</label>
                <input type="text" id="CreateAgreement_FollowOnRate" value="1" runat="server" class="form-control"/>
            </div>
            
            <hr />            

            <div class="form-group">
                <label for="btnCaptureCardDetails">Card Details</label>
                <button id="btnCaptureCardDetails" class="btn btn-default" runat="server" >Capture Card</button>
                <p class="help-block">Card Details Not Yet Captured</p>
            </div>


            <button class="btn btn-default" type="submit" id="btnCreateAgreement" runat="server">Create Agreement</button>
   
            <button class="btn btn-default" type="submit" id="btnGoBack" runat="server">Back to Portal</button>
            <br />
            <br />
            
    </form>
    </div>
    
</body>
</html>
