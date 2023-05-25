using System.Data;
public static class FlightManager
{

    public static List<Flight> GetAllFlights(string? filter = null)
    {
        DataTable dt;
        List<Flight> Flights = new List<Flight>();
        if (filter == null)
        {
            dt = DatabaseManager.QueryResult("SELECT * FROM flights");
        }
        else
        {
            // TODO: add filter
            dt = DatabaseManager.QueryResult("SELECT * FROM flights");
        }

        foreach (DataRow dr in dt.Rows)
        {
            int id = (int)dr["id"];
            string departure = AirportManager.GetAirport((int)dr["departure_id"]).Country;
            string destionation = AirportManager.GetAirport((int)dr["destination_id"]).Country; ;
            DateTime departureTime;
            if (DateTime.TryParse((string)dr["departure_time"], out departureTime)) { }
            else
            {
                ConsoleUtils.Error("departure tijd error");
            }
            DateTime arrivalTime;
            if (DateTime.TryParse((string)dr["arrival_time"], out arrivalTime)) { }
            else
            {
                ConsoleUtils.Error("arrival tijd error");
            }
            Airplane airplane = AirplaneManager.GetAirplane((int)dr["id"]);

            Flights.Add(new Flight(id, departure, destionation, departureTime, arrivalTime, airplane));
        }

        return Flights;
    }

    public static int GetNewestId()
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT id FROM flights ORDER BY id ASC");
        try
        {
            DataRow dr = dt.Rows[0];
            return (int)dr["id"] + 1;
        }
        catch (System.Exception)
        {
            return 1;
        }

        
    }
}