using System;
using System.Web.Security;

namespace webprog
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            LoginService loginService = new LoginService();

            if (txtLogin.Text.Trim().Equals(""))
            {
                lblLoginError.InnerHtml = "Geef een gebruikersnaam in.";
            }
            else {
                if (loginService.getLogin(txtLogin.Text) == null)
                {
                    lblLoginError.InnerHtml = "";
                    if (txtPassword.Text.Equals(txtPassword2.Text))
                    {
                        lblPasswordError.InnerHtml = "";
                        if (txtPassword.Text.Equals(""))
                        {
                            lblPasswordError.InnerHtml = "Geef een wachtwoord in.";
                        }
                        else
                        {
                            lblPasswordError.InnerHtml = "";

#pragma warning disable CS0618 // Type or member is obsolete
                            loginService.registerLogin(txtLogin.Text, FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1"));
#pragma warning restore CS0618 // Type or member is obsolete
                            Response.Redirect("index.aspx");
                        }
                    }
                    else
                    {
                        lblPasswordError.InnerHtml = "De wachtwoorden komen niet overeen.";
                    }
                }
                else
                {
                    lblLoginError.InnerHtml = "Deze gebruikersnaam is al in gebruik.";
                }
            }
        }
    }
}