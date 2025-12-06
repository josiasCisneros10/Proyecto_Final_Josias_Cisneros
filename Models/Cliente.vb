Public Class Cliente
    Public Property IdCliente As Integer
    Public Property Nombre As String
    Public Property Apellido As String
    Public Property Email As String
    Public Property Direccion As String

    Public Sub New()
    End Sub

    Public Sub New(idCliente As Integer, nombre As String, apellido As String, email As String, direccion As String)
        Me.IdCliente = idCliente
        Me.Nombre = nombre
        Me.Apellido = apellido
        Me.Email = email
        Me.Direccion = direccion
    End Sub
End Class

