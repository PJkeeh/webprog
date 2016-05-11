﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webprog.Domain;
using webprog.BLL;

namespace webprog
{
    public partial class clubs : System.Web.UI.Page
    {
        private ClubService c;
        private MatchService m;
        private List<Club> teams;
        private List<Match> matches;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                }

                if (!Page.IsPostBack)
                {
                    fillDDL(teams);
                }
            }
        }

        private void fillClubDiv(int id)
        {
            clubSelected.InnerHtml = "<h1>" + teams.ElementAt(id).name + "</h1>";
            clubSelected.InnerHtml += "<p>" + teams.ElementAt(id).description + "</p>";
            clubSelected.InnerHtml += "<p>Meer informatie over het <a href='stadion.aspx?stadion=" + teams.ElementAt(id).stadion.id + "'>" + teams.ElementAt(id).stadion.name + ".</a>";

            matchesID.InnerHtml = "<h1><a href=" + Page.ResolveUrl("~/Calendar.aspx?club=" + teams.ElementAt(id).id) + ">Matches</a></h1>";
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