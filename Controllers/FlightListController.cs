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

    // TODO: Enhance the documentation. I have no clue what this method does.
    /// <summary>
    /// Shows the flights?
    /// </summary>
    public void ShowFlights() {
        var flights = new List<Flight>();
        // fill list with flights from database
        // for now
        Airplane airplane = new Airplane(1, 100, "Boeing je moeder");
        // flights.Add(new Flight(1, "Amsterdam", "New York", new DateTime(2019, 1, 1), new DateTime(2019, 1, 2), airplane));
        // flights.Add(new Flight(2, "Amsterdam", "New York", new DateTime(2019, 1, 3), new DateTime(2019, 1, 4), airplane));
        // flights.Add(new Flight(3, "Amsterdam", "New York", new DateTime(2019, 1, 5), new DateTime(2019, 1, 6), airplane));
        FlightListView flightListView = FlightListView.Instance;
        flightListView.Show(flights);
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
}