public class FlightController {
    private static readonly FlightController instance = new FlightController();

    static FlightController() {
    }
    private FlightController() {
    }

    public static FlightController Instance {
        get {
            return instance;
        }
    }

    public void ShowFlight(Flight flight) {
        FlightView flightView = FlightView.Instance;
        flightView.Display(flight);
        try {
            int selection = int.Parse(flightView.ViewBag["FlightViewSelection"]);
            if (selection == 0) {
                //_seatController.Run(flight);
            }
            else if (selection == 1) {
                MainMenuController.Instance.ShowMainMenu();
            }
        }
        catch (Exception e) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }
}