<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="shoppingcart.aspx.cs" Inherits="webprog.Shoppingcart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <h1>Shopping cart</h1>
        <div id="cart" runat="server">
            The shopping cart is empty.
        </div>
    </div>
</asp:Content>
