<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="ticketSale.aspx.cs" Inherits="webprog.ticketSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <h1 id="contentTitle" runat="server"></h1>
        <div id="content" runat="server">
        </div>
        <form id="buyForm" runat="server">
            <input id="amount" type="number" runat="server" />
            <br />
            <asp:Button ID="ticket_add" runat="server" Text="Toevoegen aan winkelmandje" OnClick="ticket_add_Click" />
        </form>
    </div>
</asp:Content>
