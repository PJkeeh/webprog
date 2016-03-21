using System;
using System.Collections.Generic;
using System.Linq;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class TicketView : pageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
            else {
                MatchService matchService = new MatchService();
                if (Request.QueryString["match"] == null || matchService.getMatch(Convert.ToInt32(Request.QueryString["match"])) == null)
                {
                    content.InnerHtml = "<h1>Geen ticket geselecteerd.</h1>"
                        + "<p>Klik <a href=\"clubs.aspx\">hier</a> om een club te selecteren "
                        + "of <a href=\"calendar.aspx\">hier</a> om de kalender te bekijken.";
                }
                else
                {
                    TicketService ticketService = new TicketService();

                    int match_id = Convert.ToInt32(Request.QueryString["match"]);

                    Match m = matchService.getMatch(match_id);

                    Boolean sale_closed = m.date.Date < DateTime.Today.Date;
                    Boolean sale_too_soon = m.date.Date.AddMonths(-1) >= DateTime.Today.Date;

                    List<int[]> intTickets = matchService.getTicketsAvailable(m);

                    if (sale_closed)
                    {
                        content.InnerHtml = "";
                        ticketSaleClosed.InnerHtml = "";

                        matchOver_title.InnerHtml = m.homeTeam.name + " - " + m.awayTeam.name + "(" + m.date.ToString() + ")";
                        int sold = 0;
                        int total = 0;
                        for (int i = 0; i < intTickets.Count; i++)
                        {
                            sold += intTickets.ElementAt(i)[1];
                            total += intTickets.ElementAt(i)[2];
                        }
                        if (sold == total)
                            matchOver_tickets.InnerHtml = "Het " + m.homeTeam.stadion.name.ToString() + " was op " + m.date.ToShortDateString() + " met " + sold.ToString() + " verkochte tickets uitverkocht.";
                        else
                            matchOver_tickets.InnerHtml = "Er waren " + sold.ToString() + " tickets van de " + total.ToString() + " verkocht.";

                        MatchOver_date.InnerHtml = m.date.Date.ToString();
                    }
                    else if (sale_too_soon)
                    {
                        content.InnerHtml = "";
                        matchOver.InnerHtml = "";
                    
                        ticketSaleClosed_title.InnerHtml = m.homeTeam.name + " - " + m.awayTeam.name + "(" + m.date.ToShortDateString() + ")";
                        ticketSaleClosed_OpenDate.InnerHtml = m.date.Date.AddMonths(-1).ToShortDateString();
                    }
                    else {
                        for (int i = 0; i < intTickets.Count; i++)
                        {
                            ticketSaleClosed.InnerHtml = "";
                            matchOver.InnerHtml = "";

                            content.InnerHtml += "<p>" + ticketService.getTicket_type(intTickets.ElementAt(i)[0]).name + " (" + intTickets.ElementAt(i)[1].ToString() + "/" + intTickets.ElementAt(i)[2].ToString() + ")</p>";
                            content_title.InnerHtml = m.homeTeam.name + " - " + m.awayTeam.name + "(" + m.date.ToShortDateString() + ")";
                        }
                    }
                }
            }
        }
    }
}