/// <summary>
/// A flight.
/// </summary>
public class Flight {
    /// <summary> The id of the flight. </summary>
    private int _id;
    /// <summary> The departure airport. </summary>
    private Airport _departure;
    /// <summary> The destination airport. </summary>
    private Airport _destination;
    /// <summary> The departure time. </summary>
    private DateTime _departureTime;
    /// <summary> The arrival time. </summary>
    private DateTime _arrivalTime;
    /// <summary> The airplane. </summary>
    private Airplane _airplane;
    /// <summary> The taken seats. </summary>
    private List<Seat> _takenSeats;

    public int Id { get => _id; set => _id = value; }
    public Airport Departure { get => _departure; set => _departure = value; }
    public Airport Destination { get => _destination; set => _destination = value; }
    public DateTime DepartureTime { get => _departureTime; set => _departureTime = value; }
    public DateTime ArrivalTime { get => _arrivalTime; set => _arrivalTime = value; }
    public Airplane Airplane { get => _airplane; set => _airplane = value; }
    public List<Seat> TakenSeats { get => _takenSeats; }

    public Flight(int id, Airport departure, Airport destination, DateTime departureTime, DateTime arrivalTime, Airplane airplane) {
        _id = id;
        _departure = departure;
        _destination = destination;
        _departureTime = departureTime;
        _arrivalTime = arrivalTime;
        _airplane = airplane;
        SetSeatPrices();
        SetTakenSeats();
    }

    /// <summary>
    /// Adds a taken seat to the flight.
    /// </summary>
    /// <param name="seat">The seat to add.</param>
    public void AddTakenSeat(Seat seat) {
        _takenSeats.Add(seat);
    }

    /// <summary>
    /// Removes a taken seat from the flight.
    /// </summary>
    /// <param name="seat">The seat to remove.</param>
    public void RemoveTakenSeat(Seat seat) {
        _takenSeats.Remove(seat);
    }

    /// <summary>
    /// Checks if a seat is taken.
    /// </summary>
    /// <param name="seat">The seat to check.</param>
    /// <returns>Returns true if the seat is taken, false if not.</returns>
    public bool IsSeatTaken(Seat seat) {
        foreach (Seat takenSeat in _takenSeats) {
            if (takenSeat.Number == seat.Number) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Sets the seat prices inside of the Airplane.
    /// </summary>
    public void SetSeatPrices() {
        Dictionary<string, double> prices = SeatManager.GetSeatPrices(this);
        foreach (Seat seat in _airplane.Seats) {
            seat.Price = prices[seat.Color];
        }
    }

    /// <summary>
    /// Sets the taken seats.
    /// </summary>
    public void SetTakenSeats() {
        List<Seat>? takenSeats = SeatManager.GetTakenSeats(this);
        if (takenSeats != null) {
            _takenSeats = takenSeats;
        }
        else {
            _takenSeats = new List<Seat>();
        }
    }

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

    public override string ToString()
    {
        string flightString = $"Vlucht {Id} van {Departure.Name} naar {Destination.Name}\n";
        flightString += $"Vertrek op: {DepartureTime.ToShortDateString()} om {DepartureTime.ToShortTimeString()}\n";
        flightString += $"Aankomst: {ArrivalTime.ToShortDateString()} om {ArrivalTime.ToShortTimeString()}\n";
        flightString += $"Vliegtuig: {Airplane.Name}\n";
        return flightString;
    }
}