public class Flight {
    private int _id;
    private Airport _departure;
    private Airport _destination;
    private DateTime _departureTime;
    private DateTime _arrivalTime;
    private Airplane _airplane;
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
        _takenSeats = new List<Seat>();
        SetSeatPrices();
    }

    public void AddTakenSeat(Seat seat) {
        _takenSeats.Add(seat);
    }

    public void RemoveTakenSeat(Seat seat) {
        _takenSeats.Remove(seat);
    }

    public bool IsSeatTaken(Seat seat) {
        foreach (Seat takenSeat in _takenSeats) {
            if (takenSeat.Number == seat.Number) {
                return true;
            }
        }
        return false;
    }

    public void SetSeatPrices() {
        Dictionary<string, double> prices = SeatManager.GetSeatPrices(this);
        foreach (Seat seat in _airplane.Seats) {
            seat.Price = prices[seat.Color];
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
        string flightString = $"Flight {Id} from {Departure.Name} to {Destination.Name}\n";
        flightString += $"Departure: {DepartureTime.ToShortDateString()} at {DepartureTime.ToShortTimeString()}\n";
        flightString += $"Arrival: {ArrivalTime.ToShortDateString()} at {ArrivalTime.ToShortTimeString()}\n";
        flightString += $"Airplane: {Airplane.Name}\n";
        return flightString;
    }
}