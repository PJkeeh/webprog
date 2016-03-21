using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webprog.BLL;

namespace webprog
{
    public partial class Site1 :masterpageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginService loginService = new LoginService();

            if (loggedIn())
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