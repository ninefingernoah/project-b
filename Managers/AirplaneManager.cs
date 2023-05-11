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
}