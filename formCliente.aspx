<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormCliente.aspx.vb" Inherits="Proyecto_Final_Josias_Cisneros.formCliente" %>

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

        <!-- Nombre -->
        <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ValidationGroup="vgCliente"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Se requiere el nombre"
            ControlToValidate="txtNombre"></asp:RequiredFieldValidator>

        <!-- Apellido -->
        <asp:TextBox ID="txtApellido" CssClass="form-control" placeholder="Apellido" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ValidationGroup="vgCliente"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Se requiere el apellido"
            ControlToValidate="txtApellido"></asp:RequiredFieldValidator>

        <!-- Email -->
        <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Correo electrónico" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="vgCliente"
            Display="Dynamic"
            CssClass="alert alert-warning"
            ErrorMessage="Se requiere el email"
            ControlToValidate="txtEmail"></asp:RequiredFieldValidator>

        <!-- Dirección -->
        <asp:TextBox ID="txtDireccion" CssClass="form-control" placeholder="Dirección" runat="server"></asp:TextBox>

        <!-- Botones -->
        <asp:Button ID="btnGuardar" runat="server"
            CssClass="btn btn-primary btn-hover-move"
            Text="Guardar"
            OnClick="btnGuardar_Click"
            ValidationGroup="vgCliente" />

        <asp:Button ID="btnActualizar" runat="server"
            Visible="false"
            CssClass="btn btn-primary btn-hover-move"
            Text="Actualizar"
            OnClick="btnActualizar_Click"
            ValidationGroup="vgCliente" />

        <asp:Button ID="btnCancelar" runat="server"
            Visible="false"
            SkinID="DangerButton"
            CssClass="btn btn-danger btn-hover-move"
            Text="Cancelar"
            OnClick="btnCancelar_Click"
            CausesValidation="False" />

        <asp:ValidationSummary ID="vsCliente" ValidationGroup="vgCliente" runat="server" ShowSummary="true"
            CssClass="alert alert-warning"
            HeaderText="Corrige los siguientes errores:"
            DisplayMode="BulletList" />
    </div>

    <asp:GridView ID="gvClientes" CssClass="table table-striped table-hover shadow-sm"
        BorderStyle="None"
        CellPadding="6"
        GridLines="None"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="IdCliente"
        OnRowDeleting="gvClientes_RowDeleting"
        OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-primary" />
            <asp:BoundField DataField="IdCliente" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
    </asp:GridView>

</asp:Content>
