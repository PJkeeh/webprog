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
        public List<Abonnement> getAllOfTeam(int id, Seizoen s)
        {
            return dao.getAllOfTeam(id, s);
        }

        public List<Abonnement> getAllOfTicketType(Club c, Seizoen s, Ticket_type tt)
        {
            return dao.getAllOfTicketType(c.id, s, tt);
        }

        public List<Abonnement> getAllOfTicketTeam(Seizoen s, Ticket_team ttm)
        {
            return dao.getAllOfTicketType(ttm.club.id, s, ttm.ticket_type);
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

        public Abonnement getAbonnement(Login l, Seizoen s)
        {
            return dao.getOfLoginInSeason(l, s);
        }

        public void buyAbonnement(Abonnement a)
        {
            dao.setAbonnement(a);
        }

        public List<Abonnement> getAllOfLogin(Login l)
        {
            return dao.getAllOfLogin(l);
        }
        
    }
}