public class Airport {
    private int _id;
    private string _name;
    private string _city;
    private string _country;
    private string _code;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public string City { get => _city; set => _city = value; }
    public string Country { get => _country; set => _country = value; }
    public string Code { get => _code; set => _code = value; }

    public Airport(int id, string name, string city, string country, string code) {
        _id = id;
        _name = name;
        _city = city;
        _country = country;
        _code = code;
    }
}