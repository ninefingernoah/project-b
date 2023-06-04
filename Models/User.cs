public class User {
    private int _id;
    private string _email;
    private string _firstName;
    private string _lastName;
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

    public Boolean IsAdmin() {
        return _role.ToLower() == "admin";
    }

    public List<Reservation> GetReservations()
    {
        return ReservationManager.GetReservationsByUser(this);
    }

    public int GetReservationCount()
    {
        var res = DatabaseManager.QueryResult($"SELECT COUNT(*) FROM reservations WHERE user_id = {Id}").Rows[0];
        return (int)(long)res["Count(*)"];
    }
}