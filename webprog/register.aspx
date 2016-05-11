<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="webprog.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <p>*Verplichte velden</p>
    <p>
        <label class="registerLabel">Login*:</label>
        <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
        <label id="lblLoginError" runat="server" class="errorLabel"></label>
    </p>
        <p>
        <label class="registerLabel">Naam:</label>
        <asp:TextBox ID="txtNaam" runat="server"></asp:TextBox>
        <label id="lblNaamError" runat="server" class="errorLabel"></label>
    </p>
    <p>
        <label class="registerLabel">E-mailadres*:</label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <label id="lblEmailError" runat="server" class="errorLabel"></label>
    </p>
    <p>
       <label class="registerLabel">Wachtwoord*:</label>
       <asp:TextBox TextMode="password" ID="txtPassword" runat="server"></asp:TextBox>
       <label id="lblPasswordError" runat="server" class="errorLabel"></label>
    </p>
    <p>
       <label class="registerLabel">Wachtwoord herhalen*:</label>
       <asp:TextBox TextMode="password" ID="txtPassword2" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnRegister" runat="server" Text="Registreer" OnClick="btnRegister_Click" />
    </p>
    </form>
</asp:Content>
