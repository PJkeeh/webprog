using System;
using System.Collections.Generic;
using webprog.Domain;
using webprog.DAO;

namespace webprog.BLL
{
    public class ClubService
    {

        private ClubDAO dao;
        public ClubService()
        {
            dao= new ClubDAO();
        }


        public List<Club> getClubs()
        {
            return dao.getAll();
        }

        public Club getClub(int id)
        {
            return dao.getClub(id);
        }
    }
}
