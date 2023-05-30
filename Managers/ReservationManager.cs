using System.Data;
public static class ReservationManager
{

    public static void MakeReservation(Reservation res)
    {
        string made_on = res.MadeOn.ToString();
        // main reservation
        DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,flight_id,user_id,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.Flight.Id}','{res.User.Id}','{res.Price}','{made_on}','{res.IsPaid}')");

        // reservation passengers
        foreach (var pass in res.Passengers)
        {
            //push passenger
            PassengerManager.AddPassenger(pass);

            DatabaseManager.QueryNonResult($"INSERT INTO reservation_passengers (reservation_number, passenger_id) VALUES ('{res.ReservationNumber}','{pass.Id}');");
        }



        foreach (var seat in res.Seats)
        {
            // reservation seats
            DatabaseManager.QueryNonResult($"INSERT INTO reservation_seats (reservation_number, seat_number, airplane_id) VALUES ('{res.ReservationNumber}','{seat.Number}','{res.Flight.Airplane.Id}')");

            // flight taken seats
            DatabaseManager.QueryNonResult($"INSERT INTO flight_takenseats (flight_id, seat_number, airplane_id) VALUES ('{res.Flight.Id}','{seat.Number}','{res.Flight.Airplane.Id}')");
        }
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
        foreach (var seat in reservation.Seats)
        {
            // delete reservation seat
            DatabaseManager.QueryNonResult($"DELETE FROM reservation_seats WHERE reservation_number = {reservation.ReservationNumber};");

            // delete flight taken seat
            DatabaseManager.QueryNonResult($"DELETE FROM flight_takenseats WHERE flight_id = {reservation.Flight.Id} AND seat_number = {seat.Number};");
        }
        // delete main
        DatabaseManager.QueryNonResult($"DELETE FROM reservations WHERE number = {reservation.ReservationNumber};");
    }
}