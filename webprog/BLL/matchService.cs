﻿using System;
using System.Collections.Generic;

namespace webprog
{
    public class MatchService
    {
        MatchDAO dao = new MatchDAO();

        public MatchService()
        {
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