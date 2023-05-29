public class SeatSelectionView {
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly SeatSelectionView instance = new SeatSelectionView();

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
    }
}