public class FlightController
{
    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    private static readonly FlightController instance = new FlightController();

    static FlightController()
    {
    }
    private FlightController()
    {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static FlightController Instance
    {
        get
        {
            return instance;
        }
    }

    public Flight? GetFlightType()
    {
        // ask for menu here
        List<string> list = new List<string>()
        {
            "Retour",
            "Enkele",
            "Last minute",
            "Ga terug"
        };
        MenuView.Instance.ClearViewBag();
        MenuView.Instance.Display("Wat voor type vlucht wil je", list);
        int choice = MenuView.Instance.LastChoice;
        if (choice != list.Count - 1)
        {
            return FlightListController.Instance.SelectFlight(MenuView.Instance.ViewBag["Selection"]);
        }
        else
        {
            return null;
        }
    }

    public void NewFlight()
    {
        MenuView.Instance.ClearViewBag();
        int Id = FlightManager.GetNewestId();
        string Departure;
        string Destination;
        Airplane? airplane;

        DateTime DepTime = new DateTime();
        DateTime ArrivalTime = new DateTime();

        do
        {
            List<string> options = new List<string>()
        {
            "Vertrek locatie",
            "Vertrek tijd",
            "Aankomst locatie",
            "Aankomst tijd",
            "Vliegtuig",
            "-",
            "Terug"

        };
            MenuView.Instance.Display("Nieuwe vlucht", options, ViewBagBool: true);

            switch (MenuView.Instance.LastChoice)
            {
                case 0:
                    // Get departure location
                    Airport airportDep = GetAirport("Van welk vliegveld moet deze vlucht vertrekken?");
                    Departure = airportDep.City;
                    MenuView.Instance.ViewBag["Vertrek Locatie"] = Departure;
                    break;
                case 1:
                    // Get Departure Time
                    DepTime = GetTime("Welke tijd moet de vlucht vertrekken?");
                    break;
                case 2:
                    // Get  destination
                    Airport airportDest = GetAirport("Bij welk vliegveld moet deze vlucht aankomen?");
                    Destination = airportDest.City;
                    break;
                case 3:
                    // Get Arrival Time
                    ArrivalTime = GetTime("Welke tijd zou het vliegtuig moeten aankomen?");
                    break;
                case 4:
                    // Get Airplane
                    airplane = new Airplane(0, 0, "1");
                    break;
                default:
                    break;
            }
        } while (true);
        // push to database
        Flight flight = new Flight(Id, Departure, Destination, DepTime, ArrivalTime, airplane);
    }

    private Airport GetAirport(string Question)
    {
        var airports = AirportManager.GetAllAirports();
        List<string> AirportMenu = new List<string>();
        foreach (Airport airport in airports)
        {
            AirportMenu.Add($"Land: {airport.Country}, Stad: {airport.City}");
        }
        MenuView.Instance.Display(Question, AirportMenu);
        int choice = MenuView.Instance.LastChoice;

        return airports[choice];
    }

    private DateTime GetTime(string Question)
    {
        return DateTime.Now;
    }
}
