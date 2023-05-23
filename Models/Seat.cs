public class Seat
{
    private string _number;
    private string _color;
    private double _price;

    public string Number { get => _number; set => _number = value; }
    public string Color { get => _color; set => _color = value; }
    public double Price { get => _price; set => _price = value; }

    public Seat(string number, string color) {
        _number = number;
        _color = color;
    }
    public Seat(string number, int color, double price)
    {
        _number = number;
        _color = color;
        _price = price;
    }

}