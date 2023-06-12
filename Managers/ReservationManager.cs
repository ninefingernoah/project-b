using System.Data;
using System;
using System.Text;

/// <summary>
/// Manages all the reservation related logic
/// </summary>
public static class ReservationManager
{
    /// <summary>
    /// Makes a reservation in the database.
    /// </summary>
    /// <param name="res">The reservation to be made.</param>
    /// <returns>Whether the addition was successful</returns>
    public static bool MakeReservation(Reservation res)
    {
        string made_on = res.ReservationDate.ToString();

        try
        {
            // main reservation
            if (res.User != null)
            {
                if (res.InwardFlight == null)
                {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,outward_flight_id,user_id,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.OutwardFlight.Id}','{res.User.Id}','{res.Price}','{made_on}','{res.IsPaid}')");
                }
                else
                {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,outward_flight_id,inward_flight_id,user_id,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.OutwardFlight.Id}','{res.InwardFlight.Id}','{res.User.Id}','{res.Price}','{made_on}','{res.IsPaid}')");
                }
            }
            else
            {
                if (res.InwardFlight == null)
                {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,outward_flight_id,email,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.OutwardFlight.Id}','{res.Email}','{res.Price}','{made_on}','{res.IsPaid}')");
                }
                else
                {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,outward_flight_id,inward_flight_id,email,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.OutwardFlight.Id}','{res.InwardFlight.Id}','{res.Email}','{res.Price}','{made_on}','{res.IsPaid}')");
                }
            }
            foreach (var seat in res.OutwardSeats)
            {
                // reservation seats
                DatabaseManager.QueryNonResult($"INSERT INTO reservations_seats (reservation_number, seat_number, airplane_id, flight_id) VALUES ('{res.ReservationNumber}','{seat.Number}','{res.OutwardFlight.Airplane.Id}', '{res.OutwardFlight.Id}')");

                // flight taken seats
                DatabaseManager.QueryNonResult($"INSERT INTO flight_takenseats (flight_id, seat_number, reservation_number) VALUES ('{res.OutwardFlight.Id}','{seat.Number}','{res.ReservationNumber}')");
            }
            if (res.InwardFlight != null && res.InwardSeats != null)
            {
                foreach (var seat in res.InwardSeats)
                {
                    // reservation seats
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations_seats (reservation_number, seat_number, airplane_id, flight_id) VALUES ('{res.ReservationNumber}','{seat.Number}','{res.InwardFlight.Airplane.Id}', '{res.InwardFlight.Id}')");

                    // flight taken seats
                    DatabaseManager.QueryNonResult($"INSERT INTO flight_takenseats (flight_id, seat_number, reservation_number) VALUES ('{res.InwardFlight.Id}','{seat.Number}','{res.ReservationNumber}')");
                }
            }

            // reservation passengers
            foreach (var pass in res.Passengers)
            {
                //add passenger
                PassengerManager.AddPassenger(pass);

                DatabaseManager.QueryNonResult($"INSERT INTO reservation_passengers (reservation_number, passenger_id) VALUES ('{res.ReservationNumber}','{pass.Id}');");
            }
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Deletes a reservation from the database
    /// </summary>
    /// <param name="reservation">The reservation to be deleted</param>
    public static void DeleteReservation(Reservation? reservation)
    {
        if (reservation == null) return;

        // delete passenger data
        foreach (var pass in reservation.Passengers)
        {
            // delete passenger
            PassengerManager.DeletePassenger(pass);

            // delete reservation passenger
            DatabaseManager.QueryNonResult($"DELETE FROM reservation_passengers WHERE reservation_number = '{reservation.ReservationNumber}';");
        }

        // delete reservation seat
        DatabaseManager.QueryNonResult($"DELETE FROM reservations_seats WHERE reservation_number = '{reservation.ReservationNumber}';");
        foreach (var seat in reservation.OutwardSeats)
        {
            // delete taken seats from flight
            DatabaseManager.QueryNonResult($"DELETE FROM flight_takenseats WHERE flight_id = {reservation.OutwardFlight.Id} AND seat_number = {seat.Number};");
        }
        if (reservation.InwardFlight != null && reservation.InwardSeats != null)
        {
            foreach (var seat in reservation.InwardSeats)
            {
                // delete taken seats from flight
                DatabaseManager.QueryNonResult($"DELETE FROM flight_takenseats WHERE flight_id = {reservation.InwardFlight.Id} AND seat_number = {seat.Number};");
            }
        }
        // Delete the reservation it self.
        DatabaseManager.QueryNonResult($"DELETE FROM reservations WHERE number = '{reservation.ReservationNumber}';");
    }


    /// <summary>
    /// Updates the reservation in the database
    /// </summary>
    /// <param name="reservation">The reservation to update</param>
    /// <returns>Whether the update was successful</returns>
    // I do not really get why this is a bool. It will always return true?
    public static bool UpdateReservation(Reservation reservation)
    {
        // TODO: update reservation_seats en flight_takenseats
        if (reservation.User == null)
        {
            DatabaseManager.QueryNonResult($"UPDATE reservations SET outward_flight_id = {reservation.OutwardFlight.Id}, user_id = NULL, email = '{reservation.Email}', price = {reservation.Price}, made_on = '{reservation.ReservationDate.ToString("dd-MM-yyyy")}' WHERE number = '{reservation.ReservationNumber}'");
        }
        else
        {
            DatabaseManager.QueryNonResult($"UPDATE reservations SET outward_flight_id = {reservation.OutwardFlight.Id}, user_id = {reservation.User.Id}, email = '{reservation.Email}', price = {reservation.Price}, made_on = '{reservation.ReservationDate.ToString("dd-MM-yyyy")}' WHERE number = '{reservation.ReservationNumber}'");
        }
        if (reservation.InwardFlight != null)
        {
            DatabaseManager.QueryNonResult($"UPDATE reservations SET inward_flight_id = {reservation.InwardFlight.Id} WHERE number = '{reservation.ReservationNumber}'");

        }
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
            DatabaseManager.QueryNonResult($"UPDATE passengers SET email = '{p.Email}', first_name = '{p.FirstName}', last_name = '{p.LastName}', document_number = '{p.DocumentNumber}', date_of_birth = '{((DateTime)p.BirthDate!).ToString("dd-MM-yyyy")}' WHERE id = {p.Id}");
            DatabaseManager.QueryNonResult($"UPDATE addresses SET street = '{p.Address.Street}', street_number = '{p.Address.HouseNumber}', city = '{p.Address.City}' WHERE id = {address_id}");
        }
        if (reservation.User != null)
        {
            DatabaseManager.QueryNonResult($"UPDATE users SET email = '{reservation.User.Email}' WHERE id = {reservation.User.Id}");
        }
        UpdateReservationSeats(reservation);

        return true;
    }

    public static void UpdateReservationSeats(Reservation reservation)
    {
        // delete old seats
        bool tryDelete1 = DatabaseManager.QueryNonResult($"DELETE FROM flight_takenseats WHERE flight_id = {reservation.OutwardFlight.Id} AND reservation_number = '{reservation.ReservationNumber}'");
        bool tryDelete2 = DatabaseManager.QueryNonResult($"DELETE FROM reservations_seats WHERE reservation_number = '{reservation.ReservationNumber}'");
        bool tryDelete3 = true;
        if (reservation.InwardFlight != null){
            tryDelete3 = DatabaseManager.QueryNonResult($"DELETE FROM flight_takenseats WHERE flight_id = {reservation.InwardFlight.Id} AND reservation_number = '{reservation.ReservationNumber}'");
        }
        // if old seats are deleted, add new seats
        if (tryDelete1 && tryDelete2 && tryDelete3) {
            // outward seats
            if(reservation.OutwardSeats != null && reservation.OutwardSeats.Count > 0)
            {
                foreach (var seat in reservation.OutwardSeats)
                {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations_seats (reservation_number, seat_number, airplane_id, flight_id) VALUES ('{reservation.ReservationNumber}', '{seat.Number}', {reservation.OutwardFlight.Airplane.Id}, {reservation.OutwardFlight.Id});");
                    DatabaseManager.QueryNonResult($"INSERT INTO flight_takenseats (flight_id, seat_number, reservation_number) VALUES ({reservation.OutwardFlight.Id}, '{seat.Number}', '{reservation.ReservationNumber}');");
                }
            }
            // inward seats
            if(reservation.InwardSeats != null && reservation.InwardSeats.Count > 0)
            {
                foreach (var seat in reservation.InwardSeats)
                {
                    DatabaseManager.QueryNonResult($"INSERT INTO reservations_seats (reservation_number, seat_number, airplane_id, flight_id) VALUES ('{reservation.ReservationNumber}', '{seat.Number}', {reservation.InwardFlight.Airplane.Id}, {reservation.InwardFlight.Id});");
                    DatabaseManager.QueryNonResult($"INSERT INTO flight_takenseats (flight_id, seat_number, reservation_number) VALUES ({reservation.InwardFlight.Id}, '{seat.Number}', '{reservation.ReservationNumber}');");
                }
            }
        }
            
            
    }

    /// <summary>
    /// Gets a reservation from the database
    /// </summary>
    /// <param name="code">The code of the reservation.</param>
    /// <returns>The reservation object. Might return null if the reservation cannot be found.</returns>
    public static Reservation? GetReservation(string code)
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM reservations WHERE number = '{code}'");
        if (dt == null || dt.Rows.Count == 0)
        {
            return null;
        }

        return GetReservation(dt.Rows[0]);
    }

    /// <summary>
    /// Gets a reservation object from a DataRow. This is for ease of use.
    /// </summary>
    /// <param name="dr">The datarow</param>
    /// <returns>The reservation object.</returns>
    public static Reservation GetReservation(DataRow dr)
    {
        Flight? f_out = FlightManager.GetFlight((int)(long)dr["outward_flight_id"]);
        if (f_out == null) throw new Exception("Outward flight not found"); // The outward flight should always be found.
        Flight? f_in = null;
        try
        {
            f_in = FlightManager.GetFlight((int)(long)dr["inward_flight_id"]);
        }
        catch (Exception)
        {
            f_in = null;
        }
        User? user = null;
        try
        {
            user = UserManager.GetUser((int)(long)dr["user_id"]);
        }
        catch (Exception)
        {
            user = null;
        }
        Reservation? r = new Reservation(
            (string)dr["number"],
            f_out,
            f_in,
            user,
            (string)dr["email"],
            new List<Passenger>(),
            (double)dr["price"],
            DateTime.Parse((string)dr["made_on"])
        );
        //outward
        var outSeats = DatabaseManager.QueryResult($"SELECT seat_number FROM reservations_seats WHERE reservation_number = '{r.ReservationNumber}' AND flight_id = {r.OutwardFlight.Id}");
        foreach (DataRow seat in outSeats.Rows)
        {
            Seat? planeSeat = f_out.Airplane.Seats.Find(s => s.Number == (string)seat["seat_number"]);
            if (planeSeat != null) r.AddOutwardSeat(planeSeat);
        }
        //inward
        if (f_in != null && r.InwardFlight != null)
        {
            var inSeats = DatabaseManager.QueryResult($"SELECT seat_number FROM reservations_seats WHERE reservation_number = '{r.ReservationNumber}' AND flight_id = {r.InwardFlight.Id}");
            foreach (DataRow seat in inSeats.Rows)
            {
                Seat? planeSeat = f_in.Airplane.Seats.Find(s => s.Number == (string)seat["seat_number"]);
                if (planeSeat != null) r.AddInwardSeat(planeSeat);
            }
        }
        var passengers = DatabaseManager.QueryResult($"SELECT p.id, p.email, p.first_name, p.last_name, p.document_number FROM passengers p INNER JOIN reservation_passengers rp ON p.id = rp.passenger_id WHERE rp.reservation_number = '{r.ReservationNumber}'");
        foreach (DataRow dr2 in passengers.Rows)
        {
            r.Passengers.Add(PassengerManager.GetPassenger((int)(long)dr2["id"]));
        }
        return r;
    }

    /// <summary>
    /// Gathers a list of reservations which the user made. Might return an empty list if none are found.
    /// </summary>
    /// <param name="user">The user</param>
    /// <returns>A list of reservations which the user made. Might return an empty list if none are found.</returns>
    public static List<Reservation> GetReservationsByUser(User user)
    {
        DataTable res = DatabaseManager.QueryResult($"SELECT * FROM reservations WHERE user_id = {user.Id}");
        List<Reservation> reservations = new List<Reservation>();
        foreach (DataRow dr in res.Rows)
        {
            reservations.Add(GetReservation(dr));
        }
        return reservations;
    }

    /// <summary>
    /// Generates a random reservation code. It will always be unique.
    /// </summary>
    /// <returns>The random reservation code.</returns>
    public static string GetNewReservationCode()
    {
        Random random = new Random();
        int length = 5;
        string Rando;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder stringBuilder = new StringBuilder(length);
        do
        {
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }
            Rando = stringBuilder.ToString();
        } while (GetReservation(Rando) != null);
        return Rando;
    }
}