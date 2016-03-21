using System;
using webprog.BLL;

namespace webprog
{
    public class masterpageFunctions : System.Web.UI.MasterPage
    {
        public Boolean loggedIn()
        {
            LoginService loginService = new LoginService();
            return !(Session["username"] != null
                && !((string)Session["username"]).Equals("")
                && loginService.getLogin((string)Session["username"]) != null);
        }
    }
}