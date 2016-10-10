Public Class CustomerPortal
    Inherits System.Web.UI.Page
    Private NCSoapClient As NCWebAPI.ProductsAndOrderingSoapClient
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        NCSoapClient = New NCWebAPI.ProductsAndOrderingSoapClient()

    End Sub

End Class