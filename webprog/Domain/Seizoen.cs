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

        public override string ToString()
        {

            return string.Format("{0:dd-MM-yyyy}", startDate.Date)+ " - " + string.Format("{0:dd-MM-yyyy}", endDate.Date);
        }

        public String toString { get; set; } //Used in DropDownList

        public void updateToString()
        {
            toString = ToString();
        }
    }
}