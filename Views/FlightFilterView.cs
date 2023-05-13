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
        PopulateViewBag();
        Menu menu = new Menu("Filter vluchten", new string[] {
            "Datum van vertrek",
            "Vertrek van",
            "Aankomst in",
            "Zoek",
            "Terug"
        });

        ViewBag["FlightFilterSelection"] = menu.Run().ToString();
    }

    public void PopulateViewBag() {
        if (!FlightFilterView.Instance.ViewBag.ContainsKey("destinationid")) {
            FlightFilterView.Instance.ViewBag.Add("destinationid", "0");
        }
        if (!FlightFilterView.Instance.ViewBag.ContainsKey("departureid")) {
            FlightFilterView.Instance.ViewBag.Add("departureid", "0");
        }
        if (!FlightFilterView.Instance.ViewBag.ContainsKey("departureDate")) {
            FlightFilterView.Instance.ViewBag.Add("departureDate", "<vul in>");
        }
        if (!FlightFilterView.Instance.ViewBag.ContainsKey("FlightFilterSelection")) {
            FlightFilterView.Instance.ViewBag.Add("FlightFilterSelection", "");
        }
    }
}