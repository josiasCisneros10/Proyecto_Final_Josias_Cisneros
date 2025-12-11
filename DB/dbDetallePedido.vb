Imports System.Data
Imports System.Data.SqlClient

Public Class dbDetallePedido

    Private ReadOnly dbHelper As New DbHelper()

    Public Function GetByPedido(idPedido As Integer) As DataTable
        Dim sql As String =
            "SELECT d.IdDetalle,
                    d.IdProducto,
                    p.TipoProducto,
                    p.Marca,
                    p.Modelo,
                    d.Cantidad,
                    d.PrecioUnitario,
                    d.Subtotal
             FROM DetallePedido d
             INNER JOIN Producto p ON d.IdProducto = p.IdProducto
             WHERE d.IdPedido = @IdPedido"
        Dim parametros As New List(Of SqlParameter) From {
            dbHelper.CreateParameter("@IdPedido", idPedido)
        }
        Return dbHelper.ExecuteQuery(sql, parametros)
    End Function
    Public Function Create(dp As DetallePedido) As Boolean
        Try
            Dim sql As String =
                "INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, PrecioUnitario)
                 VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario)"
            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@IdPedido", dp.IdPedido),
                dbHelper.CreateParameter("@IdProducto", dp.IdProducto),
                dbHelper.CreateParameter("@Cantidad", dp.Cantidad),
                dbHelper.CreateParameter("@PrecioUnitario", dp.PrecioUnitario)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Update(det As DetallePedido) As Boolean
        Try
            Dim sql As String = "UPDATE DetallePedido 
                                 SET IdProducto = @IdProducto,
                                     Cantidad = @Cantidad,
                                     PrecioUnitario = @PrecioUnitario
                                 WHERE IdPedido = @IdPedido"

            Dim parametros As New List(Of SqlClient.SqlParameter) From {
                dbHelper.CreateParameter("@IdPedido", det.IdPedido),
                dbHelper.CreateParameter("@IdProducto", det.IdProducto),
                dbHelper.CreateParameter("@Cantidad", det.Cantidad),
                dbHelper.CreateParameter("@PrecioUnitario", det.PrecioUnitario)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
