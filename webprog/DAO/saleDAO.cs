using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webprog.Domain;

namespace webprog.DAO
{
    public class SaleDAO
    {
        private string dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public SaleDAO() { }

        public List<Sale> getSale(Login login)
        {
            return getSale(login.login);
        }

        public List<Sale> getSale(String login)
        {
            cnn = new SqlConnection(dbLoc);
            List<Sale> retVal = new List<Sale>();

            string strSQL = "SELECT * FROM sale where login=@login order by datum DESC;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@login", login);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateSale(reader)); }

                // Call close when done reading
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
            finally
            {
                cnn.Close();
                reader.Close();
            }
        }

        public void setSale(Sale sale)
        {
            cnn = new SqlConnection(dbLoc);
            Abonnement retVal = new Abonnement();

            string strSQL = "INSERT INTO sale VALUES "
                + "(@abo_id, @ticket_id, @sale_date, @login)";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            if (sale.abonnement == null)
            {
                com.Parameters.AddWithValue("@abo_id", DBNull.Value);
            }
            else {
                com.Parameters.AddWithValue("@abo_id", sale.abonnement.abo_id);
            }
            if (sale.ticket == null)
            {
                com.Parameters.AddWithValue("@ticket_id", DBNull.Value);
            }
            else {
                com.Parameters.AddWithValue("@ticket_id", sale.ticket.id);
            }
            com.Parameters.AddWithValue("@sale_date", sale.saleDate);
            com.Parameters.AddWithValue("@login", sale.login.login);

            try
            {
                cnn.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
            finally
            {
                cnn.Close();
            }
        }

        private Sale CreateSale(SqlDataReader reader)
        {
            AbonnementDAO aboDAO = new AbonnementDAO();
            TicketDAO ticketDAO = new TicketDAO();
            LoginDAO loginDAO = new LoginDAO();

            Abonnement abo = null;
            Ticket ticket = null;
            if(!Convert.IsDBNull(reader["abo_id"]))
            {
                abo = aboDAO.getAbo(Convert.ToInt32(reader["abo_id"]));
            }
            else if (!Convert.IsDBNull(reader["ticket_id"]))
            {
                ticket = ticketDAO.get(Convert.ToInt32(reader["ticket_id"]));
            }
            return new Sale
            {
                sale_id = Convert.ToInt32(reader["sale_id"]),
                abonnement = abo,
                ticket = ticket,
                saleDate = Convert.ToDateTime(reader["datum"]),
                login = loginDAO.getLogin((String)reader["login"])
            };
        }
    }
}