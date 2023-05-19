public class AirportSeatAndPricesView : IView
{
    private static readonly AirportSeatAndPricesView instance = new AirportSeatAndPricesView();
    
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    public static AirportSeatAndPricesView Instance
    {
        get { return instance; }
    }

    public void Display()
    {
        populateViewBag();

        List<string> optionsList = new List<string>()
        {
            $"Boeing 737:\n   Blue:{ViewBag["Boeing737Blue"]}\n   Yellow: {ViewBag["Boeing737Yellow"]}",
            $"Airbus 330:\n   White:{ViewBag["Airbus330White"]}\n   Darkblue: {ViewBag["Airbus330Darkblue"]}\n   Purple: {ViewBag["Airbus330Purple"]}\n   Pink: {ViewBag["Airbus330Pink"]}\n   Grey(Business Class): {ViewBag["Airbus330Grey"]}",
            $"Boeing 787:\n   White:{ViewBag["Boeing787White"]}\n   Blue: {ViewBag["Boeing787Blue"]}\n   Orange(Business Class): {ViewBag["Boeing787Orange"]}",
            "-",
            "terug"
        };

        string[] options = optionsList.ToArray();

        Menu AirportSeatAndPricesMenu = new Menu("Prijzen van de vliegklassen", options);
        int choice = AirportSeatAndPricesMenu.Run();
        ViewBag["AirportSeatAndPricesMenuSelection"] = choice.ToString();
    }

    private void populateViewBag()
    {
        if(!ViewBag.ContainsKey("Boeing737Blue")) //Boeing 737 Blue
            ViewBag["Boeing737Blue"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Boeing737Yellow")) //Boeing 737 Yellow
            ViewBag["Boeing737Yellow"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Airbus330White")) //Airbus 330 White
            ViewBag["Airbus330White"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Airbus330Darkblue")) //Airbus 330 Darkblue
            ViewBag["Airbus330Darkblue"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Airbus330Purple")) //Airbus 330 Purple
            ViewBag["Airbus330Purple"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Airbus330Pink")) //Airbus 330 Pink
            ViewBag["Airbus330Pink"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Airbus330Grey")) //Airbus 330 Grey
            ViewBag["Airbus330Grey"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Boeing787White")) //Boeing 787 White
            ViewBag["Boeing787White"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Boeing787Blue")) //Boeing 787 Blue 
            ViewBag["Boeing787Blue"] = "<Vul In>";
        if(!ViewBag.ContainsKey("Boeing787Orange")) //Boeing 787 Orange
            ViewBag["Boeing787Orange"] = "<Vul In>";
    }

    public void ClearViewBag() //Clears the viewbag
    {
        ViewBag.Clear();
    }
}