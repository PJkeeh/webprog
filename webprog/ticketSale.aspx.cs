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
        private float price = 0;

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

                    Ticket_type tt = ticketService.getTicket_type(Convert.ToInt32(Request.QueryString["ticket"]));

                    Match m = matchService.getMatch(Convert.ToInt32(Request.QueryString["match"]));

                    price = ticketService.getTicket_TypePrice(tt, m.homeTeam);

                    List<int[]> tickNum = new MatchService().getTicketsAvailable(m);

                    contentTitle.InnerHtml = m.homeTeam.name + "- " + m.awayTeam.name + "(" + m.date.ToLongDateString() + ")";
                    updatePrice();

                    if (tickNum[tt.id][1] < tickNum[tt.id][2])
                    {
                        numBought = ticketService.getTicketsOfLoginOnMatch(getLogin(), m).Count;
                        content.InnerHtml = "<p>Je hebt al " + numBought + " tickets gekocht. Beschikbaar: " + tickNum[tt.id][1].ToString() + "/" + tickNum[tt.id][2].ToString() + "</p>";
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
            int wannaBuy = 0;
            if (Int32.TryParse(amount.Text, out wannaBuy))
            {

                Match m = new MatchService().getMatch(Convert.ToInt32(Request.QueryString["match"]));
                Ticket_type tt = new TicketService().getTicket_type(Convert.ToInt32(Request.QueryString["ticket"]));

                int available = new MatchService().getTicketsAvailableOfTicketType(m, tt)[1];

                if (wannaBuy > maxBuy)
                {
                    errorMessage.InnerHtml = "Een persoon mag slechts <b>" + maxBuy + "</b> ticket(s) kopen.";
                }
                else if (wannaBuy <= 0)
                {
                    errorMessage.InnerHtml = "Geef een gelig getal in van 1 tot " + maxBuy;
                }
                else if (wannaBuy + numBought > maxBuy)
                {
                    errorMessage.InnerHtml = "Je hebt al "
                        + numBought + " ticket(s) gekocht. Als je nog "
                        + wannaBuy + " ticket(s) bijkoopt, heb je "
                        + (numBought + wannaBuy - maxBuy) + " ticket(s) teveel. Een persoon mag slechts <b>"
                        + maxBuy + "</b> ticket(s) kopen.";
                }
                else if (available + wannaBuy > new MatchService().getTicketsAvailableOfTicketType(m, tt)[2])
                {
                    errorMessage.InnerHtml = "Er zijn slechts " + available + " tickets beschikbaar.";
                }
                else
                {
                    if (Session["shoppingCart"] == null)
                    {
                        Session["shoppingCart"] = new List<Ticket>();
                    }

                    Login l = new LoginService().getLogin(getLogin());

                    List<Ticket> shoppingCart = (List<Ticket>)Session["shoppingCart"];

                    int toBuy = getOfMatch(shoppingCart, m).Count;

                    if (toBuy + wannaBuy + numBought <= maxBuy)
                    {

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
                    else {
                        errorMessage.InnerHtml = "Je hebt al "
                            + numBought + " ticket(s) gekocht voor deze match. Momenteel bevinden zich al "
                            + toBuy + " ticket(s) in je winkelwagen. Bij het aankopen van nog "
                            + wannaBuy + " ticket(s) wordt het maximum van "
                            + maxBuy + " per match overschreden.";
                    }
                }
            } else
            {
                errorMessage.InnerHtml = "Geef een geldig getal op.";
            }
        }

        private List<Ticket> getOfMatch(List<Ticket> input, Match m)
        {
            List<Ticket> retVal = new List<Ticket>();

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].match.id == m.id)
                {
                    retVal.Add(input[i]);
                }
            }

            return retVal;
        }

        private void updatePrice()
        {
            priceLabel.InnerHtml = " * € " + price.ToString();
        }
    }
}