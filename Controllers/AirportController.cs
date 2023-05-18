public class AirportController
{
    private static readonly AirportController instance = new AirportController();

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
            case 0:
                showAirportCountryInputMenu();
                break;
            case 1:
                showAirportCityInputMenu();
                break;
            case 2:
                showAirportNameInputMenu();
                break;
            case 3:
                showFlightsAndPricesMenu();
                break;
            case 4:
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
        FlightListController.Instance.ShowFlights();
    }
}