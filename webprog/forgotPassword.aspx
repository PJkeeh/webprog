<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="forgotPassword.aspx.cs" Inherits="webprog.forgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" method="post" action="recoverPassword.aspx" class="content">
    <p>
        <label class="registerLabel">Gebruikersnaam:</label>
        <input type="text" name="loginname" />
    </p>
    <p>
        <label class="registerLabel">E-mailadres:</label>
        <input type="text" name="email" />
    </p>
    <p>
        <input type="submit" value="Verstuur" />
    </p>
    </form>
</asp:Content>
