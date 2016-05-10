<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="calendar.aspx.cs" Inherits="webprog.Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftContent">
        <div id="matchesID" runat="server" class="content">
            <h1>Kalender</h1>
        </div>
    </div>
    <div class ="rightContent">
        <div id="filter" class="content">
            <div>CalenderFilterPlaceholder</div>
            <div>ClubFilterPlaceholder</div>
        </div>
   </div>
</asp:Content>
