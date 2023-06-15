/// <summary>
/// The view for the reservation overview. Singleton.
/// </summary>
public class ReservationOverviewView : IView {
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static readonly ReservationOverviewView instance = new ReservationOverviewView();

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();

    static ReservationOverviewView() {
    }
    private ReservationOverviewView() {
    }

    /// <summary>
    /// The getter for the singleton instance.
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
        // TODO: Stoelen wijzigen
        Reservation reservation = (Reservation)ViewBag["reservation"];
        string email = reservation.Email != null ? reservation.Email : reservation.User.Email;
        List<string> optionsList = new List<string>();
        if (reservation.InwardFlight != null){
            optionsList = new List<string>() {
            $"Boekingsnummer: {reservation.ReservationNumber}",
            $"Email: {email}", 
            $"\nVlucht: {reservation.OutwardFlight}",
            $"Retourvlucht: {reservation.InwardFlight}",
            $"Passagiers: {reservation.Passengers.Count} personen",
            $"Stoelselectie", //TODO: prijsweergave
            "-",
            "Wijzigingen opslaan",
            "Annuleer reservering",
            "Terug"
        };
        }
        else {
            optionsList = new List<string>() {
            $"Boekingsnummer: {reservation.ReservationNumber}",
            $"Email: {email}", 
            $"Vlucht: {reservation.OutwardFlight}",
            $"Retourvlucht: Geen",
            $"\nPassagiers: {reservation.Passengers.Count} personen",
            $"Stoelselectie",
            "-",
            "Wijzigingen opslaan",
            "Annuleer reservering",
            "Terug"
        };
        }
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

    /// <summary>
    /// Resets the viewbag to its default values.
    /// </summary>
    private void PopulateViewBag() {
        
    }

    /// <summary>
    /// Clears the viewbag.
    /// </summary>
    public void ClearViewBag() {
        ViewBag.Clear();
    }

    /// <summary>
    /// Displays the passengers in the reservation.
    /// </summary>
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
