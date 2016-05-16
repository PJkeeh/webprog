using System;
using System.Collections.Generic;
using System.Linq;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class account : pageFunctions
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
                    SaleService ss = new SaleService();
                    details.InnerHtml = "<h1>" + login.name + "</h1>";

                    List<Sale> sales = ss.getSales(login);
                    if (sales != null && sales.Count != 0)
                    {
                        details.InnerHtml += "<h2>Gekocht:</h2>";

                        int count = 0;
                        DateTime date = new DateTime();
                        for (int i = 0; i < sales.Count; i++)
                        {
                            if(sales[i].saleDate.Date != date.Date)
                            {
                                date = sales[i].saleDate;
                                details.InnerHtml += "<h3>" + string.Format("{0:dd-MM-yyyy}", date) + "</h3>";
                            }
                            count++;
                            if (i + 1 != sales.Count && sales[i].ticket != null && sales[i+1].ticket != null && sales[i].ticket.match.id == sales[i + 1].ticket.match.id && sales[i].ticket.ticket_type.id == sales[i + 1].ticket.ticket_type.id)
                            {
                                //Do nothing
                            }
                            else if(sales[i].ticket != null)
                            {
                                details.InnerHtml += "<p>" + count + "x " + sales[i].ticket.match.ToString() + "</p>";
                                count = 0;
                            }

                            if (sales[i].abonnement != null)
                            {
                                details.InnerHtml += "<p>" + count + "x abonnement" + sales[i].abonnement.ToString() + "</p>";
                                count = 0;
                            }

                        }
                    }
                    else
                    {
                        details.InnerHtml += "Geen aankopen gevonden.";
                    }
                }
            }
        }
    }
}