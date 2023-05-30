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
        seats.Add(new Seat("1", "1"));
        seats.Add(new Seat("2", "1"));
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
        Console.Clear();
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

    public void AskReservation()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw reserveringscode in: ");
        string reservationCode = menu.Run()!;
        if (reservationCode.ToLower() == "terug")
        {
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
        if (email.ToLower() == "terug")
        {
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
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
        ReservationOverviewView.Instance.ViewBag["reservation"] = reservation;
        ReservationOverviewView.Instance.Display();
        int choice = int.Parse((string)ReservationOverviewView.Instance.ViewBag["MainMenuSelection"]);
        if (choice == 0 || (choice > 1 && choice <= 5))
        {
            ShowReservationToReservationOwner(reservation);
            return;
        }
        switch (choice)
        {
            case 1:
                StringInputMenu emailMenu = new StringInputMenu("Vul uw emailadres in: ");
                string email = emailMenu.Run()!;
                if (email.ToLower() == "terug")
                {
                    ShowReservationToReservationOwner(reservation);
                    return;
                }
                if (!StringUtils.CheckValidEmail(email))
                {
                    ConsoleUtils.Error("Het ingevoerde emailadres is ongeldig.");
                    ShowReservationToReservationOwner(reservation);
                    return;
                }
                if (reservation.Email != null)
                    reservation.Email = email;
                if (reservation.User != null)
                {
                    reservation.User.Email = email;
                }
                ShowReservationToReservationOwner(reservation);
                break;
            case 6: // Show passengers
                ShowPassengers(reservation);
                break;
            case 8:
                ReservationManager.UpdateReservation(reservation);
                ConsoleUtils.Success("Uw reservering is succesvol gewijzigd.");
                MainMenuController.Instance.ShowMainMenu();
                break;
            case 9:
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }

    private void ShowPassengers(Reservation reservation)
    {
        ReservationOverviewView.Instance.ViewBag["passengers"] = reservation.Passengers;
        ReservationOverviewView.Instance.DisplayPassengers();
        int choice = int.Parse((string)ReservationOverviewView.Instance.ViewBag["PassengerSelection"]);
        if (choice > reservation.Passengers.Count)
        {
            ShowReservationToReservationOwner(reservation);
            return;
        }
        Passenger passenger = reservation.Passengers[choice];
        ShowPassengerEditor(passenger); // TODO: Show passenger
    }

    public void ShowPassengerEditor(Passenger passenger)
    {
        PassengerOverviewView.Instance.PopulateViewBag(passenger);
        PassengerOverviewView.Instance.Display();
        int choice = int.Parse((string)PassengerOverviewView.Instance.ViewBag["MainMenuSelection"]);
        switch (choice)
        {
            case 0: // Voornaam
                ChangeFirstName(passenger);
                break;
            case 1: // Achternaam
                ChangeLastName(passenger);
                break;
            case 2: // Email
                StringInputMenu emailMenu = new StringInputMenu("Vul het emailadres in: ");
                string email = emailMenu.Run()!;
                if (email.ToLower() == "terug")
                {
                    ShowPassengerEditor(passenger);
                    return;
                }
                passenger.Email = email;
                ShowPassengerEditor(passenger);
                break;
            case 3: // Documentnummer
                StringInputMenu documentNumberMenu = new StringInputMenu("Vul het documentnummer in: ");
                string documentNumber = documentNumberMenu.Run()!;
                if (documentNumber.ToLower() == "terug")
                {
                    ShowPassengerEditor(passenger);
                    return;
                }
                passenger.DocumentNumber = documentNumber;
                ShowPassengerEditor(passenger);
                break;
            case 4: // Geboortedatum
                StringInputMenu birthDateMenu = new StringInputMenu("Vul de geboortedatum in: ");
                string birthDate = birthDateMenu.Run()!;
                DateTime birthDateDateTime;
                if (birthDate.ToLower() == "terug")
                {
                    ShowPassengerEditor(passenger);
                    return;
                }
                try
                {
                    birthDateDateTime = DateTime.Parse(birthDate);
                }
                catch (Exception)
                {
                    ConsoleUtils.Error("De ingevoerde geboortedatum is ongeldig.");
                    ShowPassengerEditor(passenger);
                    return;
                }
                if (birthDateDateTime > (DateTime.Now - TimeSpan.FromDays(60)))
                {
                    ConsoleUtils.Error("De ingevoerde geboortedatum is ongeldig.");
                    ShowPassengerEditor(passenger);
                    return;
                }
                passenger.BirthDate = birthDateDateTime;
                ShowPassengerEditor(passenger);
                break;
            case 5: // Address
                AddressController.Instance.ShowAddressEditingMenu(passenger);
                break;
            case 7:
                Reservation? reservation = ReservationOverviewView.Instance.ViewBag["reservation"] as Reservation;
                Passenger originalPassenger = reservation.Passengers.Find(p => p.Id == passenger.Id);
                if (!originalPassenger.Equals(passenger))
                {
                    int index = reservation.Passengers.IndexOf(originalPassenger);
                    reservation.Passengers[index] = passenger;
                }
                ShowPassengers(reservation);
                break;
        }
    }

    private void ChangeFirstName(Passenger passenger)
    {
        if (!passenger.CanChangeName())
        {
            ConsoleUtils.Error("Deze passagier kan zijn/haar naam niet meer veranderen. Neem contact op met de klantenservice.");
            ShowPassengerEditor(passenger);
            return;
        }
        StringInputMenu firstNameMenu = new StringInputMenu("Vul de voornaam in: ");
        string firstName = firstNameMenu.Run()!;
        if (firstName.ToLower() == "terug")
        {
            ShowPassengerEditor(passenger);
            return;
        }
        if (CalcUtils.DeltaString(firstName, passenger.FirstName) > 2)
        {
            ConsoleUtils.Error("De ingevoerde voornaam wijkt te veel af van de huidige voornaam.");
            ShowPassengerEditor(passenger);
            return;
        }
        passenger.FirstName = firstName;
        // Het locken pas doen bij het opslaan van de naam.
        // passenger.LockName();
        ShowPassengerEditor(passenger);
    }

    private void ChangeLastName(Passenger passenger)
    {
        if (!passenger.CanChangeName())
        {
            ConsoleUtils.Error("Deze passagier kan zijn/haar naam niet meer veranderen. Neem contact op met de klantenservice.");
            ShowPassengerEditor(passenger);
            return;
        }
        StringInputMenu lastNameMenu = new StringInputMenu("Vul de achternaam in: ");
        string lastName = lastNameMenu.Run()!;
        if (lastName.ToLower() == "terug")
        {
            ShowPassengerEditor(passenger);
            return;
        }
        if (CalcUtils.DeltaString(lastName, passenger.LastName) > 2)
        {
            ConsoleUtils.Error("De ingevoerde achternaam wijkt te veel af van de huidige achternaam.");
            ShowPassengerEditor(passenger);
            return;
        }
        passenger.LastName = lastName;
        // passenger.LockName();
        ShowPassengerEditor(passenger);
    }

    // Dit is niet nodig
    // private void ShowNonUserReservation(Reservation reservation)
    // {
    //     ReservationOverviewView.Instance.ViewBag["Reservation"] = reservation;

    // }

}