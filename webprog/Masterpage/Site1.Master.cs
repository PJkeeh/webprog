using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webprog
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginService loginService = new LoginService();

            if (Session["username"] == null || ((String)Session["username"]).Equals("") || loginService.getLogin((String)Session["username"]) == null)
            {
                if (!Page.IsPostBack)
                {
                    login.InnerHtml ="<li><a id = \"register\" href = \"" + Page.ResolveUrl("~/register.aspx") + "\" > Register </a></li>"
                                   + "<li><a id=\"login\" href=\"" + Page.ResolveUrl("~/login.aspx") + "\">Login</a></li>";
                }
            }
        }
    }
}