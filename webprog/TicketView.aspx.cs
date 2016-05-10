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

                        matchOver_title.InnerHtml = m.homeTeam.name + " - " + m.awayTeam.name + "(" + m.date.ToShortDateString() + ")";
                        int sold = 0;
                        int total = 0;
                        for (int i = 0; i < intTickets.Count; i++)
                        {
                            sold += intTickets.ElementAt(i)[1];
                            total += intTickets.ElementAt(i)[2];
                        }
                        if (sold == total)
                            matchOver_tickets.InnerHtml = "Het " + m.homeTeam.stadion.name.ToString() + " was op " + m.date.ToShortDateString() + " met " + sold.ToString() + " verkochte ticket(s) uitverkocht.";
                        else
                            matchOver_tickets.InnerHtml = "Er waren " + sold.ToString() + " ticket(s) van de " + total.ToString() + " verkocht.";

                        matchOver_date.InnerHtml = m.date.Date.ToShortDateString();
                    }
                    else if (sale_too_soon)
                    {
                        content.InnerHtml = "";
                        matchOver.InnerHtml = "";

                        ticketSaleClosed_title.InnerHtml = m.homeTeam.name + " - " + m.awayTeam.name + "(" + m.date.ToShortDateString() + ")";
                        ticketSaleClosed_OpenDate.InnerHtml = m.date.Date.AddMonths(-1).ToShortDateString();
                    }
                    else
                    {
                        String[] innerHTMLs = { "", "" };
                        int j; //Used to place it in the right innerHTMLs

                        for (int i = 0; i < intTickets.Count; i++)
                        {
                            ticketSaleClosed.InnerHtml = "";
                            matchOver.InnerHtml = "";

                            Ticket_type tt = ticketService.getTicket_type(intTickets.ElementAt(i)[0]);
                            
                            Club club = new Club();
                            if (tt.hometeam)
                            {
                                club = m.homeTeam;
                                j = 0;
                            }
                            else
                            {
                                club = m.awayTeam;
                                j = 1;
                            }

                            if (intTickets.ElementAt(i)[1] == intTickets.ElementAt(i)[2])
                            {
                                innerHTMLs[j] += "<p>" + club.name +": " + tt.name.Trim() + " (" + intTickets.ElementAt(i)[1].ToString() + "/" + intTickets.ElementAt(i)[2].ToString() + ") </p>";
                            }
                            else
                            {
                                innerHTMLs[j] += "<p><a href=\"ticketSale.aspx?ticket=" + tt.id + "&match=" + m.id + "\">" + club.name.Trim()+": " + tt.name   + " (" + intTickets.ElementAt(i)[1].ToString() + "/" + intTickets.ElementAt(i)[2].ToString() + ")</a></p>";
                            }
                        }

                        ticketStats.InnerHtml += "<h2>Thuisplaatsen</h2>" + innerHTMLs[0]
                                               + "<br /><h2>Bezoekersplaatsen</h2>" + innerHTMLs[1];

                        content_title.InnerHtml = m.homeTeam.name + " - " + m.awayTeam.name + "(" + m.date.ToShortDateString() + ")";
                    }
                }
            }
        }
    }
}