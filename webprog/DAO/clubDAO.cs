using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace webprog
{

    public class ClubDAO
    {
        private String dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;

        public ClubDAO() { }

        public List<Club> getAll()
        {
            cnn = new SqlConnection(dbLoc);
            List<Club> retVal = new List<Club>();

            String strSQL = "SELECT * FROM team;";

            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                SqlDataReader reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateClub(reader)); }

                // Call close when done reading
                reader.Close();
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
        }

        private Club CreateClub(SqlDataReader reader)
        {
            Club retVal = new Club
            {
                id = Convert.ToInt32(reader["team_id"]),
                name = Convert.ToString(reader["team_name"]),
                description = Convert.ToString(reader["team_description"]),
            };

            return retVal;
        }
    }

}
