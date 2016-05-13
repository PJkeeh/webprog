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
    public partial class Calendar : System.Web.UI.Page
    {
        MatchService matchService = new MatchService();
        List<Match> matches;

        protected void Page_Load(object sender, EventArgs e)
        {
            string club = Request.QueryString["club"];
            int selected;

            if (club == null || club.Equals("") || Int32.TryParse(club, out selected) == false)
            {
                matches = matchService.getAllMatches();
            }
            else
            {
                matches = matchService.getAllMatchesOfTeam(selected);
            }

            fillMatchesDiv(matches);
        }

        private void fillMatchesDiv(List<Match> matches)
        {
            if (matches.Count == 0)
            {
                matchesID.InnerHtml += "<p>Geen matches gepland.</p>";
            }
            else
            {
                SeizoenService seizoenService = new SeizoenService();
                List<Seizoen> seizoenen = new List<Seizoen>();

                if ( matches.Count < 10) { 

                     seizoenen = seizoenService.getAll();
                }
                else
                {
                    Seizoen s;
                    if (seizoenService.getCurrent() != null)
                        s = seizoenService.getCurrent();
                    else
                        s = seizoenService.getNext();
                    seizoenen.Add(s);
                }

                for (int s = 0; s < seizoenen.Count; s++)
                {
                    Boolean header = false; 

                    for (int i = 0; i < matches.Count; i++)
                    {
                        if (matches.ElementAt(i).date.Date >= seizoenen.ElementAt(s).startDate.Date
                        && matches.ElementAt(i).date.Date < seizoenen.ElementAt(s).endDate.Date)
                        {
                            if (!header)
                            {
                                matchesID.InnerHtml += "<h1>" + seizoenen.ElementAt(s).startDate.ToShortDateString() + " - " + seizoenen.ElementAt(s).endDate.ToShortDateString() + "</h1>";
                                header = true;
                            }
                            matchesID.InnerHtml += "<h2><a href=\"ticketView.aspx?match=" + matches.ElementAt(i).id + "\">" + matches.ElementAt(i).homeTeam.name + " - " + matches.ElementAt(i).awayTeam.name + "</a></h2>";
                            matchesID.InnerHtml += "<p>" + string.Format("{0:dd-MM-yyyy}", matches.ElementAt(i).date) + " </p>";
                        }
                    }
                }
            }
        }
    }
}