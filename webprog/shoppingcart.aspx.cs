using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class Shoppingcart : pageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
            if (Session["shoppingCart"] != null && ((List<Ticket>)Session["shoppingCart"]).Count != 0)
            {
                cart.InnerHtml = "";
                List<Ticket_type> ticketTypes = new TicketService().getTicket_types();
                List<Ticket> shoppingCart = (List<Ticket>)Session["shoppingCart"];

                List<Match> matches = countMatches(shoppingCart);

                for (int i = 0; i < matches.Count; i++)
                {
                    cart.InnerHtml += "<h2>" + matches[i].homeTeam.name + "- " + matches[i].awayTeam.name + " " + matches[i].date.ToShortDateString() + "</h2>";
                    for (int k = 0; k < ticketTypes.Count; k++)
                    {
                        int amount = 0;
                        for (int j = 0; j < shoppingCart.Count; j++)
                        {
                            if (matches[i].id == shoppingCart[j].match.id)
                            {
                                if (shoppingCart[j].ticket_type.id == ticketTypes[k].id)
                                {
                                    amount++;
                                }
                            }
                        }
                        if (amount != 0)
                        {
                            Club club = new Club();
                            if (ticketTypes[k].hometeam)
                            {
                                club = matches[i].homeTeam;
                            }
                            else
                            {
                                club = matches[i].homeTeam;
                            }
                            if(amount == 1)
                                cart.InnerHtml += "<p><b>" + amount + "</b> ticket voor <b>" + club.name.Trim() + ": " + ticketTypes[k].name + "</b></p>";
                            else
                                cart.InnerHtml += "<p><b>" + amount + "</b> tickets voor " + club.name.Trim() + ": " + ticketTypes[k].name + "</p>";
                        }
                    }
                }
            }
        }

        private List<Match> countMatches(List<Ticket> cart)
        {
            List<Match> retVal = new List<Match>();
            
            for (int i = 0; i < cart.Count; i++)
            {
                Boolean contains = false;
                for(int j = 0; j < retVal.Count; j++)
                {
                    if(retVal[j].id == cart[i].match.id)
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    retVal.Add(cart[i].match);
                }
            }

            return retVal;
        }
    }
}