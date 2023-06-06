using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Data;
public static class SeatManager {
    
    public static bool AddSeat(Seat seat) {
        // Add a seat to the database
        return true;
    }

    public static bool RemoveSeat(Seat seat) {
        // Remove a seat from the database
        return true;
    }

    public static bool UpdateSeat(Seat seat) {
        // Update a seat in the database
        return true;
    }

    public static Seat GetSeat(int id) {
        // Get a seat from the database
        return null;
    }

    public static Dictionary<string, double> GetSeatPrices(Flight flight) {
        // Get seat prices from json
        JObject jsonObj = JSONManager.GetJSON("seatprices.json");
        string location = flight.Departure.City == "Amsterdam" ? flight.Destination.City : flight.Departure.City;
        var prices = jsonObj[location][flight.Airplane.Name]
        .Children<JProperty>()
        .ToDictionary(prop => prop.Name, prop => prop.Value.Value<double>());
        return prices;
    }

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
            takenSeatNumbers.Add(row["seat_number"].ToString());
        }
        foreach (Seat seat in flight.Airplane.Seats) {
            if (takenSeatNumbers.Contains(seat.Number)) {
                takenSeats.Add(seat);
            }
        }
        return takenSeats;
    }
}