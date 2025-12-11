Imports Proyecto_Final_Josias_Cisneros.Utils

Public Class FormCliente
    Inherits System.Web.UI.Page

    Private dbCliente As New dbCliente()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim usuario = TryCast(Session("Usuario"), Usuario)
        If usuario Is Nothing OrElse usuario.Rol <> 2 Then
            Response.Redirect("~/Login.aspx")
            Return
        End If
        If Not IsPostBack Then
            CargarClientes()
        End If
    End Sub

    Private Sub CargarClientes()
        gvClientes.DataSource = dbCliente.GetAll()
        gvClientes.DataBind()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then Return
        Dim cliente As New Cliente With {
            .Nombre = txtNombre.Text.Trim(),
            .Apellido = txtApellido.Text.Trim(),
            .Email = txtEmail.Text.Trim(),
            .Direccion = txtDireccion.Text.Trim()
        }
        If dbCliente.Create(cliente) Then
            ShowSwal(Me, "Cliente guardado")
            Limpiar()
            CargarClientes()
        Else
            ShowSwalError(Me, "Error al guardar el cliente")
        End If
    End Sub

    Protected Sub gvClientes_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim id As Integer = Convert.ToInt32(gvClientes.DataKeys(e.RowIndex).Value)
        If dbCliente.Delete(id) Then
            ShowSwal(Me, "Cliente eliminado")
        Else
            ShowSwalError(Me, "Error al eliminar el cliente")
        End If
        e.Cancel = True
        CargarClientes()
    End Sub

    Protected Sub gvClientes_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gvClientes.SelectedRow
        editando.Value = row.Cells(1).Text
        txtNombre.Text = row.Cells(2).Text
        txtApellido.Text = row.Cells(3).Text
        txtEmail.Text = row.Cells(4).Text
        txtDireccion.Text = row.Cells(5).Text

        btnGuardar.Visible = False
        btnActualizar.Visible = True
        btnCancelar.Visible = True
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then Return
        Dim cliente As New Cliente With {
            .IdCliente = Convert.ToInt32(editando.Value),
            .Nombre = txtNombre.Text.Trim(),
            .Apellido = txtApellido.Text.Trim(),
            .Email = txtEmail.Text.Trim(),
            .Direccion = txtDireccion.Text.Trim()
        }
        If dbCliente.Update(cliente) Then
            ShowSwal(Me, "Cliente actualizado")
            Limpiar()
            CargarClientes()
        Else
            ShowSwalError(Me, "Error al actualizar el cliente")
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Limpiar()
    End Sub

    Private Sub Limpiar()
        editando.Value = ""
        txtNombre.Text = ""
        txtApellido.Text = ""
        txtEmail.Text = ""
        txtDireccion.Text = ""

        btnGuardar.Visible = True
        btnActualizar.Visible = False
        btnCancelar.Visible = False
    End Sub
End Class
