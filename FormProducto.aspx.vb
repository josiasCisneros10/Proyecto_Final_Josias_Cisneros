Imports Proyecto_Final_Josias_Cisneros.Utils

Public Class FormProducto
    Inherits System.Web.UI.Page
    Private dbProducto As New dbProducto()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim usuario = TryCast(Session("Usuario"), Usuario)
        If usuario Is Nothing OrElse usuario.Rol <> 2 Then
            Response.Redirect("~/Login.aspx")
            Return
        End If

        If Not IsPostBack Then
            CargarProductos()
        End If
    End Sub

    Private Sub CargarProductos()
        gvProducto.DataSource = dbProducto.GetAll()
        gvProducto.DataBind()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim producto As New Producto With {
            .TipoProducto = txtTipoProducto.Text.Trim(),
            .Marca = txtMarca.Text.Trim(),
            .Modelo = TxtModelo.Text.Trim(),
            .Precio = Convert.ToDecimal(txtPrecio.Text),
            .Cantidad = Convert.ToInt32(txtCantidad.Text)
        }
        If dbProducto.create(producto) Then
            ShowSwal(Me, "Producto guardado")
        Else
            ShowSwalError(Me, "Ocurrió un error al guardar el producto")
        End If
        Limpiar()
        CargarProductos()
    End Sub

    Protected Sub gvProducto_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim id As Integer = Convert.ToInt32(gvProducto.DataKeys(e.RowIndex).Value)

        If dbProducto.delete(id) Then
            ShowSwal(Me, "Producto eliminado")
        Else
            ShowSwalError(Me, "Error al eliminar el producto")
        End If
        CargarProductos()
        e.Cancel = True
    End Sub

    Protected Sub gvProducto_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gvProducto.SelectedRow
        editando.Value = row.Cells(1).Text
        txtTipoProducto.Text = row.Cells(2).Text
        txtMarca.Text = row.Cells(3).Text
        TxtModelo.Text = row.Cells(4).Text
        txtPrecio.Text = row.Cells(5).Text
        txtCantidad.Text = row.Cells(6).Text

        btnGuardar.Visible = False
        btnActualizar.Visible = True
        btnCancelar.Visible = True
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        Dim producto As New Producto With {
            .IdProducto = Convert.ToInt32(editando.Value),
            .TipoProducto = txtTipoProducto.Text,
            .Marca = txtMarca.Text,
            .Modelo = TxtModelo.Text,
            .Precio = Convert.ToDecimal(txtPrecio.Text),
            .Cantidad = Convert.ToInt32(txtCantidad.Text)
        }
        If dbProducto.update(producto) Then
            ShowSwal(Me, "Producto actualizado")
        Else
            ShowSwalError(Me, "Error al actualizar el producto")
        End If

        Limpiar()
        CargarProductos()
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Limpiar()
    End Sub

    Private Sub Limpiar()
        txtTipoProducto.Text = ""
        txtMarca.Text = ""
        TxtModelo.Text = ""
        txtPrecio.Text = ""
        txtCantidad.Text = ""

        btnGuardar.Visible = True
        btnActualizar.Visible = False
        btnCancelar.Visible = False
    End Sub
End Class

