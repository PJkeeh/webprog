using System;
using System.Collections.Generic;
using webprog.Domain;
using webprog.DAO;

namespace webprog.BLL
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
