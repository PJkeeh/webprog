<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="newPassword.aspx.cs" Inherits="webprog.newPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content" runat="server" id="notWorking">Sorry, deze link lijkt niet te werken.</div>
    <div class="content" runat="server" id="working">
        <form runat="server">
        <p>
            <label class="registerLabel">Nieuw wachtwoord:</label>
            <asp:TextBox ID="txtNewPassword" runat="server"></asp:TextBox>
            <label id="lblNewPassError" runat="server" class="errorLabel"></label>
        </p>
        <p>
            <label class="registerLabel">herhaal nieuw wachtwoord:</label>
            <asp:TextBox ID="txtNewPassword2" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnChangePassword" runat="server" Text="Aanpassen" OnClick="btnChangePassword_Click"/>
        </p>
        </form>
    </div>
</asp:Content>
