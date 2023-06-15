using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Data;

/// <summary>
/// Manages all the seat related logic
/// </summary>
public static class SeatManager {

    /// <summary>
    /// Gathers the seatprices for a flight.
    /// </summary>
    /// <param name="flight">The flight</param>
    /// <returns>A dictionairy with the class-name of the seat and the price.</returns>
    public static Dictionary<string, double> GetSeatPrices(Flight flight) {
        // Get seat prices from json
        JObject? jsonObj = JSONManager.GetJSON("seatprices.json");
        if (jsonObj == null) {
            throw new Exception("Could not find seatprices.json");
        }
        string location = flight.Departure.City == "Amsterdam" ? flight.Destination.City : flight.Departure.City;
        var prices = jsonObj[location]![flight.Airplane.Name]!
        .Children<JProperty>()
        .ToDictionary(prop => prop.Name, prop => prop.Value.Value<double>());
        return prices;
    }

    /// <summary>
    /// Gathers a list of takens seats on a flight.
    /// </summary>
    /// <param name="flight">The flight.</param>
    /// <returns>Returns a list of taken seats on a flight. Could return an empty list.</returns>
    public static List<Seat> GetTakenSeats(Flight flight) {
        // Get taken seats from database
        List<Seat> takenSeats = new List<Seat>();
        DataTable dt = DatabaseManager.QueryResult(@$"
            SELECT seat_number
            FROM reservations_seats
            WHERE flight_id = {flight.Id}
        ");
        List<string> takenSeatNumbers = new List<string>();
        foreach (DataRow row in dt.Rows) {
            string? seatNumber = row["seat_number"].ToString();
            if (seatNumber != null)
                takenSeatNumbers.Add(seatNumber);
        }
        foreach (Seat seat in flight.Airplane.Seats) {
            if (takenSeatNumbers.Contains(seat.Number)) {
                takenSeats.Add(seat);
            }
        }
        return takenSeats;
    }
}