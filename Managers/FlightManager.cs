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