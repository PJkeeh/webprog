using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace webprog
{

    public class StadionDAO
    {
        private String dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;

        public StadionDAO() { }

        public List<Stadion> getAll()
        {
            cnn = new SqlConnection(dbLoc);
            List<Stadion> retVal = new List<Stadion>();

            String strSQL = "SELECT * FROM stadion;";

            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                SqlDataReader reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                {
                    retVal.Add(CreateStadion(reader));
                }

                // Call close when done reading
                reader.Close();
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
        }

        public Stadion getStadion(int id)
        {
            cnn = new SqlConnection(dbLoc);
            Stadion retVal = new Stadion();

            String strSQL = "SELECT * FROM stadion where stadion_id = " + id + ";";
            
            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                SqlDataReader reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                {
                    retVal = CreateStadion(reader);
                }

                // Call close when done reading
                reader.Close();
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong ", ex);
            }
        }

        private Stadion CreateStadion(SqlDataReader reader)
        {
            Stadion retVal = new Stadion
            {
                id = Convert.ToInt32(reader["stadion_id"]),
                name = Convert.ToString(reader["stadion_name"]),
                description = Convert.ToString(reader["stadion_description"]),
            };

            return retVal;
        }
    }

}
