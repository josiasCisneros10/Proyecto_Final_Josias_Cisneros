
Public Class Pedido
    Public Property IdPedido As Integer
    Public Property IdCliente As Integer
    Public Property FechaPedido As DateTime
    Public Property Total As Decimal
    Public Property Estado As String

    Public Sub New()
    End Sub

    Public Sub New(idPedido As Integer, idCliente As Integer, fecha As DateTime, total As Decimal, estado As String)
        Me.IdPedido = idPedido
        Me.IdCliente = idCliente
        Me.FechaPedido = fecha
        Me.Total = total
        Me.Estado = estado
    End Sub
End Class