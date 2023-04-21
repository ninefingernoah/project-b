public class Location
{
    private int _id;
    private string _name;

    /// Dict<plane_id, Dict<color_rank, price>>
    public Dictionary<int, Dictionary<int, int>> _prices;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public Dictionary<int, Dictionary<int, int>> Prices { get => _prices; set => _prices = value; }

    public Location(int id, string name, Dictionary<int, Dictionary<int, int>> prices)
    {
        _id = id;
        _name = name;
        _prices = prices;
    }
}