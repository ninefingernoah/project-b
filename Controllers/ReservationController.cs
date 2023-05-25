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
        switch(choice)
        {
            case 5: // Show passengers
                ShowPassengers(reservation);
                break;
        }
    }

    private void ShowPassengers(Reservation reservation)
    {
        ReservationOverviewView.Instance.ViewBag["passengers"] = reservation.Passengers;
        ReservationOverviewView.Instance.DisplayPassengers();
        int choice = int.Parse((string)ReservationOverviewView.Instance.ViewBag["MainMenuSelection"]);
        if(choice > reservation.Passengers.Count)
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
        switch(choice)
        {
            case 5:
                Reservation reservation = ReservationOverviewView.Instance.ViewBag["reservation"] as Reservation;
                Passenger originalPassenger = reservation.Passengers.Find(p => p.Id == passenger.Id);
                if(!originalPassenger.Equals(passenger))
                {
                    int index = reservation.Passengers.IndexOf(originalPassenger);
                    reservation.Passengers[index] = passenger;
                }
                ShowPassengers(reservation);
                break;
        }
    }

    // Dit is niet nodig
    // private void ShowNonUserReservation(Reservation reservation)
    // {
    //     ReservationOverviewView.Instance.ViewBag["Reservation"] = reservation;
        
    // }

}