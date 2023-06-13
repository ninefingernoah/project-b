/// <summary>
/// A user
/// </summary>
public class User {
    /// <summary>The id of the user</summary>
    private int _id;
    /// <summary>The email of the user</summary>
    private string _email;
    /// <summary>The first name of the user</summary>
    private string _firstName;
    /// <summary>The last name of the user</summary>
    private string _lastName;
    /// <summary>The role of the user</summary>
    private string _role;

    /// <summary>The id of the user</summary>
    public int Id { get => _id; set => _id = value; }
    /// <summary>The email of the user</summary>
    public string Email { get => _email; set => _email = value; }
    /// <summary>The first name of the user</summary>
    public string FirstName { get => _firstName; set => _firstName = value; }
    /// <summary>The last name of the user</summary>
    public string LastName { get => _lastName; set => _lastName = value; }
    /// <summary>The role of the user</summary>
    public string Role { get => _role; set => _role = value; }

    /// <summary>
    /// Constructor for User
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <param name="email">The email of the user</param>
    /// <param name="firstName">The first name of the user</param>
    /// <param name="lastName">The last name of the user</param>
    /// <param name="role">The role of the user</param>
    public User(int id, string email, string firstName, string lastName, string role) {
        _id = id;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _role = role;
    }

    /// <summary>
    /// Returns whether the user is an admin
    /// </summary>
    /// <returns>True if the user is an admin. False if the user is not an admin.</returns>
    public Boolean IsAdmin() {
        return _role.ToLower() == "admin";
    }

    /// <summary>
    /// Returns the reservations of the user
    /// </summary>
    /// <returns>The reservations of the user</returns>
    public List<Reservation> GetReservations()
    {
        return ReservationManager.GetReservationsByUser(this);
    }


    /// <summary>
    /// Returns the number of reservations of the user
    /// </summary>
    public int GetReservationCount()
    {
        var res = DatabaseManager.QueryResult($"SELECT COUNT(*) FROM reservations WHERE user_id = {Id}").Rows[0];
        return (int)(long)res["Count(*)"];
    }
}