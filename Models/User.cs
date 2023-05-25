public class User {
    private int _id;
    private string _email;
    private string _firstName;
    private string _lastName;

    public int Id { get => _id; set => _id = value; }
    public string Email { get => _email; set => _email = value; }
    public string FirstName { get => _firstName; set => _firstName = value; }
    public string LastName { get => _lastName; set => _lastName = value; }

    public User(int id, string email, string firstName, string lastName) {
        _id = id;
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
    }
}