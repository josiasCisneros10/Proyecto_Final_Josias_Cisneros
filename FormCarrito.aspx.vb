Imports Proyecto_Final_Josias_Cisneros.Utils

Public Class FormCarrito
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim usuario = TryCast(Session("Usuario"), Usuario)
        If usuario Is Nothing OrElse usuario.Rol <> 1 Then
            Response.Redirect("~/Login.aspx")
            Return
        End If

        If Not IsPostBack Then
            CargarCarrito()
        End If
    End Sub

    Private Sub CargarCarrito()
        Dim carrito As List(Of Carrito) = TryCast(Session("Carrito"), List(Of Carrito))

        If carrito Is Nothing OrElse carrito.Count = 0 Then
            gvCarrito.DataSource = Nothing
            gvCarrito.DataBind()
            lblTotal.Text = "No hay productos en el carrito."
            Return
        End If

        gvCarrito.DataSource = carrito
        gvCarrito.DataBind()

        Dim total As Decimal = 0D
        For Each item In carrito
            total += item.Subtotal
        Next

        lblTotal.Text = "Total: $" & total.ToString("N2")
    End Sub

    Protected Sub gvCarrito_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim carrito As List(Of Carrito) = TryCast(Session("Carrito"), List(Of Carrito))
        If carrito Is Nothing Then
            e.Cancel = True
            Return
        End If

        Dim index As Integer = e.RowIndex
        If index >= 0 AndAlso index < carrito.Count Then
            carrito.RemoveAt(index)
        End If

        Session("Carrito") = carrito

        e.Cancel = True
        CargarCarrito()
        ShowSwal(Me, "Producto eliminado del carrito")
    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs)
        Dim carrito As List(Of Carrito) = TryCast(Session("Carrito"), List(Of Carrito))

        If carrito Is Nothing OrElse carrito.Count = 0 Then
            ShowSwalError(Me, "No hay productos en el carrito.")
            Return
        End If

        Session("Carrito") = Nothing

        CargarCarrito()
        ShowSwal(Me, "Compra confirmada", "Gracias por su compra.")
    End Sub

End Class
