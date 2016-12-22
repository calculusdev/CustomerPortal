Public Class NewEnquiry
    Inherits System.Web.UI.Page
    Private NCSoapClient As NCWebAPI.ProductsAndOrderingSoapClient
    Private mySystemID As String = "NCompass"
    Private mySystemCode As String = "34-0100-9999"
    Private myUsername As String = "Demo"
    Private myPassword As String = "Demo"
    Private myAvailableDates As List(Of NCWebAPI.DeliveryDateSuggestion)
    Private myEngineerToBook As Long = 1
    Private myStartSalespersonCode As Long = 1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NCSoapClient = New NCWebAPI.ProductsAndOrderingSoapClient()
    End Sub

    Private Sub LoadAvailableDays(ByVal NumOfDays As Integer, ByVal Postcode As String)

        Dim AvailabilityReply As NCWebAPI.Get_FieldCalls_AvailabilityList_Reply = NCSoapClient.Get_FieldCalls_AvailabilityList(mySystemID, mySystemCode, myUsername, myPassword, Postcode, DateNumNow(), 31, 1, NumOfDays, False, True, -1, -1)
        If AvailabilityReply.ReturnCode = 0 Then
            myAvailableDates = AvailabilityReply.AvailableDates.ToList()
            AvailableDays.Items.Clear()
            For Each DateSuggest In myAvailableDates
                Dim newOption As New ListItem
                newOption.Text = DateNumToDateString(DateSuggest.DateNum)
                newOption.Value = DateSuggest.DateNum
                AvailableDays.Items.Add(newOption)
            Next
        Else
            InfoText.InnerText = "Error getting availability list. " + AvailabilityReply.ErrorMessage
        End If

    End Sub

    Private Sub EnquirySubmit() Handles btnSubmit.ServerClick
        InfoText.InnerText = ""
        If CheckFields() Then

            Try
                Dim NewCustCode = RequestNewCustomerCode()
                Try
                    Dim Response = CreateApplication(NewCustCode)
                    Dim ApplicationCode = Response.ApplicationCode
                    Dim CustomerCode = Response.CustomerCode

                    'Then need to book a SJ and FieldCall to see the customer.
                    Try
                        Dim ServiceJobCode = CreateServiceJobForApplication(CustomerCode, ApplicationCode)

                        Try
                            Dim FieldCallCode = BookFieldCallForServiceJob(ServiceJobCode)

                            InfoText.InnerHtml = "<i>Your enquiry has been successfully sent. You can now log in <a href='Login.aspx'>here</a> to view the status of your application.</i>"
                            MainContent.Visible = False
                        Catch ex As Exception
                            InfoText.InnerText = "Unexpected error in BookFieldCallForServiceJob. Details: " + ex.Message
                        End Try


                    Catch ex As Exception
                        InfoText.InnerText = "Unexpected error in CreateServiceJobForApplication. Details: " + ex.Message
                    End Try


                Catch ex As Exception
                    InfoText.InnerText = "Unexpected error in CreateApplication. Details: " + ex.Message
                End Try


            Catch ex As Exception
                InfoText.InnerText = "Unexpected error in RequestNewCustomer. Details: " + ex.Message
            End Try
        Else
            InfoText.InnerText = "One or more required fields marked with a * have been left empty. Please double check and try again."
        End If


        Dim applicationRequest As New NCWebAPI.CreateNewCustomerAgreementApplicationRequest


    End Sub

    Private Function BookFieldCallForServiceJob(ByVal ServiceJobCode As Long) As Long
        Dim BookForDate As Long = AvailableDays.SelectedItem.Value
        Dim objReply As NCWebAPI.ServiceJob_ScheduleDelivery_Reply = NCSoapClient.ServiceJob_ScheduleDelivery(mySystemID, mySystemCode, myUsername, myPassword, ServiceJobCode, myEngineerToBook, -1, BookForDate, 7, 43200, BookForDate, 43200, False, "Booked from Customer Portal")

        If objReply.ReturnCode = 0 Then
            Return objReply.FieldCallCode
        Else
            Throw New Exception(objReply.ErrorMessage)
        End If
    End Function

    Private Function CreateServiceJobForApplication(ByVal CustomerCode As Long, ByVal ApplicationCode As Long) As Long
        Dim objRequest As New NCWebAPI.CreateServiceJobRequestDetails With {
            .JobEntry_CategoryCode = -1,
            .JobEntry_Charge = 0,
            .JobEntry_FullText = "Created from Customer Portal",
            .JobEntry_HasCharge = False,
            .JobEntry_IncludeOnJobReport = True,
            .JobToCreate = New NCWebAPI.WSServiceJob With {
                .BranchCode = 1,
                .CustomerRef = "Customer Portal",
                .CustomerCode = CustomerCode,
                .FaultDesc = "Perform Initial Visit",
                .ShortFaultDesc = "Perform Initial Visit",
                .LeadEngineerCode = myEngineerToBook,
                .SICode = -999,
                .StartDateNum = DateNumNow(),
                .StartSalespersonCode = myStartSalespersonCode,
                .StartTimeNum = TimeNumNow(),
                .LinkedCustomerApplicationCode = ApplicationCode,
                .StatusCode = 100
            }
        }

        Dim objreply As NCWebAPI.CreateServiceJobReply = NCSoapClient.CreateServiceJob(mySystemID, mySystemCode, myUsername, myPassword, objRequest)

        If objreply.ReturnCode = 0 Then
            Return objreply.ServiceJobCode
        Else
            Throw New Exception(objreply.ErrorMessage)
        End If
    End Function

    Private Sub btnLoadDays_Click() Handles btnLoadDays.ServerClick
        If EnquiryPostcode.Value.Length >= 6 Then
            LoadAvailableDays(7, EnquiryPostcode.Value)
        Else
            Response.Write("<script>alert('Please enter a valid postcode')</script>")
        End If
    End Sub

    Private Function CreateApplication(ByVal NewCustomerCode As Long) As NCWebAPI.CreateNewCustomerAgreementApplicationReply
        Dim applicationRequest As New NCWebAPI.CreateNewCustomerAgreementApplicationRequest With {
            .AgreementApplication = New NCWebAPI.CustomerAgreementApplication With {
                .CustomerCode = -999,
                .NewCustomerCode = NewCustomerCode,
                .OpeningComment = EnquiryMainText.Value
            }
        }

        Dim applicationResponse As NCWebAPI.CreateNewCustomerAgreementApplicationReply = NCSoapClient.CreateNewCustomerApplication("NCompass", "34-0100-9999", "Demo", "Demo", applicationRequest)

        If applicationResponse.ReturnCode = 0 Then
            Return applicationResponse
        Else
            Throw New Exception(applicationResponse.ErrorMessage)
        End If


    End Function

    Private Function RequestNewCustomerCode() As Long


        Dim reply As NCWebAPI.CustomerReply = NCSoapClient.RequestNewCustomer("NCompass", "34-0100-9999", "Demo", "Demo", EnquiryTitle.SelectedItem.Value, "", EnquiryForename.Value, EnquirySurname.Value, EnquiryAddress1.Value, EnquiryAddress2.Value, EnquiryAddress3.Value, EnquiryAddress4.Value, EnquiryAddress5.Value, EnquiryPostcode.Value, EnquiryTel1.Value, EnquiryEmail.Value, EnquiryTel2.Value, EnquiryPassword.Value, "", False, False)

        If reply.ReturnCode = 0 Then
            Return reply.CustomerCode
        Else
            Throw New Exception(reply.ErrorMessage)
        End If
    End Function

    Private Function CheckFields() As Boolean
        If EnquiryForename.Value = "" OrElse EnquirySurname.Value = "" OrElse EnquiryAddress1.Value = "" OrElse EnquiryAddress4.Value = "" OrElse EnquiryAddress5.Value = "" OrElse EnquiryPostcode.Value = "" OrElse EnquiryTel1.Value = "" OrElse EnquiryEmail.Value = "" OrElse EnquiryPassword.Value = "" OrElse EnquiryMainText.Value = "" Then
            Return False
        End If

        Return True
    End Function
    Private Function DateNumNow() As Long
        Return DateTimeToDateNum(DateTime.Now)
    End Function

    Private Function TimeNumNow() As Long
        Return (DateTime.Now - DateTime.Today).TotalSeconds
    End Function

    Private Function DateTimeToDateNum(ByVal dt As Date) As Long
        Return dt.Subtract(DateTime.Parse("1/1/1900")).TotalDays
    End Function

    Private Function DateNumToDateString(ByVal DateNum As Long) As String
        Return DateTime.Parse("1/1/1900").AddDays(DateNum).ToString("dd/MM/yyyy")
    End Function


End Class