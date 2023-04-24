public class Seat {
    private string _number;
    private string _color;
    private double _priceLON;
    private double _priceMAD;
    private double _priceSTO;
    private double _priceBER;
    private double _pricePRA;
    private double _priceROM;

    public string Number { get => _number; set => _number = value; }
    public string Color { get => _color; set => _color = value; }
    public double PriceLON { get => _priceLON; set => _priceLON = value; }
    public double PriceMAD { get => _priceMAD; set => _priceMAD = value; }
    public double PriceSTO { get => _priceSTO; set => _priceSTO = value; }
    public double PriceBER { get => _priceBER; set => _priceBER = value; }
    public double PricePRA { get => _pricePRA; set => _pricePRA = value; }
    public double PriceROM { get => _priceROM; set => _priceROM = value; }

    public Seat(string number, string color, double priceLON, double priceMAD, double priceSTO, double priceBER, double pricePRA, double priceROM) {
        _number = number;
        _color = color;
        _priceLON = priceLON;
        _priceMAD = priceMAD;
        _priceSTO = priceSTO;
        _priceBER = priceBER;
        _pricePRA = pricePRA;
        _priceROM = priceROM;
    }
}