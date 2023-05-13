using System.Data;
public static class FlightManager
{
    public static Flight GetFlight(int id)
    {
        DataRow dr = DatabaseManager.QueryResult($"SELECT * FROM flights WHERE id = {id}").Rows[0];
        int departureId = (int)(long)dr["departure_id"];
        int destinationId = (int)(long)dr["destination_id"];

        Airport? departure = AirportManager.GetAirport(departureId);
        Airport? destination = AirportManager.GetAirport(destinationId);
        if (departure == null || destination == null)
        {
            throw new Exception("Departure or destination is null");
        }

        DateTime departureTime = DateTime.Parse((string)dr["departure_time"]);
        DateTime arrivalTime = DateTime.Parse((string)dr["arrival_time"]);
        Airplane? airplane = AirplaneManager.GetAirplane((int)(long)dr["airplane_id"]);
        if (airplane == null)
        {
            throw new Exception("Airplane is null");
        }
        Flight flight = new Flight(
            (int)(long)dr["id"],
            departure,
            destination,
            departureTime,
            arrivalTime,
            airplane
        );
        return flight;
    }

    public static List<Flight> GetFlights(DateTime? departureTime, Airport? departure, Airport? destination) {
        string query = "SELECT * FROM flights WHERE ";
        if (departure != null) {
            query += $"departure_id = {departure.Id} ";
        }
        if (destination != null) {
            query += $"destination_id = {destination.Id} ";
        }
        if (departureTime != null) {
            query += $"date(departure_time) = date('{departureTime.Value.ToString("yyyy-MM-dd")}') ";
        }

        if (departure == null && destination == null && departureTime == null) {
            query = "SELECT * FROM flights";
        }

        DataTable dt = DatabaseManager.QueryResult(query);
        List<Flight> flights = new List<Flight>();
        foreach (DataRow dr in dt.Rows) {
            int departureId = (int)(long)dr["departure_id"];
            int destinationId = (int)(long)dr["destination_id"];

            Airport? departureAirport = AirportManager.GetAirport(departureId);
            Airport? destinationAirport = AirportManager.GetAirport(destinationId);
            if (departureAirport == null || destinationAirport == null) {
                throw new Exception("Departure or destination is null");
            }

            DateTime depTime = DateTime.Parse((string)dr["departure_time"]);
            DateTime arrTime = DateTime.Parse((string)dr["arrival_time"]);
            Airplane? airplane = AirplaneManager.GetAirplane((int)(long)dr["airplane_id"]);
            if (airplane == null) {
                throw new Exception("Airplane is null");
            }
            Flight flight = new Flight(
                (int)(long)dr["id"],
                departureAirport,
                destinationAirport,
                depTime,
                arrTime,
                airplane
            );
            flights.Add(flight);
        }
        return flights;
    }
}