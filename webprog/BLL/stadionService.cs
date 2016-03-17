using System;
using System.Collections.Generic;

namespace webprog
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
