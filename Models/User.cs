public class User {
    private int _id;
    private string _email;
    private string _firstName;
    private string _lastName;
    /// <summary> The role of the user. (e.g. ADMIN, USER) </summary>
    private string _role;

    public int Id { get => _id; set => _id = value; }
    public string Email { get => _email; set => _email = value; }
    public string FirstName { get => _firstName; set => _firstName = value; }
    public string LastName { get => _lastName; set => _lastName = value; }
    public string Role { get => _role; set => _role = value; }

    public User(int id, string email, string firstName, string lastName, string role) {
        _id = id;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _role = role;
    }

    /// <summary>
    /// Checks if the user is an admin.
    /// </summary>
    /// <returns>Returns true if the user is an admin, false if not.</returns>
    public Boolean IsAdmin() {
        return _role.ToLower() == "admin";
    }

    /// <summary>
    /// Gets the reservations of the user.
    /// </summary>
    /// <returns>Returns a list of reservations.</returns>
    public List<Reservation> GetReservations()
    {
        return ReservationManager.GetReservationsByUser(this);
    }

    /// <summary>
    /// Gets the amount of reservations the user has.
    /// </summary> 
    /// <returns>Returns the amount of reservations the user has.</returns>
    public int GetReservationCount()
    {
        var res = DatabaseManager.QueryResult($"SELECT COUNT(*) FROM reservations WHERE user_id = {Id}").Rows[0];
        return (int)(long)res["Count(*)"];
    }
}