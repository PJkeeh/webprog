using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using webprog.Domain;

namespace webprog.DAO
{
    public class Ticket_typeDAO
    {
        private String dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public Ticket_type getTicket_type(int id)
        {
            cnn = new SqlConnection(dbLoc);
            Ticket_type retVal = new Ticket_type();

            String strSQL = "SELECT * FROM ticket_type where ticket_type_id = @ticket_type_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@ticket_type_id", id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                reader.Read();

                retVal = CreateTicket_type(reader);

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
        public List<Ticket_type> getAllTicket_types()
        {
            cnn = new SqlConnection(dbLoc);
            List<Ticket_type> retVal = new List<Ticket_type>();

            String strSQL = "SELECT * FROM ticket_type;";

            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read()) { 
                    retVal.Add(CreateTicket_type(reader));
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

        private Ticket_type CreateTicket_type(SqlDataReader reader)
        {
            Ticket_type retVal = new Ticket_type
            {
                id = Convert.ToInt32(reader["ticket_type_id"]),
                name = Convert.ToString(reader["ticket_type_name"]),
                hometeam = Convert.ToBoolean(reader["ticket_type_hometeam"])
            };
            return retVal;
        }
    }
}