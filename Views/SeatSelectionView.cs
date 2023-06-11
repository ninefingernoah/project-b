public class SeatSelectionView {
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly SeatSelectionView instance = new SeatSelectionView();
    public List<Seat> SelectedSeats = new List<Seat>();

    static SeatSelectionView() {
    }
    private SeatSelectionView() {
    }

    public static SeatSelectionView Instance {
        get {
            return instance;
        }
    }
    
    public void Display(Flight flight) {
        // show seat grid
        SeatSelectionMenu menu = new SeatSelectionMenu(flight);
        menu.Run();
        if (menu.SelectedSeats.Count > 0) {
            SelectedSeats.AddRange(menu.SelectedSeats);
        }
    }
}