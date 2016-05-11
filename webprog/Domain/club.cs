using System;

namespace webprog.Domain
{
    public class Club
    {
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public Stadion stadion { get; set; }

        public override string ToString()
        {
            return id + " " + name;
        }
    }
}
