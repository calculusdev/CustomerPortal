Public Class AutoLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString.Get("CustomerCode") IsNot Nothing AndAlso Request.QueryString.Get("CustomerCode").Length > 0 Then
            SetCookie(Request.QueryString.Get("CustomerCode"))
        Else
            Response.Redirect("Login.aspx")
        End If

    End Sub

    Private Sub SetCookie(ByVal CustomerCode As String)
        Dim expiration = DateTime.Now.AddMinutes(30)
        Dim ticket = New FormsAuthenticationTicket(1, "NCCustomerCode", Date.Now, Date.Now.AddMinutes(30), True, CustomerCode + ",1")
        Dim encrypted = FormsAuthentication.Encrypt(ticket)
        Dim cookie = New HttpCookie("NCCustomerAuth", encrypted)
        cookie.Expires = expiration
        Response.Cookies.Add(cookie)

        Response.Redirect("CustomerPortal.aspx")
    End Sub

End Class