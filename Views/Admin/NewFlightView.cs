public class NewFlightView : IView
{
    private static readonly NewFlightView instance = new NewFlightView();
    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();
    public Flight? CurrentFlight = new Flight(FlightManager.GetNewestId(), null, null, DateTime.Now, DateTime.Now, null);

    static NewFlightView()
    {
    }

    /// <summary>
    /// Displays the flight editor view.
    /// </summary>
    public void Display()
    {

        List<string> optionsList = new List<string>() {
            $"Vertrek vanaf: {CurrentFlight.Departure.Name}",
            $"Aankomst op: {CurrentFlight.Destination.Name}",
            $"Vliegtuig: {CurrentFlight.Airplane.Name}",
            $"Vertrekdatum: {CurrentFlight.DepartureTime.ToString("dd-MM-yyyy HH:mm")}",
            $"Aankomstdatum: {CurrentFlight.ArrivalTime.ToString("dd-MM-yyyy HH:mm")}",
            "-",
            "Opslaan",
            "Verwijder",
            "-",
            "Terug"
        };
        string[] options = optionsList.ToArray();
        Menu editorMenu;
        if (ViewBag.ContainsKey("MainMenuSelection"))
        {
            editorMenu = new Menu("Vlucht bewerken", options, int.Parse((string)ViewBag["MainMenuSelection"]));
        }
        else
        {
            editorMenu = new Menu("Vlucht bewerken", options);
        }
        int choice = editorMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static NewFlightView Instance
    {
        get
        {
            return instance;
        }
    }

    public void ClearViewBag()
    {
        ViewBag.Clear();
        CurrentFlight = null;
    }
}
