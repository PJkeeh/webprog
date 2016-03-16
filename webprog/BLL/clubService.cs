using System;
using System.Collections.Generic;

namespace webprog
{
    public class ClubService
    {
        public ClubService()
        {
        }

        public List<Club> getClubs()
        {
            ClubDAO dao = new ClubDAO();
            return dao.getAll();
        }
    }
}
