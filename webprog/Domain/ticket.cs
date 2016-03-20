using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webprog.Domain
{
    public class Ticket
    {
        public int id { get; set; }

        public Ticket_type ticket_type { get; set; }

        public Match match { get; set; }

        public Login login { get; set; }
    }
}