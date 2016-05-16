using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webprog.Domain
{
    public class Abonnement
    {
        public int abo_id { get; set; }

        public Club club { get; set; }

        public Login login { get; set; }

        public Seizoen seizoen { get; set; }

        public Ticket_type ticket_type { get; set; }

        public override string ToString()
        {

            return "("+seizoen.ToString() + ") - " + club.name + " - " + ticket_type.name;
        }

    }
}