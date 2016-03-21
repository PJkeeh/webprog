using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webprog.Domain
{
    public class Match
    {
        public int id { get; set; }
        public Club homeTeam { get; set; }
        public Club awayTeam { get; set; }
        public DateTime date { get; set; }

        public String toString()
        {
            return id.ToString() + " " + homeTeam.name + " - " + awayTeam.name + " " + date.ToString();
        }
    }
}