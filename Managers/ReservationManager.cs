using System.Data;
public static class ReservationManager
{
    public static bool AddReservation(Reservation reservation)
    {
        // Add a reservation to the database
        return true;
    }
    public static bool RemoveReservation(Reservation reservation)
    {
        // Remove a reservation from the database
        return true;
    }
    public static bool UpdateReservation(Reservation reservation)
    {
        // Update a reservation in the database
        return true;
    }
    public static Reservation GetReservation(int id)
    {
        // Get a reservation from the database
        return null;
    }

    public static Reservation GetReservation(DataRow dr)
    {
        Flight f = FlightManager.GetFlight((int)(long)dr["flight_id"])!;
        int user_id = (int)(long)dr["user_id"];
        User user = UserManager.GetUser(user_id);
        Reservation r = new Reservation(
            (string)dr["number"],
            f,
            user,
            (string)dr["email"],
            (double)dr["price"],
            DateTime.Parse((string)dr["made_on"])
        );
        var seats = DatabaseManager.QueryResult($"SELECT * FROM reservations_seats WHERE reservation_number = '{r.ReservationNumber}'");
        foreach (DataRow seat in seats.Rows)
        {
            string color = (string)seat["color"];
            r.Seats.Add(new Seat((string)seat["seat_number"], color));
        }
        var passengers = DatabaseManager.QueryResult($"SELECT * FROM passengers INNER JOIN reservation_passengers ON passengers.id = reservation_passengers.passenger_id WHERE reservation_number = '{r.ReservationNumber}'");
        foreach (DataRow dr2 in passengers.Rows)
        {
            Passenger passenger = new Passenger(
                (int)(long)dr2["id"],
                (string)dr2["email"],
                (string)dr2["first_name"],
                (string)dr2["last_name"],
                (string)dr2["document_number"]
            );
        }
        return r;
    }

    public static List<Reservation> GetReservationsByUser(User user)
    {
        DataTable res = DatabaseManager.QueryResult($"SELECT * FROM reservations WHERE user_id = {user.Id} OR email = '{user.Email}'");
        List<Reservation> reservations = new List<Reservation>();
        foreach (DataRow dr in res.Rows)
        {
            reservations.Add(GetReservation(dr));
        }
        return reservations;
    }
}