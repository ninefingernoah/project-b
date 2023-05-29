public class Seat {
    private string _number;
    private int _row;
    private string _column;
    private string _color;
    private double _price;

    public string Number { get => _number; set => _number = value; }
    public int Row { get => _row; set => _row = value; }
    public string Column { get => _column; set => _column = value; }
    public string Color { get => _color; set => _color = value; }
    public double Price { get => _price; set => _price = value; }

    public Seat(string number, string color) {
        _number = number;
        _color = color;
        // get row from number
        _row = int.Parse(new String(_number.Where(Char.IsDigit).ToArray()));
        // get column from number
        _column = new String(_number.Where(Char.IsLetter).ToArray());
    }
}