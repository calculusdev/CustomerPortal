Public Class BookVisit
    Inherits System.Web.UI.Page
    Private NCSoapClient As NCWebAPI.ProductsAndOrderingSoapClient
    Private myCustomerCode As String
    Private myApplicationCode As String
    Private mySystemID As String = "NCompass"
    Private mySystemCode As String = "34-0100-9999"
    Private myUsername As String = "Demo"
    Private myPassword As String = "Demo"
    Private myEngineerToBook As Long = 1
    Private myAvailableDates As List(Of NCWebAPI.DeliveryDateSuggestion)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NCSoapClient = New NCWebAPI.ProductsAndOrderingSoapClient()
        If CheckAuth() Then
            'LoadAvailabilityForWeek(DateTime.Now)
            If Request.QueryString.Get("ApplicationCode") IsNot Nothing Then
                myApplicationCode = Request.QueryString.Get("ApplicationCode")
                LoadAvailableDays(7)
            Else
                Response.Redirect("CustomerPortal.aspx")
            End If
        End If
    End Sub

    Private Sub LoadAvailableDays(ByVal NumOfDays As Integer)

        Dim CustDetailsReply As NCWebAPI.GetCustomerDetailsReply = NCSoapClient.GetCustomerDetails(mySystemID, mySystemCode, myUsername, myPassword, myCustomerCode)

        If CustDetailsReply.ReturnCode = 0 Then
            Dim AvailabilityReply As NCWebAPI.Get_FieldCalls_AvailabilityList_Reply = NCSoapClient.Get_FieldCalls_AvailabilityList(mySystemID, mySystemCode, myUsername, myPassword, CustDetailsReply.CustomerData.PostCode, DateNumNow(), 31, 1, NumOfDays, False, True, -1, -1)
            If AvailabilityReply.ReturnCode = 0 Then
                myAvailableDates = AvailabilityReply.AvailableDates.ToList()

                For Each DateSuggest In myAvailableDates
                    Dim newOption As New ListItem
                    newOption.Text = DateNumToDateString(DateSuggest.DateNum)
                    newOption.Value = DateSuggest.DateNum
                    AvailableDays.Items.Add(newOption)
                Next
            Else
                InfoText.InnerText = "Error getting availability list. " + AvailabilityReply.ErrorMessage
            End If
        Else
            InfoText.InnerText = "Error getting customer details. " + CustDetailsReply.ErrorMessage
        End If

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

    Private Sub btnBook_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBook.ServerClick
        'Do some stuff
        Try
            Dim BookForDateNum As Long = AvailableDays.SelectedItem.Value
            Dim objReply = NCSoapClient.CustomerApplication_ScheduleDelivery_Agreements(mySystemID, mySystemCode, myUsername, myPassword, myCustomerCode, myApplicationCode, myEngineerToBook, -1, BookForDateNum, 7, 43200, BookForDateNum, 43200, False, "Booked from Customer Portal - Please Install")
            If objReply.ReturnCode = 0 Then
                btnBook.Visible = False
                InfoText.InnerText = "Your booking has been received. You will receive confirmation by email once this has been confirmed."
            Else
                InfoText.InnerText = "Error sending booking. " + objReply.ErrorMessage
            End If
        Catch ex As Exception
            InfoText.InnerText = "Error sending booking. " + ex.StackTrace
        End Try


    End Sub

    Private Sub btnReturn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReturn.ServerClick
        Response.Redirect("CustomerPortal.aspx")
    End Sub

    Private Function DateNumNow() As Long
        Return DateTimeToDateNum(DateTime.Now)
    End Function

    Private Function DateTimeToDateNum(ByVal dt As Date) As Long
        Return dt.Subtract(DateTime.Parse("1/1/1900")).TotalDays
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