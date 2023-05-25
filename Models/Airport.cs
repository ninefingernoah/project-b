public class Airport
{
    private int _id;
    private string _country;
    private string _city;

    /// Dict<plane_id, Dict<color_rank, price>>
    public Dictionary<int, Dictionary<int, int>> _prices;

    public int Id { get => _id; set => _id = value; }
    public string Country { get => _country; set => _country = value; }
    public string City { get => _city; set => _city = value; }
    public Dictionary<int, Dictionary<int, int>> Prices { get => _prices; set => _prices = value; }

    public Airport(int id, string country, string city, Dictionary<int, Dictionary<int, int>> prices)
    {
        _id = id;
        _country = country;
        _city = city;
        _prices = prices;
    }
}