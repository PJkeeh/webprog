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

        public int getAmountOfTicket_types()
        {
            return getTicket_types().Count;
        }

        public Ticket_type getTicket_type(int id)
        {
            return new Ticket_typeDAO().getTicket_type(id);
        }

        public List<Ticket> getTicketsOfLoginOnMatch(String login, Match m)
        {
            return dao.getAllOfLoginFromMatch(login, m);
        }

        public void buyTickets(List<Ticket> t)
        {
            dao.setTicket(t);
        }
    }
}