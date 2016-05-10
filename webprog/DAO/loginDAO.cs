using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using webprog.Domain;

namespace webprog.DAO
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

        public Login setLogin(String login, String hash, String email)
        {
            //Should not have to be used
            return setLogin(login, hash, null , email);
        }

        public Login setLogin(String login, String hash, String naam, String email)
        {
            cnn = new SqlConnection(dbLoc);

            String strSQL = "insert into login values (@login, @hash,@naam,@email);";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@login", login);
            com.Parameters.AddWithValue("@hash", hash);

            if (naam == null)
                com.Parameters.AddWithValue("@naam", DBNull.Value);
            else
                com.Parameters.AddWithValue("@naam", naam);

            com.Parameters.AddWithValue("@email", email);

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
            //Cleanup This code
            String name = null;
            if(reader["name"] != null)
            {
                name = Convert.ToString(reader["name"]);
            }
            Login retVal = new Login
            {
                login = Convert.ToString(reader["login"]),
                password = Convert.ToString(reader["password"]),
                name = name,
                email = Convert.ToString(reader["e-mail"])
            };

            return retVal;
        }
    }

}
