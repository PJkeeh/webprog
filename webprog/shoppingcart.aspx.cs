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

                String error = allowedToBuy(shoppingCart);

                if (error != null)
                {
                    cart.InnerHtml += "<div class=\"errorLabel\" >" + error + "</div>";
                    buy.Enabled = false;
                }

                for (int i = 0; i < matches.Count; i++)
                {
                    cart.InnerHtml += "<h2>" + matches[i].homeTeam.name + "- " + matches[i].awayTeam.name 
                        + " " + matches[i].date.ToShortDateString() + "</h2>";
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
                            cart.InnerHtml += "<p><a href=\"deleteShopping.aspx?tt=" + ticketTypes[k].id 
                                + "&m=+" + matches[i].id 
                                + "\"><img src=\"img/remove.png\" height=\"12px\" /></a>";
                            if (amount == 1)
                                cart.InnerHtml += "<b>" + amount + "</b> ticket voor <b>" 
                                    + club.name.Trim() + ": " + ticketTypes[k].name + "</b></p>";
                            else
                                cart.InnerHtml += "<b>" + amount + "</b> tickets voor " 
                                    + club.name.Trim() + ": " + ticketTypes[k].name + "</p>";
                        }
                    }
                }
            }
            else{
                buy.Enabled = false;
            }
        }

        private List<Match> countMatches(List<Ticket> cart)
        {
            List<Match> retVal = new List<Match>();

            for (int i = 0; i < cart.Count; i++)
            {
                Boolean contains = false;
                for (int j = 0; j < retVal.Count; j++)
                {
                    if (retVal[j].id == cart[i].match.id)
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

        private String allowedToBuy(List<Ticket> cart)
        {
            String retVal = null;
            List<String> errors = new List<String>();

            Boolean failed = false;
            for (int i = 0; i < cart.Count; i++)
            {
                if (failed)
                    break;

                for (int j = 0; j < cart.Count; j++)
                {
                    if (failed)
                        break;
                    if (cart[i].match.id != cart[j].match.id)
                    {
                        if (cart[i].match.date.Date.Equals(cart[j].match.date.Date))
                        {
                            errors.Add("Je kan geen verschillende matchen bekijken op dezelfde dag.");
                            failed = true;
                        }
                    }
                }
            }
            failed = false;


            if(errors.Count > 0)
            {
                retVal = "";
                for (int i = 0; i < errors.Count; i++)
                {
                    retVal += errors[i] + "</ br>";
                }
                retVal.Substring(0, retVal.Length - 6);
            }

            return retVal;
        }

        protected void buy_Click(object sender, EventArgs e)
        {
            if (Session["shoppingCart"] != null && ((List<Ticket>)Session["shoppingCart"]).Count != 0)
            {
                List<Ticket> shoppingCart = (List<Ticket>)Session["shoppingCart"];

                String error = allowedToBuy(shoppingCart);
                if (allowedToBuy(shoppingCart) == null)
                {
                    buy_tickets();
                }
            }
        }

        private void buy_tickets()
        {
            if (Session["shoppingCart"] != null && ((List<Ticket>)Session["shoppingCart"]).Count != 0)
            {
                List<Ticket> shoppingCart = (List<Ticket>)Session["shoppingCart"];

                TicketService t = new TicketService();

                t.buyTickets(shoppingCart);
                Session["shoppingCart"] = null;
            }
        }
    }
}