using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webprog.Domain
{
    public class Sale
    {
        public int sale_id { get; set; }

        public Abonnement abonnement { get; set; }

        public Ticket ticket { get; set; }

        public DateTime saleDate { get; set; }

        public Login login { get; set; }
    }
}