using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class abo_buy : pageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                ClubService c = new ClubService();
                List<Club> teams = c.getClubs();

                if (!Page.IsPostBack)
                {
                    fillDDL(teams);
                }

                string club = Request.QueryString["club"];
                int selected = 0;

                if (teams.Count == 0)
                {
                    clubSelected.InnerHtml = "<p>Er zijn geen teams gevonden.</p>";
                }
                else
                {
                    if (club == null || club.Equals("") || Int32.TryParse(club, out selected) == false || selected >= teams.Count || selected < 0)
                    {
                        clubSelected.InnerHtml = "Gelieve een team te selecteren.";
                        //fillClubDiv(0);
                        //matches = m.getAllComingMatchesOfTeam(0);
                    }
                    else
                    {
                        clubSelected.InnerHtml = "";
                        Club selected_team = c.getClub(selected);
                        SeizoenService seizoenService = new SeizoenService();
                        Seizoen next_seizoen = seizoenService.getNext();

                        if (next_seizoen == null)
                        {
                            available_abo.InnerHtml = "<p>Er zijn geen abonnementen te koop voor het volgende seizoen, want het volgende seizoen is nog niet aangekondigd.";
                        }
                        else
                        {
                            if (DateTime.Today.AddMonths(1) >= next_seizoen.startDate)
                            { //Sluit de verkoop een maand voor het start van het seizoen en start de ticketverkoop
                                available_abo.InnerHtml = "<p>De verkoop van abonnementen is gesloten vanaf " + next_seizoen.startDate.AddMonths(-1).ToShortDateString();
                            }
                            else {
                                Login login = new LoginService().getLogin(getLogin());

                                AboService aboService = new AboService();
                                Abonnement abo = aboService.getAbonnement(login, next_seizoen);

                                if (abo != null || Session["abo_cart"] != null)
                                {
                                    available_abo.InnerHtml = "<p>Je hebt al een abonnement gekocht voor " + abo.club.name + " (" + abo.seizoen + ")";
                                }
                                else
                                {
                                    TicketService ticketService = new TicketService();

                                    List<Ticket_team> ttms = ticketService.getAllTicketTeam(selected_team);

                                    for (int i = 0; i < ttms.Count; i++)
                                    {
                                        Ticket_team ttm = ttms[i];

                                        int sold = aboService.getAllOfTicketTeam(next_seizoen, ttm).Count;
                                        if (ttm.ticket_type.hometeam)
                                        {
                                            if (sold < ttm.amount)
                                            {
                                                if (Request.QueryString["buy"] != null && Request.QueryString["buy"].Equals(ttm.ticket_type.id.ToString()))
                                                {
                                                    if (Session["abo_cart"] == null)
                                                    {
                                                        Session["abo_cart"] = new Abonnement
                                                        {
                                                            club = selected_team,
                                                            ticket_type = ttm.ticket_type,
                                                            login = login,
                                                            seizoen = next_seizoen,
                                                        };
                                                    }
                                                    Response.Redirect("shoppingcart.aspx");
                                                }
                                                else {
                                                    available_abo.InnerHtml += "<p><a href='?club="+selected_team.id+"&buy=" + ttm.ticket_type.id
                                                       + "' >" + ttm.ticket_type.name + " " + ttm.club.name + " €" + ttm.price *5f + " (" + sold + "/" + ttm.amount + ")</a></p>";
                                                }
                                            }
                                            else {
                                                if (!IsPostBack)
                                                    available_abo.InnerHtml += "<p>" + ttm.ticket_type.name + " " + ttm.club.name + " €" + ttm.price * 5f + " (Sold out)</p>";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void fillDDL(List<Club> t)
        {
            ddlClubs.DataSource = t;
            ddlClubs.DataTextField = "name";
            ddlClubs.DataValueField = "id";
            ddlClubs.DataBind();
        }

        protected void ddlClubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("club", ddlClubs.SelectedItem.Value);
            string url = Request.Url.AbsolutePath;
            string updatedQueryString = "?" + nameValues.ToString();
            Response.Redirect(url + updatedQueryString);
        }

    }
}