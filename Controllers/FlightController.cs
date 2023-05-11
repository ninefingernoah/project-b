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

    /// <summary>
    /// Displays the menu for booking a flight and handles the user input.
    /// </summary>
    /// <param name="flight">The flight to book.</param>
    public void ShowFlight(Flight flight)
    {
        FlightView flightView = FlightView.Instance;
        flightView.Display(flight);
        try
        {
            int selection = int.Parse(flightView.ViewBag["FlightViewSelection"]);
            switch (selection)
            {
                case 0:
                    //_seatController.Run(flight);
                    break;
                case 1:
                    MainMenuController.Instance.ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("Er is iets fout gegaan.");
                    // return to main menu
                    break;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }

    /// <summary>
    public void ShowFlightEditor(Flight flight)
    {
        FlightEditorView.Instance.PopulateViewBag(flight);
        ShowFlightEditor();
    }

    private void ShowFlightEditor()
    {
        if (FlightEditorView.Instance.CurrentFlight == null)
        {
            throw new Exception("Flight is null");
        }
        FlightEditorView.Instance.Display();
        int selection = int.Parse((string)FlightEditorView.Instance.ViewBag["MainMenuSelection"]);
        switch (selection)
        {
            case 0:
                Airport? temp = GetAirport();
                if (temp != null)
                {
                    FlightEditorView.Instance.CurrentFlight.Departure = temp;
                }
                ShowFlightEditor();
                break;
            case 1:
                temp = GetAirport();
                if (temp != null)
                {
                    FlightEditorView.Instance.CurrentFlight.Destination = temp;
                }
                ShowFlightEditor();
                break;
        }
    }

    private Airport? GetAirport()
    {
        List<Airport> airports = AirportManager.GetAirports();
        string[] options = new string[airports.Count + 1];
        for (int i = 0; i < airports.Count; i++)
        {
            options[i] = airports[i].Name + " (" + airports[i].Code + ")" + " - " + airports[i].City + ", " + airports[i].Country;
        }
        options[airports.Count] = "Terug";
        Menu menu = new Menu("Selecteer een luchthaven", options);
        int selection = menu.Run();
        if (selection == airports.Count)
        {
            return null;
        }
        else
        {
            return airports[selection];
        }
    }

    internal void ShowFlightEditor(object value)
    {
        throw new NotImplementedException();
    }
}
