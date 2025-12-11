Public Class Carrito
    Public Property IdProducto As Integer
    Public Property Descripcion As String   ' TipoProducto, Marca, Modelo
    Public Property PrecioUnitario As Decimal
    Public Property Cantidad As Integer

    Public ReadOnly Property Subtotal As Decimal
        Get
            Return PrecioUnitario * Cantidad
        End Get
    End Property
End Class
