using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using webprog.Domain;
using webprog.BLL;

namespace webprog
{
    public partial class clubs : pageFunctions
    {
        private ClubService c;
        private MatchService m;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Club> teams;
            List<Match> matches;
            c = new ClubService();
            m = new MatchService();
            teams = c.getClubs();

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
                    fillClubDiv(selected);
                    matches = m.getAllComingMatchesOfTeam(selected);

                    fillMatchesDiv(matches);
                    if (loggedIn())
                        fillAboDiv(selected);
                }

                if (!Page.IsPostBack)
                {
                    fillDDL(teams);
                }
            }
        }

        private void fillClubDiv(int id)
        {
            Club team = c.getClub(id);
            clubSelected.InnerHtml = "<h1>" + team.name + "</h1>";
            clubSelected.InnerHtml += "<p>" + team.description + "</p>";
            clubSelected.InnerHtml += "<p>Meer informatie over het <a href='stadion.aspx?stadion=" + team.stadion.id + "'>" + team.stadion.name + ".</a>";

            matchesID.InnerHtml = "<h1><a href=" + Page.ResolveUrl("~/Calendar.aspx?club=" + team.id) + ">Matches</a></h1>";
        }

        private void fillAboDiv(int club)
        {
            LoginService loginservice = new LoginService();
            SeizoenService seizoenservice = new SeizoenService();
            Club team = c.getClub(club);

            Login login = loginservice.getLogin(getLogin());
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

        private void fillMatchesDiv(List<Match> matches)
        {
            if (matches.Count == 0)
            {
                matchesID.InnerHtml += "<p>Geen matches gepland.</p>";
            }
            else
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    if (i == 5)
                        break;
                    matchesID.InnerHtml += "<h2><a href=\"ticketView.aspx?match=" + matches.ElementAt(i).id + "\">" + matches.ElementAt(i).homeTeam.name + " - " + matches.ElementAt(i).awayTeam.name + "</a></h2>";
                    matchesID.InnerHtml += "<p>" + string.Format("{0:dd-MM-yyyy}", matches.ElementAt(i).date) + "</p>";
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