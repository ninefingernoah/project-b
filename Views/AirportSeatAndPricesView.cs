/// <summary>
/// View for displaying the seat and prices of the different classes for editing them. Singleton.
public class AirportSeatAndPricesView : IView
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    private static readonly AirportSeatAndPricesView instance = new AirportSeatAndPricesView();
    
    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    /// <summary>
    /// The getter for the singleton instance
    /// </summary>
    public static AirportSeatAndPricesView Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Displays the view.
    /// </summary>
    public void Display()
    {
        populateViewBag();

        List<string> optionsList = new List<string>() // TODO: Maak dit niet gehardcode. Haal de data uit een nog niet gekozen bron en maak het een modulaire manier dat er een nieuw soortvliegtuig makkelijk aan toegevoegd kan worden.
        {
            $"Boeing 737:",
            $"   Blue:{ViewBag["Boeing737Blue"]}",
            $"   Yellow: {ViewBag["Boeing737Yellow"]}",
            "-", //Divider
            $"Airbus 330:",
            $"   White:{ViewBag["Airbus330White"]}",
            $"   Darkblue: {ViewBag["Airbus330Darkblue"]}",
            $"   Purple: {ViewBag["Airbus330Purple"]}",
            $"   Pink: {ViewBag["Airbus330Pink"]}",
            $"   Grey(Business Class): {ViewBag["Airbus330Grey"]}",
            "-", //Divider
            $"Boeing 787:",
            $"   White:{ViewBag["Boeing787White"]}",
            $"   Blue: {ViewBag["Boeing787Blue"]}",
            $"   Orange(Business Class): {ViewBag["Boeing787Orange"]}",
            "-", //Divider
            "Terug"
        };

        string[] options = optionsList.ToArray();

        Menu AirportSeatAndPricesMenu = new Menu("Prijzen van de vliegklassen", options);
        int choice = AirportSeatAndPricesMenu.Run();
        ViewBag["AirportSeatAndPricesMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// Populates the viewbag with the current prices.
    /// </summary>
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

    /// <summary>
    /// Checks if all fields are filled in.
    /// </summary>
    public bool AllFieldsFilledIn()
    {
        foreach(KeyValuePair<string, string> entry in ViewBag)
        {
            if(entry.Value == "<Vul In>")
                return false;
        }
        return true;
    }

    /// <summary>
    /// Clears the viewbag.
    /// </summary>
    public void ClearViewBag()
    {
        ViewBag.Clear();
    }
}