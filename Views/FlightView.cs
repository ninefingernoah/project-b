public class FlightView {
    private static readonly FlightView instance = new FlightView();
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();

    static FlightView() {
    }
    private FlightView() {
    }

    public static FlightView Instance {
        get {
            return instance;
        }
    }

    public void Display(Flight flight) {
        string[] options = { "Reserveer stoelen", "Terug" };
        Menu menu = new Menu(flight.ToString(), options);
        int selection = menu.Run();
        ViewBag["FlightViewSelection"] = selection.ToString();
    }
}