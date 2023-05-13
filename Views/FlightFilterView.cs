public class FlightFilterView : IView {
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly FlightFilterView instance = new FlightFilterView();

    static FlightFilterView() {
    }
    private FlightFilterView() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static FlightFilterView Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Displays filter options and sends the user input to the controller.
    /// </summary>
    public void Display() {
        Menu menu = new Menu("Filter vluchten", new string[] {
            "Datum van vertrek: " + (!ViewBag.ContainsKey("departuredate") ? "<vul in>" : ViewBag["departuredate"]),
            "Vertrek van: " + (!ViewBag.ContainsKey("departure") ? "<vul in>" : ViewBag["departure"]),
            "Aankomst in: " + (!ViewBag.ContainsKey("arrival") ? "<vul in>" : ViewBag["arrival"]),
            "Zoek",
            "Terug"
        });

        ViewBag["FlightFilterSelection"] = menu.Run().ToString();
    }
}