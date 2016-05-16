<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="account.aspx.cs" Inherits="webprog.account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftContent">
        <div class="content" runat="server" id="details">

        </div>
    </div>

    <div class="rightContent">
        <div class="content">
            <p><a href="abonnement.aspx">Abonnement bekijken</a></p>
            <p><a href="account.aspx">Aankopen bekijken</a></p>
            <p><a href="updatePassword.aspx">Wachtwoord wijzigen</a></p>
        </div>
    </div>
</asp:Content>
