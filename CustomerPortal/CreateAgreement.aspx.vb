Public Class CreateAgreement
    Inherits System.Web.UI.Page
    Private NCSoapClient As NCWebAPI.ProductsAndOrderingSoapClient
    Private myCustomerCode As Long
    Private myApplicationCode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        NCSoapClient = New NCWebAPI.ProductsAndOrderingSoapClient()

        If CheckAuth() Then
            If Request.QueryString.Get("ApplicationCode") IsNot Nothing Then
                myApplicationCode = Request.QueryString.Get("ApplicationCode")
                CreateAgreement_StartChargesFrom.Value = DateTime.Today.ToString("dd/MM/yyyy")
            End If
        Else
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Private Sub CreateAgreement_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateAgreement.ServerClick
        Try
            SendCreateAgreementRequest()
        Catch ex As Exception
            CreateAgreement_Result.InnerText = "Unexpected Error sending request. " + ex.Message
        End Try
    End Sub

    Private Sub btnGoBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoBack.ServerClick
        Response.Redirect("CustomerPortal.aspx")
    End Sub

    Private Sub SendCreateAgreementRequest()
        'Generate the request and send it
        Dim objAgreementLines As New List(Of NCWebAPI.RentalAgreementLine)

        If CreateAgreement_CablingQty.Value <> "" AndAlso CInt(CreateAgreement_CablingQty.Value) > 0 Then
            Dim objCarpetLine = New NCWebAPI.RentalAgreementLine With {
            .ListedItemCode = CreateAgreement_Cabling.Value,
            .SpecificItemCode = -999,
            .Quantity = CreateAgreement_CablingQty.Value,
            .TotalLinePrice = CreateAgreement_CablingValue.Value
        }
            objAgreementLines.Add(objCarpetLine)
        End If


        If CreateAgreement_PointsQty.Value <> "" AndAlso CInt(CreateAgreement_PointsQty.Value) > 0 Then
            Dim objPointsLine = New NCWebAPI.RentalAgreementLine With {
            .ListedItemCode = CreateAgreement_Points.Value,
            .SpecificItemCode = -999,
            .Quantity = CreateAgreement_PointsQty.Value,
            .TotalLinePrice = CreateAgreement_PointsValue.Value
        }
            objAgreementLines.Add(objPointsLine)
        End If

        If CreateAgreement_SundriesQty.Value <> "" AndAlso CInt(CreateAgreement_SundriesQty.Value) > 0 Then
            Dim objMiscLine = New NCWebAPI.RentalAgreementLine With {
            .ListedItemCode = CreateAgreement_Sundries.Value,
            .SpecificItemCode = -999,
            .Quantity = CreateAgreement_SundriesQty.Value,
            .TotalLinePrice = CreateAgreement_SundriesValue.Value
        }
            objAgreementLines.Add(objMiscLine)
        End If




        Dim objRequest As New NCWebAPI.CreateNewRentalAgreementRequest With {
            .AgreementNumber = CreateAgreement_AgreementNumber.Value,
            .CustomerCode = myCustomerCode,
            .NewCustomerCode = -999,
            .InstallCustomerCode = myCustomerCode,
            .InstallNewCustomerCode = -999,
            .BranchCode = 1,
            .SalespersonCode = 1,
            .StartDateNum = DateNumNow(),
            .FirstDebitDateNum = DateStringToDateNum(CreateAgreement_StartChargesFrom.Value),
            .FirstPaymentDueDateNum = DateStringToDateNum(CreateAgreement_StartChargesFrom.Value),
            .PaymentMethodType = 1,
            .PaymentBranchCode = 1,
            .BranchPreferredPaymentMethodCode = 1,
            .FrequencyCode = CreateAgreement_InitFrequency.SelectedItem.Value,
            .OpeningCharge = 0,
            .CurrentRate = CreateAgreement_InitRate.Value,
            .AdminFeeInc = 0,
            .AgreementCategoryCode = -1,
            .blUseSpecialChargeMode = True,
            .SpecialCharge_FollowOnRate = CreateAgreement_FollowOnRate.Value,
            .SpecialCharge_FollowOnFrequency = CreateAgreement_FollowOnFrequency.SelectedItem.Value,
            .SpecialCharge_InitialChargesStartDateNum = DateStringToDateNum(CreateAgreement_StartChargesFrom.Value),
            .SpecialCharge_InitialQuantity = CreateAgreement_NumPayments.Value,
            .SpecialCharge_OpeningCharge = CreateAgreement_OpeningCharge.Value,
            .AgreementLinesToAdd = objAgreementLines.ToArray(),
            .RegisteredCardCode = -999,
            .BlockDiscountCode = -999,
            .LinkedCustomerApplicationCode = myApplicationCode
            }

        Dim objReply As NCWebAPI.CreateNewRentalAgreementReply = NCSoapClient.CreateNewRentalAgreement("NCompass", "34-0100-9999", "Demo", "Demo", objRequest)

        If objReply.ReturnCode = 0 Then
            CreateAgreement_Result.InnerHtml = "Agreement " + objReply.AgreementNumber + " Created Successfully<br/>Click <a href='http://ncmobile.com/termsconditions'>here</a> to sign Terms and Conditions"
            form1.Visible = False
        Else
            CreateAgreement_Result.InnerText = objReply.ReturnCode + " Error creating agreement. Error: " + objReply.ErrorMessage
        End If
    End Sub

    Private Sub btnRecalculate_Click() Handles btnRecalculate.ServerClick
        Dim total As Decimal = 0.00

        If CreateAgreement_CablingQty.Value <> "" AndAlso CInt(CreateAgreement_CablingQty.Value) > 0 Then
            total += CreateAgreement_CablingValue.Value
        End If


        If CreateAgreement_PointsQty.Value <> "" AndAlso CInt(CreateAgreement_PointsQty.Value) > 0 Then
            total += CreateAgreement_PointsValue.Value
        End If

        If CreateAgreement_SundriesQty.Value <> "" AndAlso CInt(CreateAgreement_SundriesQty.Value) > 0 Then
            total += CreateAgreement_SundriesValue.Value
        End If

        total = total / CInt(CreateAgreement_NumPayments.Value)

        CreateAgreement_InitRate.Value = total
    End Sub

    Private Function CheckAuth() As Boolean
        Try
            Dim cookie As HttpCookie = Request.Cookies("NCCustomerAuth")
            If cookie IsNot Nothing Then
                myCustomerCode = FormsAuthentication.Decrypt(cookie.Value).UserData.Split(",")(0)
                Return True
            Else
                Response.Redirect("Login.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Login.aspx")
        End Try
        Return False
    End Function

    Private Function DateNumNow() As Long
        Return DateTime.Now.Subtract(DateTime.Parse("1/1/1900")).TotalDays
    End Function

    Private Function DateStringToDateNum(ByVal dateStr As String) As Long
        Return DateTime.Parse(dateStr).Subtract(DateTime.Parse("1/1/1900")).TotalDays
    End Function

End Class