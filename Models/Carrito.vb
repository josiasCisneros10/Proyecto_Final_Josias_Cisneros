Public Class Carrito
    Public Property IdProducto As Integer
    Public Property TipoProducto As String
    Public Property Marca As String
    Public Property Modelo As String
    Public Property Precio As Decimal
    Public Property Cantidad As Integer

    Public ReadOnly Property Subtotal As Decimal
        Get
            Return Precio * Cantidad
        End Get
    End Property
End Class
