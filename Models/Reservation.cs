/// <summary>
/// A reservation
/// </summary>
public class Reservation
{
    /// <summary>The reservation number</summary>
    private string _reservationNumber;
    /// <summary>The outward flight</summary>
    private Flight _outwardflight;
    /// <summary>The inward flight. May be null if the reservation is a single flight.</summary>
    private Flight? _inwardflight;
    /// <summary>The user. May be null if the reservation is made by a guest.</summary>
    private User? _user;
    /// <summary>The email. May be null if the reservation is made by a user.</summary>
    private string? _email;
    /// <summary>The seats for the outward flight</summary>
    private List<Seat> _outwardSeats;
    /// <summary>The seats for the inward flight. May be null if the reservation is a single flight.</summary>
    private List<Seat>? _inwardSeats;
    /// <summary>The price of the reservation</summary>
    private double _price;
    /// <summary>The passengers</summary>
    private List<Passenger> _passengers;
    /// <summary>The date of the reservation</summary>
    private DateTime _reservationDate;
    /// <summary>Whether the reservation has been paid</summary>
    private bool _isPaid;

    /// <summary>The reservation number</summary>
    public string ReservationNumber { get => _reservationNumber; set => _reservationNumber = value; }
    /// <summary>The outward flight</summary>
    public Flight OutwardFlight { get => _outwardflight; set => _outwardflight = value; }
    /// <summary>The inward flight. May be null if the reservation is a single flight.</summary>
    public Flight? InwardFlight { get => _inwardflight; set => _inwardflight = value; }
    /// <summary>The user. May be null if the reservation is made by a guest.</summary>
    public User? User { get => _user; set => _user = value; }
    /// <summary>The email. May be null if the reservation is made by a user.</summary>
    public string? Email { get => _email; set => _email = value; }
    /// <summary>The seats for the outward flight</summary>
    public List<Seat> OutwardSeats { get => _outwardSeats; set=> _outwardSeats = value; }
    /// <summary>The seats for the inward flight. May be null if the reservation is a single flight.</summary>
    public List<Seat>? InwardSeats { get => _inwardSeats; set => _inwardSeats = value; }
    /// <summary>The price of the reservation</summary>
    public double Price { get => _price; set => _price = value; }
    /// <summary>The passengers</summary>
    public List<Passenger> Passengers { get => _passengers; }
    /// <summary>The date of the reservation</summary>
    public DateTime ReservationDate { get => _reservationDate; set => _reservationDate = value; }
    /// <summary>Whether the reservation has been paid</summary>
    public bool IsPaid { get => _isPaid; set => _isPaid = value; }

    /// <summary>
    /// Constructor for Reservation. Either email or user must be filled in.
    /// </summary>
    /// <param name="reservationNumber">The reservation number</param>
    /// <param name="outwardFlight">The outward flight</param>
    /// <param name="inwardFlight">The inward flight. May be null if the reservation is a single flight.</param>
    /// <param name="user">The user. May be null if the reservation is made by a guest.</param>
    /// <param name="email">The email. May be null if the reservation is made by a user.</param>
    /// <param name="passengers">The passengers</param>
    /// <param name="price">The price of the reservation</param>
    /// <param name="reservationDate">The date of the reservation</param>
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

    /// <summary>
    /// The string representation of a reservation
    /// </summary>
    public override string ToString()
    {
        if (InwardFlight == null)
            return $"#{ReservationNumber} - {OutwardFlight.Departure.Code} -> {OutwardFlight.Destination.Code} ({OutwardFlight.DepartureTime.ToString("dd/MM/yyyy HH:mm")})";
        return $"#{ReservationNumber} - {OutwardFlight.Departure.Code} -> {OutwardFlight.Destination.Code} RETOUR ({OutwardFlight.DepartureTime.ToString("dd/MM/yyyy HH:mm")} - {InwardFlight.DepartureTime.ToString("dd/MM/yyyy HH:mm")})";

    }

    /// <summary>
    /// Adds a taken seat to the outwarseats list in the reservation
    /// </summary>
    public void AddOutwardSeat(Seat seat)
    {
        _outwardSeats.Add(seat);
    }

    /// <summary>
    /// Adds a taken seat to the inwardseats list in the reservation
    /// </summary>
    public void AddInwardSeat(Seat seat)
    {
        if (_inwardSeats == null)
            _inwardSeats = new List<Seat>();
        _inwardSeats.Add(seat);
    }

}