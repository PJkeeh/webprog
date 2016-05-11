<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="updatePassword.aspx.cs" Inherits="webprog.updatePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" method="post">
    <p>
        <label class="registerLabel">Oud wachtwoord:</label>
        <asp:TextBox ID="txtOldPass" runat="server" TextMode="password"></asp:TextBox>
        <label id="lblOldPassError" runat="server" class="errorLabel"></label>
    </p>
        <p>
        <label class="registerLabel">Nieuw wachtwoord:</label>
        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="password"></asp:TextBox>
        <label id="lblNewPassError" runat="server" class="errorLabel"></label>
    </p>
    <p>
        <label class="registerLabel">herhaal nieuw wachtwoord:</label>
        <asp:TextBox ID="txtNewPassword2" runat="server" TextMode="password"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnChangePassword" runat="server" Text="Aanpassen" OnClick="btnChangePassword_Click"/>
    </p>
    </form>
</asp:Content>
