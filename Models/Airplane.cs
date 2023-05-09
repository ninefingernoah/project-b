public class Airplane {
    protected int _id;
    private string _name;
    protected List<Seat> _seats;
    protected int _totalCapacity;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public List<Seat> Seats { get => _seats; }
    public int TotalCapacity { get => _totalCapacity; set => _totalCapacity = value; }

    public Airplane(int id, int totalCapacity, string name) {
        _id = id;
        _totalCapacity = totalCapacity;
        _seats = new List<Seat>();
        _name = name;
        InitializeSeats();
    }

    public void AddSeat(Seat seat) {
        _seats.Add(seat);
    }

    public void RemoveSeat(Seat seat) {
        _seats.Remove(seat);
    }

    public Seat? GetSeat(string seatNumber) {
        foreach (Seat seat in _seats) {
            if (seat.Number == seatNumber) {
                return seat;
            }
        }
        return null;
    }

    public void InitializeSeats() {
        // get seats from database w seatmanager
        // add seats to _seats
        // test code
        /* for (int i = 0; i < _totalCapacity/4; i++) {
            _seats.Add(new Seat(i.ToString(), "white"));
            _seats.Add(new Seat(i.ToString(), "dark_blue"));
            _seats.Add(new Seat(i.ToString(), "purple"));
        } */
    }

}