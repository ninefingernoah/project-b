/// <summary>
/// The controller for the airport.
/// </summary>
public class AirportController
{
    private static readonly AirportController instance = new AirportController();
    private AirportController()
    {
    }
    static AirportController()
    {
    }
    public static AirportController Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// Displays the details of an airport.
    /// </summary>
    /// <param name="airport">The airport to display.</param>
    public void ShowAirportDetails(Airport airport)
    {
        AirportDetailsView.Instance.Display(airport);
        int choice = int.Parse(AirportDetailsView.Instance.ViewBag["AirportDetailsSelection"]);
        ShowAirportList();
    }

    /// <summary>
    /// Displays a list of all the airports.
    /// </summary>
    public void ShowAirportList()
    {
        AirportListView.Instance.Display(AirportManager.GetAirports());
        int choice = int.Parse(AirportListView.Instance.ViewBag["AirportListSelection"]);
        if (choice == AirportManager.GetAirports().Count)
        {
            MainMenuController.Instance.ShowMainMenu();
        }
        else
        {
            Airport airport = AirportManager.GetAirports()[choice];
            ShowAirportDetails(airport);
        }
    }

    /// <summary>
    /// Displays the menu for creating an airport.
    /// </summary>
    public void showAirportCreationMenu()
    {
        AirportView.Instance.Display();
        int choice = int.Parse(AirportView.Instance.ViewBag["AirportMenuSelection"]);

        switch (choice)
        {
            case 0: // Country
                showAirportInputMenu("Vul het land in waar het vliegveld zich bevindt:", "AirportCountry");
                break;
            case 1: // City
                showAirportInputMenu("Vul de stad in waar het vliegveld zich bevindt:", "AirportCity");
                break;
            case 2: // Name
                showAirportInputMenu("Vul de naam van het vliegveld in:", "AirportName");
                break;
            case 3: // Code
                showAirportInputMenu("Vul de code van het vliegveld in:", "AirportCode");
                break;
            case 4: // Flights and prices
                showFlightsAndPricesMenu();
                break;
            case 5: // Facilities
                AirportView.Instance.facilities = changeFacilities(AirportView.Instance.facilities);
                showAirportCreationMenu();
                break;
            case 7: // Confirm (adds to the JSON and DB.)
                bool allFieldsFilledIn = AirportView.Instance.AllFieldsFilledIn();
                bool allFIeldsFilledInPrices = AirportSeatAndPricesView.Instance.AllFieldsFilledIn();
                if (!allFieldsFilledIn || !allFIeldsFilledInPrices)
                {
                    ConsoleUtils.Error("Niet alle velden zijn ingevuld. Probeer het opnieuw.", showAirportCreationMenu);
                    return;
                }
                Airport NewAirport = new Airport(0, AirportView.Instance.ViewBag["AirportName"], AirportView.Instance.ViewBag["AirportCity"], AirportView.Instance.ViewBag["AirportCountry"], AirportView.Instance.ViewBag["AirportCode"]);
                JSONManager.AddAirportToJson(NewAirport);
                AirportManager.AddAirport(NewAirport);
                break;
            case 8: // Cancel (Cancels the creation of the airport.)
                AirportView.Instance.ClearViewBag();
                AirportSeatAndPricesView.Instance.ClearViewBag();
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }

    /// <summary>
    /// Displays and handles the menu for changing the facilities of an airport.
    /// </summary>
    /// <param name="facilities">The facilities to change.</param>
    /// <returns>The changed facilities.</returns>
    private List<string> changeFacilities(List<string> facilities)
    {
        string[] options = new string[] { "Toevoegen", "Overzicht", "Verwijderen", "Terug" };
        Menu menu = new Menu("Faciliteiten", options);
        int choice = menu.Run();
        if (choice > 0 && choice < 3)
        {
            if (facilities.Count < 1)
            {
                ConsoleUtils.Error("Er zijn nog geen faciliteiten toegevoegd.");
                return changeFacilities(facilities);
            }
        }
        switch (choice)
        {
            case 0: // Add
                string? facility = askFacility();
                if (facility != null && facility.ToLower() != "terug")
                {
                    facilities.Add(facility);
                }
                changeFacilities(facilities);
                break;
            case 1: // Show
                Console.Clear();
                System.Console.WriteLine("Faciliteiten:");
                foreach (string facility1 in facilities)
                {
                    Console.WriteLine("- " + facility1);
                }
                System.Console.WriteLine("Druk op een toets om terug te gaan.");
                Console.ReadKey();
                changeFacilities(facilities);
                break;
            case 2: // Delete
                List<string> deletionMenuOptions = new List<string>(facilities);
                deletionMenuOptions.Add("-");
                deletionMenuOptions.Add("Terug");
                Menu facilitiesMenu = new Menu("Kies een faciliteit om te verwijderen:", deletionMenuOptions.ToArray());
                int facilityChoice = facilitiesMenu.Run();
                if (facilityChoice != facilities.Count + 1)
                {
                    facilities.RemoveAt(facilityChoice);
                }
                changeFacilities(facilities);
                break;
            case 3:	// Let it end. It will return the facilities.
                break;
        }
        return facilities;
    }

    /// <summary>
    /// Asks the user for a facility.
    /// </summary>
    /// <returns>The facility.</returns>
    private string? askFacility()
    {
        StringInputMenu menu = new StringInputMenu("Vul de faciliteit in die u wilt toevoegen:");
        string? input = menu.Run();
        return input;
    }

    /// <summary>
    /// Asks the user for input and stores it in the ViewBag.
    /// </summary>
    /// <param name="vraag">The question to ask.</param>
    /// <param name="ViewBagNaam">The name of the ViewBag to store the input in.</param>
    private void showAirportInputMenu(string vraag, string ViewBagNaam)
    {
        StringInputMenu menu = new StringInputMenu(vraag);
        string? input = menu.Run();
        if (input == null)
        {
            showAirportCreationMenu();
            return;
        }

        AirportView.Instance.ViewBag[ViewBagNaam] = input!;
        showAirportCreationMenu();
    }

    /// <summary>
    /// Displays the menu for changing the prices of the seats in the planes.
    /// </summary>
    private void showFlightsAndPricesMenu()
    {
        AirportSeatAndPricesView.Instance.Display();
        int choice = int.Parse(AirportSeatAndPricesView.Instance.ViewBag["AirportSeatAndPricesMenuSelection"]);

        switch (choice)
        {
            case 1:
                ShowPlaneClassInputMenu("blauwe", "Boeing 737", "Boeing737Blue");
                break;
            case 2:
                ShowPlaneClassInputMenu("gele", "Boeing 737", "Boeing737Yellow");
                break;
            case 5:
                ShowPlaneClassInputMenu("witte", "Airbus 330", "Airbus330White");
                break;
            case 6:
                ShowPlaneClassInputMenu("donkerblauwe", "Airbus 330", "Airbus330Darkblue");
                break;
            case 7:
                ShowPlaneClassInputMenu("paarse", "Airbus 330", "Airbus330Purple");
                break;
            case 8:
                ShowPlaneClassInputMenu("roze", "Airbus 330", "Airbus330Pink");
                break;
            case 9:
                ShowPlaneClassInputMenu("grijze", "Airbus 330", "Airbus330Grey");
                break;
            case 12:
                ShowPlaneClassInputMenu("witte", "Boeing 787", "Boeing787White");
                break;
            case 13:
                ShowPlaneClassInputMenu("blauwe", "Boeing 787", "Boeing787Blue");
                break;
            case 14:
                ShowPlaneClassInputMenu("oranje", "Boeing 787", "Boeing787Orange");
                break;
            case 16:
                showAirportCreationMenu();
                break;
        }

    }

    /// <summary>
    /// Asks the user to fill in the price for a plane class.
    /// </summary>
    /// <param name="color">The color of the plane class.</param>
    /// <param name="airplane">The plane.</param>
    /// <param name="ViewBagName">The name of the ViewBag to store the input in.</param>
    private void ShowPlaneClassInputMenu(string color, string airplane, string ViewBagName)
    {
        StringInputMenu menu = new StringInputMenu($"Vul de prijs in voor de {color} klasse in de {airplane}:");
        string? price = menu.Run();
        if (price == null)
        {
            showFlightsAndPricesMenu();
            return;
        }

        AirportSeatAndPricesView.Instance.ViewBag[ViewBagName] = price!;
        showFlightsAndPricesMenu();
    }
}