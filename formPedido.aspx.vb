Imports Proyecto_Final_Josias_Cisneros.Utils
Imports System.Web.UI.WebControls
Imports System.Data

Public Class FormPedido
    Inherits System.Web.UI.Page

    Private dbPedido As New dbPedido()
    Private dbCliente As New dbCliente()
    Private dbProducto As New dbProducto()
    Private dbDetalle As New dbDetallePedido()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim usuario = TryCast(Session("Usuario"), Usuario)
        If usuario Is Nothing OrElse usuario.Rol <> 2 Then
            Response.Redirect("~/Login.aspx")
            Return
        End If

        If Not IsPostBack Then
            CargarClientes()
            CargarProductos()
            CargarPedidos()
        End If
    End Sub

    Private Sub CargarClientes()
        Dim dt = dbCliente.GetAll()
        ddlCliente.Items.Clear()
        ddlCliente.Items.Add(New ListItem("Seleccione un cliente", ""))

        For Each row As DataRow In dt.Rows
            ddlCliente.Items.Add(New ListItem(
                row("Nombre") & " " & row("Apellido"),
                row("IdCliente").ToString()))
        Next
    End Sub

    Private Sub CargarProductos()
        Dim dt = dbProducto.GetAll()
        ddlProducto.Items.Clear()
        ddlProducto.Items.Add(New ListItem("Seleccione un producto", ""))

        For Each row As DataRow In dt.Rows
            Dim texto As String = $"{row("TipoProducto")} {row("Marca")} {row("Modelo")} - ${CDec(row("Precio")).ToString("N2")}"
            Dim valor As String = row("IdProducto").ToString()
            ddlProducto.Items.Add(New ListItem(texto, valor))
        Next
    End Sub

    Private Sub CargarPedidos()
        gvPedidos.DataSource = dbPedido.GetAll()
        gvPedidos.DataBind()
    End Sub

    Private Sub CargarDetalle(idPedido As Integer)
        gvDetalle.DataSource = dbDetalle.GetByPedido(idPedido)
        gvDetalle.DataBind()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then Return

        If String.IsNullOrEmpty(ddlCliente.SelectedValue) Then
            ShowSwalError(Me, "Debe seleccionar un cliente.")
            Return
        End If

        If String.IsNullOrEmpty(ddlProducto.SelectedValue) Then
            ShowSwalError(Me, "Debe seleccionar un producto.")
            Return
        End If

        If String.IsNullOrEmpty(ddlEstado.SelectedValue) Then
            ShowSwalError(Me, "Debe seleccionar un estado.")
            Return
        End If

        Dim cantidad As Integer
        If Not Integer.TryParse(txtCantidad.Text.Trim(), cantidad) OrElse cantidad <= 0 Then
            ShowSwalError(Me, "La cantidad debe ser un número mayor a cero.")
            Return
        End If

        Dim prod = dbProducto.GetById(CInt(ddlProducto.SelectedValue))
        If prod Is Nothing Then
            ShowSwalError(Me, "No se pudo obtener el producto seleccionado.")
            Return
        End If

        Dim precioUnitario As Decimal = prod.Precio
        Dim totalDecimal As Decimal = precioUnitario * cantidad

        txtTotal.Value = totalDecimal.ToString("N2")

        Dim p As New Pedido With {
            .IdCliente = CInt(ddlCliente.SelectedValue),
            .FechaPedido = DateTime.Now,
            .Total = totalDecimal,
            .Estado = ddlEstado.SelectedValue
        }

        Dim idPedido As Integer = dbPedido.Create(p)
        If idPedido <= 0 Then
            ShowSwalError(Me, "Error al guardar el pedido.")
            Return
        End If

        Dim detallepedido As New DetallePedido With {
            .IdPedido = idPedido,
            .IdProducto = CInt(ddlProducto.SelectedValue),
            .Cantidad = cantidad,
            .PrecioUnitario = precioUnitario
        }

        If Not dbDetalle.Create(detallepedido) Then
            ShowSwalError(Me, "El pedido se creó, pero ocurrió un error al guardar el detalle.")
            Return
        End If

        ShowSwal(Me, "Pedido guardado")

        CargarPedidos()
        Limpiar()
    End Sub

    Protected Sub gvPedidos_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim id As Integer = Convert.ToInt32(gvPedidos.DataKeys(e.RowIndex).Values("IdPedido"))
        If dbPedido.Delete(id) Then
            ShowSwal(Me, "Pedido eliminado correctamente")
        Else
            ShowSwalError(Me, "Error al eliminar el pedido")
        End If

        e.Cancel = True
        CargarPedidos()
        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()
    End Sub

    Protected Sub gvPedidos_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim idx As Integer = gvPedidos.SelectedRow.RowIndex

        Dim idPedido As Integer = Convert.ToInt32(gvPedidos.DataKeys(idx).Values("IdPedido"))
        Dim idCliente As Integer = Convert.ToInt32(gvPedidos.DataKeys(idx).Values("IdCliente"))

        editando.Value = idPedido.ToString()
        ddlCliente.SelectedValue = idCliente.ToString()

        txtTotal.Value = gvPedidos.SelectedRow.Cells(4).Text
        ddlEstado.SelectedValue = gvPedidos.SelectedRow.Cells(5).Text

        btnGuardar.Visible = False
        btnActualizar.Visible = True
        btnCancelar.Visible = True

        CargarDetalle(idPedido)

        Dim dtDet As DataTable = dbDetalle.GetByPedido(idPedido)
        If dtDet IsNot Nothing AndAlso dtDet.Rows.Count > 0 Then
            Dim fila As DataRow = dtDet.Rows(0)

            Dim idProd As String = fila("IdProducto").ToString()
            Dim cant As String = fila("Cantidad").ToString()

            Dim item = ddlProducto.Items.FindByValue(idProd)
            If item IsNot Nothing Then
                ddlProducto.SelectedValue = idProd
            Else
                ddlProducto.SelectedIndex = 0
            End If

            txtCantidad.Text = cant
        Else
            ddlProducto.SelectedIndex = 0
            txtCantidad.Text = ""
        End If
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then Return
        If String.IsNullOrEmpty(editando.Value) Then
            ShowSwalError(Me, "No hay un pedido seleccionado para actualizar.")
            Return
        End If
        If String.IsNullOrEmpty(ddlCliente.SelectedValue) Then
            ShowSwalError(Me, "Debe seleccionar un cliente.")
            Return
        End If
        If String.IsNullOrEmpty(ddlProducto.SelectedValue) Then
            ShowSwalError(Me, "Debe seleccionar un producto.")
            Return
        End If
        If String.IsNullOrEmpty(ddlEstado.SelectedValue) Then
            ShowSwalError(Me, "Debe seleccionar un estado.")
            Return
        End If

        Dim cantidad As Integer
        If Not Integer.TryParse(txtCantidad.Text.Trim(), cantidad) OrElse cantidad <= 0 Then
            ShowSwalError(Me, "La cantidad debe ser un número mayor a cero.")
            Return
        End If

        Dim prod = dbProducto.GetById(CInt(ddlProducto.SelectedValue))
        If prod Is Nothing Then
            ShowSwalError(Me, "No se pudo obtener el producto seleccionado.")
            Return
        End If
        Dim precioUnitario As Decimal = prod.Precio
        Dim totalDecimal As Decimal = precioUnitario * cantidad

        txtTotal.Value = totalDecimal.ToString("N2")

        Dim pedido As New Pedido With {
        .IdPedido = CInt(editando.Value),
        .IdCliente = CInt(ddlCliente.SelectedValue),
        .Total = totalDecimal,
        .Estado = ddlEstado.SelectedValue
    }
        If dbPedido.Update(pedido) Then
            Dim det As New DetallePedido With {
            .IdPedido = CInt(editando.Value),
            .IdProducto = CInt(ddlProducto.SelectedValue),
            .Cantidad = cantidad,
            .PrecioUnitario = precioUnitario
        }
            If Not dbDetalle.Update(det) Then
                ShowSwalError(Me, "El pedido se actualizó, pero ocurrió un error al actualizar el detalle.")
                Return
            End If
            ShowSwal(Me, "Pedido actualizado")
            Limpiar()
            CargarPedidos()
        Else
            ShowSwalError(Me, "Error al actualizar el pedido.")
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Limpiar()
    End Sub

    Private Sub Limpiar()
        editando.Value = ""
        ddlCliente.SelectedIndex = 0
        ddlProducto.SelectedIndex = 0
        txtCantidad.Text = ""
        txtTotal.Value = ""
        ddlEstado.SelectedIndex = 0

        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()

        btnGuardar.Visible = True
        btnActualizar.Visible = False
        btnCancelar.Visible = False
    End Sub
End Class
