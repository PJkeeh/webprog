<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="clubs.aspx.cs" Inherits="webprog.clubs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="clubSelected" runat="server">
        
    </div>
    <form runat="server">
        <asp:DropDownList ID="ddlClubs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClubs_SelectedIndexChanged"></asp:DropDownList>
    </form>
</asp:Content>
