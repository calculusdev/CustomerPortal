Public Class Login
    Inherits System.Web.UI.Page
    Private NCSoapClient As NCWebAPI.ProductsAndOrderingSoapClient
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NCSoapClient = New NCWebAPI.ProductsAndOrderingSoapClient()
    End Sub

    Private Sub LoginSubmit() Handles btnSubmitLogin.ServerClick
        Try
            Dim validateLoginReply As NCWebAPI.ValidateLoginReply = NCSoapClient.ValidateLogin("NCompass", "34-0100-9999", usernameBox.Value, passwordBox.Value)
            If validateLoginReply.ReturnCode = 0 Then
                Dim expiration = DateTime.Now.AddMinutes(30)
                Dim ticket = New FormsAuthenticationTicket(1, "NCCustomerCode", Date.Now, Date.Now.AddMinutes(30), True, CStr(validateLoginReply.CustomerCode) + ",0")
                Dim encrypted = FormsAuthentication.Encrypt(ticket)
                Dim cookie = New HttpCookie("NCCustomerAuth", encrypted)
                cookie.Expires = expiration
                cookie.Domain = ".findesolutions.com"
                Response.Cookies.Add(cookie)


                Response.Redirect("CustomerPortal.aspx")
            Else
                loginErrorText.InnerText = "Unsuccessful Login. " + validateLoginReply.ErrorMessage
            End If
        Catch ex As Exception
            loginErrorText.InnerText = "Unsuccessful Login. " + ex.Message
        End Try

    End Sub

End Class