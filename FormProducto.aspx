<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormProducto.aspx.vb" Inherits="Proyecto_Final_Josias_Cisneros.FormProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .btn-hover-move {
            transition: transform 0.5s ease, box-shadow 0.2s;
        }

            .btn-hover-move:hover {
                transform: translateY(-4px) scale(1.04);
                box-shadow: 0 6px 18px rgba(0,0,0,0.15);
            }
    </style>

    <asp:HiddenField ID="editando" runat="server" />

    <div class="container d-flex flex-column mb-3 gap-2">

        <%-- Tipo de producto --%>
        <asp:TextBox ID="txtTipoProducto" CssClass="form-control" placeholder="Tipo de producto" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvTipoProducto" runat="server" ValidationGroup="vgProducto"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Se requiere el tipo de producto"
            ControlToValidate="txtTipoProducto"></asp:RequiredFieldValidator>

        <%-- Marca --%>
        <asp:TextBox ID="txtMarca" CssClass="form-control" placeholder="Marca" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvMarca" runat="server" ValidationGroup="vgProducto"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Se requiere la marca"
            ControlToValidate="txtMarca"></asp:RequiredFieldValidator>

        <%-- Modelo --%>
        <asp:TextBox ID="TxtModelo" CssClass="form-control" placeholder="Modelo" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvModelo" runat="server" ValidationGroup="vgProducto"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Se requiere el modelo"
            ControlToValidate="TxtModelo"></asp:RequiredFieldValidator>

        <%-- Precio --%>
        <asp:TextBox ID="txtPrecio" CssClass="form-control" placeholder="Precio" runat="server" TextMode="Number"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ValidationGroup="vgProducto"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Se requiere el precio"
            ControlToValidate="txtPrecio"></asp:RequiredFieldValidator>

        <%-- Cantidad --%>
        <asp:TextBox ID="txtCantidad" CssClass="form-control" placeholder="Cantidad" runat="server" TextMode="Number"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ValidationGroup="vgProducto"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Se requiere la cantidad"
            ControlToValidate="txtCantidad"></asp:RequiredFieldValidator>

        <%-- Botones --%>
        <asp:Button ID="btnGuardar" runat="server"
            CssClass="btn btn-primary btn-hover-move"
            Text="Guardar"
            OnClick="btnGuardar_Click"
            ValidationGroup="vgProducto" />

        <asp:Button ID="btnActualizar" runat="server"
            Visible="false"
            CssClass="btn btn-success btn-hover-move"
            Text="Actualizar"
            OnClick="btnActualizar_Click"
            ValidationGroup="vgProducto" />

        <asp:Button ID="btnCancelar" runat="server"
            Visible="false"
            CssClass="btn btn-danger btn-hover-move"
            Text="Cancelar"
            OnClick="btnCancelar_Click"
            CausesValidation="False" />

        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>

        <asp:ValidationSummary ID="vsProducto" ValidationGroup="vgProducto" runat="server" ShowSummary="true"
            CssClass="alert alert-warning"
            HeaderText="Corrige los siguientes errores:"
            DisplayMode="BulletList" />
    </div>

    <asp:GridView ID="gvProducto" CssClass="table table-striped table-hover table-success"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="IdProducto"
        OnRowDeleting="gvProducto_RowDeleting"
        OnSelectedIndexChanged="gvProducto_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-success" />
            <asp:BoundField DataField="IdProducto" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="TipoProducto" HeaderText="Tipo" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
    </asp:GridView>

</asp:Content>


