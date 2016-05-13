<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="ticketSale.aspx.cs" Inherits="webprog.ticketSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <h1 id="contentTitle" runat="server"></h1>
        <div id="content" runat="server">
        </div>
        <form id="buyForm" runat="server">
            <p class=".errorLabel" id="errorMessage" runat="server"></p>
            <asp:TextBox TextMode="Number" runat="server" ID="amount" Text="1" min="1" max="10" step="1"/> 
            <label id="priceLabel" runat="server"></label>
            <br />
            <asp:Button ID="ticket_add" runat="server" Text="Toevoegen aan winkelmandje" OnClick="ticket_add_Click" />
        </form>
    </div>
</asp:Content>
