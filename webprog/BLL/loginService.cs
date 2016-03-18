using System;
using System.Collections.Generic;
using System.Linq;

namespace webprog
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

        public Login registerLogin(String login, String password)
        {
            LoginDAO dao = new LoginDAO();

            return dao.setLogin(login, password);
            
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
    }
}
