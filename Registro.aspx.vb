Imports Proyecto_Final_Josias_Cisneros.Utils

Public Class Registro
    Inherits System.Web.UI.Page

    Protected loginDb As New dbLogin()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs)

        Dim nombreUsuario As String = txtUsuario.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()
        Dim confirmPassword As String = txtConfirmarPassword.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()

        If String.IsNullOrWhiteSpace(nombreUsuario) OrElse
           String.IsNullOrWhiteSpace(email) OrElse
           String.IsNullOrWhiteSpace(password) OrElse
           String.IsNullOrWhiteSpace(confirmPassword) Then

            ShowSwalError(Me, "Debes completar todos los campos")
            Return
        End If

        If password <> confirmPassword Then
            ShowSwalError(Me, "Las contraseñas no coinciden")
            Return
        End If

        Dim encrypter As New Simple3Des("MiClaveSecreta123")
        Dim passEncrypt As String = encrypter.EncryptData(password)

        Dim nuevoUsuario As New Usuario(nombreUsuario, passEncrypt)
        nuevoUsuario.Email = email

        Dim mensaje As String = loginDb.RegisterUser(nuevoUsuario)

        If mensaje.Contains("Error") Then
            ShowSwalError(Me, mensaje)
            Return
        End If

        ShowSwal(Me, "Usuario registrado") ' 
        ' Limpiar campos
        txtUsuario.Text = ""
        txtEmail.Text = ""
        txtPassword.Text = ""
        txtConfirmarPassword.Text = ""
    End Sub
End Class
