using System.Data;
public static class AirportManager
{
    /// <summary>
    /// Gathers the airports from the database.
    /// </summary>
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

    /// <summary>
    /// Gets a specific airport from the database.
    /// </summary>
    /// <param name="id">The ID of the airport to get.</param>
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

    /// <summary>
    /// Gets a specific airport from the database.
    /// </summary>
    /// <param name="name">The name of the airport to get.</param>
    // This code is never used, but it's here for reference.
    // public static Airport? GetAirport(string name)
    // {
    //     DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM airports WHERE name = '{name}'");
    //     if (dt.Rows.Count == 0)
    //     {
    //         return null;
    //     }
    //     DataRow row = dt.Rows[0];
    //     return new Airport(
    //         (int)(long)row["id"],
    //         (string)row["name"],
    //         (string)row["city"],
    //         (string)row["country"],
    //         (string)row["code"]
    //     );
    // }
}