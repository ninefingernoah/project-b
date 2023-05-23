public class FlightView {
    private static readonly FlightView instance = new FlightView();
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();

    static FlightView() {
    }
    private FlightView() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
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
        string[] options = { "Reserveer stoelen", "Terug" };
        if (UserManager.IsLoggedIn() && UserManager.GetCurrentUser().IsAdmin()) {
            options = new string[] { "Reserveer stoelen", "Bewerk vlucht [ADMIN]", "Verwijder vlucht [ADMIN]",  "Terug" };
        }
        Menu menu = new Menu(flight.ToString(), options);
        int selection = menu.Run();
        ViewBag["FlightViewSelection"] = selection.ToString();
    }
}