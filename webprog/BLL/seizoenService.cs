using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webprog.DAO;
using webprog.Domain;

namespace webprog.BLL
{
    public class SeizoenService
    {
        SeizoenDAO dao = new SeizoenDAO();

        public List<Seizoen> getAll()
        {
            return dao.getAll();
        }

        public Seizoen getCurrent()
        {
            Seizoen retVal = null;
            DateTime today = DateTime.Today;

            retVal = dao.getByDate(today);

            return retVal;
        }

        public Seizoen getNext()
        {
            Seizoen retVal = null;
            DateTime today = DateTime.Today;

            List<Seizoen> seizoenen = dao.getAll();

            for(int i = 0; i<seizoenen.Count; i++)
            {
                Seizoen s = seizoenen.ElementAt(i);

                if (s.startDate > today && s.endDate > today)
                {
                    retVal = s;
                    break;
                }
            }

            return retVal;
        }
    }
}