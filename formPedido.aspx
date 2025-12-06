<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormPedido.aspx.vb" Inherits="Proyecto_Final_Josias_Cisneros.FormPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- ID del pedido en edición -->
    <asp:HiddenField ID="editando" runat="server" />

    <!-- Total calculado internamente -->
    <asp:HiddenField ID="txtTotal" runat="server" />

    <div class="container d-flex flex-column mb-3 gap-2">

        <!-- Cliente -->
        <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvCliente" runat="server"
            ControlToValidate="ddlCliente"
            InitialValue=""
            ErrorMessage="Debe seleccionar un cliente"
            CssClass="alert alert-warning"
            Display="Dynamic"
            ValidationGroup="vgPedido" />

        <!-- Producto -->
        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvProducto" runat="server"
            ControlToValidate="ddlProducto"
            InitialValue=""
            ErrorMessage="Debe seleccionar un producto"
            CssClass="alert alert-warning"
            Display="Dynamic"
            ValidationGroup="vgPedido" />

        <!-- Cantidad -->
        <asp:TextBox ID="txtCantidad" runat="server"
            CssClass="form-control" placeholder="Cantidad" TextMode="Number" />
        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server"
            ControlToValidate="txtCantidad"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Debe ingresar una cantidad"
            ValidationGroup="vgPedido" />

        <!-- Estado -->
        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
            <asp:ListItem Text="Estado del pedido" Value="" />
            <asp:ListItem Text="Pendiente" Value="Pendiente" />
            <asp:ListItem Text="Entregado" Value="Entregado" />
            <asp:ListItem Text="Cancelado" Value="Cancelado" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvEstado" runat="server"
            ControlToValidate="ddlEstado"
            InitialValue=""
            ErrorMessage="Debe seleccionar un estado"
            CssClass="alert alert-warning"
            Display="Dynamic"
            ValidationGroup="vgPedido" />

        <!-- Botones -->
        <asp:Button ID="btnGuardar" runat="server"
            CssClass="btn btn-primary btn-hover-move"
            Text="Guardar"
            OnClick="btnGuardar_Click"
            ValidationGroup="vgPedido" />

        <asp:Button ID="btnActualizar" runat="server"
            Visible="false"
            CssClass="btn btn-primary btn-hover-move"
            Text="Actualizar"
            OnClick="btnActualizar_Click"
            ValidationGroup="vgPedido" />

        <asp:Button ID="btnCancelar" runat="server"
            Visible="false"
            SkinID="DangerButton"
            CssClass="btn btn-danger btn-hover-move"
            Text="Cancelar"
            OnClick="btnCancelar_Click"
            CausesValidation="false" />

        <asp:ValidationSummary ID="vsPedido"
            ValidationGroup="vgPedido"
            runat="server"
            CssClass="alert alert-warning"
            HeaderText="Corrige los siguientes errores:"
            DisplayMode="BulletList" />
    </div>

    <!-- GRID PRINCIPAL -->
    <asp:GridView ID="gvPedidos" runat="server"
        CssClass="table table-striped table-hover shadow-sm"
        BorderStyle="None"
        CellPadding="6"
        GridLines="None"
        AutoGenerateColumns="False"
        DataKeyNames="IdPedido,IdCliente"
        OnRowDeleting="gvPedidos_RowDeleting"
        OnSelectedIndexChanged="gvPedidos_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-primary" />
            <asp:BoundField DataField="IdPedido" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" />
            <asp:BoundField DataField="FechaPedido" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" />
            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
    </asp:GridView>

    <!-- GRID DETALLE -->
    <h4 class="mt-4">Detalle del pedido seleccionado</h4>

    <asp:GridView ID="gvDetalle" runat="server"
        CssClass="table table-striped table-hover shadow-sm"
        BorderStyle="None"
        CellPadding="6"
        GridLines="None"
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="IdDetalle" HeaderText="ID Detalle" ReadOnly="True" />
            <asp:BoundField DataField="IdProducto" HeaderText="ID Producto" />
            <asp:BoundField DataField="TipoProducto" HeaderText="Tipo" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:N2}" />
        </Columns>
    </asp:GridView>

</asp:Content>
