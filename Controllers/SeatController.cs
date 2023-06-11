public class SeatController {
    private static readonly SeatController instance = new SeatController();

    static SeatController() {
    }
    private SeatController() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static SeatController Instance {
        get {
            return instance;
        }
    }

    public void ShowSeatSelection(Reservation reservation) {
        SeatSelectionView.Instance.Display(reservation.Flight);
        if(SeatSelectionView.Instance.SelectedSeats.Count > 0) {
            reservation.Seats.AddRange(SeatSelectionView.Instance.SelectedSeats);
        }
    }
}