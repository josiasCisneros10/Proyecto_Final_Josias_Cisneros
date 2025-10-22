Public Class FormProducto
    Inherits System.Web.UI.Page
    Public producto As New Producto()
    Protected dbProducto As New dbProducto

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_guardar(sender As Object, e As EventArgs)
        Dim producto As New Producto()
        producto.TipoProducto = txtTipoProducto.Text
        producto.Marca = txtMarca.Text
        producto.Modelo = TxtModelo.Text

        Try
            producto.Precio = Convert.ToDecimal(txtPrecio.Text)
            producto.Cantidad = Convert.ToInt32(txtCantidad.Text)
        Catch ex As Exception
            lblMensaje.Text = "Error en los valores numéricos: " & ex.Message
            Exit Sub
        End Try

        If dbProducto.create(producto) Then
            lblMensaje.Text = "Producto guardado correctamente."
            gvProducto.DataBind()
        Else
            lblMensaje.Text = "Error al guardar producto."
        End If
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        Dim producto As New Producto()

        producto.TipoProducto = txtTipoProducto.Text
        producto.Marca = txtMarca.Text
        producto.Modelo = TxtModelo.Text

        Try
            producto.Precio = Convert.ToDecimal(txtPrecio.Text)
            producto.Cantidad = Convert.ToInt32(txtCantidad.Text)
        Catch ex As Exception
            lblMensaje.Text = "Error en los valores numéricos: " & ex.Message
            Exit Sub
        End Try

        producto.IdProducto = Convert.ToInt32(editando.Value)

        Dim resultado As String = dbProducto.update(producto)
        lblMensaje.Text = resultado
        gvProducto.DataBind()
        gvProducto.EditIndex = -1
    End Sub


    Protected Sub gvProducto_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            Dim id As Integer = Convert.ToInt32(gvProducto.DataKeys(e.RowIndex).Value)
            dbProducto.delete(id)
            e.Cancel = True
            gvProducto.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al eliminar el producto: " & ex.Message
        End Try
    End Sub

    Protected Sub gvProducto_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvProducto.EditIndex = -1
        gvProducto.DataBind()
    End Sub

    Protected Sub gvProducto_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(gvProducto.DataKeys(e.RowIndex).Value)
        Dim producto As New Producto()

        Try
            producto.TipoProducto = e.NewValues("TipoProducto")
            producto.Marca = e.NewValues("Marca")
            producto.Modelo = e.NewValues("Modelo")
            producto.Precio = Convert.ToDecimal(e.NewValues("Precio"))
            producto.Cantidad = Convert.ToInt32(e.NewValues("Cantidad"))
            producto.IdProducto = id
        Catch ex As Exception
            lblMensaje.Text = "Error al actualizar: " & ex.Message
            e.Cancel = True
            gvProducto.EditIndex = -1
            Return
        End Try
        Dim resultado As String = dbProducto.update(producto)
        lblMensaje.Text = resultado
        gvProducto.DataBind()
        e.Cancel = True
        gvProducto.EditIndex = -1
    End Sub

    Protected Sub gvProducto_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gvProducto.SelectedRow()
        Dim id As Integer = Convert.ToInt32(row.Cells(2).Text)
        Dim producto As New Producto()

        txtTipoProducto.Text = row.Cells(3).Text
        txtMarca.Text = row.Cells(4).Text
        TxtModelo.Text = row.Cells(5).Text

        txtPrecio.Text = Decimal.Parse(row.Cells(6).Text).ToString("F2")
        txtCantidad.Text = Integer.Parse(row.Cells(7).Text).ToString()

        editando.Value = id
    End Sub
End Class

