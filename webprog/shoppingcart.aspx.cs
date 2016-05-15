using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            Boolean buyingSomething = false;
            float totalPrice = 0;

            if (Session["shoppingCart"] != null && ((List<Ticket>)Session["shoppingCart"]).Count != 0)
            {
                buyingSomething = true;
                TicketService ticketservice = new TicketService();
                cart.InnerHtml = "";
                List<Ticket_type> ticketTypes = ticketservice.getTicket_types();
                List<Ticket> shoppingCart = (List<Ticket>)Session["shoppingCart"];

                List<Match> matches = countMatches(shoppingCart);

                string error = allowedToBuy(shoppingCart);

                if (error != null)
                {
                    cart.InnerHtml += "<div class=\"errorLabel\" >" + error + "</div>";
                    buy.Enabled = false;
                }

                for (int i = 0; i < matches.Count; i++)
                {
                    cart.InnerHtml += "<h2>" + matches[i].homeTeam.name + "- " + matches[i].awayTeam.name
                        + " " + string.Format("{0:dd-MM-yyyy}", matches.ElementAt(i).date) + "</h2>";

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

                            float price = ticketservice.getTicket_TypePrice(ticketTypes[k], club);
                            cart.InnerHtml += "<p><a href=\"deleteShopping.aspx?tt=" + ticketTypes[k].id
                                + "&m=+" + matches[i].id
                                + "\"><img src=\"img/remove.png\" height=\"12px\" /></a>";
                            if (amount == 1)
                                cart.InnerHtml += "<b>" + amount + "</b> ticket voor "
                                    + club.name.Trim() + ": " + ticketTypes[k].name + "(" + amount + " * € " + price + " = <b>" + (amount * price).ToString("0.00") + ")</b></p>";
                            else
                                cart.InnerHtml += "<b>" + amount + "</b> tickets voor "
                                    + club.name.Trim() + ": " + ticketTypes[k].name + "(" + amount + " * € " + price + " = <b>" + (amount * price).ToString("0.00") + ")</b></p>";
                            totalPrice += amount * price;
                        }
                    }
                }
            }
            if (Session["abo_cart"] != null)
            {
                if (!buyingSomething)
                {
                    cart.InnerHtml = "";
                }
                buyingSomething = true;
                Abonnement abo = (Abonnement)Session["abo_cart"];
                float price = new TicketService().getTicket_TypePrice(abo.ticket_type, abo.club) * 5;
                totalPrice = price;
                cart.InnerHtml += "<b>1</b> abonnement voor "
                                    + abo.club.name.Trim() + ": " + abo.ticket_type.name + "(" + 1 + " * € " + price.ToString("0.00") + " = <b>" + price.ToString("0.00") + ")</b></p>";

            }
            if (!buyingSomething)
            {
                buy.Enabled = false;
                buy.Visible = false;
            }
            else
            {
                cart.InnerHtml += "<p><b>Totaal:</b>" + totalPrice + "</p>";
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

        private string allowedToBuy(List<Ticket> cart)
        {
            List<Ticket> tickets = new TicketService().getAllOfLogin(getLogin());

            string retVal = null;
            List<string> errors = new List<string>();

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
                if (!failed)
                {
                    for (int j = 0; j < tickets.Count; j++)
                    {
                        if (cart[i].match.date.Date == tickets[j].match.date.Date && cart[i].match.id != tickets[j].match.id)
                        {
                            failed = true;
                            errors.Add("Je hebt al een ticket gekocht voor een andere match op " + string.Format("{0:dd-MM-yyyy}", cart[i].match.date.Date));
                            break;
                        }
                    }
                }
            }
            failed = false;


            if (errors.Count > 0)
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
            Login login = new LoginService().getLogin(getLogin());
            if (Session["shoppingCart"] != null && ((List<Ticket>)Session["shoppingCart"]).Count != 0)
            {
                List<Ticket> shoppingCart = (List<Ticket>)Session["shoppingCart"];

                string error = allowedToBuy(shoppingCart);
                if (allowedToBuy(shoppingCart) == null)
                {
                    List<Ticket> bought = buy_tickets();
                    send_mail(login, bought);
                    Session["shoppingCart"] = null;
                }
            }

            if(Session["abo_cart"] != null)
            {
                Abonnement abo = (Abonnement)Session["abo_cart"];
                AboService aboservice = new AboService();
                if (aboservice.getAbonnement(login, abo.seizoen) == null)
                {
                    aboservice.buyAbonnement(abo);
                    send_mail(login, abo);
                    Session["abo_cart"] = null;
                }
            }
            Response.Redirect(Request.Path);
        }

        private void send_mail(Domain.Login login, List<Ticket> t)
        {
            string mail = null;
            if (login.name != null && login.name.Trim() != "")
                mail = "Beste " + login.name + "\n";
            else
                mail = "Beste " + login.login + "\n";
            mail += "\n"
                + "Dit is uw bevestiging voor uw aangekochte tickets. Breng deze bevesting en de bijgevoegde vouchers mee naar het stadion samen met uw identiteitskaart. \n\n";
            for (int i = 0; i < t.Count; i++)
            {
                mail += "----------------------------------------------------------------\n";
                mail += t[i].match.homeTeam.name.Trim() + " - " + t[i].match.awayTeam.name.Trim() + " - " + string.Format("{0:dd-MM-yyyy}", t[i].match.date) + " " + t[i].id + "\n";
            }

            mail += "----------------------------------------------------------------\n";

            mail += "Gelieve op deze mail niet te antwoorden.\n"
                + "Bedankt voor uw aankoop!"
                + "\n\nHet VoetbalTickets team.";

            MailMessage o = new MailMessage("VoetbalTicketsVives@hotmail.com", login.email.Trim(), "Aankoop", mail);
            NetworkCredential netCred = new NetworkCredential("VoetbalTicketsVives@hotmail.com", "Webprogrammeren");
            SmtpClient smtpobj = new SmtpClient("smtp.live.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);
            //ADD CATCH;
        }
        private void send_mail(Domain.Login login, Abonnement a)
        {
            string mail = null;
            if (login.name != null && login.name.Trim() != "")
                mail = "Beste " + login.name + "\n";
            else
                mail = "Beste " + login.login + "\n";
            mail += "\n"
                + "Dit is uw bevestiging voor uw aangekocht "+a.club.name+" abonnement. Breng deze bevesting en de bijgevoegde voucher mee naar het stadion samen met uw identiteitskaart. \n\n";
            
            mail += "----------------------------------------------------------------\n";
            mail += a.abo_id + " " + a.club.name + " " + a.ticket_type + " " + a.login.name;
            
            mail += "----------------------------------------------------------------\n";

            mail += "Gelieve op deze mail niet te antwoorden.\n"
                + "Bedankt voor uw aankoop!"
                + "\n\nHet VoetbalTickets team.";

            MailMessage o = new MailMessage("VoetbalTicketsVives@hotmail.com", login.email.Trim(), "Aankoop", mail);
            NetworkCredential netCred = new NetworkCredential("VoetbalTicketsVives@hotmail.com", "Webprogrammeren");
            SmtpClient smtpobj = new SmtpClient("smtp.live.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);
            //ADD CATCH;
        }

        private List<Ticket> buy_tickets()
        {
            if (Session["shoppingCart"] != null && ((List<Ticket>)Session["shoppingCart"]).Count != 0)
            {
                List<Ticket> shoppingCart = (List<Ticket>)Session["shoppingCart"];

                TicketService t = new TicketService();

                return t.buyTickets(shoppingCart);
            }
            return null;
        }
    }
}