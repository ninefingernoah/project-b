/// <summary>
/// A passenger
/// </summary>
public class Passenger
{
    /// <summary>The id of the passenger</summary>
    private int _id;
    /// <summary>The email of the passenger</summary>
    private string _email;
    /// <summary>The first name of the passenger</summary>
    private string _firstName;
    /// <summary>The last name of the passenger</summary>
    private string _lastName;
    /// <summary>The document number of the passenger</summary>
    private string _documentNumber;
    /// <summary>The birth date of the passenger</summary>
    private DateTime? _birthDate;
    /// <summary>The address of the passenger</summary>
    private Address _address;

    /// <summary>The id of the passenger</summary>
    public int Id { get => _id; set => _id = value; }
    /// <summary>The email of the passenger</summary>
    public string Email { get => _email; set => _email = value; }
    /// <summary>The first name of the passenger</summary>
    public string FirstName { get => _firstName; set => _firstName = value; }
    /// <summary>The last name of the passenger</summary>
    public string LastName { get => _lastName; set => _lastName = value; }
    /// <summary>The document number of the passenger</summary>
    public string DocumentNumber { get => _documentNumber; set => _documentNumber = value; }
    /// <summary>The birth date of the passenger</summary>
    public DateTime? BirthDate { get => _birthDate; set => _birthDate = value; }
    /// <summary>The address of the passenger</summary>
    public Address Address { get => _address; set => _address = value; }
    
    /// <summary>
    /// Constructor for Passenger
    /// </summary>
    /// <param name="id">The id of the passenger</param>
    /// <param name="email">The email of the passenger</param>
    /// <param name="firstName">The first name of the passenger</param>
    /// <param name="lastName">The last name of the passenger</param>
    /// <param name="documentNumber">The document number of the passenger</param>
    /// <param name="birthDate">The birth date of the passenger. May be null</param>
    /// <param name="address">The address of the passenger</param>
    public Passenger(int id, string email, string firstName, string lastName, string documentNumber, DateTime? birthDate, Address address)
    {
        _id = id;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _documentNumber = documentNumber;
        _birthDate = birthDate;
        _address = address;
    }

    /// <summary>
    /// Returns whether the passenger can change their name. This is only possible if the passenger has not yet changed their name.
    /// </summary>
    /// <returns>True if the passenger has an unchanged name. False if the name has already been changed once.</returns>
    public bool CanChangeName()
    {
        var res = DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {_id}").Rows[0];
        return (int)(long)res["letters_changed"] == 0;
    }

    /// <summary>
    /// Locks the name of the passenger. This is done after the passenger has changed their name.
    /// </summary>
    public void LockName()
    {
        DatabaseManager.QueryNonResult($"UPDATE passengers SET letters_changed = 1 WHERE id = {_id}");
    }

    /// <summary>
    /// Returns a string representation of the passenger
    /// </summary>
    public override string ToString()
    {
        return $"Naam: {FirstName} {LastName}\nGeboorte datum: {BirthDate}\nEmail: {Email}\nDocument nummer: {DocumentNumber}";
    }
}