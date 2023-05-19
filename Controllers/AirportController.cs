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
        get { return instance; }
    }

    public void showAirportCreationMenu()
    {
        AirportView.Instance.Display();
        int choice = int.Parse(AirportView.Instance.ViewBag["AirportMenuSelection"]);

        switch(choice)
        {
            case 0: // Country
                showAirportCountryInputMenu();
                break;
            case 1: // City
                showAirportCityInputMenu();
                break;
            case 2: // Name
                showAirportNameInputMenu();
                break;
            case 3: // Flights and prices
                showFlightsAndPricesMenu();
                break;
            case 5: // Confirm (adds to the JSON and DB.)
                break;
            case 6: // Cancel (Cancels the creation of the airport.)
                AirportView.Instance.ClearViewBag();
                AirportSeatAndPricesView.Instance.ClearViewBag();
                MainMenuController.Instance.ShowMainMenu();
                break;
        }
    }

    private void showAirportCountryInputMenu()
    {
        StringInputMenu menu = new StringInputMenu("Vul het land in waar het vliegveld zich bevindt:");
        string? country = menu.Run();
        if (country == null)
        {
            showAirportCreationMenu();
            return;
        }

        AirportView.Instance.ViewBag["AirportCountry"] = country!;
        showAirportCreationMenu();
    }

    private void showAirportCityInputMenu()
    {
        StringInputMenu menu = new StringInputMenu("Vul de stad in waar het vliegveld zich bevindt:");
        string? city = menu.Run();
        if (city == null)
        {
            showAirportCreationMenu();
            return;
        }

        AirportView.Instance.ViewBag["AirportCity"] = city!;
        showAirportCreationMenu();
    }

    private void showAirportNameInputMenu()
    {
        StringInputMenu menu = new StringInputMenu("Vul de naam van het vliegveld in:");
        string? name = menu.Run();
        if (name == null)
        {
            showAirportCreationMenu();
            return;
        }

        AirportView.Instance.ViewBag["AirportName"] = name!;
        showAirportCreationMenu();
    }

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
            case 4:
                ShowPlaneClassInputMenu("witte", "Airbus 330", "Airbus330White");
                break;
            case 5:
                ShowPlaneClassInputMenu("donkerblauwe", "Airbus 330", "Airbus330Darkblue");
                break;
            case 6:
                ShowPlaneClassInputMenu("paarse", "Airbus 330", "Airbus330Purple");
                break;
            case 7:
                ShowPlaneClassInputMenu("roze", "Airbus 330", "Airbus330Pink");
                break;
            case 8:
                ShowPlaneClassInputMenu("grijze", "Airbus 330", "Airbus330Grey");
                break;
            case 10:
                ShowPlaneClassInputMenu("witte", "Boeing 787", "Boeing787White");
                break;
            case 11:
                ShowPlaneClassInputMenu("blauwe", "Boeing 787", "Boeing787Blue");
                break;
            case 12:
                ShowPlaneClassInputMenu("oranje", "Boeing 787", "Boeing787Orange");
                break;
            case 14:
                showAirportCreationMenu();
                break;
        }


    }

    private void ShowPlaneClassInputMenu(string? kleur, string? vliegtuig, string? ViewBagNaam)
    {
        StringInputMenu menu = new StringInputMenu($"Vul de prijs in voor de {kleur} klasse in de {vliegtuig}:");
        string? price = menu.Run();
        if (price == null)
        {
            showFlightsAndPricesMenu();
            return;
        }

        AirportSeatAndPricesView.Instance.ViewBag[ViewBagNaam] = price!;
        showFlightsAndPricesMenu();
    }
}