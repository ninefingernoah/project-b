using System.Data;
public static class AirplaneManager
{
    public static Airplane GetAirplane(int id)
    {
        DataRow dr = DatabaseManager.QueryResult($"SELECT * FROM airplanes WHERE id = '{id};").Rows[0];

        return new Airplane((int)dr["id"], (int)dr["total_capacity"], (string)dr["name"]);
    }
}