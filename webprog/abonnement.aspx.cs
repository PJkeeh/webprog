using System;
using System.Collections.Generic;
using System.Linq;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class Abo : pageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                LoginService loginservice = new LoginService();
                Login login = loginservice.getLogin(getLogin());
                if (login == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    SeizoenService seizoenService = new SeizoenService();
                    Seizoen seizoen_current = seizoenService.getCurrent();
                    Seizoen seizoen_next = seizoenService.getNext();

                    AboService aboService = new AboService();

                    if (seizoen_next != null)
                    {
                        Abonnement abo_next = aboService.getAbonnement(login, seizoen_next);
                        abo_next_season.InnerHtml = "<h1>" + seizoen_next + "</h1><p>Het volgende seizoen start op " + string.Format("{0:dd-MM-yyyy}", seizoen_next.startDate.Date) + ".";
                        if(abo_next != null)
                        {
                            abo_next_season.InnerHtml += "<p>Je hebt al een ticket voor dit seizoen voor " + abo_next.club.name + ".";
                        }
                        else
                        {
                            abo_next_season.InnerHtml += "<p><a href='aboBuy.aspx'>Klik hier</a> om een abonnement te kopen voor jouw club.";
                        }
                    }
                    if (seizoen_current != null)
                    {
                        Abonnement abo_now = aboService.getAbonnement(login, seizoen_current);
                        abo_this_season.InnerHtml = "<h1>" + seizoen_current + "</h1>";
                        if (abo_now != null)
                            abo_this_season.InnerHtml += "<p>Je hebt een abonnement voor " + abo_now.club.name + ". Dit abonnement vervalt op " + string.Format("{0:dd-MM-yyyy}", seizoen_current.endDate.Date) + ".";
                        else
                            abo_this_season.InnerHtml += "<p>Je kan voor dit seizoen geen abonnement meer verkrijgen. Voor losse tickets, ga naar de <a href='calendar.aspx'>Kalender</a></p>";
                    }
                }
            }
        }
    }
}