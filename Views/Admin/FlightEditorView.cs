public class FlightEditorView : IView {
    private static readonly FlightEditorView instance = new FlightEditorView();
    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();
    public Flight? CurrentFlight;

    static FlightEditorView() {
    }

    /// <summary>
    /// Displays the flight editor view.
    /// </summary>
    public void Display() {
        if(CurrentFlight == null)
        {
            throw new Exception("Flight is null");
        }

        List<string> optionsList = new List<string>() {
            $"Vertrek vanaf: {CurrentFlight.Departure.Name}",
            $"Aankomst op: {CurrentFlight.Destination.Name}",
            $"Vliegtuig: {CurrentFlight.Airplane.Name}",
            $"Vertrekdatum: {CurrentFlight.DepartureTime.ToString("dd-MM-yyyy HH:mm")}",
            $"Aankomstdatum: {CurrentFlight.ArrivalTime.ToString("dd-MM-yyyy HH:mm")}",
            "-",
            "Verwijder",
            "-",
            "Terug"
        };
        string[] options = optionsList.ToArray();
        Menu editorMenu = new Menu("Vlucht bewerken", options);
        int choice = editorMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static FlightEditorView Instance {
        get {
            return instance;
        }
    }

    public void PopulateViewBag(Flight flight)
    {
        if(CurrentFlight == null)
            CurrentFlight = new Flight(flight);
    }

    public void ClearViewBag() {
        ViewBag.Clear();
        CurrentFlight = null;
    }
}
