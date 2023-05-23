using System.Data;
public class ReservationManager
{
    public List<Reservation> Reservations = new List<Reservation>();

    public ReservationManager()
    {
        LoadReservations();
    }

    public bool PlaceReservation(Reservation reservation)
    {
        // Check if user already has reservation at that date
        List<Reservation> reservations = Program.UserManager.LoggedInUser.GetReservations();
        foreach (Reservation r in reservations)
        {
            if (r.Date == reservation.Date)
            {
                return false;
            }
        }
        // Choose seating
        bool isSeatingPossible = ChooseSeating(reservation);
        if (!isSeatingPossible)
        {
            return false;
        }
        // Add reservation to list
        Reservations.Add(reservation);
        DatabaseManager db = Program.DatabaseManager;
        if (reservation.WineArrangement == null && reservation.CourseArrangement == null)
        {
            db.Query($@"INSERT INTO Reservations (id, name, email, phonenumber, groupsize, date_and_time) VALUES (
                '{reservation.ReservationCode}',
                '{reservation.Name}',
                '{reservation.Email}',
                '{reservation.PhoneNumber}',
                '{reservation.GroupSize}',
                '{reservation.Date.ToString()}'
            );");
        }
        else if (reservation.WineArrangement == null)
        {
            db.Query($@"INSERT INTO Reservations (id, name, email, phonenumber, groupsize, date_and_time, course_arrangement_id) VALUES (
                            '{reservation.ReservationCode}',
                            '{reservation.Name}',
                            '{reservation.Email}',
                            '{reservation.PhoneNumber}',
                            '{reservation.GroupSize}',
                            '{reservation.Date.ToString()}',
                            '{reservation.CourseArrangement.id}'
            );");
        }
        else if (reservation.CourseArrangement == null)
        {
            db.Query($@"INSERT INTO Reservations (id, name, email, phonenumber, groupsize, date_and_time, wine_arrangement_id) VALUES (
                '{reservation.ReservationCode}',
                '{reservation.Name}',
                '{reservation.Email}',
                '{reservation.PhoneNumber}',
                '{reservation.GroupSize}',
                '{reservation.Date.ToString()}',
                '{reservation.WineArrangement.id}'
            );");
        }
        else
        {
            db.Query($@"INSERT INTO Reservations (id, name, email, phonenumber, groupsize, date_and_time, course_arrangement_id, wine_arrangement_id) VALUES (
                '{reservation.ReservationCode}',
                '{reservation.Name}',
                '{reservation.Email}',
                '{reservation.PhoneNumber}',
                '{reservation.GroupSize}',
                '{reservation.Date.ToString()}',
                '{reservation.CourseArrangement.id}',
                '{reservation.WineArrangement.id}'
            );");

        }

        return true;
        // Reservations.Add(reservation);
    }

    private bool ChooseSeating(Reservation reservation)
    {
        SeatingManager seatingManager = Program.SeatingManager;
        if (reservation.GroupSize == 1)
        {
            List<Seating> availableSeatings = seatingManager.FindAvailableSeating(1, (DateTime)reservation.Date!, "Bar");
            if (availableSeatings.Count > 0)
            {
                reservation.AddSeating(availableSeatings[0]);
                return true;
            }
            else
            {
                availableSeatings = seatingManager.FindAvailableSeating(2, (DateTime)reservation.Date!);
                if (availableSeatings.Count > 0)
                {
                    reservation.AddSeating(availableSeatings[0]);
                    return true;
                }
            }
        }
        if (reservation.GroupSize == 2)
        {
            List<Seating> availableSeatings = seatingManager.FindAvailableSeating(2, (DateTime)reservation.Date!);
            if (availableSeatings.Count > 0)
            {
                reservation.AddSeating(availableSeatings[0]);
                return true;
            }
        }
        if (reservation.GroupSize > 2 && reservation.GroupSize <= 4)
        {
            List<Seating> availableSeatings = seatingManager.FindAvailableSeating(4, (DateTime)reservation.Date!);
            if (availableSeatings.Count > 0)
            {
                reservation.AddSeating(availableSeatings[0]);
                return true;
            }
        }
        if (reservation.GroupSize > 4)
        {
            // Get tables with 4 seats
            List<Seating> availableSeatings = seatingManager.FindAvailableSeating(4, (DateTime)reservation.Date!);
            int groupSize = reservation.GroupSize;
            List<Seating> tempSeatings = new List<Seating>();
            bool first = true;
            while (groupSize > 0)
            {
                if (availableSeatings.Count > 0)
                {
                    if (groupSize == 3)
                    {
                        tempSeatings.Add(availableSeatings[0]);
                        availableSeatings.RemoveAt(0);
                        groupSize -= 3;
                    }
                    else if (first)
                    {
                        tempSeatings.Add(availableSeatings[0]);
                        availableSeatings.RemoveAt(0);
                        groupSize -= 3;
                        first = false;
                    }
                    else
                    {
                        tempSeatings.Add(availableSeatings[0]);
                        availableSeatings.RemoveAt(0);
                        groupSize -= 2;
                    }
                }
                else
                {
                    break;
                }
            }
            if (groupSize <= 0)
            {
                foreach (Seating seating in tempSeatings)
                {
                    reservation.AddSeating(seating);
                }
                return true;
            }
        }
        return false;
    }

