<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Catalogo.aspx.vb" Inherits="Proyecto_Final_Josias_Cisneros.Catalogo" %>

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

    <div class="card p-4 shadow-sm mb-3">
        <h2 class="mb-4">Catalogo de productos</h2>
        <p class="text-muted">
        </p>
    </div>

    <asp:GridView ID="gvCatalogo" runat="server"
        CssClass="table table-striped table-hover shadow-sm"
        BorderStyle="None"
        CellPadding="6"
        GridLines="None"
        AutoGenerateColumns="False"
        DataKeyNames="IdProducto,TipoProducto,Marca,Modelo,Precio"
        OnRowCommand="gvCatalogo_RowCommand">

        <Columns>

            <asp:BoundField DataField="TipoProducto" HeaderText="Tipo" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:N2}" />


            <asp:TemplateField HeaderText="Cantidad">
                <ItemTemplate>
                    <asp:TextBox ID="txtCantidad" runat="server"
                        CssClass="form-control"
                        TextMode="Number"
                        Text="1" />
                </ItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Button ID="btnAgregar" runat="server"
                        Text="Agregar al carrito"
                        CssClass="btn btn-primary btn-hover-move"
                        CommandName="Agregar"
                        CommandArgument='<%# Container.DataItemIndex %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
