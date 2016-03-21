<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="clubs.aspx.cs" Inherits="webprog.clubs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftContent">
        <div class="content" >
            <div id="clubSelected" runat="server"></div>
            <form runat="server">
                    <asp:DropDownList ID="ddlClubs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClubs_SelectedIndexChanged" AppendDataBoundItems="True">
                        <asp:ListItem>Selecteer een team:</asp:ListItem>

                    </asp:DropDownList>
                </form>
        </div>
    </div>

    <div class="rightContent">
        <div class="content" id="matchesID" runat="server"></div>
    </div>
</asp:Content>
