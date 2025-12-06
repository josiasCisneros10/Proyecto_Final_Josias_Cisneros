Imports System.Data
Imports System.Data.SqlClient

Public Class dbCliente

    Private ReadOnly dbHelper As New DbHelper()

    Public Function GetAll() As DataTable
        Dim sql As String = "SELECT IdCliente, Nombre, Apellido, Email, Direccion FROM Clientes"
        Return dbHelper.ExecuteQuery(sql)
    End Function

    Public Function Create(c As Cliente) As Boolean
        Try
            Dim sql As String = "INSERT INTO Clientes (Nombre, Apellido, Email, Direccion)
                                 VALUES (@Nombre, @Apellido, @Email, @Direccion)"

            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@Nombre", c.Nombre),
                dbHelper.CreateParameter("@Apellido", c.Apellido),
                dbHelper.CreateParameter("@Email", c.Email),
                dbHelper.CreateParameter("@Direccion", c.Direccion)
            }

            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Update(c As Cliente) As Boolean
        Try
            Dim sql As String = "UPDATE Clientes
                                 SET Nombre = @Nombre,
                                     Apellido = @Apellido,
                                     Email = @Email,
                                     Direccion = @Direccion
                                 WHERE IdCliente = @IdCliente"

            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@IdCliente", c.IdCliente),
                dbHelper.CreateParameter("@Nombre", c.Nombre),
                dbHelper.CreateParameter("@Apellido", c.Apellido),
                dbHelper.CreateParameter("@Email", c.Email),
                dbHelper.CreateParameter("@Direccion", c.Direccion)
            }

            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Delete(idCliente As Integer) As Boolean
        Try
            Dim sql As String = "DELETE FROM Clientes WHERE IdCliente = @IdCliente"
            Dim parametros As New List(Of SqlParameter) From {
                dbHelper.CreateParameter("@IdCliente", idCliente)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
