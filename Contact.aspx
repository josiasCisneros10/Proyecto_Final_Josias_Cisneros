<%@ Page Title="Contact" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.vb" Inherits="Proyecto_Final_Josias_Cisneros.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
    <h2 id="title"><%: Title %></h2>
   
    <div class="mb-3">
        <strong>Address:</strong><br />
        Avenida Central, Desamparados, Alajuela, Costa Rica
    </div>

    <div class="mb-3">
        <strong>Phone:</strong><br />
        +506 7356-8212
    </div>

    <div class="mb-3">
        <strong>Email:</strong><br />
        Support: <a href="mailto:support@xtechnology.com">support@xtechnology.com</a><br />
        Marketing: <a href="mailto:marketing@xtechnology.com">marketing@xtechnology.com</a>
    </div>
</main>
</asp:Content>
