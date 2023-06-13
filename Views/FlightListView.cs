/// <summary>
/// View for displaying a list of flights. Singleton.
/// </summary>
public class FlightListView {
    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();

    /// <summary>
    /// Singleton instance
    /// </summary>
    private static readonly FlightListView instance = new FlightListView();

    static FlightListView() {
    }
    private FlightListView() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static FlightListView Instance {
        get {
            return instance;
        }
    }
    
    /// <summary>
    /// Displays all flights in a list and handles the user input.
    /// </summary>
    /// <param name="flights">The flights to display.</param>
    public void Display(List<Flight> flights) {
        List<string> options = flights.Select(f => f.ToString()).ToList();
        options.Add("Terug");
        Menu menu = new Menu(flights.Count + " gevonden vluchten", options.ToArray());
        int selection = menu.Run();
        ViewBag["FlightListSelection"] = selection.ToString();
    }
}