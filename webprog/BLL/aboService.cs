using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webprog.DAO;
using webprog.Domain;

namespace webprog.BLL
{
    public class AboService
    {
        AbonnementDAO dao = new AbonnementDAO();

        public List<Abonnement> getAll()
        {
            return dao.getAll();
        }

        public List<Abonnement> getAllOfTeam(int id)
        {
            return dao.getAllOfTeam(id);
        }

        public List<Abonnement> getTicket_type(int id)
        {
            return new AbonnementDAO().getTicket_type(id);
        }

        public Abonnement getAllAbonnement(Club c, Login l)
        {
            return dao.getAllAbo(c, l);
        }

        public Abonnement getAbonnement(Club c, Login l, Seizoen s)
        {
            return dao.getAbo(c, l, s);
        }
        
    }
}