public class Seating
{
    private bool _available;
    private int _numOfChairs;
    private string _area;
    private int _seatingID;

    public bool Available { get => _available; set => _available = value; }
    public int NumOfChairs { get => _numOfChairs; set => _numOfChairs = value; }
    public string Area { get => _area; set => _area = value; }
    public int SeatingID { get => _seatingID; set => _seatingID = value; }

    public Seating(bool available, int numOfChairs, string area, int seatingID)
    {
        _available = available;
        _numOfChairs = numOfChairs;
        _area = area;
        _seatingID = seatingID;
    }

    public bool IsComplete()
    {
        return _numOfChairs > 0 && _area != null && _seatingID > 0;
    }

    public bool IsOccupied(DateTime date)
    {
        DatabaseManager db = Program.DatabaseManager;
        List<Reservation> reservations = Program.ReservationManager.Reservations;
        List<Reservation> reservationsOnDate = reservations.FindAll(r => r.Date == date || r.Date == date.AddDays(2));
        foreach (Reservation reservation in reservationsOnDate)
        {
            foreach (Seating seating in reservation.Seating)
            {
                if (seating.SeatingID == SeatingID)
                {
                    return true;
                }
            }
        }
        return false;
    }
}