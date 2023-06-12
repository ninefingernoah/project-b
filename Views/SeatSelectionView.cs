/// <summary>
/// The view for the seat selection menu.
/// </summary>
public class SeatSelectionView {
    /// <summary>
    /// The view bag. Holds temporary data for the view.
    /// </summary>
    public IDictionary<string, string> ViewBag = new Dictionary<string, string>();

    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static readonly SeatSelectionView instance = new SeatSelectionView();

    /// <summary>
    /// The selected seats.
    /// </summary>
    public List<Seat> SelectedSeats = new List<Seat>();

    /// <summary>
    /// The price.
    /// </summary>
    public double Price;

    static SeatSelectionView() {
    }
    private SeatSelectionView() {
    }

    /// <summary>
    /// The getter for the singleton instance.
    /// </summary>
    public static SeatSelectionView Instance {
        get {
            return instance;
        }
    }
    
    /// <summary>
    /// Displays the seat selection menu.
    /// </summary>
    /// <param name="flight">The flight.</param>
    /// <param name="passengerAmount">The passenger amount.</param>
    /// <param name="startPrice">The start price.</param>
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