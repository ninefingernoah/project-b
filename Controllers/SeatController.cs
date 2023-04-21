public class SeatController {
    private static readonly SeatController instance = new SeatController();

    static SeatController() {
    }
    private SeatController() {
    }

    public static SeatController Instance {
        get {
            return instance;
        }
    }

    public void ShowSeatSelection(Flight flight) {
        SeatSelectionView.Instance.Display(flight);
        try {
            string selection = SeatSelectionView.Instance.ViewBag["SeatViewSelection"];
            if (selection == "1") {
                // reserve seat
            }
        }
        catch (Exception e) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }
}