using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using webprog.BLL;
using System.Net.Mail;

namespace webprog
{
    public partial class recoverPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Form["loginname"] == null || Request.Form["email"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else { 
                LoginService loginService = new LoginService();

                string recoverhash = "";
                string login = Request.Form["loginname"];
                string email = Request.Form["email"];

                recoverhash = loginService.recoverPassword(login, email);

                if (recoverhash != null)
                {
                    send_mail(login, email, recoverhash);
                    Response.Redirect("login.aspx");
                }
            }
        }

        private void send_mail(string login, string email, string recoverHash)
        {
            string mail = null;

            mail += "<html><body>Beste, \n\n";

            mail += "U krijgt deze email omdat u een nieuw wachtwoord wil aanvragen. <a href='http://viveswebprog.azurewebsites.net/newPassword.aspx?hash=" + recoverHash+"&login="+login+"'>Klik hier</a> om dit te doen. \nIndien u deze mail niet heeft aangevraagd, kunt u deze negeren.";

            mail += "Gelieve op deze mail niet te antwoorden."
                + "\n\nHet VoetbalTickets team.</body></html>";

            MailMessage o = new MailMessage("VoetbalTicketsVives@hotmail.com", email, "Wachtwoord veranderen", mail);
            o.IsBodyHtml = true;
            o.Body = mail;
            NetworkCredential netCred = new NetworkCredential("VoetbalTicketsVives@hotmail.com", "Webprogrammeren");
            SmtpClient smtpobj = new SmtpClient("smtp.live.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);
            //ADD CATCH;
        }
    }
}