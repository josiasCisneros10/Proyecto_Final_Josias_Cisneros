Imports System.Data.SqlClient

Public Class dbProducto
    Private ReadOnly connectionString As String = ConfigurationManager.ConnectionStrings("Proyecto_FinalConnectionString").ConnectionString
    Public Function create(producto As Producto) As Boolean
        Try
            Dim sql As String = "INSERT INTO Producto (TipoProducto, Marca, Modelo, Precio, Cantidad) VALUES (@TipoProducto, @Marca, @Modelo, @Precio, @Cantidad)"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@TipoProducto", producto.TipoProducto),
                New SqlParameter("@Marca", producto.Marca),
                New SqlParameter("@Modelo", producto.Modelo),
                New SqlParameter("@Precio", producto.Precio),
                New SqlParameter("@Cantidad", producto.Cantidad)
            }

            Using conexion As New SqlConnection(connectionString)
                Using comando As New SqlCommand(sql, conexion)
                    comando.Parameters.AddRange(parametros.ToArray())
                    conexion.Open()
                    comando.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function delete(ByRef id As Integer) As String
        Try
            Dim sql As String = "DELETE FROM Producto WHERE IdProducto = @IdProducto"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdProducto", id)
            }

            Using conexion As New SqlConnection(connectionString)
                Using comando As New SqlCommand(sql, conexion)
                    comando.Parameters.AddRange(parametros.ToArray())
                    conexion.Open()
                    comando.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return "Error al eliminar: " & ex.Message
        End Try
        Return "Producto eliminado"
    End Function

    Public Function update(producto As Producto) As String
        Try
            Dim sql As String = "UPDATE Producto SET TipoProducto = @TipoProducto, Marca = @Marca, Modelo = @Modelo, Precio = @Precio, Cantidad = @Cantidad WHERE IdProducto = @IdProducto"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdProducto", producto.IdProducto),
                New SqlParameter("@TipoProducto", producto.TipoProducto),
                New SqlParameter("@Marca", producto.Marca),
                New SqlParameter("@Modelo", producto.Modelo),
                New SqlParameter("@Precio", producto.Precio),
                New SqlParameter("@Cantidad", producto.Cantidad)
            }

            Using conexion As New SqlConnection(connectionString)
                Using comando As New SqlCommand(sql, conexion)
                    comando.Parameters.AddRange(parametros.ToArray())
                    conexion.Open()
                    comando.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return "Error al actualizar: " & ex.Message
        End Try
        Return "Producto actualizado"
    End Function
End Class


