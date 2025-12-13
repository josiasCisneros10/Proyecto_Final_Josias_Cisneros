<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormCarrito.aspx.vb" Inherits="Proyecto_Final_Josias_Cisneros.FormCarrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card p-4 shadow-sm mb-3">
        <h2 class="mb-4">Carrito de compras</h2>
    </div>

    <!-- Grid del carrito -->
    <asp:GridView ID="gvCarrito" runat="server"
        CssClass="table table-striped table-hover shadow-sm"
        BorderStyle="None"
        CellPadding="6"
        GridLines="None"
        AutoGenerateColumns="False"
        OnRowDeleting="gvCarrito_RowDeleting"
        DataKeyNames="IdProducto">

        <Columns>
            <asp:BoundField DataField="TipoProducto" HeaderText="Tipo" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:N2}" />

            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
    </asp:GridView>

    <!-- Total y botones -->
    <div class="d-flex justify-content-between align-items-center mt-3">
        <asp:Label ID="lblTotal" runat="server" CssClass="h4"></asp:Label>

        <asp:Button ID="btnConfirmar" runat="server"
            Text="Confirmar compra"
            CssClass="btn btn-success btn-hover-move"
            OnClick="btnConfirmar_Click" />
    </div>

</asp:Content>
