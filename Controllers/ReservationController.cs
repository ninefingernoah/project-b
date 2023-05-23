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
    
    public void AskReservation()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw reserveringscode in: ");
        int reservationCode;
        try
        {
            reservationCode = int.Parse(menu.Run()!);
        }
        catch (Exception)
        {
            ConsoleUtils.Error("De ingevoerde reserveringscode is ongeldig.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        Reservation reservation = ReservationManager.GetReservation(reservationCode);
        if (reservation == null)
        {
            ConsoleUtils.Error("De ingevoerde reserveringscode is ongeldig.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        StringInputMenu emailMenu = new StringInputMenu("Vul uw emailadres in: ");
        string email = emailMenu.Run()!;
        if (reservation.User.Email != email && reservation.Email != email)
        {
            ConsoleUtils.Error("Het ingevoerde emailadres is ongeldig.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        ShowReservationToReservationOwner(reservation);
    }

    public void ShowReservationToReservationOwner(Reservation reservation)
    {
        if(reservation.User == null)
        {
            ShowNonUserReservation(reservation);
        }
        
    }

    private void ShowNonUserReservation(Reservation reservation)
    {
        ReservationOverviewView.Instance.ViewBag["Reservation"] = reservation;
        
    }

}