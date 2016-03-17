using System;

namespace webprog
{
    public class Stadion
    {
        public int id { get; set; }

        public String name { get; set; }

        public String description { get; set; }

        public override string ToString()
        {
            return id + " " + name;
        }
    }
}
