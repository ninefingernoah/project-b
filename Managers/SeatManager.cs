using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
        string location = flight.Departure == "Schiphol" ? flight.Destination : flight.Departure;
        JToken priceToken = jsonObj.SelectToken($"$.{flight.Airplane.Name}.{location}");
        if (priceToken != null) {
            return JsonConvert.DeserializeObject<Dictionary<string, double>>(priceToken.ToString())!;
        }
        return null;
    }
}