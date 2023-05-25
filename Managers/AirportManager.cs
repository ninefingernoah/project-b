using System.Data;

public static class AirportManager
{
    public static Airport GetAirport(int id)
    {
        DataRow dr = DatabaseManager.QueryResult($"SELECT * FROM airports WHERE id = '{id};").Rows[0];

        // TODO: fix data;
        int Id = (int)dr["id"];
        string country = (string)dr["county"];
        string city = (string)dr["city"];

        Dictionary<int, Dictionary<int, int>> prices = new Dictionary<int, Dictionary<int, int>>();
        
        return new Airport(Id, country, city, prices);
    }
}