Public Class SiteMaster
    Inherits MasterPage

    Protected autenticado As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim usuario As Usuario = TryCast(Session("Usuario"), Usuario)

        autenticado = (usuario IsNot Nothing)

        Dim esAdmin As Boolean = False
        If usuario IsNot Nothing AndAlso usuario.Rol = "2" Then
            esAdmin = True
        End If

        If autenticado Then

            liAdmin.Visible = esAdmin
            liProductos.Visible = esAdmin
            liClientes.Visible = esAdmin
            liPedidos.Visible = esAdmin

            liCatalogo.Visible = Not esAdmin
            liCarrito.Visible = Not esAdmin

            btnLogOut.Visible = True
        Else

            liAdmin.Visible = False
            liProductos.Visible = False
            liClientes.Visible = False
            liPedidos.Visible = False
            liCatalogo.Visible = False
            liCarrito.Visible = False
            btnLogOut.Visible = False
        End If
    End Sub
    Protected Sub btnLogOut_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("~/Login.aspx")
    End Sub
End Class
