using System;

namespace webprog.Domain
{
    public class Login
    {
        public string login { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public string email { get; set; }
        

        public override string ToString()
        {
            return login;
        }
    }
}
