<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="shoppingcart.aspx.cs" Inherits="webprog.Shoppingcart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="content">
            <h1>Shopping cart</h1>
            <div id="cart" runat="server">
                Het winkelwagentje is leeg.
            </div>
            <asp:Button ID="buy" runat="server" Text="Koop tickets" OnClick="buy_Click" />
        </div>
    </form>
</asp:Content>
