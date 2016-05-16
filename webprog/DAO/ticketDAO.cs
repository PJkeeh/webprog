using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webprog.Domain;

namespace webprog.DAO
{
    public class TicketDAO
    {
        private string dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public Ticket get(int id)
        {
            cnn = new SqlConnection(dbLoc);
            Ticket retVal = null;

            string strSQL = "SELECT * FROM ticket where ticket_id = @ticket_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@ticket_id", id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal=CreateTicket(reader); }

                // Call close when done reading
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
            finally
            {
                reader.Close();
                cnn.Close();
            }
        }

        public List<Ticket> getAllOfMatch(Match m)
        {
            return getAllOfMatch(m.id);
        }
        public List<Ticket> getAllOfMatch(int match_id)
        {
            cnn = new SqlConnection(dbLoc);
            List<Ticket> retVal = new List<Ticket>();

            string strSQL = "SELECT * FROM ticket where match_id = @match_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@match_id", match_id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateTicket(reader)); }

                // Call close when done reading
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
            finally
            {
                reader.Close();
                cnn.Close();
            }
        }

        public List<Ticket> getAllOfLogin(string username)
        {
            cnn = new SqlConnection(dbLoc);
            List<Ticket> retVal = new List<Ticket>();

            string strSQL = "SELECT * FROM ticket where login = @login;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@login", username.Trim());

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateTicket(reader)); }

                // Call close when done reading
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
            finally
            {
                reader.Close();
                cnn.Close();
            }
        }

        public List<Ticket> getAllOfLoginOnDate(string username, DateTime date)
        {
            cnn = new SqlConnection(dbLoc);
            List<Ticket> retVal = new List<Ticket>();

            string strSQL = "select* from ticket inner join match on ticket.match_id=match.match_id where login=@login and match_date = @date;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@login", username);
            com.Parameters.AddWithValue("@date", date.Date);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateTicket(reader)); }

                // Call close when done reading
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
            finally
            {
                reader.Close();
                cnn.Close();
            }
        }

        public List<Ticket> getAllOfLoginFromMatch(string username, Match m)
        {
            return getAllOfLoginFromMatch(username, m.id);
        }
        public List<Ticket> getAllOfLoginFromMatch(string username, int match_id)
        {
            cnn = new SqlConnection(dbLoc);
            List<Ticket> retVal = new List<Ticket>();

            string strSQL = "SELECT * FROM ticket where login = @login and match_id = @match_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@login", username);
            com.Parameters.AddWithValue("@match_id", match_id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateTicket(reader)); }

                // Call close when done reading
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
            finally
            {
                reader.Close();
                cnn.Close();
            }
        }

        public void setTicket(Ticket t)
        {
            cnn = new SqlConnection(dbLoc);
            Ticket retVal = new Ticket();

            string strSQL = "INSERT INTO ticket VALUES (@ticket_type, @match_id, @login)";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@ticket_type", t.ticket_type.id);
            com.Parameters.AddWithValue("@match_id", t.match.id);
            com.Parameters.AddWithValue("@login", t.login.login.Trim());

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
                reader.Close();
                cnn.Close();
            }
        }

        public void setTicket(List<Ticket> t)
        {
            for (int i = 0; i < t.Count; i++)
            {
                setTicket(t[i]);
            }
        }

        private Ticket CreateTicket(SqlDataReader reader)
        {
            Ticket retVal = new Ticket
            {
                id = Convert.ToInt32(reader["ticket_id"]),
                login = new LoginDAO().getLogin(Convert.ToString(reader["login"])),
                match = new MatchDAO().getMatch(Convert.ToInt32(reader["match_id"])),
                ticket_type = new Ticket_typeDAO().getTicket_type(Convert.ToInt32(reader["ticket_type"])),
            };
            return retVal;
        }

    }
}