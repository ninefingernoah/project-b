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
        MenuView.Instance.Display("Wat voor type vlucht wil je", list, "top", "bot");
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
}
