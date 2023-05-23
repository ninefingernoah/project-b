using System.Data;
public static class FlightManager
{

    public static List<Flight> GetAllFlights()
    {
        DataTable dt = DatabaseManager.QueryResult("");
    }
}