/// <summary>
/// An airplane.
/// </summary>
public class Airplane {
    /// <summary>The ID of the airplane.</summary>
    protected int _id;
    /// <summary>The name of the airplane.</summary>
    private string _name;
    /// <summary>The seats of the airplane.</summary>
    protected List<Seat> _seats;
    /// <summary>The total capacity of the airplane.</summary>
    protected int _totalCapacity;

    /// <summary>The ID of the airplane.</summary>
    public int Id { get => _id; set => _id = value; }
    /// <summary>The name of the airplane.</summary>
    public string Name { get => _name; set => _name = value; }
    /// <summary>The seats of the airplane.</summary>
    public List<Seat> Seats { get => _seats; }
    /// <summary>The total capacity of the airplane.</summary>
    public int TotalCapacity { get => _totalCapacity; set => _totalCapacity = value; }

    /// <summary>
    /// The constructor for the Airplane class.
    /// </summary>
    /// <param name="id">The ID of the airplane.</param>
    /// <param name="totalCapacity">The total capacity of the airplane.</param>
    /// <param name="name">The name of the airplane.</param>
    public Airplane(int id, int totalCapacity, string name) {
        _id = id;
        _totalCapacity = totalCapacity;
        _seats = new List<Seat>();
        _name = name;
        InitializeSeats();
    }

    /// <summary>
    /// Adds a seat to the airplane.
    /// </summary>
    public void AddSeat(Seat seat) {
        _seats.Add(seat);
    }

    /// <summary>
    /// Removes a seat from the airplane.
    /// </summary>
    public void RemoveSeat(Seat seat) {
        _seats.Remove(seat);
    }

    /// <summary>
    /// Gets a seat from the airplane by seatnumber.
    /// </summary>
    /// <param name="seatNumber">The seatnumber of the seat.</param>
    /// <returns>Returns the seat if found, null if not.</returns>
    public Seat? GetSeat(string seatNumber) {
        foreach (Seat seat in _seats) {
            if (seat.Number == seatNumber) {
                return seat;
            }
        }
        return null;
    }

    /// <summary>
    /// Returns a string representation of the airplane.
    /// </summary>
    public override string ToString()
    {
        return $"{Name} ({TotalCapacity} stoelen)";
    }

    /// <summary>
    /// Initializes the seats of the airplane.
    /// </summary>
    public void InitializeSeats() {
        AirplaneLayout airplaneLayout = AirplaneManager.GetAirplaneLayout(_name);
        List<Seat> allSeats = new List<Seat>();

        foreach (var section in airplaneLayout.SeatLayout)
        {
            allSeats.AddRange(section.Seats);
        }

        _seats = allSeats;
        _totalCapacity = allSeats.Count;
    }

}