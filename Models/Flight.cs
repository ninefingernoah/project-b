/// <summary>
/// A flight
/// </summary>
public class Flight {
    
    /// <summary>The id of the flight</summary>
    private int _id;
    /// <summary>The departure airport</summary>
    private Airport _departure;
    /// <summary>The destination airport</summary>
    private Airport _destination;
    /// <summary>The departure time</summary>
    private DateTime _departureTime;
    /// <summary>The arrival time</summary>
    private DateTime _arrivalTime;
    /// <summary>The airplane</summary>
    private Airplane _airplane;
    /// <summary>The taken seats</summary>
    private List<Seat> _takenSeats;

    /// <summary>The id of the flight</summary>
    public int Id { get => _id; set => _id = value; }
    /// <summary>The departure airport</summary>
    public Airport Departure { get => _departure; set => _departure = value; }
    /// <summary>The destination airport</summary>
    public Airport Destination { get => _destination; set => _destination = value; }
    /// <summary>The departure time</summary>
    public DateTime DepartureTime { get => _departureTime; set => _departureTime = value; }
    /// <summary>The arrival time</summary>
    public DateTime ArrivalTime { get => _arrivalTime; set => _arrivalTime = value; }
    /// <summary>The airplane</summary>
    public Airplane Airplane { get => _airplane; set => _airplane = value; }
    /// <summary>The taken seats</summary>
    public List<Seat> TakenSeats { get => _takenSeats; }

    /// <summary>
    /// Constructor for Flight
    /// </summary>
    /// <param name="id">The id of the flight</param>
    /// <param name="departure">The departure airport</param>
    /// <param name="destination">The destination airport</param>
    /// <param name="departureTime">The departure time</param>
    /// <param name="arrivalTime">The arrival time</param>
    /// <param name="airplane">The airplane</param>
    public Flight(int id, Airport departure, Airport destination, DateTime departureTime, DateTime arrivalTime, Airplane airplane) {
        _id = id;
        _departure = departure;
        _destination = destination;
        _departureTime = departureTime;
        _arrivalTime = arrivalTime;
        _airplane = airplane;
        SetSeatPrices();
        _takenSeats = GetTakenSeatsFromDatabase();
    }

    /// <summary>
    /// Copy constructor for Flight
    /// </summary>
    /// <param name="flight">The flight to copy</param>
    public Flight(Flight flight)
    {
        _id = flight.Id;
        _departure = flight.Departure;
        _destination = flight.Destination;
        _departureTime = flight.DepartureTime;
        _arrivalTime = flight.ArrivalTime;
        _airplane = flight.Airplane;
        _takenSeats = flight.TakenSeats;
    }

    /// <summary>
    /// Adds a taken seat
    /// </summary>
    /// <param name="seat">The seat to add</param>
    public void AddTakenSeat(Seat seat) {
        _takenSeats.Add(seat);
    }

    /// <summary>
    /// Removes a taken seat
    /// </summary>
    /// <param name="seat">The seat to remove</param>
    public void RemoveTakenSeat(Seat seat) {
        _takenSeats.Remove(seat);
    }

    /// <summary>
    /// Checks if a seat is taken
    /// </summary>
    /// <param name="seat">The seat to check</param>
    /// <returns>True if the seat is taken, false if not</returns>
    public bool IsSeatTaken(Seat seat) {
        foreach (Seat takenSeat in _takenSeats) {
            if (takenSeat.Number == seat.Number) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Sets the prices of the seats
    /// </summary>
    public void SetSeatPrices() {
        Dictionary<string, double> prices = SeatManager.GetSeatPrices(this);
        foreach (Seat seat in _airplane.Seats) {
            seat.Price = prices[seat.Color];
        }
    }

    /// <summary>
    /// Gets the taken seats
    /// </summary>
    /// <returns>A list of taken seats. An empty list of 0 are found.</returns>
    public List<Seat> GetTakenSeatsFromDatabase() {
        List<Seat>? takenSeats = SeatManager.GetTakenSeats(this);
        if (takenSeats != null) {
            return takenSeats;
        }
        return new List<Seat>();
    }

    /// <summary>
    /// Returns a string of the flight
    /// </summary>
    /// <returns>A string of the flight</returns>
    public override string ToString()
    {
        string flightString = $"Vlucht {Id} van {Departure.Name} naar {Destination.Name}\n";
        flightString += $"Vertrek op: {DepartureTime.ToShortDateString()} om {DepartureTime.ToShortTimeString()}\n";
        flightString += $"Aankomst: {ArrivalTime.ToShortDateString()} om {ArrivalTime.ToShortTimeString()}\n";
        flightString += $"Vliegtuig: {Airplane.Name}\n";
        return flightString;
    }
}