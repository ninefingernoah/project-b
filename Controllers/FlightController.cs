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
            case 0: // Departure
                Airport? temp = GetAirport();
                if (temp != null)
                {
                    FlightEditorView.Instance.CurrentFlight.Departure = temp;
                }
                ShowFlightEditor();
                break;
            case 1: // Destination
                temp = GetAirport();
                if (temp != null)
                {
                    FlightEditorView.Instance.CurrentFlight.Destination = temp;
                }
                ShowFlightEditor();
                break;
            case 2: // Airplane
                Airplane? airplane = GetAirplane();
                if (airplane != null)
                {
                    Airplane oldPlane = FlightEditorView.Instance.CurrentFlight.Airplane;
                    if (airplane.TotalCapacity < oldPlane.TotalCapacity)
                        ConsoleUtils.Warn($"Let op: Het nieuwe vliegtuig ({airplane}) heeft een kleinere capaciteit dan ({oldPlane}). Sommige stoelen zullen moeten worden herboekt.");
                    FlightEditorView.Instance.CurrentFlight.Airplane = airplane;
                }
                ShowFlightEditor();
                break;
            case 3: // Departure time
                DateTime? departureTime = GetDateTime();
                if (departureTime != null)
                {
                    FlightEditorView.Instance.CurrentFlight.DepartureTime = departureTime.Value;
                }
                ShowFlightEditor();
                break;
            case 4: // Arrival time
                DateTime? arrivalTime = GetDateTime();
                if (arrivalTime != null)
                {
                    // Check if arrival time is after departure time. You can't arrive before you depart.
                    if (arrivalTime < FlightEditorView.Instance.CurrentFlight.DepartureTime)
                    {
                        ConsoleUtils.Error("De aankomsttijd kan niet voor de vertrektijd liggen.");
                    }
                    else
                    {
                        FlightEditorView.Instance.CurrentFlight.ArrivalTime = arrivalTime.Value;
                    }
                }
                ShowFlightEditor();
                break;
            case 6: //Save
                try
                {
                    FlightManager.UpdateFlight(FlightEditorView.Instance.CurrentFlight);
                } catch(Exception e)
                {
                    ConsoleUtils.Error(e.Message);
                    FlightEditorView.Instance.ClearViewBag();
                    MainMenuController.Instance.ShowMainMenu();
                    break;
                }
                int flightId = FlightEditorView.Instance.CurrentFlight.Id;
                string departcode = FlightEditorView.Instance.CurrentFlight.Departure.Code;
                string destinationcode = FlightEditorView.Instance.CurrentFlight.Destination.Code;
                ConsoleUtils.Success($"Vlucht #{flightId} ({departcode} -> {destinationcode}) succesvol opgeslagen.");
                FlightEditorView.Instance.ClearViewBag();
                MainMenuController.Instance.ShowMainMenu();
                break;
            case 7: // Delete
                MainMenuController.Instance.ShowMainMenu();
                break;
            case 9: // Go back
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }

    private DateTime? GetDateTime()
    {
        StringInputMenu menu = new StringInputMenu("Voer een datum in (dd-mm-jjjj), of 'Terug' om terug te gaan.");
        string? input = menu.Run();
        if (input == "Terug" || input == "") return null;

        // Validate input
        if (!DateTime.TryParse(input, out DateTime date))
        {
            ConsoleUtils.Error("Dat is geen geldige datum.");
            return null;
        }

        DateTime now = DateTime.Now;

        // Check if date is not in the past
        if(date < now && date.Day != now.Day)
        {
            ConsoleUtils.Error("Deze datum ligt in het verleden.");
            return null;
        }

        StringInputMenu menu2 = new StringInputMenu("Voer een tijd in (uu:mm), of 'Terug' om terug te gaan.");
        string? input2 = menu2.Run();
        if (input2 == "Terug" || input2 == "") return null;
        if (!TimeOnly.TryParse(input2, out TimeOnly time))
        {
            ConsoleUtils.Error("Dat is geen geldige tijd.");
            return null;
        }

        // Combine date and time
        DateTime dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

        // Check if date is not in the past
        if (dateTime < now)
        {
            ConsoleUtils.Error("Deze combinatie van tijd en datum liggen in het verleden.");
            return null;
        }
        return dateTime;
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

    /// <summary>
    /// Prompts the user to select an airplane from a list of airplanes.
    /// </summary>
    private Airplane? GetAirplane()
    {
        List<Airplane> airplanes = AirplaneManager.GetAirplanes();
        string[] options = new string[airplanes.Count + 1];
        for (int i = 0; i < airplanes.Count; i++)
        {
            options[i] = airplanes[i].Name + " (" + airplanes[i].TotalCapacity + " seats)";
        }
        options[airplanes.Count] = "Terug";
        Menu menu = new Menu("Selecteer een vliegtuig", options);
        int selection = menu.Run();
        if (selection == airplanes.Count) return null;
        return airplanes[selection];
    }
}
