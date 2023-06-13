public class SeatSelectionView {
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly SeatSelectionView instance = new SeatSelectionView();
    /// <summary> The seats that have been selected by the user. </summary>
    public List<Seat> SelectedSeats = new List<Seat>();
    /// <summary> The total price of the seats that have been selected by the user. </summary>
    public double Price;

    static SeatSelectionView() {
    }
    private SeatSelectionView() {
    }

    public static SeatSelectionView Instance {
        get {
            return instance;
        }
    }
    
    public void Display(Flight flight, List<Passenger> passengers, double startPrice) {
        // show seat grid
        SeatSelectionMenu menu = new SeatSelectionMenu(flight, passengers, startPrice);
        menu.SelectedSeats = new List<Seat>(SelectedSeats);
        menu.Run();
        SelectedSeats = new List<Seat>(menu.SelectedSeats);
        // if (menu.SelectedSeats.Count > 0) {
        //     SelectedSeats.AddRange(menu.SelectedSeats);
        // }
        menu.SelectedSeats.Clear();
        Price = menu.CurrentPrice;
    }
}