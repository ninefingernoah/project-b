public class Seat
{
    private string _number;
    private int _color;
    private double _price;

    public string Number { get => _number; set => _number = value; }
    public int Color { get => _color; set => _color = value; }
    public double Price { get => _price; set => _price = value; }

    public Seat(string number, int color)
    {
        _number = number;
        _color = color;
        _price = 0;
    }
    public Seat(string number, int color, double price)
    {
        _number = number;
        _color = color;
        _price = price;
    }

}