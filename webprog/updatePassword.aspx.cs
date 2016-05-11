using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webprog.BLL;

namespace webprog
{
    public partial class updatePassword : pageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            LoginService loginservice = new LoginService();

            string loginname = (string)Session["username"];

            if (loginservice.checkLogin(loginname, txtOldPass.Text))
            {
                lblOldPassError.InnerHtml = "";

                if (txtNewPassword.Text.Equals(txtNewPassword2.Text))
                {
                    lblNewPassError.InnerHtml = "";

                    loginservice.changePassword(loginname, txtNewPassword.Text);

                    Response.Redirect("index.aspx");
                }
                else
                {
                    lblNewPassError.InnerHtml = "De wachtwoorden komen niet overeen.";
                }
            }
            else
            {
                lblOldPassError.InnerHtml = "Het wachtwoord is onjuist.";
            }
        }
    }
}