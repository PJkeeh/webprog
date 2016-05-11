using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using webprog.BLL;
using webprog.Domain;

namespace webprog
{
    public partial class newPassword : System.Web.UI.Page
    {
        Login login = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean w = false;
            if(Request.Params["hash"] == null || Request.Params["login"] == null)
            {
            }
            else
            {
                LoginService loginservice = new LoginService();

                string strLogin = (string)Request.Params["login"];
                string strHash = (string)Request.Params["hash"];
                login = loginservice.getLogin(strLogin);

                if (login != null) { 
                    string recoverHash = loginservice.recoverPassword(login);
                    if (recoverHash.Equals(strHash)){
                        w = true;
                    }
                }
            }

            if (w)
            {
                notWorking.InnerHtml = "";
            }
            else
            {
                working.InnerHtml = "";
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if(login != null)
            {
                if (txtNewPassword.Text.Equals(txtNewPassword2.Text)) { 
                    LoginService loginservice = new LoginService();

                    loginservice.changePassword(login.login, txtNewPassword.Text);
                    Response.Redirect("login.aspx");
                } else
                {
                    lblNewPassError.InnerHtml = "De passwoorden komen niet overeen.";
                }
            }
        }
    }
}