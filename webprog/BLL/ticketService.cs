using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webprog.DAO;
using webprog.Domain;

namespace webprog.BLL
{
    public class TicketService
    {
        TicketDAO dao = new TicketDAO();

        public List<Ticket> getAllOfMatch(int id)
        {
            return dao.getAllOfMatch(id);
        }

        public List<Ticket_type> getTicket_types()
        {
            return new Ticket_typeDAO().getAllTicket_types();
        }

        public float getTicket_TypePrice(Ticket_type ticket_type, Club c)
        {
            Ticket_team ttm = new Ticket_teamDAO().getTicket_team(ticket_type.id, c.id);
            if(ttm  != null)
            {
                return ttm.price;
            }
            else
            {
                return 0;
            }
        }

        public int getAmountOfTicket_types()
        {
            return getTicket_types().Count;
        }

        public Ticket_type getTicket_type(int id)
        {
            return new Ticket_typeDAO().getTicket_type(id);
        }

        public List<Ticket> getTicketsOfLoginOnMatch(string login, Match m)
        {
            return dao.getAllOfLoginFromMatch(login, m);
        }

        public List<Ticket> buyTickets(List<Ticket> t)
        {
            Login login = t[0].login;
            List<Ticket> before = dao.getAllOfLogin(login.login.Trim());
            dao.setTicket(t);
            List<Ticket> after = dao.getAllOfLogin(login.login.Trim());

            return after.Except(before).ToList();
            
        }
    }
}