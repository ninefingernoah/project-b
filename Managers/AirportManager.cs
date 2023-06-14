using System.Data;

/// <summary>
/// Manages all the airport related logic
/// </summary>
public static class AirportManager
{
    /// <summary>
    /// Gathers the airports from the database.
    /// </summary>
    /// <returns>A list of airports.</returns>
    public static List<Airport> GetAirports()
    {
        DataTable dt = DatabaseManager.QueryResult("SELECT * FROM airports");
        List<Airport> airports = new List<Airport>();
        foreach (DataRow row in dt.Rows)
        {
            Airport? airport = GetAirport((int)(long)row["id"]);
            if (airport != null)
            {
                airports.Add(airport);
            }
        }
        return airports;
    }

    /// <summary>
    /// Gets a specific airport from the database.
    /// </summary>
    /// <param name="id">The ID of the airport to get. Might return null if the airport is not found.</param>
    public static Airport? GetAirport(int id)
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM airports WHERE id = {id}");
        if (dt.Rows.Count == 0)
        {
            return null;
        }
        DataRow row = dt.Rows[0];
        
        Airport airport = new Airport(
            (int)(long)row["id"],
            (string)row["name"],
            (string)row["city"],
            (string)row["country"],
            (string)row["code"]
        );
        List<String>? facilities = getFacilities(airport.Id);
        // If there are facilities, add them to the airport. We don't want to add null to the airport.
        if(facilities != null)
        {
            airport.Facilities = facilities;
        }
        return airport;
    }

    /// <summary>
    /// Returns a list of facilities for a specific airport.
    /// </summary>
    /// <param name="airportId">The id of an airport</param>
    /// <returns>A list of facilities. The list might be null if none are found.</returns>
    private static List<string>? getFacilities(int airportId)
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM airport_facilities WHERE airport_id = {airportId}");
        List<string> facilities = new List<string>();
        foreach (DataRow row in dt.Rows)
        {
            facilities.Add((string)row["facility"]);
        }
        return facilities;
    }

    /// <summary>
    /// Adds an airport to the database.
    /// </summary>
    /// <param name="airport">The airport to be added</param>
    public static void AddAirport(Airport airport)
    {
        try
        {
            DatabaseManager.QueryNonResult($"INSERT INTO airports (name, city, country, code) VALUES ('{airport.Name}', '{airport.City}', '{airport.Country}', '{airport.Code}')");
        }
        catch (Exception)
        {
            ConsoleUtils.Error("Er is iets fout gegaan bij het toevoegen van het vliegveld aan de database.");
        }       
    }

    public static void AddFacilitiesToAirport(Airport newAirport, List<string> facilities)
    {
        int airportId = newAirport.Id;
        foreach(string facility in facilities)
        {
            DatabaseManager.QueryNonResult($"INSERT INTO airport_facilities (airport_id, facility) VALUES ({airportId}, '{facility}')");
        }
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