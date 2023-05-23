using System.Data;
public static class ReservationManager
{

    public static void MakeReservation(Reservation res)
    {
        string made_on = res.ReservationDate.ToString();
        DatabaseManager.QueryNonResult($"INSERT INTO reservations (number,flight_id,user_id,price,made_on,is_paid) VALUES ('{res.ReservationNumber}','{res.Flight.Id}','{res.User.Id}','{res.Price}','{made_on}','{res.IsPaid}')");
    }
}