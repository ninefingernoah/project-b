public sealed class ReservationController
{
    private static readonly ReservationController instance = new ReservationController();

    static ReservationController()
    {
    }
    private ReservationController()
    {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static ReservationController Instance
    {
        get
        {
            return instance;
        }
    }

    // TODO: Get Seat selection and prices
    public void Start()
    {
        // get flight
        var flight = FlightController.Instance.GetFlightType();

        // get passengers
        List<Passenger> passengers = GetPassengerAmountInfo();

        // see TODO
        List<Seat> seats = new List<Seat>();
        seats.Add(new Seat("1", 1));
        seats.Add(new Seat("2", 1));
        double Price = 100;

        DisplayData(flight, passengers, seats);

        // check if logged in
        User? user;
        if (UserManager.GetCurrentUser() != null)
        {
            user = UserManager.GetCurrentUser();
        }
        else
        {
            user = null;
        }

        Reservation res = new Reservation("1234", flight, user, seats, passengers, Price, DateTime.Now);
        ReservationManager.MakeReservation(res);
    }

    public List<Passenger> GetPassengerAmountInfo()
    {
        IntInputMenu menu = new IntInputMenu("Met hoeveel reizgers bent u?");
        int? amount = menu.Run();
        Console.WriteLine(amount);
        List<Passenger> passengers = new List<Passenger>();
        if (amount != null && amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                passengers.Add(PassengerController.Instance.NewPassenger());
            }
        }

        return passengers;
    }

    public void DisplayData(Flight flight, List<Passenger> passengers, List<Seat> seats)
    {
        //Console.Clear();
        Console.WriteLine("Vlucht informatie:");
        Console.WriteLine(flight.ToString());
        Console.WriteLine("Reizigers:");
        foreach (var passenger in passengers)
        {
            Console.WriteLine(passenger.ToString());
        }
        Console.WriteLine("Stoelen:");
        foreach (var seat in seats)
        {
            Console.WriteLine(seat.ToString());
        }
    }

    public void UserCancelReservation(Reservation ress)
    {
        if (ConsoleUtils.Confirm("Weet u zeker dat u de reservering wilt annuleren?"))
        {
            ReservationManager.DeleteReservation(ress);
            ConsoleUtils.Success("U reservering is geannuleerd");
        }
    }
}
