
Public Class DetallePedido
    Public Property IdDetalle As Integer
    Public Property IdPedido As Integer
    Public Property IdProducto As Integer
    Public Property Cantidad As Integer
    Public Property PrecioUnitario As Decimal

    Public Sub New()
        End Sub

    Public Sub New(idDetalle As Integer, idPedido As Integer, idProducto As Integer,
                   cantidad As Integer, precioUnitario As Decimal)
        Me.IdDetalle = idDetalle
        Me.IdPedido = idPedido
        Me.IdProducto = idProducto
        Me.Cantidad = cantidad
        Me.PrecioUnitario = precioUnitario
    End Sub
End Class
