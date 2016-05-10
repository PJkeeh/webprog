using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using webprog.Domain;
using webprog.DAO;

namespace webprog.BLL
{
    public class LoginService
    {
        public LoginService()
        {
        }

        public List<Login> getLogins()
        {
            LoginDAO dao = new LoginDAO();
            List<Login> logins = dao.getAll();

            logins = removePasswords(logins);

            return logins;
        }

        public Login getLogin(String login)
        {
            LoginDAO dao = new LoginDAO();
            Login logins = dao.getLogin(login);
            
            return logins;
        }

        public Boolean checkLogin(String login, String password)
        {
            Boolean retVal = false;
            string hash = hashPassword(password);

            if (getLogin(login) == null)
                retVal = false;
            else
                retVal = getLogin(login).password.Trim().Equals(hash.Trim());

            return retVal;
        }

        public Login registerLogin(String login, string password, String email)
        {
            return registerLogin(login, password, null, email);
        }

        public Login registerLogin(String login, String password, String naam, String email)
        {
            LoginDAO dao = new LoginDAO();
            string hash = hashPassword(password);
            return dao.setLogin(login, hash, naam, email);
        }

        private List<Login> removePasswords(List<Login> logins)
        {
            List<Login> retVal = new List<Login>();
            for(int i = 0; i<logins.Count; i++)
            {
                retVal.Add(new Login
                    {
                        login = logins.ElementAt(i).login,
                        password = ""
                    }
                );
            }
            return retVal;
        }

        private String hashPassword(String password)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
