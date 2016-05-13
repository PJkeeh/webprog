<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="calendar.aspx.cs" Inherits="webprog.Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftContent">
        <div id="matchesID" runat="server" class="content">
            <h1>Kalender</h1>
        </div>
    </div>
    <div class ="rightContent">
        <div id="abonnement" class="content" runat="server">
        </div>
        <div id="filter" class="content">
            <form runat="server">
                <asp:DropDownList ID="ddlSeizoen" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlseizoen_SelectedIndexChanged" AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">Alle seizoenen</asp:ListItem>

                </asp:DropDownList>
                <br />
                <asp:DropDownList ID="ddlClubs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlClubs_SelectedIndexChanged" AppendDataBoundItems="True">
                    <asp:ListItem Value="-1">Alle clubs</asp:ListItem>

                </asp:DropDownList>
            </form>
        </div>
   </div>
</asp:Content>
