using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webprog.Domain
{
    public class Seizoen
    {
        public int id { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
    }
}