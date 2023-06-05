using System.Data;
public static class ReservationManager
{

    public static bool MakeReservation(Reservation res)
    {
        string made_on = res.ReservationDate.ToString();
        
        try {
            // main reservation
            if (res.User != null) {
                if (res.InwardFlight == null) {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,outward_flight_id,user_id,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.OutwardFlight.Id}','{res.User.Id}','{res.Price}','{made_on}','{res.IsPaid}')");
                }
                else {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,outward_flight_id,inward_flight_id,user_id,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.OutwardFlight.Id}','{res.InwardFlight.Id}','{res.User.Id}','{res.Price}','{made_on}','{res.IsPaid}')");
                    foreach (var seat in res.InwardSeats)
                    {
                        // reservation seats
                        DatabaseManager.QueryNonResult($"INSERT INTO reservations_seats (reservation_number, seat_number, airplane_id, flight_id) VALUES ('{res.ReservationNumber}','{seat.Number}','{res.InwardFlight.Airplane.Id}', '{res.InwardFlight.Id}')");

                        // flight taken seats
                        DatabaseManager.QueryNonResult($"INSERT INTO flight_takenseats (flight_id, seat_number, airplane_id) VALUES ('{res.InwardFlight.Id}','{seat.Number}','{res.InwardFlight.Airplane.Id}')");
                    }
                }
            }
            else {
                if (res.InwardFlight == null) {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,outward_flight_id,email,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.OutwardFlight.Id}','{res.Email}','{res.Price}','{made_on}','{res.IsPaid}')");
                }
                else {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,outward_flight_id,inward_flight_id,email,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.OutwardFlight.Id}','{res.InwardFlight.Id}','{res.Email}','{res.Price}','{made_on}','{res.IsPaid}')");
                }
            }
            foreach (var seat in res.OutwardSeats)
            {
                // reservation seats
                DatabaseManager.QueryNonResult($"INSERT INTO reservations_seats (reservation_number, seat_number, airplane_id, flight_id) VALUES ('{res.ReservationNumber}','{seat.Number}','{res.OutwardFlight.Airplane.Id}', '{res.OutwardFlight.Id}')");

                // flight taken seats
                DatabaseManager.QueryNonResult($"INSERT INTO flight_takenseats (flight_id, seat_number, airplane_id) VALUES ('{res.OutwardFlight.Id}','{seat.Number}','{res.OutwardFlight.Airplane.Id}')");
            }
            

            // reservation passengers
            foreach (var pass in res.Passengers)
            {
                //add passenger
                PassengerManager.AddPassenger(pass);

                DatabaseManager.QueryNonResult($"INSERT INTO reservation_passengers (reservation_number, passenger_id) VALUES ('{res.ReservationNumber}','{pass.Id}');");
            }
        }
        catch (Exception e)
        {
            return false;
        }
        
        return true;
    }

    public static void DeleteReservation(Reservation reservation)
    {

        // delete passenger data
        foreach (var pass in reservation.Passengers)
        {
            // delete passenger
            PassengerManager.DeletePassenger(pass);

            // delete reservation passenger
            DatabaseManager.QueryNonResult($"DELETE FROM reservation_passengers WHERE reservation_number = {reservation.ReservationNumber};");
        }

        // delete seat data
            // delete reservation seat
            DatabaseManager.QueryNonResult($"DELETE FROM reservation_seats WHERE reservation_number = {reservation.ReservationNumber};");
        foreach (var seat in reservation.OutwardSeats)
        {
            // delete flight taken seat
            DatabaseManager.QueryNonResult($"DELETE FROM flight_takenseats WHERE flight_id = {reservation.OutwardFlight.Id} AND seat_number = {seat.Number};");
        }
        if (reservation.InwardSeats.Count > 0) {
            foreach (var seat in reservation.InwardSeats) {
            // delete flight taken seat
            DatabaseManager.QueryNonResult($"DELETE FROM flight_takenseats WHERE flight_id = {reservation.InwardFlight.Id} AND seat_number = {seat.Number};");
            }
        }
        // delete main
        DatabaseManager.QueryNonResult($"DELETE FROM reservations WHERE number = {reservation.ReservationNumber};");
    }


    /// <summary>
    /// Updates the reservation in the database
    /// </summary>
    /// <param name="reservation">The reservation to update</param>
    public static bool UpdateReservation(Reservation reservation)
    {
        DatabaseManager.QueryNonResult($"UPDATE reservations SET flight_id = {reservation.InwardFlight.Id}, user_id = {reservation.User.Id}, email = '{reservation.Email}', price = {reservation.Price}, made_on = {reservation.ReservationDate.ToString("dd-MM-yyyy")}' WHERE number = '{reservation.ReservationNumber}'");
        foreach (Passenger p in reservation.Passengers)
        {
            // Check if the name has changed
            var oldFirstName = DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {p.Id}").Rows[0]["first_name"];
            var oldLastName = DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {p.Id}").Rows[0]["last_name"];
            var oldName = $"{oldFirstName} {oldLastName}";
            if (oldName != (p.FirstName + " " + p.LastName))
                DatabaseManager.QueryNonResult($"UPDATE passengers SET letters_changed = 1 WHERE id = {p.Id}"); // Places a lock on the passengername.
            int address_id = (int)(long)DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {p.Id}").Rows[0]["address_id"];
            // Changes the passenger and their address
            DatabaseManager.QueryNonResult($"UPDATE passengers SET email = '{p.Email}', first_name = '{p.FirstName}', last_name = '{p.LastName}', document_number = '{p.DocumentNumber}', date_of_birth = '{((DateTime)p.BirthDate).ToString("dd-MM-yyyy")}' WHERE id = {p.Id}");
            DatabaseManager.QueryNonResult($"UPDATE addresses SET street = '{p.Address.Street}', street_number = '{p.Address.StreetNumber}', city = '{p.Address.City}' WHERE id = {address_id}");
        }
        if (reservation.User != null)
        {
            DatabaseManager.QueryNonResult($"UPDATE users SET email = '{reservation.User.Email}' WHERE id = {reservation.User.Id}");
        }
        return true;
    }
    public static Reservation? GetReservation(string code)
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM reservations WHERE number = '{code}'");
        if (dt == null || dt.Rows.Count == 0)
        {
            return null;
        }

        return GetReservation(dt.Rows[0]);
    }

    public static Reservation GetReservation(DataRow dr)
    {
        Flight f_out = FlightManager.GetFlight((int)(long)dr["outward_flight_id"])!;
        Flight f_in = FlightManager.GetFlight((int)(long)dr["inward_flight_id"])!;
        int user_id = (int)(long)dr["user_id"];
        User user = UserManager.GetUser(user_id);
        Reservation r = new Reservation(
            (int)dr["number"],
            f_out,
            f_in,
            user,
            (string)dr["email"],
            null,
            (double)dr["price"],
            DateTime.Parse((string)dr["made_on"])
        );
        //outward
        var outSeats = DatabaseManager.QueryResult($"SELECT * FROM reservations_seats WHERE reservation_number = '{r.ReservationNumber}' AND flight_id = '{r.OutwardFlight.Id}'");
        foreach (DataRow seat in outSeats.Rows)
        {
            string color = (string)seat["color"];
            r.AddOutwardSeat(new Seat((string)seat["seat_number"], color));
        }
        //inward
        if(f_in != null) {
            var inSeats = DatabaseManager.QueryResult($"SELECT * FROM reservations_seats WHERE reservation_number = '{r.ReservationNumber}' AND flight_id = '{r.InwardFlight.Id}'");
            foreach (DataRow seat in inSeats.Rows)
            {
                string color = (string)seat["color"];
                r.AddInwardSeat(new Seat((string)seat["seat_number"], color));
            }
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

    public static int GetReservationCode()
    {
        return (int)(long)DatabaseManager.QueryResult("SELECT MAX(number) FROM reservations").Rows[0][0] + 1;
    }
}