<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormProducto.aspx.vb" Inherits="Proyecto_Final_Josias_Cisneros.FormProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="editando" runat="server"/> 

    <div class="container d-flex flex-column mb-3 gap-2">

    <asp:TextBox ID="txtTipoProducto" CssClass="form-control" placeholder="Tipo de producto" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="txtTipoProducto" ErrorMessage="Tipo de producto requerido" ForeColor="Red" Display="Dynamic" runat="server" />

    <asp:TextBox ID="txtMarca" CssClass="form-control" placeholder="Marca" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ControlToValidate="txtTipoProducto" ErrorMessage="Marca requerida" ForeColor="Red" Display="Dynamic" runat="server" />

    <asp:TextBox ID="TxtModelo" CssClass="form-control" placeholder="Modelo" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ControlToValidate="txtTipoProducto" ErrorMessage="Modelo requerido" ForeColor="Red" Display="Dynamic" runat="server" />

    <asp:TextBox ID="txtPrecio" CssClass="form-control" placeholder="Precio" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ControlToValidate="txtTipoProducto" ErrorMessage="Precio requerido" ForeColor="Red" Display="Dynamic" runat="server" />

    <asp:TextBox ID="txtCantidad" CssClass="form-control" placeholder="Cantidad" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ControlToValidate="txtTipoProducto" ErrorMessage="Cantidad requerida" ForeColor="Red" Display="Dynamic" runat="server" />
    
    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary btn-hover-move" Text="Guardar" OnClick="btn_guardar" />
    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-primary btn-hover-move" Text="Actualizar" OnClick="btnActualizar_Click" />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    </div>

    <asp:GridView ID="gvProducto" runat="server" AutoGenerateColumns="False" DataKeyNames="IdProducto" DataSourceID="SqlDataSource1" 
        OnRowDeleting="gvProducto_RowDeleting" OnRowCancelingEdit="gvProducto_RowCancelingEdit" OnRowUpdating="gvProducto_RowUpdating" OnSelectedIndexChanged="gvProducto_SelectedIndexChanged" Width="918px" Height="155px">
        <Columns>
            <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass ="btn btn-success" />
            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass ="btn btn-primary" />
            <asp:BoundField DataField="IdProducto" HeaderText="IdProducto" InsertVisible="False" ReadOnly="True" SortExpression="IdProducto" />
            <asp:BoundField DataField="TipoProducto" HeaderText="TipoProducto" SortExpression="TipoProducto" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" SortExpression="Marca" />
            <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass ="btn btn-danger" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Proyecto_FinalConnectionString %>" SelectCommand="SELECT * FROM [Producto]"></asp:SqlDataSource>
</asp:Content>

