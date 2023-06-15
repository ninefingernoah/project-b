/// <summary>
/// The controller responsible for handling the reservations.
/// </summary>
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

    /// <summary>
    /// Shows the menu for creating a new reservation.
    /// </summary>
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
            Flight? inwardFlight;
            try
            {
                inwardFlight = flights[1];
            }
            catch (System.Exception)
            {

                inwardFlight = null;
            }

            // get passengers
            List<Passenger>? passengers = new();
            while(passengers != null && passengers.Count < 1 || passengers.Count > 10)
            {
                passengers = GetPassengerAmountInfo();
                if (passengers.Count > 10)
                {
                    ConsoleUtils.Error("U kunt maximaal 10 passagiers per reservering toevoegen.");
                }
                else if (passengers.Count < 1)
                {
                    ConsoleUtils.Error("U moet minimaal 1 passagier toevoegen.");
                }
            }
            
            if (passengers == null)
            {
                MainMenuController.Instance.ShowMainMenu();
                return;
            }

            // check if logged in
            User? user;
            string? email;
            if (UserManager.GetCurrentUser() != null)
            {
                user = UserManager.GetCurrentUser();
                email = user!.Email; // This cannot be null here.

            }
            else
            {
                user = null;
                // I made it in such a way that the user is forced to enter an email. It was the easiest way.
                StringInputMenu emailMenu = new StringInputMenu("Vul het emailadres in: ", false);
                email = emailMenu.Run();
                while (email == null || !StringUtils.CheckValidEmail(email))
                {
                    ConsoleUtils.Error("Het ingevoerde emailadres is ongeldig.");
                    email = emailMenu.Run();
                }
            }
            string reservationCode = ReservationManager.GetNewReservationCode();
            res = new Reservation(reservationCode, outwardFlight, inwardFlight, user, email, passengers, 0, DateTime.Now);

            // ask if they want to change seats
            //TODO: fiks prijzen (4 euro wordt niet toegevoegd)
            if (ConsoleUtils.Confirm("Wilt u stoelen kiezen? (LET OP: Stoelen kiezen kost 4 euro extra per stoel, buiten de kosten van de stoel zelf)"))
            {
                // show seat selection menu
                int kostenPerStoel = 4;
                SeatController.Instance.ShowSeatSelection(res, kostenPerStoel);
            }

            if (ReservationManager.MakeReservation(res))
            {
                if (DisplayData(res))
                {
                    correct = true;
                    ConsoleUtils.Success("Uw reservering is succesvol geplaatst. Uw reserveringscode is: " + res.ReservationNumber + ".\nU kunt deze code gebruiken om uw reservering te bekijken of te wijzigen.");
                }
                else
                {
                    if (ConsoleUtils.Confirm("Wilt u de huidige reservering bewerken? (Zo niet keert u terug naar het hoofdmenu)"))
                    {
                        correct = true;
                        ShowReservationToReservationOwner(res);
                    }
                    else
                    {
                        ReservationManager.DeleteReservation(res);
                        MainMenuController.Instance.ShowMainMenu();
                        return;
                    }
                }

            }
        } while (!correct);
        MainMenuController.Instance.ShowMainMenu();
    }

    /// <summary>
    /// Gathers the flights the user wants to book.
    /// </summary>
    /// <param name="type">The type of booking the user wants to make. Either is 'enkel' or 'retour'</param>
    /// <returns>A list of flights.</returns>
    private List<Flight> GetFlights(string type)
    {
        if (type == "Enkel")
        {
            FlightListController.Instance.ShowFlightSearchMenu();
            Flight? flight = FlightController.Instance.GetChosenFlight();
            List<Flight> flights = new List<Flight>();
            if (flight != null)
                flights.Add(flight);
            return flights;
        }
        if (type == "Retour")
        {
            FlightListController.Instance.ShowFlightSearchMenu();
            Flight? outwardflight = FlightController.Instance.GetChosenFlight();
            if (outwardflight == null)
            {
                throw new Exception("Outward flight is null");
            }
            Airport retarr = outwardflight.Departure;
            Airport retdep = outwardflight.Destination;
            FlightListController.Instance.ShowFlights(retdep, retarr, outwardflight.ArrivalTime);
            Flight? returnflight = FlightController.Instance.GetChosenFlight();
            List<Flight> flights = new List<Flight>();
            flights.Add(outwardflight);
            if (returnflight == null)
            {
                throw new Exception("Return flight is null while return booking.");
            }
            flights.Add(returnflight);
            return flights;
        }
        return new List<Flight>();
    }

    /// <summary>
    /// Gets the type of booking the user wants to make.
    /// </summary>
    /// <returns>The type of booking the user wants to make.</returns>
    // TODO: Refactor BookingType to be an enum.
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

    /// <summary>
    /// Gets the passengers the user wants to book for.
    /// </summary>
    /// <returns>A list of passengers.</returns>
    public List<Passenger>? GetPassengerAmountInfo()
    {
        IntInputMenu menu = new IntInputMenu("Met hoeveel reizigers bent u?");
        int? amount = menu.Run();
        if (amount == null) return new List<Passenger>();
        if (amount <= 0 || amount > 10)
        {
            return new List<Passenger>();
        }
        List<Passenger> passengers = new List<Passenger>();
        for (int i = 0; i < amount; i++)
        {
            Passenger? newPassenger = PassengerController.Instance.NewPassenger();
            if (newPassenger == null)
            {
                return new List<Passenger>();
            }
            passengers.Add(newPassenger);
        }
        return passengers;
    }

    /// <summary>
    /// Displays the reservation data to the user.
    /// </summary>
    /// <param name="ress">The reservation to display.</param>
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
            Console.WriteLine("\nVlucht terug:");
            Console.WriteLine(ress.InwardFlight.ToString());
            Console.WriteLine("Stoelen:");
            if (ress.InwardSeats != null)
            {
                foreach (var seat in ress.InwardSeats)
                {
                    Console.WriteLine(seat.Number);
                }
            }
        }

        Console.WriteLine("\nReizigers:");
        foreach (Passenger passenger in ress.Passengers)
        {
            Console.WriteLine(passenger.ToString());
        }
        Console.WriteLine();
        Console.WriteLine($"Totale prijs: {ress.Price} euro");

        return ConsoleUtils.Confirm("Gaat u akkoord met deze reservering?", false);

    }

    /// <summary>
    /// Asks the user if they want to cancel their reservation. Cancels the reservation if they do.
    /// </summary>
    /// <param name="ress">The reservation to cancel.</param>
    public void UserCancelReservation(Reservation ress)
    {
        if (ConsoleUtils.Confirm("Weet u zeker dat u de reservering wilt annuleren?"))
        {
            ReservationManager.DeleteReservation(ress);
            ConsoleUtils.Success("U reservering is geannuleerd");
        }
    }

    /// <summary>
    /// Asks the user for their reservation code and email and shows the reservation if it exists.
    /// </summary>
    public void AskReservation()
    {
        StringInputMenu menu = new StringInputMenu("Vul uw reserveringscode in: ");
        string? reservationCode = menu.Run();
        if (reservationCode == null || reservationCode.ToLower() == "terug")
        {
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        Reservation? reservation = ReservationManager.GetReservation(reservationCode);
        if (reservation == null)
        {
            ConsoleUtils.Error("De ingevoerde reserveringscode is ongeldig.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        StringInputMenu emailMenu = new StringInputMenu("Vul uw emailadres in: ");
        string email = emailMenu.Run()!;
        if (email == null || email.ToLower() == "terug")
        {
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        string emailOnReservation = reservation.User == null ? reservation.Email! : reservation.User.Email;
        if (emailOnReservation != email)
        {
            ConsoleUtils.Error("Het ingevoerde emailadres is ongeldig.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        ShowReservationToReservationOwner(reservation);
    }

    /// <summary>
    /// Shows the reservation to the owner of the reservation.
    /// </summary>
    /// <param name="reservation">The reservation to show.</param>
    public void ShowReservationToReservationOwner(Reservation reservation)
    {
        ReservationOverviewView.Instance.ViewBag["reservation"] = reservation;
        ReservationOverviewView.Instance.Display();
        int choice = int.Parse((string)ReservationOverviewView.Instance.ViewBag["MainMenuSelection"]);
        if (choice == 0 || (choice > 1 && choice <= 3))
        {
            ShowReservationToReservationOwner(reservation);
            return;
        }
        switch (choice)
        {
            case 1:
                StringInputMenu emailMenu = new StringInputMenu("Vul uw emailadres in: ");
                string email = emailMenu.Run()!;
                if (email == null || email.ToLower() == "terug")
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
            case 4: // Show passengers
                ShowPassengers(reservation);
                break;
            case 5:
                SeatController.Instance.ShowSeatSelection(reservation, 4);
                ShowReservationToReservationOwner(reservation);
                break;
            case 7:
                ReservationManager.UpdateReservation(reservation);
                ConsoleUtils.Success($"Uw reservering: {reservation.ReservationNumber} is succesvol gewijzigd.");
                MainMenuController.Instance.ShowMainMenu();
                break;
            case 8:
                UserCancelReservation(reservation);
                MainMenuController.Instance.ShowMainMenu();
                break;
            case 9:
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }

    /// <summary>
    /// Shows the passengers of the reservation.
    /// </summary>
    /// <param name="reservation">The reservation to show the passengers of.</param>
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

    /// <summary>
    /// Shows the passenger editor.
    /// </summary>
    /// <param name="passenger">The passenger to edit.</param>
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
                if(reservation == null)
                {
                    ConsoleUtils.Error("Er is iets fout gegaan bij het opslaan van de passagier.");
                    ShowPassengerEditor(passenger);
                    return;
                }
                Passenger? originalPassenger = reservation.Passengers.Find(p => p.Id == passenger.Id);
                if (reservation != null && originalPassenger != null && !originalPassenger.Equals(passenger))
                {
                    int index = reservation.Passengers.IndexOf(originalPassenger);
                    reservation.Passengers[index] = passenger;
                }
                ShowPassengers(reservation!);
                break;
        }
    }

    /// <summary>
    /// Changes the first name of the passenger.
    /// </summary>
    /// <param name="passenger">The passenger to change the first name of.</param>
    private void ChangeFirstName(Passenger passenger)
    {
        if (!passenger.CanChangeName())
        {
            ConsoleUtils.Error("Deze passagier kan zijn/haar naam niet meer veranderen. Neem contact op met de klantenservice.");
            ShowPassengerEditor(passenger);
            return;
        }
        StringInputMenu firstNameMenu = new StringInputMenu("Vul de voornaam in: ");
        string? firstName = firstNameMenu.Run();
        if (firstName == null || firstName.ToLower() == "terug")
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

    /// <summary>
    /// Changes the last name of the passenger.
    /// </summary>
    /// <param name="passenger">The passenger to change the last name of.</param>
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

}