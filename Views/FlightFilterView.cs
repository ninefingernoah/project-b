public class FlightFilterView : IView {
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly FlightFilterView instance = new FlightFilterView();

    static FlightFilterView() {
    }
    private FlightFilterView() {
        // Set default values for the ViewBag
        ResetViewBag();
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
        // PopulateViewBag();
        Menu menu = new Menu("Filter vluchten", new string[] {
            "Datum van vertrek: " + ViewBag["departureDate"],
            "Vertrek van: " + (ViewBag["departureid"] == "0" ? "" : AirportManager.GetAirport(int.Parse(ViewBag["departureid"])).Name),
            "Aankomst in: " + (ViewBag["destinationid"] == "0" ? "" : AirportManager.GetAirport(int.Parse(ViewBag["destinationid"])).Name),
            "-",
            "Reset filters",
            "Zoek",
            "Terug"
        });

        ViewBag["FlightFilterSelection"] = menu.Run().ToString();
    }

    /// <summary>
    /// Populates the ViewBag with default values if they don't exist yet.
    /// </summary>
    public void PopulateViewBag() {
        // deprecated
    }

    public void ResetViewBag() {
        ViewBag.Clear();
        ViewBag.Add("FlightFilterSelection", "");
        ViewBag.Add("destinationid", "0");
        ViewBag.Add("departureid", "0");
        ViewBag.Add("departureDate", "Alle");
    }
}