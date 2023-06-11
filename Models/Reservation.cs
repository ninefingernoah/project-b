public class Reservation
{
    private string _reservationNumber;
    private Flight _outwardflight;
    private Flight? _inwardflight;
    private User? _user;
    private string? _email;
    private List<Seat> _outwardSeats;
    private List<Seat>? _inwardSeats;
    private double _price;
    private List<Passenger> _passengers;
    private DateTime _reservationDate;
    private bool _isPaid;


    public string ReservationNumber { get => _reservationNumber; set => _reservationNumber = value; }
    public Flight OutwardFlight { get => _outwardflight; set => _outwardflight = value; }
    public Flight? InwardFlight { get => _inwardflight; set => _inwardflight = value; }
    public User? User { get => _user; set => _user = value; }
    public string? Email { get => _email; set => _email = value; }
    public List<Seat> OutwardSeats { get => _outwardSeats; set=> _outwardSeats = value; }
    public List<Seat>? InwardSeats { get => _inwardSeats; set => _inwardSeats = value; }
    public double Price { get => _price; set => _price = value; }
    public List<Passenger> Passengers { get => _passengers; }
    public DateTime ReservationDate { get => _reservationDate; set => _reservationDate = value; }
    public bool IsPaid { get => _isPaid; set => _isPaid = value; }

    //TODO: Maak reservationNumber een string en maak een methode die een random string van 6 tekens genereert
    public Reservation(string reservationNumber, Flight outwardFlight, Flight? inwardFlight, User? user, string? email, List<Passenger> passengers, double price, DateTime reservationDate)
    {
        if (email == null && user == null)
            throw new Exception("Either email or user must be filled in to create a reservation.");
        _reservationNumber = reservationNumber;
        _outwardflight = outwardFlight;
        _inwardflight = inwardFlight;
        _user = user;
        _price = price;
        _passengers = passengers;
        _email = email;
        _reservationDate = reservationDate;
        _outwardSeats = new List<Seat>();
        _inwardSeats = null;
    }

    public override string ToString()
    {
        if (InwardFlight == null)
            return $"#{ReservationNumber} - {OutwardFlight.Departure.Code} -> {OutwardFlight.Destination.Code} ({OutwardFlight.DepartureTime.ToString("dd/MM/yyyy HH:mm")})";
        return $"#{ReservationNumber} - {OutwardFlight.Departure.Code} -> {OutwardFlight.Destination.Code} RETOUR ({OutwardFlight.DepartureTime.ToString("dd/MM/yyyy HH:mm")} - {InwardFlight.DepartureTime.ToString("dd/MM/yyyy HH:mm")})";

    }

    public void AddOutwardSeat(Seat seat)
    {
        _outwardSeats.Add(seat);
    }

    public void AddInwardSeat(Seat seat)
    {
        if (_inwardSeats == null)
            _inwardSeats = new List<Seat>();
        _inwardSeats.Add(seat);
    }

    public void UpdatePrice() {
    }
}