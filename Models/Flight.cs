public class Flight {
    private int _id;
    private string _departure;
    private string _destination;
    private DateTime _departureTime;
    private DateTime _arrivalTime;
    private Airplane _airplane;
    private List<Seat> _takenSeats;

    public int Id { get => _id; set => _id = value; }
    public string Departure { get => _departure; set => _departure = value; }
    public string Destination { get => _destination; set => _destination = value; }
    public DateTime DepartureTime { get => _departureTime; set => _departureTime = value; }
    public DateTime ArrivalTime { get => _arrivalTime; set => _arrivalTime = value; }
    public Airplane Airplane { get => _airplane; set => _airplane = value; }
    public List<Seat> TakenSeats { get => _takenSeats; }

    public Flight(int id, string departure, string destination, DateTime departureTime, DateTime arrivalTime, Airplane airplane) {
        _id = id;
        _departure = departure;
        _destination = destination;
        _departureTime = departureTime;
        _arrivalTime = arrivalTime;
        _airplane = airplane;
        _takenSeats = new List<Seat>();
    }

    public override string ToString()
    {
        string flightString = $"Flight {Id} from {Departure} to {Destination}\n";
        flightString += $"Departure: {DepartureTime.ToShortDateString()} at {DepartureTime.ToShortTimeString()}\n";
        flightString += $"Arrival: {ArrivalTime.ToShortDateString()} at {ArrivalTime.ToShortTimeString()}\n";
        flightString += $"Airplane: {Airplane.Name}\n";
        return flightString;
    }
}