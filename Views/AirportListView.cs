public class AirportListView  {
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();
    private static readonly AirportListView instance = new AirportListView();

    static AirportListView() {
    }
    private AirportListView() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static AirportListView Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Displays a list of airports.
    /// </summary>
    public void Display(List<Airport> airports) {
        List<string> optionsList = new List<string>();
        foreach (Airport airport in airports) {
            optionsList.Add($"{airport.Id} - {airport.Name} {airport.Code}, {airport.City}, {airport.Country}");
        }
        optionsList.Add("Terug");
        string[] options = optionsList.ToArray();
        Menu mainMenu = new Menu("Luchthavens", options);
        int choice = mainMenu.Run();
        ViewBag["AirportListSelection"] = choice.ToString();
    }
}