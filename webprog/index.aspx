<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/Site1.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="webprog.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftContent">
        <div class="content">
            <p class="textContent">
                Na de reguliere competitie worden er play-offs afgewerkt,
             waarbij de zes hoogst gerangschikte ploegen het nogmaals tweemaal tegen elkaar opnemen in play-off I.
             Deze matches zijn de belangrijkste van het seizoen, en daarom ook de spannenste!
             Bij de start van play-off I wordt het puntenaantal dat de deelnemende ploegen in de reguliere competitie hebben behaald,
             gedeeld door twee en naar boven afgerond. De ploegen herbeginnen dus niet vanaf 0 punten.
            </p>
            <p class="textContent">
                De verkoop van deze tickets gebeurt op twee manieren: een abonnement voor de hele play-offs, of tickets per match.
            Een abonnement kan enkel worden aangekocht voor de start van de eerste match. 
            Tickets kunnen aangekocht worden vanaf een maand voor de match.
            Het is niet mogelijk tickets te verkrijgen voor verschillende matches op dezelfde dag. 
            Per match kan een maximaal van tien tickets per persoon worden aangekocht.
            </p>
            <p class="textContent">Alvast veel voetbalplezier!</p>
            <br />
            <p class="textContent">Het VoetbalTickets team.</p>
        </div>
    </div>

    <div class="rightContent">
        <img src="./img/jpl-stier.png" alt="logo Jupiler Pro League" draggable="false" />
    </div>
</asp:Content>
