using System;
using System.Collections.Generic;
using webprog.Domain;
using webprog.DAO;

namespace webprog.BLL
{
    public class StadionService
    {
        public StadionService()
        {
        }

        public Stadion getStadion(int id)
        {
            StadionDAO dao = new StadionDAO();
            return dao.getStadion(id);
        }
    }
}
