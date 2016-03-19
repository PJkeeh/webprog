using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webprog
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String login = txtLogin.Text;
            String password = txtPassword.Text;

            if(login.Equals(""))
            {
                lblError.InnerHtml = "<p>Geef een gebruikersnaam en wachtwoord in.</p>";
            }
            else if(!passwordCorrect(login, password))
            {
                //lblError.InnerHtml = "<p>De gebruikersnaam en het wachtwoord komen niet overeen.</p>";
            }
            else
            {
                Session["username"] = login;

                //TODO change into previous page
                Response.Redirect("index.aspx");
            }
        }

        private Boolean passwordCorrect(String login, String password)
        {
            Boolean retVal = false;
            LoginService loginService = new LoginService();

            lblError.InnerHtml = loginService.getLogin(login).password;
            retVal = loginService.checkLogin(login, password);

            return retVal;
        }
    }
}