using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class Calendar : pageFunctions
    {
        MatchService matchService = new MatchService();
        List<Match> matches;

        List<Club> teams;
        List<Seizoen> seizoenen;

        int selected;
        int seizoenselected;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClubService clubservice = new ClubService();

            teams = clubservice.getClubs();

            string club = Request.QueryString["club"];
            string seizoen = Request.QueryString["seizoen"];
            selected = -1;

            if (club == null || club.Equals("") || Int32.TryParse(club, out selected) == false)
            {
                matches = matchService.getAllMatches();
            }
            else
            {
                matches = matchService.getAllMatchesOfTeam(selected);
                fillAboDiv(selected);
            }

            seizoenselected = -1;

            SeizoenService seizoenservice = new SeizoenService();
            if (seizoen == null || seizoen.Equals("") || Int32.TryParse(seizoen, out seizoenselected) == false)
            {
                Seizoen s = seizoenservice.getCurrent();
                if (s == null)
                {
                    s = seizoenservice.getNext();
                }
                if(s == null)
                {
                    seizoenen = seizoenservice.getAll();
                }
                else
                {
                    seizoenen = new List<Seizoen>();
                    seizoenen.Add(s);
                }
            }
            else
            {
                Seizoen s = seizoenservice.get(seizoenselected);
                if(s != null)
                {
                    seizoenen = new List<Seizoen>();
                    seizoenen.Add(s);
                }
                else
                {
                    seizoenen = seizoenservice.getAll();
                }
            }

            fillMatchesDiv(matches, seizoenen);
            if (!IsPostBack) { 
                fillClubDDL(teams);
                fillCalendarDDL(seizoenservice.getAll());
            }
        }

        private void fillMatchesDiv(List<Match> matches, List<Seizoen> seizoenen)
        {
            if (matches.Count == 0)
            {
                matchesID.InnerHtml += "<p>Geen matches gepland.</p>";
            }
            else
            {
                SeizoenService seizoenService = new SeizoenService();

                if ( matches.Count < 10 && seizoenselected == -1) { 

                     seizoenen = seizoenService.getAll();
                }

                for (int s = 0; s < seizoenen.Count; s++)
                {
                    Boolean header = false;
                    matchesID.InnerHtml += "<h1>" + seizoenen.ElementAt(s).startDate.ToShortDateString() + " - " + seizoenen.ElementAt(s).endDate.ToShortDateString() + "</h1>";

                    for (int i = 0; i < matches.Count; i++)
                    {
                        if (matches.ElementAt(i).date.Date >= seizoenen.ElementAt(s).startDate.Date
                        && matches.ElementAt(i).date.Date < seizoenen.ElementAt(s).endDate.Date)
                        {
                            if (!header)
                            {
                                header = true;
                            }
                            matchesID.InnerHtml += "<h2><a href=\"ticketView.aspx?match=" + matches.ElementAt(i).id + "\">" + matches.ElementAt(i).homeTeam.name + " - " + matches.ElementAt(i).awayTeam.name + "</a></h2>";
                            matchesID.InnerHtml += "<p>" + string.Format("{0:dd-MM-yyyy}", matches.ElementAt(i).date) + " </p>";
                        }
                    }
                    if (!header)
                        matchesID.InnerHtml += "<p>Geen matches gevonden in dit seizoen.";
                }
            }
        }

        private void fillAboDiv(int club)
        {
            LoginService loginservice = new LoginService();
            SeizoenService seizoenservice = new SeizoenService();
            Club team = new ClubService().getClub(club);
            Login login = null;

            if (loggedIn()) {
                login = loginservice.getLogin(getLogin());
            }

            Seizoen seizoen = seizoenservice.getCurrent();

            if (seizoen == null)
            {
                seizoen = seizoenservice.getNext();
            }
            if (login != null && seizoen != null)
            {
                AboService aboservice = new AboService();

                Abonnement abo = aboservice.getAbonnement(team, login, seizoen);

                if (abo == null)
                {
                    abonnement.InnerHtml = "Je hebt geen abonnement voor dit team.";
                }
                else
                {
                    abonnement.InnerHtml = "Je hebt een abonnement voor het seizoen van " + abo.seizoen;
                }
            }
        }

        private void fillClubDDL(List<Club> t)
        {
            if (t != null) { 
                ddlClubs.DataSource = t;
                ddlClubs.DataTextField = "name";
                ddlClubs.DataValueField = "id";
                ddlClubs.DataBind();
                if(selected>=0 && selected < t.Count)
                {
                    ddlClubs.SelectedValue=selected.ToString();
                }
            }
        }
        private void fillCalendarDDL(List<Seizoen> t)
        {
            if(t != null) { 
                ddlSeizoen.DataSource = t;
                ddlSeizoen.DataTextField = "toString";
                ddlSeizoen.DataValueField = "id";
                ddlSeizoen.DataBind();
                if (seizoenselected >= 0 && seizoenselected < t.Count)
                {
                    ddlSeizoen.SelectedValue = seizoenselected.ToString();
                }
            }
        }

        protected void ddlClubs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("club", ddlClubs.SelectedItem.Value);
            string url = Request.Url.AbsolutePath;
            string updatedQueryString = "?" + nameValues.ToString();
            Response.Redirect(url + updatedQueryString);
        }
        protected void ddlseizoen_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("seizoen", ddlSeizoen.SelectedItem.Value);
            string url = Request.Url.AbsolutePath;
            string updatedQueryString = "?" + nameValues.ToString();
            Response.Redirect(url + updatedQueryString);
        }

    }
}