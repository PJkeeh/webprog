﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace webprog
{

    public class MatchDAO
    {
        private String dbLoc = ConfigurationManager.ConnectionStrings["webprog"].ConnectionString;
        private SqlConnection cnn;
        SqlDataReader reader;

        public MatchDAO() { }

        public List<Match> getAll()
        {
            cnn = new SqlConnection(dbLoc);
            List<Match> retVal = new List<Match>();

            String strSQL = "SELECT * FROM match;";

            SqlCommand com = new SqlCommand(strSQL, cnn);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateMatch(reader)); }

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

        public List<Match> getAllOfTeam(int id)
        {
            cnn = new SqlConnection(dbLoc);
            List<Match> retVal = new List<Match>();

            String strSQL = "SELECT * FROM match where match_hometeam_id = @team OR match_awayteam_id = @team;";

            SqlCommand com = new SqlCommand(strSQL, cnn);
            com.Parameters.AddWithValue("@team", id);

            try
            {
                cnn.Open();
                reader = com.ExecuteReader();

                // Call Read before accessing data
                while (reader.Read())
                { retVal.Add(CreateMatch(reader)); }

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


        private Match CreateMatch(SqlDataReader reader)
        {
            Match retVal = null;
            ClubDAO clubDAO = new ClubDAO();

            retVal = new Match
            {
                id = Convert.ToInt32(reader["match_id"]),
                homeTeam = clubDAO.getClub(Convert.ToInt32(reader["match_hometeam_id"])),
                awayTeam = clubDAO.getClub(Convert.ToInt32(reader["match_awayteam_id"])),
                date = Convert.ToDateTime(reader["match_date"])
            };

            return retVal;
        }
    }

}