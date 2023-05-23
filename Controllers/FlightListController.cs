public class FlightListController
{
    private static readonly FlightListController instance = new FlightListController();

    static FlightListController()
    {
    }
    private FlightListController()
    {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static FlightListController Instance
    {
        get
        {
            return instance;
        }
    }



    // get all flight data
    // TEMP
    public List<Flight> Allflights(string? filter = null)
    {
        // TODO: get data from Database
        var flights = new List<Flight>();
        Airplane airplane = new Airplane(1, 100, "Boeing je moeder");
        flights.Add(new Flight(1, "Amsterdam", "New York", new DateTime(2019, 1, 1), new DateTime(2019, 1, 2), airplane));
        flights.Add(new Flight(2, "Amsterdam", "New York", new DateTime(2019, 1, 3), new DateTime(2019, 1, 4), airplane));
        flights.Add(new Flight(3, "Amsterdam", "New York", new DateTime(2019, 1, 5), new DateTime(2019, 1, 6), airplane));

        return flights;
    }
    public Flight SelectFlight(string? filter = null)
    {
        var Flights = Allflights(filter);

        List<string> options = Flights.Select(f => f.Destination.ToString()).ToList<string>();

        MenuView.Instance.Display("Welke vlucht mot je", options);
        int choice = MenuView.Instance.LastChoice;

        return Flights[choice];
    }
}