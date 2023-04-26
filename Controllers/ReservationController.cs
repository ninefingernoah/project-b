public sealed class ReservationController {
    private static readonly ReservationController instance = new ReservationController();

    static ReservationController() {
    }
    private ReservationController() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static ReservationController Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Sends the user to the seat selection view. Handles the user input.
    /// </summary>
    /// <param name="flight">The flight for which the user wants to select seats.</param>
    public void ShowSeatSelection(Flight flight) {
        SeatSelectionView.Instance.Display(flight);
        try {
            int selection = int.Parse(SeatSelectionView.Instance.ViewBag["SeatViewSelection"]);
            if (selection == 1) {
                // TODO:
                // confirm seats
                // create reservation
                // get seats from viewbag
                // add seats to reservation
                // add reservation to database
            }
            else if (selection == 2) {
                // return to flight details
                FlightController.Instance.ShowFlight(flight);
            }
        }
        catch (Exception e) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }
}
