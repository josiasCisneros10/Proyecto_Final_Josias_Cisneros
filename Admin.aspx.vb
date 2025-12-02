Public Class Admin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim user = TryCast(Session("Usuario"), Usuario)
        If user Is Nothing OrElse user.Rol <> 2 Then
            Response.Redirect("~/Login.aspx")
            Return
        End If
        Dim Usuario As Usuario = Session("Usuario")
        lblUsuario.Text = Usuario.NombreUsuario
        lblEmail.Text = Usuario.Email
    End Sub
End Class
