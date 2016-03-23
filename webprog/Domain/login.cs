using System;

namespace webprog.Domain
{
    public class Login
    {
        public String login { get; set; }

        public String password { get; set; }

        public String name { get; set; }

        public String email { get; set; }
        

        public override string ToString()
        {
            return login;
        }
    }
}
