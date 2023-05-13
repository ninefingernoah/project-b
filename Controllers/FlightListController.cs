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
                    var temp = FlightController.Instance.GetAirport();
                    FlightFilterView.Instance.ViewBag["departureid"] = temp.Id.ToString();
                    ShowFilters();
                    break;
                case 2:
                    temp = FlightController.Instance.GetAirport();
                    FlightFilterView.Instance.ViewBag["destinationid"] = temp.Id.ToString();
                    ShowFilters();
                    break;
                case 3:
                    // ZOEK
                    DateTime.TryParse(FlightFilterView.Instance.ViewBag["departureDate"], out result);
                    FlightListController.Instance.ShowFlights(FlightManager.GetFlights(
                        result,
                        Int32.Parse(FlightFilterView.Instance.ViewBag["departureid"]),
                        Int32.Parse(FlightFilterView.Instance.ViewBag["destinationid"])
                    ));
                    break;
                case 4:
                    ShowFilters();
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