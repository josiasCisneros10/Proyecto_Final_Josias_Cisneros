Public Class Pedidos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim usuario = TryCast(Session("Usuario"), Usuario)
        If usuario Is Nothing OrElse usuario.Rol <> 2 Then
            Response.Redirect("~/Login.aspx")
            Return
        End If
    End Sub
End Class