using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webprog
{
    public partial class clubs : System.Web.UI.Page
    {
        private ClubService c;
        private List<Club> teams;

        protected void Page_Load(object sender, EventArgs e)
        {
            c = new ClubService();
            teams = c.getClubs();

            String club = Request.QueryString["club"];
            int selected = 0;

            if (teams.Count == 0)
            {
                clubSelected.InnerHtml = "<p>Er zijn geen teams gevonden.</p>";
            }
            else
            {
                if (club == null || club == "" || Int32.TryParse(club, out selected) == false || selected >= teams.Count)
                {
                    clubSelected.InnerHtml = "<h1>" + teams.ElementAt(0).name + "</h1>";
                    clubSelected.InnerHtml += "<p>" + teams.ElementAt(0).description + "</p>";
                    clubSelected.InnerHtml += "<p>Meer informatie over het <a href='stadion.aspx?stadion=" + teams.ElementAt(0).stadion.id + "'>" + teams.ElementAt(0).stadion.name + ".</a>";
                }
                else
                {
                    clubSelected.InnerHtml = "<h1>" + teams.ElementAt(selected).name + "</h1>";
                    clubSelected.InnerHtml += "<p>" + teams.ElementAt(selected).description + "</p>";
                    clubSelected.InnerHtml += "<p>Meer informatie over het <a href='stadion.aspx?stadion=" + teams.ElementAt(selected).stadion.id + "'>" + teams.ElementAt(selected).stadion.name + ".</a>";
                }

                if (!Page.IsPostBack)
                {
                    fillDDL(teams);
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