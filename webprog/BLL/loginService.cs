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

        public Login getLogin(string login)
        {
            LoginDAO dao = new LoginDAO();
            Login logins = dao.getLogin(login);
            
            return logins;
        }

        public Boolean checkLogin(string login, string password)
        {
            Boolean retVal = false;
            string hash = hashPassword(password);

            if (getLogin(login) == null)
                retVal = false;
            else
                retVal = getLogin(login).password.Trim().Equals(hash.Trim());

            return retVal;
        }

        public Login registerLogin(string login, string password, string email)
        {
            return registerLogin(login, password, null, email);
        }
        public Login registerLogin(string login, string password, string naam, string email)
        {
            LoginDAO dao = new LoginDAO();
            string hash = hashPassword(password);
            return dao.setLogin(login, hash, naam, email);
        }

        public string recoverPassword(Login login)
        {
            return recoverPassword(login.login.Trim(), login.email.Trim());
        }
        public string recoverPassword(string login, string email)
        {
            string retVal = null;

            LoginDAO dao = new LoginDAO();

            Login l = dao.getLogin(login);

            login.Trim();
            email.Trim();

            if (l != null && l.login.Trim().ToLower().Equals(login.ToLower()) && l.email.Trim().ToLower().Equals(email.ToLower()))
            {
                retVal = getRecoverHash(login, email, dao.getHash(login));
            }

            return retVal;
        }

        public void changePassword(string login, string password)
        {
            LoginDAO dao = new LoginDAO();
            string hash = hashPassword(password);
            dao.changePassword(login, hash);
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

        private string hashPassword(string password)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
#pragma warning restore CS0618 // Type or member is obsolete
        }

        private string getRecoverHash(string login, string email, string hash)
        {
            return hashPassword(login + email + hash);
        }
    }
}