    private void LoadReservations()
    {
        DatabaseManager db = Program.DatabaseManager;
        DataTable dt = db.GetTable("Reservations");
        // Dictionary<Reservation, Seating> reservationSeatings = getReservationSeatings();

        foreach (DataRow row in dt.Rows)
        {
            string reservationCode = row["id"].ToString();
            string name = row["name"].ToString();
            string email = row["email"].ToString();
            string phoneNumber = row["phonenumber"].ToString();
            int groupSize = int.Parse(row["groupsize"].ToString());
            DateTime date = DateTime.Parse(row["date_and_time"].ToString());
            Reservation reservation = new Reservation(reservationCode, name, email, phoneNumber, groupSize, date, null, null);

            // foreach(KeyValuePair<Reservation, Seating> reservationSeating in reservationSeatings)
            // {
            //     if(reservationSeating.Key.ReservationCode == reservation.ReservationCode)
            //     {
            //         reservation.Seating.Add(reservationSeating.Value);
            //     }
            // }

            if (row["course_arrangement_id"].ToString() != "")
            {
                reservation.CourseArrangement = Program.ArrangementManager.GetArrangement(int.Parse(row["course_arrangement_id"].ToString()));
            }
            if (row["wine_arrangement_id"].ToString() != "")
            {
                reservation.WineArrangement = Program.ArrangementManager.GetArrangement(int.Parse(row["wine_arrangement_id"].ToString()));
            }
            Reservations.Add(reservation);
        }
    }

    private Dictionary<Reservation, Seating> getReservationSeatings()
    {
        DatabaseManager db = Program.DatabaseManager;
        DataTable dt = db.GetTable("Reservations_seatings");
        Dictionary<Reservation, Seating> reservationSeatings = new Dictionary<Reservation, Seating>();
        foreach (DataRow row in dt.Rows)
        {
            string reservationCode = row["reservation_id"].ToString();
            int seatingId = int.Parse(row["seating_id"].ToString());
            Reservation reservation = GetReservation(reservationCode);
            Seating seating = Program.SeatingManager.GetSeating(seatingId);
            reservationSeatings.Add(reservation, seating);
        }
        return reservationSeatings;
    }

    private Reservation GetReservation(string reservationCode)
    {
        foreach (Reservation reservation in Reservations)
        {
            if (reservation.ReservationCode == reservationCode)
            {
                return reservation;
            }
        }
        return null;
    }

    public bool UpdateReservation(Reservation reservation, Reservation originalReservation)
    {
        DatabaseManager db = Program.DatabaseManager;
        DeleteReservation(originalReservation.ReservationCode);
        if (PlaceReservation(reservation)) return true;
        PlaceReservation(originalReservation);
        return false;
    }


    public static Reservation FindReservation(string? GivenResCode, string? GivenResMail)
    {
        Reservation? SpecReservation = Program.ReservationManager.Reservations.Find(r => r.ReservationCode == GivenResCode && r.Email == GivenResMail);
        return SpecReservation;
    }

    public void DeleteReservation(string reservationCode)
    {
        DatabaseManager db = Program.DatabaseManager;
        db.Query($@"DELETE FROM Reservations_seatings
        WHERE reservation_id = '{reservationCode}';");
        db.Query($@"DELETE FROM Reservations
        WHERE id = '{reservationCode}';");
        Reservations.Remove(GetReservation(reservationCode));
    }
}