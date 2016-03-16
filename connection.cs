using System;

public class Connection
{
    SqlConnection conn = null;

    public Connection()
    {
    }

    public void connect()
    {
        string strCon = System.Web
                              .Configuration
                              .WebConfigurationManager
                              .ConnectionStrings["webprog"]
                              .ConnectionString;

        conn = new SqlConnection(strCon);
        conn.Open();
    }

    public Club[] getTeams()
    {
        if (conn != null)
        {
            Club[] retVal;
            String sql = "SELECT * FROM team";

            SqlCommand comm = new SqlCommand(sql, conn);

            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                int team_id = (int)nwReader["team_id"];
                String team_name = nwReader["team_name"];
                String description = nwReader["team_description"];
                Club c = new Club(team_id, team_name, description);
                retVal[retVal.Length] = c;
            }
            nwReader.Close();
        }

        return retVal;
    }

    public void close()
    {
        if(conn != null)
            conn.close();
    }
}
