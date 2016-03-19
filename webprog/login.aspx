<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="webprog.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <label id="lblError" runat="server" class="errorLabel"></label>
        <p>
            <label class="loginWidth">Login:</label><asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
        </p>
        <p>
            <label class="loginWidth">Passwoord:</label><asp:TextBox TextMode="Password" ID="txtPassword" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </p>
    </form>
</asp:Content>
