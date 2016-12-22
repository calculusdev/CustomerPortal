Public Class CustomerPortal
    Inherits System.Web.UI.Page
    Private NCSoapClient As NCWebAPI.ProductsAndOrderingSoapClient
    Private myCustomerCode As String
    Private isAgent As Boolean
    Private mySystemID As String = "NCompass"
    Private mySystemCode As String = "34-0100-9999"
    Private myUsername As String = "Demo"
    Private myPassword As String = "Demo"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        NCSoapClient = New NCWebAPI.ProductsAndOrderingSoapClient()
        If CheckAuth() Then
            Try
                LoadCustomerData()
                LoadActiveAgreements()
                LoadApplications()
                MainContent.Visible = True
            Catch ex As Exception
                ErrorText.InnerText = ex.Message
            End Try



        End If
    End Sub

    Private Sub LoadApplications()
        Dim applicationsRequest As New NCWebAPI.GetCustomerAgreementApplicationsRequest With {
            .ApplicationCode = -999,
            .CustomerCode = myCustomerCode
        }

        Dim applicationsResponse As NCWebAPI.GetCustomerAgreementApplicationsReply = NCSoapClient.GetCustomerAgreementApplications(mySystemID, mySystemCode, myUsername, myPassword, applicationsRequest)

        If applicationsResponse.ReturnCode = 0 Then
            If applicationsResponse.CustomerApplications.Count > 0 Then
                NoApplicationsTxt.InnerText = ""
            End If
            For Each objApplication As NCWebAPI.CustomerAgreementApplication In applicationsResponse.CustomerApplications
                Dim appLi = New HtmlGenericControl("li")
                Dim appUl = New HtmlGenericControl("ul")
                appUl.Attributes.Add("class", "list-unstyled")

                Dim appNumberLi = New HtmlGenericControl("li")
                appNumberLi.InnerHtml = "<strong>Application Number " + objApplication.ApplicationNumber + "</strong>"
                appUl.Controls.Add(appNumberLi)

                Dim appCreatedLi = New HtmlGenericControl("li")
                appCreatedLi.InnerHtml = "Created on " + DateNumToDateString(objApplication.TakenDateNum) + "<br/>"
                appUl.Controls.Add(appCreatedLi)

                Dim appStatusLi = New HtmlGenericControl("li")
                appStatusLi.InnerText = "Current Status: " + CStr(objApplication.StatusCode) + " since " + TimeNumToString(objApplication.StatusSetTimeNum) + " on " + DateNumToDateString(objApplication.StatusSetDateNum)
                appUl.Controls.Add(appStatusLi)

                If objApplication.StatusCode = -1 Then 'Change to be whatever status means delivery can be booked
                    Dim btnLi = New HtmlGenericControl("li")

                    Dim bookDeliveryBtn = New HtmlButton()
                    bookDeliveryBtn.InnerText = "Book Installation"
                    bookDeliveryBtn.ID = objApplication.ApplicationCode
                    AddHandler bookDeliveryBtn.ServerClick, AddressOf BookDelivery_ButtonClick
                    bookDeliveryBtn.Attributes.Add("class", "btn btn-default")
                    bookDeliveryBtn.Attributes.Add("type", "submit")

                    btnLi.Controls.Add(bookDeliveryBtn)
                    appUl.Controls.Add(btnLi)
                End If

                If isAgent Then

                    Dim btnAgrLi = New HtmlGenericControl("li")

                    Dim createAgrBtn = New HtmlButton()
                    createAgrBtn.InnerText = "Create Agreement"
                    createAgrBtn.ID = "ra" + CStr(objApplication.ApplicationCode)
                    AddHandler createAgrBtn.ServerClick, AddressOf CreateAgreement_ButtonClick
                    createAgrBtn.Attributes.Add("class", "btn btn-default")
                    createAgrBtn.Attributes.Add("type", "submit")
                    btnAgrLi.Controls.Add(New HtmlGenericControl("br"))
                    btnAgrLi.Controls.Add(createAgrBtn)
                    appUl.Controls.Add(btnAgrLi)
                End If

                appLi.Controls.Add(appUl)
                ApplicationsList.Controls.Add(appLi)
            Next

        Else
                Throw New Exception("Error getting customer applications. " + applicationsResponse.ErrorMessage)
        End If
    End Sub

    Private Sub LoadActiveAgreements()
        Dim agreementsReply As NCWebAPI.GetCustomerAgreementsReply = NCSoapClient.GetCustomerAgreements(mySystemID, mySystemCode, myUsername, myPassword, myCustomerCode)

        If agreementsReply.ReturnCode = 0 Then
            If agreementsReply.ActiveAgreements.Count > 0 Then
                NoAgreementTxt.InnerText = ""
            End If
            For Each objAgreement As NCWebAPI.NMRentalAgreement In agreementsReply.ActiveAgreements
                Dim agrli = New HtmlGenericControl("li")
                Dim agrUl = New HtmlGenericControl("ul")
                agrUl.Attributes.Add("class", "list-unstyled")


                Dim agrNumberLi = New HtmlGenericControl("li")
                agrNumberLi.InnerHtml = "<strong>Agreement Number " + objAgreement.AgreementCode + "</strong>"
                agrUl.Controls.Add(agrNumberLi)


                Dim agrBalanceLi = New HtmlGenericControl("li")
                agrBalanceLi.InnerText = "Current Balance: £" + CStr(objAgreement.CurrentBalance)
                agrUl.Controls.Add(agrBalanceLi)

                Dim agrOverdueLi = New HtmlGenericControl("li")
                agrOverdueLi.InnerText = "Over Payments Total: £0"
                agrUl.Controls.Add(agrOverdueLi)

                Dim agrPaymentsLiHeader = New HtmlGenericControl("li")
                agrPaymentsLiHeader.InnerHtml = "<strong>Recent Payments</strong>"
                If objAgreement.RecentPayments.Count = 0 Then
                    agrPaymentsLiHeader.InnerHtml = "<strong>No Recent Payments</strong>"
                End If
                agrUl.Controls.Add(agrPaymentsLiHeader)

                Dim agrPaymentsLi = New HtmlGenericControl("li")
                agrPaymentsLi.Controls.Add(GeneratePaymentsListForAgreement(objAgreement))
                agrUl.Controls.Add(agrPaymentsLi)

                Dim agrTakePaymentLi = New HtmlGenericControl("li")
                Dim agrTakePaymentBtn = New HtmlButton
                agrTakePaymentBtn.Attributes.Add("class", "btn btn-default")
                agrTakePaymentBtn.InnerText = "Take Payment"
                agrTakePaymentBtn.Disabled = True
                agrTakePaymentLi.Controls.Add(agrTakePaymentBtn)
                agrUl.Controls.Add(agrTakePaymentLi)

                agrli.Controls.Add(agrUl)
                AgreementList.Controls.Add(agrli)
            Next
        Else
            Throw New Exception("Error getting customer agreements. " + agreementsReply.ErrorMessage)
        End If
    End Sub

    Private Function GeneratePaymentsListForAgreement(ByVal objAgreement As NCWebAPI.NMRentalAgreement) As HtmlGenericControl
        Dim paymentsul = New HtmlGenericControl("ul")

        For Each objPayment As NCWebAPI.NMAgreementPayment In objAgreement.RecentPayments
            Dim paymentLi = New HtmlGenericControl("li")
            paymentLi.InnerText = "£" + CStr(objPayment.PaidAmount) + " by " + objPayment.PaymentMethod + " received on " + DateNumToDateString(objPayment.PaidDateNum)
            paymentsul.Controls.Add(paymentLi)
        Next

        Return paymentsul
    End Function

    Private Sub LoadCustomerData()
        Dim custReply As NCWebAPI.GetCustomerDetailsReply = NCSoapClient.GetCustomerDetails(mySystemID, mySystemCode, myUsername, myPassword, myCustomerCode)

        If custReply.ReturnCode = 0 Then
            CustName.InnerText = custReply.CustomerData.NameField

            CustAddress.InnerHtml = custReply.CustomerData.Address1 + "<br/>" + custReply.CustomerData.Address2 + "<br/>" + custReply.CustomerData.Address3 + "<br/>" + custReply.CustomerData.Address4 + "<br/>" + custReply.CustomerData.Address5 + "<br/>" + custReply.CustomerData.PostCode

            CustEmail.InnerText = custReply.CustomerData.EmailAddress
            CustTel1.InnerText = custReply.CustomerData.Telephone1
            CustTel2.InnerText = custReply.CustomerData.Telephone2

        Else
            Throw New Exception("Error getting customer details. " + custReply.ErrorMessage)
        End If
    End Sub

    Private Function CheckAuth() As Boolean
        Try
            Dim cookie As HttpCookie = Request.Cookies("NCCustomerAuth")
            If cookie IsNot Nothing Then
                myCustomerCode = FormsAuthentication.Decrypt(cookie.Value).UserData.Split(",")(0)
                isAgent = Convert.ToBoolean(CInt(FormsAuthentication.Decrypt(cookie.Value).UserData.Split(",")(1)))
                Return True
            Else
                Response.Redirect("Login.aspx")
                'ErrorText.InnerText = "Error1?"
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
            'ErrorText.InnerText = "Error2?"
        End Try
        Return False
    End Function

    Private Sub BookDelivery_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        'This will need to book delivery for customerapplicationcode. This functionality doesn't yet exist

        'Maybe just take them to a page to start with booking availability, but this doesn't look like it exists either. GetEngineerList?
        Dim appCode = CType(sender, HtmlButton).ID
        Response.Redirect("BookVisit.aspx?ApplicationCode=" + appCode)

    End Sub

    Private Sub CreateAgreement_ButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim appCode = CType(sender, HtmlButton).ID.Replace("ra", "")
        Response.Redirect("CreateAgreement.aspx?ApplicationCode=" + appCode)
    End Sub

    Private Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.ServerClick
        If Request.Cookies("NCCustomerAuth") IsNot Nothing Then
            Request.Cookies("NCCustomerAuth").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies.Add(Request.Cookies("NCCustomerAuth"))
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Private Function DateNumNow() As Long
        Return DateTime.Now.Subtract(DateTime.Parse("1/1/1900")).TotalDays
    End Function

    Private Function DateStringToDateNum(ByVal dateStr As String) As Long
        Return DateTime.Parse(dateStr).Subtract(DateTime.Parse("1/1/1900")).TotalDays
    End Function

    Private Function DateNumToDateString(ByVal DateNum As Long) As String
        Return DateTime.Parse("1/1/1900").AddDays(DateNum).ToString("dd/MM/yyyy")
    End Function

    Private Function TimeNumToString(ByVal TimeNum As Long) As String
        Return DateTime.Today.AddSeconds(TimeNum).ToString("HH:mm")
    End Function


End Class