public class Reservation
{
    private int _reservationNumber;
    private Flight _outwardflight;
    private Flight? _inwardflight;
    private User _user;
    private string _email;
    private List<Seat> _outwardSeats;
    private List<Seat> _inwardSeats;
    private double _price;
    private List<Passenger> _passengers;
    private DateTime _reservationDate;
    private bool _isPaid;


    public int ReservationNumber { get => _reservationNumber; set => _reservationNumber = value; }
    public Flight OutwardFlight { get => _outwardflight; set => _outwardflight = value; }
    public Flight InwardFlight { get => _inwardflight; set => _inwardflight = value; }
    public User User { get => _user; set => _user = value; }
    public string Email { get => _email; set => _email = value; }
    public List<Seat> OutwardSeats { get => _outwardSeats; }
    public List<Seat> InwardSeats { get => _inwardSeats; }
    public double Price { get => _price; set => _price = value; }
    public List<Passenger> Passengers { get => _passengers; }
    public DateTime ReservationDate { get => _reservationDate; set => _reservationDate = value; }
    public bool IsPaid { get => _isPaid; set => _isPaid = value; }

    public Reservation(int reservationNumber, Flight outwardFlight, Flight? inwardFlight, User user, string email, List<Passenger> passengers, double price, DateTime reservationDate)
    {
        _reservationNumber = reservationNumber;
        _outwardflight = outwardFlight;
        _inwardflight = inwardFlight;
        _user = user;
        _price = price;
        _passengers = passengers;
        _email = email;
        _reservationDate = reservationDate;
        _outwardSeats = new List<Seat>();
        _inwardSeats = new List<Seat>();
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
        _inwardSeats.Add(seat);
    }
}