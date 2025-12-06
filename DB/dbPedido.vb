Imports System.Data
Imports System.Data.SqlClient
Public Class dbPedido
    Private ReadOnly dbHelper As New DbHelper()
    Public Function GetAll() As DataTable
        Dim sql As String =
            "SELECT p.IdPedido,
                    p.IdCliente,
                    c.Nombre + ' ' + c.Apellido AS NombreCliente,
                    p.FechaPedido,
                    p.Total,
                    p.Estado
             FROM Pedidos p
             INNER JOIN Clientes c ON p.IdCliente = c.IdCliente"

        Return dbHelper.ExecuteQuery(sql)
    End Function

    Public Function Create(p As Pedido) As Integer
        Try
            Dim sql As String =
                "INSERT INTO Pedidos (IdCliente, FechaPedido, Total, Estado)
                 OUTPUT INSERTED.IdPedido
                 VALUES (@IdCliente, @FechaPedido, @Total, @Estado)"
            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@IdCliente", p.IdCliente),
                dbHelper.CreateParameter("@FechaPedido", p.FechaPedido),
                dbHelper.CreateParameter("@Total", p.Total),
                dbHelper.CreateParameter("@Estado", p.Estado)
            }
            Dim dt = dbHelper.ExecuteQuery(sql, parametros)
            If dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0)("IdPedido"))
            End If
        Catch ex As Exception
        End Try
        Return 0
    End Function

    Public Function Update(p As Pedido) As Boolean
        Try
            Dim sql As String =
                "UPDATE Pedidos
                 SET IdCliente = @IdCliente,
                     Total = @Total,
                     Estado = @Estado
                 WHERE IdPedido = @IdPedido"
            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@IdPedido", p.IdPedido),
                dbHelper.CreateParameter("@IdCliente", p.IdCliente),
                dbHelper.CreateParameter("@Total", p.Total),
                dbHelper.CreateParameter("@Estado", p.Estado)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Delete(idPedido As Integer) As Boolean
        Try
            Dim sql As String = "DELETE FROM Pedidos WHERE IdPedido = @IdPedido"
            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@IdPedido", idPedido)
            }

            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
