<<<<<<< Updated upstream
=======
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

    // TODO: Document method. I have no clue what this method does.
    public void ShowSeatSelection(Flight flight) {
        SeatSelectionView.Instance.Display(flight);
        try {
            int selection = int.Parse(SeatSelectionView.Instance.ViewBag["SeatViewSelection"]);
            if (selection == 1) {
                // confirm seats
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
>>>>>>> Stashed changes
