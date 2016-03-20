using System;

namespace webprog.Domain
{
    public class Login
    {
        public String login { get; set; }

        public String password { get; set; }
        

        public override string ToString()
        {
            return login;
        }
    }
}
