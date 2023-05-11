using System.Data;
public static class AirportManager
{
    public static List<Airport> GetAirports()
    {
        DataTable dt = DatabaseManager.QueryResult("SELECT * FROM airports");
        List<Airport> airports = new List<Airport>();
        foreach (DataRow row in dt.Rows)
        {
            airports.Add(new Airport(
                (int)(long)row["id"],
                (string)row["name"],
                (string)row["city"],
                (string)row["country"],
                (string)row["code"]
            ));
        }
        return airports;
    }

    public static Airport? GetAirport(int id)
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM airports WHERE id = {id}");
        if (dt.Rows.Count == 0)
        {
            return null;
        }
        DataRow row = dt.Rows[0];
        return new Airport(
            (int)(long)row["id"],
            (string)row["name"],
            (string)row["city"],
            (string)row["country"],
            (string)row["code"]
        );
    }

    public static Airport? GetAirport(string name)
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM airports WHERE name = '{name}'");
        if (dt.Rows.Count == 0)
        {
            return null;
        }
        DataRow row = dt.Rows[0];
        return new Airport(
            (int)(long)row["id"],
            (string)row["name"],
            (string)row["city"],
            (string)row["country"],
            (string)row["code"]
        );
    }
}