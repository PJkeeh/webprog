<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="webprog.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <p>
        <label class="registerLabel">Login:</label>
        <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
        <label id="lblLoginError" runat="server" class="errorLabel"></label>
    </p>
    <p>
       <label class="registerLabel">Wachtwoord:</label>
       <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
       <label id="lblPasswordError" runat="server" class="errorLabel"></label>
    </p>
    <p>
       <label class="registerLabel">Wachtwoord herhalen:</label>
       <asp:TextBox ID="txtPassword2" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnRegister" runat="server" Text="Button" OnClick="btnRegister_Click" />
    </p>
    </form>
</asp:Content>
