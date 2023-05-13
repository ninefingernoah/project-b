using System.Data;
public static class AirplaneManager {
    public static bool AddAirplane(Airplane airplane) {
        // Add an airplane to the database
        return true;
    }

    public static bool RemoveAirplane(Airplane airplane) {
        // Remove an airplane from the database
        return true;
    }

    public static bool UpdateAirplane(Airplane airplane) {
        // Update an airplane in the database
        return true;
    }

    /// <summary>
    /// Gets an airplane from the database.
    /// </summary>
    /// <param name="id">The ID of the airplane to get.</param>
    public static Airplane? GetAirplane(int id) {
        // Get an airplane from the database
        DataRow dr = DatabaseManager.QueryResult($"SELECT * FROM airplanes WHERE id = {id}").Rows[0];
        Airplane airplane = new Airplane(
            (int)(long)dr["id"],
            (int)(long)dr["total_capacity"],
            (string)dr["name"]
        );
        return airplane;
    }

    /// <summary>
    /// Gathers all the airplanes from the database.
    /// </summary>
    public static List<Airplane> GetAirplanes()
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM airplanes");
        List<Airplane> airplanes = new List<Airplane>();
        foreach(DataRow dr in dt.Rows)
        {
            Airplane airplane = new Airplane(
                (int)(long)dr["id"],
                (int)(long)dr["total_capacity"],
                (string)dr["name"]
            );
            airplanes.Add(airplane);
        }
        return airplanes;
    }
}