public class ReservationOverviewView : IView {
    private static readonly ReservationOverviewView instance = new ReservationOverviewView();
    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();

    static ReservationOverviewView() {
    }
    private ReservationOverviewView() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static ReservationOverviewView Instance {
        get {
            return instance;
        }
    }

    
    /// <summary>
    /// Displays the login menu.
    /// </summary>
    public void Display() {
        // TODO: Is deze manier een beetje oke?
        Reservation reservation = (Reservation)ViewBag["reservation"];
        string email = reservation.Email != null ? reservation.Email : reservation.User.Email;
        List<string> optionsList = new List<string>() {
            $"Boekingsnummer: {reservation.ReservationNumber}",
            $"Email: {email}", 
            $"Vertrek van: {reservation.OutwardFlight}",
            $"Vertrek naar: {reservation.InwardFlight}",
            $"Vertrek op: {reservation.OutwardFlight.DepartureTime}",
            $"Aankomst op: {reservation.InwardFlight.ArrivalTime}",
            $"Passagiers: {reservation.Passengers.Count} personen",
            "-",
            "Wijzigingen Opslaan",
            "Ga terug"
        };
        string[] options = optionsList.ToArray();
        Menu overviewmenu;
        if (ViewBag.ContainsKey("MainMenuSelection"))
        {
            overviewmenu = new Menu($"Reservering #{reservation.ReservationNumber}", options, int.Parse((string)ViewBag["MainMenuSelection"]));
        } else
        {
            overviewmenu = new Menu($"Reservering #{reservation.ReservationNumber}", options);
        }
        int choice = overviewmenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    private void PopulateViewBag() {
        
    }

    public void ClearViewBag() {
        ViewBag.Clear();
    }

    public void DisplayPassengers()
    {
        List<Passenger> passengers = (List<Passenger>)ViewBag["passengers"];
        List<string> optionsList = new List<string>();
        foreach (Passenger passenger in passengers)
        {
            optionsList.Add(passenger.ToString());
        }
        optionsList.Add("-");
        optionsList.Add("Ga terug");
        string[] options = optionsList.ToArray();
        Menu overviewmenu = new Menu("Passagiers", options);
        int choice = overviewmenu.Run();
        ViewBag["PassengerSelection"] = choice.ToString();
    }
}
