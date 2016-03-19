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
        SqlDataReader reader;

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
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateClub(reader)); }

                // Call close when done reading
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
            finally{
                reader.Close();
                cnn.Close();
            }
        }

        public Club getClub(int id)
        {
            cnn = new SqlConnection(dbLoc);
            Club retVal = new Club();

            String strSQL = "SELECT * FROM team where team_id = @team_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@team_id", id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal=CreateClub(reader); }

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

        private Club CreateClub(SqlDataReader reader)
        {
            Club retVal = new Club
            {
                id = Convert.ToInt32(reader["team_id"]),
                name = Convert.ToString(reader["team_name"]),
                description = Convert.ToString(reader["team_description"]),
                stadion = setStadion(Convert.ToInt32(reader["team_stadion_id"]))
            };

            return retVal;
        }

        private Stadion setStadion(int id)
        {
            StadionDAO stadionDAO = new StadionDAO();
            return stadionDAO.getStadion(id);
        }
    }

}
