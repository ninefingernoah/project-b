using System.Data;
public class SeatingManager
{
    private List<Seating> _seatings = new List<Seating>();
    private static bool _initialized = false;

    public List<Seating> Seatings
    {
        get { return _seatings; }
    }
    public SeatingManager()
    {
        if (_initialized)
            throw new Exception("SeatingManager already initialized.");
        LoadSeatings();
        _initialized = true;
    }

    /// <summary>
    /// Adds a seating to the restaurant (also implements into the database).
    /// </summary>
    /// <param name="seatingID">The ID of the seating.</param>
    /// <param name="numOfChairs">The amount of chairs in the seating.</param>
    /// <param name="area">The area of the seating.</param>
    /// <param name="available">Whether the seating is available or not.</param>
    public void AddSeating(int seatingID, int numOfChairs, string area, bool available)
    {
        Seating seating = new Seating(available, numOfChairs, area, seatingID);
        _seatings.Add(seating);
        DatabaseManager db = Program.DatabaseManager;
        db.Query($"INSERT INTO seatings (id, amount_of_chairs, area, available) VALUES ({seatingID}, {numOfChairs}, '{area}', {available})");
    }

    /// <summary>
    /// Removes a seating to the restaurant (also removes it out off the database).
    /// </summary>
    /// <param name="seatingID">The ID of the seating.</param>
    public void RemoveSeating(int seatingID)
    {
        Seating? seating = _seatings.Find(s => s.SeatingID == seatingID);
        if (seating != null)
        {
            _seatings.Remove(seating);
            DatabaseManager db = Program.DatabaseManager;
            db.Query($"DELETE FROM seating WHERE id = {seatingID}");
        }

    }

    /// <summary>
    /// Updates a seating to the restaurant (also updates it in the database).
    /// </summary>
    /// <param name="seatingID">The ID of the seating.</param>
    /// <param name="numOfChairs">The amount of chairs in the seating.</param>
    /// <param name="area">The area of the seating.</param>
    /// <param name="available">Whether the seating is available or not.</param>
    public void UpdateSeating(int seatingID, int numOfChairs, string area, bool available)
    {
        Seating? seating = _seatings.Find(s => s.SeatingID == seatingID);
        if (seating != null)
        {
            seating.NumOfChairs = numOfChairs;
            seating.Area = area;
            seating.Available = available;
            DatabaseManager db = Program.DatabaseManager;
            db.Query($"UPDATE seating SET amount_of_chairs = {numOfChairs}, area = '{area}', available = {available} WHERE id = {seatingID}");
        }
    }

    public List<Seating> FindAvailableSeating(int num_of_chairs, DateTime date)
    {
        List<Seating> seatings = _seatings.FindAll(s => s.NumOfChairs == num_of_chairs && s.Available == true);
        List<Seating> availableSeatings = new List<Seating>();
        foreach (Seating seating in seatings)
        {
            if (!seating.IsOccupied(date))
            {
                availableSeatings.Add(seating);
            }
        }
        return availableSeatings;
    }

    public List<Seating> FindAvailableSeating(int num_of_chairs, DateTime date, string area)
    {
        List<Seating> seatings = _seatings.FindAll(s => s.NumOfChairs == num_of_chairs && s.Available == true && s.Area.ToLower() == area.ToLower());
        List<Seating> availableSeatings = new List<Seating>();
        foreach (Seating seating in seatings)
        {
            if (!seating.IsOccupied(date))
            {
                availableSeatings.Add(seating);
            }
        }
        return availableSeatings;
    }

    /// <summary>
    /// Gets a seating from the restaurant.
    /// </summary>
    /// <param name="seatingID">The ID of the seating.</param>
    /// <returns>The seating.</returns>
    public Seating? GetSeating(int seatingID)
    {
        Seating? seating = _seatings.Find(s => s.SeatingID == seatingID);
        return seating;
    }

    /// <summary>
    /// Gets all the seatings from the restaurant.
    /// </summary>
    /// <returns>The seatings.</returns>
    private void LoadSeatings()
    {
        DatabaseManager db = Program.DatabaseManager;
        DataTable table = db.GetTable("Seatings");
        foreach (DataRow row in table.Rows)
        {
            int seatingID = Convert.ToInt32(row["id"]);
            int numOfChairs = Convert.ToInt32(row["amount_of_chairs"]);
            string area = row["area"].ToString()!;
            bool available = Convert.ToBoolean(row["available"]);
            AddSeatingNonDB(seatingID, numOfChairs, area, available);
        }
    }

    /// <summary>
    /// Adds a seating to the restaurant (without implementing into the database).
    /// </summary>
    /// <param name="seatingID">The ID of the seating.</param>
    /// <param name="numOfChairs">The amount of chairs in the seating.</param>
    /// <param name="area">The area of the seating.</param>
    /// <param name="available">Whether the seating is available or not.</param>
    private void AddSeatingNonDB(int seatingID, int numOfChairs, string area, bool available)
    {
        Seating seating = new Seating(available, numOfChairs, area, seatingID);
        _seatings.Add(seating);
    }

    public bool SeatingExists(int seatingID)
    {
        return _seatings.Exists(s => s.SeatingID == seatingID);
    }

    public void AddSeating(Seating seating)
    {
        _seatings.Add(seating);
        DatabaseManager db = Program.DatabaseManager;
        db.Query($"INSERT INTO seatings (id, amount_of_chairs, area, available) VALUES ({seating.SeatingID}, {seating.NumOfChairs}, '{seating.Area}', {seating.Available})");
    }

    public void UpdateSeating(int seatingID, Seating seating)
    {
        Seating? oldSeating = _seatings.Find(s => s.SeatingID == seatingID);
        if (oldSeating == null) return;
        _seatings.Remove(oldSeating);
        _seatings.Add(seating);
        DatabaseManager db = Program.DatabaseManager;
        db.Query($"UPDATE Seatings SET id = '{seating.SeatingID}', available = '{seating.Available}', area = '{seating.Area}', amount_of_chairs = '{seating.NumOfChairs}' WHERE id = '{oldSeating.SeatingID}'");
        ReservationManager rm = Program.ReservationManager;
        
    }

    /// <summary>
    /// Checks whether an update to the seating will cause a conflict.
    /// </summary>
    public bool IsConflictedAfterUpdate(Seating oldSeating, Seating newSeating)
    {
        ReservationManager rm = Program.ReservationManager;
        List<Reservation> reservations = rm.Reservations.FindAll(r => r.Seating.FindAll(s => s.SeatingID == oldSeating.SeatingID).Count > 0);
        if(reservations.Count > 0)
        {
            // ID case
            if(oldSeating.SeatingID != newSeating.SeatingID)
            {
                return true;
            }

            // Size case
            if(newSeating.NumOfChairs < oldSeating.SeatingID)
            {
                return true;
            }

            // Available case
            if(!newSeating.Available)
            {
                return true;
            }
        }
        return false;
    }

}