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

        public override string ToString()
        {
            return homeTeam.name + " - " + awayTeam.name + " " + string.Format("{0:dd-MM-yyyy}", date.Date);
        }
    }
}