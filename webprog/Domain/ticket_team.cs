using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webprog.Domain
{
    public class Ticket_team
    {
        public Ticket_type ticket_type { get; set; }

        public Club club { get; set; }

        public float price { get; set; }

        public int amount { get; set; }
    }
}