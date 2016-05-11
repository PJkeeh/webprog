using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using webprog.Domain;

namespace webprog.DAO
{

    public class StadionDAO
    {
        private string dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;

        SqlDataReader reader;

        public StadionDAO() { }

        public List<Stadion> getAll()
        {
            cnn = new SqlConnection(dbLoc);
            List<Stadion> retVal = new List<Stadion>();

            string strSQL = "SELECT * FROM stadion;";

            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                {
                    retVal.Add(CreateStadion(reader));
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

        public Stadion getStadion(int id)
        {
            cnn = new SqlConnection(dbLoc);
            Stadion retVal = new Stadion();

            string strSQL = "SELECT * FROM stadion where stadion_id = " + id + ";";
            
            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                {
                    retVal = CreateStadion(reader);
                }

                // Call close when done reading
                return retVal;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString(), ex);
            }
            finally
            {
                reader.Close();
                cnn.Close();
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
