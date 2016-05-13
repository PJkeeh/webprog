﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webprog.Domain;

namespace webprog.DAO
{
    public class AbonnementDAO
    {
        private string dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public AbonnementDAO() { }

        public List<Abonnement> getTicket_type(int type_id)
        {
            cnn = new SqlConnection(dbLoc);
            List<Abonnement> retVal = new List<Abonnement>();

            string strSQL = "SELECT * FROM abonnement where abo__ticket_type=@abo__ticket_type;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@abo__ticket_type", type_id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateAbonnement(reader)); }

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

        public List<Abonnement> getAllOfTeam(int team_id)
        {
            cnn = new SqlConnection(dbLoc);
            List<Abonnement> retVal = new List<Abonnement>();

            string strSQL = "SELECT * FROM abonnement where abo_team_id=@team_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@team_id", team_id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateAbonnement(reader)); }

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

        public Abonnement getAbo(Club c, Login l, Seizoen s)
        {
            cnn = new SqlConnection(dbLoc);
           Abonnement retVal = null;

            string strSQL = "SELECT * FROM abonnement where abo_team_id=@team_id and abo_login=@login and abo_seizoen=@seizoen_id;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@team_id", c.id);
            com.Parameters.AddWithValue("@login", l.login);
            com.Parameters.AddWithValue("@seizoen_id", s.id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal=CreateAbonnement(reader); }

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

        public Abonnement getAllAbo(Club c, Login l)
        {
            cnn = new SqlConnection(dbLoc);
            Abonnement retVal = null;

            string strSQL = "SELECT * FROM abonnement where abo_team_id=@team_id and abo_login=@login;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@team_id", c.id);
            com.Parameters.AddWithValue("@login", l.login);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal = CreateAbonnement(reader); }

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


        public List<Abonnement> getAll()
        {
            cnn = new SqlConnection(dbLoc);
            List<Abonnement> retVal = new List<Abonnement>();

            string strSQL = "SELECT * FROM abonnement;";

            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateAbonnement(reader)); }

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

        public List<Abonnement> getAllOfTeam(int team_id, DateTime seasonDate)
        {
            cnn = new SqlConnection(dbLoc);
            List<Abonnement> retVal = new List<Abonnement>();

            string strSQL = "SELECT * FROM abonnement where abo_team_id=@team_id and abo_start_date<=@seasonDate and abo_end_date>@seasonDate;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@team_id", team_id);
            com.Parameters.AddWithValue("@seasonDate", seasonDate.Date);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateAbonnement(reader)); }

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

        private Abonnement CreateAbonnement(SqlDataReader reader)
        {
            ClubDAO clubDAO = new ClubDAO();
            LoginDAO loginDAO = new LoginDAO();
            Ticket_typeDAO ticket_typeDAO = new Ticket_typeDAO();
            SeizoenDAO seizoenDAO = new SeizoenDAO();

            return new Abonnement
            {
                abo_id = Convert.ToInt32(reader["abo_id"]),
                club = clubDAO.getClub(Convert.ToInt32(reader["abo_team_id"])),
                login = loginDAO.getLogin(Convert.ToString(reader["abo_login"])),
                seizoen = seizoenDAO.getByID(Convert.ToInt32(reader["abo_seizoen"])),
                ticket_type = ticket_typeDAO.getTicket_type(Convert.ToInt32(reader["abo_ticket_type"])),
            };
        }
    }
}