using System;
using System.Collections.Generic;
using webprog.Domain;
using webprog.DAO;

namespace webprog.BLL
{
    public class MatchService
    {
        MatchDAO dao = new MatchDAO();

        public MatchService()
        {
        }

        public List<Match> getAllComing()
        {
            return dao.getAllComing();
        }

        public List<Match> getAllComingMatchesOfTeam(int id)
        {
            return dao.getAllComingOfTeam(id);
        }

        public List<Match> getAllMatches()
        {
            return dao.getAll();
        }

        public List<Match> getAllMatchesOfTeam(int id)
        {
            return dao.getAllOfTeam(id);
        }
    }
}
