/// <summary>
/// The view for the flight menu. Singleton.
/// </summary>
public class FlightView {
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static readonly FlightView instance = new FlightView();

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();

    static FlightView() {
    }
    private FlightView() {
    }

    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static FlightView Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Displays the flight and handles the user input.
    /// </summary>
    /// <param name="flight">The flight to display.</param>
    public void Display(Flight flight) {
        string[] options = { "Boek vlucht", "Terug" };
        if (UserManager.IsLoggedIn() && UserManager.GetCurrentUser != null && UserManager.GetCurrentUser()!.IsAdmin()) {
            options = new string[] { "Kies vlucht", "Bewerk vlucht", "Verwijder vlucht",  "Terug" };
        }
        Menu menu = new Menu(flight.ToString(), options);
        int selection = menu.Run();
        ViewBag["FlightViewSelection"] = selection.ToString();
    }
}