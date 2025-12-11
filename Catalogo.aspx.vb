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
            CargarProductos()
        End If
    End Sub

    Private Sub CargarProductos()
        gvCatalogo.DataSource = dbProducto.GetAll()
        gvCatalogo.DataBind()
    End Sub

    Private Function ObtenerCarrito() As List(Of Carrito)
        Dim carrito As List(Of Carrito) =
            TryCast(Session("Carrito"), List(Of Carrito))

        If carrito Is Nothing Then
            carrito = New List(Of Carrito)()
            Session("Carrito") = carrito
        End If
        Return carrito
    End Function

    Private Sub AgregarAlCarrito(idProducto As Integer,
                                 descripcion As String,
                                 precioUnitario As Decimal,
                                 cantidad As Integer)

        Dim carrito As List(Of Carrito) = ObtenerCarrito()

        ' Buscar si ya existe ese producto en el carrito
        Dim existente As Carrito = Nothing
        For Each item As Carrito In carrito
            If item.IdProducto = idProducto Then
                existente = item
                Exit For
            End If
        Next

        If existente Is Nothing Then
            Dim nuevo As New Carrito()
            nuevo.IdProducto = idProducto
            nuevo.Descripcion = descripcion
            nuevo.PrecioUnitario = precioUnitario
            nuevo.Cantidad = cantidad
            carrito.Add(nuevo)
        Else
            existente.Cantidad += cantidad
        End If
        ' Guardar de nuevo en sesión
        Session("Carrito") = carrito
    End Sub

    Protected Sub gvCatalogo_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Agregar" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvCatalogo.Rows(index)

            ' Obtener cantidad
            Dim txtCantidad As TextBox = CType(row.FindControl("txtCantidad"), TextBox)
            Dim cantidad As Integer

            If txtCantidad Is Nothing OrElse
               Not Integer.TryParse(txtCantidad.Text.Trim(), cantidad) OrElse
               cantidad <= 0 Then
                ShowSwalError(Me, "La cantidad debe ser un número mayor a cero.")
                Return
            End If

            ' Obtener datos desde DataKeys
            Dim idProducto As Integer = CInt(gvCatalogo.DataKeys(index).Values("IdProducto"))
            Dim tipo As String = gvCatalogo.DataKeys(index).Values("TipoProducto").ToString()
            Dim marca As String = gvCatalogo.DataKeys(index).Values("Marca").ToString()
            Dim modelo As String = gvCatalogo.DataKeys(index).Values("Modelo").ToString()
            Dim precioUnitario As Decimal = CDec(gvCatalogo.DataKeys(index).Values("Precio"))

            Dim descripcion As String = tipo & " " & marca & " " & modelo

            ' Agregar al carrito
            AgregarAlCarrito(idProducto, descripcion, precioUnitario, cantidad)
            ShowSwal(Me, "Producto agregado al carrito")
        End If
    End Sub
End Class
