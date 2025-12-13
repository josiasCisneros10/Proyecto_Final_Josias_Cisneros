Imports Proyecto_Final_Josias_Cisneros.Utils
Imports System.Data

Public Class Catalogo
    Inherits System.Web.UI.Page
    Private dbProducto As New dbProducto()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim usuario = TryCast(Session("Usuario"), Usuario)
        If usuario Is Nothing OrElse usuario.Rol <> 1 Then
            Response.Redirect("~/Login.aspx")
            Return
        End If
        If Not IsPostBack Then
            CargarCatalogo()
        End If
    End Sub

    Private Sub CargarCatalogo()
        gvCatalogo.DataSource = dbProducto.GetAll()
        gvCatalogo.DataBind()
    End Sub

    Protected Sub gvCatalogo_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Agregar" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvCatalogo.Rows(index)

            ' DataKeys: IdProducto, TipoProducto, Marca, Modelo, Precio
            Dim keys = gvCatalogo.DataKeys(index)
            Dim idProducto As Integer = CInt(keys("IdProducto"))
            Dim tipo As String = keys("TipoProducto").ToString()
            Dim marca As String = keys("Marca").ToString()
            Dim modelo As String = keys("Modelo").ToString()
            Dim precio As Decimal = CDec(keys("Precio"))

            ' Cantidad desde el TextBox
            Dim txtCantidad As TextBox = TryCast(row.FindControl("txtCantidad"), TextBox)
            Dim cantidad As Integer

            If txtCantidad Is Nothing OrElse
               Not Integer.TryParse(txtCantidad.Text.Trim(), cantidad) OrElse cantidad <= 0 Then

                ShowSwalError(Me, "La cantidad debe ser un número mayor a cero.")
                Return
            End If

            ' Obtener producto para validar stock
            Dim prod = dbProducto.GetById(idProducto)
            If prod Is Nothing Then
                ShowSwalError(Me, "No se pudo obtener la información del producto.")
                Return
            End If

            ' Cargar carrito de sesión
            Dim carrito As List(Of Carrito) = TryCast(Session("Carrito"), List(Of Carrito))
            If carrito Is Nothing Then
                carrito = New List(Of Carrito)()
            End If

            ' Ver si ya existe en el carrito
            Dim existente As Carrito = Nothing
            For Each item In carrito
                If item.IdProducto = idProducto Then
                    existente = item
                    Exit For
                End If
            Next

            ' Validar cantidad total (existente + nuevo)
            Dim cantidadTotal As Integer = cantidad
            If existente IsNot Nothing Then
                cantidadTotal += existente.Cantidad
            End If

            If cantidadTotal > prod.Cantidad Then
                ShowSwalError(Me, $"Solo hay {prod.Cantidad} unidades disponibles de este producto.")
                Return
            End If

            ' Agregar o actualizar en el carrito
            If existente IsNot Nothing Then
                existente.Cantidad += cantidad
            Else
                Dim nuevo As New Carrito With {
                    .IdProducto = idProducto,
                    .TipoProducto = tipo,
                    .Marca = marca,
                    .Modelo = modelo,
                    .Precio = precio,
                    .Cantidad = cantidad
                }
                carrito.Add(nuevo)
            End If

            Session("Carrito") = carrito
            ShowSwal(Me, "Producto agregado al carrito")
        End If
    End Sub

End Class
