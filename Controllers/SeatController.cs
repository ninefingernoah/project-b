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

    /// <summary>
    /// Displays the seat selection views for the given reservation.
    /// </summary>
    /// <param name="reservation">The reservation to display the seat selection view for.</param>
    public void ShowSeatSelection(Reservation reservation) {
        //TODO: add default cost to changing seats (THIS IS NEXT TO TO USERS SELECTING MORE EXPENSIVE SEATS)
        SeatSelectionView.Instance.SelectedSeats.Clear();
        SeatSelectionView.Instance.SelectedSeats.AddRange(reservation.OutwardSeats);
        SeatSelectionView.Instance.Display(reservation.OutwardFlight, reservation.Passengers.Count);
        if(SeatSelectionView.Instance.SelectedSeats.Count > 0) {
            reservation.OutwardSeats.AddRange(SeatSelectionView.Instance.SelectedSeats);
        }
        SeatSelectionView.Instance.SelectedSeats.Clear();
        SeatSelectionView.Instance.SelectedSeats.AddRange(reservation.InwardSeats);
        if (reservation.InwardFlight != null) {
            SeatSelectionView.Instance.Display(reservation.InwardFlight, reservation.Passengers.Count);
            if (SeatSelectionView.Instance.SelectedSeats.Count > 0) {
                reservation.InwardSeats.AddRange(SeatSelectionView.Instance.SelectedSeats);
            }
        }
        SeatSelectionView.Instance.SelectedSeats.Clear();
    }
}