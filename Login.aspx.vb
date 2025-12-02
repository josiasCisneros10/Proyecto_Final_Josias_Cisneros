Imports Proyecto_Final_Josias_Cisneros.Utils

Public Class Login
    Inherits System.Web.UI.Page

    Protected loginDb As New dbLogin()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnIniciarSesion_Click(sender As Object, e As EventArgs) Handles btnIniciarSesion.Click
        Dim usuario As String = txtUsuario.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If String.IsNullOrWhiteSpace(usuario) OrElse String.IsNullOrWhiteSpace(password) Then
            ShowSwalError(Me, "Debe ingresar usuario y contraseña")
            Return
        End If

        Dim encrypter As New Simple3Des("MiClaveSecreta123")
        Dim passwordEncriptada As String = encrypter.EncryptData(password)
        Dim esValido As Boolean = loginDb.ValidateLogin(usuario, passwordEncriptada)

        If esValido Then

            Dim userObj As Usuario = DirectCast(loginDb.GetUser(usuario), Usuario)
            Session("Usuario") = userObj
            Session("Rol") = userObj.Rol

            If userObj.Rol = "2" Then
                Response.Redirect("Admin.aspx")
            Else
                Response.Redirect("Home.aspx")
            End If
        Else
            ShowSwalError(Me, "Credenciales incorrectas")
        End If
    End Sub
End Class
