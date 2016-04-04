<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="ticketView.aspx.cs" Inherits="webprog.TicketView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content" runat="server">
        <div class="content">
            <h1 id="content_title" runat="server"></h1>
        </div>
        <div id ="matchOver" class="content" runat="server">
            <h1 id="matchOver_title" runat ="server"></h1>
            <p>De match werd gespeeld op <span id="MatchOver_date" runat="server"></span>.</p>
            <p id="matchOver_tickets" runat="server"></p>
        </div>
        <div id ="ticketSaleClosed" class="content" runat="server">
            <h1 id="ticketSaleClosed_title" runat ="server"></h1>
            <p>De ticketverkoop start op <span id="ticketSaleClosed_OpenDate" runat="server"></span></p>
        </div>
    </div>
</asp:Content>