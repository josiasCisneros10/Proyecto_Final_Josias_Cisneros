Imports System.Data.SqlClient
Imports System.Data

Public Class dbProducto

    Private ReadOnly dbHelper As New DbHelper()

    Public Function GetAll() As DataTable
        Dim sql As String = "SELECT * FROM Producto"
        Return dbHelper.ExecuteQuery(sql)
    End Function

    Public Function create(p As Producto) As Boolean
        Try
            Dim sql As String = "INSERT INTO Producto (TipoProducto, Marca, Modelo, Precio, Cantidad) 
                                 VALUES (@TipoProducto, @Marca, @Modelo, @Precio, @Cantidad)"

            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@TipoProducto", p.TipoProducto),
                dbHelper.CreateParameter("@Marca", p.Marca),
                dbHelper.CreateParameter("@Modelo", p.Modelo),
                dbHelper.CreateParameter("@Precio", p.Precio),
                dbHelper.CreateParameter("@Cantidad", p.Cantidad)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function delete(id As Integer) As Boolean
        Try
            Dim sql As String = "DELETE FROM Producto WHERE IdProducto = @IdProducto"
            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@IdProducto", id)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function update(p As Producto) As Boolean
        Try
            Dim sql As String = "UPDATE Producto SET TipoProducto = @TipoProducto,
                                 Marca = @Marca, Modelo = @Modelo,
                                 Precio = @Precio, Cantidad = @Cantidad
                                 WHERE IdProducto = @IdProducto"
            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@IdProducto", p.IdProducto),
                dbHelper.CreateParameter("@TipoProducto", p.TipoProducto),
                dbHelper.CreateParameter("@Marca", p.Marca),
                dbHelper.CreateParameter("@Modelo", p.Modelo),
                dbHelper.CreateParameter("@Precio", p.Precio),
                dbHelper.CreateParameter("@Cantidad", p.Cantidad)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class