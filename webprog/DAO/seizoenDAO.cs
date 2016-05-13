using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webprog.Domain;

namespace webprog.DAO
{
    public class SeizoenDAO
    {
        private string dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public List<Seizoen> getAll()
        {
            cnn = new SqlConnection(dbLoc);
            List<Seizoen> retVal = new List<Seizoen>();

            string strSQL = "SELECT * FROM seizoen;";

            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(createSeizoen(reader)); }

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

        public Seizoen getByID(int id)
        {
            cnn = new SqlConnection(dbLoc);
            Seizoen retVal = new Seizoen();

            string strSQL = "SELECT * FROM seizoen where seizoen_id = @seizoen_id";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@seizoen_id", id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal = createSeizoen(reader); }

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

        public Seizoen getByDate(DateTime date)
        {
            cnn = new SqlConnection(dbLoc);
            Seizoen retVal = new Seizoen();

            string strSQL = "SELECT * FROM seizoen where seizoen_start <= @date and seizoen_einde > @date;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@date", date.Date);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal=createSeizoen(reader); }

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

        private Seizoen createSeizoen(SqlDataReader reader)
        {
            Seizoen retVal = new Seizoen
            {
                id = Convert.ToInt32(reader["seizoen_id"]),
                endDate = Convert.ToDateTime(reader["seizoen_einde"]),
                startDate = Convert.ToDateTime(reader["seizoen_start"]),
            };

            retVal.updateToString();

            return retVal;
        }
    }
}