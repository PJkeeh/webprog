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
                    for (int k = 0; k < ticketTypes.Count; k++)
                    {
                        int amount = 0;
                        for (int j = 0; j < shoppingCart.Count; j++)
                        {
                            if (matches[i] == shoppingCart[j].match)
                            {
                                if (shoppingCart[j].ticket_type.id == ticketTypes[k].id)
                                {
                                    amount++;
                                }
                            }
                        }
                        if (amount != 0)
                            cart.InnerHtml += "<p><b>" + amount + "</b> tickets voor <b>" + ticketTypes[k].name + "</b></p>";
                    }
                }
            }
        }

        private List<Match> countMatches(List<Ticket> cart)
        {
            List<Match> retVal = new List<Match>();

            for (int i = 0; i < cart.Count; i++)
            {
                if (!retVal.Contains(cart[i].match))
                {
                    retVal.Add(cart[i].match);
                }
            }

            return retVal;
        }
    }
}