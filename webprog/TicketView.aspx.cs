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
                if (Request.QueryString["match"]==null || matchService.getMatch(Convert.ToInt32(Request.QueryString["match"]))==null){
                    content.InnerHtml = "<h1>Geen ticket geselecteerd.</h1>"
                        +"<p>Klik <a href=\"clubs.aspx\">hier</a> om een club te selecteren "
                        +"of <a href=\"calendar.aspx\">hier</a> om de kalender te bekijken.";
                }
                else
                {
                    TicketService ticketService = new TicketService();
                    
                    int match_id = Convert.ToInt32(Request.QueryString["match"]);

                    Match m = matchService.getMatch(match_id);

                    List<int[]> intTickets= matchService.getTicketsAvailable(m);
                   
                    for(int i = 0; i<intTickets.Count; i++) {
                        content.InnerHtml += "<p>" + ticketService.getTicket_type(intTickets.ElementAt(i)[0]).name + " (" + intTickets.ElementAt(i)[1].ToString() + "/" + intTickets.ElementAt(i)[2].ToString() + ")</p>";
                    }
                }
            }
        }
    }
}