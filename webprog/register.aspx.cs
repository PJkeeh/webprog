using System;
using System.Text.RegularExpressions;
using System.Web.Security;
using webprog.BLL;

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

            lblEmailError.InnerText = "";
            lblLoginError.InnerText = "";
            lblNaamError.InnerText = "";
            lblPasswordError.InnerText = "";

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

                            string email = txtEmail.Text;
                            Regex regex = new Regex(@"\w[\w\.]*@\w+\.\w+");
                            Match match = regex.Match(email);
                            if (!match.Success)
                            {
                                lblEmailError.InnerHtml = "Geef een geldig emailadres op.";
                            }
                            else {
                                lblEmailError.InnerHtml = "";

                                if (txtNaam.Text == null || txtNaam.Text.Trim() == "")
                                {
                                    loginService.registerLogin(txtLogin.Text, txtPassword.Text, email);
                                }
                                else
                                {
                                    loginService.registerLogin(txtLogin.Text, txtPassword.Text, txtNaam.Text, email);
                                }
                                Response.Redirect("index.aspx");
                            }
                        }
                    }
                    else
                    {
                        lblPasswordError.InnerHtml = "De wachtwoorden komen niet overeen.";
                    }
                }
                else
                    lblLoginError.InnerHtml = "Deze gebruikersnaam is al in gebruik.";
            }
        }
    }
}