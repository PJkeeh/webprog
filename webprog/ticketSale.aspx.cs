using System;
using System.Collections.Generic;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class ticketSale : pageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
            else {
                TicketService ticketService = new TicketService();
                MatchService matchService = new MatchService();
                if (Request.QueryString["ticket"] == null
                    || ticketService.getTicket_type(Convert.ToInt32(Request.QueryString["ticket"])) == null
                    || Request.QueryString["match"] == null
                    || matchService.getMatch(Convert.ToInt32(Request.QueryString["match"])) == null)
                {
                    Response.Redirect("index.aspx");
                }
                else
                {
                    Ticket_type tt = new TicketService().getTicket_type(Convert.ToInt32(Request.QueryString["ticket"]));

                    Match m = matchService.getMatch(Convert.ToInt32(Request.QueryString["match"]));
                    List<int[]> tickNum = new MatchService().getTicketsAvailable(m);

                    contentTitle.InnerHtml = m.homeTeam + " - " + m.awayTeam + "(" + m.date.ToLongDateString() + ")";

                    if (tickNum[tt.id][1] < tickNum[tt.id][2])
                    {
                        int numBought = ticketService.getTicketsOfLoginOnMatch(getLogin(), m).Count;
                        content.InnerHtml = "You bought " + numBought + " already. Available:" + tickNum[tt.id][1].ToString() + "/" + tickNum[tt.id][2].ToString();
                    }
                    else
                    {
                        content.InnerHtml = "De tickets zijn uitverkocht.";
                    }
                }
            }
        }
    }
}