public class FlightController {
    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    private static readonly FlightController instance = new FlightController();

    static FlightController() {
    }
    private FlightController() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static FlightController Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Displays the menu for booking a flight and handles the user input.
    /// </summary>
    /// <param name="flight">The flight to book.</param>
    public void ShowFlight(Flight flight) {
        FlightView flightView = FlightView.Instance;
        flightView.Display(flight);
        try {
            int selection = int.Parse(flightView.ViewBag["FlightViewSelection"]);
            switch(selection)
            {
                case 0:
                    //_seatController.Run(flight);
                    break;
                case 1:
                    MainMenuController.Instance.ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("Er is iets fout gegaan.");
                    // return to main menu
                    break;
            }
        }
        catch (Exception) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }
}
