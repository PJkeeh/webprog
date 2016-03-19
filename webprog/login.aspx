<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="webprog.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <p><label class="loginWidth">Login:</label><asp:TextBox ID="txtLogin" runat="server"></asp:TextBox></p>
        <p><label class="loginWidth">Passwoord:</label><asp:TextBox TextMode="Password" ID="txtPassword" runat="server"></asp:TextBox></p>
    </form>
</asp:Content>
