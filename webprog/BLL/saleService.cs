using System;
using System.Collections.Generic;
using System.Linq;
using webprog.DAO;
using webprog.Domain;

namespace webprog.BLL
{
    public class SaleService
    {
        SaleDAO dao = new SaleDAO();

        public List<Sale> getSales(Login login)
        {
           return dao.getSale(login);
        }

        public void registerSale(Sale s)
        {
            dao.setSale(s);
        }
        public void registerSales(List<Sale> sales)
        {
            for (int i = 0; i < sales.Count; i++)
            {
                registerSale(sales[i]);
            }
        }
        public void registerSales(List<Ticket> tickets)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                registerSale(new Sale
                {
                    ticket = tickets[i],
                    abonnement = null,
                    saleDate = DateTime.Today,
                    login = tickets[0].login
                });
            }
        }
        public void registerSale(Abonnement a)
        {
            registerSale(new Sale
            {
                abonnement = a,
                ticket = null,
                saleDate = DateTime.Today,
                login = a.login
            });
        }
    }
}