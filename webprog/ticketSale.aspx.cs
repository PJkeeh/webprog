using System;
using System.Collections.Generic;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class ticketSale : pageFunctions
    {
        private int numBought = 0;
        private int maxBuy = 10;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
            else {
                if (Request.QueryString["match"] == null
                 || Request.QueryString["ticket"] == null
                 || new MatchService().getMatch(Convert.ToInt32(Request.QueryString["match"])) == null
                 || new TicketService().getTicket_type(Convert.ToInt32(Request.QueryString["ticket"])) == null)
                {
                    Response.Redirect("calendar.aspx");
                }
                else
                {
                    TicketService ticketService = new TicketService();
                    MatchService matchService = new MatchService();

                    Ticket_type tt = new TicketService().getTicket_type(Convert.ToInt32(Request.QueryString["ticket"]));

                    Match m = matchService.getMatch(Convert.ToInt32(Request.QueryString["match"]));
                    List<int[]> tickNum = new MatchService().getTicketsAvailable(m);

                    contentTitle.InnerHtml = m.homeTeam + " - " + m.awayTeam + "(" + m.date.ToLongDateString() + ")";

                    if (tickNum[tt.id][1] < tickNum[tt.id][2])
                    {
                        numBought = ticketService.getTicketsOfLoginOnMatch(getLogin(), m).Count;
                        content.InnerHtml = "<p>You bought " + numBought + " already. Available:" + tickNum[tt.id][1].ToString() + "/" + tickNum[tt.id][2].ToString() + "</p>";

                    }
                    else
                    {
                        content.InnerHtml = "De tickets zijn uitverkocht.";
                    }

                }
            }
        }

        protected void ticket_add_Click(object sender, EventArgs e)
        {
            int wannaBuy = Convert.ToInt32(amount.Value);

            Match m = new MatchService().getMatch(Convert.ToInt32(Request.QueryString["match"]));
            Ticket_type tt = new TicketService().getTicket_type(Convert.ToInt32(Request.QueryString["ticket"]));

            if (wannaBuy + numBought > maxBuy)
            {
                //Show an error
            }
            else if (new MatchService().getTicketsAvailableOfTicketType(m, tt)[1] + wannaBuy > new MatchService().getTicketsAvailableOfTicketType(m, tt)[2])
            {
                //Show an error
            }
            else
            {
                if (Session["shoppingCart"] == null)
                {
                    Session["shoppingCart"] = new List<Ticket>();
                }

                Login l = new LoginService().getLogin(getLogin());

                List<Ticket> shoppingCart = (List<Ticket>)Session["shoppingCart"];
                for (int i = 0; i < wannaBuy; i++)
                {
                    shoppingCart.Add(new Ticket
                    {
                        id = 0,
                        login = l,
                        match = m,
                        ticket_type = tt
                    });
                }

                Response.Redirect("shoppingcart.aspx");
            }
        }
    }
}