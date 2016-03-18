using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace webprog
{

    public class LoginDAO
    {
        private String dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public LoginDAO() { }

        public List<Login> getAll()
        {
            cnn = new SqlConnection(dbLoc);
            List<Login> retVal = new List<Login>();

            String strSQL = "SELECT * FROM login;";

            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateLogin(reader)); }

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

        public Login getLogin(String login)
        {
            cnn = new SqlConnection(dbLoc);
            Login retVal = null;

            String strSQL = "SELECT * FROM login where login=@login;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@login", login);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal = CreateLogin(reader); }
                
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

        public Login setLogin(String login, String hash)
        {
            cnn = new SqlConnection(dbLoc);

            String strSQL = "insert into login values (@login, @hash);";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@login", login);
            com.Parameters.AddWithValue("@hash", hash);

            cnn.Open();

            com.ExecuteReader().Close();
            
            cnn.Close();

            return new Login
            {
                login = login,
                password = hash
            };

            
        }

        private Login CreateLogin(SqlDataReader reader)
        {
            Login retVal = new Login
            {
                login = Convert.ToString(reader["login"]),
                password = Convert.ToString(reader["password"])
            };

            return retVal;
        }
    }

}
