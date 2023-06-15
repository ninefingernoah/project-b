public class Passenger
{
    private int _id;
    private string _email;
    private string _firstName;
    private string _lastName;
    private string _documentNumber;
    private DateTime? _birthDate;
    private Address _address;

    public int Id { get => _id; set => _id = value; }
    public string Email { get => _email; set => _email = value; }
    public string FirstName { get => _firstName; set => _firstName = value; }
    public string LastName { get => _lastName; set => _lastName = value; }
    public string DocumentNumber { get => _documentNumber; set => _documentNumber = value; }
    public DateTime? BirthDate { get => _birthDate; set => _birthDate = value; }
    public Address Address { get => _address; set => _address = value; }

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

    public bool CanChangeName()
    {
        var res = DatabaseManager.QueryResult($"SELECT * FROM passengers WHERE id = {_id}").Rows[0];
        return (int)(long)res["letters_changed"] == 0;
    }

    public void LockName()
    {
        DatabaseManager.QueryNonResult($"UPDATE passengers SET letters_changed = 1 WHERE id = {_id}");
    }

    public override string ToString()
    {
        return $"Naam: {FirstName} {LastName}\nGeboorte datum: {BirthDate}\nEmail: {Email}\nDocument nummer: {DocumentNumber}";
    }
}