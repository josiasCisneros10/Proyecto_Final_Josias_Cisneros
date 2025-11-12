Imports Proyecto_Final_Josias_Cisneros.Utils

Public Class FormProducto
    Inherits System.Web.UI.Page
    Public producto As New Producto()
    Protected dbProducto As New dbProducto

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_guardar(sender As Object, e As EventArgs)

        If txtTipoProducto.Text = "" Or txtMarca.Text = "" Or TxtModelo.Text = "" Or txtPrecio.Text = "" Or txtCantidad.Text = "" Then
            ShowSwalError(Me, "Debe completar todos los campos")
            Return
        End If

        Dim producto As New Producto()
        producto.TipoProducto = txtTipoProducto.Text
        producto.Marca = txtMarca.Text
        producto.Modelo = TxtModelo.Text

        Try
            producto.Precio = Convert.ToDecimal(txtPrecio.Text)
            producto.Cantidad = Convert.ToInt32(txtCantidad.Text)

            If producto.Precio < 0 Or producto.Cantidad < 0 Then
                lblMensaje.Text = "Precio y cantidad no pueden ser negativos"
                lblMensaje.CssClass = "alert alert-danger"
                Return
            End If

        Catch ex As Exception
            ShowSwalError(Me, "Error en los valores numéricos: " & ex.Message)
            lblMensaje.Text = "Error en los valores numéricos: " & ex.Message
            lblMensaje.CssClass = "alert alert-danger"
            Return
        End Try

        If dbProducto.create(producto) Then
            ShowSwal(Me, "Producto guardado correctamente")
            lblMensaje.Text = "Producto guardado correctamente"
            lblMensaje.CssClass = "alert alert-success"


            txtTipoProducto.Text = ""
            txtMarca.Text = ""
            TxtModelo.Text = ""
            txtPrecio.Text = ""
            txtCantidad.Text = ""
        Else
            ShowSwalError(Me, "Error al guardar producto")
            lblMensaje.Text = "Error al guardar producto"
            lblMensaje.CssClass = "alert alert-danger"
        End If

        gvProducto.DataBind()
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)

        If txtTipoProducto.Text = "" Or txtMarca.Text = "" Or TxtModelo.Text = "" Or txtPrecio.Text = "" Or txtCantidad.Text = "" Then
            ShowSwalError(Me, "Debe completar todos los campos")
            Return
        End If

        Dim producto As Producto = New Producto With {
        .TipoProducto = txtTipoProducto.Text,
        .Marca = txtMarca.Text,
        .Modelo = TxtModelo.Text,
        .IdProducto = Convert.ToInt32(editando.Value)
    }

        Try
            producto.Precio = Convert.ToDecimal(txtPrecio.Text)
            producto.Cantidad = Convert.ToInt32(txtCantidad.Text)

            If producto.Precio < 0 Or producto.Cantidad < 0 Then
                lblMensaje.Text = "Precio y cantidad no pueden ser negativos"
                lblMensaje.CssClass = "alert alert-danger"
                Return
            End If

        Catch ex As Exception
            ShowSwalError(Me, "Error en los valores numéricos: " & ex.Message)
            lblMensaje.Text = "Error en los valores numéricos: " & ex.Message
            lblMensaje.CssClass = "alert alert-danger"
            Return
        End Try

        Dim resultado As String = dbProducto.update(producto)

        If resultado.Contains("Error") Then
            ShowSwalError(Me, resultado)
            lblMensaje.Text = resultado
            lblMensaje.CssClass = "alert alert-danger"
            Return
        Else
            ShowSwal(Me, resultado)
            lblMensaje.Text = resultado
            lblMensaje.CssClass = "alert alert-success"
        End If

        gvProducto.DataBind()
        gvProducto.EditIndex = -1
        LimpiarCampos()
    End Sub

    Protected Sub LimpiarCampos()
        btnActualizar.Visible = False
        btnGuardar.Visible = True
        btnCancelar.Visible = False

        txtTipoProducto.Text = ""
        txtMarca.Text = ""
        TxtModelo.Text = ""
        txtPrecio.Text = ""
        txtCantidad.Text = ""
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

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        LimpiarCampos()
    End Sub
End Class

