public class FlightListView {
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly FlightListView instance = new FlightListView();

    static FlightListView() {
    }
    private FlightListView() {
    }

    public static FlightListView Instance {
        get {
            return instance;
        }
    }
    
    public void Show(List<Flight> flights) {
        string[] options = flights.Select(f => f.Id.ToString()).ToArray();
        options.Append("-");
        options.Append("Terug");
        Menu menu = new Menu("Gevonden vluchten", options);
        int selection = menu.Run();
        ViewBag["FlightListSelection"] = selection.ToString();
    }
}