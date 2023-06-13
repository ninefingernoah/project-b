/// <summary>
/// A seat
/// </summary>
public class Seat {
    /// <summary>The number of the seat</summary>
    private string _number;
    /// <summary>The row of the seat</summary>
    private int _row;
    /// <summary>The column of the seat</summary>
    private string _column;
    /// <summary>The color(class) of the seat</summary>
    private string _color;
    /// <summary>The price of the seat</summary>
    private double _price;

    /// <summary>The number of the seat</summary>
    public string Number { get => _number; set => _number = value; }
    /// <summary>The row of the seat</summary>
    public int Row { get => _row; set => _row = value; }
    /// <summary>The column of the seat</summary>
    public string Column { get => _column; set => _column = value; }
    /// <summary>The color(class) of the seat</summary>
    public string Color { get => _color; set => _color = value; }
    /// <summary>The price of the seat</summary>
    public double Price { get => _price; set => _price = value; }

    /// <summary>
    /// Constructor for Seat
    /// </summary>
    /// <param name="number">The number of the seat</param>
    /// <param name="color">The color(class) of the seat</param>
    public Seat(string number, string color) {
        _number = number;
        _color = color;
        // get row from number
        _row = int.Parse(new String(_number.Where(Char.IsDigit).ToArray()));
        // get column from number
        _column = new String(_number.Where(Char.IsLetter).ToArray());
    }
}