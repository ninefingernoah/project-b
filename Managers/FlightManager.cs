using System.Data;
public static class FlightManager
{
    /// <summary>
    /// Gets a specific flight from the database and creates a Flight object from it.
    /// </summary>
    /// <param name="id">The ID of the flight to get.</param>
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

    public static List<Flight> GetFlights(DateTime? departureTime, int departure_id, int destination_id) {
        string query = "SELECT * FROM flights WHERE ";
        if (departure_id != 0) {
            query += $"departure_id = {departure_id} ";
        }
        if (destination_id != 0) {
            query += $"destination_id = {destination_id} ";
        }
        if (departureTime == null || departureTime.Value == DateTime.MinValue) {
            query += $"date(departure_time) = date('{departureTime.Value.ToString("yyyy-MM-dd")}') ";
        }

        if (departure_id == 0 && destination_id == 0 && (departureTime == null || departureTime.Value == DateTime.MinValue)) {
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

    /// <summary>
    /// Updates the flight in the database.
    /// </summary>
    /// <param name="flight">The flight to update.</param>
    public static void UpdateFlight(Flight flight)
    {
        DatabaseManager.QueryNonResult($"UPDATE flights SET departure_id = {flight.Departure.Id}, destination_id = {flight.Destination.Id}, departure_time = '{flight.DepartureTime.ToString("dd-MM-yyyy HH:mm")}', arrival_time = '{flight.ArrivalTime.ToString("dd-MM-yyyy HH:mm")}', airplane_id = {flight.Airplane.Id} WHERE id = {flight.Id}");
        // TODO: Rebook passengers if airplane capacity is smaller than before.
    }
}