public class Reservation {
    private string _reservationNumber;
    private Flight _flight;
    private User _user;
    private String _email;
    private List<Seat> _seats;
    private double _price;
    private List<Passenger> _passengers;
    private DateTime _madeOn;
    private bool _isPaid;

    public string ReservationNumber { get => _reservationNumber; set => _reservationNumber = value; }
    public Flight Flight { get => _flight; set => _flight = value; }
    public User User { get => _user; set => _user = value; }
    public string Email { get => _email; set => _email = value; }
    public List<Seat> Seats { get => _seats; }
    public double Price { get => _price; set => _price = value; }
    public List<Passenger> Passengers { get => _passengers; }
    public DateTime MadeOn { get => _madeOn; set => _madeOn = value; }
    public bool IsPaid { get => _isPaid; set => _isPaid = value; }

    public Reservation(string reservationNumber, Flight flight, User user, string email, double price, DateTime madeOn) {
        _reservationNumber = reservationNumber;
        _flight = flight;
        _user = user;
        _seats = new List<Seat>();
        _price = price;
        _passengers = new List<Passenger>();
        _email = email;
        _madeOn = madeOn;
    }
}