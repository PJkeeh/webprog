using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webprog
{
    public class Match
    {
        public int id { get; set; }
        public Club homeTeam { get; set; }
        public Club awayTeam { get; set; }
        public DateTime date { get; set; }
    }
}