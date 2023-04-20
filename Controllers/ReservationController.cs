public sealed class ReservationController {
    private static readonly ReservationController instance = new ReservationController();

    static ReservationController() {
    }
    private ReservationController() {
    }

    public static ReservationController Instance {
        get {
            return instance;
        }
    }

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