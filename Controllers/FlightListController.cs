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
        flights = FlightManager.GetAllFlights();

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