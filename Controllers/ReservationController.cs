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

    // TODO: Get Seat selection and prices, Push to database.
    public void Start()
    {
        var flight = FlightController.Instance.GetFlightType();
        List<Passenger> passengers = GetPassengerAmountInfo();

        // see TODO
        List<Seat> seats = new List<Seat>();
        seats.Add(new Seat("1", 1));
        seats.Add(new Seat("2", 1));
        double Price = 100;

        DisplayData(flight, passengers, seats);

        Reservation res = new Reservation("1234", flight, UserManager.GetCurrentUser(), seats, passengers, Price, DateTime.Now);

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
                passengers.Add(PassengerController.Instance.NewPassenger(i));
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
}
