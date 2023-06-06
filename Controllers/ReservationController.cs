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

    public void ShowBookingMenu()
    {
        bool correct = true;
        Reservation res;
        do
        {
            // get type of booking
            string type = GetBookingType();

            List<Flight> flights = GetFlights(type);
            if (flights == null)
            {
                MainMenuController.Instance.ShowMainMenu();
                return;
            }
            Flight outwardFlight = flights[0];
            Flight inwardFlight;
            try
            {
                inwardFlight = flights[1];
            }
            catch (System.Exception)
            {

                inwardFlight = null;
            }

            // get passengers
            List<Passenger>? passengers = GetPassengerAmountInfo();
            if (passengers == null)
            {
                MainMenuController.Instance.ShowMainMenu();
                return;
            }

            // check if logged in
            User? user;
            string email;
            if (UserManager.GetCurrentUser() != null)
            {
                user = UserManager.GetCurrentUser();
                email = user.Email;

            }
            else
            {
                user = null;
                StringInputMenu emailMenu = new StringInputMenu("Vul het emailadres in: ");
                email = emailMenu.Run()!;
                if (email.ToLower() == "terug")
                {
                    //TODO: return to passenger info
                    return;
                }
            }
            int reservationCode = ReservationManager.GetReservationCode();
            res = new Reservation(reservationCode, outwardFlight, inwardFlight, user, email, passengers, 0, DateTime.Now);
            
            // ask if they want to change seats
            if (ConsoleUtils.Confirm("Wilt u stoelen kiezen? (LET OP: Stoelen kiezen kost €4 extra per stoel, buiten de kosten van de stoel zelf)"))
            {
                // show seat selection menu
                int kostenPerStoel = 4;
                SeatController.Instance.ShowSeatSelection(res, kostenPerStoel);
            }

            if (ReservationManager.MakeReservation(res))
            {
                if (DisplayData(res)) {
                    correct = true;
                }
                else {
                    if(ConsoleUtils.Confirm("Wilt u de huidige reservering bewerken? (Zo niet keert u terug naar het hoofdmenu)"))
                    {
                        correct = true;
                        ShowReservationToReservationOwner(res); // TODO: add seat selection to editor //TODO: maybe also add changing the flights if its more than 30 days away uwu
                    }
                    else
                    {
                        ReservationManager.DeleteReservation(res);
                        MainMenuController.Instance.ShowMainMenu();
                        return;
                    }
                }
                
            }
        } while (!correct); //TODO: remove loop
        MainMenuController.Instance.ShowMainMenu();
    }

    private List<Flight> GetFlights(string type)
    {
        if (type == "Enkel")
        {
            FlightListController.Instance.ShowFlightSearchMenu();
            Flight flight = FlightController.Instance.GetChosenFlight();
            List<Flight> flights = new List<Flight>();
            flights.Add(flight);
            return flights;
        }
        if (type == "Retour")
        {
            FlightListController.Instance.ShowFlightSearchMenu();
            Flight outwardflight = FlightController.Instance.GetChosenFlight();
            Airport retarr = outwardflight.Departure;
            Airport retdep = outwardflight.Destination;
            FlightListController.Instance.ShowFlights(retdep, retarr);
            Flight returnflight = FlightController.Instance.GetChosenFlight();
            List<Flight> flights = new List<Flight>();
            flights.Add(outwardflight);
            flights.Add(returnflight);
            return flights;
        }
        return null;
    }

    private Airport GetAirport(string prompt)
    {
        List<Airport> airports = AirportManager.GetAirports();
        List<string> options = new List<string>();
        foreach (var airport in airports)
        {
            options.Add(airport.ToString());
        }
        options.Add("-");
        options.Add("Annuleer");
        Menu menu = new Menu(prompt, options.ToArray());
        int choice = menu.Run();
        if (choice == airports.Count + 1)
        {
            return null;
        }
        return airports[choice];
    }

    private string GetBookingType()
    {
        List<string> options = new List<string>();
        options.Add("Enkele reis");
        options.Add("Retour");
        options.Add("Terug");
        MenuView.Instance.Display("Selecteer het type reis", options);
        int choice = MenuView.Instance.LastChoice;
        switch (choice)
        {
            case 0:
                return "Enkel";
            case 1:
                return "Retour";
            case 2:
                MainMenuController.Instance.ShowMainMenu();
                return "";
            default:
                return "";
        }
    }

    public List<Passenger> GetPassengerAmountInfo()
    {
        IntInputMenu menu = new IntInputMenu("Met hoeveel reizgers bent u?");
        int? amount = menu.Run();
        if (amount == null)
        {
            return null;
        }
        List<Passenger> passengers = new List<Passenger>();
        if (amount != null && amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Passenger newPassenger = PassengerController.Instance.NewPassenger();
                if (newPassenger == null)
                {
                    return null;
                }
                passengers.Add(newPassenger);
                // passengers.Add(new Passenger(1, null, null, null, null, null, null));
            }
        }

        return passengers;
    }

    public bool DisplayData(Reservation ress)
    {
        Console.Clear();
        Console.WriteLine("Vlucht informatie");
        Console.WriteLine("------------------");
        Console.WriteLine("Vlucht heen:");
        Console.WriteLine(ress.OutwardFlight.ToString());
        Console.WriteLine("Stoelen:");
        foreach (var seat in ress.OutwardSeats)
        {
            Console.WriteLine(seat.Number);
        }
        if (ress.InwardFlight != null)
        {
            Console.WriteLine("Vlucht terug:");
            Console.WriteLine(ress.InwardFlight.ToString());
            Console.WriteLine("Stoelen:");
            foreach (var seat in ress.InwardSeats)
            {
                Console.WriteLine(seat.Number);
            }
        }

        Console.WriteLine("Reizigers:");
        foreach (Passenger passenger in ress.Passengers)
        {
            Console.WriteLine(passenger.ToString());
        }
        Console.WriteLine();
        Console.WriteLine($"Totale prijs: {ress.Price}");

        return ConsoleUtils.Confirm("Gaat u akkoord met deze reservering?", false);

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
                ConsoleUtils.Success($"Uw reservering: {reservation.ReservationNumber} is succesvol gewijzigd.");
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