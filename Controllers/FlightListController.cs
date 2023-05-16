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
    /// Shows all the flights in the list as a menu
    /// </summary>
    /// <param name="flights">The list of flights to show</param>
    public void ShowFlights() {
        FlightListView flightListView = FlightListView.Instance;
        DateTime.TryParse(FlightFilterView.Instance.ViewBag["departureDate"], out DateTime result);
        List<Flight> flights = FlightManager.GetFlights(
                        result,
                        Int32.Parse(FlightFilterView.Instance.ViewBag["departureid"]),
                        Int32.Parse(FlightFilterView.Instance.ViewBag["destinationid"]));
        flightListView.Display(flights);
        try {
            string selection = flightListView.ViewBag["FlightListSelection"];
            int selectionInt = int.Parse(selection);
            // Could we use a switch statement here? These tend to be more readable and faster.
            if (selectionInt == flights.Count) {
                MainMenuController.Instance.ShowMainMenu();
            }
            else {
                FlightController.Instance.ShowFlight(flights[selectionInt]);
            }
        }
        catch (Exception) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }

    // TODO: Break this up
    /// <summary>
    /// Shows the menu for filtering flights
    /// </summary>
    public void ShowFilters() {
        FlightFilterView.Instance.Display();
        try {
            string selection = FlightFilterView.Instance.ViewBag["FlightFilterSelection"];
            int selectionInt = int.Parse(selection);
            switch (selectionInt) {

                // Lets user input a departure date
                case 0:
                    StringInputMenu departureDateMenu = new StringInputMenu("Voer een datum in: ");
                    string departureDate = departureDateMenu.Run();
                    if (!DateTime.TryParse(departureDate, out DateTime result)) {
                        ConsoleUtils.Error("Ongeldige datum.");
                    }
                    if (result == DateTime.MinValue) {
                        FlightFilterView.Instance.ViewBag["departureDate"] = "Alle";
                    }
                    else {
                        FlightFilterView.Instance.ViewBag["departureDate"] = result.ToShortDateString();
                    }
                    ShowFilters();
                    break;

                // Lets user input a departure airport
                case 1:
                    var temp = FlightController.Instance.GetAirport();
                    FlightFilterView.Instance.ViewBag["departureid"] = temp.Id.ToString();
                    ShowFilters();
                    break;

                // Lets user input a destination airport
                case 2:
                    temp = FlightController.Instance.GetAirport();
                    FlightFilterView.Instance.ViewBag["destinationid"] = temp.Id.ToString();
                    ShowFilters();
                    break;

                case 4:
                    FlightFilterView.Instance.ResetViewBag();
                    ShowFilters();
                    break;

                // Shows the filtered flights
                case 5:
                    FlightListController.Instance.ShowFlights();
                    break;

                // Returns to the main menu
                case 6:
                    FlightFilterView.Instance.ResetViewBag();
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