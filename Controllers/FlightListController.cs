public class FlightListController {
    private static readonly FlightListController instance = new FlightListController();

    static FlightListController() {
    }
    private FlightListController() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static FlightListController Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Shows all the flights in the list
    /// </summary>
    /// <param name="flights">The list of flights to show</param>
    public void ShowFlights(List<Flight> flights) {
        // fill list with flights from database
        // for now
        Airplane airplane = new Airplane(1, 100, "Boeing je moeder");
        // flights.Add(new Flight(1, "Amsterdam", "New York", new DateTime(2019, 1, 1), new DateTime(2019, 1, 2), airplane));
        // flights.Add(new Flight(2, "Amsterdam", "New York", new DateTime(2019, 1, 3), new DateTime(2019, 1, 4), airplane));
        // flights.Add(new Flight(3, "Amsterdam", "New York", new DateTime(2019, 1, 5), new DateTime(2019, 1, 6), airplane));
        FlightListView flightListView = FlightListView.Instance;
        flightListView.Display(flights);
        try {
            string selection = flightListView.ViewBag["FlightListSelection"];
            int selectionInt = int.Parse(selection);
            // Could we use a switch statement here? These tend to be more readable and faster.
            if (selectionInt == flights.Count) {
                MainMenuController.Instance.ShowMainMenu();
                return;
            }
            FlightController.Instance.ShowFlight(flights[selectionInt]);
        }
        catch (Exception) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }

    public void ShowFilters() {
        FlightFilterView.Instance.Display();
        try {
            string selection = FlightFilterView.Instance.ViewBag["FlightFilterSelection"];
            int selectionInt = int.Parse(selection);
            switch (selectionInt) {
                case 0:
                    StringInputMenu departureDateMenu = new StringInputMenu("Voer een datum in: ");
                    string departureDate = departureDateMenu.Run();
                    if (!DateTime.TryParse(departureDate, out DateTime result)) {
                        ConsoleUtils.Error("Ongeldige datum.");
                    }
                    FlightFilterView.Instance.ViewBag["departureDate"] = result.ToShortDateString();
                    ShowFilters();
                    break;
                case 1:
                    StringInputMenu departureMenu = new StringInputMenu("Voer een vertreklocatie in: ");
                    FlightFilterView.Instance.ViewBag["departure"] = departureMenu.Run();
                    ShowFilters();
                    break;
                case 2:
                    StringInputMenu arrivalMenu = new StringInputMenu("Voer een aankomstlocatie in: ");
                    FlightFilterView.Instance.ViewBag["arrival"] = arrivalMenu.Run();
                    ShowFilters();
                    break;
                case 3:
                    // ZOEK
                    /* FlightListController.Instance.ShowFlights(FlightManager.GetFlights(
                        DateTime.Parse(FlightFilterView.Instance.ViewBag["departureDate"]),
                        FlightFilterView.Instance.ViewBag["departure"],
                        FlightFilterView.Instance.ViewBag["arrival"]
                    )); */
                case 4:
                    MainMenuController.Instance.ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("Ongeldige keuze.");
                    break;
            }
        }
        catch (Exception) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }
}