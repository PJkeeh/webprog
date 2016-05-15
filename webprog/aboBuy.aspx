<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="aboBuy.aspx.cs" Inherits="webprog.abo_buy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftContent">
        <div class="content" id="clubSelected" runat="server"></div>
        <div class="content" id="available_abo" runat="server"></div>
    </div>
    <div class="rightContent">
        <div class="content">
            <form runat="server">
                <asp:DropDownList ID="ddlClubs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClubs_SelectedIndexChanged" AppendDataBoundItems="True">
                    <asp:ListItem>Selecteer een team:</asp:ListItem>

                </asp:DropDownList>
            </form>
        </div>
        </div>
</asp:Content>
