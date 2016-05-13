using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webprog.Domain;

namespace webprog.DAO
{
    public class Ticket_teamDAO
    {
        private string dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public List<Ticket_team> getAllOfTeam(int team_id)
        {
            cnn = new SqlConnection(dbLoc);
            List<Ticket_team> retVal = new List<Ticket_team>();

            string strSQL = "SELECT * FROM ticket_team where team_id = @team_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@team_id", team_id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                {
                    retVal.Add(CreateTicket_team(reader));
                }
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

        public Ticket_team getTicket_team(int ticket_type_id, int team_id)
        {
            cnn = new SqlConnection(dbLoc);
            Ticket_team retVal = new Ticket_team();

            string strSQL = "SELECT * FROM ticket_team where team_id = @team_id and ticket_type_id = @tt_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@team_id", team_id);
            com.Parameters.AddWithValue("@tt_id", ticket_type_id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                {
                    retVal=CreateTicket_team(reader);
                    break;
                }
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

        private Ticket_team CreateTicket_team(SqlDataReader reader)
        {
            return new Ticket_team
            {
                ticket_type = new Ticket_typeDAO().getTicket_type(Convert.ToInt32(reader["ticket_type_id"])),
                club = new ClubDAO().getClub(Convert.ToInt32(reader["team_id"])),
                amount = Convert.ToInt32(reader["ticket_amount"]),
                price = Convert.ToSingle(reader["ticket_price"])
            };
        }
    }
}