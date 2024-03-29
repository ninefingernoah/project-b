/// <summary>
/// The controller for the flights.
/// </summary>
public class FlightController
{
    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    private static readonly FlightController instance = new FlightController();

    Flight? chosenFlight;

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
    /// Returns the flight that was chosen by the user in the selection menu.
    /// </summary>
    /// <returns>The chosen flight.</returns>
    public Flight? GetChosenFlight()
    {
        return chosenFlight;
    }



    /// <summary>
    /// Displays the menu for booking a flight and handles the user input.
    /// </summary>
    /// <param name="flight">The flight to book.</param>
    public void ShowFlight(Flight flight)
    {
        if (UserManager.IsLoggedIn() && UserManager.GetCurrentUser() != null && UserManager.GetCurrentUser()!.IsAdmin())
        {
            ShowFlightAdmin(flight);
            return;
        }

        FlightView flightView = FlightView.Instance;
        flightView.Display(flight);
        try
        {
            int selection = int.Parse(flightView.ViewBag["FlightViewSelection"]);
            switch (selection)
            {
                case 0:
                    chosenFlight = flight;
                    break;
                case 1:
                    FlightListController.Instance.ShowFlights();
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
    // public Flight? GetFlightType()
    // {
    //     // ask for menu here
    //     List<string> list = new List<string>()
    //     {
    //         "Retour",
    //         "Enkele",
    //         "Last minute",
    //         "Terug"
    //     };
    //     MenuView.Instance.ClearViewBag();
    //     MenuView.Instance.Display("Wat voor type vlucht wil je", list);
    //     int choice = MenuView.Instance.LastChoice;
    //     // if (choice != list.Count - 1)
    //     // {
    //     //     return FlightListController.Instance.SelectFlight(MenuView.Instance.ViewBag["Selection"]);
    //     // }

    //     return null;
    // }

    /// <summary>
    /// Displays the flight menu for admins and handles the user input.
    /// </summary>
    /// <param name="flight">The flight to edit.</param>
    public void ShowFlightAdmin(Flight flight)
    {
        FlightView flightView = FlightView.Instance;
        flightView.Display(flight);
        try
        {
            int selection = int.Parse(flightView.ViewBag["FlightViewSelection"]);
            switch (selection)
            {
                case 0:
                    chosenFlight = flight;
                    break;
                case 1:
                    ShowFlightEditor(flight);
                    break;
                case 2:
                    string warning = (flight.TakenSeats.Count > 0 ? "Er zijn nog stoelen geboekt voor deze vlucht." : "");
                    warning += "Weet u zeker dat u deze vlucht wilt verwijderen?\nDeze actie kan niet ongedaan worden gemaakt.";
                    warning += "\n\n" + flight.ToString();
                    if (ConsoleUtils.Confirm(warning))
                    {
                        FlightManager.DeleteFlight(flight.Id);
                        ConsoleUtils.Success("Vlucht verwijderd.");
                    }
                    else
                    {
                        ConsoleUtils.Warn("Vlucht niet verwijderd.");
                    }
                    FlightListController.Instance.ShowFlightSearchMenu();
                    break;
                case 3:
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

    public void NewFlight()
    {
        int selection = NewFlightView.Instance.Display();
        switch (selection)
        {
            case 0: // Departure
                Airport? temp = GetAirport();
                if (temp != null)
                {
                    NewFlightView.Instance.Departure = temp;
                }
                NewFlight();
                break;
            case 1: // Destination
                temp = GetAirport();
                if (temp != null)
                {
                    NewFlightView.Instance.Arrival = temp;
                }
                NewFlight();
                break;
            case 2: // Airplane
                Airplane? airplane = GetAirplane();
                if (airplane != null)
                {
                    Airplane oldPlane = NewFlightView.Instance.Plane;
                    if (oldPlane != null)
                    {
                        if (airplane.TotalCapacity < oldPlane.TotalCapacity)
                            ConsoleUtils.Warn($"Let op: Het nieuwe vliegtuig ({airplane}) heeft een kleinere capaciteit dan ({oldPlane}). Sommige stoelen zullen moeten worden herboekt.");

                    }
                    NewFlightView.Instance.Plane = airplane;
                }
                NewFlight();
                break;
            case 3: // Departure time
                DateTime? departureTime = GetDateTime();
                if (departureTime != null)
                {
                    NewFlightView.Instance.depTime = departureTime.Value;
                }
                NewFlight();
                break;
            case 4: // Arrival time
                DateTime? arrivalTime = GetDateTime();
                if (arrivalTime != null)
                {
                    // Check if arrival time is after departure time. You can't arrive before you depart.
                    if (arrivalTime < NewFlightView.Instance.depTime)
                    {
                        ConsoleUtils.Error("De aankomsttijd kan niet voor de vertrektijd liggen.");
                    }
                    else
                    {
                        NewFlightView.Instance.ArrTime = arrivalTime.Value;
                    }
                }
                NewFlight();
                break;
            case 6: //Save
                if (NewFlightView.Instance.CheckValid())
                {
                    try
                    {
                        int id = FlightManager.GetNewestId();
                        FlightManager.NewFlight(new Flight(id, NewFlightView.Instance.Departure, NewFlightView.Instance.Arrival, NewFlightView.Instance.depTime, NewFlightView.Instance.ArrTime, NewFlightView.Instance.Plane));
                        string departcode = NewFlightView.Instance.Departure.Code;
                        string destinationcode = NewFlightView.Instance.Arrival.Code;
                        ConsoleUtils.Success($"Vlucht #{id} ({departcode} -> {destinationcode}) succesvol opgeslagen.");
                        FlightEditorView.Instance.ClearViewBag();
                        MainMenuController.Instance.ShowMainMenu();
                    }
                    catch (Exception e)
                    {
                        ConsoleUtils.Error(e.Message);
                        NewFlightView.Instance.ClearViewBag();
                        MainMenuController.Instance.ShowMainMenu();
                        break;
                    }

                }
                else
                {
                    NewFlight();
                }

                break;
            case 8: // Go back
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }


    /// <summary>
    /// Displays the menu for booking a flight and handles the user input. This method populates the viewbag with the flight.
    /// </summary>
    /// <param name="flight">The flight to edit.</param>
    public void ShowFlightEditor(Flight flight)
    {
        FlightEditorView.Instance.PopulateViewBag(flight);
        ShowFlightEditor();
    }

    /// <summary>
    /// Displays the menu for booking a flight and handles the user input. This method assumes the viewbag is already populated with the flight.
    /// </summary>
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
                if(departureTime == null)
                {
                    ShowFlightEditor();
                    break;
                }
                FlightEditorView.Instance.CurrentFlight.DepartureTime = departureTime.Value;
                FlightEditorView.Instance.CurrentFlight.ArrivalTime = default(DateTime);

                ShowFlightEditor();
                break;
            case 4: // Arrival time
                DateTime? arrivalTime = GetDateTime();
                if (arrivalTime == null)
                {
                    ShowFlightEditor();
                    break;
                }
                // Check if arrival time is after departure time. You can't arrive before you depart.
                if (arrivalTime < FlightEditorView.Instance.CurrentFlight.DepartureTime)
                {
                    ConsoleUtils.Error("De aankomsttijd kan niet voor de vertrektijd liggen.");
                }
                else
                {
                    FlightEditorView.Instance.CurrentFlight.ArrivalTime = arrivalTime.Value;
                }
                ShowFlightEditor();
                break;
            case 6: //Save
                if (FlightEditorView.Instance.CurrentFlight.ArrivalTime < FlightEditorView.Instance.CurrentFlight.DepartureTime)
                {
                    ConsoleUtils.Error("De aankomsttijd kan niet voor de vertrektijd liggen.");
                    ShowFlightEditor();
                    return;
                }
                if(FlightEditorView.Instance.CurrentFlight.ArrivalTime < DateTime.Now || FlightEditorView.Instance.CurrentFlight.DepartureTime < DateTime.Now)
                {
                    ConsoleUtils.Error("De vlucht kan niet in het verleden plaatsvinden.");
                    ShowFlightEditor();
                    return;
                }
                try
                {
                    FlightManager.UpdateFlight(FlightEditorView.Instance.CurrentFlight);
                }
                catch (Exception e)
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

    /// <summary>
    /// Prompts the user for a date and time. Returns null if the user cancels.
    /// </summary>
    /// <returns>DateTime or null</returns>
    private DateTime? GetDateTime()
    {
        StringInputMenu menu = new StringInputMenu("Voer een datum in (dd-mm-jjjj), of 'Terug' om terug te gaan.");
        string? input = menu.Run();
        if (input == null || input.ToLower() == "terug" || input == "") return null;

        // Validate input
        if (!DateTime.TryParse(input, out DateTime date))
        {
            ConsoleUtils.Error("Dat is geen geldige datum.");
            return null;
        }

        DateTime now = DateTime.Now;

        // Check if date is not in the past
        if (date < now && date.Day != now.Day)
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

    /// <summary>
    /// Prompts the user to select an airport from a list of airports.
    /// </summary>
    /// <returns>Airport or null</returns>
    public Airport? GetAirport()
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
    /// <returns>Airplane or null</returns>
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
