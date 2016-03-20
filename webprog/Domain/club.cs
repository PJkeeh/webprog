using System;

namespace webprog.Domain
{
    public class Club
    {
        public int id { get; set; }

        public String name { get; set; }

        public String description { get; set; }

        public Stadion stadion { get; set; }

        public override string ToString()
        {
            return id + " " + name;
        }
    }
}
