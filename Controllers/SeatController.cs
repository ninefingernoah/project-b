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
    public void ShowSeatSelection(Reservation reservation, int costPerSeat) {
        // clear view
        SeatSelectionView.Instance.SelectedSeats.Clear();

        // add previously selected seats
        SeatSelectionView.Instance.SelectedSeats.AddRange(reservation.OutwardSeats);

        // display view
        SeatSelectionView.Instance.Display(reservation.OutwardFlight, reservation.Passengers.Count, reservation.Price);

        // update price
        reservation.Price = SeatSelectionView.Instance.Price;

        // update seats
        if(SeatSelectionView.Instance.SelectedSeats.Count > 0) {
            // add the costs of the amount of seats that have been changed to the price
            reservation.Price += AmountOfSeatsChanged(reservation.OutwardSeats, SeatSelectionView.Instance.SelectedSeats) * costPerSeat;
            reservation.OutwardSeats = new List<Seat>(SeatSelectionView.Instance.SelectedSeats);
        }

        // repeat for inward flight
        SeatSelectionView.Instance.SelectedSeats.Clear();
        if (reservation.InwardFlight != null && reservation.InwardSeats != null) {
            SeatSelectionView.Instance.SelectedSeats.AddRange(reservation.InwardSeats);
            SeatSelectionView.Instance.Display(reservation.InwardFlight, reservation.Passengers.Count, reservation.Price);
            reservation.Price = SeatSelectionView.Instance.Price;
            if (SeatSelectionView.Instance.SelectedSeats.Count > 0) {
                reservation.Price += AmountOfSeatsChanged(reservation.InwardSeats, SeatSelectionView.Instance.SelectedSeats) * costPerSeat;
                reservation.InwardSeats = new List<Seat>(SeatSelectionView.Instance.SelectedSeats);
            }
        }
    }

    /// <summary>
    /// Returns the amount of seats that have been changed between the old and new list of seats.
    /// </summary>
    /// <param name="oldSeats">The old list of seats.</param>
    /// <param name="newSeats">The new list of seats.</param>
    public static int AmountOfSeatsChanged(List<Seat> oldSeats, List<Seat> newSeats) {
        int amount = 0;
        foreach (Seat seat in oldSeats) {
            if (!newSeats.Contains(seat)) {
                amount++;
            }
        }
        return amount;
    }
}