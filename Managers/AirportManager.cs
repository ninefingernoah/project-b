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


    public static List<Airport> GetAllAirports()
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM airports");
        List<Airport> airports = new List<Airport>();

        foreach (DataRow dr in dt.Rows)
        {
            // TODO: fix data;
            var Id = (int)(long)dr["id"];
            string country = (string)dr["country"];
            string city = (string)dr["city"];

            Dictionary<int, Dictionary<int, int>> prices = new Dictionary<int, Dictionary<int, int>>();

            airports.Add(new Airport(Id, country, city, prices));
        }

        return airports;
    }
}