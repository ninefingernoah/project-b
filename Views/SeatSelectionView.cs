public class SeatSelectionView {
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly SeatSelectionView instance = new SeatSelectionView();
    public List<Seat> SelectedSeats = new List<Seat>();
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
    
    public void Display(Flight flight, int passengerAmount, double startPrice) {
        // show seat grid
        SeatSelectionMenu menu = new SeatSelectionMenu(flight, passengerAmount, startPrice);
        menu.Run();
        SelectedSeats = new List<Seat>(menu.SelectedSeats);
        // if (menu.SelectedSeats.Count > 0) {
        //     SelectedSeats.AddRange(menu.SelectedSeats);
        // }
        menu.SelectedSeats.Clear();
        Price = menu.CurrentPrice;
    }
}