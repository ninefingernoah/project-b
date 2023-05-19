public class Passenger
{
    private int _id;
    private string _email;
    private string _firstName;
    private string _lastName;
    private string _documentNumber;
    private DateTime? _birthDate;

    public int Id { get => _id; set => _id = value; }
    public string Email { get => _email; set => _email = value; }
    public string FirstName { get => _firstName; set => _firstName = value; }
    public string LastName { get => _lastName; set => _lastName = value; }
    public string DocumentNumber { get => _documentNumber; set => _documentNumber = value; }
    public DateTime? BirthDate { get => _birthDate; set => _birthDate = value; }

    public Passenger(int id, string email, string firstName, string lastName, string documentNumber, DateTime? birthDate)
    {
        _id = id;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _documentNumber = documentNumber;
        _birthDate = birthDate;
    }

    public override string ToString()
    {
        return $"Naam: {FirstName} {LastName}\nGeboorte datum: {BirthDate}\nEmail: {Email}\nDocument nummer: {DocumentNumber}";
    }
}