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
        DatabaseManager.QueryNonResult($"UPDATE reservations SET flight_id = {reservation.Flight.Id}, user_id = {reservation.User.Id}, email = '{reservation.Email}', price = {reservation.Price}, made_on = '{reservation.MadeOn.ToString("dd-MM-yyyy")}' WHERE number = '{reservation.ReservationNumber}'");
        foreach(Passenger p in reservation.Passengers)
        {
            var oldFirstName = DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {p.Id}").Rows[0]["first_name"];
            var oldLastName = DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {p.Id}").Rows[0]["last_name"];
            var oldName = $"{oldFirstName} {oldLastName}";
            if (oldName != (p.FirstName + " " + p.LastName))
                DatabaseManager.QueryNonResult($"UPDATE passengers SET letters_changed = 1 WHERE id = {p.Id}");
            int address_id = (int)(long)DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {p.Id}").Rows[0]["address_id"];
            DatabaseManager.QueryNonResult($"UPDATE passengers SET email = '{p.Email}', first_name = '{p.FirstName}', last_name = '{p.LastName}', document_number = '{p.DocumentNumber}', date_of_birth = '{((DateTime) p.BirthDate).ToString("dd-MM-yyyy")}' WHERE id = {p.Id}");
            DatabaseManager.QueryNonResult($"UPDATE addresses SET street = '{p.Address.Street}', street_number = '{p.Address.StreetNumber}', city = '{p.Address.City}' WHERE id = {address_id}");
        }
        return true;
    }
    public static Reservation? GetReservation(string code)
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM reservations WHERE number = '{code}'");
        if(dt == null || dt.Rows.Count == 0)
        {
            return null;
        }
        
        return GetReservation(dt.Rows[0]);
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
        var passengers = DatabaseManager.QueryResult($"SELECT passengers.id, passengers.email, passengers.first_name, passengers.last_name, passengers.document_number FROM passengers INNER JOIN reservation_passengers ON passengers.id = reservation_passengers.passenger_id WHERE reservation_number = '{r.ReservationNumber}'");
        foreach (DataRow dr2 in passengers.Rows)
        {
            r.Passengers.Add(PassengerManager.GetPassenger((int)(long)dr2["id"]));
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