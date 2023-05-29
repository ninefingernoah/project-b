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
        if (choice >= 0 && choice <= 4)
        {
            ShowReservationToReservationOwner(reservation);
            return;
        }
        switch (choice)
        {
            case 5: // Show passengers
                ShowPassengers(reservation);
                break;
            case 7:
                ReservationManager.UpdateReservation(reservation);
                MainMenuController.Instance.ShowMainMenu();
                break;
            case 8:
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
        if(!passenger.CanChangeName())
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
        if(CalcUtils.DeltaString(firstName, passenger.FirstName) > 2)
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
        if(!passenger.CanChangeName())
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
        if(CalcUtils.DeltaString(lastName, passenger.LastName) > 2)
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