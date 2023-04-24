public class User {
    private int _id;
    private string _email;
    private string _firstName;
    private string _lastName;
    private string _password;

    public int Id { get => _id; set => _id = value; }
    public string Email { get => _email; set => _email = value; }
    public string FirstName { get => _firstName; set => _firstName = value; }
    public string LastName { get => _lastName; set => _lastName = value; }
    public string Password { get => _password; set => _password = value; }

    public User(int id, string email, string firstName, string lastName, string password) {
        _id = id;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
        _password = password;
    }
}