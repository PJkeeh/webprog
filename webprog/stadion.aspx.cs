using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webprog
{
    public partial class stadion : System.Web.UI.Page
    {
        StadionService s;

        protected void Page_Load(object sender, EventArgs e)
        {
            String id = Request.QueryString["stadion"];
            int stadion_id;

            if (id == null|| id == "" || Int32.TryParse(id, out stadion_id) == false)
            {
                stadion_id = 0;
            }

            s = new StadionService();
            Stadion stadion = s.getStadion(stadion_id);

            if (stadion == null)
                stadionSelected.InnerHtml = "<p>Stadion niet gevonden</p>";
            else { 
                stadionSelected.InnerHtml = "<h1>" + stadion.name + "</h1>";
                stadionSelected.InnerHtml += "<p>" + stadion.description + "</p>";
            }
        }
    }
}