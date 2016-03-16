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
        private ClubService clubService = new ClubService();
        String[] teams = { "Club Brugge", "AA Gent", "Anderlecht", "Oostende", "Racing Genk", "Zulte Waregem" };

        protected void Page_Load(object sender, EventArgs e)
        {
            String club = Request.QueryString["club"];
            int selected = 0;

            if(club == null || club == "" || Int32.TryParse(club, out selected) == false || selected >=teams.Length)
            {
                clubSelected.InnerHtml = teams[0];
            }
            else
            {
                clubSelected.InnerHtml = teams[selected];
            }
        }
    }
}