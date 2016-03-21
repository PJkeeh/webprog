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
        private String dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public List<Ticket_team> getAllOfTeam(int team_id)
        {
            cnn = new SqlConnection(dbLoc);
            List<Ticket_team> retVal = new List<Ticket_team>();

            String strSQL = "SELECT * FROM ticket_team where team_id = @team_id;";

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